using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class ConditionalActivation : MonoBehaviour
{
	// Token: 0x06000E31 RID: 3633 RVA: 0x0000AF41 File Offset: 0x00009141
	protected void Awake()
	{
		base.gameObject.SetActive(this._activateOnFalse ? (!this._value) : this._value);
	}

	// Token: 0x04000E92 RID: 3730
	[SerializeField]
	private BoolSO _value;

	// Token: 0x04000E93 RID: 3731
	[SerializeField]
	private bool _activateOnFalse;
}
