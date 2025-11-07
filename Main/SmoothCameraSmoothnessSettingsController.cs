using System;
using UnityEngine;

// Token: 0x020003BA RID: 954
public class SmoothCameraSmoothnessSettingsController : ListSettingsController
{
	// Token: 0x0600118A RID: 4490 RVA: 0x000427A0 File Offset: 0x000409A0
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		this._smoothnesses = new float[]
		{
			20f,
			18f,
			16f,
			14f,
			12f,
			10f,
			8f,
			6f,
			4f,
			2f
		};
		FloatSO smoothCameraPositionSmooth = this._smoothCameraPositionSmooth;
		idx = 2;
		numberOfElements = this._smoothnesses.Length;
		for (int i = 0; i < this._smoothnesses.Length; i++)
		{
			if (smoothCameraPositionSmooth == this._smoothnesses[i])
			{
				idx = i;
				return true;
			}
		}
		return true;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x0000D59D File Offset: 0x0000B79D
	protected override void ApplyValue(int idx)
	{
		this._smoothCameraPositionSmooth.value = this._smoothnesses[idx];
		this._smoothCameraRotationSmooth.value = this._smoothnesses[idx];
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x00042804 File Offset: 0x00040A04
	protected override string TextForValue(int idx)
	{
		return string.Format("{0:0.0}", 1f - (this._smoothnesses[idx] - this._smoothnesses[this._smoothnesses.Length - 1]) / (this._smoothnesses[0] - this._smoothnesses[this._smoothnesses.Length - 1]));
	}

	// Token: 0x0400115D RID: 4445
	[SerializeField]
	private FloatSO _smoothCameraPositionSmooth;

	// Token: 0x0400115E RID: 4446
	[SerializeField]
	private FloatSO _smoothCameraRotationSmooth;

	// Token: 0x0400115F RID: 4447
	private float[] _smoothnesses;
}
