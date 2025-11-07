using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class PlayerDataFileManagerSO : PersistentScriptableObject
{
	// Token: 0x060007EB RID: 2027 RVA: 0x00028CEC File Offset: 0x00026EEC
	public void Save(PlayerData playerData)
	{
		PlayerSaveData playerSaveData = new PlayerSaveData();
		playerSaveData.localPlayers = new List<PlayerSaveData.LocalPlayer>(1);
		PlayerSaveData.LocalPlayer localPlayer = new PlayerSaveData.LocalPlayer();
		playerSaveData.localPlayers.Add(localPlayer);
		localPlayer.playerName = playerData.playerName;
		localPlayer.playerId = playerData.playerId;
		localPlayer.shouldShowTutorialPrompt = playerData.shouldShowTutorialPrompt;
		localPlayer.shouldShow360Warning = playerData.shouldShow360Warning;
		localPlayer.agreedToEula = playerData.agreedToEula;
		localPlayer.lastSelectedBeatmapDifficulty = playerData.lastSelectedBeatmapDifficulty;
		localPlayer.lastSelectedBeatmapCharacteristicName = playerData.lastSelectedBeatmapCharacteristic.serializedName;
		localPlayer.gameplayModifiers = new PlayerSaveData.GameplayModifiers();
		localPlayer.gameplayModifiers.energyType = (PlayerSaveData.GameplayModifiers.EnergyType)playerData.gameplayModifiers.energyType;
		localPlayer.gameplayModifiers.noFail = playerData.gameplayModifiers.noFail;
		localPlayer.gameplayModifiers.instaFail = playerData.gameplayModifiers.instaFail;
		localPlayer.gameplayModifiers.failOnSaberClash = playerData.gameplayModifiers.failOnSaberClash;
		localPlayer.gameplayModifiers.enabledObstacleType = (PlayerSaveData.GameplayModifiers.EnabledObstacleType)playerData.gameplayModifiers.enabledObstacleType;
		localPlayer.gameplayModifiers.fastNotes = playerData.gameplayModifiers.fastNotes;
		localPlayer.gameplayModifiers.strictAngles = playerData.gameplayModifiers.strictAngles;
		localPlayer.gameplayModifiers.disappearingArrows = playerData.gameplayModifiers.disappearingArrows;
		localPlayer.gameplayModifiers.ghostNotes = playerData.gameplayModifiers.ghostNotes;
		localPlayer.gameplayModifiers.noBombs = playerData.gameplayModifiers.noBombs;
		localPlayer.gameplayModifiers.songSpeed = (PlayerSaveData.GameplayModifiers.SongSpeed)playerData.gameplayModifiers.songSpeed;
		localPlayer.playerSpecificSettings = new PlayerSaveData.PlayerSpecificSettings();
		localPlayer.playerSpecificSettings.leftHanded = playerData.playerSpecificSettings.leftHanded;
		localPlayer.playerSpecificSettings.playerHeight = playerData.playerSpecificSettings.playerHeight;
		localPlayer.playerSpecificSettings.automaticPlayerHeight = playerData.playerSpecificSettings.automaticPlayerHeight;
		localPlayer.playerSpecificSettings.staticLights = playerData.playerSpecificSettings.staticLights;
		localPlayer.playerSpecificSettings.sfxVolume = playerData.playerSpecificSettings.sfxVolume;
		localPlayer.playerSpecificSettings.reduceDebris = playerData.playerSpecificSettings.reduceDebris;
		localPlayer.playerSpecificSettings.advancedHud = playerData.playerSpecificSettings.advancedHud;
		localPlayer.playerSpecificSettings.noTextsAndHuds = playerData.playerSpecificSettings.noTextsAndHuds;
		localPlayer.playerSpecificSettings.saberTrailIntensity = playerData.playerSpecificSettings.saberTrailIntensity;
		localPlayer.practiceSettings = new PlayerSaveData.PracticeSettings();
		localPlayer.practiceSettings.songSpeedMul = playerData.practiceSettings.songSpeedMul;
		localPlayer.practiceSettings.startSongTime = playerData.practiceSettings.startSongTime;
		localPlayer.playerAllOverallStatsData = playerData.playerAllOverallStatsData.ToPlayerAllOverallStatsData();
		localPlayer.levelsStatsData = new List<PlayerSaveData.PlayerLevelStatsData>(playerData.levelsStatsData.Count);
		foreach (PlayerLevelStatsData playerLevelStatsData in playerData.levelsStatsData)
		{
			PlayerSaveData.PlayerLevelStatsData playerLevelStatsData2 = new PlayerSaveData.PlayerLevelStatsData();
			playerLevelStatsData2.levelId = playerLevelStatsData.levelID;
			playerLevelStatsData2.difficulty = playerLevelStatsData.difficulty;
			playerLevelStatsData2.beatmapCharacteristicName = playerLevelStatsData.beatmapCharacteristic.serializedName;
			playerLevelStatsData2.highScore = playerLevelStatsData.highScore;
			playerLevelStatsData2.maxCombo = playerLevelStatsData.maxCombo;
			playerLevelStatsData2.fullCombo = playerLevelStatsData.fullCombo;
			playerLevelStatsData2.maxRank = playerLevelStatsData.maxRank;
			playerLevelStatsData2.playCount = playerLevelStatsData.playCount;
			playerLevelStatsData2.validScore = playerLevelStatsData.validScore;
			localPlayer.levelsStatsData.Add(playerLevelStatsData2);
		}
		localPlayer.missionsStatsData = new List<PlayerSaveData.PlayerMissionStatsData>(playerData.missionsStatsData.Count);
		foreach (PlayerMissionStatsData playerMissionStatsData in playerData.missionsStatsData)
		{
			PlayerSaveData.PlayerMissionStatsData playerMissionStatsData2 = new PlayerSaveData.PlayerMissionStatsData();
			playerMissionStatsData2.missionId = playerMissionStatsData.missionId;
			playerMissionStatsData2.cleared = playerMissionStatsData.cleared;
			localPlayer.missionsStatsData.Add(playerMissionStatsData2);
		}
		localPlayer.showedMissionHelpIds = playerData.showedMissionHelpIds;
		playerSaveData.guestPlayers = new List<PlayerSaveData.GuestPlayer>(playerData.guestPlayerNames.Count);
		foreach (string playerName in playerData.guestPlayerNames)
		{
			PlayerSaveData.GuestPlayer guestPlayer = new PlayerSaveData.GuestPlayer();
			guestPlayer.playerName = playerName;
			playerSaveData.guestPlayers.Add(guestPlayer);
		}
		List<PlayerSaveData.ColorScheme> list = new List<PlayerSaveData.ColorScheme>(playerData.colorSchemesSettings.GetNumberOfColorSchemes());
		for (int i = 0; i < playerData.colorSchemesSettings.GetNumberOfColorSchemes(); i++)
		{
			ColorScheme colorSchemeForIdx = playerData.colorSchemesSettings.GetColorSchemeForIdx(i);
			if (colorSchemeForIdx.isEditable)
			{
				PlayerSaveData.ColorScheme item = new PlayerSaveData.ColorScheme(colorSchemeForIdx.colorSchemeId, colorSchemeForIdx.saberAColor, colorSchemeForIdx.saberBColor, colorSchemeForIdx.environmentColor0, colorSchemeForIdx.environmentColor1, colorSchemeForIdx.obstaclesColor);
				list.Add(item);
			}
		}
		localPlayer.colorSchemesSettings = new PlayerSaveData.ColorSchemesSettings(playerData.colorSchemesSettings.overrideDefaultColors, playerData.colorSchemesSettings.selectedColorSchemeId, list);
		localPlayer.overrideEnvironmentSettings = new PlayerSaveData.OverrideEnvironmentSettings();
		localPlayer.overrideEnvironmentSettings.overrideEnvironments = playerData.overrideEnvironmentSettings.overrideEnvironments;
		EnvironmentInfoSO overrideEnvironmentInfoForType = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(this._a360DegreesEnvironmentType);
		localPlayer.overrideEnvironmentSettings.override360EnvironmentName = ((overrideEnvironmentInfoForType != null) ? overrideEnvironmentInfoForType.serializedName : "");
		EnvironmentInfoSO overrideEnvironmentInfoForType2 = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(this._normalEnvironmentType);
		localPlayer.overrideEnvironmentSettings.overrideNormalEnvironmentName = ((overrideEnvironmentInfoForType2 != null) ? overrideEnvironmentInfoForType2.serializedName : "");
		if (playerData.favoritesLevelIds != null)
		{
			localPlayer.favoritesLevelIds = new List<string>(playerData.favoritesLevelIds);
		}
		else
		{
			localPlayer.favoritesLevelIds = new List<string>();
		}
		string filePath = Application.persistentDataPath + "/PlayerData.dat";
		string tempFilePath = Application.persistentDataPath + "/PlayerData.dat.tmp";
		string backupFilePath = Application.persistentDataPath + "/PlayerData.dat.bak";
		FileHelpers.SaveToJSONFile(playerSaveData, filePath, tempFilePath, backupFilePath);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0002930C File Offset: 0x0002750C
	public PlayerData Load()
	{
		string filePath = Application.persistentDataPath + "/PlayerData.dat";
		string filePath2 = Application.persistentDataPath + "/PlayerData.dat.bak";
		string jsonString = FileHelpers.LoadJSONFile(filePath, null);
		PlayerData playerData = this.LoadFromJSONString(jsonString);
		if (playerData == null)
		{
			jsonString = FileHelpers.LoadJSONFile(filePath2, null);
			playerData = this.LoadFromJSONString(jsonString);
		}
		if (playerData == null)
		{
			playerData = this.CreateDefaultPlayerData();
		}
		return playerData;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00029368 File Offset: 0x00027568
	private PlayerData LoadFromJSONString(string jsonString)
	{
		try
		{
			VersionSaveData versionSaveData = JsonUtility.FromJson<VersionSaveData>(jsonString);
			if (versionSaveData != null && (versionSaveData.version == "" || versionSaveData.version == "1.0.1"))
			{
				PlayerSaveDataV1_0_1 playerDataModelSaveData = JsonUtility.FromJson<PlayerSaveDataV1_0_1>(jsonString);
				return this.LoadFromVersionV1_0_1(playerDataModelSaveData);
			}
			PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(jsonString);
			return this.LoadFromCurrentVersion(playerSaveData);
		}
		catch
		{
		}
		return null;
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000293DC File Offset: 0x000275DC
	private PlayerData LoadFromCurrentVersion(PlayerSaveData playerSaveData)
	{
		if (playerSaveData == null || playerSaveData.localPlayers == null || playerSaveData.localPlayers.Count == 0)
		{
			return null;
		}
		PlayerSaveData.LocalPlayer localPlayer = playerSaveData.localPlayers[0];
		GameplayModifiers gameplayModifiers = new GameplayModifiers();
		gameplayModifiers.energyType = (GameplayModifiers.EnergyType)localPlayer.gameplayModifiers.energyType;
		gameplayModifiers.noFail = localPlayer.gameplayModifiers.noFail;
		gameplayModifiers.instaFail = localPlayer.gameplayModifiers.instaFail;
		gameplayModifiers.failOnSaberClash = localPlayer.gameplayModifiers.failOnSaberClash;
		gameplayModifiers.enabledObstacleType = (GameplayModifiers.EnabledObstacleType)localPlayer.gameplayModifiers.enabledObstacleType;
		gameplayModifiers.fastNotes = localPlayer.gameplayModifiers.fastNotes;
		gameplayModifiers.strictAngles = localPlayer.gameplayModifiers.strictAngles;
		gameplayModifiers.disappearingArrows = localPlayer.gameplayModifiers.disappearingArrows;
		gameplayModifiers.ghostNotes = localPlayer.gameplayModifiers.ghostNotes;
		gameplayModifiers.noBombs = localPlayer.gameplayModifiers.noBombs;
		gameplayModifiers.songSpeed = (GameplayModifiers.SongSpeed)localPlayer.gameplayModifiers.songSpeed;
		PlayerSpecificSettings playerSpecificSettings = new PlayerSpecificSettings();
		playerSpecificSettings.leftHanded = localPlayer.playerSpecificSettings.leftHanded;
		playerSpecificSettings.playerHeight = localPlayer.playerSpecificSettings.playerHeight;
		playerSpecificSettings.automaticPlayerHeight = localPlayer.playerSpecificSettings.automaticPlayerHeight;
		playerSpecificSettings.staticLights = localPlayer.playerSpecificSettings.staticLights;
		playerSpecificSettings.sfxVolume = localPlayer.playerSpecificSettings.sfxVolume;
		playerSpecificSettings.reduceDebris = localPlayer.playerSpecificSettings.reduceDebris;
		playerSpecificSettings.advancedHud = localPlayer.playerSpecificSettings.advancedHud;
		playerSpecificSettings.noTextsAndHuds = localPlayer.playerSpecificSettings.noTextsAndHuds;
		playerSpecificSettings.saberTrailIntensity = localPlayer.playerSpecificSettings.saberTrailIntensity;
		PlayerAllOverallStatsData playerAllOverallStatsData = localPlayer.playerAllOverallStatsData.ToPlayerAllOverallStatsData();
		List<PlayerLevelStatsData> list = new List<PlayerLevelStatsData>(localPlayer.levelsStatsData.Count);
		foreach (PlayerSaveData.PlayerLevelStatsData playerLevelStatsData in localPlayer.levelsStatsData)
		{
			BeatmapCharacteristicSO beatmapCharacteristicBySerializedName = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(playerLevelStatsData.beatmapCharacteristicName);
			if (beatmapCharacteristicBySerializedName != null)
			{
				PlayerLevelStatsData item = new PlayerLevelStatsData(playerLevelStatsData.levelId, playerLevelStatsData.difficulty, beatmapCharacteristicBySerializedName, playerLevelStatsData.highScore, playerLevelStatsData.maxCombo, playerLevelStatsData.fullCombo, playerLevelStatsData.maxRank, playerLevelStatsData.validScore, playerLevelStatsData.playCount);
				list.Add(item);
			}
		}
		List<PlayerMissionStatsData> list2 = new List<PlayerMissionStatsData>(localPlayer.missionsStatsData.Count);
		foreach (PlayerSaveData.PlayerMissionStatsData playerMissionStatsData in localPlayer.missionsStatsData)
		{
			PlayerMissionStatsData item2 = new PlayerMissionStatsData(playerMissionStatsData.missionId, playerMissionStatsData.cleared);
			list2.Add(item2);
		}
		List<string> showedMissionHelpIds = localPlayer.showedMissionHelpIds;
		PracticeSettings practiceSettings = new PracticeSettings(localPlayer.practiceSettings.startSongTime, localPlayer.practiceSettings.songSpeedMul);
		List<string> list3 = new List<string>(playerSaveData.guestPlayers.Count);
		foreach (PlayerSaveData.GuestPlayer guestPlayer in playerSaveData.guestPlayers)
		{
			list3.Add(guestPlayer.playerName);
		}
		ColorSchemesSettings colorSchemesSettings = new ColorSchemesSettings(this._defaultColorSchemes.colorSchemes);
		if (localPlayer.colorSchemesSettings != null && localPlayer.colorSchemesSettings.colorSchemes != null)
		{
			for (int i = 0; i < localPlayer.colorSchemesSettings.colorSchemes.Count; i++)
			{
				PlayerSaveData.ColorScheme colorScheme = localPlayer.colorSchemesSettings.colorSchemes[i];
				ColorScheme colorScheme2 = null;
				if (colorScheme.colorSchemeId != null)
				{
					colorScheme2 = colorSchemesSettings.GetColorSchemeForId(colorScheme.colorSchemeId);
				}
				if (colorScheme2 != null && colorScheme2.isEditable)
				{
					colorSchemesSettings.SetColorSchemeForId(new ColorScheme(colorScheme2.colorSchemeId, colorScheme2.colorSchemeName, colorScheme2.isEditable, colorScheme.saberAColor, colorScheme.saberBColor, colorScheme.environmentColor0, colorScheme.environmentColor1, colorScheme.obstaclesColor));
				}
			}
			colorSchemesSettings.overrideDefaultColors = localPlayer.colorSchemesSettings.overrideDefaultColors;
			if (!string.IsNullOrEmpty(localPlayer.colorSchemesSettings.selectedColorSchemeId))
			{
				colorSchemesSettings.selectedColorSchemeId = localPlayer.colorSchemesSettings.selectedColorSchemeId;
			}
		}
		OverrideEnvironmentSettings overrideEnvironmentSettings = new OverrideEnvironmentSettings();
		overrideEnvironmentSettings.overrideEnvironments = localPlayer.overrideEnvironmentSettings.overrideEnvironments;
		EnvironmentInfoSO environmentInfo = this._allEnvironmentInfos.GetEnviromentInfoBySerializedName(localPlayer.overrideEnvironmentSettings.overrideNormalEnvironmentName) ?? this._allEnvironmentInfos.GetFirstEnvironmentInfoWithType(this._normalEnvironmentType);
		overrideEnvironmentSettings.SetEnvironmentInfoForType(this._normalEnvironmentType, environmentInfo);
		EnvironmentInfoSO environmentInfo2 = this._allEnvironmentInfos.GetEnviromentInfoBySerializedName(localPlayer.overrideEnvironmentSettings.override360EnvironmentName) ?? this._allEnvironmentInfos.GetFirstEnvironmentInfoWithType(this._a360DegreesEnvironmentType);
		overrideEnvironmentSettings.SetEnvironmentInfoForType(this._a360DegreesEnvironmentType, environmentInfo2);
		BeatmapCharacteristicSO beatmapCharacteristicSO = this._beatmapCharacteristicCollection.GetBeatmapCharacteristicBySerializedName(localPlayer.lastSelectedBeatmapCharacteristicName);
		if (beatmapCharacteristicSO == null)
		{
			beatmapCharacteristicSO = this._defaultLastSelectedBeatmapCharacteristic;
		}
		return new PlayerData(localPlayer.playerId, localPlayer.playerName, localPlayer.shouldShowTutorialPrompt, localPlayer.shouldShow360Warning, localPlayer.agreedToEula, localPlayer.lastSelectedBeatmapDifficulty, beatmapCharacteristicSO, gameplayModifiers, playerSpecificSettings, practiceSettings, playerAllOverallStatsData, list, list2, showedMissionHelpIds, list3, colorSchemesSettings, overrideEnvironmentSettings, localPlayer.favoritesLevelIds);
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00029928 File Offset: 0x00027B28
	private PlayerData LoadFromVersionV1_0_1(PlayerSaveDataV1_0_1 playerDataModelSaveData)
	{
		if (playerDataModelSaveData.localPlayers == null || playerDataModelSaveData.localPlayers.Count == 0)
		{
			return null;
		}
		PlayerSaveDataV1_0_1.LocalPlayer localPlayer = playerDataModelSaveData.localPlayers[0];
		GameplayModifiers gameplayModifiers = new GameplayModifiers();
		gameplayModifiers.energyType = (GameplayModifiers.EnergyType)localPlayer.gameplayModifiers.energyType;
		gameplayModifiers.noFail = localPlayer.gameplayModifiers.noFail;
		gameplayModifiers.instaFail = localPlayer.gameplayModifiers.instaFail;
		gameplayModifiers.failOnSaberClash = localPlayer.gameplayModifiers.failOnSaberClash;
		gameplayModifiers.enabledObstacleType = (GameplayModifiers.EnabledObstacleType)localPlayer.gameplayModifiers.enabledObstacleType;
		gameplayModifiers.fastNotes = localPlayer.gameplayModifiers.fastNotes;
		gameplayModifiers.strictAngles = localPlayer.gameplayModifiers.strictAngles;
		gameplayModifiers.disappearingArrows = localPlayer.gameplayModifiers.disappearingArrows;
		gameplayModifiers.noBombs = localPlayer.gameplayModifiers.noBombs;
		gameplayModifiers.songSpeed = (GameplayModifiers.SongSpeed)localPlayer.gameplayModifiers.songSpeed;
		PlayerSpecificSettings playerSpecificSettings = new PlayerSpecificSettings();
		playerSpecificSettings.leftHanded = localPlayer.playerSpecificSettings.leftHanded;
		playerSpecificSettings.playerHeight = localPlayer.playerSpecificSettings.playerHeight;
		playerSpecificSettings.staticLights = localPlayer.playerSpecificSettings.staticLights;
		playerSpecificSettings.sfxVolume = 1f;
		playerSpecificSettings.reduceDebris = localPlayer.playerSpecificSettings.reduceDebris;
		PlayerAllOverallStatsData playerAllOverallStatsData = localPlayer.playerAllOverallStatsData.ToPlayerAllOverallStatsData();
		List<PlayerLevelStatsData> list = new List<PlayerLevelStatsData>(localPlayer.levelsStatsData.Count);
		foreach (PlayerSaveDataV1_0_1.PlayerLevelStatsData playerLevelStatsData in localPlayer.levelsStatsData)
		{
			BeatmapCharacteristicSO beatmapCharacteristicFromV_1_0_1LevelId = PlayerDataFileManagerSO.GetBeatmapCharacteristicFromV_1_0_1LevelId(this._beatmapCharacteristicCollection, playerLevelStatsData.levelId);
			if (beatmapCharacteristicFromV_1_0_1LevelId != null)
			{
				string levelIdFromV_1_0_1LevelId = PlayerDataFileManagerSO.GetLevelIdFromV_1_0_1LevelId(playerLevelStatsData.levelId, beatmapCharacteristicFromV_1_0_1LevelId);
				if (levelIdFromV_1_0_1LevelId != null)
				{
					PlayerLevelStatsData item = new PlayerLevelStatsData(levelIdFromV_1_0_1LevelId, playerLevelStatsData.difficulty, beatmapCharacteristicFromV_1_0_1LevelId, playerLevelStatsData.highScore, playerLevelStatsData.maxCombo, playerLevelStatsData.fullCombo, playerLevelStatsData.maxRank, playerLevelStatsData.validScore, playerLevelStatsData.playCount);
					list.Add(item);
				}
			}
		}
		List<PlayerMissionStatsData> list2 = new List<PlayerMissionStatsData>(localPlayer.missionsStatsData.Count);
		foreach (PlayerSaveDataV1_0_1.PlayerMissionStatsData playerMissionStatsData in localPlayer.missionsStatsData)
		{
			PlayerMissionStatsData item2 = new PlayerMissionStatsData(playerMissionStatsData.missionId, playerMissionStatsData.cleared);
			list2.Add(item2);
		}
		List<string> showedMissionHelpIds = localPlayer.showedMissionHelpIds;
		PracticeSettings practiceSettings = new PracticeSettings();
		List<string> guestPlayerNames = new List<string>();
		ColorSchemesSettings colorSchemesSettings = new ColorSchemesSettings(this._defaultColorSchemes.colorSchemes);
		OverrideEnvironmentSettings overrideEnvironmentSettings = this.CreateDefaultOverrideEnvironmentSettings();
		return new PlayerData(localPlayer.playerId, localPlayer.playerName, localPlayer.shouldShowTutorialPrompt, localPlayer.shouldShow360Warning, false, playerDataModelSaveData.lastSelectedBeatmapDifficulty, this._defaultLastSelectedBeatmapCharacteristic, gameplayModifiers, playerSpecificSettings, practiceSettings, playerAllOverallStatsData, list, list2, showedMissionHelpIds, guestPlayerNames, colorSchemesSettings, overrideEnvironmentSettings, new List<string>());
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00029C18 File Offset: 0x00027E18
	public PlayerData CreateDefaultPlayerData()
	{
		ColorSchemesSettings colorSchemesSettings = new ColorSchemesSettings(this._defaultColorSchemes.colorSchemes);
		OverrideEnvironmentSettings overrideEnvironmentSettings = this.CreateDefaultOverrideEnvironmentSettings();
		return new PlayerData("", "<NO NAME!>", this._defaultLastSelectedBeatmapCharacteristic, colorSchemesSettings, overrideEnvironmentSettings);
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00029C54 File Offset: 0x00027E54
	public static string GetLevelIdFromV_1_0_1LevelId(string oldLevelId, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		string compoundIdPartName = beatmapCharacteristic.compoundIdPartName;
		if (oldLevelId.EndsWith(compoundIdPartName))
		{
			return oldLevelId.Substring(0, oldLevelId.Length - compoundIdPartName.Length);
		}
		return null;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00029C88 File Offset: 0x00027E88
	public static BeatmapCharacteristicSO GetBeatmapCharacteristicFromV_1_0_1LevelId(BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection, string levelId)
	{
		BeatmapCharacteristicSO beatmapCharacteristicSO = null;
		foreach (BeatmapCharacteristicSO beatmapCharacteristicSO2 in beatmapCharacteristicCollection.beatmapCharacteristics)
		{
			if (levelId.Contains(beatmapCharacteristicSO2.compoundIdPartName) && (beatmapCharacteristicSO == null || beatmapCharacteristicSO.compoundIdPartName.Length < beatmapCharacteristicSO2.compoundIdPartName.Length))
			{
				beatmapCharacteristicSO = beatmapCharacteristicSO2;
			}
		}
		return beatmapCharacteristicSO;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00029CE4 File Offset: 0x00027EE4
	private OverrideEnvironmentSettings CreateDefaultOverrideEnvironmentSettings()
	{
		OverrideEnvironmentSettings overrideEnvironmentSettings = new OverrideEnvironmentSettings();
		overrideEnvironmentSettings.overrideEnvironments = false;
		overrideEnvironmentSettings.SetEnvironmentInfoForType(this._normalEnvironmentType, this._allEnvironmentInfos.GetFirstEnvironmentInfoWithType(this._normalEnvironmentType));
		overrideEnvironmentSettings.SetEnvironmentInfoForType(this._a360DegreesEnvironmentType, this._allEnvironmentInfos.GetFirstEnvironmentInfoWithType(this._a360DegreesEnvironmentType));
		return overrideEnvironmentSettings;
	}

	// Token: 0x04000848 RID: 2120
	[SerializeField]
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x04000849 RID: 2121
	[SerializeField]
	private ColorSchemesListSO _defaultColorSchemes;

	// Token: 0x0400084A RID: 2122
	[SerializeField]
	private EnvironmentsListSO _allEnvironmentInfos;

	// Token: 0x0400084B RID: 2123
	[SerializeField]
	private EnvironmentTypeSO _normalEnvironmentType;

	// Token: 0x0400084C RID: 2124
	[SerializeField]
	private EnvironmentTypeSO _a360DegreesEnvironmentType;

	// Token: 0x0400084D RID: 2125
	[SerializeField]
	private BeatmapCharacteristicSO _defaultLastSelectedBeatmapCharacteristic;

	// Token: 0x0400084E RID: 2126
	private const string kPlayerDataFileName = "PlayerData.dat";

	// Token: 0x0400084F RID: 2127
	private const string kTempFileName = "PlayerData.dat.tmp";

	// Token: 0x04000850 RID: 2128
	private const string kBackupFileName = "PlayerData.dat.bak";
}
