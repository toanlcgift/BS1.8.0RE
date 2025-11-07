using System;

namespace OnlineServices
{
	// Token: 0x0200048D RID: 1165
	public class LeaderboardEntryData
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x000102FF File Offset: 0x0000E4FF
		public LeaderboardEntryData(int score, int rank, string displayName, string playerId, GameplayModifiers gameplayModifiers)
		{
			this.score = score;
			this.rank = rank;
			this.displayName = displayName;
			this.playerId = playerId;
			this.gameplayModifiers = gameplayModifiers;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004F0D0 File Offset: 0x0004D2D0
		public override string ToString()
		{
			return string.Format("LeaderboardEntry: score = {0}, rank = {1}, playerName = {2}, playerId = {3}, gameplayModifiers = {4}", new object[]
			{
				this.score,
				this.rank,
				this.displayName,
				this.playerId,
				this.gameplayModifiers
			});
		}

		// Token: 0x040015C7 RID: 5575
		public readonly int score;

		// Token: 0x040015C8 RID: 5576
		public readonly int rank;

		// Token: 0x040015C9 RID: 5577
		public string displayName;

		// Token: 0x040015CA RID: 5578
		public readonly string playerId;

		// Token: 0x040015CB RID: 5579
		public readonly GameplayModifiers gameplayModifiers;
	}
}
