using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class ObstacleSaberSoundEffect : MonoBehaviour
{
	// Token: 0x06000183 RID: 387 RVA: 0x0001832C File Offset: 0x0001652C
	protected void Awake()
	{
		base.enabled = false;
		this._audioSource.volume = 0f;
		this._obstacleSaberSparkleEffectManager.sparkleEffectDidStartEvent += this.HandleSparkleEffectDidStart;
		this._obstacleSaberSparkleEffectManager.sparkleEffectDidEndEvent += this.HandleSparkleEffecDidEnd;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x000033A6 File Offset: 0x000015A6
	protected void OnDestroy()
	{
		if (this._obstacleSaberSparkleEffectManager != null)
		{
			this._obstacleSaberSparkleEffectManager.sparkleEffectDidStartEvent -= this.HandleSparkleEffectDidStart;
			this._obstacleSaberSparkleEffectManager.sparkleEffectDidEndEvent -= this.HandleSparkleEffecDidEnd;
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00018380 File Offset: 0x00016580
	protected void LateUpdate()
	{
		this._audioSource.volume = Mathf.Lerp(this._audioSource.volume, this._targetVolume, Time.deltaTime * 8f);
		if (this._audioSource.volume <= 0.01f && this._audioSource.isPlaying)
		{
			base.enabled = false;
			this._audioSource.Stop();
		}
		base.transform.position = this._obstacleSaberSparkleEffectManager.BurnMarkPosForSaberType(this._saberType);
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00018408 File Offset: 0x00016608
	private void HandleSparkleEffectDidStart(SaberType saberType)
	{
		if (saberType != this._saberType)
		{
			return;
		}
		base.enabled = true;
		if (!this._audioSource.isPlaying)
		{
			this._audioSource.time = UnityEngine.Random.Range(0f, Mathf.Max(0f, this._audioSource.clip.length - 0.1f));
			this._audioSource.Play();
		}
		this._targetVolume = this._volume;
		this._audioSource.volume = this._volume;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000033E4 File Offset: 0x000015E4
	private void HandleSparkleEffecDidEnd(SaberType saberType)
	{
		if (saberType != this._saberType)
		{
			return;
		}
		this._targetVolume = 0f;
	}

	// Token: 0x04000174 RID: 372
	[SerializeField]
	private ObstacleSaberSparkleEffectManager _obstacleSaberSparkleEffectManager;

	// Token: 0x04000175 RID: 373
	[SerializeField]
	private SaberType _saberType;

	// Token: 0x04000176 RID: 374
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000177 RID: 375
	[SerializeField]
	private float _volume;

	// Token: 0x04000178 RID: 376
	private const float kSmooth = 8f;

	// Token: 0x04000179 RID: 377
	private float _targetVolume;
}
