using System;
using UnityEngine;
using Zenject;

// Token: 0x02000461 RID: 1121
public class MissionLevelNoTransitionInstaller : NoTransitionInstaller
{
	// Token: 0x06001529 RID: 5417 RVA: 0x0004E428 File Offset: 0x0004C628
	public override void InstallBindings(DiContainer container)
	{
		IDifficultyBeatmap difficultyBeatmap = this._beatmapLevel.GetDifficultyBeatmap(this._beatmapCharacteristic, this._beatmapDifficulty);
		this._scenesTransitionSetupData.Init(difficultyBeatmap, this._missionObjectives, new OverrideEnvironmentSettings(), this._colorScheme.colorScheme, this._gameplayModifiers, this._playerSpecificSettings, this._backButtonText);
		this._scenesTransitionSetupData.InstallBindings(container);
	}

	// Token: 0x04001515 RID: 5397
	[SerializeField]
	private BeatmapLevelSO _beatmapLevel;

	// Token: 0x04001516 RID: 5398
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x04001517 RID: 5399
	[SerializeField]
	private BeatmapDifficulty _beatmapDifficulty;

	// Token: 0x04001518 RID: 5400
	[SerializeField]
	private ColorSchemeSO _colorScheme;

	// Token: 0x04001519 RID: 5401
	[SerializeField]
	private MissionObjective[] _missionObjectives;

	// Token: 0x0400151A RID: 5402
	[SerializeField]
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x0400151B RID: 5403
	[SerializeField]
	private PlayerSpecificSettings _playerSpecificSettings;

	// Token: 0x0400151C RID: 5404
	[SerializeField]
	private string _backButtonText;

	// Token: 0x0400151D RID: 5405
	[Space]
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _scenesTransitionSetupData;
}
