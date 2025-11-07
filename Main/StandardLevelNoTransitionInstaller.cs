using System;
using UnityEngine;
using Zenject;

// Token: 0x02000462 RID: 1122
public class StandardLevelNoTransitionInstaller : NoTransitionInstaller
{
	// Token: 0x0600152B RID: 5419 RVA: 0x0004E490 File Offset: 0x0004C690
	public override void InstallBindings(DiContainer container)
	{
		IDifficultyBeatmap difficultyBeatmap = this._beatmapLevel.GetDifficultyBeatmap(this._beatmapCharacteristic, this._beatmapDifficulty);
		this._scenesTransitionSetupData.Init(difficultyBeatmap, new OverrideEnvironmentSettings(), this._colorScheme.colorScheme, this._gameplayModifiers, this._playerSpecificSettings, this._practiceSettings, this._backButtonText, this._useTestNoteCutSoundEffects);
		this._scenesTransitionSetupData.InstallBindings(container);
	}

	// Token: 0x0400151E RID: 5406
	[SerializeField]
	private BeatmapLevelSO _beatmapLevel;

	// Token: 0x0400151F RID: 5407
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x04001520 RID: 5408
	[SerializeField]
	private BeatmapDifficulty _beatmapDifficulty;

	// Token: 0x04001521 RID: 5409
	[SerializeField]
	private ColorSchemeSO _colorScheme;

	// Token: 0x04001522 RID: 5410
	[SerializeField]
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x04001523 RID: 5411
	[SerializeField]
	private PlayerSpecificSettings _playerSpecificSettings;

	// Token: 0x04001524 RID: 5412
	[SerializeField]
	private PracticeSettings _practiceSettings;

	// Token: 0x04001525 RID: 5413
	[SerializeField]
	private string _backButtonText;

	// Token: 0x04001526 RID: 5414
	[SerializeField]
	private bool _useTestNoteCutSoundEffects;

	// Token: 0x04001527 RID: 5415
	[Space]
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _scenesTransitionSetupData;
}
