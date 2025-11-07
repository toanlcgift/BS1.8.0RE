using System;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x02000277 RID: 631
public class FlyingTextEffect : FlyingObjectEffect
{
	// Token: 0x06000AA0 RID: 2720 RVA: 0x00008498 File Offset: 0x00006698
	public void InitAndPresent(string text, float duration, Vector3 targetPos, Quaternion rotation, Color color, float fontSize, bool shake)
	{
		base.InitAndPresent(duration, targetPos, rotation, shake);
		this._color = color;
		this._text.text = text;
		this._text.fontSize = fontSize;
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x000084C7 File Offset: 0x000066C7
	protected override void ManualUpdate(float t)
	{
		this._text.color = this._color.ColorWithAlpha(this._fadeAnimationCurve.Evaluate(t));
	}

	// Token: 0x04000B07 RID: 2823
	[SerializeField]
	private TextMeshPro _text;

	// Token: 0x04000B08 RID: 2824
	[SerializeField]
	private AnimationCurve _fadeAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x04000B09 RID: 2825
	protected Color _color;

	// Token: 0x02000278 RID: 632
	public class Pool : MonoMemoryPool<FlyingTextEffect>
	{
	}
}
