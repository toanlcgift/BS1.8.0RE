using System;
using UnityEngine;
using Zenject;

// Token: 0x02000244 RID: 580
public class GameNoteController : NoteController
{
	// Token: 0x1700026E RID: 622
	// (get) Token: 0x0600095E RID: 2398 RVA: 0x000076C2 File Offset: 0x000058C2
	public bool ghostNote
	{
		get
		{
			return this._ghostNote;
		}
	}

	// Token: 0x1700026F RID: 623
	// (set) Token: 0x0600095F RID: 2399 RVA: 0x000076CA File Offset: 0x000058CA
	public override bool hide
	{
		set
		{
			this._wrapperGO.SetActive(!value);
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x000076DB File Offset: 0x000058DB
	protected override void Awake()
	{
		base.Awake();
		this._bigCuttableBySaber.wasCutBySaberEvent += this.HandleBigWasCutBySaberEvent;
		this._smallCuttableBySaber.wasCutBySaberEvent += this.HandleSmallWasCutBySaberEvent;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0002D800 File Offset: 0x0002BA00
	public void Init(NoteData noteData, float worldRotation, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, bool disappearingArrow, bool ghostNote, float cutDirectionAngleOffset)
	{
		this._disappearingArrow = disappearingArrow;
		this._ghostNote = ghostNote;
		this._bigCuttableBySaber.canBeCut = false;
		this._smallCuttableBySaber.canBeCut = false;
		base.Init(noteData, worldRotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, cutDirectionAngleOffset);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0002D84C File Offset: 0x0002BA4C
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._bigCuttableBySaber != null)
		{
			this._bigCuttableBySaber.wasCutBySaberEvent -= this.HandleBigWasCutBySaberEvent;
		}
		if (this._smallCuttableBySaber != null)
		{
			this._smallCuttableBySaber.wasCutBySaberEvent -= this.HandleSmallWasCutBySaberEvent;
		}
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00007711 File Offset: 0x00005911
	protected override void Update()
	{
		base.Update();
		if (this._noteMovement.movementPhase == NoteMovement.MovementPhase.Jumping && (this._disappearingArrow || this._ghostNote))
		{
			this._disappearingArrowController.ManualUpdate();
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00007742 File Offset: 0x00005942
	protected override void NoteDidPassMissedMarker()
	{
		this._bigCuttableBySaber.canBeCut = false;
		this._smallCuttableBySaber.canBeCut = false;
		base.SendNoteWasMissedEvent();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00007762 File Offset: 0x00005962
	protected override void NoteDidStartDissolving()
	{
		this._bigCuttableBySaber.canBeCut = false;
		this._smallCuttableBySaber.canBeCut = false;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0000777C File Offset: 0x0000597C
	private void HandleBigWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		this.HandleCut(saber, cutPoint, orientation, cutDirVec, false);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0000778A File Offset: 0x0000598A
	private void HandleSmallWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		this.HandleCut(saber, cutPoint, orientation, cutDirVec, true);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0002D8AC File Offset: 0x0002BAAC
	private void HandleCut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec, bool allowBadCut)
	{
		float timeDeviation = base.noteData.time - this._audioTimeSyncController.songTime;
		bool flag;
		bool flag2;
		bool flag3;
		float cutDirDeviation;
		NoteBasicCutInfo.GetBasicCutInfo(base.noteTransform, base.noteData.noteType, base.noteData.cutDirection, saber.saberType, saber.bladeSpeed, cutDirVec, out flag, out flag2, out flag3, out cutDirDeviation);
		SaberSwingRatingCounter swingRatingCounter = null;
		if (flag && flag2 && flag3)
		{
			swingRatingCounter = saber.CreateSwingRatingCounter(base.noteTransform);
		}
		else if (!allowBadCut)
		{
			return;
		}
		Vector3 vector = orientation * Vector3.up;
		Plane plane = new Plane(vector, cutPoint);
		float cutDistanceToCenter = Mathf.Abs(plane.GetDistanceToPoint(base.noteTransform.position));
		NoteCutInfo noteCutInfo = new NoteCutInfo(flag2, flag, flag3, false, saber.bladeSpeed, cutDirVec, saber.saberType, timeDeviation, cutDirDeviation, plane.ClosestPointOnPlane(base.transform.position), vector, swingRatingCounter, cutDistanceToCenter);
		this._bigCuttableBySaber.canBeCut = false;
		this._smallCuttableBySaber.canBeCut = false;
		base.SendNoteWasCutEvent(noteCutInfo);
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00007798 File Offset: 0x00005998
	protected override void NoteDidStartJump()
	{
		this._bigCuttableBySaber.canBeCut = true;
		this._smallCuttableBySaber.canBeCut = true;
	}

	// Token: 0x040009BD RID: 2493
	[SerializeField]
	private BoxCuttableBySaber _bigCuttableBySaber;

	// Token: 0x040009BE RID: 2494
	[SerializeField]
	private BoxCuttableBySaber _smallCuttableBySaber;

	// Token: 0x040009BF RID: 2495
	[SerializeField]
	private DisappearingArrowController _disappearingArrowController;

	// Token: 0x040009C0 RID: 2496
	[SerializeField]
	private GameObject _wrapperGO;

	// Token: 0x040009C1 RID: 2497
	[Inject]
	private AudioTimeSyncController _audioTimeSyncController;

	// Token: 0x040009C2 RID: 2498
	private bool _disappearingArrow;

	// Token: 0x040009C3 RID: 2499
	private bool _ghostNote;
}
