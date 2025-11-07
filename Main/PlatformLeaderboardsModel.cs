using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x020000E3 RID: 227
public class PlatformLeaderboardsModel : MonoBehaviour
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000376 RID: 886 RVA: 0x0001F708 File Offset: 0x0001D908
	// (remove) Token: 0x06000377 RID: 887 RVA: 0x0001F740 File Offset: 0x0001D940
	public event Action allScoresDidUploadEvent;

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000378 RID: 888 RVA: 0x000041E9 File Offset: 0x000023E9
	public bool valid
	{
		get
		{
			return this.platformLeaderboardsHandler != null;
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000041F4 File Offset: 0x000023F4
	public void Init()
	{
		if (this._platformLeaderboardsHandler != null && this._getUserInfoAsyncRequest == null)
		{
			this._getUserInfoAsyncRequest = this._platformUserModel.GetUserInfo(delegate(PlatformUserModelSO.GetUserInfoResult result, PlatformUserModelSO.UserInfo userInfo)
			{
				this._getUserInfoAsyncRequest = null;
				if (result == PlatformUserModelSO.GetUserInfoResult.OK)
				{
					this._playerId = userInfo.userId;
					this._leaderboardScoreUploader.Init(new LeaderboardScoreUploader.UploadScoreCallback(this.UploadScore), this._playerId);
					this._leaderboardScoreUploader.allScoresDidUploadEvent += this.HandleAllScoresDidUpload;
				}
			});
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600037A RID: 890 RVA: 0x00004223 File Offset: 0x00002423
	private PlatformLeaderboardsHandler platformLeaderboardsHandler
	{
		get
		{
			if (this._platformLeaderboardsHandler != null)
			{
				return this._platformLeaderboardsHandler;
			}
			this.Init();
			return this._platformLeaderboardsHandler;
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00004240 File Offset: 0x00002440
	private HMAsyncRequest UploadScore(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler)
	{
		if (this.platformLeaderboardsHandler != null)
		{
			return this.platformLeaderboardsHandler.UploadScore(scoreData, completionHandler);
		}
		if (completionHandler != null)
		{
			completionHandler(PlatformLeaderboardsModel.UploadScoreResult.Failed);
		}
		return null;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x00004263 File Offset: 0x00002463
	private HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.ScoresScope scope, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		if (this.platformLeaderboardsHandler != null && this._playerId != null)
		{
			return this.platformLeaderboardsHandler.GetScores(beatmap, count, fromRank, scope, this._playerId, completionHandler);
		}
		if (completionHandler != null)
		{
			completionHandler(PlatformLeaderboardsModel.GetScoresResult.Failed, null, -1);
		}
		return null;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0000429D File Offset: 0x0000249D
	private void HandleAllScoresDidUpload()
	{
		Action action = this.allScoresDidUploadEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000042AF File Offset: 0x000024AF
	public HMAsyncRequest GetScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		return this.GetScores(beatmap, count, fromRank, PlatformLeaderboardsModel.ScoresScope.Global, completionHandler);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000042BD File Offset: 0x000024BD
	public HMAsyncRequest GetScoresAroundPlayer(IDifficultyBeatmap beatmap, int count, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		return this.GetScores(beatmap, count, 0, PlatformLeaderboardsModel.ScoresScope.AroundPlayer, completionHandler);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x000042CA File Offset: 0x000024CA
	public HMAsyncRequest GetFriendsScores(IDifficultyBeatmap beatmap, int count, int fromRank, PlatformLeaderboardsModel.GetScoresCompletionHandler completionHandler)
	{
		return this.GetScores(beatmap, count, fromRank, PlatformLeaderboardsModel.ScoresScope.Friends, completionHandler);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001F778 File Offset: 0x0001D978
	public void UploadScore(IDifficultyBeatmap beatmap, int rawScore, int modifiedScore, bool fullCombo, int goodCutsCount, int badCutsCount, int missedCount, int maxCombo, GameplayModifiers gameplayModifiers)
	{
		if (this._leaderboardScoreUploader != null)
		{
			LeaderboardScoreUploader.ScoreData scoreData = new LeaderboardScoreUploader.ScoreData(this._playerId, beatmap, rawScore, modifiedScore, fullCombo, goodCutsCount, badCutsCount, missedCount, maxCombo, gameplayModifiers);
			this._leaderboardScoreUploader.AddScore(scoreData);
		}
	}

	// Token: 0x040003C8 RID: 968
	[Inject]
	private LeaderboardScoreUploader _leaderboardScoreUploader;

	// Token: 0x040003C9 RID: 969
	[Inject]
	private PlatformUserModelSO _platformUserModel;

	// Token: 0x040003CA RID: 970
	[Inject]
	private PlatformLeaderboardsHandler _platformLeaderboardsHandler;

	// Token: 0x040003CC RID: 972
	private string _playerId;

	// Token: 0x040003CD RID: 973
	private HMAsyncRequest _getUserInfoAsyncRequest;

	// Token: 0x020000E4 RID: 228
	public enum GetScoresResult
	{
		// Token: 0x040003CF RID: 975
		OK,
		// Token: 0x040003D0 RID: 976
		Failed
	}

	// Token: 0x020000E5 RID: 229
	public enum UploadScoreResult
	{
		// Token: 0x040003D2 RID: 978
		OK,
		// Token: 0x040003D3 RID: 979
		Failed
	}

	// Token: 0x020000E6 RID: 230
	public enum ScoresScope
	{
		// Token: 0x040003D5 RID: 981
		Global,
		// Token: 0x040003D6 RID: 982
		AroundPlayer,
		// Token: 0x040003D7 RID: 983
		Friends
	}

	// Token: 0x020000E7 RID: 231
	public enum GetPlayerIdResult
	{
		// Token: 0x040003D9 RID: 985
		OK,
		// Token: 0x040003DA RID: 986
		Failed
	}

	// Token: 0x020000E8 RID: 232
	// (Invoke) Token: 0x06000385 RID: 901
	public delegate void GetScoresCompletionHandler(PlatformLeaderboardsModel.GetScoresResult result, PlatformLeaderboardsModel.LeaderboardScore[] scores, int referencePlayerScoreIndex);

	// Token: 0x020000E9 RID: 233
	// (Invoke) Token: 0x06000389 RID: 905
	public delegate void UploadScoreCompletionHandler(PlatformLeaderboardsModel.UploadScoreResult result);

	// Token: 0x020000EA RID: 234
	// (Invoke) Token: 0x0600038D RID: 909
	public delegate void GetPlayerIdCompletionHandler(PlatformLeaderboardsModel.GetPlayerIdResult result, LeaderboardPlayerInfo playerInfo);

	// Token: 0x020000EB RID: 235
	public class LeaderboardScore
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000042D8 File Offset: 0x000024D8
		// (set) Token: 0x06000391 RID: 913 RVA: 0x000042E0 File Offset: 0x000024E0
		public int score { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000042E9 File Offset: 0x000024E9
		// (set) Token: 0x06000393 RID: 915 RVA: 0x000042F1 File Offset: 0x000024F1
		public int rank { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000042FA File Offset: 0x000024FA
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00004302 File Offset: 0x00002502
		public string playerName { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000430B File Offset: 0x0000250B
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00004313 File Offset: 0x00002513
		public string playerId { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000431C File Offset: 0x0000251C
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00004324 File Offset: 0x00002524
		public List<GameplayModifierParamsSO> gameplayModifiers { get; private set; }

		// Token: 0x0600039A RID: 922 RVA: 0x0000432D File Offset: 0x0000252D
		public LeaderboardScore(int score, int rank, string playerName, string playerId, List<GameplayModifierParamsSO> gameplayModifiers)
		{
			this.score = score;
			this.rank = rank;
			this.playerName = playerName;
			this.playerId = playerId;
			this.gameplayModifiers = gameplayModifiers;
		}
	}
}
