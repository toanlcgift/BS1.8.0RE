using System;

namespace OnlineServices
{
	// Token: 0x0200048E RID: 1166
	public readonly struct GetLeaderboardEntriesResult
	{
		// Token: 0x060015A8 RID: 5544 RVA: 0x0001032C File Offset: 0x0000E52C
		public GetLeaderboardEntriesResult(bool isError, LeaderboardEntryData[] leaderboardEntries, int referencePlayerScoreIndex)
		{
			this.isError = isError;
			this.leaderboardEntries = leaderboardEntries;
			this.referencePlayerScoreIndex = referencePlayerScoreIndex;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00010343 File Offset: 0x0000E543
		public static GetLeaderboardEntriesResult resultWithError
		{
			get
			{
				return new GetLeaderboardEntriesResult(true, null, -1);
			}
		}

		// Token: 0x040015CC RID: 5580
		public readonly bool isError;

		// Token: 0x040015CD RID: 5581
		public readonly LeaderboardEntryData[] leaderboardEntries;

		// Token: 0x040015CE RID: 5582
		public readonly int referencePlayerScoreIndex;
	}
}
