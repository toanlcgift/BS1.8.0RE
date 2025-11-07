using System;
using System.Collections.Generic;

// Token: 0x020001F6 RID: 502
public class PlayerData
{
	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000066D1 File Offset: 0x000048D1
	// (set) Token: 0x060007B5 RID: 1973 RVA: 0x000066D9 File Offset: 0x000048D9
	public string playerId { get; private set; }

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000066E2 File Offset: 0x000048E2
	// (set) Token: 0x060007B7 RID: 1975 RVA: 0x000066EA File Offset: 0x000048EA
	public string playerName { get; private set; }

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000066F3 File Offset: 0x000048F3
	// (set) Token: 0x060007B9 RID: 1977 RVA: 0x000066FB File Offset: 0x000048FB
	public bool shouldShowTutorialPrompt { get; private set; }

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x00006704 File Offset: 0x00004904
	// (set) Token: 0x060007BB RID: 1979 RVA: 0x0000670C File Offset: 0x0000490C
	public bool shouldShow360Warning { get; private set; }

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060007BC RID: 1980 RVA: 0x00006715 File Offset: 0x00004915
	// (set) Token: 0x060007BD RID: 1981 RVA: 0x0000671D File Offset: 0x0000491D
	public bool agreedToEula { get; private set; }

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060007BE RID: 1982 RVA: 0x00006726 File Offset: 0x00004926
	// (set) Token: 0x060007BF RID: 1983 RVA: 0x0000672E File Offset: 0x0000492E
	public BeatmapDifficulty lastSelectedBeatmapDifficulty { get; private set; }

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00006737 File Offset: 0x00004937
	// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0000673F File Offset: 0x0000493F
	public BeatmapCharacteristicSO lastSelectedBeatmapCharacteristic { get; private set; }

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00006748 File Offset: 0x00004948
	// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00006750 File Offset: 0x00004950
	public GameplayModifiers gameplayModifiers { get; private set; }

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00006759 File Offset: 0x00004959
	// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00006761 File Offset: 0x00004961
	public PlayerSpecificSettings playerSpecificSettings { get; private set; }

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000676A File Offset: 0x0000496A
	// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00006772 File Offset: 0x00004972
	public PracticeSettings practiceSettings { get; private set; }

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000677B File Offset: 0x0000497B
	// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00006783 File Offset: 0x00004983
	public PlayerAllOverallStatsData playerAllOverallStatsData { get; private set; }

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060007CA RID: 1994 RVA: 0x0000678C File Offset: 0x0000498C
	// (set) Token: 0x060007CB RID: 1995 RVA: 0x00006794 File Offset: 0x00004994
	public List<PlayerLevelStatsData> levelsStatsData { get; private set; }

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060007CC RID: 1996 RVA: 0x0000679D File Offset: 0x0000499D
	// (set) Token: 0x060007CD RID: 1997 RVA: 0x000067A5 File Offset: 0x000049A5
	public List<PlayerMissionStatsData> missionsStatsData { get; private set; }

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x060007CE RID: 1998 RVA: 0x000067AE File Offset: 0x000049AE
	// (set) Token: 0x060007CF RID: 1999 RVA: 0x000067B6 File Offset: 0x000049B6
	public List<string> showedMissionHelpIds { get; private set; }

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000067BF File Offset: 0x000049BF
	// (set) Token: 0x060007D1 RID: 2001 RVA: 0x000067C7 File Offset: 0x000049C7
	public List<string> guestPlayerNames { get; private set; }

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000067D0 File Offset: 0x000049D0
	// (set) Token: 0x060007D3 RID: 2003 RVA: 0x000067D8 File Offset: 0x000049D8
	public ColorSchemesSettings colorSchemesSettings { get; private set; }

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000067E1 File Offset: 0x000049E1
	// (set) Token: 0x060007D5 RID: 2005 RVA: 0x000067E9 File Offset: 0x000049E9
	public OverrideEnvironmentSettings overrideEnvironmentSettings { get; private set; }

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000067F2 File Offset: 0x000049F2
	// (set) Token: 0x060007D7 RID: 2007 RVA: 0x000067FA File Offset: 0x000049FA
	public HashSet<string> favoritesLevelIds { get; private set; }

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x060007D8 RID: 2008 RVA: 0x000289A4 File Offset: 0x00026BA4
	// (remove) Token: 0x060007D9 RID: 2009 RVA: 0x000289DC File Offset: 0x00026BDC
	public event Action favoriteLevelsSetDidChangeEvent;

