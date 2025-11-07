using System;

// Token: 0x02000116 RID: 278
public static class BeatmapEnvironmentHelper
{
	// Token: 0x06000452 RID: 1106 RVA: 0x000210C8 File Offset: 0x0001F2C8
	public static EnvironmentInfoSO GetEnvironmentInfo(this IDifficultyBeatmap difficultyBeatmap)
	{
		IBeatmapLevel level = difficultyBeatmap.level;
		if (!difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.containsRotationEvents)
		{
			return level.environmentInfo;
		}
		return level.allDirectionsEnvironmentInfo;
	}
}
