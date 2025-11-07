using System;
using System.Collections;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x0200038D RID: 909
public class SimpleRetailDemoFlowCoordinator : FlowCoordinator
{
	// Token: 0x0600107B RID: 4219 RVA: 0x00040B58 File Offset: 0x0003ED58
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._level1DifficultyBeatmap = this._demoLevel1.beatmapLevelData.GetDifficultyBeatmap(this._demoLevel1Characteristic, this._demoLevel1Difficulty);
			this._level2DifficultyBeatmap = this._demoLevel2.beatmapLevelData.GetDifficultyBeatmap(this._demoLevel2Characteristic, this._demoLevel2Difficulty);
		}
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._howToPlayViewController.Setup(false);
			base.ProvideInitialViewControllers(this._simpleDemoViewController, this._howToPlayViewController, null, null, null);
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
		this._simpleDemoViewController.didFinishEvent += this.HandleSimpleDemoViewControllerDidFinish;
		this._resultsViewController.continueButtonPressedEvent += this.HandleResultsViewControllerContinueButtonPressed;
		this._resultsViewController.restartButtonPressedEvent += this.HandleResultsViewControllerRestartButtonPressed;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00040C28 File Offset: 0x0003EE28
	protected override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		this._simpleDemoViewController.didFinishEvent -= this.HandleSimpleDemoViewControllerDidFinish;
		this._resultsViewController.continueButtonPressedEvent -= this.HandleResultsViewControllerContinueButtonPressed;
		this._resultsViewController.restartButtonPressedEvent -= this.HandleResultsViewControllerRestartButtonPressed;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00040C7C File Offset: 0x0003EE7C
	private void HandleSimpleDemoViewControllerDidFinish(SimpleRetailDemoViewController viewController, SimpleRetailDemoViewController.MenuButton menuButton)
	{
		switch (menuButton)
		{
		case SimpleRetailDemoViewController.MenuButton.PlayTutorial:
			this._menuTransitionsHelper.StartTutorial(null);
			return;
		case SimpleRetailDemoViewController.MenuButton.PlayLevel1:
			this._selectedLevelDifficultyBeatmap = this._level1DifficultyBeatmap;
			this.StartLevel(null);
			return;
		case SimpleRetailDemoViewController.MenuButton.PlayLevel2:
			this._selectedLevelDifficultyBeatmap = this._level2DifficultyBeatmap;
			this.StartLevel(null);
			return;
		case SimpleRetailDemoViewController.MenuButton.Exit:
			this._fadeInOut.FadeOutInstant();
			Application.Quit();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0000C922 File Offset: 0x0000AB22
	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController resultsViewController)
	{
		if (this._quittingApplication)
		{
			return;
		}
		this._quittingApplication = true;
		base.StartCoroutine(this.QuitApplicationCoroutine());
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0000C941 File Offset: 0x0000AB41
	private IEnumerator QuitApplicationCoroutine()
	{
		float num = 0.5f;
		this._fadeInOut.FadeOut(num);
		yield return new WaitForSeconds(num);
		Application.Quit();
		yield break;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00040CE8 File Offset: 0x0003EEE8
	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController resultsViewController)
	{
		this.StartLevel(delegate
		{
			this.DismissViewController(resultsViewController, null, true);
			this.SetLeftScreenViewController(this._howToPlayViewController, true);
		});
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00040D1C File Offset: 0x0003EF1C
	private void StartLevel(Action beforeSceneSwitchCallback)
	{
		PlayerSpecificSettings defaultSettings = PlayerSpecificSettings.defaultSettings;
		GameplayModifiers defaultModifiers = GameplayModifiers.defaultModifiers;
		defaultModifiers.demoNoFail = true;
		this._menuTransitionsHelper.StartStandardLevel(this._selectedLevelDifficultyBeatmap, null, null, defaultModifiers, defaultSettings, null, Localization.Get("BUTTON_MENU"), false, beforeSceneSwitchCallback, new Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults>(this.HandleLevelDidFinish));
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00040D6C File Offset: 0x0003EF6C
	private void HandleLevelDidFinish(StandardLevelScenesTransitionSetupDataSO standardLevelSceneSetupData, LevelCompletionResults levelCompletionResults)
	{
		if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
		{
			this.StartLevel(null);
			return;
		}
		if (levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Failed && levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared)
		{
			return;
		}
		this._resultsViewController.Init(levelCompletionResults, this._selectedLevelDifficultyBeatmap, false, false);
		base.PresentViewController(this._resultsViewController, null, true);
	}

	// Token: 0x04001096 RID: 4246
	[SerializeField]
	private MenuTransitionsHelper _menuTransitionsHelper;

	// Token: 0x04001097 RID: 4247
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x04001098 RID: 4248
	[Space]
	[SerializeField]
	private BeatmapLevelSO _demoLevel1;

	// Token: 0x04001099 RID: 4249
	[SerializeField]
	private BeatmapCharacteristicSO _demoLevel1Characteristic;

	// Token: 0x0400109A RID: 4250
	[SerializeField]
	private BeatmapDifficulty _demoLevel1Difficulty;

	// Token: 0x0400109B RID: 4251
	[SerializeField]
	private BeatmapLevelSO _demoLevel2;

	// Token: 0x0400109C RID: 4252
	[SerializeField]
	private BeatmapCharacteristicSO _demoLevel2Characteristic;

	// Token: 0x0400109D RID: 4253
	[SerializeField]
	private BeatmapDifficulty _demoLevel2Difficulty;

	// Token: 0x0400109E RID: 4254
	[Space]
	[SerializeField]
	private SimpleRetailDemoViewController _simpleDemoViewController;

	// Token: 0x0400109F RID: 4255
	[Inject]
	private HowToPlayViewController _howToPlayViewController;

	// Token: 0x040010A0 RID: 4256
	[Inject]
	private ResultsViewController _resultsViewController;

	// Token: 0x040010A1 RID: 4257
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x040010A2 RID: 4258
	[Inject]
	private FadeInOutController _fadeInOut;

	// Token: 0x040010A3 RID: 4259
	private IDifficultyBeatmap _level1DifficultyBeatmap;

	// Token: 0x040010A4 RID: 4260
	private IDifficultyBeatmap _level2DifficultyBeatmap;

	// Token: 0x040010A5 RID: 4261
	private IDifficultyBeatmap _selectedLevelDifficultyBeatmap;

	// Token: 0x040010A6 RID: 4262
	private bool _quittingApplication;
}
