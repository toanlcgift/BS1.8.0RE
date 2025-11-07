using System;
using System.Runtime.CompilerServices;
using System.Threading;
using HMUI;
using Polyglot;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x02000411 RID: 1041
public class StandardLevelDetailViewController : ViewController
{
	// Token: 0x140000B7 RID: 183
	// (add) Token: 0x060013A5 RID: 5029 RVA: 0x000489D4 File Offset: 0x00046BD4
	// (remove) Token: 0x060013A6 RID: 5030 RVA: 0x00048A0C File Offset: 0x00046C0C
	public event Action<StandardLevelDetailViewController> didPressPlayButtonEvent;

	// Token: 0x140000B8 RID: 184
	// (add) Token: 0x060013A7 RID: 5031 RVA: 0x00048A44 File Offset: 0x00046C44
	// (remove) Token: 0x060013A8 RID: 5032 RVA: 0x00048A7C File Offset: 0x00046C7C
	public event Action<StandardLevelDetailViewController, IBeatmapLevelPack> didPressOpenLevelPackButtonEvent;

	// Token: 0x140000B9 RID: 185
	// (add) Token: 0x060013A9 RID: 5033 RVA: 0x00048AB4 File Offset: 0x00046CB4
	// (remove) Token: 0x060013AA RID: 5034 RVA: 0x00048AEC File Offset: 0x00046CEC
	public event Action<StandardLevelDetailViewController, bool> levelFavoriteStatusDidChangeEvent;

	// Token: 0x140000BA RID: 186
	// (add) Token: 0x060013AB RID: 5035 RVA: 0x00048B24 File Offset: 0x00046D24
	// (remove) Token: 0x060013AC RID: 5036 RVA: 0x00048B5C File Offset: 0x00046D5C
	public event Action<StandardLevelDetailViewController, IBeatmapLevel> didPressPracticeButtonEvent;

	// Token: 0x140000BB RID: 187
	// (add) Token: 0x060013AD RID: 5037 RVA: 0x00048B94 File Offset: 0x00046D94
	// (remove) Token: 0x060013AE RID: 5038 RVA: 0x00048BCC File Offset: 0x00046DCC
	public event Action<StandardLevelDetailViewController, IDifficultyBeatmap> didChangeDifficultyBeatmapEvent;

