using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class NoteCutSoundEffect : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000168 RID: 360 RVA: 0x00017950 File Offset: 0x00015B50
	// (remove) Token: 0x06000169 RID: 361 RVA: 0x00017988 File Offset: 0x00015B88
	public event Action<NoteCutSoundEffect> didFinishEvent;

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600016A RID: 362 RVA: 0x0000326B File Offset: 0x0000146B
	public NoteData noteData
	{
		get
		{
			return this._noteData;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600016C RID: 364 RVA: 0x0000327C File Offset: 0x0000147C
	// (set) Token: 0x0600016B RID: 363 RVA: 0x00003273 File Offset: 0x00001473
	public float volumeMultiplier
	{
		get
		{
			return this._volumeMultiplier;
		}
		set
		{
			this._volumeMultiplier = value;
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00003284 File Offset: 0x00001484
	protected void Awake()
	{
		this._badCutRandomSoundPicker = new RandomObjectPicker<AudioClip>(this._badCutSoundEffectAudioClips, 0.01f);
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000329C File Offset: 0x0000149C
	protected void Start()
	{
		this._audioSource.loop = false;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000179C0 File Offset: 0x00015BC0
	public void Init(AudioClip audioClip, double noteDSPTime, float aheadTime, float missedTimeOffset, float timeToPrevNote, float timeToNextNote, Saber saber, NoteData noteData, bool handleWrongSaberTypeAsGood, float volumeMultiplier, bool ignoreSaberSpeed, bool ignoreBadCuts)
	{
		this._ignoreSaberSpeed = ignoreSaberSpeed;
		this._ignoreBadCuts = ignoreBadCuts;
		this._beforeCutVolume = 0f;
		this._volumeMultiplier = volumeMultiplier;
		base.enabled = true;
		this._audioSource.clip = audioClip;
		this._noteMissedTimeOffset = missedTimeOffset;
		this._aheadTime = aheadTime;
		this._timeToNextNote = timeToNextNote;
		this._timeToPrevNote = timeToPrevNote;
		this._saber = saber;
		this._noteData = noteData;
		this._handleWrongSaberTypeAsGood = handleWrongSaberTypeAsGood;
		this._noteWasCut = false;
		if (this._ignoreSaberSpeed)
		{
			this._beforeCutVolume = this._goodCutVolume;
			this._audioSource.volume = this._goodCutVolume;
		}
		else
		{
			this._beforeCutVolume = 0f;
			this._audioSource.volume = this._speedToVolumeCurve.Evaluate(saber.bladeSpeed);
		}
		this._audioSource.pitch = 1f;
		this._audioSource.priority = 128;
		base.transform.position = saber.saberBladeTopPos;
		this.ComputeDSPTimes(noteDSPTime, aheadTime, timeToPrevNote, timeToNextNote);
		this._audioSource.PlayScheduled(this._startDSPTime);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00017AE0 File Offset: 0x00015CE0
	private void ComputeDSPTimes(double noteDSPTime, float aheadTime, float timeToPrevNote, float timeToNextNote)
	{
		this._startDSPTime = noteDSPTime - (double)aheadTime;
		this._fadeOutStartDSPtime = noteDSPTime + (double)Mathf.Clamp(timeToNextNote - 0.01f + 100.01f, 0.01f, this._audioSource.clip.length - aheadTime);
		this._endDSPtime = this._fadeOutStartDSPtime + 0.009999999776482582;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00017B40 File Offset: 0x00015D40
	protected void LateUpdate()
	{
		if (this._paused)
		{
			return;
		}
		double dspTime = AudioSettings.dspTime;
		if (dspTime - this._endDSPtime > 0.0)
		{
			this.StopPlayingAndFinish();
			return;
		}
		if (!this._noteWasCut)
		{
			if (dspTime > this._startDSPTime + (double)this._aheadTime - 0.05000000074505806)
			{
				this._audioSource.priority = 32;
			}
			base.transform.position = this._saber.saberBladeTopPos;
			float num = this._goodCutVolume;
			if (!this._ignoreSaberSpeed)
			{
				num *= this._speedToVolumeCurve.Evaluate(this._saber.bladeSpeed) * (1f - Mathf.Clamp01((this._audioSource.time - this._aheadTime) / this._noteMissedTimeOffset));
			}
			if (num < this._beforeCutVolume)
			{
				this._beforeCutVolume = Mathf.Lerp(this._beforeCutVolume, num, Time.deltaTime * 4f);
			}
			else
			{
				this._beforeCutVolume = num;
			}
			this._audioSource.volume = this._beforeCutVolume * this._volumeMultiplier;
			return;
		}
		if (this._goodCut)
		{
			this._audioSource.volume = this._goodCutVolume * this._volumeMultiplier;
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x000032AA File Offset: 0x000014AA
	public void StopPlayingAndFinish()
	{
		base.enabled = false;
		this._audioSource.Stop();
		this._isPlaying = false;
		Action<NoteCutSoundEffect> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000032D6 File Offset: 0x000014D6
	public void PausePlaying()
	{
		this._paused = true;
		if (AudioSettings.dspTime >= this._startDSPTime)
		{
			this._audioSource.Pause();
			return;
		}
		this._audioSource.Stop();
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00017C74 File Offset: 0x00015E74
	public void ResumePlaying(double noteDSPTime)
	{
		this.ComputeDSPTimes(noteDSPTime, this._aheadTime, this._timeToPrevNote, this._timeToNextNote);
		this._paused = false;
		if (AudioSettings.dspTime < this._startDSPTime && !this._noteWasCut)
		{
			this._audioSource.PlayScheduled(this._startDSPTime);
			return;
		}
		this._audioSource.Play();
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00017CD4 File Offset: 0x00015ED4
	public void NoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.id != this._noteData.id)
		{
			return;
		}
		this._noteWasCut = true;
		if (!this._ignoreBadCuts && ((!this._handleWrongSaberTypeAsGood && !noteCutInfo.allIsOK) || (this._handleWrongSaberTypeAsGood && (!noteCutInfo.allExceptSaberTypeIsOK || noteCutInfo.saberTypeOK))))
		{
			this._audioSource.priority = 16;
			AudioClip clip = this._badCutRandomSoundPicker.PickRandomObject();
			this._audioSource.clip = clip;
			this._audioSource.time = 0f;
			this._audioSource.Play();
			this._goodCut = false;
			this._audioSource.volume = this._badCutVolume;
			this._endDSPtime = AudioSettings.dspTime + (double)this._audioSource.clip.length + 0.10000000149011612;
		}
		else
		{
			this._audioSource.priority = 24;
			this._audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.2f);
			this._goodCut = true;
			this._audioSource.volume = this._goodCutVolume;
		}
		base.transform.position = noteCutInfo.cutPoint;
	}

	// Token: 0x0400013F RID: 319
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000140 RID: 320
	[SerializeField]
	private AnimationCurve _speedToVolumeCurve;

	// Token: 0x04000141 RID: 321
	[SerializeField]
	private AudioClip[] _badCutSoundEffectAudioClips;

	// Token: 0x04000142 RID: 322
	[SerializeField]
	private float _badCutVolume = 1f;

	// Token: 0x04000143 RID: 323
	[SerializeField]
	private float _goodCutVolume = 1f;

	// Token: 0x04000145 RID: 325
	private bool _isPlaying;

	// Token: 0x04000146 RID: 326
	private Saber _saber;

	// Token: 0x04000147 RID: 327
	private NoteData _noteData;

	// Token: 0x04000148 RID: 328
	private float _volumeMultiplier;

	// Token: 0x04000149 RID: 329
	private bool _noteWasCut;

	// Token: 0x0400014A RID: 330
	private float _aheadTime;

	// Token: 0x0400014B RID: 331
	private float _timeToNextNote;

	// Token: 0x0400014C RID: 332
	private float _timeToPrevNote;

	// Token: 0x0400014D RID: 333
	private double _startDSPTime;

	// Token: 0x0400014E RID: 334
	private double _endDSPtime;

	// Token: 0x0400014F RID: 335
	private double _fadeOutStartDSPtime;

	// Token: 0x04000150 RID: 336
	private float _noteMissedTimeOffset;

	// Token: 0x04000151 RID: 337
	private float _beforeCutVolume;

	// Token: 0x04000152 RID: 338
	private bool _goodCut;

	// Token: 0x04000153 RID: 339
	private RandomObjectPicker<AudioClip> _badCutRandomSoundPicker;

	// Token: 0x04000154 RID: 340
	private bool _handleWrongSaberTypeAsGood;

	// Token: 0x04000155 RID: 341
	private bool _paused;

	// Token: 0x04000156 RID: 342
	private bool _ignoreSaberSpeed;

	// Token: 0x04000157 RID: 343
	private bool _ignoreBadCuts;

	// Token: 0x04000158 RID: 344
	private const float kEndOverlap = 100.01f;

	// Token: 0x04000159 RID: 345
	private const float kEndFadeLength = 0.01f;

	// Token: 0x02000055 RID: 85
	public class Pool : MemoryPoolWithActiveItems<NoteCutSoundEffect>
	{
	}
}
