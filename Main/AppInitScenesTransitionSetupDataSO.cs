using System;

// Token: 0x0200046D RID: 1133
public class AppInitScenesTransitionSetupDataSO : SingleFixedSceneScenesTransitionSetupDataSO
{
	// Token: 0x0600154A RID: 5450 RVA: 0x0000FFC2 File Offset: 0x0000E1C2
	public void Init()
	{
		base.Init(new AppInitScenesTransitionSetupDataSO.AppInitSceneSetupData(AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.AppRestart));
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
	public void __Init(AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType appInitOverrideStartType)
	{
		base.Init(new AppInitScenesTransitionSetupDataSO.AppInitSceneSetupData(appInitOverrideStartType));
	}

	// Token: 0x0200046E RID: 1134
	public enum AppInitOverrideStartType
	{
		// Token: 0x04001541 RID: 5441
		DoNotOverride,
		// Token: 0x04001542 RID: 5442
		AppStart,
		// Token: 0x04001543 RID: 5443
		AppRestart,
		// Token: 0x04001544 RID: 5444
		MultiSceneEditor
	}

	// Token: 0x0200046F RID: 1135
	public class AppInitSceneSetupData : SceneSetupData
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0000FFE6 File Offset: 0x0000E1E6
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0000FFEE File Offset: 0x0000E1EE
		public AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType appInitOverrideStartType { get; private set; }

		// Token: 0x0600154F RID: 5455 RVA: 0x0000FFF7 File Offset: 0x0000E1F7
		public AppInitSceneSetupData(AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType appInitOverrideStartType)
		{
			this.appInitOverrideStartType = appInitOverrideStartType;
		}
	}
}
