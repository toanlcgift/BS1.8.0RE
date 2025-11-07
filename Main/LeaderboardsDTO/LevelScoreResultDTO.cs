using System;

namespace LeaderboardsDTO
{
	// Token: 0x02000487 RID: 1159
	[Serializable]
	public class LevelScoreResultDTO
	{
		// Token: 0x04001595 RID: 5525
		public string guid;

		// Token: 0x04001596 RID: 5526
		public string guids;

		// Token: 0x04001597 RID: 5527
		public string guidInstance;

		// Token: 0x04001598 RID: 5528
		public int rawScore;

		// Token: 0x04001599 RID: 5529
		public int modifiedScore;

		// Token: 0x0400159A RID: 5530
		public bool fullCombo;

		// Token: 0x0400159B RID: 5531
		public int goodCutsCount;

		// Token: 0x0400159C RID: 5532
		public int badCutsCount;

		// Token: 0x0400159D RID: 5533
		public int missedCount;

		// Token: 0x0400159E RID: 5534
		public int maxCombo;

		// Token: 0x0400159F RID: 5535
		public GameplayModifiersDTO[] gameplayModifiers;

		// Token: 0x040015A0 RID: 5536
		public string leaderboardId;

		// Token: 0x040015A1 RID: 5537
		public string deviceModel;

		// Token: 0x040015A2 RID: 5538
		public string extraDataBase64;
	}
}
