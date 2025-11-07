using System;
using System.Collections.Generic;
using Steamworks;

// Token: 0x020000EC RID: 236
public class SteamPlatformLeaderboardsHandler : PlatformLeaderboardsHandler
{
	// Token: 0x0600039B RID: 923 RVA: 0x0000435A File Offset: 0x0000255A
	public SteamPlatformLeaderboardsHandler()
	{
		this._steamLeaderboardsByLeaderboardId = new Dictionary<string, SteamLeaderboard_t>();
		this._streamRequests = new HashSet<HMAutoincrementedRequestId>();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001F814 File Offset: 0x0001DA14
	private void UploadScore(SteamLeaderboard_t steamLeaderboardId, int modifiedScore, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		if (SteamManager.Initialized)
		{
			SteamAsyncRequest<LeaderboardScoreUploaded_t> steamRequest = new SteamAsyncRequest<LeaderboardScoreUploaded_t>();
			this._streamRequests.Add(steamRequest);
			if (asyncRequest != null)
			{
				asyncRequest.CancelHandler = delegate(HMAsyncRequest request)
				{
					steamRequest.Cancel();
					this._streamRequests.Remove(steamRequest);
				};
			}
			SteamAPICall_t apiCall = SteamUserStats.UploadLeaderboardScore(steamLeaderboardId, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, modifiedScore, null, 0);
			steamRequest.MakeRequest(apiCall, delegate(LeaderboardScoreUploaded_t result, bool ioFailure)
			{
				this._streamRequests.Remove(steamRequest);
				PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler3 = completionHandler;
				if (completionHandler3 == null)
				{
					return;
				}
				completionHandler3(PlatformLeaderboardsModel.UploadScoreResult.OK);
			});
			return;
		}
		PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler2 = completionHandler;
		if (completionHandler2 == null)
		{
			return;
		}
		completionHandler2(PlatformLeaderboardsModel.UploadScoreResult.Failed);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001F8A4 File Offset: 0x0001DAA4
	private void GetScores(SteamLeaderboard_t steamLeaderboardId, int rangeStart, int rangeEnd, ELeaderboardDataRequest scope, string referencePlayerId, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		if (SteamManager.Initialized)
		{
			SteamAsyncRequest<LeaderboardScoresDownloaded_t> steamRequest = new SteamAsyncRequest<LeaderboardScoresDownloaded_t>();
			this._streamRequests.Add(steamRequest);
			if (asyncRequest != null)
			{
				asyncRequest.CancelHandler = delegate(HMAsyncRequest request)
				{
					steamRequest.Cancel();
					this._streamRequests.Remove(steamRequest);
				};
			}
			SteamAPICall_t apiCall = SteamUserStats.DownloadLeaderboardEntries(steamLeaderboardId, scope, rangeStart, rangeEnd);
			steamRequest.MakeRequest(apiCall, delegate(LeaderboardScoresDownloaded_t result, bool ioFailure)
			{
				this._streamRequests.Remove(steamRequest);
				if (ioFailure)
				{
					PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler3 = completionHandler;
					if (completionHandler3 == null)
					{
						return;
					}
					completionHandler3(PlatformLeaderboardsModel.GetScoresResult.Failed, null, -1);
					return;
				}
				else
				{
					PlatformLeaderboardsModel.LeaderboardScore[] array = new PlatformLeaderboardsModel.LeaderboardScore[result.m_cEntryCount];
					LeaderboardEntry_t[] array2 = new LeaderboardEntry_t[result.m_cEntryCount];
					int referencePlayerScoreIndex = -1;
					for (int i = 0; i < result.m_cEntryCount; i++)
					{
						SteamUserStats.GetDownloadedLeaderboardEntry(result.m_hSteamLeaderboardEntries, i, out array2[i], null, 0);
						array[i] = new PlatformLeaderboardsModel.LeaderboardScore(array2[i].m_nScore, array2[i].m_nGlobalRank, SteamFriends.GetFriendPersonaName(array2[i].m_steamIDUser), array2[i].m_steamIDUser.m_SteamID.ToString(), new List<GameplayModifierParamsSO>());
						if (array[i].playerId == referencePlayerId)
						{
							referencePlayerScoreIndex = i;
						}
					}
					PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler4 = completionHandler;
					if (completionHandler4 == null)
					{
						return;
					}
					completionHandler4(PlatformLeaderboardsModel.GetScoresResult.OK, array, referencePlayerScoreIndex);
					return;
				}
			});
			return;
		}
		PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler2 = completionHandler;
		if (completionHandler2 == null)
		{
			return;
		}
		completionHandler2(PlatformLeaderboardsModel.GetScoresResult.Failed, new PlatformLeaderboardsModel.LeaderboardScore[0], 0);
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0001F948 File Offset: 0x0001DB48
	private void GetSteamLeaderboardId(string leaderboadId, HMAsyncRequest asyncRequest, SteamPlatformLeaderboardsHandler.FindLeaderboardCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			SteamPlatformLeaderboardsHandler.FindLeaderboardCompletionHandler completionHandler2 = completionHandler;
			if (completionHandler2 == null)
			{
				return;
			}
			completionHandler2(false, default(SteamLeaderboard_t));
			return;
		}
		else
		{
			SteamLeaderboard_t leaderboardId;
			if (!this._steamLeaderboardsByLeaderboardId.TryGetValue(leaderboadId, out leaderboardId))
			{
				SteamAsyncRequest<LeaderboardFindResult_t> steamRequest = new SteamAsyncRequest<LeaderboardFindResult_t>();
				this._streamRequests.Add(steamRequest);
				if (asyncRequest != null)
				{
					asyncRequest.CancelHandler = delegate(HMAsyncRequest c)
					{
						steamRequest.Cancel();
						this._streamRequests.Remove(steamRequest);
					};
				}
				SteamAPICall_t apiCall = SteamUserStats.FindLeaderboard(leaderboadId);
				steamRequest.MakeRequest(apiCall, delegate(LeaderboardFindResult_t result, bool ioFailure)
				{
					this._streamRequests.Remove(steamRequest);
					if (result.m_bLeaderboardFound == 0 || ioFailure)
					{
						SteamPlatformLeaderboardsHandler.FindLeaderboardCompletionHandler completionHandler4 = completionHandler;
						if (completionHandler4 == null)
						{
							return;
						}
						completionHandler4(false, default(SteamLeaderboard_t));
						return;
					}
					else
					{
						if (!this._steamLeaderboardsByLeaderboardId.ContainsKey(leaderboadId))
						{
							this._steamLeaderboardsByLeaderboardId.Add(leaderboadId, result.m_hSteamLeaderboard);
						}
						SteamPlatformLeaderboardsHandler.FindLeaderboardCompletionHandler completionHandler5 = completionHandler;
						if (completionHandler5 == null)
						{
							return;
						}
						completionHandler5(true, result.m_hSteamLeaderboard);
						return;
					}
				});
				return;
			}
			SteamPlatformLeaderboardsHandler.FindLeaderboardCompletionHandler completionHandler3 = completionHandler;
			if (completionHandler3 == null)
			{
				return;
			}
			completionHandler3(true, leaderboardId);
			return;
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001FA24 File Offset: 0x0001DC24
	public override HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, string referencePlayerId, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		HMAsyncRequest asyncRequest = new HMAsyncRequest();
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(beatmap);
		this.GetSteamLeaderboardId(leaderboardID, asyncRequest, delegate(bool found, SteamLeaderboard_t steamLeaderboardId)
		{
			if (found)
			{
				ELeaderboardDataRequest scope2;
				int num;
				int rangeEnd;
				if (scope == PlatformLeaderboardsModel.ScoresScope.AroundPlayer)
				{
					scope2 = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser;
					num = -(count - 1) / 2;
					rangeEnd = count + num - 1;
				}
				else if (scope == PlatformLeaderboardsModel.ScoresScope.Friends)
				{
					scope2 = ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends;
					num = fromRank;
					rangeEnd = num + count;
				}
				else
				{
					scope2 = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;
					num = fromRank;
					rangeEnd = num + count;
				}
				this.GetScores(steamLeaderboardId, num, rangeEnd, scope2, referencePlayerId, asyncRequest, completionHandler);
				return;
			}
			PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler2 = completionHandler;
			if (completionHandler2 == null)
			{
				return;
			}
			completionHandler2(PlatformLeaderboardsModel.GetScoresResult.Failed, null, -1);
		});
		return asyncRequest;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001FA98 File Offset: 0x0001DC98
	public override HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		HMAsyncRequest asyncRequest = new HMAsyncRequest();
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(scoreData.beatmap);
		this.GetSteamLeaderboardId(leaderboardID, asyncRequest, delegate(bool found, SteamLeaderboard_t steamLeaderboardId)
		{
			if (found)
			{
				this.UploadScore(steamLeaderboardId, scoreData.modifiedScore, asyncRequest, completionHandler);
				return;
			}
			PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler2 = completionHandler;
			if (completionHandler2 == null)
			{
				return;
			}
			completionHandler2(PlatformLeaderboardsModel.UploadScoreResult.Failed);
		});
		return asyncRequest;
	}

	// Token: 0x040003E0 RID: 992
	private Dictionary<string, SteamLeaderboard_t> _steamLeaderboardsByLeaderboardId;

	// Token: 0x040003E1 RID: 993
	private HashSet<HMAutoincrementedRequestId> _streamRequests;

	// Token: 0x020000ED RID: 237
	// (Invoke) Token: 0x060003A2 RID: 930
	private delegate void FindLeaderboardCompletionHandler(bool found, SteamLeaderboard_t leaderboardId);
}
