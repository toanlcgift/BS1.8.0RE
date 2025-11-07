using System;

// Token: 0x02000155 RID: 341
public interface IDifficultyBeatmap
{
	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000545 RID: 1349
	IBeatmapLevel level { get; }

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000546 RID: 1350
	IDifficultyBeatmapSet parentDifficultyBeatmapSet { get; }

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000547 RID: 1351
	BeatmapDifficulty difficulty { get; }

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000548 RID: 1352
	int difficultyRank { get; }

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000549 RID: 1353
	float noteJumpMovementSpeed { get; }

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x0600054A RID: 1354
	float noteJumpStartBeatOffset { get; }

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x0600054B RID: 1355
	BeatmapData beatmapData { get; }
}
