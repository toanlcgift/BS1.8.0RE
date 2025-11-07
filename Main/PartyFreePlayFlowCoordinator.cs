using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000385 RID: 901
public class PartyFreePlayFlowCoordinator : LevelSelectionFlowCoordinator
{
	// Token: 0x14000089 RID: 137
	// (add) Token: 0x06001048 RID: 4168 RVA: 0x00040234 File Offset: 0x0003E434
	// (remove) Token: 0x06001049 RID: 4169 RVA: 0x0004026C File Offset: 0x0003E46C
	public event Action<LevelSelectionFlowCoordinator> didFinishEvent;

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x0600104A RID: 4170 RVA: 0x0000C732 File Offset: 0x0000A932
	protected override LeaderboardViewController leaderboardViewController
	{
		get
		{
			return this._localLeaderboardViewController;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x0600104B RID: 4171 RVA: 0x00002907 File Offset: 0x00000B07
	protected override bool showPlayerStatsInDetailView
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x0600104C RID: 4172 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool showBackButtonForMainViewController
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000402A4 File Offset: 0x0003E4A4
	protected override void LevelSelectionFlowCoordinatorDidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			base.title = Localization.Get("TITLE_PARTY");
			this._localLeaderboardViewController.Setup(true);
			base.gameplaySetupViewController.Setup(true, true, true);
			this._resultsViewController.continueButtonPressedEvent += this.HandleResultsViewControllerContinueButtonPressed;
			this._resultsViewController.restartButtonPressedEvent += this.HandleResultsViewControllerRestartButtonPressed;
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00040320 File Offset: 0x0003E520
	protected override void LevelSelectionFlowCoordinatorDidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._localLeaderboardViewController.leaderboardsModel.ClearLastScorePosition();
			if (this._resultsViewController != null)
			{
				this._resultsViewController.continueButtonPressedEvent -= this.HandleResultsViewControllerContinueButtonPressed;
				this._resultsViewController.restartButtonPressedEvent -= this.HandleResultsViewControllerRestartButtonPressed;
			}
		}
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0004037C File Offset: 0x0003E57C
	protected override void ProcessLevelCompletionResultsAfterLevelDidFinish(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (!practice)
		{
			this._playerDataModel.playerData.playerAllOverallStatsData.UpdatePartyFreePlayOverallStatsData(levelCompletionResults, difficultyBeatmap);
		}
		if (base.HandleBasicLevelCompletionResults(levelCompletionResults, practice))
		{
			if (!practice)
			{
				this._playerDataModel.Save();
			}
			return;
		}
		string leaderboardId = LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
		if (this.WillScoreGoToLeaderboard(levelCompletionResults, leaderboardId, practice))
		{
			this._enterNameViewController.Init(delegate(EnterPlayerGuestNameViewController viewController, string playerName)
			{
				bool flag = this.IsNewHighScore(levelCompletionResults, leaderboardId);
				if (flag)
				{
					this._menuLightsManager.SetColorPreset(this._resultsLightsPreset, true);
				}
				this.ProcessScore(levelCompletionResults, leaderboardId, playerName);
				this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, flag);
				this.ReplaceTopViewController(this._resultsViewController, null, false, ViewController.SlideAnimationDirection.Down);
			});
			base.PresentViewController(this._enterNameViewController, null, true);
		}
		else
		{
			this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, false);
			base.PresentViewController(this._resultsViewController, null, true);
		}
		this._playerDataModel.Save();
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0000C73A File Offset: 0x0000A93A
	private bool WillScoreGoToLeaderboard(LevelCompletionResults levelCompletionResults, string leaderboardId, bool practice)
	{
		return !practice && levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared && this._localLeaderboardViewController.leaderboardsModel.WillScoreGoIntoLeaderboard(leaderboardId, levelCompletionResults.modifiedScore);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0000C761 File Offset: 0x0000A961
	private bool IsNewHighScore(LevelCompletionResults levelCompletionResults, string leaderboardId)
	{
		return this._localLeaderboardViewController.leaderboardsModel.GetHighScore(leaderboardId, LocalLeaderboardsModel.LeaderboardType.AllTime) < levelCompletionResults.modifiedScore;
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0000C77D File Offset: 0x0000A97D
	private void ProcessScore(LevelCompletionResults levelCompletionResults, string leaderboardId, string playerName)
	{
		if (string.IsNullOrEmpty(playerName))
		{
			return;
		}
		this._localLeaderboardViewController.leaderboardsModel.AddScore(leaderboardId, playerName, levelCompletionResults.modifiedScore, levelCompletionResults.fullCombo);
		this._localLeaderboardViewController.leaderboardsModel.Save();
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController resultsViewController)
	{
		base.DismissViewController(resultsViewController, null, false);
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00040488 File Offset: 0x0003E688
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		base.StartLevelOrShow360Warning(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
		}, resultsViewController.practice);
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
	protected override void BackButtonWasPressed(ViewController topViewController)
	{
		base.BackButtonWasPressed(topViewController);
		if (topViewController == base.levelSelectionNavigationController)
		{
			Action<LevelSelectionFlowCoordinator> action = this.didFinishEvent;
			if (action == null)
			{
				return;
			}
			action(this);
		}
	}

	// Token: 0x04001070 RID: 4208
	[Space]
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x04001071 RID: 4209
	[SerializeField]
	private MenuLightsPresetSO _resultsLightsPreset;

	// Token: 0x04001072 RID: 4210
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x04001073 RID: 4211
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x04001074 RID: 4212
	[Inject]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	// Token: 0x04001075 RID: 4213
	[Inject]
	private EnterPlayerGuestNameViewController _enterNameViewController;
}
