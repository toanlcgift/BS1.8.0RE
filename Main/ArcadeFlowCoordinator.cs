using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000376 RID: 886
public class ArcadeFlowCoordinator : LevelSelectionFlowCoordinator
{
	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0000C058 File Offset: 0x0000A258
	protected override LeaderboardViewController leaderboardViewController
	{
		get
		{
			return this._localLeaderboardViewController;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0000C060 File Offset: 0x0000A260
	protected override ViewController topScreenViewController
	{
		get
		{
			return this._tabBarViewController;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00002907 File Offset: 0x00000B07
	protected override bool showPlayerStatsInDetailView
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool hidePacksIfOneOrNone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
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

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0003EAE4 File Offset: 0x0003CCE4
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

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0003EB7C File Offset: 0x0003CD7C
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

	// Token: 0x06000FC4 RID: 4036 RVA: 0x0003EC64 File Offset: 0x0003CE64
	private bool WillScoreGoToLeaderboard(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, bool practice)
	{
		if (practice || levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared)
		{
			return false;
		}
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
		return this._localLeaderboardsModel.WillScoreGoIntoLeaderboard(leaderboardID, levelCompletionResults.modifiedScore);
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x0003EC98 File Offset: 0x0003CE98
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

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0000C068 File Offset: 0x0000A268
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController resultsViewController)
	{
		base.DismissViewController(resultsViewController, null, false);
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0003ECFC File Offset: 0x0003CEFC
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		base.StartLevelOrShow360Warning(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
		}, resultsViewController.practice);
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0000C085 File Offset: 0x0000A285
	private void HandleHowToPlayViewControllerDidPressTutorialButton()
	{
		base.menuTransitionsHelper.StartTutorial(null);
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0000C093 File Offset: 0x0000A293
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

	// Token: 0x0400100C RID: 4108
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardsModel;

	// Token: 0x0400100D RID: 4109
	[Space]
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x0400100E RID: 4110
	[SerializeField]
	private MenuLightsPresetSO _resultsLightsPreset;

	// Token: 0x0400100F RID: 4111
	[Inject]
	private TabBarViewController _tabBarViewController;

	// Token: 0x04001010 RID: 4112
	[Inject]
	private HowToPlayViewController _howToPlayViewController;

	// Token: 0x04001011 RID: 4113
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x04001012 RID: 4114
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x04001013 RID: 4115
	[Inject]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	// Token: 0x04001014 RID: 4116
	[Inject]
	private EnterPlayerGuestNameViewController _enterNameViewController;
}
