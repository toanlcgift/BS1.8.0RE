using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x0200005E RID: 94
public class AchievementsEvaluationHandler : MonoBehaviour
{
	// Token: 0x0600019D RID: 413 RVA: 0x0001888C File Offset: 0x00016A8C
	protected void Start()
	{
		this._achievementsModel.Initialize();
		this._playerDataModel.playerData.playerAllOverallStatsData.didUpdatePartyFreePlayOverallStatsDataEvent += this.HandlePartyFreePlayOverallStatsDataDidUpdate;
		this._playerDataModel.playerData.playerAllOverallStatsData.didUpdateSoloFreePlayOverallStatsDataEvent += this.HandleSoloFreePlayOverallStatsDataDidUpdate;
		this._playerDataModel.playerData.playerAllOverallStatsData.didUpdateCampaignOverallStatsDataEvent += this.HandleCampaignOverallStatsDataDidUpdate;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00018908 File Offset: 0x00016B08
	protected void OnDestroy()
	{
		if (this._playerDataModel != null && this._playerDataModel.playerData != null)
		{
			this._playerDataModel.playerData.playerAllOverallStatsData.didUpdatePartyFreePlayOverallStatsDataEvent -= this.HandlePartyFreePlayOverallStatsDataDidUpdate;
			this._playerDataModel.playerData.playerAllOverallStatsData.didUpdateSoloFreePlayOverallStatsDataEvent -= this.HandleSoloFreePlayOverallStatsDataDidUpdate;
			this._playerDataModel.playerData.playerAllOverallStatsData.didUpdateCampaignOverallStatsDataEvent -= this.HandleCampaignOverallStatsDataDidUpdate;
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000034D5 File Offset: 0x000016D5
	private void HandleSoloFreePlayOverallStatsDataDidUpdate(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap)
	{
		this.ProcessLevelFinishData(difficultyBeatmap, levelCompletionResults);
		this.ProcessSoloFreePlayLevelFinishData(difficultyBeatmap, levelCompletionResults);
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x000034E7 File Offset: 0x000016E7
	private void HandlePartyFreePlayOverallStatsDataDidUpdate(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap)
	{
		this.ProcessLevelFinishData(difficultyBeatmap, levelCompletionResults);
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00018994 File Offset: 0x00016B94
	public void HandleCampaignOverallStatsDataDidUpdate(MissionCompletionResults missionCompletionResults, MissionNode missionNode)
	{
		this.ProcessMissionFinishData(missionNode, missionCompletionResults);
		IDifficultyBeatmap difficultyBeatmap = missionNode.missionData.level.beatmapLevelData.GetDifficultyBeatmap(missionNode.missionData.beatmapCharacteristic, missionNode.missionData.beatmapDifficulty);
		this.ProcessLevelFinishData(difficultyBeatmap, missionCompletionResults.levelCompletionResults);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000189E4 File Offset: 0x00016BE4
	private void ProcessMissionFinishData(MissionNode missionNode, MissionCompletionResults missionCompletionResults)
	{
		if (!missionCompletionResults.IsMissionComplete)
		{
			return;
		}
		int num = 0;
		string missionId = this._missionNodesManager.finalMissionNode.missionId;
		if (missionNode.missionId == missionId)
		{
			this._achievementsModel.UnlockAchievement(this._finalMissionClearedAchievement);
		}
		using (List<PlayerMissionStatsData>.Enumerator enumerator = this._playerDataModel.playerData.missionsStatsData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.cleared)
				{
					num++;
				}
			}
		}
		if (num >= 30)
		{
			this._achievementsModel.UnlockAchievement(this._cleared30MissionsAchievement);
		}
		if (num >= this._missionNodesManager.allMissionNodes.Length)
		{
			this._achievementsModel.UnlockAchievement(this._allMissionClearedAchievement);
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00018AB8 File Offset: 0x00016CB8
	private void ProcessSoloFreePlayLevelFinishData(IDifficultyBeatmap difficultyBeatmap, LevelCompletionResults levelCompletionResults)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		foreach (PlayerLevelStatsData playerLevelStatsData in this._playerDataModel.playerData.levelsStatsData)
		{
			if (playerLevelStatsData.validScore)
			{
				if (playerLevelStatsData.fullCombo)
				{
					if (playerLevelStatsData.difficulty == BeatmapDifficulty.Expert)
					{
						num++;
					}
					if (playerLevelStatsData.difficulty == BeatmapDifficulty.Hard)
					{
						num2++;
					}
				}
				if (playerLevelStatsData.maxRank >= RankModel.Rank.S)
				{
					if (playerLevelStatsData.difficulty == BeatmapDifficulty.Expert)
					{
						num3++;
					}
					if (playerLevelStatsData.difficulty == BeatmapDifficulty.Hard)
					{
						num4++;
					}
				}
			}
		}
		if (levelCompletionResults.rank >= RankModel.Rank.S)
		{
			if (num4 >= 15 && difficultyBeatmap.difficulty == BeatmapDifficulty.Hard)
			{
				this._achievementsModel.UnlockAchievement(this._15HardLevelsRankSAchievement);
			}
			if (num3 >= 15 && difficultyBeatmap.difficulty == BeatmapDifficulty.Expert)
			{
				this._achievementsModel.UnlockAchievement(this._15ExpertLevelsRankSAchievement);
			}
		}
		if (levelCompletionResults.fullCombo)
		{
			if (num2 >= 15 && difficultyBeatmap.difficulty == BeatmapDifficulty.Hard)
			{
				this._achievementsModel.UnlockAchievement(this._15HardLevelsFullComboAchievement);
			}
			if (num >= 15 && difficultyBeatmap.difficulty == BeatmapDifficulty.Expert)
			{
				this._achievementsModel.UnlockAchievement(this._15ExpertLevelsFullComboAchievement);
			}
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00018BFC File Offset: 0x00016DFC
	private void ProcessLevelFinishData(IDifficultyBeatmap difficultyBeatmap, LevelCompletionResults levelCompletionResults)
	{
		PlayerAllOverallStatsData.PlayerOverallStatsData allOverallStatsData = this._playerDataModel.playerData.playerAllOverallStatsData.allOverallStatsData;
		bool flag = levelCompletionResults.gameplayModifiers.IsWithoutModifiers();
		if (allOverallStatsData.timePlayed >= 86400f)
		{
			this._achievementsModel.UnlockAchievement(this._24HoursPlayedAchievement);
		}
		if (levelCompletionResults.modifiedScore > 0 && allOverallStatsData.totalScore >= 100000000L)
		{
			this._achievementsModel.UnlockAchievement(this._totalScore100MillionAchievement);
		}
		if ((levelCompletionResults.leftHandMovementDistance > 0f || levelCompletionResults.rightHandMovementDistance > 0f) && allOverallStatsData.handDistanceTravelled >= 100000)
		{
			this._achievementsModel.UnlockAchievement(this._kilometersTravelled100Achievement);
		}
		if (levelCompletionResults.goodCutsCount > 0 && allOverallStatsData.goodCutsCount >= 10000)
		{
			this._achievementsModel.UnlockAchievement(this._goodCuts10000Achievement);
		}
		if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			if (flag)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevelWithoutModifiersAchievement);
			}
			if (allOverallStatsData.cleardLevelsCount >= 100)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevel100Achievement);
			}
			if (difficultyBeatmap.difficulty == BeatmapDifficulty.Expert && flag)
			{
				this._achievementsModel.UnlockAchievement(this._expertLevelClearedWithoutModifiersAchievement);
			}
			if (levelCompletionResults.gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Faster)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevelWithSongSpeedFasterModifierAchievement);
			}
			if (levelCompletionResults.gameplayModifiers.instaFail)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevelWithInstaFailModifierAchievement);
			}
			if (levelCompletionResults.gameplayModifiers.disappearingArrows)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevelWithDisappearingArrowsModifierAchievement);
			}
			if (levelCompletionResults.gameplayModifiers.batteryEnergy)
			{
				this._achievementsModel.UnlockAchievement(this._clearedLevelWithBatteryEnergyModifierAchievement);
			}
			if (levelCompletionResults.rank >= RankModel.Rank.A && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Normal)
			{
				this._achievementsModel.UnlockAchievement(this._resultMinRankANormalWithoutModifiersAchievement);
			}
			if (levelCompletionResults.rank >= RankModel.Rank.S && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Hard)
			{
				this._achievementsModel.UnlockAchievement(this._resultMinRankSHardWithoutModifiersAchievement);
			}
			if (levelCompletionResults.rank >= RankModel.Rank.SS && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Expert)
			{
				this._achievementsModel.UnlockAchievement(this._resultMinRankSSExpertWithoutModifiersAchievement);
			}
			if (levelCompletionResults.fullCombo && (difficultyBeatmap.difficulty == BeatmapDifficulty.Expert && flag))
			{
				this._achievementsModel.UnlockAchievement(this._fullComboExpertWithoutModifiersAchievement);
			}
			if (levelCompletionResults.maxCombo >= 50 && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Normal)
			{
				this._achievementsModel.UnlockAchievement(this._combo50NormalWithoutModifiersAchievement);
			}
			if (levelCompletionResults.maxCombo >= 100 && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Hard)
			{
				this._achievementsModel.UnlockAchievement(this._combo100HardWithoutModifiersAchievement);
			}
			if (levelCompletionResults.maxCombo >= 500 && flag && difficultyBeatmap.difficulty == BeatmapDifficulty.Expert)
			{
				this._achievementsModel.UnlockAchievement(this._combo500ExpertWithoutModifiersAchievement);
			}
		}
	}

	// Token: 0x04000190 RID: 400
	[SerializeField]
	private AchievementsModelSO _achievementsModel;

	// Token: 0x04000191 RID: 401
	[Space]
	[SerializeField]
	private AchievementSO _clearedLevel100Achievement;

	// Token: 0x04000192 RID: 402
	[SerializeField]
	private AchievementSO _totalScore100MillionAchievement;

	// Token: 0x04000193 RID: 403
	[SerializeField]
	private AchievementSO _24HoursPlayedAchievement;

	// Token: 0x04000194 RID: 404
	[SerializeField]
	private AchievementSO _kilometersTravelled100Achievement;

	// Token: 0x04000195 RID: 405
	[Space]
	[SerializeField]
	private AchievementSO _15ExpertLevelsRankSAchievement;

	// Token: 0x04000196 RID: 406
	[SerializeField]
	private AchievementSO _15ExpertLevelsFullComboAchievement;

	// Token: 0x04000197 RID: 407
	[SerializeField]
	private AchievementSO _15HardLevelsRankSAchievement;

	// Token: 0x04000198 RID: 408
	[SerializeField]
	private AchievementSO _15HardLevelsFullComboAchievement;

	// Token: 0x04000199 RID: 409
	[Space]
	[SerializeField]
	private AchievementSO _expertLevelClearedWithoutModifiersAchievement;

	// Token: 0x0400019A RID: 410
	[SerializeField]
	private AchievementSO _fullComboExpertWithoutModifiersAchievement;

	// Token: 0x0400019B RID: 411
	[SerializeField]
	private AchievementSO _goodCuts10000Achievement;

	// Token: 0x0400019C RID: 412
	[Space]
	[SerializeField]
	private AchievementSO _resultMinRankANormalWithoutModifiersAchievement;

	// Token: 0x0400019D RID: 413
	[SerializeField]
	private AchievementSO _resultMinRankSHardWithoutModifiersAchievement;

	// Token: 0x0400019E RID: 414
	[SerializeField]
	private AchievementSO _resultMinRankSSExpertWithoutModifiersAchievement;

	// Token: 0x0400019F RID: 415
	[Space]
	[SerializeField]
	private AchievementSO _combo50NormalWithoutModifiersAchievement;

	// Token: 0x040001A0 RID: 416
	[SerializeField]
	private AchievementSO _combo100HardWithoutModifiersAchievement;

	// Token: 0x040001A1 RID: 417
	[SerializeField]
	private AchievementSO _combo500ExpertWithoutModifiersAchievement;

	// Token: 0x040001A2 RID: 418
	[Space]
	[SerializeField]
	private AchievementSO _clearedLevelWithoutModifiersAchievement;

	// Token: 0x040001A3 RID: 419
	[SerializeField]
	private AchievementSO _clearedLevelWithSongSpeedFasterModifierAchievement;

	// Token: 0x040001A4 RID: 420
	[SerializeField]
	private AchievementSO _clearedLevelWithInstaFailModifierAchievement;

	// Token: 0x040001A5 RID: 421
	[SerializeField]
	private AchievementSO _clearedLevelWithDisappearingArrowsModifierAchievement;

	// Token: 0x040001A6 RID: 422
	[SerializeField]
	private AchievementSO _clearedLevelWithBatteryEnergyModifierAchievement;

	// Token: 0x040001A7 RID: 423
	[Space]
	[SerializeField]
	private AchievementSO _cleared30MissionsAchievement;

	// Token: 0x040001A8 RID: 424
	[SerializeField]
	private AchievementSO _finalMissionClearedAchievement;

	// Token: 0x040001A9 RID: 425
	[SerializeField]
	private AchievementSO _allMissionClearedAchievement;

	// Token: 0x040001AA RID: 426
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x040001AB RID: 427
	[Inject]
	private MissionNodesManager _missionNodesManager;
}
