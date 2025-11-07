using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class PCAppInit : AppInit
{
	// Token: 0x060000F0 RID: 240 RVA: 0x00002ABB File Offset: 0x00000CBB
	protected override void AppStartAndMultiSceneEditorSetup()
	{
		this._steamInit.Init();
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00002AC8 File Offset: 0x00000CC8
	protected override void RepeatableSetup()
	{
		this._mainSettingsModel.Load(false);
		MainSettingsDefaultValues.SetFixedDefaultValues(this._mainSettingsModel);
		this._mainSystemInit.Init();
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00002AEC File Offset: 0x00000CEC
	protected override void TransitionToNextScene()
	{
		this._defaultScenesTransitionsFromInit.TransitionToNextScene(base.GetAppStartType() == AppInit.AppStartType.AppRestart || CommandLineArguments.Contains(this._goStraightToMenuCommandArgument));
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00016CDC File Offset: 0x00014EDC
	public override void InstallBindings()
	{
		base.Container.Bind<MenuScenesTransitionSetupDataSO>().FromInstance(this._defaultScenesTransitionsFromInit.mainMenuScenesTransitionSetupData).AsSingle();
		base.Container.Bind<IAnalyticsModel>().To<UnityAnalyticsModel>().AsSingle();
		this._mainSystemInit.InstallBindings(base.Container);
	}

	// Token: 0x040000C5 RID: 197
	[SerializeField]
	private MainSystemInit _mainSystemInit;

	// Token: 0x040000C6 RID: 198
	[SerializeField]
	private OculusInit _oculusInit;

	// Token: 0x040000C7 RID: 199
	[SerializeField]
	private SteamInit _steamInit;

	// Token: 0x040000C8 RID: 200
	[SerializeField]
	private DefaultScenesTransitionsFromInit _defaultScenesTransitionsFromInit;

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040000CA RID: 202
	[SerializeField]
	private string _goStraightToMenuCommandArgument;
}
