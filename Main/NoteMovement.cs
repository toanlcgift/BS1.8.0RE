using System;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class NoteMovement : MonoBehaviour, IManualUpdate
{
	// Token: 0x14000036 RID: 54
	// (add) Token: 0x060009D1 RID: 2513 RVA: 0x0002ED38 File Offset: 0x0002CF38
	// (remove) Token: 0x060009D2 RID: 2514 RVA: 0x0002ED70 File Offset: 0x0002CF70
	public event Action didInitEvent;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x060009D3 RID: 2515 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
	// (remove) Token: 0x060009D4 RID: 2516 RVA: 0x0002EDE0 File Offset: 0x0002CFE0
	public event Action noteDidStartJumpEvent;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x060009D5 RID: 2517 RVA: 0x0002EE18 File Offset: 0x0002D018
	// (remove) Token: 0x060009D6 RID: 2518 RVA: 0x0002EE50 File Offset: 0x0002D050
	public event Action noteDidFinishJumpEvent;

	// Token: 0x14000039 RID: 57
	// (add) Token: 0x060009D7 RID: 2519 RVA: 0x0002EE88 File Offset: 0x0002D088
	// (remove) Token: 0x060009D8 RID: 2520 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
	public event Action noteDidPassMissedMarkerEvent;

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x060009D9 RID: 2521 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
	// (remove) Token: 0x060009DA RID: 2522 RVA: 0x0002EF30 File Offset: 0x0002D130
	public event Action noteDidPassHalfJumpEvent;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x060009DB RID: 2523 RVA: 0x0002EF68 File Offset: 0x0002D168
	// (remove) Token: 0x060009DC RID: 2524 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
	public event Action<NoteMovement> noteDidPassJumpThreeQuartersEvent;

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060009DD RID: 2525 RVA: 0x00007AE4 File Offset: 0x00005CE4
	// (set) Token: 0x060009DE RID: 2526 RVA: 0x00007AEC File Offset: 0x00005CEC
	public NoteMovement.MovementPhase movementPhase { get; private set; }

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x00007AF5 File Offset: 0x00005CF5
	public Vector3 position
	{
		get
		{
			return this._position;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00007AFD File Offset: 0x00005CFD
	public Vector3 prevPosition
	{
		get
		{
			return this._prevPosition;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00007B05 File Offset: 0x00005D05
	public Quaternion worldRotation
	{
		get
		{
			return this._floorMovement.worldRotation;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00007B12 File Offset: 0x00005D12
	public Quaternion inverseWorldRotation
	{
		get
		{
			return this._floorMovement.inverseWorldRotation;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00007B1F File Offset: 0x00005D1F
	public Vector3 moveEndPos
	{
		get
		{
			return this._floorMovement.endPos;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00007B2C File Offset: 0x00005D2C
	public float moveStartTime
	{
		get
		{
			return this._floorMovement.startTime;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00007B39 File Offset: 0x00005D39
	public float moveDuration
	{
		get
		{
			return this._floorMovement.moveDuration;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00007B46 File Offset: 0x00005D46
	public Vector3 beatPos
	{
		get
		{
			return this._jump.beatPos;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00007B53 File Offset: 0x00005D53
	public float jumpDuration
	{
		get
		{
			return this._jump.jumpDuration;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00007B60 File Offset: 0x00005D60
	public Vector3 jumpMoveVec
	{
		get
		{
			return this._jump.moveVec;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00007B6D File Offset: 0x00005D6D
	public float distanceToPlayer
	{
		get
		{
			if (this.movementPhase != NoteMovement.MovementPhase.Jumping)
			{
				return this._floorMovement.distanceToPlayer;
			}
			return this._jump.distanceToPlayer;
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
	public void Init(float beatTime, float worldRotation, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float flipYSide, NoteCutDirection cutDirection, float cutDirectionAngleOffset)
	{
		moveStartPos.z += this._zOffset;
		moveEndPos.z += this._zOffset;
		jumpEndPos.z += this._zOffset;
		this._floorMovement.Init(worldRotation, moveStartPos, moveEndPos, moveDuration, beatTime - moveDuration - jumpDuration * 0.5f);
		this._position = this._floorMovement.SetToStart();
		this._prevPosition = this._position;
		this._jump.Init(beatTime, worldRotation, moveEndPos, jumpEndPos, jumpDuration, jumpGravity, flipYSide, cutDirection, cutDirectionAngleOffset);
		this.movementPhase = NoteMovement.MovementPhase.MovingOnTheFloor;
		Action action = this.didInitEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0002F088 File Offset: 0x0002D288
	protected void Awake()
	{
		this.movementPhase = NoteMovement.MovementPhase.None;
		this._floorMovement.floorMovementDidFinishEvent += this.HandleFloorMovementDidFinish;
		this._jump.noteJumpDidFinishEvent += this.HandleNoteJumpDidFinish;
		this._jump.noteJumpDidPassMissedMarkerEvent += this.HandleNoteJumpDidPassMissedMark;
		this._jump.noteJumpDidPassThreeQuartersEvent += this.HandleNoteJumpDidPassThreeQuarters;
		this._jump.noteJumpDidPassHalfEvent += this.HandleNoteJumpNoteJumpDidPassHalf;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0002F110 File Offset: 0x0002D310
	protected void OnDestroy()
	{
		if (this._floorMovement)
		{
			this._floorMovement.floorMovementDidFinishEvent -= this.HandleFloorMovementDidFinish;
		}
		if (this._jump)
		{
			this._jump.noteJumpDidFinishEvent -= this.HandleNoteJumpDidFinish;
			this._jump.noteJumpDidPassMissedMarkerEvent -= this.HandleNoteJumpDidPassMissedMark;
			this._jump.noteJumpDidPassThreeQuartersEvent -= this.HandleNoteJumpDidPassThreeQuarters;
			this._jump.noteJumpDidPassHalfEvent -= this.HandleNoteJumpNoteJumpDidPassHalf;
		}
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00007B8F File Offset: 0x00005D8F
	private void HandleFloorMovementDidFinish()
	{
		this.movementPhase = NoteMovement.MovementPhase.Jumping;
		this._position = this._jump.ManualUpdate();
		Action action = this.noteDidStartJumpEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00007BB9 File Offset: 0x00005DB9
	private void HandleNoteJumpDidFinish()
	{
		this.movementPhase = NoteMovement.MovementPhase.None;
		Action action = this.noteDidFinishJumpEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00007BD2 File Offset: 0x00005DD2
	private void HandleNoteJumpDidPassMissedMark()
	{
		Action action = this.noteDidPassMissedMarkerEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00007BE4 File Offset: 0x00005DE4
	private void HandleNoteJumpDidPassThreeQuarters(NoteJump noteJump)
	{
		Action<NoteMovement> action = this.noteDidPassJumpThreeQuartersEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00007BF7 File Offset: 0x00005DF7
	private void HandleNoteJumpNoteJumpDidPassHalf()
	{
		Action action = this.noteDidPassHalfJumpEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00007C09 File Offset: 0x00005E09
	public void ManualUpdate()
	{
		this._prevPosition = this._position;
		if (this.movementPhase == NoteMovement.MovementPhase.MovingOnTheFloor)
		{
			this._position = this._floorMovement.ManualUpdate();
			return;
		}
		this._position = this._jump.ManualUpdate();
	}

	// Token: 0x04000A1B RID: 2587
	[SerializeField]
	private NoteFloorMovement _floorMovement;

	// Token: 0x04000A1C RID: 2588
	[SerializeField]
	private NoteJump _jump;

	// Token: 0x04000A1D RID: 2589
	[Space]
	[SerializeField]
	private float _zOffset;

	// Token: 0x04000A25 RID: 2597
	private Vector3 _position;

	// Token: 0x04000A26 RID: 2598
	private Vector3 _prevPosition;

	// Token: 0x02000251 RID: 593
	public enum MovementPhase
	{
		// Token: 0x04000A28 RID: 2600
		None,
		// Token: 0x04000A29 RID: 2601
		MovingOnTheFloor,
		// Token: 0x04000A2A RID: 2602
		Jumping
	}
}
