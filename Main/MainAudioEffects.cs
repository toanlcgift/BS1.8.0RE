using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class MainAudioEffects : MonoBehaviour
{
	// Token: 0x06000163 RID: 355 RVA: 0x0000320F File Offset: 0x0000140F
	protected void Start()
	{
		this._audioLowPassFilter.enabled = false;
		base.enabled = false;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000178DC File Offset: 0x00015ADC
	protected void LateUpdate()
	{
		float cutoffFrequency = this._audioLowPassFilter.cutoffFrequency;
		float num = Mathf.Lerp(cutoffFrequency, this._targetFrequency, Time.deltaTime * this._smooth);
		if (Mathf.Abs(cutoffFrequency - this._targetFrequency) < 1f)
		{
			base.enabled = false;
			num = this._targetFrequency;
			if (num == 22000f)
			{
				this._audioLowPassFilter.enabled = false;
			}
		}
		this._audioLowPassFilter.cutoffFrequency = num;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00003224 File Offset: 0x00001424
	public void ResumeNormalSound()
	{
		base.enabled = true;
		this._targetFrequency = 22000f;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00003238 File Offset: 0x00001438
	public void TriggerLowPass()
	{
		base.enabled = true;
		this._audioLowPassFilter.enabled = true;
		this._targetFrequency = 150f;
	}

	// Token: 0x0400013A RID: 314
	[SerializeField]
	private AudioLowPassFilter _audioLowPassFilter;

	// Token: 0x0400013B RID: 315
	[SerializeField]
	private float _smooth = 8f;

	// Token: 0x0400013C RID: 316
	private const int kDefaultCutoffFrequency = 22000;

	// Token: 0x0400013D RID: 317
	private const int kLowPassCutoffFrequency = 150;

	// Token: 0x0400013E RID: 318
	private float _targetFrequency;
}
