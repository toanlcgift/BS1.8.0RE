using System;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000474 RID: 1140
public class MenuTransitionsHelper : MonoBehaviour
{
	// Token: 0x0600155D RID: 5469 RVA: 0x0004E5DC File Offset: 0x0004C7DC
	public void StartStandardLevel(IDifficultyBeatmap difficultyBeatmap, OverrideEnvironmentSettings overrideEnvironmentSettings, ColorScheme overrideColorScheme, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, string backButtonText, bool useTestNoteCutSoundEffects, Action beforeSceneSwitchCallback, Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> levelFinishedCallback)
	{
		this.StartStandardLevel(difficultyBeatmap, overrideEnvironmentSettings, overrideColorScheme, gameplayModifiers, playerSpecificSettings, practiceSettings, backButtonText, useTestNoteCutSoundEffects, beforeSceneSwitchCallback, null, levelFinishedCallback);
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x0004E604 File Offset: 0x0004C804
	public void StartStandardLevel(IDifficultyBeatmap difficultyBeatmap, OverrideEnvironmentSettings overrideEnvironmentSettings, ColorScheme overrideColorScheme, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, string backButtonText, bool useTestNoteCutSoundEffects, Action beforeSceneSwitchCallback, Action afterSceneSwitchCallback, Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> levelFinishedCallback)
	{
		this._standardLevelFinishedCallback = levelFinishedCallback;
		this._standardLevelScenesTransitionSetupData.didFinishEvent -= this.HandleMainGameSceneDidFinish;
		this._standardLevelScenesTransitionSetupData.didFinishEvent += this.HandleMainGameSceneDidFinish;
		this._standardLevelScenesTransitionSetupData.Init(difficultyBeatmap, overrideEnvironmentSettings, overrideColorScheme, gameplayModifiers, playerSpecificSettings, practiceSettings, backButtonText, useTestNoteCutSoundEffects);
		this._gameScenesManager.PushScenes(this._standardLevelScenesTransitionSetupData, 0.7f, beforeSceneSwitchCallback, afterSceneSwitchCallback);
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x0004E67C File Offset: 0x0004C87C
	public void StartMissionLevel(IDifficultyBeatmap difficultyBeatmap, OverrideEnvironmentSettings overrideEnvironmentSettings, ColorScheme overrideColorScheme, GameplayModifiers gameplayModifiers, MissionObjective[] missionObjectives, PlayerSpecificSettings playerSpecificSettings, Action beforeSceneSwitchCallback, Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults> levelFinishedCallback)
	{
		this._missionLevelFinishedCallback = levelFinishedCallback;
		this._missionLevelScenesTransitionSetupData.didFinishEvent -= this.HandleMissionLevelSceneDidFinish;
		this._missionLevelScenesTransitionSetupData.didFinishEvent += this.HandleMissionLevelSceneDidFinish;
		this._missionLevelScenesTransitionSetupData.Init(difficultyBeatmap, missionObjectives, overrideEnvironmentSettings, overrideColorScheme, gameplayModifiers, playerSpecificSettings, Localization.Get("BUTTON_MENU"));
		this._gameScenesManager.PushScenes(this._missionLevelScenesTransitionSetupData, 0.7f, beforeSceneSwitchCallback, null);
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x0004E6F8 File Offset: 0x0004C8F8
	public void StartTutorial(Action beforeSceneSwitchCallback = null)
	{
		this._tutorialScenesTransitionSetupData.didFinishEvent -= this.HandleTutorialSceneDidFinish;
		this._tutorialScenesTransitionSetupData.didFinishEvent += this.HandleTutorialSceneDidFinish;
		this._tutorialScenesTransitionSetupData.Init();
		this._gameScenesManager.PushScenes(this._tutorialScenesTransitionSetupData, 0.7f, beforeSceneSwitchCallback, null);
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0004E758 File Offset: 0x0004C958
	public void ShowCredits()
	{
		this._creditsScenesTransitionSetupData.didFinishEvent -= this.HandleCreditsSceneDidFinish;
		this._creditsScenesTransitionSetupData.didFinishEvent += this.HandleCreditsSceneDidFinish;
		this._creditsScenesTransitionSetupData.Init();
		this._gameScenesManager.PushScenes(this._creditsScenesTransitionSetupData, 0.7f, null, null);
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0004E7B8 File Offset: 0x0004C9B8
	public void StartBeatmapEditor(Action beatmapEditorFinishedCallback)
	{
		this._beatmapEditorFinishedCallback = beatmapEditorFinishedCallback;
		this._beatmapEditorScenesTransitionSetupData.didFinishEvent -= this.HandleBeatmapEditorSceneDidFinish;
		this._beatmapEditorScenesTransitionSetupData.didFinishEvent += this.HandleBeatmapEditorSceneDidFinish;
		this._beatmapEditorScenesTransitionSetupData.Init();
		this._gameScenesManager.PushScenes(this._beatmapEditorScenesTransitionSetupData, 0.7f, null, null);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x00010087 File Offset: 0x0000E287
	public void RestartGame()
	{
		this._appInitScenesTransitionSetupDataContainer.appInitScenesTransitionSetupData.Init();
		this._gameScenesManager.ClearAndOpenScenes(this._appInitScenesTransitionSetupDataContainer.appInitScenesTransitionSetupData, 0.35f, null, null, true);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x0004E820 File Offset: 0x0004CA20
	private void HandleMainGameSceneDidFinish(StandardLevelScenesTransitionSetupDataSO standardLevelScenesTransitionSetupData, LevelCompletionResults levelCompletionResults)
	{
		standardLevelScenesTransitionSetupData.didFinishEvent -= this.HandleMainGameSceneDidFinish;
		this._gameScenesManager.PopScenes((levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed || levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared) ? 1.3f : 0.35f, null, delegate
		{
			Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> standardLevelFinishedCallback = this._standardLevelFinishedCallback;
			if (standardLevelFinishedCallback == null)
			{
				return;
			}
			standardLevelFinishedCallback(standardLevelScenesTransitionSetupData, levelCompletionResults);
		});
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x0004E8A0 File Offset: 0x0004CAA0
	private void HandleMissionLevelSceneDidFinish(MissionLevelScenesTransitionSetupDataSO missionLevelScenesTransitionSetupData, MissionCompletionResults missionCompletionResults)
	{
		missionLevelScenesTransitionSetupData.didFinishEvent -= this.HandleMissionLevelSceneDidFinish;
		this._gameScenesManager.PopScenes((missionCompletionResults.levelCompletionResults.levelEndAction != LevelCompletionResults.LevelEndAction.Quit) ? 1.3f : 0.35f, null, delegate
		{
			Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults> missionLevelFinishedCallback = this._missionLevelFinishedCallback;
			if (missionLevelFinishedCallback == null)
			{
				return;
			}
			missionLevelFinishedCallback(missionLevelScenesTransitionSetupData, missionCompletionResults);
		});
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x0004E918 File Offset: 0x0004CB18
	private void HandleTutorialSceneDidFinish(TutorialScenesTransitionSetupDataSO tutorialSceneTransitionSetupData, TutorialScenesTransitionSetupDataSO.TutorialEndStateType endState)
	{
		tutorialSceneTransitionSetupData.didFinishEvent -= this.HandleTutorialSceneDidFinish;
		this._gameScenesManager.PopScenes((endState == TutorialScenesTransitionSetupDataSO.TutorialEndStateType.Completed) ? 1.3f : 0.35f, null, delegate
		{
			if (endState == TutorialScenesTransitionSetupDataSO.TutorialEndStateType.Restart)
			{
				this.StartTutorial(null);
			}
		});
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000100B7 File Offset: 0x0000E2B7
	private void HandleCreditsSceneDidFinish(CreditsScenesTransitionSetupDataSO creditsSceneTransitionSetupData)
	{
		creditsSceneTransitionSetupData.didFinishEvent -= this.HandleCreditsSceneDidFinish;
		this._gameScenesManager.PopScenes(1.3f, null, null);
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000100DD File Offset: 0x0000E2DD
	private void HandleBeatmapEditorSceneDidFinish(BeatmapEditorScenesTransitionSetupDataSO beatmapEditorScenesTransitionSetupData)
	{
		beatmapEditorScenesTransitionSetupData.didFinishEvent -= this.HandleBeatmapEditorSceneDidFinish;
		this._gameScenesManager.PopScenes(0.35f, null, delegate
		{
			Action beatmapEditorFinishedCallback = this._beatmapEditorFinishedCallback;
			if (beatmapEditorFinishedCallback == null)
			{
				return;
			}
			beatmapEditorFinishedCallback();
		});
	}

	// Token: 0x0400154C RID: 5452
	[SerializeField]
	private AppInitScenesTransitionSetupDataContainerSO _appInitScenesTransitionSetupDataContainer;

	// Token: 0x0400154D RID: 5453
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelScenesTransitionSetupData;

	// Token: 0x0400154E RID: 5454
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelScenesTransitionSetupData;

	// Token: 0x0400154F RID: 5455
	[SerializeField]
	private TutorialScenesTransitionSetupDataSO _tutorialScenesTransitionSetupData;

	// Token: 0x04001550 RID: 5456
	[SerializeField]
	private CreditsScenesTransitionSetupDataSO _creditsScenesTransitionSetupData;

	// Token: 0x04001551 RID: 5457
	[SerializeField]
	private BeatmapEditorScenesTransitionSetupDataSO _beatmapEditorScenesTransitionSetupData;

	// Token: 0x04001552 RID: 5458
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04001553 RID: 5459
	private Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> _standardLevelFinishedCallback;

	// Token: 0x04001554 RID: 5460
	private Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults> _missionLevelFinishedCallback;

	// Token: 0x04001555 RID: 5461
	private Action _beatmapEditorFinishedCallback;
}
