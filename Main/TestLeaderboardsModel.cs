using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineServices;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class TestLeaderboardsModel : ILeaderboardsModel
{
	// Token: 0x14000012 RID: 18
	// (add) Token: 0x060006A0 RID: 1696 RVA: 0x00025D1C File Offset: 0x00023F1C
	// (remove) Token: 0x060006A1 RID: 1697 RVA: 0x00025D54 File Offset: 0x00023F54
	public event Action<string> scoreForLeaderboardDidUploadEvent;

	// Token: 0x060006A2 RID: 1698 RVA: 0x00005D7F File Offset: 0x00003F7F
	public string GetLeaderboardId(IDifficultyBeatmap difficultyBeatmap)
	{
		return LeaderboardsModel.GetLeaderboardID(difficultyBeatmap);
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00025D8C File Offset: 0x00023F8C
	public async Task<GetLeaderboardEntriesResult> GetLeaderboardEntriesAsync(GetLeaderboardFilterData leaderboardFilterData, CancellationToken cancellationToken)
	{
		await Task.Delay(200);
		LeaderboardEntryData[] array = new LeaderboardEntryData[10];
		int num = UnityEngine.Random.Range(0, 10);
		for (int i = 0; i < array.Length; i++)
		{
			string displayName = (num != i) ? (LeaderboardsModel.GetLeaderboardID(leaderboardFilterData.beatmap) + " " + UnityEngine.Random.Range(100000, 999999)) : string.Format("YOU - {0}", leaderboardFilterData.scope);
			array[i] = new LeaderboardEntryData(10000 / (i + 1) + UnityEngine.Random.Range(0, 100), leaderboardFilterData.fromRank + i, displayName, null, null);
		}
		return new GetLeaderboardEntriesResult(false, array, num);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00025DD4 File Offset: 0x00023FD4
	public async Task<SendLeaderboardEntryResult> SendLevelScoreResultAsync(LevelScoreResultsData levelScoreResult, CancellationToken cancellationToken)
	{
		await Task.Delay(200);
		Action<string> action = this.scoreForLeaderboardDidUploadEvent;
		if (action != null)
		{
			action(this.GetLeaderboardId(levelScoreResult.difficultyBeatmap));
		}
		return SendLeaderboardEntryResult.OK;
	}
}
