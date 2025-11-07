using System;

// Token: 0x02000194 RID: 404
public class NoteExecutionRating : BeatmapObjectExecutionRating
{
	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000667 RID: 1639 RVA: 0x00005BF7 File Offset: 0x00003DF7
	// (set) Token: 0x06000668 RID: 1640 RVA: 0x00005BFF File Offset: 0x00003DFF
	public NoteExecutionRating.Rating rating { get; set; }

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000669 RID: 1641 RVA: 0x00005C08 File Offset: 0x00003E08
	// (set) Token: 0x0600066A RID: 1642 RVA: 0x00005C10 File Offset: 0x00003E10
	public int cutScore { get; set; }

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x0600066B RID: 1643 RVA: 0x00005C19 File Offset: 0x00003E19
	// (set) Token: 0x0600066C RID: 1644 RVA: 0x00005C21 File Offset: 0x00003E21
	public float cutTimeDeviation { get; set; }

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x0600066D RID: 1645 RVA: 0x00005C2A File Offset: 0x00003E2A
	// (set) Token: 0x0600066E RID: 1646 RVA: 0x00005C32 File Offset: 0x00003E32
	public float cutDirDeviation { get; set; }

	// Token: 0x0600066F RID: 1647 RVA: 0x00005C3B File Offset: 0x00003E3B
	public NoteExecutionRating(float time, NoteExecutionRating.Rating rating, int cutScore, float cutTimeDeviation, float cutDirDeviation)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Note;
		base.time = time;
		this.rating = rating;
		this.cutScore = cutScore;
		this.cutTimeDeviation = cutTimeDeviation;
		this.cutDirDeviation = cutDirDeviation;
	}

	// Token: 0x02000195 RID: 405
	public enum Rating
	{
		// Token: 0x040006C8 RID: 1736
		GoodCut,
		// Token: 0x040006C9 RID: 1737
		Missed,
		// Token: 0x040006CA RID: 1738
		BadCut
	}
}
