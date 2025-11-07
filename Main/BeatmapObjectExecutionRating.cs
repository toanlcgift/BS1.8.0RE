using System;

// Token: 0x02000090 RID: 144
public class BeatmapObjectExecutionRating
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000234 RID: 564 RVA: 0x000038A5 File Offset: 0x00001AA5
	// (set) Token: 0x06000235 RID: 565 RVA: 0x000038AD File Offset: 0x00001AAD
	public BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType beatmapObjectRatingType { get; set; }

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000236 RID: 566 RVA: 0x000038B6 File Offset: 0x00001AB6
	// (set) Token: 0x06000237 RID: 567 RVA: 0x000038BE File Offset: 0x00001ABE
	public float time { get; set; }

	// Token: 0x02000091 RID: 145
	public enum BeatmapObjectExecutionRatingType
	{
		// Token: 0x0400025E RID: 606
		Note,
		// Token: 0x0400025F RID: 607
		Bomb,
		// Token: 0x04000260 RID: 608
		Obstacle
	}
}
