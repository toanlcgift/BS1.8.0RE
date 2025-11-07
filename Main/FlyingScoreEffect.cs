using System;
using TMPro;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class FlyingScoreEffect : FlyingObjectEffect
{
	// Token: 0x06000A95 RID: 2709 RVA: 0x00031644 File Offset: 0x0002F844
	public void InitAndPresent(NoteCutInfo noteCutInfo, int multiplier, float duration, Vector3 targetPos, Quaternion rotation, Color color)
	{
		this._color = color;
		this._noteCutInfo = noteCutInfo;
		this._noteCutInfo.swingRatingCounter.didChangeEvent += this.HandleSaberSwingRatingCounterDidChangeEvent;
		int num;
		int num2;
		int num3;
		ScoreModel.RawScoreWithoutMultiplier(noteCutInfo, out num, out num2, out num3);
		this._text.text = this.GetScoreText(num + num2);
		bool enabled = num3 == 15;
		this._maxCutDistanceScoreIndicator.enabled = enabled;
		this._colorAMultiplier = (((float)(num + num2) > 103.5f) ? 1f : 0.3f);
		base.InitAndPresent(duration, targetPos, rotation, false);
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x000083B4 File Offset: 0x000065B4
	protected void OnDisable()
	{
		if (this._noteCutInfo != null && this._noteCutInfo.swingRatingCounter != null)
		{
			this._noteCutInfo.swingRatingCounter.didChangeEvent -= this.HandleSaberSwingRatingCounterDidChangeEvent;
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x000316D8 File Offset: 0x0002F8D8
	protected override void ManualUpdate(float t)
	{
		Color color = this._color.ColorWithAlpha(this._fadeAnimationCurve.Evaluate(t) * this._colorAMultiplier);
		this._text.color = color;
		this._maxCutDistanceScoreIndicator.color = color;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0003171C File Offset: 0x0002F91C
	private void HandleSaberSwingRatingCounterDidChangeEvent(SaberSwingRatingCounter saberSwingRatingCounter, float rating)
	{
		int num;
		int num2;
		int num3;
		ScoreModel.RawScoreWithoutMultiplier(this._noteCutInfo, out num, out num2, out num3);
		int num4 = num + num2 + num3;
		this._text.text = this.GetScoreText(num4);
		this._colorAMultiplier = (((float)num4 > 103.5f) ? 1f : 0.3f);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00005A88 File Offset: 0x00003C88
	private string GetScoreText(int score)
	{
		return score.ToString();
	}

	// Token: 0x04000AFE RID: 2814
	[SerializeField]
	private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x04000AFF RID: 2815
	[SerializeField]
	private SpriteRenderer _maxCutDistanceScoreIndicator;

	// Token: 0x04000B00 RID: 2816
	[SerializeField]
	private TextMeshPro _text;

	// Token: 0x04000B01 RID: 2817
	private Color _color;

	// Token: 0x04000B02 RID: 2818
	private float _colorAMultiplier;

	// Token: 0x04000B03 RID: 2819
	private NoteCutInfo _noteCutInfo;

	// Token: 0x02000274 RID: 628
	public class Pool : MemoryPoolWithActiveItems<FlyingScoreEffect>
	{
	}
}
