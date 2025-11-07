using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FC RID: 508
[Serializable]
public class PlayerSaveData : VersionSaveData
{
	// Token: 0x06000814 RID: 2068 RVA: 0x00006A76 File Offset: 0x00004C76
	public PlayerSaveData()
	{
		this.version = "2.0.6";
	}

	// Token: 0x0400085E RID: 2142
	public const string kCurrentVersion = "2.0.6";

	// Token: 0x0400085F RID: 2143
	public List<PlayerSaveData.LocalPlayer> localPlayers;

	// Token: 0x04000860 RID: 2144
	public List<PlayerSaveData.GuestPlayer> guestPlayers;

	// Token: 0x020001FD RID: 509
	[Serializable]
	public class GameplayModifiers
	{
		// Token: 0x04000861 RID: 2145
		public PlayerSaveData.GameplayModifiers.EnergyType energyType;

		// Token: 0x04000862 RID: 2146
		public bool noFail;

		// Token: 0x04000863 RID: 2147
		public bool instaFail;

		// Token: 0x04000864 RID: 2148
		public bool failOnSaberClash;

		// Token: 0x04000865 RID: 2149
		public PlayerSaveData.GameplayModifiers.EnabledObstacleType enabledObstacleType;

		// Token: 0x04000866 RID: 2150
		public bool fastNotes;

		// Token: 0x04000867 RID: 2151
		public bool strictAngles;

		// Token: 0x04000868 RID: 2152
		public bool disappearingArrows;

		// Token: 0x04000869 RID: 2153
		public bool ghostNotes;

		// Token: 0x0400086A RID: 2154
		public bool noBombs;

		// Token: 0x0400086B RID: 2155
		public PlayerSaveData.GameplayModifiers.SongSpeed songSpeed;

		// Token: 0x020001FE RID: 510
		public enum EnabledObstacleType
		{
			// Token: 0x0400086D RID: 2157
			All,
			// Token: 0x0400086E RID: 2158
			FullHeightOnly,
			// Token: 0x0400086F RID: 2159
			None
		}

		// Token: 0x020001FF RID: 511
		public enum EnergyType
		{
			// Token: 0x04000871 RID: 2161
			Bar,
			// Token: 0x04000872 RID: 2162
			Battery
		}

		// Token: 0x02000200 RID: 512
		public enum SongSpeed
		{
			// Token: 0x04000874 RID: 2164
			Normal,
			// Token: 0x04000875 RID: 2165
			Faster,
			// Token: 0x04000876 RID: 2166
			Slower
		}
	}

	// Token: 0x02000201 RID: 513
	[Serializable]
	public class PlayerSpecificSettings
	{
		// Token: 0x04000877 RID: 2167
		public bool staticLights;

		// Token: 0x04000878 RID: 2168
		public bool leftHanded;

		// Token: 0x04000879 RID: 2169
		public float playerHeight = 1.7f;

		// Token: 0x0400087A RID: 2170
		public bool automaticPlayerHeight;

		// Token: 0x0400087B RID: 2171
		public float sfxVolume = 0.7f;

		// Token: 0x0400087C RID: 2172
		public bool reduceDebris;

		// Token: 0x0400087D RID: 2173
		public bool noTextsAndHuds;

		// Token: 0x0400087E RID: 2174
		public bool advancedHud;

		// Token: 0x0400087F RID: 2175
		public float saberTrailIntensity = 0.5f;
	}

