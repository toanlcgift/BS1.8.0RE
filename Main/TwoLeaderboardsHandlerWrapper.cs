using System;

// Token: 0x020001A5 RID: 421
public class TwoLeaderboardsHandlerWrapper : PlatformLeaderboardsHandler
{
	// Token: 0x0600069B RID: 1691 RVA: 0x00005D57 File Offset: 0x00003F57
	public TwoLeaderboardsHandlerWrapper(PlatformLeaderboardsHandler mainHandler, PlatformLeaderboardsHandler shadowHandler)
	{
		this._mainHandler = mainHandler;
		this._shadowHandler = shadowHandler;
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00025C6C File Offset: 0x00023E6C
	public override HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		if (this._getScoresShadowAsyncRequest != null)
		{
			this._getScoresShadowAsyncRequest.Cancel();
			this._getScoresShadowAsyncRequest = null;
		}
		this._getScoresShadowAsyncRequest = this._shadowHandler.GetScores(beatmap, count, fromRank, scope, referencePlayerId, delegate(PlatformLeaderboardsModel.GetScoresResult result, PlatformLeaderboardsModel.LeaderboardScore[] scores, int referencePlayerScoreIndex)
		{
			this._getScoresShadowAsyncRequest = null;
		});
		return this._mainHandler.GetScores(beatmap, count, fromRank, scope, referencePlayerId, completionHandler);
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x00025CCC File Offset: 0x00023ECC
	public override HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		if (this._uploadScoreShadowAsyncRequest != null)
		{
			this._uploadScoreShadowAsyncRequest.Cancel();
			this._uploadScoreShadowAsyncRequest = null;
		}
		this._shadowHandler.UploadScore(scoreData, delegate(PlatformLeaderboardsModel.UploadScoreResult result)
		{
			this._uploadScoreShadowAsyncRequest = null;
		});
		return this._mainHandler.UploadScore(scoreData, completionHandler);
	}

	// Token: 0x04000702 RID: 1794
	private PlatformLeaderboardsHandler _mainHandler;

	// Token: 0x04000703 RID: 1795
	private PlatformLeaderboardsHandler _shadowHandler;

	// Token: 0x04000704 RID: 1796
	private HMAsyncRequest _getScoresShadowAsyncRequest;

	// Token: 0x04000705 RID: 1797
	private HMAsyncRequest _uploadScoreShadowAsyncRequest;
}
