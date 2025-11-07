using System;

// Token: 0x020000E1 RID: 225
public abstract class PlatformLeaderboardsHandler
{
	// Token: 0x06000370 RID: 880
	public abstract HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler);

	// Token: 0x06000371 RID: 881
	public abstract HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler);
}
