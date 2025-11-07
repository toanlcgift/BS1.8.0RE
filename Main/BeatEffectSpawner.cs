using System;
using UnityEngine;
using Zenject;

// Token: 0x0200025A RID: 602
public class BeatEffectSpawner : MonoBehaviour
{
	// Token: 0x06000A2C RID: 2604 RVA: 0x00007EA9 File Offset: 0x000060A9
	protected void Start()
	{
		this._beatmapObjectManager.noteDidStartJumpEvent += this.HandleNoteDidStartJumpEvent;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00007EC2 File Offset: 0x000060C2
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager)
		{
			this._beatmapObjectManager.noteDidStartJumpEvent -= this.HandleNoteDidStartJumpEvent;
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000301A0 File Offset: 0x0002E3A0
	private void HandleNoteDidStartJumpEvent(NoteController noteController)
	{
		NoteData noteData = noteController.noteData;
		BeatEffect beatEffect = this._beatEffectPool.Spawn();
		beatEffect.didFinishEvent += this.HandleBeatEffectDidFinish;
		beatEffect.transform.SetPositionAndRotation(noteController.noteTransform.position - new Vector3(0f, 0.15f, 0f), Quaternion.identity);
		beatEffect.Init(this._colorManager.ColorForNoteType(noteData.noteType), this._effectDuration, noteController.transform.localRotation);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00007EE8 File Offset: 0x000060E8
	private void HandleBeatEffectDidFinish(BeatEffect beatEffect)
	{
		beatEffect.didFinishEvent -= this.HandleBeatEffectDidFinish;
		this._beatEffectPool.Despawn(beatEffect);
	}

	// Token: 0x04000A77 RID: 2679
	[SerializeField]
	private float _effectDuration = 1f;

	// Token: 0x04000A78 RID: 2680
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000A79 RID: 2681
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000A7A RID: 2682
	[Inject]
	private BeatEffect.Pool _beatEffectPool;

	// Token: 0x04000A7B RID: 2683
	private SongController _songController;
}
