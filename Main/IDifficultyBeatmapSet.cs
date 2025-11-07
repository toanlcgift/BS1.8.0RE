using System;

// Token: 0x02000157 RID: 343
public interface IDifficultyBeatmapSet
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x0600054D RID: 1357
	BeatmapCharacteristicSO beatmapCharacteristic { get; }

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x0600054E RID: 1358
	IDifficultyBeatmap[] difficultyBeatmaps { get; }
}
