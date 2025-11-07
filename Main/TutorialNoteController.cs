using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class TutorialNoteController : NoteController
{
	// Token: 0x17000299 RID: 665
	// (set) Token: 0x060009F6 RID: 2550 RVA: 0x00007C43 File Offset: 0x00005E43
	public override bool hide
	{
		set
		{
			this._wrapperGO.SetActive(!value);
		}
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00007C54 File Offset: 0x00005E54
	protected override void Awake()
	{
		base.Awake();
		this._cuttableBySaberCore.wasCutBySaberEvent += this.HandleCoreWasCutBySaberEvent;
		this._cuttableBySaberBeforeNote.wasCutBySaberEvent += this.HandleBeforeNoteWasCutBySaberEvent;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0002F248 File Offset: 0x0002D448
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._cuttableBySaberCore != null)
		{
			this._cuttableBySaberCore.wasCutBySaberEvent -= this.HandleCoreWasCutBySaberEvent;
		}
		if (this._cuttableBySaberBeforeNote != null)
		{
			this._cuttableBySaberBeforeNote.wasCutBySaberEvent -= this.HandleBeforeNoteWasCutBySaberEvent;
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00007C8A File Offset: 0x00005E8A
	protected override void NoteDidPassMissedMarker()
	{
		this._cuttableBySaberCore.canBeCut = false;
		this._cuttableBySaberBeforeNote.canBeCut = false;
		base.SendNoteWasMissedEvent();
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
	private void HandleBeforeNoteWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		if (this._beforeNoteCutWasOK)
		{
			return;
		}
		bool flag;
		bool flag2;
		bool flag3;
		float num;
		NoteBasicCutInfo.GetBasicCutInfo(base.noteTransform, base.noteData.noteType, base.noteData.cutDirection, saber.saberType, saber.bladeSpeed, cutDirVec, out flag, out flag2, out flag3, out num);
		this._beforeNoteCutWasOK = (flag && flag2 && flag3);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0002F300 File Offset: 0x0002D500
	private void HandleCoreWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		float timeDeviation = 0f;
		bool flag;
		bool flag2;
		bool flag3;
		float cutDirDeviation;
		NoteBasicCutInfo.GetBasicCutInfo(base.noteTransform, base.noteData.noteType, base.noteData.cutDirection, saber.saberType, saber.bladeSpeed, cutDirVec, out flag, out flag2, out flag3, out cutDirDeviation);
		bool wasCutTooSoon = this._beforeNoteCutWasOK && flag3 && (!flag || !flag2);
		Vector3 vector = orientation * Vector3.up;
		Plane plane = new Plane(vector, cutPoint);
		NoteCutInfo noteCutInfo = new NoteCutInfo(flag2, flag, flag3, wasCutTooSoon, saber.bladeSpeed, cutDirVec, saber.saberType, timeDeviation, cutDirDeviation, plane.ClosestPointOnPlane(base.transform.position), vector, null, 0f);
		base.SendNoteWasCutEvent(noteCutInfo);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0002F3BC File Offset: 0x0002D5BC
	public override void Init(NoteData noteData, float worldRotation, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float cutDirectionAngleOffset)
	{
		base.Init(noteData, worldRotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, cutDirectionAngleOffset);
		this._beforeNoteCutWasOK = false;
		this._cuttableBySaberCore.canBeCut = true;
		this._cuttableBySaberBeforeNote.canBeCut = true;
	}

	// Token: 0x04000A2D RID: 2605
	[SerializeField]
	private BoxCuttableBySaber _cuttableBySaberCore;

	// Token: 0x04000A2E RID: 2606
	[SerializeField]
	private BoxCuttableBySaber _cuttableBySaberBeforeNote;

	// Token: 0x04000A2F RID: 2607
	[SerializeField]
	private GameObject _wrapperGO;

	// Token: 0x04000A30 RID: 2608
	private bool _beforeNoteCutWasOK;
}
