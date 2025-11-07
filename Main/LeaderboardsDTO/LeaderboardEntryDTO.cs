using System;

namespace LeaderboardsDTO
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	public class LeaderboardEntryDTO
	{
		// Token: 0x040015A3 RID: 5539
		public int score;

		// Token: 0x040015A4 RID: 5540
		public int unmodifiedScore;

		// Token: 0x040015A5 RID: 5541
		public int rank;

		// Token: 0x040015A6 RID: 5542
		public DateTime updated;

		// Token: 0x040015A7 RID: 5543
		public string userDisplayName;

		// Token: 0x040015A8 RID: 5544
		public string platformUserId;

		// Token: 0x040015A9 RID: 5545
		public GameplayModifiersDTO[] gameplayModifiers;
	}
}
