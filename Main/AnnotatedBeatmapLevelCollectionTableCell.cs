using System;
using System.Threading;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000353 RID: 851
public class AnnotatedBeatmapLevelCollectionTableCell : TableCell
{
	// Token: 0x1700032A RID: 810
	// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0003C9E8 File Offset: 0x0003ABE8
	public bool showNewRibbon
	{
		set
		{
			GameObject[] newPromoRibbonObjects = this._newPromoRibbonObjects;
			for (int i = 0; i < newPromoRibbonObjects.Length; i++)
			{
				newPromoRibbonObjects[i].SetActive(value);
			}
		}
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x0003CA14 File Offset: 0x0003AC14
	public void SetData(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		this._annotatedBeatmapLevelCollection = annotatedBeatmapLevelCollection;
		this._infoText.text = this.GetInfoText(this._annotatedBeatmapLevelCollection.collectionName, annotatedBeatmapLevelCollection.beatmapLevelCollection.beatmapLevels.Length, -1);
		this._coverImage.sprite = annotatedBeatmapLevelCollection.coverImage;
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x0000B7F9 File Offset: 0x000099F9
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0000B7F9 File Offset: 0x000099F9
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0003CA64 File Offset: 0x0003AC64
	private void RefreshVisuals()
	{
		this._coverImage.color = (base.highlighted ? new Color(0.15f, 0.15f, 0.15f, 1f) : Color.white);
		this._selectionImage.enabled = base.selected;
		this._infoText.enabled = base.highlighted;
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0000B801 File Offset: 0x00009A01
	protected override void WasPreparedForReuse()
	{
		this.CancelAsyncOperations();
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x0000B809 File Offset: 0x00009A09
	private string GetInfoText(string name, int songs, int purchased = -1)
	{
		if (purchased >= 0)
		{
			return string.Format("{0}\n\nSongs {1}\nPurchased {2}", name, songs, purchased);
		}
		return string.Format("{0}\n\nSongs {1}", name, songs);
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0003CAC8 File Offset: 0x0003ACC8
	public async void RefreshAvailabilityAsync(AdditionalContentModel contentModel)
	{
		try
		{
			this._infoText.text = this.GetInfoText(this._annotatedBeatmapLevelCollection.collectionName, this._annotatedBeatmapLevelCollection.beatmapLevelCollection.beatmapLevels.Length, -1);
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._cancellationTokenSource = new CancellationTokenSource();
			CancellationToken cancellationToken = this._cancellationTokenSource.Token;
			int numberOfOwnedLevels = 0;
			bool error = false;
			foreach (IPreviewBeatmapLevel previewBeatmapLevel in this._annotatedBeatmapLevelCollection.beatmapLevelCollection.beatmapLevels)
			{
				AdditionalContentModel.EntitlementStatus entitlementStatus = await contentModel.GetLevelEntitlementStatusAsync(previewBeatmapLevel.levelID, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				if (entitlementStatus == AdditionalContentModel.EntitlementStatus.Owned)
				{
					numberOfOwnedLevels++;
				}
				else if (entitlementStatus == AdditionalContentModel.EntitlementStatus.Failed)
				{
					error = true;
					break;
				}
			}
			IPreviewBeatmapLevel[] array = null;
			if (!error && numberOfOwnedLevels != this._annotatedBeatmapLevelCollection.beatmapLevelCollection.beatmapLevels.Length)
			{
				this._infoText.text = this.GetInfoText(this._annotatedBeatmapLevelCollection.collectionName, this._annotatedBeatmapLevelCollection.beatmapLevelCollection.beatmapLevels.Length, numberOfOwnedLevels);
			}
			cancellationToken = default(CancellationToken);
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0000B838 File Offset: 0x00009A38
	public void CancelAsyncOperations()
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource == null)
		{
			return;
		}
		cancellationTokenSource.Cancel();
	}

	// Token: 0x04000F54 RID: 3924
	[SerializeField]
	private TextMeshProUGUI _infoText;

	// Token: 0x04000F55 RID: 3925
	[SerializeField]
	private Image _coverImage;

	// Token: 0x04000F56 RID: 3926
	[SerializeField]
	private Image _selectionImage;

	// Token: 0x04000F57 RID: 3927
	[SerializeField]
	private GameObject[] _newPromoRibbonObjects;

	// Token: 0x04000F58 RID: 3928
	private IAnnotatedBeatmapLevelCollection _annotatedBeatmapLevelCollection;

	// Token: 0x04000F59 RID: 3929
	private CancellationTokenSource _cancellationTokenSource;
}
