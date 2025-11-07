using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class BasicSpectrogramData : MonoBehaviour
{
	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00008DB4 File Offset: 0x00006FB4
	public float[] Samples
	{
		get
		{
			if (!this._hasData && this._audioSource)
			{
				this._audioSource.GetSpectrumData(this._samples, 0, FFTWindow.BlackmanHarris);
				this._hasData = true;
			}
			return this._samples;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00008DEB File Offset: 0x00006FEB
	public List<float> ProcessedSamples
	{
		get
		{
			if (!this._hasProcessedData)
			{
				this.ProcessSamples(this.Samples, this._processedSamples);
				this._hasProcessedData = true;
			}
			return this._processedSamples;
		}
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x00033EF8 File Offset: 0x000320F8
	protected void Awake()
	{
		this._hasData = false;
		this._hasProcessedData = false;
		for (int i = 0; i < 64; i++)
		{
			this._processedSamples.Add(0f);
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x00008E14 File Offset: 0x00007014
	protected void LateUpdate()
	{
		this._hasData = false;
		this._hasProcessedData = false;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x00033F30 File Offset: 0x00032130
	private void ProcessSamples(float[] sourceSamples, List<float> processedSamples)
	{
		float deltaTime = Time.deltaTime;
		for (int i = 0; i < sourceSamples.Length; i++)
		{
			float f = 10000f * sourceSamples[i];
			float num = 0.05f * Mathf.Log(Mathf.Abs(f) + 1f, 10f);
			if (processedSamples[i] < num)
			{
				processedSamples[i] = Mathf.Lerp(processedSamples[i], num, deltaTime * 16f);
			}
			else
			{
				processedSamples[i] = Mathf.Lerp(processedSamples[i], num, deltaTime * 4f);
			}
		}
	}

	// Token: 0x04000BE6 RID: 3046
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000BE7 RID: 3047
	public const int kNumberOfSamples = 64;

	// Token: 0x04000BE8 RID: 3048
	private bool _hasData;

	// Token: 0x04000BE9 RID: 3049
	private bool _hasProcessedData;

	// Token: 0x04000BEA RID: 3050
	private float[] _samples = new float[64];

	// Token: 0x04000BEB RID: 3051
	private List<float> _processedSamples = new List<float>(64);
}
