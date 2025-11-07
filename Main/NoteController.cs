using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000247 RID: 583
public abstract class NoteController : MonoBehaviour, INoteController
{
	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06000972 RID: 2418 RVA: 0x0002D9AC File Offset: 0x0002BBAC
	// (remove) Token: 0x06000973 RID: 2419 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
	public event Action<NoteController> didInitEvent;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06000974 RID: 2420 RVA: 0x0002DA1C File Offset: 0x0002BC1C
	// (remove) Token: 0x06000975 RID: 2421 RVA: 0x0002DA54 File Offset: 0x0002BC54
	public event Action<NoteController> noteDidStartJumpEvent;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06000976 RID: 2422 RVA: 0x0002DA8C File Offset: 0x0002BC8C
	// (remove) Token: 0x06000977 RID: 2423 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
	public event Action<NoteController> noteDidFinishJumpEvent;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06000978 RID: 2424 RVA: 0x0002DAFC File Offset: 0x0002BCFC
	// (remove) Token: 0x06000979 RID: 2425 RVA: 0x0002DB34 File Offset: 0x0002BD34
	public event Action<NoteController> noteDidPassJumpThreeQuartersEvent;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x0600097A RID: 2426 RVA: 0x0002DB6C File Offset: 0x0002BD6C
	// (remove) Token: 0x0600097B RID: 2427 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
	public event Action<NoteController, NoteCutInfo> noteWasCutEvent;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x0600097C RID: 2428 RVA: 0x0002DBDC File Offset: 0x0002BDDC
	// (remove) Token: 0x0600097D RID: 2429 RVA: 0x0002DC14 File Offset: 0x0002BE14
	public event Action<NoteController> noteWasMissedEvent;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x0600097E RID: 2430 RVA: 0x0002DC4C File Offset: 0x0002BE4C
	// (remove) Token: 0x0600097F RID: 2431 RVA: 0x0002DC84 File Offset: 0x0002BE84
	public event Action<NoteController, float> noteDidStartDissolvingEvent;

	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000980 RID: 2432 RVA: 0x0002DCBC File Offset: 0x0002BEBC
	// (remove) Token: 0x06000981 RID: 2433 RVA: 0x0002DCF4 File Offset: 0x0002BEF4
	public event Action<NoteController> noteDidDissolveEvent;

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000982 RID: 2434 RVA: 0x000077C3 File Offset: 0x000059C3
	public NoteData noteData
	{
		get
		{
			return this._noteData;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x000077CB File Offset: 0x000059CB
	public Transform noteTransform
	{
		get
		{
			return this._noteTransform;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000984 RID: 2436 RVA: 0x000077D3 File Offset: 0x000059D3
	public Quaternion worldRotation
	{
		get
		{
			return this._noteMovement.worldRotation;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000985 RID: 2437 RVA: 0x000077E0 File Offset: 0x000059E0
	public Quaternion inverseWorldRotation
	{
		get
		{
			return this._noteMovement.inverseWorldRotation;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000986 RID: 2438 RVA: 0x000077ED File Offset: 0x000059ED
	public float moveStartTime
	{
		get
		{
			return this._noteMovement.moveStartTime;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000987 RID: 2439 RVA: 0x000077FA File Offset: 0x000059FA
	public float moveDuration
	{
		get
		{
			return this._noteMovement.moveDuration;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000988 RID: 2440 RVA: 0x00007807 File Offset: 0x00005A07
	public float jumpDuration
	{
		get
		{
			return this._noteMovement.jumpDuration;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000989 RID: 2441 RVA: 0x00007814 File Offset: 0x00005A14
	public Vector3 jumpMoveVec
	{
		get
		{
			return this._noteMovement.jumpMoveVec;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x0600098A RID: 2442 RVA: 0x00007821 File Offset: 0x00005A21
	public Vector3 beatPos
	{
		get
		{
			return this._noteMovement.beatPos;
		}
	}

	// Token: 0x1700027F RID: 639
	// (set) Token: 0x0600098B RID: 2443
	public abstract bool hide { set; }

	// Token: 0x0600098C RID: 2444 RVA: 0x0002DD2C File Offset: 0x0002BF2C
	protected virtual void Awake()
	{
		this._noteMovement.noteDidFinishJumpEvent += this.HandleNoteDidFinishJumpEvent;
		this._noteMovement.noteDidStartJumpEvent += this.HandleNoteDidStartJumpEvent;
		this._noteMovement.noteDidPassJumpThreeQuartersEvent += this.HandleNoteDidPassJumpThreeQuartersEvent;
		this._noteMovement.noteDidPassMissedMarkerEvent += this.HandleNoteDidPassMissedMarkerEvent;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0002DD98 File Offset: 0x0002BF98
	protected virtual void OnDestroy()
	{
		if (this._noteMovement != null)
		{
			this._noteMovement.noteDidFinishJumpEvent -= this.HandleNoteDidFinishJumpEvent;
			this._noteMovement.noteDidStartJumpEvent -= this.HandleNoteDidStartJumpEvent;
			this._noteMovement.noteDidPassJumpThreeQuartersEvent -= this.HandleNoteDidPassJumpThreeQuartersEvent;
			this._noteMovement.noteDidPassMissedMarkerEvent -= this.HandleNoteDidPassMissedMarkerEvent;
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0000782E File Offset: 0x00005A2E
	protected virtual void Update()
	{
		this._noteMovement.ManualUpdate();
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0000783B File Offset: 0x00005A3B
	private void HandleNoteDidStartJumpEvent()
	{
		this.NoteDidStartJump();
		Action<NoteController> action = this.noteDidStartJumpEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00007854 File Offset: 0x00005A54
	private void HandleNoteDidFinishJumpEvent()
	{
		if (this._dissolving)
		{
			return;
		}
		this.NoteDidFinishJump();
		Action<NoteController> action = this.noteDidFinishJumpEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00007876 File Offset: 0x00005A76
	private void HandleNoteDidPassJumpThreeQuartersEvent(NoteMovement noteMovement)
	{
		if (this._dissolving)
		{
			return;
		}
		this.NoteDidPassJumpThreeQuarters(noteMovement);
		Action<NoteController> action = this.noteDidPassJumpThreeQuartersEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00007899 File Offset: 0x00005A99
	private void HandleNoteDidPassMissedMarkerEvent()
	{
		if (this._dissolving)
		{
			return;
		}
		this.NoteDidPassMissedMarker();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x000023E9 File Offset: 0x000005E9
	protected virtual void NoteDidStartJump()
	{
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x000023E9 File Offset: 0x000005E9
	protected virtual void NoteDidFinishJump()
	{
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x000023E9 File Offset: 0x000005E9
	protected virtual void NoteDidPassJumpThreeQuarters(NoteMovement noteMovement)
	{
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x000023E9 File Offset: 0x000005E9
	protected virtual void NoteDidPassMissedMarker()
	{
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x000023E9 File Offset: 0x000005E9
	protected virtual void NoteDidStartDissolving()
	{
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x000078AA File Offset: 0x00005AAA
	protected void SendNoteWasMissedEvent()
	{
		Action<NoteController> action = this.noteWasMissedEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x000078BD File Offset: 0x00005ABD
	protected void SendNoteWasCutEvent(NoteCutInfo noteCutInfo)
	{
		Action<NoteController, NoteCutInfo> action = this.noteWasCutEvent;
		if (action == null)
		{
			return;
		}
		action(this, noteCutInfo);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0002DE10 File Offset: 0x0002C010
	public virtual void Init(NoteData noteData, float worldRotation, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float cutDirectionAngleOffset)
	{
		this._noteData = noteData;
		this._noteMovement.Init(noteData.time, worldRotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, this._noteData.flipYSide, this._noteData.cutDirection, cutDirectionAngleOffset);
		Action<NoteController> action = this.didInitEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x000078D1 File Offset: 0x00005AD1
	private IEnumerator DissolveCoroutine(float duration)
	{
		Action<NoteController, float> action = this.noteDidStartDissolvingEvent;
		if (action != null)
		{
			action(this, duration);
		}
		yield return new WaitForSeconds(duration);
		this._dissolving = false;
		Action<NoteController> action2 = this.noteDidDissolveEvent;
		if (action2 != null)
		{
			action2(this);
		}
		yield break;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x000078E7 File Offset: 0x00005AE7
	public void Dissolve(float duration)
	{
		if (this._dissolving)
		{
			return;
		}
		this._dissolving = true;
		this.NoteDidStartDissolving();
		base.StartCoroutine(this.DissolveCoroutine(duration));
	}

	// Token: 0x040009C5 RID: 2501
	[SerializeField]
	protected NoteMovement _noteMovement;

	// Token: 0x040009C6 RID: 2502
	[SerializeField]
	private Transform _noteTransform;

	// Token: 0x040009CF RID: 2511
	private NoteData _noteData;

	// Token: 0x040009D0 RID: 2512
	private bool _dissolving;

	// Token: 0x02000248 RID: 584
	public class Pool : MemoryPoolWithActiveItems<NoteController>
	{
	}
}
