using System;
using UnityEngine;
using Zenject;

// Token: 0x02000047 RID: 71
public class AudioTimeSyncController : MonoBehaviour, IAudioTimeSource
{
	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600012B RID: 299 RVA: 0x00002E5C File Offset: 0x0000105C
	public AudioTimeSyncController.State state
	{
		get
		{
			return this._state;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600012C RID: 300 RVA: 0x00002E64 File Offset: 0x00001064
	public float songTime
	{
		get
		{
			return this._songTime;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x0600012D RID: 301 RVA: 0x00002E6C File Offset: 0x0000106C
	public float songLength
	{
		get
		{
			return this._initData.audioClip.length;
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x0600012E RID: 302 RVA: 0x00002E7E File Offset: 0x0000107E
	public bool isAudioLoaded
	{
		get
		{
			return this._initData.audioClip.loadState == AudioDataLoadState.Loaded;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600012F RID: 303 RVA: 0x00002E93 File Offset: 0x00001093
	public float songEndTime
	{
		get
		{
			return this.songLength - this._songTimeOffset;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000130 RID: 304 RVA: 0x00002EA2 File Offset: 0x000010A2
	public float timeScale
	{
		get
		{
			return this._timeScale;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000131 RID: 305 RVA: 0x00002EAA File Offset: 0x000010AA
	public double dspTimeOffset
	{
		get
		{
			return this._dspTimeOffset;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000132 RID: 306 RVA: 0x00002EB2 File Offset: 0x000010B2
	public WaitUntil waitUntilAudioIsLoaded
	{
		get
		{
			return new WaitUntil(() => this.isAudioLoaded);
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000133 RID: 307 RVA: 0x00002EC5 File Offset: 0x000010C5
	public AudioSource audioSource
	{
		get
		{
			return this._audioSource;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00002ECD File Offset: 0x000010CD
	protected void Awake()
	{
		this._songTime = 0f;
		this._audioSource.Stop();
		this._audioSource.clip = null;
		this._state = AudioTimeSyncController.State.Stopped;
		this._canStartSong = false;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0001721C File Offset: 0x0001541C
	protected void Start()
	{
		this._timeScale = this._initData.timeScale;
		this._audioSource.clip = this._initData.audioClip;
		this._audioSource.pitch = this._initData.timeScale;
		this._startSongTime = this._initData.startSongTime;
		this._songTimeOffset = this._initData.songTimeOffset + this._audioLatency;
		AudioClip audioClip = this._initData.audioClip;
		if (audioClip != null)
		{
			audioClip.LoadAudioData();
		}
		this._canStartSong = true;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000172B4 File Offset: 0x000154B4
	protected void Update()
	{
		if (this._state == AudioTimeSyncController.State.Stopped)
		{
			return;
		}
		float num = Time.deltaTime * this._timeScale;
		if (Time.captureFramerate != 0)
		{
			this._songTime += num;
			this._songTime = Mathf.Min(this._songTime, this._audioSource.clip.length - 0.01f);
			this._audioSource.time = this._songTime;
			this._dspTimeOffset = AudioSettings.dspTime - (double)this._songTime;
			return;
		}
		if (this.timeSinceStart < this._audioStartTimeOffsetSinceStart)
		{
			this._songTime += num;
			return;
		}
		if (!this._audioStarted)
		{
			this._audioStarted = true;
			this._audioSource.Play();
		}
		if (this._audioSource.clip == null || !this._audioSource.isPlaying)
		{
			return;
		}
		int timeSamples = this._audioSource.timeSamples;
		float num2 = this._audioSource.time;
		float num3 = this.timeSinceStart - this._audioStartTimeOffsetSinceStart;
		if (this._prevAudioSamplePos > timeSamples)
		{
			this._playbackLoopIndex++;
		}
		this._prevAudioSamplePos = timeSamples;
		num2 += (float)this._playbackLoopIndex * this._audioSource.clip.length / this._timeScale;
		this._dspTimeOffset = AudioSettings.dspTime - (double)(num2 / this._timeScale);
		float num4 = Mathf.Abs(num3 - num2);
		if ((num4 > this._forcedSyncDeltaTime || this._state == AudioTimeSyncController.State.Paused) && !this.forcedNoAudioSync)
		{
			this._audioStartTimeOffsetSinceStart = this.timeSinceStart - num2;
			num3 = num2;
		}
		else
		{
			if (this._fixingAudioSyncError)
			{
				if (num4 < this._stopSyncDeltaTime)
				{
					this._fixingAudioSyncError = false;
				}
			}
			else if (num4 > this._startSyncDeltaTime)
			{
				this._fixingAudioSyncError = true;
			}
			if (this._fixingAudioSyncError)
			{
				this._audioStartTimeOffsetSinceStart = Mathf.Lerp(this._audioStartTimeOffsetSinceStart, this.timeSinceStart - num2, num * this._audioSyncLerpSpeed);
			}
		}
		this._songTime = num3 - this._songTimeOffset;
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000137 RID: 311 RVA: 0x00002EFF File Offset: 0x000010FF
	private float timeSinceStart
	{
		get
		{
			return Time.timeSinceLevelLoad * this._timeScale;
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000174A8 File Offset: 0x000156A8
	public void StartSong()
	{
		this._timeScale = this._initData.timeScale;
		this._audioSource.clip = this._initData.audioClip;
		this._audioSource.pitch = this._initData.timeScale;
		this._startSongTime = this._initData.startSongTime;
		this._songTimeOffset = this._initData.songTimeOffset + this._audioLatency;
		this._songTime = this._startSongTime;
		float num = this._startSongTime + this._songTimeOffset;
		if (num >= 0f)
		{
			this._audioSource.time = num;
			this._audioSource.Play();
			this._audioStarted = true;
		}
		else
		{
			this._audioSource.time = 0f;
			this._audioStarted = false;
		}
		this._audioStartTimeOffsetSinceStart = this.timeSinceStart - num;
		this._fixingAudioSyncError = false;
		this._prevAudioSamplePos = (int)((float)this._audioSource.clip.frequency * num);
		this._playbackLoopIndex = 0;
		this._dspTimeOffset = AudioSettings.dspTime - (double)num;
		this._songTime = this._startSongTime;
		this._state = AudioTimeSyncController.State.Playing;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00002F0D File Offset: 0x0000110D
	public void StopSong()
	{
		this._audioSource.Stop();
		this._state = AudioTimeSyncController.State.Stopped;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00002F21 File Offset: 0x00001121
	public void Pause()
	{
		if (this._state == AudioTimeSyncController.State.Paused || this._state == AudioTimeSyncController.State.Stopped)
		{
			return;
		}
		this._audioSource.Pause();
		this._state = AudioTimeSyncController.State.Paused;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00002F48 File Offset: 0x00001148
	public void Resume()
	{
		if (this._state != AudioTimeSyncController.State.Paused)
		{
			return;
		}
		this._state = AudioTimeSyncController.State.Playing;
		this._audioSource.UnPause();
	}

	// Token: 0x040000FF RID: 255
	[SerializeField]
	private float _audioSyncLerpSpeed = 1f;

	// Token: 0x04000100 RID: 256
	[SerializeField]
	private float _forcedSyncDeltaTime = 0.03f;

	// Token: 0x04000101 RID: 257
	[SerializeField]
	private float _startSyncDeltaTime = 0.02f;

	// Token: 0x04000102 RID: 258
	[SerializeField]
	private float _stopSyncDeltaTime = 0.01f;

	// Token: 0x04000103 RID: 259
	[Space]
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000104 RID: 260
	[Space]
	[SerializeField]
	private FloatSO _audioLatency;

	// Token: 0x04000105 RID: 261
	[Inject]
	private AudioTimeSyncController.InitData _initData;

	// Token: 0x04000106 RID: 262
	[NonSerialized]
	public bool forcedNoAudioSync;

	// Token: 0x04000107 RID: 263
	private bool _fixingAudioSyncError;

	// Token: 0x04000108 RID: 264
	private float _audioStartTimeOffsetSinceStart;

	// Token: 0x04000109 RID: 265
	private int _playbackLoopIndex;

	// Token: 0x0400010A RID: 266
	private int _prevAudioSamplePos;

	// Token: 0x0400010B RID: 267
	private float _startSongTime;

	// Token: 0x0400010C RID: 268
	private float _songTimeOffset;

	// Token: 0x0400010D RID: 269
	private bool _audioStarted;

	// Token: 0x0400010E RID: 270
	private float _timeScale;

	// Token: 0x0400010F RID: 271
	private float _songTime;

	// Token: 0x04000110 RID: 272
	private double _dspTimeOffset;

	// Token: 0x04000111 RID: 273
	private AudioTimeSyncController.State _state;

	// Token: 0x04000112 RID: 274
	private bool _canStartSong;

	// Token: 0x02000048 RID: 72
	public class InitData
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00002FA2 File Offset: 0x000011A2
		public InitData(AudioClip audioClip, float startSongTime, float songTimeOffset, float timeScale)
		{
			this.audioClip = audioClip;
			this.startSongTime = startSongTime;
			this.songTimeOffset = songTimeOffset;
			this.timeScale = timeScale;
		}

		// Token: 0x04000113 RID: 275
		public readonly AudioClip audioClip;

		// Token: 0x04000114 RID: 276
		public readonly float startSongTime;

		// Token: 0x04000115 RID: 277
		public readonly float songTimeOffset;

		// Token: 0x04000116 RID: 278
		public readonly float timeScale;
	}

	// Token: 0x02000049 RID: 73
	public enum State
	{
		// Token: 0x04000118 RID: 280
		Playing,
		// Token: 0x04000119 RID: 281
		Paused,
		// Token: 0x0400011A RID: 282
		Stopped
	}
}
