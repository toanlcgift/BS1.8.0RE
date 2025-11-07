using System;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class AssetObjectListSO : PersistentScriptableObject
{
	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x0600147F RID: 5247 RVA: 0x0000F74E File Offset: 0x0000D94E
	public UnityEngine.Object[] objects
	{
		get
		{
			return this._objects;
		}
	}

	// Token: 0x04001430 RID: 5168
	[SerializeField]
	[Reorderable]
	private UnityEngine.Object[] _objects;
}
