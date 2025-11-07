using System;
using System.Collections.Generic;

// Token: 0x020001E6 RID: 486
[Serializable]
public class PlayerSaveDataV1_0_1
{
	// Token: 0x040007D7 RID: 2007
	private const BeatmapDifficulty kDefaulLastSelectedBeatmapDifficulty = BeatmapDifficulty.Normal;

	// Token: 0x040007D8 RID: 2008
	public const string kCurrentVersion = "";

	// Token: 0x040007D9 RID: 2009
	public string version;

	// Token: 0x040007DA RID: 2010
	public List<PlayerSaveDataV1_0_1.LocalPlayer> localPlayers;

	// Token: 0x040007DB RID: 2011
	public List<PlayerSaveDataV1_0_1.GuestPlayer> guestPlayers;

	// Token: 0x040007DC RID: 2012
	public BeatmapDifficulty lastSelectedBeatmapDifficulty = BeatmapDifficulty.Normal;

	// Token: 0x020001E7 RID: 487
	[Serializable]
	public class GameplayModifiers
	{
		// Token: 0x040007DD RID: 2013
		public PlayerSaveDataV1_0_1.GameplayModifiers.EnergyType energyType;

		// Token: 0x040007DE RID: 2014
		public bool noFail;

		// Token: 0x040007DF RID: 2015
		public bool instaFail;

		// Token: 0x040007E0 RID: 2016
		public bool failOnSaberClash;

		// Token: 0x040007E1 RID: 2017
		public PlayerSaveDataV1_0_1.GameplayModifiers.EnabledObstacleType enabledObstacleType;

		// Token: 0x040007E2 RID: 2018
		public bool fastNotes;

		// Token: 0x040007E3 RID: 2019
		public bool strictAngles;

		// Token: 0x040007E4 RID: 2020
		public bool disappearingArrows;

		// Token: 0x040007E5 RID: 2021
		public bool noBombs;

		// Token: 0x040007E6 RID: 2022
		public PlayerSaveDataV1_0_1.GameplayModifiers.SongSpeed songSpeed;

		// Token: 0x020001E8 RID: 488
		public enum EnabledObstacleType
		{
			// Token: 0x040007E8 RID: 2024
			All,
			// Token: 0x040007E9 RID: 2025
			FullHeightOnly,
			// Token: 0x040007EA RID: 2026
			None
		}

		// Token: 0x020001E9 RID: 489
		public enum EnergyType
		{
			// Token: 0x040007EC RID: 2028
			Bar,
			// Token: 0x040007ED RID: 2029
			Battery
		}

		// Token: 0x020001EA RID: 490
		public enum SongSpeed
		{
			// Token: 0x040007EF RID: 2031
			Normal,
			// Token: 0x040007F0 RID: 2032
			Faster,
			// Token: 0x040007F1 RID: 2033
			Slower
		}
	}

	// Token: 0x020001EB RID: 491
	[Serializable]
	public class PlayerSpecificSettings
	{
		// Token: 0x040007F2 RID: 2034
		public bool staticLights;

		// Token: 0x040007F3 RID: 2035
		public bool leftHanded;

		// Token: 0x040007F4 RID: 2036
		public bool swapColors;

		// Token: 0x040007F5 RID: 2037
		public float playerHeight;

		// Token: 0x040007F6 RID: 2038
		public bool disableSFX;

		// Token: 0x040007F7 RID: 2039
		public bool reduceDebris;

		// Token: 0x040007F8 RID: 2040
		public bool advancedHud;

		// Token: 0x040007F9 RID: 2041
		public bool noTextsAndHuds;
	}

	// Token: 0x020001EC RID: 492
	[Serializable]
	public class PlayerAllOverallStatsData
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00006480 File Offset: 0x00004680
		public PlayerAllOverallStatsData()
		{
			this.campaignOverallStatsData = new PlayerSaveDataV1_0_1.PlayerOverallStatsData();
			this.soloFreePlayOverallStatsData = new PlayerSaveDataV1_0_1.PlayerOverallStatsData();
			this.partyFreePlayOverallStatsData = new PlayerSaveDataV1_0_1.PlayerOverallStatsData();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000064A9 File Offset: 0x000046A9
		public PlayerAllOverallStatsData(PlayerSaveDataV1_0_1.PlayerOverallStatsData campaignOverallStatsData, PlayerSaveDataV1_0_1.PlayerOverallStatsData soloFreePlayOverallStatsData, PlayerSaveDataV1_0_1.PlayerOverallStatsData partyFreePlayOverallStatsData)
		{
			this.campaignOverallStatsData = campaignOverallStatsData;
			this.soloFreePlayOverallStatsData = soloFreePlayOverallStatsData;
			this.partyFreePlayOverallStatsData = partyFreePlayOverallStatsData;
		}

		// Token: 0x040007FA RID: 2042
		public PlayerSaveDataV1_0_1.PlayerOverallStatsData campaignOverallStatsData;

		// Token: 0x040007FB RID: 2043
		public PlayerSaveDataV1_0_1.PlayerOverallStatsData soloFreePlayOverallStatsData;

		// Token: 0x040007FC RID: 2044
		public PlayerSaveDataV1_0_1.PlayerOverallStatsData partyFreePlayOverallStatsData;
	}

