using System;
using UnityEngine;
using Zenject;

// Token: 0x02000321 RID: 801
public class TutorialNoteCutEffectSpawner : MonoBehaviour
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0000AD04 File Offset: 0x00008F04
	// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0000AD0C File Offset: 0x00008F0C
	public bool handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts { get; set; }

	// Token: 0x06000E04 RID: 3588 RVA: 0x0000AD15 File Offset: 0x00008F15
	protected void Start()
	{
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCut;
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissed;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x0000AD45 File Offset: 0x00008F45
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCut;
			this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissed;
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0003A0C8 File Offset: 0x000382C8
	private void HandleNoteWasCut(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		Vector3 position = noteController.noteTransform.position;
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			this.SpawnBombCutEffect(position, noteController, noteCutInfo);
		}
		else if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			this.SpawnNoteCutEffect(position, noteController, noteCutInfo);
		}
		this._noteCutHapticEffect.HitNote(noteCutInfo.saberType);
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0003A130 File Offset: 0x00038330
	private void SpawnNoteCutEffect(Vector3 pos, INoteController noteController, NoteCutInfo noteCutInfo)
	{
		Vector3 moveVec = noteController.worldRotation * noteController.jumpMoveVec;
		if ((!this.handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts && noteCutInfo.allIsOK) || (this.handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts && noteCutInfo.allExceptSaberTypeIsOK && !noteCutInfo.saberTypeOK))
		{
			Color c = this._colorManager.ColorForNoteType(noteController.noteData.noteType).ColorWithAlpha(0.5f);
			this._noteCutParticlesEffect.SpawnParticles(pos, noteCutInfo.cutNormal, noteCutInfo.saberDir, moveVec, c, 150, 50, 1f, noteCutInfo.saberType);
			Vector3 pos2 = pos;
			pos2.y = 0.01f;
			this._shockwaveEffect.SpawnShockwave(pos2);
		}
		else if (!this.handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts)
		{
			string failText = noteCutInfo.FailText;
			if (failText != "")
			{
				this._failFlyingTextSpawner.SpawnText(pos, noteController.worldRotation, noteController.inverseWorldRotation, failText);
			}
		}
		this._noteDebrisSpawner.SpawnDebris(noteCutInfo, noteController, moveVec);
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0003A228 File Offset: 0x00038428
	private void SpawnBombCutEffect(Vector3 pos, INoteController noteController, NoteCutInfo noteCutInfo)
	{
		this._bombExplosionEffect.SpawnExplosion(pos);
		this._failFlyingTextSpawner.SpawnText(pos, noteController.worldRotation, noteController.inverseWorldRotation, "Do not cut!");
		Vector3 pos2 = pos;
		pos2.y = 0.01f;
		this._shockwaveEffect.SpawnShockwave(pos2);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0000AD82 File Offset: 0x00008F82
	private void HandleNoteWasMissed(INoteController noteController)
	{
		Vector3 position = noteController.noteTransform.position;
		NoteType noteType = noteController.noteData.noteType;
	}

	// Token: 0x04000E5A RID: 3674
	[SerializeField]
	private NoteCutParticlesEffect _noteCutParticlesEffect;

	// Token: 0x04000E5B RID: 3675
	[SerializeField]
	private ShockwaveEffect _shockwaveEffect;

	// Token: 0x04000E5C RID: 3676
	[SerializeField]
	private NoteDebrisSpawner _noteDebrisSpawner;

	// Token: 0x04000E5D RID: 3677
	[SerializeField]
	private NoteCutHapticEffect _noteCutHapticEffect;

	// Token: 0x04000E5E RID: 3678
	[SerializeField]
	private FlyingTextSpawner _failFlyingTextSpawner;

	// Token: 0x04000E5F RID: 3679
	[SerializeField]
	private BombExplosionEffect _bombExplosionEffect;

	// Token: 0x04000E60 RID: 3680
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000E61 RID: 3681
	[Inject]
	private ColorManager _colorManager;
}
