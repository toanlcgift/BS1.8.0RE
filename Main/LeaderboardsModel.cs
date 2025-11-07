using System;

// Token: 0x020000D5 RID: 213
public class LeaderboardsModel
{
	// Token: 0x06000327 RID: 807 RVA: 0x0000401C File Offset: 0x0000221C
	public static string GetLeaderboardID(IDifficultyBeatmap difficultyBeatmap)
	{
		return difficultyBeatmap.SerializedName();
	}
}
