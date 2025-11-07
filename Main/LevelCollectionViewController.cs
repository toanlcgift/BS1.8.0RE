using System;
using System.Threading;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x020003DC RID: 988
public class LevelCollectionViewController : ViewController
{
	// Token: 0x1400009D RID: 157
	// (add) Token: 0x0600124D RID: 4685 RVA: 0x000449BC File Offset: 0x00042BBC
	// (remove) Token: 0x0600124E RID: 4686 RVA: 0x000449F4 File Offset: 0x00042BF4
	public event Action<LevelCollectionViewController, IPreviewBeatmapLevel> didSelectLevelEvent;

	// Token: 0x1400009E RID: 158
	// (add) Token: 0x0600124F RID: 4687 RVA: 0x00044A2C File Offset: 0x00042C2C
	// (remove) Token: 0x06001250 RID: 4688 RVA: 0x00044A64 File Offset: 0x00042C64
	public event Action<LevelCollectionViewController> didSelectHeaderEvent;

	// Token: 0x06001251 RID: 4689 RVA: 0x00044A9C File Offset: 0x00042C9C
	public void SetData(IBeatmapLevelCollection beatmapLevelCollection, string headerText, Sprite headerSprite, bool sortLevels, GameObject noDataInfoPrefab)
	{
		this._showHeader = !string.IsNullOrEmpty(headerText);
		this._levelCollectionTableView.Init(headerText, headerSprite);
		if (this._noDataInfoGO != null)
		{
			UnityEngine.Object.Destroy(this._noDataInfoGO);
			this._noDataInfoGO = null;
		}
		if (beatmapLevelCollection != null && beatmapLevelCollection.beatmapLevels.Length != 0)
		{
			this._levelCollectionTableView.gameObject.SetActive(true);
			IPreviewBeatmapLevel[] beatmapLevels = beatmapLevelCollection.beatmapLevels;
			this._levelCollectionTableView.SetData(beatmapLevels, this._playerDataModel.playerData.favoritesLevelIds, sortLevels);
			this._levelCollectionTableView.RefreshLevelsAvailability();
		}
		else
		{
			if (noDataInfoPrefab != null)
			{
				this._noDataInfoGO = this._container.InstantiatePrefab(noDataInfoPrefab, this._noDataInfoContainer);
			}
			this._levelCollectionTableView.gameObject.SetActive(false);
		}
		if (base.isInViewControllerHierarchy)
		{
			if (this._showHeader)
			{
				this._levelCollectionTableView.SelectLevelPackHeaderCell();
			}
			this._songPreviewPlayer.CrossfadeToDefault();
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x0000DE65 File Offset: 0x0000C065
	public void SelectLevel(IPreviewBeatmapLevel beatmapLevel)
	{
		this._levelCollectionTableView.SelectLevel(beatmapLevel);
		this._previewBeatmapLevelToBeSelected = beatmapLevel;
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0000DE7A File Offset: 0x0000C07A
	public void RefreshFavorites()
	{
		this._levelCollectionTableView.RefreshFavorites(this._playerDataModel.playerData.favoritesLevelIds);
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00044B90 File Offset: 0x00042D90
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._levelCollectionTableView.didSelectLevelEvent += this.HandleLevelCollectionTableViewDidSelectLevel;
			this._levelCollectionTableView.didSelectHeaderEvent += this.HandleLevelCollectionTableViewDidSelectPack;
			if (this._showHeader)
			{
				this._levelCollectionTableView.SelectLevelPackHeaderCell();
			}
		}
		this._levelCollectionTableView.RefreshLevelsAvailability();
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
		if (this._previewBeatmapLevelToBeSelected != null)
		{
			this._levelCollectionTableView.SelectLevel(this._previewBeatmapLevelToBeSelected);
		}
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00044C1C File Offset: 0x00042E1C
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._levelCollectionTableView.didSelectLevelEvent -= this.HandleLevelCollectionTableViewDidSelectLevel;
			this._levelCollectionTableView.didSelectHeaderEvent -= this.HandleLevelCollectionTableViewDidSelectPack;
		}
		this._levelCollectionTableView.CancelAsyncOperations();
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
		this._songPreviewPlayer.CrossfadeToDefault();
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = null;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0000DE97 File Offset: 0x0000C097
	private void HandleLevelCollectionTableViewDidSelectLevel(LevelCollectionTableView tableView, IPreviewBeatmapLevel level)
	{
		this._previewBeatmapLevelToBeSelected = null;
		this.SongPlayerCrossfadeToLevelAsync(level);
		Action<LevelCollectionViewController, IPreviewBeatmapLevel> action = this.didSelectLevelEvent;
		if (action == null)
		{
			return;
		}
		action(this, level);
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00044CA0 File Offset: 0x00042EA0
	private async void SongPlayerCrossfadeToLevelAsync(IPreviewBeatmapLevel level)
	{
		if (!(this._songPlayerCrossfadignToLevelId == level.levelID))
		{
			try
			{
				this._songPlayerCrossfadignToLevelId = level.levelID;
				CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				this._cancellationTokenSource = new CancellationTokenSource();
				CancellationToken cancellationToken = this._cancellationTokenSource.Token;
				AudioClip audioClip = await level.GetPreviewAudioClipAsync(cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				this._songPreviewPlayer.CrossfadeTo(audioClip, level.previewStartTime, level.previewDuration, 1f);
				if (this._songPlayerCrossfadignToLevelId == level.levelID)
				{
					this._songPlayerCrossfadignToLevelId = null;
				}
				cancellationToken = default(CancellationToken);
			}
			catch (OperationCanceledException)
			{
				if (this._songPlayerCrossfadignToLevelId == level.levelID)
				{
					this._songPlayerCrossfadignToLevelId = null;
				}
			}
		}
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0000DEB9 File Offset: 0x0000C0B9
	private void HandleLevelCollectionTableViewDidSelectPack(LevelCollectionTableView tableView)
	{
		this._songPreviewPlayer.CrossfadeToDefault();
		Action<LevelCollectionViewController> action = this.didSelectHeaderEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x0000DED7 File Offset: 0x0000C0D7
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this._levelCollectionTableView.RefreshLevelsAvailability();
	}

	// Token: 0x04001207 RID: 4615
	[SerializeField]
	private LevelCollectionTableView _levelCollectionTableView;

	// Token: 0x04001208 RID: 4616
	[SerializeField]
	private RectTransform _noDataInfoContainer;

	// Token: 0x04001209 RID: 4617
	[Space]
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x0400120A RID: 4618
	[Inject]
	private DiContainer _container;

	// Token: 0x0400120B RID: 4619
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x0400120C RID: 4620
	[Inject]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x0400120F RID: 4623
	private bool _showHeader = true;

	// Token: 0x04001210 RID: 4624
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x04001211 RID: 4625
	private string _songPlayerCrossfadignToLevelId;

	// Token: 0x04001212 RID: 4626
	private GameObject _noDataInfoGO;

	// Token: 0x04001213 RID: 4627
	private IPreviewBeatmapLevel _previewBeatmapLevelToBeSelected;
}
