using System;

namespace OnlineServices
{
	// Token: 0x0200048C RID: 1164
	public readonly struct LevelScoreResultsData
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x0004F07C File Offset: 0x0004D27C
		public LevelScoreResultsData(IDifficultyBeatmap difficultyBeatmap, int rawScore, int modifiedScore, bool fullCombo, int goodCutsCount, int badCutsCount, int missedCount, int maxCombo, GameplayModifiers gameplayModifiers)
		{
			this.rawScore = rawScore;
			this.difficultyBeatmap = difficultyBeatmap;
			this.modifiedScore = modifiedScore;
			this.fullCombo = fullCombo;
			this.goodCutsCount = goodCutsCount;
			this.badCutsCount = badCutsCount;
			this.missedCount = missedCount;
			this.maxCombo = maxCombo;
			this.gameplayModifiers = gameplayModifiers;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000102BF File Offset: 0x0000E4BF
		public override string ToString()
		{
			return string.Format("LevelScoreResultsData: difficultyBeatmap = {0}, rawScore = {1}, modifiedScore = {2}, gameplayModifiers = {3}", new object[]
			{
				this.difficultyBeatmap,
				this.rawScore,
				this.modifiedScore,
				this.gameplayModifiers
			});
		}

		// Token: 0x040015BE RID: 5566
		public readonly IDifficultyBeatmap difficultyBeatmap;

		// Token: 0x040015BF RID: 5567
		public readonly int rawScore;

		// Token: 0x040015C0 RID: 5568
		public readonly int modifiedScore;

		// Token: 0x040015C1 RID: 5569
		public readonly bool fullCombo;

		// Token: 0x040015C2 RID: 5570
		public readonly int goodCutsCount;

		// Token: 0x040015C3 RID: 5571
		public readonly int badCutsCount;

		// Token: 0x040015C4 RID: 5572
		public readonly int missedCount;

		// Token: 0x040015C5 RID: 5573
		public readonly int maxCombo;

		// Token: 0x040015C6 RID: 5574
		public readonly GameplayModifiers gameplayModifiers;
	}
}
