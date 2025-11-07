using System;

// Token: 0x020000E2 RID: 226
public class TestPlatformLeaderboardsHandler : PlatformLeaderboardsHandler
{
	// Token: 0x06000373 RID: 883 RVA: 0x000041DA File Offset: 0x000023DA
	public override HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		return new HMAsyncRequest();
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000041DA File Offset: 0x000023DA
	public override HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		return new HMAsyncRequest();
	}
}
