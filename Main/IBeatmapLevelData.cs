using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public interface IBeatmapLevelData
{
	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000541 RID: 1345
	AudioClip audioClip { get; }

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000542 RID: 1346
	IDifficultyBeatmapSet[] difficultyBeatmapSets { get; }
}
