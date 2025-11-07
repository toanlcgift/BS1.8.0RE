using System;
using UnityEngine;
using Zenject;

// Token: 0x02000342 RID: 834
public class PromoViewControllersInstaller : MonoInstaller
{
	// Token: 0x06000E7C RID: 3708 RVA: 0x0000B1EC File Offset: 0x000093EC
	public override void InstallBindings()
	{
		base.Container.Bind<PromoViewController>().FromComponentInNewPrefab(this._defaultPromoViewControllerPrefab).AsSingle();
	}

	// Token: 0x04000EF0 RID: 3824
	[SerializeField]
	private PromoViewController _defaultPromoViewControllerPrefab;

	// Token: 0x04000EF1 RID: 3825
	[SerializeField]
	private PromoViewController _psPromoViewControllerPrefab;
}
