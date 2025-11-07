using System;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class GameNoteCollidersSizeController : MonoBehaviour
{
	// Token: 0x06000958 RID: 2392 RVA: 0x0002D710 File Offset: 0x0002B910
	protected void Awake()
	{
		this._initColliderSize = this._bigCuttableBySaber.colliderSize;
		this._gameNoteController.didInitEvent += this.HandleGameNoteControllerDidInit;
		this._gameNoteController.noteDidStartJumpEvent += this.HandleGameNoteControllerNoteDidStartJump;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00007664 File Offset: 0x00005864
	protected void OnDestroy()
	{
		if (this._gameNoteController != null)
		{
			this._gameNoteController.didInitEvent -= this.HandleGameNoteControllerDidInit;
			this._gameNoteController.noteDidStartJumpEvent -= this.HandleGameNoteControllerNoteDidStartJump;
		}
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000076A2 File Offset: 0x000058A2
	private void HandleGameNoteControllerDidInit(NoteController noteController)
	{
		this._prevPosIsValid = false;
		base.enabled = false;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x000076B2 File Offset: 0x000058B2
	private void HandleGameNoteControllerNoteDidStartJump(NoteController noteController)
	{
		this._prevPosIsValid = false;
		base.enabled = true;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0002D75C File Offset: 0x0002B95C
	protected void Update()
	{
		if (this._noteMovement.movementPhase != NoteMovement.MovementPhase.Jumping)
		{
			return;
		}
		Vector3 localPosition = base.transform.localPosition;
		if (this._prevPosIsValid)
		{
			float num = this._prevPos.z - localPosition.z - this._initColliderSize.z;
			if (num < 0f)
			{
				num = 0f;
			}
			float z = num * 0.5f;
			Vector3 initColliderSize = this._initColliderSize;
			initColliderSize.z += num;
			this._bigCuttableBySaber.SetColliderCenterAndSize(new Vector3(0f, 0f, z), initColliderSize);
		}
		this._prevPos = localPosition;
		this._prevPosIsValid = true;
	}

	// Token: 0x040009B7 RID: 2487
	[SerializeField]
	private GameNoteController _gameNoteController;

	// Token: 0x040009B8 RID: 2488
	[SerializeField]
	private BoxCuttableBySaber _bigCuttableBySaber;

	// Token: 0x040009B9 RID: 2489
	[SerializeField]
	private NoteMovement _noteMovement;

	// Token: 0x040009BA RID: 2490
	private Vector3 _prevPos;

	// Token: 0x040009BB RID: 2491
	private bool _prevPosIsValid;

	// Token: 0x040009BC RID: 2492
	private Vector3 _initColliderSize;
}
