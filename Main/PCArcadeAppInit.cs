using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class PCArcadeAppInit : AppInit
{
	// Token: 0x060000F5 RID: 245 RVA: 0x00002B10 File Offset: 0x00000D10
	protected override void AppStartAndMultiSceneEditorSetup()
	{
		this._steamInit.Init();
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00002B1D File Offset: 0x00000D1D
	protected override void RepeatableSetup()
	{
		this._mainSettingsModel.Load(false);
		MainSettingsDefaultValues.SetFixedDefaultValues(this._mainSettingsModel);
		this._mainSystemInit.Init();
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00002B41 File Offset: 0x00000D41
	protected override void TransitionToNextScene()
	{
		this._defaultScenesTransitionsFromInit.TransitionToNextScene(base.GetAppStartType() == AppInit.AppStartType.AppRestart);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00016D34 File Offset: 0x00014F34
	public override void InstallBindings()
	{
		base.Container.Bind<IAnalyticsModel>().To<NoAnalyticsModel>().AsSingle();
		if (CommandLineArguments.Contains("vrsenal"))
		{
			base.Container.Bind<VRsenalLogger>().FromComponentInNewPrefab(this._vrsenalLoggerPrefab).AsSingle().NonLazy();
		}
		this._mainSystemInit.InstallBindings(base.Container);
	}

	// Token: 0x040000CB RID: 203
	[SerializeField]
	private MainSystemInit _mainSystemInit;

	// Token: 0x040000CC RID: 204
	[SerializeField]
	private SteamInit _steamInit;

	// Token: 0x040000CD RID: 205
	[SerializeField]
	private DefaultScenesTransitionsFromInit _defaultScenesTransitionsFromInit;

	// Token: 0x040000CE RID: 206
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040000CF RID: 207
	[Space]
	[SerializeField]
	private VRsenalLogger _vrsenalLoggerPrefab;
}
