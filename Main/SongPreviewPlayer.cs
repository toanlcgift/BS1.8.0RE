using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class SongPreviewPlayer : MonoBehaviour
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000190 RID: 400 RVA: 0x0000344A File Offset: 0x0000164A
	// (set) Token: 0x06000191 RID: 401 RVA: 0x00003452 File Offset: 0x00001652
	public float volume
	{
		get
		{
			return this._volume;
		}
		set
		{
			this._volume = value;
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00018520 File Offset: 0x00016720
	protected void OnEnable()
	{
		this._fadeSpeed = this._defaultCrossfadeSpeed;
		this._audioSources = new AudioSource[this._channelsCount];
		for (int i = 0; i < this._channelsCount; i++)
		{
			this._audioSources[i] = UnityEngine.Object.Instantiate<AudioSource>(this._audioSourcePrefab, base.transform);
			this._audioSources[i].volume = 0f;
			this._audioSources[i].loop = false;
			this._audioSources[i].reverbZoneMix = 0f;
			this._audioSources[i].playOnAwake = false;
		}
		this.CrossfadeTo(this._defaultAudioClip, Mathf.Max(UnityEngine.Random.Range(0f, this._defaultAudioClip.length - 0.1f), 0f), -1f, this._ambientVolumeScale);
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000185F0 File Offset: 0x000167F0
	protected void OnDisable()
	{
		if (this._audioSources != null)
		{
			foreach (AudioSource audioSource in this._audioSources)
			{
				if (audioSource != null)
				{
					UnityEngine.Object.Destroy(audioSource.gameObject);
				}
			}
			this._audioSources = null;
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0001863C File Offset: 0x0001683C
	protected void Update()
	{
		for (int i = 0; i < this._audioSources.Length; i++)
		{
			AudioSource audioSource = this._audioSources[i];
			float num = audioSource.volume;
			if (this._activeChannel == i)
			{
				if (num < this._volume * this._volumeScale)
				{
					audioSource.volume = Mathf.Min(this._volume * this._volumeScale, num + Time.deltaTime * this._fadeSpeed);
				}
				else if (num > this._volume * this._volumeScale)
				{
					audioSource.volume = this._volume * this._volumeScale;
				}
			}
			else if (num > 0f)
			{
				num -= Time.deltaTime * this._fadeSpeed;
				if (num <= 0f)
				{
					audioSource.volume = 0f;
					audioSource.Stop();
				}
				else
				{
					audioSource.volume = num;
				}
			}
		}
		if (this._transitionAfterDelay)
		{
			this._timeToDefaultAudioTransition -= Time.deltaTime;
			if (this._timeToDefaultAudioTransition <= 0f)
			{
				this.CrossfadeTo(this._defaultAudioClip, 0f, -1f, this._ambientVolumeScale);
			}
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00018754 File Offset: 0x00016954
	public void CrossfadeTo(AudioClip audioClip, float startTime, float duration, float volumeScale = 1f)
	{
		this._fadeSpeed = this._defaultCrossfadeSpeed;
		float num = this._volume;
		int num2 = 0;
		for (int i = 0; i < this._audioSources.Length; i++)
		{
			float volume = this._audioSources[i].volume;
			if (volume <= num)
			{
				num2 = i;
				num = volume;
			}
		}
		this._volumeScale = volumeScale;
		this._activeChannel = num2;
		AudioSource audioSource = this._audioSources[num2];
		audioSource.volume = 0f;
		audioSource.clip = audioClip;
		audioSource.time = startTime;
		this._timeToDefaultAudioTransition = Mathf.Max(0f, duration - 1f / this._fadeSpeed);
		this._transitionAfterDelay = (duration > 0f);
		audioSource.loop = !this._transitionAfterDelay;
		audioSource.Play();
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000345B File Offset: 0x0000165B
	public void FadeOut()
	{
		this._fadeSpeed = this._defaultFadeOutSpeed;
		this._transitionAfterDelay = false;
		this._activeChannel = -1;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00018810 File Offset: 0x00016A10
	public void CrossfadeToDefault()
	{
		if (!this._transitionAfterDelay && this._activeChannel > 0 && this._audioSources[this._activeChannel].clip == this._defaultAudioClip)
		{
			return;
		}
		this.CrossfadeTo(this._defaultAudioClip, Mathf.Max(UnityEngine.Random.Range(0f, this._defaultAudioClip.length - 0.1f), 0f), -1f, this._ambientVolumeScale);
	}

	// Token: 0x04000181 RID: 385
	[Tooltip("Minimum 2, maximum 6")]
	[SerializeField]
	private int _channelsCount = 3;

	// Token: 0x04000182 RID: 386
	[SerializeField]
	private AudioSource _audioSourcePrefab;

	// Token: 0x04000183 RID: 387
	[SerializeField]
	private AudioClip _defaultAudioClip;

	// Token: 0x04000184 RID: 388
	[SerializeField]
	private float _volume = 1f;

	// Token: 0x04000185 RID: 389
	[SerializeField]
	private float _ambientVolumeScale = 1f;

	// Token: 0x04000186 RID: 390
	[SerializeField]
	private float _defaultCrossfadeSpeed = 1f;

	// Token: 0x04000187 RID: 391
	[SerializeField]
	private float _defaultFadeOutSpeed = 2f;

	// Token: 0x04000188 RID: 392
	private AudioSource[] _audioSources;

	// Token: 0x04000189 RID: 393
	private int _activeChannel;

	// Token: 0x0400018A RID: 394
	private float _timeToDefaultAudioTransition;

	// Token: 0x0400018B RID: 395
	private bool _transitionAfterDelay;

	// Token: 0x0400018C RID: 396
	private float _volumeScale;

	// Token: 0x0400018D RID: 397
	private float _fadeSpeed;
}
