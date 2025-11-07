using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class ConditionalMove : MonoBehaviour
{
	// Token: 0x06000E37 RID: 3639 RVA: 0x0000AFD5 File Offset: 0x000091D5
	protected void Awake()
	{
		if (!this._activateOnFalse == this._value)
		{
			base.transform.localPosition = base.transform.localPosition + this._offset;
		}
	}

	// Token: 0x04000E9C RID: 3740
	[SerializeField]
	private Vector3 _offset;

	// Token: 0x04000E9D RID: 3741
	[SerializeField]
	private BoolSO _value;

	// Token: 0x04000E9E RID: 3742
	[SerializeField]
	private bool _activateOnFalse;
}
