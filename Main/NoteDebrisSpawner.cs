using System;
using UnityEngine;
using Zenject;

// Token: 0x02000264 RID: 612
public class NoteDebrisSpawner : MonoBehaviour
{
	// Token: 0x06000A47 RID: 2631 RVA: 0x0003071C File Offset: 0x0002E91C
	public void SpawnDebris(NoteCutInfo noteCutInfo, INoteController noteController, Vector3 moveVec)
	{
		Vector3 cutPoint = noteCutInfo.cutPoint;
		Vector3 cutNormal = noteCutInfo.cutNormal;
		float num = Vector3.Dot(cutNormal, Vector3.up);
		float d = 5f;
		NoteDebris noteDebris = this._noteDebrisPool.Spawn();
		noteDebris.didFinishEvent += this.HandleNoteDebrisDidFinish;
		noteDebris.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		NoteDebris noteDebris2 = this._noteDebrisPool.Spawn();
		noteDebris2.didFinishEvent += this.HandleNoteDebrisDidFinish;
		noteDebris2.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		float sqrMagnitude = moveVec.sqrMagnitude;
		if (moveVec.sqrMagnitude > 121f)
		{
			moveVec *= 0.6f;
		}
		else
		{
			moveVec *= 0.4f;
		}
		NoteType noteType = noteController.noteData.noteType;
		Transform noteTransform = noteController.noteTransform;
		float lifeTime = Mathf.Clamp(noteController.noteData.timeToNextBasicNote + 0.4f, 0.8f, 1.5f);
		float num2 = 3f;
		float num3 = Mathf.Abs(num) * 4f;
		Vector3 force = -(cutNormal + UnityEngine.Random.onUnitSphere * 0.2f) * (num2 + ((num <= 0f) ? num3 : 0f)) + moveVec;
		Vector3 force2 = (cutNormal + UnityEngine.Random.onUnitSphere * 0.2f) * (num2 + ((num > 0f) ? num3 : 0f)) + moveVec;
		noteDebris.Init(noteType, noteTransform, cutPoint, -cutNormal, force, UnityEngine.Random.insideUnitSphere * d, lifeTime);
		noteDebris2.Init(noteType, noteTransform, cutPoint, cutNormal, force2, UnityEngine.Random.insideUnitSphere * d, lifeTime);
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x00008066 File Offset: 0x00006266
	private void HandleNoteDebrisDidFinish(NoteDebris noteDebris)
	{
		noteDebris.didFinishEvent -= this.HandleNoteDebrisDidFinish;
		this._noteDebrisPool.Despawn(noteDebris);
	}

	// Token: 0x04000AA6 RID: 2726
	[Inject]
	private NoteDebris.Pool _noteDebrisPool;

	// Token: 0x04000AA7 RID: 2727
	private const float kMinLifeTime = 0.8f;

	// Token: 0x04000AA8 RID: 2728
	private const float kMaxLifeTime = 1.5f;

	// Token: 0x04000AA9 RID: 2729
	private const float kLifeTimeOffset = 0.4f;
}
