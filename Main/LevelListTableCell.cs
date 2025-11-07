using System;
using System.Threading;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200035C RID: 860
public class LevelListTableCell : TableCell
{
	// Token: 0x06000F23 RID: 3875 RVA: 0x0003CE4C File Offset: 0x0003B04C
	public async void SetDataFromLevelAsync(IPreviewBeatmapLevel level, bool isFavorite)
	{
		if (!(this._settingDataFromLevelId == level.levelID))
		{
			try
			{
				this._favoritesBadgeImage.enabled = isFavorite;
				this._settingDataFromLevelId = level.levelID;
				CancellationTokenSource settingDataCancellationTokenSource = this._settingDataCancellationTokenSource;
				if (settingDataCancellationTokenSource != null)
				{
					settingDataCancellationTokenSource.Cancel();
				}
				this._settingDataCancellationTokenSource = new CancellationTokenSource();
				this._songNameText.text = string.Format("{0} <size=80%>{1}</size>", level.songName, level.songSubName);
				this._authorText.text = level.songAuthorName;
				this._coverRawImage.texture = null;
				this._coverRawImage.color = Color.clear;
				CancellationToken cancellationToken = this._settingDataCancellationTokenSource.Token;
				Texture2D texture = await level.GetCoverImageTexture2DAsync(cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				this._coverRawImage.texture = texture;
				this._coverRawImage.color = Color.white;
				float num = -1f;
				for (int i = 0; i < this._beatmapCharacteristics.Length; i++)
				{
					bool flag = false;
					PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets = level.previewDifficultyBeatmapSets;
					for (int j = 0; j < previewDifficultyBeatmapSets.Length; j++)
					{
						if (previewDifficultyBeatmapSets[j].beatmapCharacteristic == this._beatmapCharacteristics[i])
						{
							flag = true;
							break;
						}
					}
					Image image = this._beatmapCharacteristicImages[i];
					if (flag)
					{
						image.enabled = true;
						image.rectTransform.anchoredPosition = new Vector2(num, 0f);
						num -= image.rectTransform.sizeDelta.x + 0.5f;
					}
					else
					{
						image.enabled = false;
					}
				}
				this._songNameText.rectTransform.offsetMax = new Vector2(num, this._songNameText.rectTransform.offsetMax.y);
				this._authorText.rectTransform.offsetMax = new Vector2(num, this._authorText.rectTransform.offsetMax.y);
				cancellationToken = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (this._settingDataFromLevelId == level.levelID)
				{
					this._settingDataFromLevelId = null;
				}
			}
		}
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0000BA1B File Offset: 0x00009C1B
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0000BA1B File Offset: 0x00009C1B
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0003CE98 File Offset: 0x0003B098
	private void RefreshVisuals()
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._songNameText.color = (base.highlighted ? this._selectedHighlightElementsColor : Color.black);
			this._authorText.color = this._songNameText.color;
			for (int i = 0; i < this._beatmapCharacteristicImages.Length; i++)
			{
				this._beatmapCharacteristicImages[i].color = (base.highlighted ? this._selectedHighlightElementsColor : new Color(0f, 0f, 0f, 1f));
			}
			return;
		}
		this._bgImage.enabled = false;
		this._songNameText.color = (this._bought ? Color.white : (base.highlighted ? new Color(1f, 1f, 1f, 0.75f) : new Color(1f, 1f, 1f, 0.25f)));
		this._authorText.color = ((this._bought || base.highlighted) ? new Color(1f, 1f, 1f, 0.25f) : new Color(1f, 1f, 1f, 0.1f));
		for (int j = 0; j < this._beatmapCharacteristicImages.Length; j++)
		{
			this._beatmapCharacteristicImages[j].color = this._beatmapCharacteristicImagesNormalColor;
		}
		this._highlightImage.enabled = base.highlighted;
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0000BA23 File Offset: 0x00009C23
	protected override void WasPreparedForReuse()
	{
		this.CancelAsyncOperations();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x0003D030 File Offset: 0x0003B230
	public async void RefreshAvailabilityAsync(AdditionalContentModel contentModel, string levelID)
	{
		if (!(this._refreshingAvailabilityLevelID == levelID))
		{
			try
			{
				this._bought = true;
				this.RefreshVisuals();
				this._refreshingAvailabilityLevelID = levelID;
				CancellationTokenSource refreshingAvailabilityCancellationTokenSource = this._refreshingAvailabilityCancellationTokenSource;
				if (refreshingAvailabilityCancellationTokenSource != null)
				{
					refreshingAvailabilityCancellationTokenSource.Cancel();
				}
				this._refreshingAvailabilityCancellationTokenSource = new CancellationTokenSource();
				CancellationToken cancellationToken = this._refreshingAvailabilityCancellationTokenSource.Token;
				AdditionalContentModel.EntitlementStatus entitlementStatus = await contentModel.GetLevelEntitlementStatusAsync(levelID, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				this._bought = (entitlementStatus != AdditionalContentModel.EntitlementStatus.NotOwned);
				this.RefreshVisuals();
				this._refreshingAvailabilityLevelID = null;
				cancellationToken = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				if (this._refreshingAvailabilityLevelID == levelID)
				{
					this._refreshingAvailabilityLevelID = null;
				}
			}
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0000BA2B File Offset: 0x00009C2B
	public void CancelAsyncOperations()
	{
		CancellationTokenSource refreshingAvailabilityCancellationTokenSource = this._refreshingAvailabilityCancellationTokenSource;
		if (refreshingAvailabilityCancellationTokenSource != null)
		{
			refreshingAvailabilityCancellationTokenSource.Cancel();
		}
		this._refreshingAvailabilityCancellationTokenSource = null;
		CancellationTokenSource settingDataCancellationTokenSource = this._settingDataCancellationTokenSource;
		if (settingDataCancellationTokenSource != null)
		{
			settingDataCancellationTokenSource.Cancel();
		}
		this._settingDataCancellationTokenSource = null;
	}

	// Token: 0x04000F7D RID: 3965
	[SerializeField]
	private Color _selectedHighlightElementsColor = new Color(0f, 0.7529412f, 1f, 1f);

	// Token: 0x04000F7E RID: 3966
	[SerializeField]
	private Color _beatmapCharacteristicImagesNormalColor = new Color(0f, 0f, 0f, 0.5f);

	// Token: 0x04000F7F RID: 3967
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x04000F80 RID: 3968
	[SerializeField]
	private TextMeshProUGUI _authorText;

	// Token: 0x04000F81 RID: 3969
	[SerializeField]
	private RawImage _coverRawImage;

	// Token: 0x04000F82 RID: 3970
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F83 RID: 3971
	[SerializeField]
	private Image _highlightImage;

	// Token: 0x04000F84 RID: 3972
	[SerializeField]
	private Image[] _beatmapCharacteristicImages;

	// Token: 0x04000F85 RID: 3973
	[SerializeField]
	private BeatmapCharacteristicSO[] _beatmapCharacteristics;

	// Token: 0x04000F86 RID: 3974
	[SerializeField]
	private RawImage _favoritesBadgeImage;

	// Token: 0x04000F87 RID: 3975
	private CancellationTokenSource _refreshingAvailabilityCancellationTokenSource;

	// Token: 0x04000F88 RID: 3976
	private CancellationTokenSource _settingDataCancellationTokenSource;

	// Token: 0x04000F89 RID: 3977
	private bool _bought;

	// Token: 0x04000F8A RID: 3978
	private string _refreshingAvailabilityLevelID;

	// Token: 0x04000F8B RID: 3979
	private string _settingDataFromLevelId;
}
