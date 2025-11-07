using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class NetEaseAppInit : AppInit
{
	// Token: 0x06000049 RID: 73 RVA: 0x000023E9 File Offset: 0x000005E9
	protected override void AppStartAndMultiSceneEditorSetup()
	{
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000023EB File Offset: 0x000005EB
	protected override void RepeatableSetup()
	{
		this._mainSettingsModel.Load(false);
		MainSettingsDefaultValues.SetFixedDefaultValues(this._mainSettingsModel);
		this._mainSystemInit.Init();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000240F File Offset: 0x0000060F
	protected override void TransitionToNextScene()
	{
		this._netEaseLoginSceneTransitionSetupData.Init();
		base.gameScenesManager.ReplaceScenes(this._netEaseLoginSceneTransitionSetupData, 0f, null, null);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00002434 File Offset: 0x00000634
	public override void InstallBindings()
	{
		base.Container.Bind<IAnalyticsModel>().To<NoAnalyticsModel>().AsSingle();
		this._mainSystemInit.InstallBindings(base.Container);
	}

	// Token: 0x04000042 RID: 66
	[SerializeField]
	private MainSystemInit _mainSystemInit;

	// Token: 0x04000043 RID: 67
	[SerializeField]
	private NoSetupDataSingleFixedSceneScenesTransitionSetupDataSO _netEaseLoginSceneTransitionSetupData;

	// Token: 0x04000044 RID: 68
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;
}