	// Token: 0x060007DA RID: 2010 RVA: 0x00028A14 File Offset: 0x00026C14
	public PlayerData(string playerId, string playerName, BeatmapCharacteristicSO lastSelectedBeatmapCharacteristic, ColorSchemesSettings colorSchemesSettings, OverrideEnvironmentSettings overrideEnvironmentSettings) : this(playerId, playerName, true, true, false, BeatmapDifficulty.Normal, lastSelectedBeatmapCharacteristic, GameplayModifiers.defaultModifiers, PlayerSpecificSettings.defaultSettings, new PracticeSettings(), new PlayerAllOverallStatsData(), new List<PlayerLevelStatsData>(), new List<PlayerMissionStatsData>(), new List<string>(), new List<string>(), colorSchemesSettings, overrideEnvironmentSettings, null)
	{
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00028A5C File Offset: 0x00026C5C
	public PlayerData(string playerId, string playerName, bool shouldShowTutorialPrompt, bool shouldShow360Warning, bool agreedToEula, BeatmapDifficulty lastSelectedBeatmapDifficulty, BeatmapCharacteristicSO lastSelectedBeatmapCharacteristic, GameplayModifiers gameplayModifiers, PlayerSpecificSettings playerSpecificSettings, PracticeSettings practiceSettings, PlayerAllOverallStatsData playerAllOverallStatsData, List<PlayerLevelStatsData> levelsStatsData, List<PlayerMissionStatsData> missionsStatsData, List<string> showedMissionHelpIds, List<string> guestPlayerNames, ColorSchemesSettings colorSchemesSettings, OverrideEnvironmentSettings overrideEnvironmentSettings, List<string> favoritesLevelIds)
	{
		this.playerId = playerId;
		this.playerName = playerName;
		this.shouldShowTutorialPrompt = shouldShowTutorialPrompt;
		this.shouldShow360Warning = shouldShow360Warning;
		this.agreedToEula = agreedToEula;
		this.lastSelectedBeatmapDifficulty = lastSelectedBeatmapDifficulty;
		this.lastSelectedBeatmapCharacteristic = lastSelectedBeatmapCharacteristic;
		this.gameplayModifiers = gameplayModifiers;
		this.playerSpecificSettings = playerSpecificSettings;
		this.practiceSettings = practiceSettings;
		this.playerAllOverallStatsData = playerAllOverallStatsData;
		this.levelsStatsData = levelsStatsData;
		this.missionsStatsData = missionsStatsData;
		this.showedMissionHelpIds = showedMissionHelpIds;
		this.guestPlayerNames = guestPlayerNames;
		this.colorSchemesSettings = colorSchemesSettings;
		this.overrideEnvironmentSettings = overrideEnvironmentSettings;
		if (favoritesLevelIds != null)
		{
			this.favoritesLevelIds = new HashSet<string>(favoritesLevelIds);
			return;
		}
		this.favoritesLevelIds = new HashSet<string>();
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x00028B14 File Offset: 0x00026D14
	public PlayerLevelStatsData GetPlayerLevelStatsData(IDifficultyBeatmap difficultyBeatmap)
	{
		string levelID = difficultyBeatmap.level.levelID;
		BeatmapDifficulty difficulty = difficultyBeatmap.difficulty;
		BeatmapCharacteristicSO beatmapCharacteristic = difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic;
		return this.GetPlayerLevelStatsData(levelID, difficulty, beatmapCharacteristic);
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00028B4C File Offset: 0x00026D4C
	public PlayerLevelStatsData GetPlayerLevelStatsData(string levelId, BeatmapDifficulty difficulty, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		foreach (PlayerLevelStatsData playerLevelStatsData in this.levelsStatsData)
		{
			if (playerLevelStatsData.levelID == levelId && playerLevelStatsData.difficulty == difficulty && playerLevelStatsData.beatmapCharacteristic == beatmapCharacteristic)
			{
				return playerLevelStatsData;
			}
		}
		PlayerLevelStatsData playerLevelStatsData2 = new PlayerLevelStatsData(levelId, difficulty, beatmapCharacteristic);
		this.levelsStatsData.Add(playerLevelStatsData2);
		return playerLevelStatsData2;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00028BDC File Offset: 0x00026DDC
	public PlayerMissionStatsData GetPlayerMissionStatsData(string missionId)
	{
		foreach (PlayerMissionStatsData playerMissionStatsData in this.missionsStatsData)
		{
			if (playerMissionStatsData.missionId == missionId)
			{
				return playerMissionStatsData;
			}
		}
		PlayerMissionStatsData playerMissionStatsData2 = new PlayerMissionStatsData(missionId, false);
		this.missionsStatsData.Add(playerMissionStatsData2);
		return playerMissionStatsData2;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00006803 File Offset: 0x00004A03
	public bool WasMissionHelpShowed(MissionHelpSO missionHelp)
	{
		return this.showedMissionHelpIds.Contains(missionHelp.missionHelpId);
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00006816 File Offset: 0x00004A16
	public void MissionHelpWasShowed(MissionHelpSO missionHelp)
	{
		if (!this.showedMissionHelpIds.Contains(missionHelp.missionHelpId))
		{
			this.showedMissionHelpIds.Add(missionHelp.missionHelpId);
		}
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0000683C File Offset: 0x00004A3C
	public bool IsLevelUserFavorite(IPreviewBeatmapLevel level)
	{
		return this.favoritesLevelIds.Contains(level.levelID);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0000684F File Offset: 0x00004A4F
	public void AddLevelToFavorites(IPreviewBeatmapLevel level)
	{
		if (!this.IsLevelUserFavorite(level))
		{
			this.favoritesLevelIds.Add(level.levelID);
			Action action = this.favoriteLevelsSetDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action();
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0000687C File Offset: 0x00004A7C
	public void RemoveLevelFromFavorites(IPreviewBeatmapLevel level)
	{
		if (this.IsLevelUserFavorite(level))
		{
			this.favoritesLevelIds.Remove(level.levelID);
			Action action = this.favoriteLevelsSetDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action();
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x000068A9 File Offset: 0x00004AA9
	public void MarkTutorialAsShown()
	{
		this.shouldShowTutorialPrompt = false;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x000068B2 File Offset: 0x00004AB2
	public void Mark360WarningAsShown()
	{
		this.shouldShow360Warning = false;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x000068BB File Offset: 0x00004ABB
	public void MarkEulaAsAgreed()
	{
		this.agreedToEula = true;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00028C54 File Offset: 0x00026E54
	public void AddGuestPlayerName(string playerName)
	{
		for (int i = 0; i < this.guestPlayerNames.Count; i++)
		{
			if (this.guestPlayerNames[i] == playerName)
			{
				if (i > 0)
				{
					this.guestPlayerNames.Insert(0, this.guestPlayerNames[i]);
					this.guestPlayerNames.RemoveAt(i + 1);
				}
				return;
			}
		}
		this.guestPlayerNames.Insert(0, playerName);
		if (this.guestPlayerNames.Count > 10)
		{
			this.guestPlayerNames.RemoveAt(this.guestPlayerNames.Count - 1);
		}
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x000068C4 File Offset: 0x00004AC4
	public void DeleteAllGuestPlayers()
	{
		this.guestPlayerNames.Clear();
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x000068D1 File Offset: 0x00004AD1
	public void SetLastSelectedBeatmapDifficulty(BeatmapDifficulty beatmapDifficulty)
	{
		this.lastSelectedBeatmapDifficulty = beatmapDifficulty;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x000068DA File Offset: 0x00004ADA
	public void SetLastSelectedBeatmapCharacteristic(BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this.lastSelectedBeatmapCharacteristic = beatmapCharacteristic;
	}

	// Token: 0x04000834 RID: 2100
	public const int kMaxGuestPlayers = 10;
}
