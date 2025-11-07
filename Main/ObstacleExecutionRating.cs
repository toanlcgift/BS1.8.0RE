using System;

// Token: 0x02000196 RID: 406
public class ObstacleExecutionRating : BeatmapObjectExecutionRating
{
	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000670 RID: 1648 RVA: 0x00005C6F File Offset: 0x00003E6F
	// (set) Token: 0x06000671 RID: 1649 RVA: 0x00005C77 File Offset: 0x00003E77
	public ObstacleExecutionRating.Rating rating { get; set; }

	// Token: 0x06000672 RID: 1650 RVA: 0x00005C80 File Offset: 0x00003E80
	public ObstacleExecutionRating(float time, ObstacleExecutionRating.Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Obstacle;
		base.time = time;
		this.rating = rating;
	}

	// Token: 0x02000197 RID: 407
	public enum Rating
	{
		// Token: 0x040006CD RID: 1741
		OK,
		// Token: 0x040006CE RID: 1742
		NotGood
	}
}
