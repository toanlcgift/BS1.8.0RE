using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class SaberTypeObject : MonoBehaviour
{
	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0000A5D0 File Offset: 0x000087D0
	public SaberType saberType
	{
		get
		{
			return this._saberType;
		}
	}

	// Token: 0x04000DD1 RID: 3537
	[SerializeField]
	private SaberType _saberType;
}
