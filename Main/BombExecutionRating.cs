using System;

// Token: 0x02000092 RID: 146
public class BombExecutionRating : BeatmapObjectExecutionRating
{
	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000239 RID: 569 RVA: 0x000038C7 File Offset: 0x00001AC7
	// (set) Token: 0x0600023A RID: 570 RVA: 0x000038CF File Offset: 0x00001ACF
	public BombExecutionRating.Rating rating { get; set; }

	// Token: 0x0600023B RID: 571 RVA: 0x000038D8 File Offset: 0x00001AD8
	public BombExecutionRating(float time, BombExecutionRating.Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Bomb;
		base.time = time;
		this.rating = rating;
	}

	// Token: 0x02000093 RID: 147
	public enum Rating
	{
		// Token: 0x04000263 RID: 611
		OK,
		// Token: 0x04000264 RID: 612
		NotGood
	}
}
