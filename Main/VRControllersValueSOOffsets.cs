using System;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class VRControllersValueSOOffsets : VRControllerTransformOffset
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0002AEBC File Offset: 0x000290BC
	public override Vector3 positionOffset
	{
		get
		{
			if (this._mirror)
			{
				Vector3 value = this._positionOffset.value;
				value.x = -value.x;
				return value;
			}
			return this._positionOffset.value;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0002AEF8 File Offset: 0x000290F8
	public override Vector3 rotationOffset
	{
		get
		{
			if (this._mirror)
			{
				Vector3 value = this._rotationOffset.value;
				value.y = -value.y;
				return value;
			}
			return this._rotationOffset.value;
		}
	}

	// Token: 0x04000919 RID: 2329
	[SerializeField]
	private Vector3SO _positionOffset;

	// Token: 0x0400091A RID: 2330
	[SerializeField]
	private Vector3SO _rotationOffset;

	// Token: 0x0400091B RID: 2331
	[SerializeField]
	private bool _mirror;
}
