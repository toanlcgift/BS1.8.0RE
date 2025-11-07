using System;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x020003BC RID: 956
public class AnnotatedBeatmapLevelCollectionsViewController : ViewController
{
	// Token: 0x14000091 RID: 145
	// (add) Token: 0x06001192 RID: 4498 RVA: 0x00042958 File Offset: 0x00040B58
	// (remove) Token: 0x06001193 RID: 4499 RVA: 0x00042990 File Offset: 0x00040B90
	public event Action<IAnnotatedBeatmapLevelCollection> didSelectAnnotatedBeatmapLevelCollectionEvent;

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001194 RID: 4500 RVA: 0x0000D616 File Offset: 0x0000B816
	public IAnnotatedBeatmapLevelCollection selectedAnnotatedBeatmapLevelCollection
	{
		get
		{
			if (this._annotatedBeatmapLevelCollections != null && this._annotatedBeatmapLevelCollections.Length > this._selectedItemIndex)
			{
				return this._annotatedBeatmapLevelCollections[this._selectedItemIndex];
			}
			return null;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001195 RID: 4501 RVA: 0x0000D63F File Offset: 0x0000B83F
	public int selectedItemIndex
	{
		get
		{
			return this._selectedItemIndex;
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000429C8 File Offset: 0x00040BC8
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._annotatedBeatmapLevelCollectionsTableView.didSelectAnnotatedBeatmapLevelCollectionEvent += this.handleDidSelectAnnotatedBeatmapLevelCollection;
			this._annotatedBeatmapLevelCollectionsTableView.SelectAndScrollToCellWithIdx(this._selectedItemIndex);
		}
		this._annotatedBeatmapLevelCollectionsTableView.RefreshAvailability();
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0000D647 File Offset: 0x0000B847
	public void RefreshAvailability()
	{
		this._annotatedBeatmapLevelCollectionsTableView.RefreshAvailability();
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0000D654 File Offset: 0x0000B854
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._annotatedBeatmapLevelCollectionsTableView.didSelectAnnotatedBeatmapLevelCollectionEvent -= this.handleDidSelectAnnotatedBeatmapLevelCollection;
		}
		this._annotatedBeatmapLevelCollectionsTableView.CancelAsyncOperations();
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x00042A24 File Offset: 0x00040C24
	public void SetData(IAnnotatedBeatmapLevelCollection[] annotatedBeatmapLevelCollections, int selectedItemIndex, bool hideIfOneOrNoPacks)
	{
		this._annotatedBeatmapLevelCollections = annotatedBeatmapLevelCollections;
		this._annotatedBeatmapLevelCollectionsTableView.SetData(this._annotatedBeatmapLevelCollections);
		this._selectedItemIndex = selectedItemIndex;
		if (base.isInViewControllerHierarchy)
		{
			this._annotatedBeatmapLevelCollectionsTableView.SelectAndScrollToCellWithIdx(selectedItemIndex);
		}
		if (annotatedBeatmapLevelCollections == null || (hideIfOneOrNoPacks && annotatedBeatmapLevelCollections.Length < 2))
		{
			this._annotatedBeatmapLevelCollectionsTableView.Hide();
		}
		else
		{
			this._annotatedBeatmapLevelCollectionsTableView.Show();
		}
		this._loadingControl.Hide();
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0000D692 File Offset: 0x0000B892
	public void ShowLoading()
	{
		this._annotatedBeatmapLevelCollectionsTableView.Hide();
		this._loadingControl.ShowLoading();
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0000D647 File Offset: 0x0000B847
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this._annotatedBeatmapLevelCollectionsTableView.RefreshAvailability();
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00042A94 File Offset: 0x00040C94
	private void handleDidSelectAnnotatedBeatmapLevelCollection(AnnotatedBeatmapLevelCollectionsTableView levelPacksTableView, IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		for (int i = 0; i < this._annotatedBeatmapLevelCollections.Length; i++)
		{
			if (annotatedBeatmapLevelCollection == this._annotatedBeatmapLevelCollections[i])
			{
				this._selectedItemIndex = i;
				break;
			}
		}
		Action<IAnnotatedBeatmapLevelCollection> action = this.didSelectAnnotatedBeatmapLevelCollectionEvent;
		if (action == null)
		{
			return;
		}
		action(annotatedBeatmapLevelCollection);
	}

	// Token: 0x04001162 RID: 4450
	[SerializeField]
	private AnnotatedBeatmapLevelCollectionsTableView _annotatedBeatmapLevelCollectionsTableView;

	// Token: 0x04001163 RID: 4451
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x04001164 RID: 4452
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x04001166 RID: 4454
	private int _selectedItemIndex;

	// Token: 0x04001167 RID: 4455
	private IAnnotatedBeatmapLevelCollection[] _annotatedBeatmapLevelCollections;
}
