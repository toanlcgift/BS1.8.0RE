using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class AudioFading : MonoBehaviour
{
	// Token: 0x06000115 RID: 277 RVA: 0x00002CF3 File Offset: 0x00000EF3
	protected void Start()
	{
		if (this._fadeInOnStart)
		{
			this._audioSource.volume = 0f;
			this.FadeIn();
			return;
		}
		base.enabled = false;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00016FC8 File Offset: 0x000151C8
	protected void Update()
	{
		if (Mathf.Abs(this._targetVolume - this._audioSource.volume) < 0.001f)
		{
			this._audioSource.volume = this._targetVolume;
			base.enabled = false;
			return;
		}
		this._audioSource.volume = Mathf.Lerp(this._audioSource.volume, this._targetVolume, Time.deltaTime * this._smooth);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00002D1B File Offset: 0x00000F1B
	public void FadeOut()
	{
		base.enabled = true;
		this._targetVolume = 0f;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00002D2F File Offset: 0x00000F2F
	public void FadeIn()
	{
		base.enabled = true;
		this._targetVolume = 1f;
	}

	// Token: 0x040000E5 RID: 229
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	private float _smooth = 4f;

	// Token: 0x040000E7 RID: 231
	[SerializeField]
	private bool _fadeInOnStart;

	// Token: 0x040000E8 RID: 232
	private float _targetVolume;
}
