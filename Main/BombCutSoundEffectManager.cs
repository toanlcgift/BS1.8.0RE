using System;
using UnityEngine;
using Zenject;

// Token: 0x0200004F RID: 79
public class BombCutSoundEffectManager : MonoBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x00003119 File Offset: 0x00001319
	protected void Start()
	{
		this._randomSoundPicker = new RandomObjectPicker<AudioClip>(this._bombExplosionAudioClips, 0f);
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCut;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0001785C File Offset: 0x00015A5C
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.noteType != NoteType.Bomb)
		{
			return;
		}
		BombCutSoundEffect bombCutSoundEffect = this._bombCutSoundEffectPool.Spawn();
		bombCutSoundEffect.transform.SetPositionAndRotation(base.transform.localPosition, Quaternion.identity);
		bombCutSoundEffect.didFinishEvent += this.HandleBombCutSoundEffectDidFinish;
		Saber saber = this._playerController.SaberForType(noteCutInfo.saberType);
		bombCutSoundEffect.Init(this._randomSoundPicker.PickRandomObject(), saber, this._volume);
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00003148 File Offset: 0x00001348
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000316E File Offset: 0x0000136E
	private void HandleBombCutSoundEffectDidFinish(BombCutSoundEffect bombCutSoundEffect)
	{
		bombCutSoundEffect.didFinishEvent -= this.HandleBombCutSoundEffectDidFinish;
		this._bombCutSoundEffectPool.Despawn(bombCutSoundEffect);
	}

	// Token: 0x04000131 RID: 305
	[SerializeField]
	private float _volume = 0.3f;

	// Token: 0x04000132 RID: 306
	[SerializeField]
	private AudioClip[] _bombExplosionAudioClips;

	// Token: 0x04000133 RID: 307
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000134 RID: 308
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000135 RID: 309
	[Inject]
	private BombCutSoundEffect.Pool _bombCutSoundEffectPool;

	// Token: 0x04000136 RID: 310
	private RandomObjectPicker<AudioClip> _randomSoundPicker;
}
