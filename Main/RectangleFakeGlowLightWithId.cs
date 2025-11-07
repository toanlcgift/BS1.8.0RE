using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class RectangleFakeGlowLightWithId : LightWithId
{
	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00008D04 File Offset: 0x00006F04
	public Color color
	{
		get
		{
			return this._rectangleFakeGlow.color;
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00008D11 File Offset: 0x00006F11
	public override void ColorWasSet(Color color)
	{
		color.a *= this._alphaMul;
		if (color.a < this._minAlpha)
		{
			color.a = this._minAlpha;
		}
		this._rectangleFakeGlow.color = color;
	}

	// Token: 0x04000BDD RID: 3037
	[SerializeField]
	private float _minAlpha;

	// Token: 0x04000BDE RID: 3038
	[SerializeField]
	private float _alphaMul = 1f;

	// Token: 0x04000BDF RID: 3039
	[SerializeField]
	private RectangleFakeGlow _rectangleFakeGlow;
}
