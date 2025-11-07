using System;
using UnityEngine;
using Zenject;

// Token: 0x02000341 RID: 833
public class MenuInstaller : MonoInstaller
{
	// Token: 0x06000E7A RID: 3706 RVA: 0x0000B1CD File Offset: 0x000093CD
	public override void InstallBindings()
	{
		base.Container.BindMemoryPool<FireworkItemController, FireworkItemController.Pool>().WithInitialSize(5).FromComponentInNewPrefab(this._fireworkItemControllerPrefab);
	}

	// Token: 0x04000EEF RID: 3823
	[SerializeField]
	private FireworkItemController _fireworkItemControllerPrefab;
}
