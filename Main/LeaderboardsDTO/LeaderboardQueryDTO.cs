using System;

namespace LeaderboardsDTO
{
	// Token: 0x02000483 RID: 1155
	[Serializable]
	public class LeaderboardQueryDTO
	{
		// Token: 0x04001589 RID: 5513
		public string leaderboardId;

		// Token: 0x0400158A RID: 5514
		public int count;

		// Token: 0x0400158B RID: 5515
		public int fromRank;

		// Token: 0x0400158C RID: 5516
		public LeaderboardQueryDTO.ScoresScope scope;

		// Token: 0x0400158D RID: 5517
		public string[] friendsUserIds;

		// Token: 0x0400158E RID: 5518
		public bool includedScoreWithModifiers;

		// Token: 0x02000484 RID: 1156
		public enum ScoresScope
		{
			// Token: 0x04001590 RID: 5520
			Global,
			// Token: 0x04001591 RID: 5521
			Friends
		}
	}
}
