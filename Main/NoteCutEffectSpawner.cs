using System;
using UnityEngine;
using Zenject;

// Token: 0x02000262 RID: 610
public class NoteCutEffectSpawner : MonoBehaviour
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x00007FFD File Offset: 0x000061FD
	protected void Start()
	{
		this._beatmapObjectManager.noteWasCutEvent += this.HandleNoteWasCutEvent;
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00008016 File Offset: 0x00006216
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager != null)
		{
			this._beatmapObjectManager.noteWasCutEvent -= this.HandleNoteWasCutEvent;
		}
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x00030538 File Offset: 0x0002E738
	private void HandleNoteWasCutEvent(INoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			this.SpawnBombCutEffect(noteCutInfo.cutPoint, noteController, noteCutInfo);
		}
		else if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			this.SpawnNoteCutEffect(noteCutInfo.cutPoint, noteController, noteCutInfo);
		}
		this._noteCutHapticEffect.HitNote(noteCutInfo.saberType);
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x000305A0 File Offset: 0x0002E7A0
	private void SpawnNoteCutEffect(Vector3 pos, INoteController noteController, NoteCutInfo noteCutInfo)
	{
		Vector3 moveVec = noteController.worldRotation * noteController.jumpMoveVec;
		if (noteCutInfo.allIsOK)
		{
			NoteData noteData = noteController.noteData;
			Color c = this._colorManager.ColorForNoteType(noteData.noteType).ColorWithAlpha(0.5f);
			this._noteCutParticlesEffect.SpawnParticles(pos, noteCutInfo.cutNormal, noteCutInfo.saberDir, moveVec, c, 150, 50, Mathf.Clamp(noteData.timeToNextBasicNote, 0.4f, 1f), noteCutInfo.saberType);
			if (this._initData.spawnScores)
			{
				int multiplierWithFever = this._scoreController.multiplierWithFever;
				this._flyingScoreSpawner.SpawnFlyingScore(noteCutInfo, noteData.lineIndex, multiplierWithFever, pos, noteController.worldRotation, noteController.inverseWorldRotation, new Color(0.8f, 0.8f, 0.8f));
			}
			Vector3 pos2 = pos;
			pos2.y = this._shockWaveYPos;
			this._shockwaveEffect.SpawnShockwave(pos2);
		}
		else if (this._initData.spawnBadCuts)
		{
			this._failFlyingSpriteSpawner.SpawnFlyingSprite(pos, noteController.worldRotation, noteController.inverseWorldRotation);
		}
		this._noteDebrisSpawner.SpawnDebris(noteCutInfo, noteController, moveVec);
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x000306D0 File Offset: 0x0002E8D0
	private void SpawnBombCutEffect(Vector3 pos, INoteController noteController, NoteCutInfo noteCutInfo)
	{
		this._bombExplosionEffect.SpawnExplosion(pos);
		this._failFlyingSpriteSpawner.SpawnFlyingSprite(pos, noteController.worldRotation, noteController.inverseWorldRotation);
		Vector3 pos2 = pos;
		pos2.y = this._shockWaveYPos;
		this._shockwaveEffect.SpawnShockwave(pos2);
	}

	// Token: 0x04000A98 RID: 2712
	[SerializeField]
	private float _shockWaveYPos = 0.1f;

	// Token: 0x04000A99 RID: 2713
	[Space]
	[SerializeField]
	private NoteCutParticlesEffect _noteCutParticlesEffect;

	// Token: 0x04000A9A RID: 2714
	[SerializeField]
	private NoteDebrisSpawner _noteDebrisSpawner;

	// Token: 0x04000A9B RID: 2715
	[SerializeField]
	private NoteCutHapticEffect _noteCutHapticEffect;

	// Token: 0x04000A9C RID: 2716
	[SerializeField]
	private FlyingSpriteSpawner _failFlyingSpriteSpawner;

	// Token: 0x04000A9D RID: 2717
	[SerializeField]
	private FlyingScoreSpawner _flyingScoreSpawner;

	// Token: 0x04000A9E RID: 2718
	[SerializeField]
	private ShockwaveEffect _shockwaveEffect;

	// Token: 0x04000A9F RID: 2719
	[SerializeField]
	private BombExplosionEffect _bombExplosionEffect;

	// Token: 0x04000AA0 RID: 2720
	[Inject]
	private NoteCutEffectSpawner.InitData _initData;

	// Token: 0x04000AA1 RID: 2721
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000AA2 RID: 2722
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000AA3 RID: 2723
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x02000263 RID: 611
	public class InitData
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x00008050 File Offset: 0x00006250
		public InitData(bool spawnScores, bool spawnBadCuts)
		{
			this.spawnScores = spawnScores;
			this.spawnBadCuts = spawnBadCuts;
		}

		// Token: 0x04000AA4 RID: 2724
		public readonly bool spawnScores;

		// Token: 0x04000AA5 RID: 2725
		public readonly bool spawnBadCuts;
	}
}
