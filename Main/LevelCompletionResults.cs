using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class LevelCompletionResults
{
	// Token: 0x060003B3 RID: 947 RVA: 0x0001FD48 File Offset: 0x0001DF48
	public LevelCompletionResults(int levelNotesCount, BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings, GameplayModifiers gameplayModifiers, GameplayModifiersModelSO gameplayModifiersModel, int rawScore, int modifiedScore, int maxCombo, float[] saberActivityValues, float leftSaberMovementDistance, float rightSaberMovementDistance, float[] handActivityValues, float leftHandMovementDistance, float rightHandMovementDistance, float songDuration, LevelCompletionResults.LevelEndStateType levelEndStateType, LevelCompletionResults.LevelEndAction levelEndAction, float energy, float songTime)
	{
		this.beatmapObjectExecutionRatings = beatmapObjectExecutionRatings;
		this.gameplayModifiers = gameplayModifiers;
		this.gameplayModifiersModel = gameplayModifiersModel;
		this.rawScore = rawScore;
		this.modifiedScore = modifiedScore;
		this.maxCombo = maxCombo;
		this.saberActivityValues = saberActivityValues;
		this.leftSaberMovementDistance = leftSaberMovementDistance;
		this.rightSaberMovementDistance = rightSaberMovementDistance;
		this.handActivityValues = handActivityValues;
		this.leftHandMovementDistance = leftHandMovementDistance;
		this.rightHandMovementDistance = rightHandMovementDistance;
		this.songDuration = songDuration;
		this.levelEndStateType = levelEndStateType;
		this.levelEndAction = levelEndAction;
		this.energy = energy;
		this.endSongTime = songTime;
		foreach (BeatmapObjectExecutionRating beatmapObjectExecutionRating in beatmapObjectExecutionRatings)
		{
			if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Note)
			{
				NoteExecutionRating noteExecutionRating = (NoteExecutionRating)beatmapObjectExecutionRating;
				if (noteExecutionRating.rating == NoteExecutionRating.Rating.GoodCut)
				{
					this.goodCutsCount++;
					float num = Mathf.Abs(noteExecutionRating.cutDirDeviation);
					float num2 = Mathf.Abs(noteExecutionRating.cutTimeDeviation);
					if (this.goodCutsCount == 1)
					{
						this.minDirDeviation = (this.maxDirDeviation = (this.averageDirDeviation = num));
						this.minTimeDeviation = (this.maxTimeDeviation = (this.averageTimeDeviation = num2));
					}
					else
					{
						if (this.minDirDeviation > num)
						{
							this.minDirDeviation = num;
						}
						if (this.maxDirDeviation < num)
						{
							this.maxDirDeviation = num;
						}
						this.averageDirDeviation += num;
						if (this.minTimeDeviation > num2)
						{
							this.minTimeDeviation = num2;
						}
						if (this.maxTimeDeviation < num2)
						{
							this.maxTimeDeviation = num2;
						}
						this.averageTimeDeviation += num2;
					}
					if (this.maxCutScore < noteExecutionRating.cutScore)
					{
						this.maxCutScore = noteExecutionRating.cutScore;
					}
					this.averageCutScore += noteExecutionRating.cutScore;
				}
				else if (noteExecutionRating.rating == NoteExecutionRating.Rating.BadCut)
				{
					this.badCutsCount++;
				}
				else if (noteExecutionRating.rating == NoteExecutionRating.Rating.Missed)
				{
					this.missedCount++;
				}
			}
			else if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Bomb)
			{
				if (((BombExecutionRating)beatmapObjectExecutionRating).rating == BombExecutionRating.Rating.OK)
				{
					this.okCount++;
				}
				else
				{
					this.notGoodCount++;
				}
			}
			else if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Obstacle)
			{
				if (((ObstacleExecutionRating)beatmapObjectExecutionRating).rating == ObstacleExecutionRating.Rating.OK)
				{
					this.okCount++;
				}
				else
				{
					this.notGoodCount++;
				}
			}
		}
		if (this.goodCutsCount > 0)
		{
			this.averageCutScore /= this.goodCutsCount;
			this.averageDirDeviation /= (float)this.goodCutsCount;
			this.averageTimeDeviation /= (float)this.goodCutsCount;
		}
		int maxRawScore = ScoreModel.MaxRawScoreForNumberOfNotes(levelNotesCount);
		int maxModifiedScore = gameplayModifiersModel.MaxModifiedScoreForMaxRawScore(maxRawScore, gameplayModifiers);
		this.rank = RankModel.GetRankForScore(this.rawScore, this.modifiedScore, maxRawScore, maxModifiedScore);
		this.fullCombo = (this.goodCutsCount == levelNotesCount && this.badCutsCount == 0 && this.notGoodCount == 0);
	}

	// Token: 0x040003F9 RID: 1017
	public readonly BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings;

	// Token: 0x040003FA RID: 1018
	public readonly GameplayModifiers gameplayModifiers;

	// Token: 0x040003FB RID: 1019
	public readonly GameplayModifiersModelSO gameplayModifiersModel;

	// Token: 0x040003FC RID: 1020
	public readonly int modifiedScore;

	// Token: 0x040003FD RID: 1021
	public readonly int rawScore;

	// Token: 0x040003FE RID: 1022
	public readonly RankModel.Rank rank;

	// Token: 0x040003FF RID: 1023
	public readonly bool fullCombo;

	// Token: 0x04000400 RID: 1024
	public readonly float[] saberActivityValues;

	// Token: 0x04000401 RID: 1025
	public readonly float leftSaberMovementDistance;

	// Token: 0x04000402 RID: 1026
	public readonly float rightSaberMovementDistance;

	// Token: 0x04000403 RID: 1027
	public readonly float[] handActivityValues;

	// Token: 0x04000404 RID: 1028
	public readonly float leftHandMovementDistance;

	// Token: 0x04000405 RID: 1029
	public readonly float rightHandMovementDistance;

	// Token: 0x04000406 RID: 1030
	public readonly float songDuration;

	// Token: 0x04000407 RID: 1031
	public readonly LevelCompletionResults.LevelEndStateType levelEndStateType;

	// Token: 0x04000408 RID: 1032
	public readonly LevelCompletionResults.LevelEndAction levelEndAction;

	// Token: 0x04000409 RID: 1033
	public readonly float energy;

	// Token: 0x0400040A RID: 1034
	public readonly int goodCutsCount;

	// Token: 0x0400040B RID: 1035
	public readonly int badCutsCount;

	// Token: 0x0400040C RID: 1036
	public readonly int missedCount;

	// Token: 0x0400040D RID: 1037
	public readonly int notGoodCount;

	// Token: 0x0400040E RID: 1038
	public readonly int okCount;

	// Token: 0x0400040F RID: 1039
	public readonly int averageCutScore;

	// Token: 0x04000410 RID: 1040
	public readonly int maxCutScore;

	// Token: 0x04000411 RID: 1041
	public readonly int maxCombo;

	// Token: 0x04000412 RID: 1042
	public readonly float minDirDeviation;

	// Token: 0x04000413 RID: 1043
	public readonly float maxDirDeviation;

	// Token: 0x04000414 RID: 1044
	public readonly float averageDirDeviation;

	// Token: 0x04000415 RID: 1045
	public readonly float minTimeDeviation;

	// Token: 0x04000416 RID: 1046
	public readonly float maxTimeDeviation;

	// Token: 0x04000417 RID: 1047
	public readonly float averageTimeDeviation;

	// Token: 0x04000418 RID: 1048
	public readonly float endSongTime;

	// Token: 0x020000F5 RID: 245
	public enum LevelEndStateType
	{
		// Token: 0x0400041A RID: 1050
		None,
		// Token: 0x0400041B RID: 1051
		Cleared,
		// Token: 0x0400041C RID: 1052
		Failed
	}

	// Token: 0x020000F6 RID: 246
	public enum LevelEndAction
	{
		// Token: 0x0400041E RID: 1054
		None,
		// Token: 0x0400041F RID: 1055
		Quit,
		// Token: 0x04000420 RID: 1056
		Restart,
		// Token: 0x04000421 RID: 1057
		LostConnection,
		// Token: 0x04000422 RID: 1058
		RoomDestroyed
	}
}
