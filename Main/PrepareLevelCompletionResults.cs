using System;
using UnityEngine;
using Zenject;

// Token: 0x020002E4 RID: 740
public class PrepareLevelCompletionResults : MonoBehaviour
{
	// Token: 0x06000C94 RID: 3220 RVA: 0x00036C5C File Offset: 0x00034E5C
	public LevelCompletionResults FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType levelEndStateType, LevelCompletionResults.LevelEndAction levelEndAction)
	{
		BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings = this._beatmapObjectExecutionRatingsRecorder.beatmapObjectExecutionRatings.ToArray();
		this._multiplierValuesRecorder.multiplierValues.ToArray();
		int prevFrameRawScore = this._scoreController.prevFrameRawScore;
		int prevFrameModifiedScore = this._scoreController.prevFrameModifiedScore;
		int maxCombo = this._scoreController.maxCombo;
		float[] saberActivityValues = this._saberActivityCounter.saberMovementAveragingValueRecorder.GetHistoryValues().ToArray();
		float leftSaberMovementDistance = this._saberActivityCounter.leftSaberMovementDistance;
		float rightSaberMovementDistance = this._saberActivityCounter.rightSaberMovementDistance;
		float[] handActivityValues = this._saberActivityCounter.handMovementAveragingValueRecorder.GetHistoryValues().ToArray();
		float leftHandMovementDistance = this._saberActivityCounter.leftHandMovementDistance;
		float rightHandMovementDistance = this._saberActivityCounter.rightHandMovementDistance;
		float songLength = this._gameSongController.songLength;
		float energy = this._gameEnergyCounter.energy;
		return new LevelCompletionResults(this._beatmapData.notesCount, beatmapObjectExecutionRatings, this._gameplayModifiers, this._gameplayModifiersModelSO, prevFrameRawScore, prevFrameModifiedScore, maxCombo, saberActivityValues, leftSaberMovementDistance, rightSaberMovementDistance, handActivityValues, leftHandMovementDistance, rightHandMovementDistance, songLength, levelEndStateType, levelEndAction, energy, this._audioTimeSyncController.songTime);
	}

	// Token: 0x04000D10 RID: 3344
	[SerializeField]
	private GameplayModifiersModelSO _gameplayModifiersModelSO;

	// Token: 0x04000D11 RID: 3345
	[Inject]
	private SaberActivityCounter _saberActivityCounter;

	// Token: 0x04000D12 RID: 3346
	[Inject]
	private BeatmapObjectExecutionRatingsRecorder _beatmapObjectExecutionRatingsRecorder;

	// Token: 0x04000D13 RID: 3347
	[Inject]
	private MultiplierValuesRecorder _multiplierValuesRecorder;

	// Token: 0x04000D14 RID: 3348
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000D15 RID: 3349
	[Inject]
	private GameEnergyCounter _gameEnergyCounter;

	// Token: 0x04000D16 RID: 3350
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000D17 RID: 3351
	[Inject]
	private BeatmapData _beatmapData;

	// Token: 0x04000D18 RID: 3352
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000D19 RID: 3353
	[Inject]
	private GameplayModifiers _gameplayModifiers;
}
