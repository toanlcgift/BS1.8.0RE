using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000056 RID: 86
public class NoteCutSoundEffectManager : MonoBehaviour
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000178 RID: 376 RVA: 0x00003329 File Offset: 0x00001529
	// (set) Token: 0x06000179 RID: 377 RVA: 0x00003331 File Offset: 0x00001531
	public bool handleWrongSaberTypeAsGood { get; set; }

	// Token: 0x0600017A RID: 378 RVA: 0x00017E0C File Offset: 0x0001600C
	protected void Start()
	{
		this._useTestAudioClips = this._initData.useTestAudioClips;
		if (this._initData.useTestAudioClips)
		{
			this._randomLongCutSoundPicker = new RandomObjectPicker<AudioClip>(this._testAudioClip, 0f);
			this._randomShortCutSoundPicker = new RandomObjectPicker<AudioClip>(this._testAudioClip, 0f);
		}
		else
		{
			this._randomLongCutSoundPicker = new RandomObjectPicker<AudioClip>(this._longCutEffectsAudioClips, 0f);
			this._randomShortCutSoundPicker = new RandomObjectPicker<AudioClip>(this._shortCutEffectsAudioClips, 0f);
		}
		this._beatAlignOffset = this._audioSamplesBeatAlignOffset + this._audioManager.sfxLatency;
		this._beatmapObjectCallbackData = this._beatmapObjectCallbackController.AddBeatmapObjectCallback(new BeatmapObjectCallbackController.BeatmapObjectCallback(this.BeatmapObjectCallback), this._beatAlignOffset + 0.5f);
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCut;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00017EEC File Offset: 0x000160EC
	protected void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.RemoveBeatmapObjectCallback(this._beatmapObjectCallbackData);
		}
		if (this._beatmapObjectManager)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00017F3C File Offset: 0x0001613C
	private void BeatmapObjectCallback(BeatmapObjectData beatmapObjectData)
	{
		if (beatmapObjectData.beatmapObjectType != BeatmapObjectType.Note)
		{
			return;
		}
		NoteData noteData = (NoteData)beatmapObjectData;
		float timeScale = this._audioTimeSyncController.timeScale;
		if (noteData.noteType != NoteType.NoteA && noteData.noteType != NoteType.NoteB)
		{
			return;
		}
		if ((noteData.noteType == NoteType.NoteA && noteData.time < this._prevNoteATime + 0.001f) || (noteData.noteType == NoteType.NoteB && noteData.time < this._prevNoteBTime + 0.001f))
		{
			return;
		}
		bool flag = false;
		if (noteData.time < this._prevNoteATime + 0.001f || noteData.time < this._prevNoteBTime + 0.001f)
		{
			if (noteData.noteType == NoteType.NoteA && this._prevNoteBSoundEffect.enabled)
			{
				this._prevNoteBSoundEffect.volumeMultiplier = 0.9f;
				flag = true;
			}
			else if (noteData.noteType == NoteType.NoteB && this._prevNoteASoundEffect.enabled)
			{
				this._prevNoteASoundEffect.volumeMultiplier = 0.9f;
				flag = true;
			}
		}
		NoteCutSoundEffect noteCutSoundEffect = this._noteCutSoundEffectPool.Spawn();
		noteCutSoundEffect.transform.SetPositionAndRotation(base.transform.localPosition, Quaternion.identity);
		noteCutSoundEffect.didFinishEvent += this.HandleCutSoundEffectDidFinish;
		Saber saber = null;
		if (noteData.noteType == NoteType.NoteA)
		{
			this._prevNoteATime = noteData.time;
			saber = this._playerController.leftSaber;
			this._prevNoteASoundEffect = noteCutSoundEffect;
		}
		else if (noteData.noteType == NoteType.NoteB)
		{
			this._prevNoteBTime = noteData.time;
			saber = this._playerController.rightSaber;
			this._prevNoteBSoundEffect = noteCutSoundEffect;
		}
		if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			bool flag2 = noteData.timeToPrevBasicNote < this._beatAlignOffset;
			AudioClip audioClip = flag2 ? this._randomShortCutSoundPicker.PickRandomObject() : this._randomLongCutSoundPicker.PickRandomObject();
			float volumeMultiplier = 1f;
			if (flag)
			{
				volumeMultiplier = 0.9f;
			}
			else if (flag2)
			{
				volumeMultiplier = 0.9f;
			}
			noteCutSoundEffect.Init(audioClip, (double)(noteData.time / timeScale) + this._audioTimeSyncController.dspTimeOffset, this._beatAlignOffset, 0.15f, noteData.timeToPrevBasicNote / timeScale, noteData.timeToNextBasicNote / timeScale, saber, noteData, this.handleWrongSaberTypeAsGood, volumeMultiplier, this._useTestAudioClips, this._initData.ignoreBadCuts);
		}
		HashSet<NoteCutSoundEffect> activeItems = this._noteCutSoundEffectPool.activeItems;
		NoteCutSoundEffect noteCutSoundEffect2 = null;
		float num = float.MaxValue;
		foreach (NoteCutSoundEffect noteCutSoundEffect3 in activeItems)
		{
			if (noteCutSoundEffect3.noteData.time < num)
			{
				num = noteCutSoundEffect3.noteData.time;
				noteCutSoundEffect2 = noteCutSoundEffect3;
			}
		}
		if (activeItems.Count > 64)
		{
			noteCutSoundEffect2.StopPlayingAndFinish();
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000181F8 File Offset: 0x000163F8
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (this._useTestAudioClips)
		{
			return;
		}
		foreach (NoteCutSoundEffect noteCutSoundEffect in this._noteCutSoundEffectPool.activeItems)
		{
			noteCutSoundEffect.NoteWasCut(noteController, noteCutInfo);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000333A File Offset: 0x0000153A
	private void HandleCutSoundEffectDidFinish(NoteCutSoundEffect cutSoundEffect)
	{
		cutSoundEffect.didFinishEvent -= this.HandleCutSoundEffectDidFinish;
		this._noteCutSoundEffectPool.Despawn(cutSoundEffect);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00018258 File Offset: 0x00016458
	public void Pause()
	{
		foreach (NoteCutSoundEffect noteCutSoundEffect in this._noteCutSoundEffectPool.activeItems)
		{
			noteCutSoundEffect.PausePlaying();
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x000182B0 File Offset: 0x000164B0
	public void Resume()
	{
		float timeScale = this._audioTimeSyncController.timeScale;
		foreach (NoteCutSoundEffect noteCutSoundEffect in this._noteCutSoundEffectPool.activeItems)
		{
			noteCutSoundEffect.ResumePlaying((double)(noteCutSoundEffect.noteData.time / timeScale) + this._audioTimeSyncController.dspTimeOffset);
		}
	}

	// Token: 0x0400015A RID: 346
	[SerializeField]
	private AudioManagerSO _audioManager;

	// Token: 0x0400015B RID: 347
	[Space]
	[SerializeField]
	private float _audioSamplesBeatAlignOffset = 0.185f;

	// Token: 0x0400015C RID: 348
	[SerializeField]
	private AudioClip[] _longCutEffectsAudioClips;

	// Token: 0x0400015D RID: 349
	[SerializeField]
	private AudioClip[] _shortCutEffectsAudioClips;

	// Token: 0x0400015E RID: 350
	[Space]
	[SerializeField]
	private AudioClip _testAudioClip;

	// Token: 0x0400015F RID: 351
	[InjectOptional]
	private NoteCutSoundEffectManager.InitData _initData = new NoteCutSoundEffectManager.InitData(false, false);

	// Token: 0x04000160 RID: 352
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000161 RID: 353
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000162 RID: 354
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000163 RID: 355
	[Inject]
	private NoteCutSoundEffect.Pool _noteCutSoundEffectPool;

	// Token: 0x04000164 RID: 356
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x04000166 RID: 358
	private const int kMaxNumberOfEffects = 64;

	// Token: 0x04000167 RID: 359
	private const float kTwoNotesAtTheSameTimeVolumeMul = 0.9f;

	// Token: 0x04000168 RID: 360
	private const float kDenseNotesVolumeMul = 0.9f;

	// Token: 0x04000169 RID: 361
	private BeatmapObjectCallbackController.BeatmapObjectCallbackData _beatmapObjectCallbackData;

	// Token: 0x0400016A RID: 362
	private RandomObjectPicker<AudioClip> _randomLongCutSoundPicker;

	// Token: 0x0400016B RID: 363
	private RandomObjectPicker<AudioClip> _randomShortCutSoundPicker;

	// Token: 0x0400016C RID: 364
	private float _prevNoteATime = -1f;

	// Token: 0x0400016D RID: 365
	private float _prevNoteBTime = -1f;

	// Token: 0x0400016E RID: 366
	private NoteCutSoundEffect _prevNoteASoundEffect;

	// Token: 0x0400016F RID: 367
	private NoteCutSoundEffect _prevNoteBSoundEffect;

	// Token: 0x04000170 RID: 368
	private float _beatAlignOffset;

	// Token: 0x04000171 RID: 369
	private bool _useTestAudioClips;

	// Token: 0x02000057 RID: 87
	public class InitData
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00003390 File Offset: 0x00001590
		public InitData(bool useTestAudioClips, bool ignoreBadCuts)
		{
			this.useTestAudioClips = useTestAudioClips;
			this.ignoreBadCuts = ignoreBadCuts;
		}

		// Token: 0x04000172 RID: 370
		public readonly bool useTestAudioClips;

		// Token: 0x04000173 RID: 371
		public readonly bool ignoreBadCuts;
	}
}
