using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class SphereCuttableBySaber : CuttableBySaber
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0000A735 File Offset: 0x00008935
	public override float radius
	{
		get
		{
			return this._collider.radius;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0000A757 File Offset: 0x00008957
	// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0000A742 File Offset: 0x00008942
	public override bool canBeCut
	{
		get
		{
			return this._canBeCut;
		}
		set
		{
			this._collider.enabled = value;
			this._canBeCut = value;
		}
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0000A75F File Offset: 0x0000895F
	protected void Awake()
	{
		this._canBeCut = this._collider.enabled;
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0000A772 File Offset: 0x00008972
	public override void Cut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		if (this._canBeCut)
		{
			base.CallWasCutBySaberEvent(saber, cutPoint, orientation, cutDirVec);
		}
	}

	// Token: 0x04000DED RID: 3565
	[SerializeField]
	private SphereCollider _collider;

	// Token: 0x04000DEE RID: 3566
	private bool _canBeCut;
}
