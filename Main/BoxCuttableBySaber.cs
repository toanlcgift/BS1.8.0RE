using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class BoxCuttableBySaber : CuttableBySaber
{
	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0000A291 File Offset: 0x00008491
	public override float radius
	{
		get
		{
			return this._radius;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0000A2AE File Offset: 0x000084AE
	// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0000A299 File Offset: 0x00008499
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

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0000A2CA File Offset: 0x000084CA
	// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0000A2B6 File Offset: 0x000084B6
	public Vector3 colliderSize
	{
		get
		{
			return this._collider.size;
		}
		set
		{
			this._collider.size = value;
			this.RefreshRadius();
		}
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0000A2D7 File Offset: 0x000084D7
	protected void Awake()
	{
		this._canBeCut = this._collider.enabled;
		this.RefreshRadius();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x0000A2F0 File Offset: 0x000084F0
	public override void Cut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		if (this._canBeCut)
		{
			base.CallWasCutBySaberEvent(saber, cutPoint, orientation, cutDirVec);
		}
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0000A305 File Offset: 0x00008505
	public void SetColliderCenterAndSize(Vector3 center, Vector3 size)
	{
		this._collider.center = center;
		this._collider.size = size;
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x00037E8C File Offset: 0x0003608C
	private void RefreshRadius()
	{
		Vector3 max = this._collider.bounds.max;
		this._radius = Mathf.Max(Mathf.Max(max.x, max.y), max.z);
	}

	// Token: 0x04000D8C RID: 3468
	[SerializeField]
	private BoxCollider _collider;

	// Token: 0x04000D8D RID: 3469
	private bool _canBeCut;

	// Token: 0x04000D8E RID: 3470
	private float _radius;
}
