using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000159 RID: 345
public interface IPreviewBeatmapLevel
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000551 RID: 1361
	string levelID { get; }

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000552 RID: 1362
	string songName { get; }

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000553 RID: 1363
	string songSubName { get; }

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000554 RID: 1364
	string songAuthorName { get; }

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000555 RID: 1365
	string levelAuthorName { get; }

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000556 RID: 1366
	float beatsPerMinute { get; }

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000557 RID: 1367
	float songTimeOffset { get; }

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000558 RID: 1368
	float shuffle { get; }

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000559 RID: 1369
	float shufflePeriod { get; }

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600055A RID: 1370
	float previewStartTime { get; }

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x0600055B RID: 1371
	float previewDuration { get; }

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x0600055C RID: 1372
	float songDuration { get; }

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x0600055D RID: 1373
	EnvironmentInfoSO environmentInfo { get; }

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x0600055E RID: 1374
	EnvironmentInfoSO allDirectionsEnvironmentInfo { get; }

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x0600055F RID: 1375
	PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets { get; }

	// Token: 0x06000560 RID: 1376
	Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken);

	// Token: 0x06000561 RID: 1377
	Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken);
}
