using System;
using System.Collections.Generic;
using System.Threading;
using OnlineServices;

// Token: 0x020001A1 RID: 417
public class LeaderboardsModelLegacyWrapper : PlatformLeaderboardsHandler
{
	// Token: 0x0600068F RID: 1679 RVA: 0x00005CF9 File Offset: 0x00003EF9
	public LeaderboardsModelLegacyWrapper(ILeaderboardsModel leaderboardsModel)
	{
		this._leaderboardsModel = leaderboardsModel;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00025824 File Offset: 0x00023A24
	public override HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken hmasyncRequestWithCancellationToken = new LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken();
		ScoresScope scope2 = ScoresScope.Global;
		if (scope == PlatformLeaderboardsModel.ScoresScope.Friends)
		{
			scope2 = ScoresScope.Friends;
		}
		else if (scope == PlatformLeaderboardsModel.ScoresScope.AroundPlayer)
		{
			scope2 = ScoresScope.Global;
		}
		GetLeaderboardFilterData leaderboardFilterData = new GetLeaderboardFilterData(beatmap, count, fromRank, scope2, true);
		this.GetLeaderboardEntriesAsync(leaderboardFilterData, hmasyncRequestWithCancellationToken, completionHandler);
		return hmasyncRequestWithCancellationToken;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x00025860 File Offset: 0x00023A60
	public override HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken hmasyncRequestWithCancellationToken = new LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken();
		LevelScoreResultsData levelScoreResultsData = new LevelScoreResultsData(scoreData.beatmap, scoreData.rawScore, scoreData.modifiedScore, scoreData.fullCombo, scoreData.goodCutsCount, scoreData.badCutsCount, scoreData.missedCount, scoreData.maxCombo, scoreData.gameplayModifiers);
		this.SendLevelScoreResutlAsync(levelScoreResultsData, hmasyncRequestWithCancellationToken, completionHandler);
		return hmasyncRequestWithCancellationToken;
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x000258BC File Offset: 0x00023ABC
	private async void GetLeaderboardEntriesAsync(GetLeaderboardFilterData leaderboardFilterData, LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken asyncRequest, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		GetLeaderboardEntriesResult getLeaderboardEntriesResult = default(GetLeaderboardEntriesResult);
		try
		{
			getLeaderboardEntriesResult = await this._leaderboardsModel.GetLeaderboardEntriesAsync(leaderboardFilterData, asyncRequest.cancellationTokenSource.Token);
		}
		catch (OperationCanceledException)
		{
			return;
		}
		if (!asyncRequest.cancelled)
		{
			if (getLeaderboardEntriesResult.isError)
			{
				if (completionHandler != null)
				{
					completionHandler(PlatformLeaderboardsModel.GetScoresResult.Failed, null, -1);
				}
			}
			else
			{
				PlatformLeaderboardsModel.LeaderboardScore[] array;
				if (getLeaderboardEntriesResult.leaderboardEntries != null && getLeaderboardEntriesResult.leaderboardEntries.Length != 0)
				{
					array = new PlatformLeaderboardsModel.LeaderboardScore[getLeaderboardEntriesResult.leaderboardEntries.Length];
					for (int i = 0; i < getLeaderboardEntriesResult.leaderboardEntries.Length; i++)
					{
						LeaderboardEntryData leaderboardEntryData = getLeaderboardEntriesResult.leaderboardEntries[i];
						List<GameplayModifierParamsSO> gameplayModifiers = new List<GameplayModifierParamsSO>();
						array[i] = new PlatformLeaderboardsModel.LeaderboardScore(leaderboardEntryData.score, leaderboardEntryData.rank, leaderboardEntryData.displayName, leaderboardEntryData.playerId, gameplayModifiers);
					}
				}
				else
				{
					array = new PlatformLeaderboardsModel.LeaderboardScore[0];
				}
				if (completionHandler != null)
				{
					completionHandler(PlatformLeaderboardsModel.GetScoresResult.OK, array, getLeaderboardEntriesResult.referencePlayerScoreIndex);
				}
			}
		}
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00025910 File Offset: 0x00023B10
	private async void SendLevelScoreResutlAsync(LevelScoreResultsData levelScoreResultsData, LeaderboardsModelLegacyWrapper.HMAsyncRequestWithCancellationToken asyncRequest, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		SendLeaderboardEntryResult sendLeaderboardEntryResult = SendLeaderboardEntryResult.Failed;
		try
		{
			sendLeaderboardEntryResult = await this._leaderboardsModel.SendLevelScoreResultAsync(levelScoreResultsData, asyncRequest.cancellationTokenSource.Token);
		}
		catch (OperationCanceledException)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformLeaderboardsModel.UploadScoreResult.Failed);
			}
			return;
		}
		if (asyncRequest.cancelled || sendLeaderboardEntryResult == SendLeaderboardEntryResult.Failed)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformLeaderboardsModel.UploadScoreResult.Failed);
			}
		}
		else if (completionHandler != null)
		{
			completionHandler(PlatformLeaderboardsModel.UploadScoreResult.OK);
		}
	}

	// Token: 0x040006F2 RID: 1778
	private ILeaderboardsModel _leaderboardsModel;

	// Token: 0x020001A2 RID: 418
	public class HMAsyncRequestWithCancellationToken : HMAsyncRequest
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00005D08 File Offset: 0x00003F08
		public CancellationTokenSource cancellationTokenSource
		{
			get
			{
				return this._cancellationTokenSource;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00005D10 File Offset: 0x00003F10
		public HMAsyncRequestWithCancellationToken()
		{
			this._cancellationTokenSource = new CancellationTokenSource();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00005D23 File Offset: 0x00003F23
		public override void Cancel()
		{
			base.Cancel();
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource == null)
			{
				return;
			}
			cancellationTokenSource.Cancel();
		}

		// Token: 0x040006F3 RID: 1779
		private CancellationTokenSource _cancellationTokenSource;
	}
}
