using System;

namespace OnlineServices
{
	// Token: 0x02000490 RID: 1168
	public readonly struct GetLeaderboardFilterData
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x0001034D File Offset: 0x0000E54D
		public GetLeaderboardFilterData(IDifficultyBeatmap beatmap, int count, int fromRank, ScoresScope scope, bool includeScoreWithModifiers)
		{
			this.beatmap = beatmap;
			this.count = count;
			this.fromRank = fromRank;
			this.scope = scope;
			this.includeScoreWithModifiers = includeScoreWithModifiers;
		}

		// Token: 0x040015D2 RID: 5586
		public readonly IDifficultyBeatmap beatmap;

		// Token: 0x040015D3 RID: 5587
		public readonly int count;

		// Token: 0x040015D4 RID: 5588
		public readonly int fromRank;

		// Token: 0x040015D5 RID: 5589
		public readonly ScoresScope scope;

		// Token: 0x040015D6 RID: 5590
		public readonly bool includeScoreWithModifiers;
	}
}
