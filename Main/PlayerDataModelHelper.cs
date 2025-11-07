using System;

// Token: 0x020001F9 RID: 505
public static class PlayerDataModelHelper
{
	// Token: 0x060007FD RID: 2045 RVA: 0x0000693F File Offset: 0x00004B3F
	public static PlayerAllOverallStatsData ToPlayerAllOverallStatsData(this PlayerSaveData.PlayerAllOverallStatsData playerAllOverallStatsData)
	{
		if (playerAllOverallStatsData == null)
		{
			return new PlayerAllOverallStatsData();
		}
		return new PlayerAllOverallStatsData(playerAllOverallStatsData.campaignOverallStatsData.ToPlayerOverallStats(), playerAllOverallStatsData.soloFreePlayOverallStatsData.ToPlayerOverallStats(), playerAllOverallStatsData.partyFreePlayOverallStatsData.ToPlayerOverallStats());
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x00006970 File Offset: 0x00004B70
	public static PlayerAllOverallStatsData ToPlayerAllOverallStatsData(this PlayerSaveDataV1_0_1.PlayerAllOverallStatsData playerAllOverallStatsData)
	{
		if (playerAllOverallStatsData == null)
		{
			return new PlayerAllOverallStatsData();
		}
		return new PlayerAllOverallStatsData(playerAllOverallStatsData.campaignOverallStatsData.ToPlayerOverallStats(), playerAllOverallStatsData.soloFreePlayOverallStatsData.ToPlayerOverallStats(), playerAllOverallStatsData.partyFreePlayOverallStatsData.ToPlayerOverallStats());
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00029D38 File Offset: 0x00027F38
	public static PlayerAllOverallStatsData.PlayerOverallStatsData ToPlayerOverallStats(this PlayerSaveData.PlayerOverallStatsData playerAllOverallStatsData)
	{
		if (playerAllOverallStatsData == null)
		{
			return new PlayerAllOverallStatsData.PlayerOverallStatsData();
		}
		return new PlayerAllOverallStatsData.PlayerOverallStatsData(playerAllOverallStatsData.goodCutsCount, playerAllOverallStatsData.badCutsCount, playerAllOverallStatsData.missedCutsCount, playerAllOverallStatsData.totalScore, playerAllOverallStatsData.playedLevelsCount, playerAllOverallStatsData.cleardLevelsCount, playerAllOverallStatsData.failedLevelsCount, playerAllOverallStatsData.fullComboCount, playerAllOverallStatsData.timePlayed, playerAllOverallStatsData.handDistanceTravelled, playerAllOverallStatsData.cummulativeCutScoreWithoutMultiplier);
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00029D98 File Offset: 0x00027F98
	public static PlayerAllOverallStatsData.PlayerOverallStatsData ToPlayerOverallStats(this PlayerSaveDataV1_0_1.PlayerOverallStatsData playerAllOverallStatsData)
	{
		if (playerAllOverallStatsData == null)
		{
			return new PlayerAllOverallStatsData.PlayerOverallStatsData();
		}
		return new PlayerAllOverallStatsData.PlayerOverallStatsData(playerAllOverallStatsData.goodCutsCount, playerAllOverallStatsData.badCutsCount, playerAllOverallStatsData.missedCutsCount, playerAllOverallStatsData.totalScore, playerAllOverallStatsData.playedLevelsCount, playerAllOverallStatsData.cleardLevelsCount, playerAllOverallStatsData.failedLevelsCount, playerAllOverallStatsData.fullComboCount, playerAllOverallStatsData.timePlayed, playerAllOverallStatsData.handDistanceTravelled, playerAllOverallStatsData.cummulativeCutScoreWithoutMultiplier);
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x000069A1 File Offset: 0x00004BA1
	public static PlayerSaveData.PlayerAllOverallStatsData ToPlayerAllOverallStatsData(this PlayerAllOverallStatsData playerAllOverallStatsData)
	{
		if (playerAllOverallStatsData == null)
		{
			return new PlayerSaveData.PlayerAllOverallStatsData();
		}
		return new PlayerSaveData.PlayerAllOverallStatsData(playerAllOverallStatsData.campaignOverallStatsData.ToPlayerOverallStatsData(), playerAllOverallStatsData.soloFreePlayOverallStatsData.ToPlayerOverallStatsData(), playerAllOverallStatsData.partyFreePlayOverallStatsData.ToPlayerOverallStatsData());
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00029DF8 File Offset: 0x00027FF8
	public static PlayerSaveData.PlayerOverallStatsData ToPlayerOverallStatsData(this PlayerAllOverallStatsData.PlayerOverallStatsData playerOverallStatsData)
	{
		if (playerOverallStatsData == null)
		{
			return new PlayerSaveData.PlayerOverallStatsData();
		}
		return new PlayerSaveData.PlayerOverallStatsData(playerOverallStatsData.goodCutsCount, playerOverallStatsData.badCutsCount, playerOverallStatsData.missedCutsCount, playerOverallStatsData.totalScore, playerOverallStatsData.playedLevelsCount, playerOverallStatsData.cleardLevelsCount, playerOverallStatsData.failedLevelsCount, playerOverallStatsData.fullComboCount, playerOverallStatsData.timePlayed, playerOverallStatsData.handDistanceTravelled, playerOverallStatsData.cummulativeCutScoreWithoutMultiplier);
	}
}
