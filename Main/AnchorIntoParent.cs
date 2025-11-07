using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class AnchorIntoParent : MonoBehaviour
{
	// Token: 0x06000E2F RID: 3631 RVA: 0x0003A9C8 File Offset: 0x00038BC8
	protected void Start()
	{
		base.transform.parent = this._parentTransform;
		base.transform.localPosition = this._positionOffset;
		base.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x04000E90 RID: 3728
	[SerializeField]
	private Transform _parentTransform;

	// Token: 0x04000E91 RID: 3729
	[SerializeField]
	private Vector3 _positionOffset;
}
