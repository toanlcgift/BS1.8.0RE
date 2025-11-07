using System;
using UnityEngine;

// Token: 0x0200047A RID: 1146
public class StandardLevelScenesTransitionSetupDataSO : ScenesTransitionSetupDataSO
{
	// Token: 0x140000CB RID: 203
	// (add) Token: 0x06001578 RID: 5496 RVA: 0x0004EAAC File Offset: 0x0004CCAC
	// (remove) Token: 0x06001579 RID: 5497 RVA: 0x0004EAE4 File Offset: 0x0004CCE4
	public event Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> didFinishEvent;

	// Token: 0x0600157A RID: 5498 RVA: 0x0004EB1C File Offset: 0x0004CD1C
	public void Init(IDifficultyBeatmap difficultyBeatmap, OverrideEnvironmentSettings overrideEnvironmentSettings, ColorScheme overrideColorScheme, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, string backButtonText, bool useTestNoteCutSoundEffects = false)
	{
		EnvironmentInfoSO environmentInfoSO = difficultyBeatmap.GetEnvironmentInfo();
		if (overrideEnvironmentSettings != null && overrideEnvironmentSettings.overrideEnvironments)
		{
			environmentInfoSO = overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(environmentInfoSO.environmentType);
		}
		ColorScheme colorScheme = overrideColorScheme ?? new ColorScheme(environmentInfoSO.colorScheme);
		IBeatmapLevel level = difficultyBeatmap.level;
		SceneSetupData[] sceneSetupData = new SceneSetupData[]
		{
			new StandardGameplaySceneSetupData(playerSpecificSettings.autoRestart, level.songName, level.songSubName, difficultyBeatmap.difficulty.Name(), backButtonText),
			new GameplayCoreSceneSetupData(difficultyBeatmap, gameplayModifiers, playerSpecificSettings, practiceSettings, useTestNoteCutSoundEffects),
			new GameCoreSceneSetupData(colorScheme)
		};
		SceneInfo[] scenes = new SceneInfo[]
		{
			environmentInfoSO.sceneInfo,
			this._standardGameplaySceneInfo,
			this._gameplayCoreSceneInfo,
			this._gameCoreSceneInfo
		};
		base.Init(scenes, sceneSetupData);
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x00010191 File Offset: 0x0000E391
	public void Finish(LevelCompletionResults levelCompletionResults)
	{
		Action<StandardLevelScenesTransitionSetupDataSO, LevelCompletionResults> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this, levelCompletionResults);
	}

	// Token: 0x04001562 RID: 5474
	[SerializeField]
	private SceneInfo _standardGameplaySceneInfo;

	// Token: 0x04001563 RID: 5475
	[SerializeField]
	private SceneInfo _gameplayCoreSceneInfo;

	// Token: 0x04001564 RID: 5476
	[SerializeField]
	private SceneInfo _gameCoreSceneInfo;
}
