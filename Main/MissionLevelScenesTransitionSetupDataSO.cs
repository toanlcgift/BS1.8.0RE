using System;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public class MissionLevelScenesTransitionSetupDataSO : ScenesTransitionSetupDataSO
{
	// Token: 0x140000CA RID: 202
	// (add) Token: 0x06001571 RID: 5489 RVA: 0x0004E978 File Offset: 0x0004CB78
	// (remove) Token: 0x06001572 RID: 5490 RVA: 0x0004E9B0 File Offset: 0x0004CBB0
	public event Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults> didFinishEvent;

	// Token: 0x06001573 RID: 5491 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
	public void Init(IDifficultyBeatmap difficultyBeatmap, MissionObjective[] missionObjectives, OverrideEnvironmentSettings overrideEnvironmentSettings, ColorScheme overrideColorScheme, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, string backButtonText)
	{
		EnvironmentInfoSO environmentInfoSO = difficultyBeatmap.GetEnvironmentInfo();
		if (overrideEnvironmentSettings != null && overrideEnvironmentSettings.overrideEnvironments)
		{
			environmentInfoSO = overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(environmentInfoSO.environmentType);
		}
		ColorScheme colorScheme = overrideColorScheme ?? new ColorScheme(environmentInfoSO.colorScheme);
		IBeatmapLevel level = difficultyBeatmap.level;
		SceneInfo[] scenes = new SceneInfo[]
		{
			environmentInfoSO.sceneInfo,
			this._missionGameplaySceneInfo,
			this._gameplayCoreSceneInfo,
			this._gameCoreSceneInfo
		};
		SceneSetupData[] sceneSetupData = new SceneSetupData[]
		{
			new MissionGameplaySceneSetupData(missionObjectives, playerSpecificSettings.autoRestart, level.songName, level.songSubName, difficultyBeatmap.difficulty.Name(), backButtonText),
			new GameplayCoreSceneSetupData(difficultyBeatmap, gameplayModifiers, playerSpecificSettings, null, false),
			new GameCoreSceneSetupData(colorScheme)
		};
		base.Init(scenes, sceneSetupData);
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x0001017D File Offset: 0x0000E37D
	public void Finish(MissionCompletionResults levelCompletionResults)
	{
		Action<MissionLevelScenesTransitionSetupDataSO, MissionCompletionResults> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this, levelCompletionResults);
	}

	// Token: 0x0400155E RID: 5470
	[SerializeField]
	private SceneInfo _missionGameplaySceneInfo;

	// Token: 0x0400155F RID: 5471
	[SerializeField]
	private SceneInfo _gameplayCoreSceneInfo;

	// Token: 0x04001560 RID: 5472
	[SerializeField]
	private SceneInfo _gameCoreSceneInfo;
}
