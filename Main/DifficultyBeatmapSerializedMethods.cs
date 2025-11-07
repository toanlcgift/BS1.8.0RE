using System;

// Token: 0x02000156 RID: 342
public static class DifficultyBeatmapSerializedMethods
{
	// Token: 0x0600054C RID: 1356 RVA: 0x00023308 File Offset: 0x00021508
	public static string SerializedName(this IDifficultyBeatmap difficultyBeatmap)
	{
		string str = difficultyBeatmap.difficulty.SerializedName();
		return difficultyBeatmap.level.levelID + difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.compoundIdPartName + str;
	}
}