	// Token: 0x140000BC RID: 188
	// (add) Token: 0x060013AF RID: 5039 RVA: 0x00048C04 File Offset: 0x00046E04
	// (remove) Token: 0x060013B0 RID: 5040 RVA: 0x00048C3C File Offset: 0x00046E3C
	public event Action<StandardLevelDetailViewController, StandardLevelDetailViewController.ContentType> didPresentContentEvent;

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0000ECA5 File Offset: 0x0000CEA5
	public IDifficultyBeatmap selectedDifficultyBeatmap
	{
		get
		{
			return this._standardLevelDetailView.selectedDifficultyBeatmap;
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x00048C74 File Offset: 0x00046E74
	public void SetData(IPreviewBeatmapLevel previewBeatmapLevel, bool showPlayerStats, bool hidePracticeButton, bool hide360DegreeBeatmapCharacteristic)
	{
		IBeatmapLevelPack levelPackForLevelId = this._beatmapLevelsModel.GetLevelPackForLevelId(previewBeatmapLevel.levelID);
		this.SetData(levelPackForLevelId, previewBeatmapLevel, showPlayerStats, hidePracticeButton, hide360DegreeBeatmapCharacteristic, false);
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x00048CA0 File Offset: 0x00046EA0
	public void SetData(IBeatmapLevelPack pack, IPreviewBeatmapLevel previewBeatmapLevel, bool showPlayerStats, bool hidePracticeButton, bool hide360DegreeBeatmapCharacteristic, bool canBuyPack)
	{
		this._canBuyPack = canBuyPack;
		this._showPlayerStats = showPlayerStats;
		this._beatmapLevel = null;
		this._pack = pack;
		this._previewBeatmapLevel = previewBeatmapLevel;
		this._standardLevelBuyView.SetContent(previewBeatmapLevel);
		this._standardLevelDetailView.hidePracticeButton = hidePracticeButton;
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x00048CF0 File Offset: 0x00046EF0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._standardLevelBuyView.buyButton, new Action(this.OpenLevelProductStoreOrShowBuyInfoAsync));
			base.buttonBinder.AddBinding(this._standardLevelBuyInfoView.buyLevelButton, new Action(this.OpenLevelProductStoreAsync));
			base.buttonBinder.AddBinding(this._standardLevelBuyInfoView.buyPackButton, new Action(this.OpenLevelPackProductStoreAsync));
			base.buttonBinder.AddBinding(this._standardLevelBuyInfoView.openPackButton, new Action(this.OpenLevelPack));
			base.buttonBinder.AddBinding(this._standardLevelDetailView.playButton, delegate
			{
				Action<StandardLevelDetailViewController> action = this.didPressPlayButtonEvent;
				if (action == null)
				{
					return;
				}
				action(this);
			});
			base.buttonBinder.AddBinding(this._standardLevelDetailView.practiceButton, new Action(this.HandleDidPressPracticeButton));
			Action handleDidPressRefreshButton = delegate()
			{
				this.RefreshAvailabilityAsync();
			};
			this._eventBinder.Bind(delegate
			{
				this._loadingControl.didPressRefreshButtonEvent += handleDidPressRefreshButton;
			}, delegate
			{
				this._loadingControl.didPressRefreshButtonEvent -= handleDidPressRefreshButton;
			});
			Action<StandardLevelDetailView, IDifficultyBeatmap> handleDidChangeDifficultyBeatmap = delegate(StandardLevelDetailView view, IDifficultyBeatmap beatmap)
			{
				this._playerDataModel.playerData.SetLastSelectedBeatmapDifficulty(beatmap.difficulty);
				this._playerDataModel.playerData.SetLastSelectedBeatmapCharacteristic(beatmap.parentDifficultyBeatmapSet.beatmapCharacteristic);
				Action<StandardLevelDetailViewController, IDifficultyBeatmap> action = this.didChangeDifficultyBeatmapEvent;
				if (action == null)
				{
					return;
				}
				action(this, beatmap);
			};
			Action<StandardLevelDetailView, Toggle> handleDidFavoriteToggleChange = delegate(StandardLevelDetailView view, Toggle toggle)
			{
				if (toggle.isOn)
				{
					this._playerDataModel.playerData.AddLevelToFavorites(this._previewBeatmapLevel);
				}
				else
				{
					this._playerDataModel.playerData.RemoveLevelFromFavorites(this._previewBeatmapLevel);
				}
				Action<StandardLevelDetailViewController, bool> action = this.levelFavoriteStatusDidChangeEvent;
				if (action == null)
				{
					return;
				}
				action(this, toggle.isOn);
			};
			this._eventBinder.Bind(delegate
			{
				this._standardLevelDetailView.didChangeDifficultyBeatmapEvent += handleDidChangeDifficultyBeatmap;
				this._standardLevelDetailView.didFavoriteToggleChangeEvent += handleDidFavoriteToggleChange;
			}, delegate
			{
				this._standardLevelDetailView.didChangeDifficultyBeatmapEvent -= handleDidChangeDifficultyBeatmap;
				this._standardLevelDetailView.didFavoriteToggleChangeEvent -= handleDidFavoriteToggleChange;
			});
		}
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
		this._beatmapLevelsModel.levelDownloadingUpdateEvent += this.HandleLevelLoadingUpdate;
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x00048E8C File Offset: 0x0004708C
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
		this._beatmapLevelsModel.levelDownloadingUpdateEvent -= this.HandleLevelLoadingUpdate;
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x00048ED8 File Offset: 0x000470D8
	protected override void OnDestroy()
	{
		this._eventBinder.ClearAllBindings();
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
		this._beatmapLevelsModel.levelDownloadingUpdateEvent -= this.HandleLevelLoadingUpdate;
		base.OnDestroy();
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x0000ECB2 File Offset: 0x0000CEB2
	private void HandleDidPressPracticeButton()
	{
		if (this._beatmapLevel != null)
		{
			Action<StandardLevelDetailViewController, IBeatmapLevel> action = this.didPressPracticeButtonEvent;
			if (action == null)
			{
				return;
			}
			action(this, this._beatmapLevel);
		}
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x00048F24 File Offset: 0x00047124
	private void HandleLevelLoadingUpdate(BeatmapLevelsModel.LevelDownloadingUpdate levelLoadingUpdate)
	{
		if (levelLoadingUpdate.levelID == this._previewBeatmapLevel.levelID)
		{
			if (levelLoadingUpdate.downloadingState == BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.Completed)
			{
				this.RefreshAvailabilityAsync();
				return;
			}
			bool preparingToDownload = levelLoadingUpdate.downloadingState == BeatmapLevelsModel.LevelDownloadingUpdate.DownloadingState.PreparingToDownload;
			this.UpdateDownloading(preparingToDownload, levelLoadingUpdate.bytesTransferred, levelLoadingUpdate.bytesTotal);
		}
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x00048F78 File Offset: 0x00047178
	private void UpdateDownloading(bool preparingToDownload, uint bytesTransferred, uint bytesTotal)
	{
		float downloadingProgress = (bytesTotal > 0f) ? (bytesTransferred / bytesTotal) : 0f;
		string downloadingText = preparingToDownload ? Localization.Get("DOWNLOADING") : Localization.Get("DOWNLOADING");
		this.ShowContent(StandardLevelDetailViewController.ContentType.OwnedAndDownloading, "", downloadingProgress, downloadingText);
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x00048FC8 File Offset: 0x000471C8
	private async void LoadBeatmapLevelAsync()
	{
		CancellationTokenSource loadingLevelCancellationTokenSource = this._loadingLevelCancellationTokenSource;
		if (loadingLevelCancellationTokenSource != null)
		{
			loadingLevelCancellationTokenSource.Cancel();
		}
		this._loadingLevelCancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = this._loadingLevelCancellationTokenSource.Token;
		try
		{
			string levelID = this._previewBeatmapLevel.levelID;
			BeatmapLevelsModel.GetBeatmapLevelResult getBeatmapLevelResult = await this._beatmapLevelsModel.GetBeatmapLevelAsync(levelID, cancellationToken);
			if (getBeatmapLevelResult.isError || getBeatmapLevelResult.beatmapLevel == null)
			{
				this.ShowContent(StandardLevelDetailViewController.ContentType.Error, (await InternetConnectionChecker.IsConnectedToInternetAsync(cancellationToken)) ? Localization.Get("ERROR_LOADING_DATA") : Localization.Get("ERROR_LOADING_DATA_NO_INTERNET_MESSAGE"), 0f, "");
			}
			else
			{
				if (getBeatmapLevelResult.beatmapLevel.levelID == this._previewBeatmapLevel.levelID)
				{
					this._beatmapLevel = getBeatmapLevelResult.beatmapLevel;
				}
				this._standardLevelDetailView.SetContent(this._beatmapLevel, this._playerDataModel.playerData.lastSelectedBeatmapDifficulty, this._playerDataModel.playerData.lastSelectedBeatmapCharacteristic, this._playerDataModel.playerData, this._showPlayerStats);
				this.ShowContent(StandardLevelDetailViewController.ContentType.OwnedAndReady, "", 0f, "");
			}
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x0000ECD3 File Offset: 0x0000CED3
	public void RefreshContentLevelDetailView()
	{
		this._standardLevelDetailView.RefreshContent();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x00049004 File Offset: 0x00047204
	private async void OpenLevelProductStoreOrShowBuyInfoAsync()
	{
		try
		{
			this.ShowContent(StandardLevelDetailViewController.ContentType.Loading, "", 0f, "");
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._cancellationTokenSource = new CancellationTokenSource();
			TaskAwaiter<AdditionalContentModel.IsPackBetterBuyThanLevelResult> taskAwaiter = this._additionalContentModel.IsPackBetterBuyThanLevelAsync(this._pack.packID, this._cancellationTokenSource.Token).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				taskAwaiter.GetResult();
				TaskAwaiter<AdditionalContentModel.IsPackBetterBuyThanLevelResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<AdditionalContentModel.IsPackBetterBuyThanLevelResult>);
			}
			switch (taskAwaiter.GetResult())
			{
			case AdditionalContentModel.IsPackBetterBuyThanLevelResult.PackIsBetter:
				this._standardLevelBuyInfoView.RefreshView(Localization.Get("BUY_VIEW_INFO_TEXT"), this._canBuyPack);
				this.ShowContent(StandardLevelDetailViewController.ContentType.BuyInfo, "", 0f, "");
				break;
			case AdditionalContentModel.IsPackBetterBuyThanLevelResult.LevelIsBetter:
				this.OpenLevelProductStoreAsync();
				break;
			case AdditionalContentModel.IsPackBetterBuyThanLevelResult.Failed:
				this.ShowContent(StandardLevelDetailViewController.ContentType.Error, Localization.Get("ERROR_LOADING_DATA"), 0f, "");
				break;
			}
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x00049040 File Offset: 0x00047240
	private async void OpenLevelProductStoreAsync()
	{
		try
		{
			this.ShowContent(StandardLevelDetailViewController.ContentType.Loading, "", 0f, "");
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this._cancellationTokenSource = new CancellationTokenSource();
			await this._additionalContentModel.OpenLevelProductStoreAsync(this._previewBeatmapLevel.levelID, this._cancellationTokenSource.Token);
			this.RefreshAvailabilityAsync();
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x0004907C File Offset: 0x0004727C
	private async void OpenLevelPackProductStoreAsync()
	{
		if (this._pack != null)
		{
			try
			{
				this.ShowContent(StandardLevelDetailViewController.ContentType.Loading, "", 0f, "");
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				await this._additionalContentModel.OpenLevelPackProductStoreAsync(this._pack.packID, this._cancellationTokenSource.Token);
				this.RefreshAvailabilityAsync();
			}
			catch (OperationCanceledException)
			{
			}
		}
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
	private void OpenLevelPack()
	{
		Action<StandardLevelDetailViewController, IBeatmapLevelPack> action = this.didPressOpenLevelPackButtonEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._pack);
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000490B8 File Offset: 0x000472B8
	private async void RefreshAvailabilityAsync()
	{
		if (base.isActiveAndEnabled)
		{
			try
			{
				this.ShowContent(StandardLevelDetailViewController.ContentType.Loading, "", 0f, "");
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				CancellationToken token = this._cancellationTokenSource.Token;
				this._requireInternetContainer.SetActive(false);
				AdditionalContentModel.EntitlementStatus entitlementStatus = await this._additionalContentModel.GetLevelEntitlementStatusAsync(this._previewBeatmapLevel.levelID, token);
				token.ThrowIfCancellationRequested();
				if (entitlementStatus != AdditionalContentModel.EntitlementStatus.Owned)
				{
					if (entitlementStatus != AdditionalContentModel.EntitlementStatus.NotOwned)
					{
						this.ShowContent(StandardLevelDetailViewController.ContentType.Error, Localization.Get("ERROR_LOADING_DATA"), 0f, "");
					}
					else
					{
						this.ShowContent(StandardLevelDetailViewController.ContentType.Buy, "", 0f, "");
					}
				}
				else
				{
					this.LoadBeatmapLevelAsync();
				}
				token = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
			}
		}
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x000490F4 File Offset: 0x000472F4
	private void ShowContent(StandardLevelDetailViewController.ContentType contentType, string errorText = "", float downloadingProgress = 0f, string downloadingText = "")
	{
		this._standardLevelDetailView.gameObject.SetActive(contentType == StandardLevelDetailViewController.ContentType.OwnedAndReady);
		this._standardLevelBuyView.gameObject.SetActive(contentType == StandardLevelDetailViewController.ContentType.Buy);
		this._standardLevelBuyInfoView.gameObject.SetActive(contentType == StandardLevelDetailViewController.ContentType.BuyInfo);
		if (contentType != StandardLevelDetailViewController.ContentType.Loading)
		{
			if (contentType != StandardLevelDetailViewController.ContentType.OwnedAndDownloading)
			{
				if (contentType != StandardLevelDetailViewController.ContentType.Error)
				{
					this._loadingControl.Hide();
				}
				else
				{
					this._loadingControl.ShowText(errorText, true);
				}
			}
			else
			{
				this._loadingControl.ShowDownloadingProgress(downloadingText, downloadingProgress);
			}
		}
		else
		{
			this._loadingControl.ShowLoading();
		}
		Action<StandardLevelDetailViewController, StandardLevelDetailViewController.ContentType> action = this.didPresentContentEvent;
		if (action == null)
		{
			return;
		}
		action(this, contentType);
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x0000ECF9 File Offset: 0x0000CEF9
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this.RefreshAvailabilityAsync();
	}

	// Token: 0x04001368 RID: 4968
	private const string kLoadingDataErrorNoInternetLocalizationKey = "ERROR_LOADING_DATA_NO_INTERNET_MESSAGE";

	// Token: 0x04001369 RID: 4969
	private const string kLoadingDataErrorLocalizationKey = "ERROR_LOADING_DATA";

	// Token: 0x0400136A RID: 4970
	[SerializeField]
	private StandardLevelDetailView _standardLevelDetailView;

	// Token: 0x0400136B RID: 4971
	[SerializeField]
	private StandardLevelBuyView _standardLevelBuyView;

	// Token: 0x0400136C RID: 4972
	[SerializeField]
	private StandardLevelBuyInfoView _standardLevelBuyInfoView;

	// Token: 0x0400136D RID: 4973
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x0400136E RID: 4974
	[SerializeField]
	private GameObject _requireInternetContainer;

	// Token: 0x0400136F RID: 4975
	[Space]
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001370 RID: 4976
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x04001371 RID: 4977
	[Inject]
	private BeatmapLevelsModel _beatmapLevelsModel;

	// Token: 0x04001378 RID: 4984
	private EventBinder _eventBinder = new EventBinder();

	// Token: 0x04001379 RID: 4985
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x0400137A RID: 4986
	private IPreviewBeatmapLevel _previewBeatmapLevel;

	// Token: 0x0400137B RID: 4987
	private IBeatmapLevel _beatmapLevel;

	// Token: 0x0400137C RID: 4988
	private IBeatmapLevelPack _pack;

	// Token: 0x0400137D RID: 4989
	private bool _canBuyPack;

	// Token: 0x0400137E RID: 4990
	private bool _showPlayerStats;

	// Token: 0x0400137F RID: 4991
	private CancellationTokenSource _loadingLevelCancellationTokenSource;

	// Token: 0x02000412 RID: 1042
	public enum ContentType
	{
		// Token: 0x04001381 RID: 4993
		Loading,
		// Token: 0x04001382 RID: 4994
		OwnedAndReady,
		// Token: 0x04001383 RID: 4995
		OwnedAndDownloading,
		// Token: 0x04001384 RID: 4996
		Buy,
		// Token: 0x04001385 RID: 4997
		BuyInfo,
		// Token: 0x04001386 RID: 4998
		Error
	}
}
