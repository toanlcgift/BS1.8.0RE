using System;
using UnityEngine;
using Zenject;

// Token: 0x02000307 RID: 775
public class SaberModelContainer : MonoBehaviour
{
	// Token: 0x06000D5C RID: 3420 RVA: 0x0000A56B File Offset: 0x0000876B
	protected void Awake()
	{
		this._saberModelController.Init(base.transform, this._saberTypeObject.saberType);
	}

	// Token: 0x04000DC5 RID: 3525
	[SerializeField]
	private SaberTypeObject _saberTypeObject;

	// Token: 0x04000DC6 RID: 3526
	[Inject]
	private ISaberModelController _saberModelController;
}
