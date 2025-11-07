using System;
using UnityEngine;
using Zenject;

// Token: 0x02000340 RID: 832
public class MainSettingsMenuViewControllersInstaller : MonoInstaller
{
	// Token: 0x06000E78 RID: 3704 RVA: 0x0000B198 File Offset: 0x00009398
	public override void InstallBindings()
	{
		base.Container.Bind<MainSettingsMenuViewController>().FromInstance(this._defaultSettingsMenuViewController);
		base.Container.Bind<TabBarViewController>().FromComponentInNewPrefab(this._tabBarViewControllerPrefab).AsTransient();
	}

	// Token: 0x04000EEA RID: 3818
	[SerializeField]
	private MainSettingsMenuViewController _defaultSettingsMenuViewController;

	// Token: 0x04000EEB RID: 3819
	[SerializeField]
	private MainSettingsMenuViewController _oculusPCSettingsMenuViewController;

	// Token: 0x04000EEC RID: 3820
	[SerializeField]
	private MainSettingsMenuViewController _questSettingsMenuViewController;

	// Token: 0x04000EED RID: 3821
	[SerializeField]
	private MainSettingsMenuViewController _psvrSettingsMenuViewController;

	// Token: 0x04000EEE RID: 3822
	[SerializeField]
	private TabBarViewController _tabBarViewControllerPrefab;
}
