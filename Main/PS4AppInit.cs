using System;
using UnityEngine;
using Zenject;

// Token: 0x0200003D RID: 61
public class PS4AppInit : AppInit
{
	// Token: 0x060000FA RID: 250 RVA: 0x00002B57 File Offset: 0x00000D57
	protected override void AppStartAndMultiSceneEditorSetup()
	{
		Debug.LogError("Trying to run Platform Init Scene on a different platform.");
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00002B63 File Offset: 0x00000D63
	protected override void RepeatableSetup()
	{
		this._mainSettingsModel.Load(false);
		MainSettingsDefaultValues.SetFixedDefaultValues(this._mainSettingsModel);
		this._mainSystemInit.Init();
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00002B87 File Offset: 0x00000D87
	protected override void TransitionToNextScene()
	{
		this._defaultScenesTransitionsFromInit.TransitionToNextScene(base.GetAppStartType() == AppInit.AppStartType.AppRestart);
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00016D98 File Offset: 0x00014F98
	public override void InstallBindings()
	{
		base.Container.Bind<MenuScenesTransitionSetupDataSO>().FromInstance(this._defaultScenesTransitionsFromInit.mainMenuScenesTransitionSetupData).AsSingle();
		base.Container.Bind<IAnalyticsModel>().To<NoAnalyticsModel>().AsSingle();
		this._mainSystemInit.InstallBindings(base.Container);
	}

	// Token: 0x040000D0 RID: 208
	[SerializeField]
	private MainSystemInit _mainSystemInit;

	// Token: 0x040000D1 RID: 209
	[SerializeField]
	private DefaultScenesTransitionsFromInit _defaultScenesTransitionsFromInit;

	// Token: 0x040000D2 RID: 210
	[SerializeField]
	private AppInitScenesTransitionSetupDataContainerSO _appInitScenesTransitionSetupDataContainer;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040000D4 RID: 212
	[SerializeField]
	private PS4ActivePublisherSKUSettingsSO _activePublisherSKUSettingsSO;

	// Token: 0x040000D5 RID: 213
	[Inject]
	private GameScenesManager _gameScenesManager;
}
