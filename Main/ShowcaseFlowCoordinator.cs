using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x0200038A RID: 906
public class ShowcaseFlowCoordinator : LevelSelectionFlowCoordinator
{
	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06001065 RID: 4197 RVA: 0x0000C886 File Offset: 0x0000AA86
	protected override LeaderboardViewController leaderboardViewController
	{
		get
		{
			return this._localLeaderboardViewController;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06001066 RID: 4198 RVA: 0x0000C88E File Offset: 0x0000AA8E
	protected override ViewController topScreenViewController
	{
		get
		{
			return this._tabBarViewController;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06001067 RID: 4199 RVA: 0x00002907 File Offset: 0x00000B07
	protected override bool showPlayerStatsInDetailView
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06001068 RID: 4200 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool hidePacksIfOneOrNone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06001069 RID: 4201 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool hideGameplaySetup
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x0600106A RID: 4202 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool hidePracticeButton
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x00040754 File Offset: 0x0003E954
	protected override void LevelSelectionFlowCoordinatorDidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._localLeaderboardViewController.Setup(false);
		}
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._localLeaderboardViewController.Setup(false);
			this._howToPlayViewController.Setup(true);
			base.gameplaySetupViewController.Setup(true, false, false);
			base.gameplaySetupViewController.gameplayModifiers.noFail = true;
			base.gameplaySetupViewController.playerSettings.automaticPlayerHeight = true;
			base.gameplaySetupViewController.playerSettings.playerHeight = 1.6f;
			this._resultsViewController.continueButtonPressedEvent += this.HandleResultsViewControllerContinueButtonPressed;
			this._resultsViewController.restartButtonPressedEvent += this.HandleResultsViewControllerRestartButtonPressed;
			this._howToPlayViewController.didPressTutorialButtonEvent += this.HandleHowToPlayViewControllerDidPressTutorialButton;
			this._tabBarViewController.Setup(new TabBarViewController.TabBarItem[]
			{
				new TabBarViewController.TabBarItem(Localization.Get("LEVEL_SELECTION_BUTTON"), delegate()
				{
					base.ReplaceTopViewController(base.levelSelectionNavigationController, null, false, ViewController.SlideAnimationDirection.Left);
				}),
				new TabBarViewController.TabBarItem(Localization.Get("HOW_TO_PLAY_BUTTON"), new Action(this.HandleHowToPlayTabSelected))
			});
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x00040878 File Offset: 0x0003EA78
	protected override void LevelSelectionFlowCoordinatorDidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._localLeaderboardsModel.ClearLastScorePosition();
			if (this._resultsViewController != null)
			{
				this._resultsViewController.continueButtonPressedEvent -= this.HandleResultsViewControllerContinueButtonPressed;
				this._resultsViewController.restartButtonPressedEvent -= this.HandleResultsViewControllerRestartButtonPressed;
			}
			if (this._howToPlayViewController != null)
			{
				this._howToPlayViewController.didPressTutorialButtonEvent -= this.HandleHowToPlayViewControllerDidPressTutorialButton;
			}
			if (this._tabBarViewController != null)
			{
				this._tabBarViewController.Clear();
			}
		}
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x00040910 File Offset: 0x0003EB10
	protected override void ProcessLevelCompletionResultsAfterLevelDidFinish(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (!practice)
		{
			this._playerDataModel.playerData.playerAllOverallStatsData.UpdatePartyFreePlayOverallStatsData(levelCompletionResults, difficultyBeatmap);
			this._playerDataModel.Save();
		}
		if (base.HandleBasicLevelCompletionResults(levelCompletionResults, practice))
		{
			return;
		}
		if (this.WillScoreGoToLeaderboard(levelCompletionResults, difficultyBeatmap, practice))
		{
			this._enterNameViewController.Init(delegate(EnterPlayerGuestNameViewController viewController, string playerName)
			{
				bool flag = this.ProcessScore(levelCompletionResults, difficultyBeatmap, practice, playerName);
				if (flag)
				{
					this._menuLightsManager.SetColorPreset(this._resultsLightsPreset, true);
				}
				this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, flag);
				this.ReplaceTopViewController(this._resultsViewController, null, false, ViewController.SlideAnimationDirection.Down);
			});
			base.PresentViewController(this._enterNameViewController, null, true);
			return;
		}
		this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, false);
		base.PresentViewController(this._resultsViewController, null, true);
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000409F8 File Offset: 0x0003EBF8
	private bool WillScoreGoToLeaderboard(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, bool practice)
	{
		if (practice || levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared)
		{
			return false;
		}
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
		return this._localLeaderboardsModel.WillScoreGoIntoLeaderboard(leaderboardID, levelCompletionResults.modifiedScore);
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00040A2C File Offset: 0x0003EC2C
	private bool ProcessScore(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, bool practice, string playerName)
	{
		if (!this.WillScoreGoToLeaderboard(levelCompletionResults, difficultyBeatmap, practice) || string.IsNullOrEmpty(playerName))
		{
			return false;
		}
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
		bool result = this._localLeaderboardsModel.GetHighScore(leaderboardID, LocalLeaderboardsModel.LeaderboardType.AllTime) < levelCompletionResults.modifiedScore;
		this._localLeaderboardsModel.AddScore(leaderboardID, playerName, levelCompletionResults.modifiedScore, levelCompletionResults.fullCombo);
		this._localLeaderboardsModel.Save();
		return result;
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0000C896 File Offset: 0x0000AA96
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController resultsViewController)
	{
		base.DismissViewController(resultsViewController, null, false);
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x00040A90 File Offset: 0x0003EC90
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		base.StartLevelOrShow360Warning(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
		}, resultsViewController.practice);
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0000C085 File Offset: 0x0000A285
	private void HandleHowToPlayViewControllerDidPressTutorialButton()
	{
		base.menuTransitionsHelper.StartTutorial(null);
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0000C8B3 File Offset: 0x0000AAB3
	private void HandleHowToPlayTabSelected()
	{
		if (base.isInPracticeView)
		{
			base.DismissPracticeViewController(delegate
			{
				base.ReplaceTopViewController(this._howToPlayViewController, null, false, ViewController.SlideAnimationDirection.Right);
			}, true);
			return;
		}
		base.ReplaceTopViewController(this._howToPlayViewController, null, false, ViewController.SlideAnimationDirection.Right);
	}

	// Token: 0x04001087 RID: 4231
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardsModel;

	// Token: 0x04001088 RID: 4232
	[SerializeField]
	private TabBarViewController _tabBarViewController;

	// Token: 0x04001089 RID: 4233
	[Space]
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x0400108A RID: 4234
	[SerializeField]
	private MenuLightsPresetSO _resultsLightsPreset;

	// Token: 0x0400108B RID: 4235
	[Inject]
	private HowToPlayViewController _howToPlayViewController;

	// Token: 0x0400108C RID: 4236
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x0400108D RID: 4237
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x0400108E RID: 4238
	[Inject]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	// Token: 0x0400108F RID: 4239
	[Inject]
	private EnterPlayerGuestNameViewController _enterNameViewController;
}
