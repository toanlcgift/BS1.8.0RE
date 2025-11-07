using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class BombNoteController : NoteController
{
	// Token: 0x1700026A RID: 618
	// (set) Token: 0x0600093B RID: 2363 RVA: 0x000074AF File Offset: 0x000056AF
	public override bool hide
	{
		set
		{
			this._wrapperGO.SetActive(!value);
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x000074C0 File Offset: 0x000056C0
	protected override void Awake()
	{
		base.Awake();
		this._cuttableBySaber.wasCutBySaberEvent += this.HandleWasCutBySaber;
		this._noteMovement.noteDidPassHalfJumpEvent += this.HandleDidPassHalfJump;
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x000074F6 File Offset: 0x000056F6
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._cuttableBySaber != null)
		{
			this._cuttableBySaber.wasCutBySaberEvent -= this.HandleWasCutBySaber;
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00007523 File Offset: 0x00005723
	protected override void NoteDidPassMissedMarker()
	{
		this._cuttableBySaber.canBeCut = false;
		base.SendNoteWasMissedEvent();
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00007537 File Offset: 0x00005737
	protected void HandleDidPassHalfJump()
	{
		this._cuttableBySaber.canBeCut = false;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0002D254 File Offset: 0x0002B454
	private void HandleWasCutBySaber(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		bool directionOK = true;
		bool speedOK = true;
		bool saberTypeOK = false;
		float timeDeviation = 0f;
		float cutDirDeviation = 0f;
		Vector3 cutNormal = orientation * Vector3.up;
		NoteCutInfo noteCutInfo = new NoteCutInfo(speedOK, directionOK, saberTypeOK, false, saber.bladeSpeed, cutDirVec, saber.saberType, timeDeviation, cutDirDeviation, cutPoint, cutNormal, null, 0f);
		base.SendNoteWasCutEvent(noteCutInfo);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0002D2AC File Offset: 0x0002B4AC
	public override void Init(NoteData noteData, float worldRotation, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float jumpGravity, float cutDirectionAngleOffset)
	{
		base.Init(noteData, worldRotation, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, jumpGravity, cutDirectionAngleOffset);
		this._cuttableBySaber.canBeCut = true;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00007537 File Offset: 0x00005737
	protected override void NoteDidStartDissolving()
	{
		this._cuttableBySaber.canBeCut = false;
	}

	// Token: 0x0400099C RID: 2460
	[SerializeField]
	private CuttableBySaber _cuttableBySaber;

	// Token: 0x0400099D RID: 2461
	[SerializeField]
	private GameObject _wrapperGO;
}
