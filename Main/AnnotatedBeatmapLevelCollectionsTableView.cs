using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x0200041B RID: 1051
public class AnnotatedBeatmapLevelCollectionsTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x140000BD RID: 189
	// (add) Token: 0x060013E2 RID: 5090 RVA: 0x00049B10 File Offset: 0x00047D10
	// (remove) Token: 0x060013E3 RID: 5091 RVA: 0x00049B48 File Offset: 0x00047D48
	public event Action<AnnotatedBeatmapLevelCollectionsTableView, IAnnotatedBeatmapLevelCollection> didSelectAnnotatedBeatmapLevelCollectionEvent;

	// Token: 0x060013E4 RID: 5092 RVA: 0x00049B80 File Offset: 0x00047D80
	private void Init()
	{
		if (this._isInitialized)
		{
			return;
		}
		this._isInitialized = true;
		this._promoPackIDs = new HashSet<string>();
		foreach (string item in this._promoPackIDStrings)
		{
			this._promoPackIDs.Add(item);
		}
		this._tableView.dataSource = this;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectColumnEvent;
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x0000EE98 File Offset: 0x0000D098
	public void SetData(IAnnotatedBeatmapLevelCollection[] annotatedBeatmapLevelCollections)
	{
		this.Init();
		this._annotatedBeatmapLevelCollections = annotatedBeatmapLevelCollections;
		this._tableView.ReloadData();
		this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
	protected void OnEnable()
	{
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x0000EED9 File Offset: 0x0000D0D9
	protected void OnDisable()
	{
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x0000EEF2 File Offset: 0x0000D0F2
	protected void OnDestroy()
	{
		this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectColumnEvent;
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x0000EF0B File Offset: 0x0000D10B
	public void Hide()
	{
		this._tableView.Hide();
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x0000EF18 File Offset: 0x0000D118
	public void Show()
	{
		this._tableView.Show();
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0000EF25 File Offset: 0x0000D125
	public float CellSize()
	{
		return this._cellWidth;
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0000EF2D File Offset: 0x0000D12D
	public int NumberOfCells()
	{
		if (this._annotatedBeatmapLevelCollections == null)
		{
			return 0;
		}
		return this._annotatedBeatmapLevelCollections.Length;
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x00049BF4 File Offset: 0x00047DF4
	public TableCell CellForIdx(TableView tableView, int idx)
	{
		AnnotatedBeatmapLevelCollectionTableCell annotatedBeatmapLevelCollectionTableCell = tableView.DequeueReusableCellForIdentifier(this._cellReuseIdentifier) as AnnotatedBeatmapLevelCollectionTableCell;
		if (annotatedBeatmapLevelCollectionTableCell == null)
		{
			annotatedBeatmapLevelCollectionTableCell = UnityEngine.Object.Instantiate<AnnotatedBeatmapLevelCollectionTableCell>(this._cellPrefab);
			annotatedBeatmapLevelCollectionTableCell.reuseIdentifier = this._cellReuseIdentifier;
		}
		IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection = this._annotatedBeatmapLevelCollections[idx];
		annotatedBeatmapLevelCollectionTableCell.SetData(annotatedBeatmapLevelCollection);
		if (annotatedBeatmapLevelCollection is IBeatmapLevelPack)
		{
			IBeatmapLevelPack beatmapLevelPack = (IBeatmapLevelPack)annotatedBeatmapLevelCollection;
			annotatedBeatmapLevelCollectionTableCell.showNewRibbon = this._promoPackIDs.Contains(beatmapLevelPack.packID);
		}
		else
		{
			annotatedBeatmapLevelCollectionTableCell.showNewRibbon = false;
		}
		annotatedBeatmapLevelCollectionTableCell.RefreshAvailabilityAsync(this._additionalContentModel);
		return annotatedBeatmapLevelCollectionTableCell;
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0000EF41 File Offset: 0x0000D141
	private void HandleDidSelectColumnEvent(TableView tableView, int column)
	{
		this._selectedColumn = column;
		Action<AnnotatedBeatmapLevelCollectionsTableView, IAnnotatedBeatmapLevelCollection> action = this.didSelectAnnotatedBeatmapLevelCollectionEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._annotatedBeatmapLevelCollections[column]);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0000EF63 File Offset: 0x0000D163
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this._tableView.ReloadData();
		this._selectedColumn = Math.Min(this._selectedColumn, this.NumberOfCells() - 1);
		this._tableView.SelectCellWithIdx(this._selectedColumn, false);
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00049C80 File Offset: 0x00047E80
	public void CancelAsyncOperations()
	{
		foreach (TableCell tableCell in this._tableView.visibleCells)
		{
			((AnnotatedBeatmapLevelCollectionTableCell)tableCell).CancelAsyncOperations();
		}
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x00049CD4 File Offset: 0x00047ED4
	public void RefreshAvailability()
	{
		if (this._annotatedBeatmapLevelCollections == null)
		{
			return;
		}
		foreach (TableCell tableCell in this._tableView.visibleCells)
		{
			((AnnotatedBeatmapLevelCollectionTableCell)tableCell).RefreshAvailabilityAsync(this._additionalContentModel);
		}
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x0000EF9B File Offset: 0x0000D19B
	public void SelectAndScrollToCellWithIdx(int idx)
	{
		this._selectedColumn = idx;
		this._tableView.ScrollToCellWithIdx(idx, TableViewScroller.ScrollPositionType.Center, false);
		this._tableView.SelectCellWithIdx(idx, false);
	}

	// Token: 0x040013A9 RID: 5033
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013AA RID: 5034
	[SerializeField]
	private AnnotatedBeatmapLevelCollectionTableCell _cellPrefab;

	// Token: 0x040013AB RID: 5035
	[SerializeField]
	private string _cellReuseIdentifier = "Cell";

	// Token: 0x040013AC RID: 5036
	[SerializeField]
	private float _cellWidth = 40f;

	// Token: 0x040013AD RID: 5037
	[Space]
	[SerializeField]
	private string[] _promoPackIDStrings;

	// Token: 0x040013AE RID: 5038
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x040013B0 RID: 5040
	private HashSet<string> _promoPackIDs;

	// Token: 0x040013B1 RID: 5041
	private IAnnotatedBeatmapLevelCollection[] _annotatedBeatmapLevelCollections;

	// Token: 0x040013B2 RID: 5042
	private bool _isInitialized;

	// Token: 0x040013B3 RID: 5043
	private int _selectedColumn;
}
