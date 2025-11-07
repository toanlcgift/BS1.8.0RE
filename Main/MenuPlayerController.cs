using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class MenuPlayerController : MonoBehaviour
{
	// Token: 0x17000371 RID: 881
	// (get) Token: 0x060010AD RID: 4269 RVA: 0x0000CACD File Offset: 0x0000ACCD
	public VRController leftController
	{
		get
		{
			return this._leftController;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x060010AE RID: 4270 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
	public VRController rightController
	{
		get
		{
			return this._rightController;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x060010AF RID: 4271 RVA: 0x0000CADD File Offset: 0x0000ACDD
	public Vector3 headPos
	{
		get
		{
			return this._headTransform.position;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0000CAEA File Offset: 0x0000ACEA
	public Quaternion headRot
	{
		get
		{
			return this._headTransform.rotation;
		}
	}

	// Token: 0x040010C6 RID: 4294
	[SerializeField]
	private VRController _leftController;

	// Token: 0x040010C7 RID: 4295
	[SerializeField]
	private VRController _rightController;

	// Token: 0x040010C8 RID: 4296
	[SerializeField]
	private Transform _headTransform;
}
