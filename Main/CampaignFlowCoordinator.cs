using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000379 RID: 889
public class CampaignFlowCoordinator : FlowCoordinator
{
	// Token: 0x14000088 RID: 136
	// (add) Token: 0x06000FD1 RID: 4049 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
	// (remove) Token: 0x06000FD2 RID: 4050 RVA: 0x0003EDFC File Offset: 0x0003CFFC
	public event Action<CampaignFlowCoordinator> didFinishEvent;

	// Token: 0x06000FD3 RID: 4051 RVA: 0x0003EE34 File Offset: 0x0003D034
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.title = Localization.Get("TITLE_CAMPAIGN");
		}
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._missionSelectionNavigationController.didPressPlayButtonEvent += this.HandleMissionSelectionNavigationControllerDidPressPlayButton;
			this._missionResultsViewController.continueButtonPressedEvent += this.HandleMissionResultsViewControllerContinueButtonPressed;
			this._missionResultsViewController.retryButtonPressedEvent += this.HandleMissionResultsViewControllerRetryButtonPressed;
			this._missionHelpViewController.didFinishEvent += this.HandleMissionHelpViewControllerDidFinish;
			base.showBackButton = true;
			this._gameplaySetupViewController.Setup(false, true, true);
			base.ProvideInitialViewControllers(this._missionSelectionNavigationController, this._gameplaySetupViewController, null, null, null);
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
	protected override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._missionSelectionNavigationController.didPressPlayButtonEvent -= this.HandleMissionSelectionNavigationControllerDidPressPlayButton;
			this._missionResultsViewController.continueButtonPressedEvent -= this.HandleMissionResultsViewControllerContinueButtonPressed;
			this._missionResultsViewController.retryButtonPressedEvent -= this.HandleMissionResultsViewControllerRetryButtonPressed;
			this._missionHelpViewController.didFinishEvent -= this.HandleMissionHelpViewControllerDidFinish;
		}
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x0000C11B File Offset: 0x0000A31B
	protected override void TopViewControllerWillChange(ViewController oldViewController, ViewController newViewController, bool immediately)
	{
		if (newViewController == this._missionSelectionNavigationController)
		{
			base.SetLeftScreenViewController(this._gameplaySetupViewController, immediately);
			base.showBackButton = true;
			return;
		}
		base.SetLeftScreenViewController(null, immediately);
		base.showBackButton = false;
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x0003EF60 File Offset: 0x0003D160
	private void HandleMissionSelectionNavigationControllerDidPressPlayButton(MissionSelectionNavigationController viewController)
	{
		MissionHelpSO missionHelp = viewController.selectedMissionNode.missionData.missionHelp;
		if (missionHelp != null && !this._playerDataModel.playerData.WasMissionHelpShowed(missionHelp))
		{
			this._playerDataModel.playerData.MissionHelpWasShowed(missionHelp);
			this._menuLightsManager.SetColorPreset(this._newObjectiveLightsPreset, true);
			this._missionHelpViewController.Setup(missionHelp);
			base.PresentViewController(this._missionHelpViewController, null, false);
			return;
		}
		this.StartLevel(null);
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x0003EFE0 File Offset: 0x0003D1E0
	private void HandleMissionHelpViewControllerDidFinish(MissionHelpViewController viewController)
	{
		this.StartLevel(delegate
		{
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
			this.DismissViewController(viewController, null, true);
		});
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0000C14F File Offset: 0x0000A34F
	private void HandleMissionResultsViewControllerContinueButtonPressed(MissionResultsViewController viewController)
	{
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
		base.DismissViewController(viewController, delegate
		{
			this._missionSelectionNavigationController.PresentMissionClearedIfNeeded(delegate(bool presented)
			{
				if (presented && this._showCredits)
				{
					this._showCredits = false;
					this._menuTransitionsHelper.ShowCredits();
				}
			});
		}, false);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0003F014 File Offset: 0x0003D214
	private void HandleMissionResultsViewControllerRetryButtonPressed(MissionResultsViewController viewController)
	{
		this.StartLevel(delegate
		{
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
			this.DismissViewController(viewController, null, true);
		});
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0003F048 File Offset: 0x0003D248
	private void StartLevel(Action beforeSceneSwitchCallback)
	{
		MissionDataSO missionData = this._missionSelectionNavigationController.selectedMissionNode.missionData;
		IDifficultyBeatmap difficultyBeatmap = missionData.level.beatmapLevelData.GetDifficultyBeatmap(missionData.beatmapCharacteristic, missionData.beatmapDifficulty);
		GameplayModifiers gameplayModifiers = missionData.gameplayModifiers;
		MissionObjective[] missionObjectives = missionData.missionObjectives;
		PlayerSpecificSettings playerSpecificSettings = this._playerDataModel.playerData.playerSpecificSettings;
		OverrideEnvironmentSettings overrideEnvironmentSettings = this._playerDataModel.playerData.overrideEnvironmentSettings;
		ColorSchemesSettings colorSchemesSettings = this._playerDataModel.playerData.colorSchemesSettings;
		ColorScheme overrideColorScheme = colorSchemesSettings.overrideDefaultColors ? colorSchemesSettings.GetSelectedColorScheme() : null;
		this._menuTransitionsHelper.StartMissionLevel(difficultyBeatmap, overrideEnvironmentSettings, overrideColorScheme, gameplayModifiers, missionObjectives, playerSpecificSettings, beforeSceneSwitchCallback, new Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults>(this.HandleMissionLevelSceneDidFinish));
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0003F100 File Offset: 0x0003D300
	private void HandleMissionLevelSceneDidFinish(MissionLevelScenesTransitionSetupDataSO missionLevelScenesTransitionSetupData, MissionCompletionResults missionCompletionResults)
	{
		MissionNode selectedMissionNode = this._missionSelectionNavigationController.selectedMissionNode;
		MissionDataSO missionData = selectedMissionNode.missionData;
		if (missionCompletionResults.IsMissionComplete)
		{
			this._showCredits = this._campaignProgressModel.WillFinishGameAfterThisMission(selectedMissionNode.missionId);
			this._campaignProgressModel.SetMissionCleared(selectedMissionNode.missionId);
		}
		this._playerDataModel.playerData.playerAllOverallStatsData.UpdateCampaignOverallStatsData(missionCompletionResults, selectedMissionNode);
		this._playerDataModel.Save();
		if (missionCompletionResults.levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
		{
			this.StartLevel(null);
			return;
		}
		if (missionCompletionResults.levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Failed && missionCompletionResults.levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared)
		{
			return;
		}
		this._menuLightsManager.SetColorPreset(this._resultsLightsPreset, true);
		this._missionResultsViewController.Init(selectedMissionNode, missionCompletionResults);
		base.PresentViewController(this._missionResultsViewController, null, true);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x0000C177 File Offset: 0x0000A377
	protected override void BackButtonWasPressed(ViewController topViewController)
	{
		if (topViewController == this._missionSelectionNavigationController)
		{
			Action<CampaignFlowCoordinator> action = this.didFinishEvent;
			if (action == null)
			{
				return;
			}
			action(this);
		}
	}

	// Token: 0x0400101B RID: 4123
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x0400101C RID: 4124
	[SerializeField]
	private MenuLightsPresetSO _resultsLightsPreset;

	// Token: 0x0400101D RID: 4125
	[SerializeField]
	private MenuLightsPresetSO _newObjectiveLightsPreset;

	// Token: 0x0400101E RID: 4126
	[Inject]
	private MenuTransitionsHelper _menuTransitionsHelper;

	// Token: 0x0400101F RID: 4127
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x04001020 RID: 4128
	[Inject]
	private MissionSelectionNavigationController _missionSelectionNavigationController;

	// Token: 0x04001021 RID: 4129
	[Inject]
	private MissionResultsViewController _missionResultsViewController;

	// Token: 0x04001022 RID: 4130
	[Inject]
	private GameplaySetupViewController _gameplaySetupViewController;

	// Token: 0x04001023 RID: 4131
	[Inject]
	private MissionHelpViewController _missionHelpViewController;

	// Token: 0x04001024 RID: 4132
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001025 RID: 4133
	[Inject]
	private CampaignProgressModel _campaignProgressModel;

	// Token: 0x04001027 RID: 4135
	private bool _showCredits;
}