	// Token: 0x02000202 RID: 514
	[Serializable]
	public class PlayerAllOverallStatsData
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00006AB2 File Offset: 0x00004CB2
		public PlayerAllOverallStatsData()
		{
			this.campaignOverallStatsData = new PlayerSaveData.PlayerOverallStatsData();
			this.soloFreePlayOverallStatsData = new PlayerSaveData.PlayerOverallStatsData();
			this.partyFreePlayOverallStatsData = new PlayerSaveData.PlayerOverallStatsData();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00006ADB File Offset: 0x00004CDB
		public PlayerAllOverallStatsData(PlayerSaveData.PlayerOverallStatsData campaignOverallStatsData, PlayerSaveData.PlayerOverallStatsData soloFreePlayOverallStatsData, PlayerSaveData.PlayerOverallStatsData partyFreePlayOverallStatsData)
		{
			this.campaignOverallStatsData = campaignOverallStatsData;
			this.soloFreePlayOverallStatsData = soloFreePlayOverallStatsData;
			this.partyFreePlayOverallStatsData = partyFreePlayOverallStatsData;
		}

		// Token: 0x04000880 RID: 2176
		public PlayerSaveData.PlayerOverallStatsData campaignOverallStatsData;

		// Token: 0x04000881 RID: 2177
		public PlayerSaveData.PlayerOverallStatsData soloFreePlayOverallStatsData;

		// Token: 0x04000882 RID: 2178
		public PlayerSaveData.PlayerOverallStatsData partyFreePlayOverallStatsData;
	}

	// Token: 0x02000203 RID: 515
	[Serializable]
	public class PlayerOverallStatsData
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x00002198 File Offset: 0x00000398
		public PlayerOverallStatsData()
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00029F0C File Offset: 0x0002810C
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

		// Token: 0x04000883 RID: 2179
		public int goodCutsCount;

		// Token: 0x04000884 RID: 2180
		public int badCutsCount;

		// Token: 0x04000885 RID: 2181
		public int missedCutsCount;

		// Token: 0x04000886 RID: 2182
		public long totalScore;

		// Token: 0x04000887 RID: 2183
		public int playedLevelsCount;

		// Token: 0x04000888 RID: 2184
		public int cleardLevelsCount;

		// Token: 0x04000889 RID: 2185
		public int failedLevelsCount;

		// Token: 0x0400088A RID: 2186
		public int fullComboCount;

		// Token: 0x0400088B RID: 2187
		public float timePlayed;

		// Token: 0x0400088C RID: 2188
		public int handDistanceTravelled;