	// Token: 0x020001ED RID: 493
	[Serializable]
	public class PlayerOverallStatsData
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00002198 File Offset: 0x00000398
		public PlayerOverallStatsData()
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00028580 File Offset: 0x00026780
		public PlayerOverallStatsData(int goodCutsCount, int badCutsCount, int missedCutsCount, long totalScore, int playedLevelsCount, int cleardLevelsCount, int failedLevelsCount, int fullComboCount, float timePlayed, int handDistanceTravelled, long cummulativeCutScoreWithoutMultiplier)
		{
			if (totalScore < 0L)
			{
				totalScore = 2147483647L + totalScore - -2147483648L;
			}
			if (cummulativeCutScoreWithoutMultiplier < 0L)
			{
				cummulativeCutScoreWithoutMultiplier = 2147483647L + cummulativeCutScoreWithoutMultiplier - -2147483648L;
			}
			this.goodCutsCount = goodCutsCount;
			this.badCutsCount = badCutsCount;
			this.missedCutsCount = missedCutsCount;
			this.totalScore = totalScore;
			this.playedLevelsCount = playedLevelsCount;
			this.cleardLevelsCount = cleardLevelsCount;
			this.failedLevelsCount = failedLevelsCount;
			this.fullComboCount = fullComboCount;
			this.timePlayed = timePlayed;
			this.handDistanceTravelled = handDistanceTravelled;
			this.cummulativeCutScoreWithoutMultiplier = cummulativeCutScoreWithoutMultiplier;
		}

		// Token: 0x040007FD RID: 2045
		public int goodCutsCount;

		// Token: 0x040007FE RID: 2046
		public int badCutsCount;

		// Token: 0x040007FF RID: 2047
		public int missedCutsCount;

		// Token: 0x04000800 RID: 2048
		public long totalScore;

		// Token: 0x04000801 RID: 2049
		public int playedLevelsCount;

		// Token: 0x04000802 RID: 2050
		public int cleardLevelsCount;

		// Token: 0x04000803 RID: 2051
		public int failedLevelsCount;

		// Token: 0x04000804 RID: 2052
		public int fullComboCount;

		// Token: 0x04000805 RID: 2053
		public float timePlayed;

		// Token: 0x04000806 RID: 2054
		public int handDistanceTravelled;

		// Token: 0x04000807 RID: 2055
		public long cummulativeCutScoreWithoutMultiplier;
	}

	// Token: 0x020001EE RID: 494
	[Serializable]
	public class PlayerLevelStatsData
	{
		// Token: 0x04000808 RID: 2056
		public string levelId;

		// Token: 0x04000809 RID: 2057
		public BeatmapDifficulty difficulty;

		// Token: 0x0400080A RID: 2058
		public int highScore;

		// Token: 0x0400080B RID: 2059
		public int maxCombo;

		// Token: 0x0400080C RID: 2060
		public bool fullCombo;

		// Token: 0x0400080D RID: 2061
		public RankModel.Rank maxRank;

		// Token: 0x0400080E RID: 2062
		public bool validScore;

		// Token: 0x0400080F RID: 2063
		public int playCount;
	}

	// Token: 0x020001EF RID: 495
	[Serializable]
	public class PlayerMissionStatsData
	{
		// Token: 0x04000810 RID: 2064
		public string missionId;

		// Token: 0x04000811 RID: 2065
		public bool cleared;
	}

	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class AchievementsData
	{
		// Token: 0x04000812 RID: 2066
		public string[] unlockedAchievements;

		// Token: 0x04000813 RID: 2067
		public string[] unlockedAchievementsToUpload;
	}

	// Token: 0x020001F1 RID: 497
	[Serializable]
	public class LocalPlayer
	{
		// Token: 0x04000814 RID: 2068
		public string playerId;

		// Token: 0x04000815 RID: 2069
		public string playerName;

		// Token: 0x04000816 RID: 2070
		public bool shouldShowTutorialPrompt = true;

		// Token: 0x04000817 RID: 2071
		public bool shouldShow360Warning = true;

		// Token: 0x04000818 RID: 2072
		public PlayerSaveDataV1_0_1.GameplayModifiers gameplayModifiers;

		// Token: 0x04000819 RID: 2073
		public PlayerSaveDataV1_0_1.PlayerSpecificSettings playerSpecificSettings;

		// Token: 0x0400081A RID: 2074
		public PlayerSaveDataV1_0_1.PlayerAllOverallStatsData playerAllOverallStatsData;

		// Token: 0x0400081B RID: 2075
		public List<PlayerSaveDataV1_0_1.PlayerLevelStatsData> levelsStatsData;

		// Token: 0x0400081C RID: 2076
		public List<PlayerSaveDataV1_0_1.PlayerMissionStatsData> missionsStatsData;

		// Token: 0x0400081D RID: 2077
		public List<string> showedMissionHelpIds;

		// Token: 0x0400081E RID: 2078
		public PlayerSaveDataV1_0_1.AchievementsData achievementsData;
	}

	// Token: 0x020001F2 RID: 498
	[Serializable]
	public class GuestPlayer
	{
		// Token: 0x0400081F RID: 2079
		public string playerName;

		// Token: 0x04000820 RID: 2080
		public PlayerSaveDataV1_0_1.PlayerSpecificSettings playerSpecificSettings;
	}
}
