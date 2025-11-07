using System;

// Token: 0x02000154 RID: 340
public static class BeatmapLevelDataExtensions
{
	// Token: 0x06000543 RID: 1347 RVA: 0x00023284 File Offset: 0x00021484
	public static IDifficultyBeatmap GetDifficultyBeatmap(this IBeatmapLevelData beatmapLevelData, BeatmapCharacteristicSO beatmapCharacteristic, BeatmapDifficulty difficulty)
	{
		if (beatmapLevelData != null)
		{
			IDifficultyBeatmapSet difficultyBeatmapSet = beatmapLevelData.GetDifficultyBeatmapSet(beatmapCharacteristic);
			if (difficultyBeatmapSet != null)
			{
				foreach (IDifficultyBeatmap difficultyBeatmap in difficultyBeatmapSet.difficultyBeatmaps)
				{
					if (difficultyBeatmap.difficulty == difficulty)
					{
						return difficultyBeatmap;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000232C4 File Offset: 0x000214C4
	public static IDifficultyBeatmapSet GetDifficultyBeatmapSet(this IBeatmapLevelData beatmapLevelData, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		if (beatmapLevelData != null && beatmapLevelData.difficultyBeatmapSets != null)
		{
			foreach (IDifficultyBeatmapSet difficultyBeatmapSet in beatmapLevelData.difficultyBeatmapSets)
			{
				if (difficultyBeatmapSet.beatmapCharacteristic == beatmapCharacteristic)
				{
					return difficultyBeatmapSet;
				}
			}
		}
		return null;
	}
}