		// Token: 0x0400088D RID: 2189
		public long cummulativeCutScoreWithoutMultiplier;
	}

	// Token: 0x02000204 RID: 516
	[Serializable]
	public class PlayerLevelStatsData
	{
		// Token: 0x0400088E RID: 2190
		public string levelId;

		// Token: 0x0400088F RID: 2191
		public BeatmapDifficulty difficulty;

		// Token: 0x04000890 RID: 2192
		public string beatmapCharacteristicName;

		// Token: 0x04000891 RID: 2193
		public int highScore;

		// Token: 0x04000892 RID: 2194
		public int maxCombo;

		// Token: 0x04000893 RID: 2195
		public bool fullCombo;

		// Token: 0x04000894 RID: 2196
		public RankModel.Rank maxRank;

		// Token: 0x04000895 RID: 2197
		public bool validScore;

		// Token: 0x04000896 RID: 2198
		public int playCount;
	}

	// Token: 0x02000205 RID: 517
	[Serializable]
	public class PlayerMissionStatsData
	{
		// Token: 0x04000897 RID: 2199
		public string missionId;

		// Token: 0x04000898 RID: 2200
		public bool cleared;
	}

	// Token: 0x02000206 RID: 518
	[Serializable]
	public class PracticeSettings
	{
		// Token: 0x04000899 RID: 2201
		public float startSongTime;

		// Token: 0x0400089A RID: 2202
		public float songSpeedMul;
	}

	// Token: 0x02000207 RID: 519
	[Serializable]
	public class ColorScheme
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public ColorScheme(string colorSchemeId, Color saberAColor, Color saberBColor, Color environmentColor0, Color environmentColor1, Color obstaclesColor)
		{
			this.colorSchemeId = colorSchemeId;
			this.saberAColor = saberAColor;
			this.saberBColor = saberBColor;
			this.environmentColor0 = environmentColor0;
			this.environmentColor1 = environmentColor1;
			this.obstaclesColor = obstaclesColor;
		}

		// Token: 0x0400089B RID: 2203
		public string colorSchemeId;

		// Token: 0x0400089C RID: 2204
		public Color saberAColor;

		// Token: 0x0400089D RID: 2205
		public Color saberBColor;

		// Token: 0x0400089E RID: 2206
		public Color environmentColor0;

		// Token: 0x0400089F RID: 2207
		public Color environmentColor1;

		// Token: 0x040008A0 RID: 2208
		public Color obstaclesColor;
	}

	// Token: 0x02000208 RID: 520
	[Serializable]
	public class ColorSchemesSettings
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00006B2D File Offset: 0x00004D2D
		public ColorSchemesSettings(bool overrideDefaultColors, string selectedColorSchemeId, List<PlayerSaveData.ColorScheme> colorSchemes)
		{
			this.overrideDefaultColors = overrideDefaultColors;
			this.selectedColorSchemeId = selectedColorSchemeId;
			this.colorSchemes = colorSchemes;
		}

		// Token: 0x040008A1 RID: 2209
		public bool overrideDefaultColors;

		// Token: 0x040008A2 RID: 2210
		public string selectedColorSchemeId;

		// Token: 0x040008A3 RID: 2211
		public List<PlayerSaveData.ColorScheme> colorSchemes;
	}

	// Token: 0x02000209 RID: 521
	[Serializable]
	public class OverrideEnvironmentSettings
	{
		// Token: 0x040008A4 RID: 2212
		public bool overrideEnvironments;

		// Token: 0x040008A5 RID: 2213
		public string overrideNormalEnvironmentName;

		// Token: 0x040008A6 RID: 2214
		public string override360EnvironmentName;
	}

	// Token: 0x0200020A RID: 522
	[Serializable]
	public class GuestPlayer
	{
		// Token: 0x040008A7 RID: 2215
		public string playerName;
	}

	// Token: 0x0200020B RID: 523
	[Serializable]
	public class LocalPlayer
	{
		// Token: 0x040008A8 RID: 2216
		public string playerId;

		// Token: 0x040008A9 RID: 2217
		public string playerName;

		// Token: 0x040008AA RID: 2218
		public bool shouldShowTutorialPrompt = true;

		// Token: 0x040008AB RID: 2219
		public bool shouldShow360Warning = true;

		// Token: 0x040008AC RID: 2220
		public bool agreedToEula;

		// Token: 0x040008AD RID: 2221
		public BeatmapDifficulty lastSelectedBeatmapDifficulty = BeatmapDifficulty.Normal;

		// Token: 0x040008AE RID: 2222
		public string lastSelectedBeatmapCharacteristicName;

		// Token: 0x040008AF RID: 2223
		public PlayerSaveData.GameplayModifiers gameplayModifiers;

		// Token: 0x040008B0 RID: 2224
		public PlayerSaveData.PlayerSpecificSettings playerSpecificSettings;

		// Token: 0x040008B1 RID: 2225
		public PlayerSaveData.PracticeSettings practiceSettings;

		// Token: 0x040008B2 RID: 2226
		public PlayerSaveData.PlayerAllOverallStatsData playerAllOverallStatsData;

		// Token: 0x040008B3 RID: 2227
		public List<PlayerSaveData.PlayerLevelStatsData> levelsStatsData;

		// Token: 0x040008B4 RID: 2228
		public List<PlayerSaveData.PlayerMissionStatsData> missionsStatsData;

		// Token: 0x040008B5 RID: 2229
		public List<string> showedMissionHelpIds;

		// Token: 0x040008B6 RID: 2230
		public PlayerSaveData.ColorSchemesSettings colorSchemesSettings;

		// Token: 0x040008B7 RID: 2231
		public PlayerSaveData.OverrideEnvironmentSettings overrideEnvironmentSettings;

		// Token: 0x040008B8 RID: 2232
		public List<string> favoritesLevelIds;
	}
}
