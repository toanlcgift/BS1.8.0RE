using System;

// Token: 0x02000313 RID: 787
public class CutScoreBuffer : HMAutoincrementedRequestId
{
	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0000A787 File Offset: 0x00008987
	public int scoreWithMultiplier
	{
		get
		{
			return this._afterCutScoreWithMultiplier + this._beforeCutScoreWithMultiplier + this._cutDistanceScoreWithMultiplier;
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0000A79D File Offset: 0x0000899D
	public int multiplier
	{
		get
		{
			return this._multiplier;
		}
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00038C98 File Offset: 0x00036E98
	public CutScoreBuffer(NoteCutInfo noteCutInfo, int multiplier)
	{
		this._multiplier = multiplier;
		this._noteCutInfo = noteCutInfo;
		noteCutInfo.swingRatingCounter.didChangeEvent += this.HandleSwingRatingCounterDidChangeEvent;
		noteCutInfo.swingRatingCounter.didFinishEvent += this.HandleSwingRatingCounterDidFinishEvent;
		this.RefreshScores();
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0000A7A5 File Offset: 0x000089A5
	private void HandleSwingRatingCounterDidChangeEvent(SaberSwingRatingCounter swingRatingCounter, float rating)
	{
		this.RefreshScores();
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00038CF0 File Offset: 0x00036EF0
	private void RefreshScores()
	{
		int num;
		int num2;
		int num3;
		ScoreModel.RawScoreWithoutMultiplier(this._noteCutInfo, out num, out num2, out num3);
		this._afterCutScoreWithMultiplier = num2 * this._multiplier;
		this._beforeCutScoreWithMultiplier = num * this._multiplier;
		this._cutDistanceScoreWithMultiplier = num3 * this._multiplier;
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0000A7AD File Offset: 0x000089AD
	private void HandleSwingRatingCounterDidFinishEvent(SaberSwingRatingCounter swingRatingCounter)
	{
		swingRatingCounter.didChangeEvent -= this.HandleSwingRatingCounterDidChangeEvent;
		swingRatingCounter.didFinishEvent -= this.HandleSwingRatingCounterDidFinishEvent;
		Action<CutScoreBuffer> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x04000DEF RID: 3567
	public Action<CutScoreBuffer> didFinishEvent;

	// Token: 0x04000DF0 RID: 3568
	private int _afterCutScoreWithMultiplier;

	// Token: 0x04000DF1 RID: 3569
	private int _beforeCutScoreWithMultiplier;

	// Token: 0x04000DF2 RID: 3570
	private int _cutDistanceScoreWithMultiplier;

	// Token: 0x04000DF3 RID: 3571
	private int _multiplier;

	// Token: 0x04000DF4 RID: 3572
	private NoteCutInfo _noteCutInfo;
}
