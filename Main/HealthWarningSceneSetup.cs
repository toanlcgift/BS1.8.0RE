using System;
using Zenject;

// Token: 0x0200045A RID: 1114
public class HealthWarningSceneSetup : MonoInstaller
{
	// Token: 0x06001513 RID: 5395 RVA: 0x0004E220 File Offset: 0x0004C420
	public override void InstallBindings()
	{
		base.Container.Bind<HealthWarningFlowCoordinator.InitData>().FromInstance(new HealthWarningFlowCoordinator.InitData(this._sceneSetupData.nextScenesTransitionSetupData)).AsSingle();
		base.Container.Bind<SafeAreaRectChecker.InitData>().FromInstance(new SafeAreaRectChecker.InitData(false)).AsSingle();
		base.Container.Bind<EulaViewController.InitData>().FromInstance(new EulaViewController.InitData(true)).AsSingle();
	}

	// Token: 0x04001505 RID: 5381
	[Inject]
	private HealthWarningSceneSetupData _sceneSetupData;
}
