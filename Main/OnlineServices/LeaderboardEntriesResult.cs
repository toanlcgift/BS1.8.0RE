using System;
using Polyglot;

namespace OnlineServices
{
	// Token: 0x0200048A RID: 1162
	public class LeaderboardEntriesResult
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x00010242 File Offset: 0x0000E442
		private LeaderboardEntriesResult(LeaderboardEntryData[] leaderboardEntries, int referencePlayerScoreIndex, bool isError, string localizedErrorMessage)
		{
			this.isError = isError;
			this.localizedErrorMessage = localizedErrorMessage;
			this.leaderboardEntries = leaderboardEntries;
			this.referencePlayerScoreIndex = referencePlayerScoreIndex;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00010267 File Offset: 0x0000E467
		private static LeaderboardEntriesResult ErrorResult(string localizedErrorMessage)
		{
			return new LeaderboardEntriesResult(null, -1, true, localizedErrorMessage);
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00010272 File Offset: 0x0000E472
		public static LeaderboardEntriesResult notInicializedError
		{
			get
			{
				return LeaderboardEntriesResult.ErrorResult(Localization.Get("LEADERBOARDS_NOT_INITIALIZED_ERROR"));
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00010283 File Offset: 0x0000E483
		public static LeaderboardEntriesResult somethingWentWrongError
		{
			get
			{
				return LeaderboardEntriesResult.ErrorResult(Localization.Get("LEADERBOARDS_SOMETHING_WENT_WRONG_ERROR"));
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00010294 File Offset: 0x0000E494
		public static LeaderboardEntriesResult onlineServicesUnavailableError
		{
			get
			{
				return LeaderboardEntriesResult.ErrorResult(Localization.Get("LEADERBOARDS_PLATFORM_SERVICES_ERROR"));
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000102A5 File Offset: 0x0000E4A5
		public static LeaderboardEntriesResult FromGetLeaderboardEntriesResult(GetLeaderboardEntriesResult getLeaderboardEntriesResult)
		{
			return new LeaderboardEntriesResult(getLeaderboardEntriesResult.leaderboardEntries, getLeaderboardEntriesResult.referencePlayerScoreIndex, getLeaderboardEntriesResult.isError, null);
		}

		// Token: 0x040015BA RID: 5562
		public readonly bool isError;

		// Token: 0x040015BB RID: 5563
		public readonly string localizedErrorMessage;

		// Token: 0x040015BC RID: 5564
		public readonly LeaderboardEntryData[] leaderboardEntries;

		// Token: 0x040015BD RID: 5565
		public readonly int referencePlayerScoreIndex;
	}
}
