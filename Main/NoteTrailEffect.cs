using System;
using UnityEngine;
using Zenject;

// Token: 0x0200028E RID: 654
public class NoteTrailEffect : MonoBehaviour
{
	// Token: 0x06000AFC RID: 2812 RVA: 0x00033248 File Offset: 0x00031448
	protected void Awake()
	{
		this._noteMovement.didInitEvent += this.HandleNoteMovementDidInit;
		this._noteMovement.noteDidStartJumpEvent += this.HandleNoteDidStartJump;
		base.enabled = this._noteMovement.enabled;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00008940 File Offset: 0x00006B40
	protected void OnDestroy()
	{
		if (this._noteMovement != null)
		{
			this._noteMovement.didInitEvent -= this.HandleNoteMovementDidInit;
			this._noteMovement.noteDidFinishJumpEvent -= this.HandleNoteDidStartJump;
		}
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00033294 File Offset: 0x00031494
	protected void Update()
	{
		Vector3 position = this._noteMovement.position;
		if (position.x * position.x + position.z * position.z < this._maxSpawnDistance * this._maxSpawnDistance)
		{
			this._noteTrailParticleSystem.Emit(this._noteMovement.prevPosition, position, this._particlesPerFrame);
		}
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0000897E File Offset: 0x00006B7E
	private void HandleNoteMovementDidInit()
	{
		base.enabled = true;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00008987 File Offset: 0x00006B87
	private void HandleNoteDidStartJump()
	{
		base.enabled = false;
	}

	// Token: 0x04000B8E RID: 2958
	[SerializeField]
	private int _particlesPerFrame = 2;

	// Token: 0x04000B8F RID: 2959
	[SerializeField]
	private float _maxSpawnDistance = 70f;

	// Token: 0x04000B90 RID: 2960
	[SerializeField]
	private NoteMovement _noteMovement;

	// Token: 0x04000B91 RID: 2961
	[Inject]
	private NoteTrailParticleSystem _noteTrailParticleSystem;
}
