using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000382 RID: 898
public class NetEaseArcadeFlowCoordinator : LevelSelectionFlowCoordinator
{
	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06001033 RID: 4147 RVA: 0x0000C5EA File Offset: 0x0000A7EA
	protected override LeaderboardViewController leaderboardViewController
	{
		get
		{
			if (!this._netEaseManager.supportsLeaderboards)
			{
				return this._localLeaderboardViewController;
			}
			return this._netEaseLeaderboardViewController;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06001034 RID: 4148 RVA: 0x0000C606 File Offset: 0x0000A806
	protected override ViewController topScreenViewController
	{
		get
		{
			return this._tabBarViewController;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06001035 RID: 4149 RVA: 0x00002907 File Offset: 0x00000B07
	protected override bool showPlayerStatsInDetailView
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0003FEB0 File Offset: 0x0003E0B0
	protected override void LevelSelectionFlowCoordinatorDidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._localLeaderboardViewController.Setup(false);
			this._howToPlayViewController.Setup(true);
			base.gameplaySetupViewController.Setup(true, false, false);
			this._tabBarViewController.Setup(new TabBarViewController.TabBarItem[]
			{
				new TabBarViewController.TabBarItem(Localization.Get("LEVEL_SELECTION_BUTTON"), delegate()
				{
					base.ReplaceTopViewController(base.levelSelectionNavigationController, null, true, ViewController.SlideAnimationDirection.Down);
				}),
				new TabBarViewController.TabBarItem(Localization.Get("HOW_TO_PLAY_BUTTON"), new Action(this.HandleHowToPlayTabSelected)),
				new TabBarViewController.TabBarItem(Localization.Get("LOGOUT_BUTTON"), new Action(this.HandleLogoutTabWasSelected))
			});
			this._howToPlayViewController.didPressTutorialButtonEvent += this.HandleHowToPlayViewControllerDidPressTutorialButton;
			this._resultsViewController.continueButtonPressedEvent += this.HandleResultsViewControllerContinueButtonPressed;
			this._resultsViewController.restartButtonPressedEvent += this.HandleResultsViewControllerRestartButtonPressed;
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0003FFAC File Offset: 0x0003E1AC
	protected override void LevelSelectionFlowCoordinatorDidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			if (this._howToPlayViewController != null)
			{
				this._howToPlayViewController.didPressTutorialButtonEvent -= this.HandleHowToPlayViewControllerDidPressTutorialButton;
			}
			if (this._tabBarViewController != null)
			{
				this._tabBarViewController.Clear();
			}
			if (this._resultsViewController != null)
			{
				this._resultsViewController.continueButtonPressedEvent -= this.HandleResultsViewControllerContinueButtonPressed;
				this._resultsViewController.restartButtonPressedEvent -= this.HandleResultsViewControllerRestartButtonPressed;
			}
		}
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0000C60E File Offset: 0x0000A80E
	protected override void ProcessLevelCompletionResultsAfterLevelDidFinish(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (base.HandleBasicLevelCompletionResults(levelCompletionResults, practice))
		{
			return;
		}
		this.ProcessScore(levelCompletionResults, difficultyBeatmap, gameplayModifiers, practice);
		this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, false);
		base.PresentViewController(this._resultsViewController, null, true);
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0000C645 File Offset: 0x0000A845
	private void ProcessScore(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (practice)
		{
			return;
		}
		string levelID = difficultyBeatmap.level.levelID;
		BeatmapDifficulty difficulty = difficultyBeatmap.difficulty;
		BeatmapCharacteristicSO beatmapCharacteristic = difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic;
		if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			this.AddScoreToLeaderboards(levelCompletionResults, difficultyBeatmap, gameplayModifiers, practice);
		}
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x00040038 File Offset: 0x0003E238
	private void AddScoreToLeaderboards(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (this.leaderboardViewController == this._localLeaderboardViewController)
		{
			string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
			string userName = this._netEaseManager.userName;
			this._localLeaderboardViewController.leaderboardsModel.GetHighScore(leaderboardID, LocalLeaderboardsModel.LeaderboardType.AllTime);
			int modifiedScore = levelCompletionResults.modifiedScore;
			this._localLeaderboardViewController.leaderboardsModel.AddScore(leaderboardID, userName, levelCompletionResults.modifiedScore, levelCompletionResults.fullCombo);
			this._localLeaderboardViewController.leaderboardsModel.Save();
			return;
		}
		if (this.leaderboardViewController == this._netEaseLeaderboardViewController)
		{
			this._netEaseManager.UploadHighscore(LeaderboardsModel.GetLeaderboardID(difficultyBeatmap), levelCompletionResults.modifiedScore);
			this._netEaseLeaderboardViewController.Refresh();
		}
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x000400EC File Offset: 0x0003E2EC
	private async void LogoutAsync()
	{
		base.SetGlobalUserInteraction(false);
		await this._netEaseManager.LogoutAsync();
		base.menuTransitionsHelper.RestartGame();
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0000C085 File Offset: 0x0000A285
	private void HandleHowToPlayViewControllerDidPressTutorialButton()
	{
		base.menuTransitionsHelper.StartTutorial(null);
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0000C67F File Offset: 0x0000A87F
	private void HandleHowToPlayTabSelected()
	{
		if (base.isInPracticeView)
		{
			base.DismissPracticeViewController(delegate
			{
				base.ReplaceTopViewController(this._howToPlayViewController, null, true, ViewController.SlideAnimationDirection.Down);
			}, true);
			return;
		}
		base.ReplaceTopViewController(this._howToPlayViewController, null, true, ViewController.SlideAnimationDirection.Down);
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x0000C6AC File Offset: 0x0000A8AC
	private void HandleLogoutTabWasSelected()
	{
		this.LogoutAsync();
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController viewController)
	{
		base.DismissViewController(viewController, null, false);
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x00040128 File Offset: 0x0003E328
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		base.StartLevelOrShow360Warning(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
		}, resultsViewController.practice);
	}

	// Token: 0x04001062 RID: 4194
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x04001063 RID: 4195
	[SerializeField]
	private NetEaseLeaderboardViewController _netEaseLeaderboardViewController;

	// Token: 0x04001064 RID: 4196
	[Inject]
	private TabBarViewController _tabBarViewController;

	// Token: 0x04001065 RID: 4197
	[Inject]
	private NetEaseManager _netEaseManager;

	// Token: 0x04001066 RID: 4198
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x04001067 RID: 4199
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x04001068 RID: 4200
	[Inject]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	// Token: 0x04001069 RID: 4201
	[Inject]
	private HowToPlayViewController _howToPlayViewController;
}
