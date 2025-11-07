using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x0200037E RID: 894
public abstract class LevelSelectionFlowCoordinator : FlowCoordinator
{
	// Token: 0x06000FF2 RID: 4082
	protected abstract void LevelSelectionFlowCoordinatorDidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType);

	// Token: 0x06000FF3 RID: 4083
	protected abstract void LevelSelectionFlowCoordinatorDidDeactivate(FlowCoordinator.DeactivationType deactivationType);

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0000C339 File Offset: 0x0000A539
	protected bool isInPracticeView
	{
		get
		{
			return base.topViewController == this._practiceViewController;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0000C34C File Offset: 0x0000A54C
	protected MenuTransitionsHelper menuTransitionsHelper
	{
		get
		{
			return this._menuTransitionsHelper;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0000C354 File Offset: 0x0000A554
	protected GameplaySetupViewController gameplaySetupViewController
	{
		get
		{
			return this._gameplaySetupViewController;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0000C35C File Offset: 0x0000A55C
	protected LevelSelectionNavigationController levelSelectionNavigationController
	{
		get
		{
			return this._levelSelectionNavigationController;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0000C364 File Offset: 0x0000A564
	protected virtual ViewController topScreenViewController
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00002907 File Offset: 0x00000B07
	protected virtual bool hidePacksIfOneOrNone
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00002907 File Offset: 0x00000B07
	protected virtual bool hideGameplaySetup
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00002907 File Offset: 0x00000B07
	protected virtual bool hidePracticeButton
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00002907 File Offset: 0x00000B07
	protected virtual bool showBackButtonForMainViewController
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06000FFD RID: 4093
	protected abstract LeaderboardViewController leaderboardViewController { get; }

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06000FFE RID: 4094
	protected abstract bool showPlayerStatsInDetailView { get; }

	// Token: 0x06000FFF RID: 4095 RVA: 0x0000C367 File Offset: 0x0000A567
	public void Setup(IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollectionToBeSelectedAfterPresent, IPreviewBeatmapLevel beatmapLevelToBeSelectedAfterPresent)
	{
		this._annotatedBeatmapLevelCollectionToBeSelectedAfterPresent = annotatedBeatmapLevelCollectionToBeSelectedAfterPresent;
		this._beatmapLevelToBeSelectedAfterPresent = beatmapLevelToBeSelectedAfterPresent;
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0000C377 File Offset: 0x0000A577
	private void ResetSetupParameters()
	{
		this._annotatedBeatmapLevelCollectionToBeSelectedAfterPresent = null;
		this._beatmapLevelToBeSelectedAfterPresent = null;
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0003F3AC File Offset: 0x0003D5AC
	protected sealed override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._levelFilteringNavigationController.didSelectAnnotatedBeatmapLevelCollectionEvent += this.HandleLevelFilteringNavigationControllerDidSelectAnnotatedBeatmapLevelCollection;
			this._levelFilteringNavigationController.didStartLoadingEvent += this.HandleLevelFilteringNavigationControllerDidStartLoading;
			this._levelSelectionNavigationController.didChangeDifficultyBeatmapEvent += this.HandleLevelSelectionNavigationControllerDidChangeDifficultyBeatmap;
			this._levelSelectionNavigationController.didSelectLevelPackEvent += this.HandleLevelSelectionNavigationControllerDidSelectPack;
			this._levelSelectionNavigationController.didPressPlayButtonEvent += this.HandleLevelSelectionNavigationControllerDidPressPlayButton;
			this._levelSelectionNavigationController.didPressPracticeButtonEvent += this.HandleLevelSelectionNavigationControllerDidPressPracticeButton;
			this._levelSelectionNavigationController.didChangeDifficultyBeatmapEvent += this.HandleLevelSelectionNavigationControllerDidChangeDifficultyBeatmap;
			this._levelSelectionNavigationController.didPresentDetailContentEvent += this.HandleLevelSelectionNavigationControllerDidPresentDetailContent;
			this._levelSelectionNavigationController.didPressOpenPackButtonEvent += this.HandleLevelSelectionNavigationControllerDidPressOpenPackButton;
			this._practiceViewController.didPressPlayButtonEvent += this.HandlePracticeViewControllerDidPressPlayButton;
			bool enableCustomLevels = true;
			this._levelFilteringNavigationController.Setup(this.hidePacksIfOneOrNone, enableCustomLevels, this._annotatedBeatmapLevelCollectionToBeSelectedAfterPresent);
			this._levelSelectionNavigationController.SelectLevel(this._beatmapLevelToBeSelectedAfterPresent);
			this.ResetSetupParameters();
			base.ProvideInitialViewControllers(this._levelSelectionNavigationController, this.hideGameplaySetup ? null : this._gameplaySetupViewController, null, this._levelFilteringNavigationController, this.topScreenViewController);
			base.showBackButton = this.showBackButtonForMainViewController;
		}
		this.LevelSelectionFlowCoordinatorDidActivate(firstActivation, activationType);
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0003F514 File Offset: 0x0003D714
	protected sealed override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._levelFilteringNavigationController.didSelectAnnotatedBeatmapLevelCollectionEvent -= this.HandleLevelFilteringNavigationControllerDidSelectAnnotatedBeatmapLevelCollection;
			this._levelFilteringNavigationController.didStartLoadingEvent -= this.HandleLevelFilteringNavigationControllerDidStartLoading;
			this._levelSelectionNavigationController.didChangeDifficultyBeatmapEvent -= this.HandleLevelSelectionNavigationControllerDidChangeDifficultyBeatmap;
			this._levelSelectionNavigationController.didSelectLevelPackEvent -= this.HandleLevelSelectionNavigationControllerDidSelectPack;
			this._levelSelectionNavigationController.didPressPlayButtonEvent -= this.HandleLevelSelectionNavigationControllerDidPressPlayButton;
			this._levelSelectionNavigationController.didPressPracticeButtonEvent -= this.HandleLevelSelectionNavigationControllerDidPressPracticeButton;
			this._levelSelectionNavigationController.didChangeDifficultyBeatmapEvent -= this.HandleLevelSelectionNavigationControllerDidChangeDifficultyBeatmap;
			this._levelSelectionNavigationController.didPresentDetailContentEvent -= this.HandleLevelSelectionNavigationControllerDidPresentDetailContent;
			this._levelSelectionNavigationController.didPressOpenPackButtonEvent -= this.HandleLevelSelectionNavigationControllerDidPressOpenPackButton;
			this._practiceViewController.didPressPlayButtonEvent -= this.HandlePracticeViewControllerDidPressPlayButton;
		}
		this.LevelSelectionFlowCoordinatorDidDeactivate(deactivationType);
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0000C387 File Offset: 0x0000A587
	protected void DismissPracticeViewController(Action finishedCallback, bool immediately)
	{
		if (base.topViewController != this._practiceViewController)
		{
			return;
		}
		base.DismissViewController(this._practiceViewController, finishedCallback, immediately);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0003F614 File Offset: 0x0003D814
	protected override void TopViewControllerWillChange(ViewController oldViewController, ViewController newViewController, bool immediately)
	{
		if (newViewController == this._levelSelectionNavigationController)
		{
			base.SetLeftScreenViewController(this.hideGameplaySetup ? null : this._gameplaySetupViewController, immediately);
			base.SetBottomScreenViewController(this._levelFilteringNavigationController, immediately);
			base.showBackButton = this.showBackButtonForMainViewController;
			if (this._levelSelectionNavigationController.selectedDifficultyBeatmap != null)
			{
				base.SetRightScreenViewController(this.leaderboardViewController, immediately);
				return;
			}
		}
		else
		{
			if (newViewController == this._practiceViewController)
			{
				base.SetLeftScreenViewController(this.hideGameplaySetup ? null : this._gameplaySetupViewController, immediately);
				base.SetRightScreenViewController(null, immediately);
				base.SetBottomScreenViewController(null, immediately);
				base.showBackButton = true;
				return;
			}
			base.SetLeftScreenViewController(null, immediately);
			base.SetRightScreenViewController(null, immediately);
			base.SetBottomScreenViewController(null, immediately);
			base.showBackButton = false;
		}
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0000C3AB File Offset: 0x0000A5AB
	private void HandleLevelFilteringNavigationControllerDidStartLoading(LevelFilteringNavigationController controller)
	{
		this._levelSelectionNavigationController.ShowLoading();
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
	private void HandleLevelFilteringNavigationControllerDidSelectAnnotatedBeatmapLevelCollection(LevelFilteringNavigationController controller, IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection, GameObject noDataInfoPrefab, BeatmapCharacteristicSO preferedBeatmapCharacteristic)
	{
		if (preferedBeatmapCharacteristic != null)
		{
			this._playerDataModel.playerData.SetLastSelectedBeatmapCharacteristic(preferedBeatmapCharacteristic);
		}
		this._levelSelectionNavigationController.SetData(annotatedBeatmapLevelCollection, true, this.showPlayerStatsInDetailView, !this.hidePracticeButton, noDataInfoPrefab);
		base.SetRightScreenViewController(null, false);
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
	private void HandleLevelSelectionNavigationControllerDidSelectPack(LevelSelectionNavigationController viewController, IBeatmapLevelPack pack)
	{
		base.SetRightScreenViewController(null, false);
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0000C3C2 File Offset: 0x0000A5C2
	private void HandleLevelSelectionNavigationControllerDidPressPlayButton(LevelSelectionNavigationController viewController)
	{
		this.StartLevelOrShow360Warning(null, false);
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x0000C3CC File Offset: 0x0000A5CC
	private void HandleLevelSelectionNavigationControllerDidPressPracticeButton(LevelSelectionNavigationController viewController, IBeatmapLevel level)
	{
		this._practiceViewController.Init(level);
		base.PresentViewController(this._practiceViewController, null, false);
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
	private void HandleLevelSelectionNavigationControllerDidChangeDifficultyBeatmap(LevelSelectionNavigationController viewController, IDifficultyBeatmap beatmap)
	{
		this.leaderboardViewController.SetData(beatmap);
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
	private void HandleLevelSelectionNavigationControllerDidPresentDetailContent(LevelSelectionNavigationController viewController, StandardLevelDetailViewController.ContentType contentType)
	{
		if (contentType == StandardLevelDetailViewController.ContentType.OwnedAndReady)
		{
			this.leaderboardViewController.SetData(viewController.selectedDifficultyBeatmap);
			base.SetRightScreenViewController(this.leaderboardViewController, false);
			return;
		}
		base.SetRightScreenViewController(null, false);
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0000C423 File Offset: 0x0000A623
	private void HandleLevelSelectionNavigationControllerDidPressOpenPackButton(LevelSelectionNavigationController viewController, IBeatmapLevelPack levelPack)
	{
		this._levelFilteringNavigationController.SelectAnnotatedBeatmapLevelCollection(levelPack);
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0000C431 File Offset: 0x0000A631
	private void HandlePracticeViewControllerDidPressPlayButton()
	{
		this.StartLevelOrShow360Warning(null, true);
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0003F728 File Offset: 0x0003D928
	private void StartLevel(IDifficultyBeatmap difficultyBeatmap, Action beforeSceneSwitchCallback, bool practice)
	{
		PlayerSpecificSettings playerSettings = this._gameplaySetupViewController.playerSettings;
		GameplayModifiers gameplayModifiers = new GameplayModifiers(this._gameplaySetupViewController.gameplayModifiers);
		PracticeSettings practiceSettings = practice ? this._practiceViewController.practiceSettings : null;
		OverrideEnvironmentSettings environmentOverrideSettings = this._gameplaySetupViewController.environmentOverrideSettings;
		ColorSchemesSettings colorSchemesSettings = this._gameplaySetupViewController.colorSchemesSettings;
		this._menuTransitionsHelper.StartStandardLevel(difficultyBeatmap, environmentOverrideSettings, colorSchemesSettings.overrideDefaultColors ? colorSchemesSettings.GetSelectedColorScheme() : null, gameplayModifiers, playerSettings, practiceSettings, Localization.Get("BUTTON_MENU"), false, beforeSceneSwitchCallback, new Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults>(this.HandleStandardLevelDidFinish));
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0003F7B8 File Offset: 0x0003D9B8
	protected void StartLevelOrShow360Warning(Action beforeSceneSwitchCallback, bool practice)
	{
		IDifficultyBeatmap difficultyBeatmap = this._levelSelectionNavigationController.selectedDifficultyBeatmap;
		if (this._appStaticSettings.enable360DegreeLevels && !this._vrPlatformHelper.isAlwaysWireless && this._playerDataModel.playerData.shouldShow360Warning && difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic == this._degree360BeatmapCharacteristic)
		{
			this._playerDataModel.playerData.Mark360WarningAsShown();
			this._playerDataModel.Save();
			Action action = null;
			this._simpleDialogPromptViewController.Init(Localization.Get("PROMPT_INFORMATION"), Localization.Get("PROMPT_HAVENT_PLAYED_360_YET"), Localization.Get("PROMPT_OK"), null, delegate(int buttonNumber)
			{
				if (buttonNumber == 0)
				{
					LevelSelectionFlowCoordinator levelSelectionFlowCoordinator = this;
					IDifficultyBeatmap difficultyBeatmap2 = difficultyBeatmap;
					Action beforeSceneSwitchCallback2;
					if ((beforeSceneSwitchCallback2 = action) == null)
					{
						beforeSceneSwitchCallback2 = (action = delegate()
						{
							this.DismissViewController(this._simpleDialogPromptViewController, null, true);
							if (beforeSceneSwitchCallback != null)
							{
								beforeSceneSwitchCallback();
							}
						});
					}
					this.StartLevel(difficultyBeatmap, beforeSceneSwitchCallback2, practice);
				}
			});
			base.PresentViewController(this._simpleDialogPromptViewController, null, false);
			return;
		}
		this.StartLevel(difficultyBeatmap, beforeSceneSwitchCallback, practice);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0003F8B8 File Offset: 0x0003DAB8
	private void HandleStandardLevelDidFinish(StandardLevelScenesTransitionSetupDataSO standardLevelScenesTransitionSetupData, LevelCompletionResults levelCompletionResults)
	{
		bool isInViewControllerHierarchy = this._practiceViewController.isInViewControllerHierarchy;
		IDifficultyBeatmap selectedDifficultyBeatmap = this._levelSelectionNavigationController.selectedDifficultyBeatmap;
		GameplayModifiers gameplayModifiers = this._gameplaySetupViewController.gameplayModifiers;
		this.ProcessLevelCompletionResultsAfterLevelDidFinish(levelCompletionResults, selectedDifficultyBeatmap, gameplayModifiers, isInViewControllerHierarchy);
		this._levelSelectionNavigationController.RefreshDetail();
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0000C43B File Offset: 0x0000A63B
	protected bool HandleBasicLevelCompletionResults(LevelCompletionResults levelCompletionResults, bool practice)
	{
		if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
		{
			this.StartLevelOrShow360Warning(null, practice);
			return true;
		}
		return levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Failed && levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared;
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0000C465 File Offset: 0x0000A665
	protected override void BackButtonWasPressed(ViewController topViewController)
	{
		if (topViewController == this._practiceViewController)
		{
			this.DismissPracticeViewController(null, false);
		}
	}

	// Token: 0x06001013 RID: 4115
	protected abstract void ProcessLevelCompletionResultsAfterLevelDidFinish(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice);

	// Token: 0x04001038 RID: 4152
	[SerializeField]
	private BeatmapCharacteristicSO _degree360BeatmapCharacteristic;

	// Token: 0x04001039 RID: 4153
	[Inject]
	protected PlayerDataModel _playerDataModel;

	// Token: 0x0400103A RID: 4154
	[Inject]
	private MenuTransitionsHelper _menuTransitionsHelper;

	// Token: 0x0400103B RID: 4155
	[Inject]
	protected LevelSelectionNavigationController _levelSelectionNavigationController;

	// Token: 0x0400103C RID: 4156
	[Inject]
	private PracticeViewController _practiceViewController;

	// Token: 0x0400103D RID: 4157
	[Inject]
	private GameplaySetupViewController _gameplaySetupViewController;

	// Token: 0x0400103E RID: 4158
	[Inject]
	private LevelFilteringNavigationController _levelFilteringNavigationController;

	// Token: 0x0400103F RID: 4159
	[Inject]
	private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	// Token: 0x04001040 RID: 4160
	[Inject]
	private VRPlatformHelper _vrPlatformHelper;

	// Token: 0x04001041 RID: 4161
	[Inject]
	private AppStaticSettingsSO _appStaticSettings;

	// Token: 0x04001042 RID: 4162
	private IAnnotatedBeatmapLevelCollection _annotatedBeatmapLevelCollectionToBeSelectedAfterPresent;

	// Token: 0x04001043 RID: 4163
	private IPreviewBeatmapLevel _beatmapLevelToBeSelectedAfterPresent;
}
