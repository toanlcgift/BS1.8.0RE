using System;
using OnlineServices;
using UnityEngine;
using Zenject;

// Token: 0x0200033D RID: 829
public class AppCoreInstaller : MonoInstaller
{
	// Token: 0x06000E6F RID: 3695 RVA: 0x0003B188 File Offset: 0x00039388
	public override void InstallBindings()
	{
		base.Container.Bind<TimeHelper>().FromComponentInNewPrefab(this._timeHelperPrefab).AsSingle().NonLazy();
		base.Container.Bind<AppStaticSettingsSO>().FromInstance(this._appStaticSettings).AsSingle();
		base.Container.Bind<PlayerDataModel>().FromComponentInNewPrefab(this._playerDataModelPrefab).AsSingle();
		base.Container.Bind<CampaignProgressModel>().FromComponentInNewPrefab(this._campaignProgressModelPrefab).AsSingle();
		base.Container.Bind<BeatmapLevelsModel>().FromComponentInNewPrefab(this._beatmapLevelsModelPrefab).AsSingle();
		base.Container.Bind<VRPlatformHelper>().FromComponentInNewPrefab(this._vrPlatformHelperPrefab).AsSingle();
		base.Container.Bind<ExternalCamerasManager>().FromComponentInNewPrefab(this._externalCamerasManagerPrefab).AsSingle().NonLazy();
		base.Container.Bind<CustomLevelLoader>().FromComponentInNewPrefab(this._customLevelLoaderPrefab).AsSingle();
		base.Container.Bind<CachedMediaAsyncLoader>().FromComponentInNewPrefab(this._cachedMediaAsyncLoaderPrefab).AsSingle();
		base.Container.Bind<VRControllersInputManager>().AsSingle();
		base.Container.Bind<PlatformUserModelSO>().FromScriptableObject(this._platformUserModelSO).AsSingle();
		base.Container.Bind<BeatmapCharacteristicCollectionSO>().FromScriptableObject(this._beatmapCharacteristicCollection).AsSingle();
		base.Container.Bind<AlwaysOwnedContentContainerSO>().FromScriptableObject(this._alwaysOwnedContentContainer).AsSingle();
		base.Container.Bind<SteamLevelProductsModelSO>().FromScriptableObject(this._steamLevelProductsModel).AsSingle();
		base.Container.Bind<AdditionalContentModel>().FromComponentInNewPrefab(this._steamPlatformAdditionalContentModelPrefab).AsSingle();
		base.Container.Bind<IBeatmapDataAssetFileModel>().To<SteamBeatmapDataAssetFileModel>().AsSingle();
		this.InstallRichPresence();
		this.InstallOldPlatformLeaderboardsModel();
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0000B15F File Offset: 0x0000935F
	private void InstallRichPresence()
	{
		base.Container.Bind<IRichPresencePlatformHandler>().To<SteamRichPresencePlatformHandler>().AsSingle();
		base.Container.Bind<RichPresenceManager>().FromComponentInNewPrefab(this._richPresenceManagerPrefab).AsSingle().NonLazy();
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0003B35C File Offset: 0x0003955C
	private void InstallOculusDestinationBindings()
	{
		base.Container.BindInterfacesAndSelfTo<OculusDeeplinkManager>().AsSingle().NonLazy();
		base.Container.Bind<IDestinationRequestManager>().To<DeeplinkManagerToDestinationRequestManagerAdapter>().AsSingle().NonLazy();
		base.Container.BindInterfacesAndSelfTo<MainMenuDestinationRequestControler>().AsSingle().NonLazy();
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0003B3B0 File Offset: 0x000395B0
	private void InstallOldPlatformLeaderboardsModel()
	{
		PlatformLeaderboardsHandler instance = new SteamPlatformLeaderboardsHandler();
		base.Container.Bind<PlatformLeaderboardsHandler>().FromInstance(instance).AsSingle();
		base.Container.Bind<LeaderboardScoreUploader>().FromComponentInNewPrefab(this._leaderboardScoreUploader).AsSingle();
		base.Container.Bind<PlatformLeaderboardsModel>().FromComponentInNewPrefab(this._platformLeaderboardsModel).AsSingle();
		base.Container.Bind<ScreenCaptureCache>().AsSingle();
	}

	// Token: 0x04000EC4 RID: 3780
	[SerializeField]
	private AppStaticSettingsSO _appStaticSettings;

	// Token: 0x04000EC5 RID: 3781
	[Space]
	[SerializeField]
	private TimeHelper _timeHelperPrefab;

	// Token: 0x04000EC6 RID: 3782
	[SerializeField]
	private PlayerDataModel _playerDataModelPrefab;

	// Token: 0x04000EC7 RID: 3783
	[SerializeField]
	private CampaignProgressModel _campaignProgressModelPrefab;

	// Token: 0x04000EC8 RID: 3784
	[SerializeField]
	private BeatmapLevelsModel _beatmapLevelsModelPrefab;

	// Token: 0x04000EC9 RID: 3785
	[SerializeField]
	private CustomLevelLoader _customLevelLoaderPrefab;

	// Token: 0x04000ECA RID: 3786
	[SerializeField]
	private CachedMediaAsyncLoader _cachedMediaAsyncLoaderPrefab;

	// Token: 0x04000ECB RID: 3787
	[SerializeField]
	private VRPlatformHelper _vrPlatformHelperPrefab;

	// Token: 0x04000ECC RID: 3788
	[SerializeField]
	private ExternalCamerasManager _externalCamerasManagerPrefab;

	// Token: 0x04000ECD RID: 3789
	[Space]
	[SerializeField]
	private AlwaysOwnedContentContainerSO _alwaysOwnedContentContainer;

	// Token: 0x04000ECE RID: 3790
	[Space]
	[SerializeField]
	private TestPlatformAdditionalContentModel _testPlatformAdditionalContentModelPrefab;

	// Token: 0x04000ECF RID: 3791
	[SerializeField]
	private PS4PlatformAdditionalContentModel _ps4PlatformAdditionalContentModelPrefab;

	// Token: 0x04000ED0 RID: 3792
	[SerializeField]
	private OculusPlatformAdditionalContentModel _oculusPlatformAdditionalContentModelPrefab;

	// Token: 0x04000ED1 RID: 3793
	[SerializeField]
	private SteamPlatformAdditionalContentModel _steamPlatformAdditionalContentModelPrefab;

	// Token: 0x04000ED2 RID: 3794
	[Space]
	[SerializeField]
	private SteamLevelProductsModelSO _steamLevelProductsModel;

	// Token: 0x04000ED3 RID: 3795
	[SerializeField]
	private OculusLevelProductsModelSO _oculusLevelProducsModel;

	// Token: 0x04000ED4 RID: 3796
	[SerializeField]
	private PS4LevelProductsModelSO _ps4LevelProductsModel;

	// Token: 0x04000ED5 RID: 3797
	[SerializeField]
	private PS4LeaderboardIdsModelSO _ps4LeaderboardIdsModel;

	// Token: 0x04000ED6 RID: 3798
	[SerializeField]
	private ServerManager _onlineServicesServerManagerPrefab;

	// Token: 0x04000ED7 RID: 3799
	[SerializeField]
	private RichPresenceManager _richPresenceManagerPrefab;

	// Token: 0x04000ED8 RID: 3800
	[SerializeField]
	private PlatformUserModelSO _platformUserModelSO;

	// Token: 0x04000ED9 RID: 3801
	[SerializeField]
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x04000EDA RID: 3802
	[Space]
	[SerializeField]
	private LeaderboardScoreUploader _leaderboardScoreUploader;

	// Token: 0x04000EDB RID: 3803
	[SerializeField]
	private PlatformLeaderboardsModel _platformLeaderboardsModel;
}
