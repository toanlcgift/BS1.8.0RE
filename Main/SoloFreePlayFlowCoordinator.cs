using System;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000390 RID: 912
public class SoloFreePlayFlowCoordinator : LevelSelectionFlowCoordinator
{
	// Token: 0x1400008B RID: 139
	// (add) Token: 0x0600108C RID: 4236 RVA: 0x00040E20 File Offset: 0x0003F020
	// (remove) Token: 0x0600108D RID: 4237 RVA: 0x00040E58 File Offset: 0x0003F058
	public event Action<LevelSelectionFlowCoordinator> didFinishEvent;

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x0600108E RID: 4238 RVA: 0x0000C993 File Offset: 0x0000AB93
	protected override LeaderboardViewController leaderboardViewController
	{
		get
		{
			return this._platformLeaderboardViewController;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x0600108F RID: 4239 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool showPlayerStatsInDetailView
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06001090 RID: 4240 RVA: 0x00002969 File Offset: 0x00000B69
	protected override bool showBackButtonForMainViewController
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00040E90 File Offset: 0x0003F090
	protected override void LevelSelectionFlowCoordinatorDidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			base.title = Localization.Get("TITLE_SOLO");
			base.gameplaySetupViewController.Setup(true, true, true);
			this._resultsViewController.continueButtonPressedEvent += this.HandleResultsViewControllerContinueButtonPressed;
			this._resultsViewController.restartButtonPressedEvent += this.HandleResultsViewControllerRestartButtonPressed;
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x00040F00 File Offset: 0x0003F100
	protected override void LevelSelectionFlowCoordinatorDidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy && this._resultsViewController != null)
		{
			this._resultsViewController.continueButtonPressedEvent -= this.HandleResultsViewControllerContinueButtonPressed;
			this._resultsViewController.restartButtonPressedEvent -= this.HandleResultsViewControllerRestartButtonPressed;
		}
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x00040F4C File Offset: 0x0003F14C
	protected override void ProcessLevelCompletionResultsAfterLevelDidFinish(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers, bool practice)
	{
		if (!practice)
		{
			this._playerDataModel.playerData.playerAllOverallStatsData.UpdateSoloFreePlayOverallStatsData(levelCompletionResults, difficultyBeatmap);
		}
		if (base.HandleBasicLevelCompletionResults(levelCompletionResults, practice))
		{
			if (!practice)
			{
				this._playerDataModel.Save();
			}
			return;
		}
		bool flag = false;
		if (!practice)
		{
			PlayerLevelStatsData playerLevelStatsData = this._playerDataModel.playerData.GetPlayerLevelStatsData(difficultyBeatmap);
			flag = this.IsNewHighScore(playerLevelStatsData, levelCompletionResults);
			if (flag)
			{
				this._menuLightsManager.SetColorPreset(this._resultsLightsPreset, true);
			}
			this.ProcessScore(playerLevelStatsData, levelCompletionResults, difficultyBeatmap);
		}
		this._resultsViewController.Init(levelCompletionResults, difficultyBeatmap, practice, flag);
		base.PresentViewController(this._resultsViewController, null, true);
		this._playerDataModel.Save();
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0000C99B File Offset: 0x0000AB9B
	private bool IsNewHighScore(PlayerLevelStatsData playerLevelStats, LevelCompletionResults levelCompletionResults)
	{
		return playerLevelStats.highScore < levelCompletionResults.modifiedScore;
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x00040FF8 File Offset: 0x0003F1F8
	private void ProcessScore(PlayerLevelStatsData playerLevelStats, LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap)
	{
		playerLevelStats.IncreaseNumberOfGameplays();
		if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			playerLevelStats.UpdateScoreData(levelCompletionResults.modifiedScore, levelCompletionResults.maxCombo, levelCompletionResults.fullCombo, levelCompletionResults.rank);
			this._platformLeaderboardsModel.UploadScore(difficultyBeatmap, levelCompletionResults.rawScore, levelCompletionResults.modifiedScore, levelCompletionResults.fullCombo, levelCompletionResults.goodCutsCount, levelCompletionResults.badCutsCount, levelCompletionResults.missedCount, levelCompletionResults.maxCombo, levelCompletionResults.gameplayModifiers);
		}
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x0000C9AB File Offset: 0x0000ABAB
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController viewController)
	{
		base.DismissViewController(viewController, null, false);
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00041070 File Offset: 0x0003F270
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		base.StartLevelOrShow360Warning(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, false);
		}, resultsViewController.practice);
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
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

	// Token: 0x040010AC RID: 4268
	[Space]
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x040010AD RID: 4269
	[SerializeField]
	private MenuLightsPresetSO _resultsLightsPreset;

	// Token: 0x040010AE RID: 4270
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x040010AF RID: 4271
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x040010B0 RID: 4272
	[Inject]
	private PlatformLeaderboardViewController _platformLeaderboardViewController;

	// Token: 0x040010B1 RID: 4273
	[Inject]
	private PlatformLeaderboardsModel _platformLeaderboardsModel;
}
