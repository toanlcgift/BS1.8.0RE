using System;
using UnityEngine;
using Zenject;

// Token: 0x02000260 RID: 608
public class MissedNoteEffectSpawner : MonoBehaviour
{
	// Token: 0x06000A3B RID: 2619 RVA: 0x00007F99 File Offset: 0x00006199
	protected void Start()
	{
		this._beatmapObjectManager.noteWasMissedEvent += this.HandleNoteWasMissed;
		this._spawnPosZ = base.transform.position.z;
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00007FC8 File Offset: 0x000061C8
	protected void OnDestroy()
	{
		if (this._beatmapObjectManager)
		{
			this._beatmapObjectManager.noteWasMissedEvent -= this.HandleNoteWasMissed;
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x000304B8 File Offset: 0x0002E6B8
	private void HandleNoteWasMissed(INoteController noteController)
	{
		if (!this._initData.spawnMisses)
		{
			return;
		}
		NoteData noteData = noteController.noteData;
		if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			Vector3 vector = noteController.noteTransform.position;
			Quaternion worldRotation = noteController.worldRotation;
			vector = noteController.inverseWorldRotation * vector;
			vector.z = this._spawnPosZ;
			vector = worldRotation * vector;
			this._missedNoteFlyingSpriteSpawner.SpawnFlyingSprite(vector, noteController.worldRotation, noteController.inverseWorldRotation);
		}
	}

	// Token: 0x04000A93 RID: 2707
	[SerializeField]
	private FlyingSpriteSpawner _missedNoteFlyingSpriteSpawner;

	// Token: 0x04000A94 RID: 2708
	[Inject]
	private MissedNoteEffectSpawner.InitData _initData;

	// Token: 0x04000A95 RID: 2709
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000A96 RID: 2710
	private float _spawnPosZ;

	// Token: 0x02000261 RID: 609
	public class InitData
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x00007FEE File Offset: 0x000061EE
		public InitData(bool spawnMisses)
		{
			this.spawnMisses = spawnMisses;
		}

		// Token: 0x04000A97 RID: 2711
		public readonly bool spawnMisses;
	}
}
