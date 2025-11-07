using System;
using UnityEngine;
using Zenject;

// Token: 0x0200004A RID: 74
public class AutomaticSFXVolume : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x00002FC7 File Offset: 0x000011C7
	protected void Start()
	{
		this.RecalculateParams();
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00002FCF File Offset: 0x000011CF
	protected void OnDisable()
	{
		if (this._audioManager != null)
		{
			this._audioManager.sfxVolume = 0f;
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00002FC7 File Offset: 0x000011C7
	protected void OnValidate()
	{
		this.RecalculateParams();
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000175D4 File Offset: 0x000157D4
	private void RecalculateParams()
	{
		this._sampleRate = (float)AudioSettings.outputSampleRate;
		float num = Mathf.Max(this._params.attackTime, 0.01f);
		float num2 = Mathf.Max(this._params.releaseTime, 0.01f);
		this._attackSamples = num * this._sampleRate;
		this._attackCoef = 1f / this._attackSamples;
		this._releaseSamples = num2 * this._sampleRate;
		this._releaseCoef = 1f / this._releaseSamples;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0001765C File Offset: 0x0001585C
	protected void OnAudioFilterRead(float[] data, int channels)
	{
		int num = data.Length / channels;
		for (int i = 0; i < num; i++)
		{
			float num2 = 0f;
			for (int j = 0; j < channels; j++)
			{
				num2 += Mathf.Abs(data[i * channels + j]) * this._params.musicVolumeMultiplier;
			}
			if (num2 > this._params.threshold)
			{
				num2 -= this._params.threshold;
			}
			else
			{
				num2 = 0f;
			}
			if (num2 > this._envelope)
			{
				this._envelope += this._attackCoef;
			}
			else if (this._envelope > 0f)
			{
				this._envelope -= this._releaseCoef;
			}
		}
		float num3 = (float)num / this._sampleRate;
		this._volume = Mathf.Lerp(this._volume, this._params.minVolume + this._envelope * this._params.impact, num3 * this._params.volumeSmooth);
		if (this._volume > this._params.maxVolume)
		{
			this._volume = this._params.maxVolume;
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00002FEF File Offset: 0x000011EF
	protected void Update()
	{
		this._audioManager.sfxVolume = this._volume + this._initData.volumeOffset;
	}

	// Token: 0x0400011B RID: 283
	[SerializeField]
	private AutomaticSFXVolumeParamsSO _params;

	// Token: 0x0400011C RID: 284
	[Space]
	[SerializeField]
	private AudioManagerSO _audioManager;

	// Token: 0x0400011D RID: 285
	[Inject]
	private AutomaticSFXVolume.InitData _initData;

	// Token: 0x0400011E RID: 286
	private float _sampleRate = 44100f;

	// Token: 0x0400011F RID: 287
	private float _volume = 1f;

	// Token: 0x04000120 RID: 288
	private float _envelope;

	// Token: 0x04000121 RID: 289
	private float _attackSamples;

	// Token: 0x04000122 RID: 290
	private float _releaseSamples;

	// Token: 0x04000123 RID: 291
	private float _attackCoef;

	// Token: 0x04000124 RID: 292
	private float _releaseCoef;

	// Token: 0x0200004B RID: 75
	public class InitData
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000302C File Offset: 0x0000122C
		public InitData(float volumeOffset)
		{
			this.volumeOffset = volumeOffset;
		}

		// Token: 0x04000125 RID: 293
		public readonly float volumeOffset;
	}
}
