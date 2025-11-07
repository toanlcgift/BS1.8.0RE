using System;
using UnityEngine;
using Zenject;

// Token: 0x0200003E RID: 62
public class QuestShowcaseAppInit : AppInit
{
	// Token: 0x060000FF RID: 255 RVA: 0x00002B9D File Offset: 0x00000D9D
	protected override void AppStartAndMultiSceneEditorSetup()
	{
		Debug.LogError("Trying to run Platform Init Scene on a different platform.");
		this._oculusInit.Init();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00002BB4 File Offset: 0x00000DB4
	protected override void RepeatableSetup()
	{
		this._mainSettingsModel.Load(false);
		MainSettingsDefaultValues.SetFixedDefaultValues(this._mainSettingsModel);
		this._mainSystemInit.Init();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00002BD8 File Offset: 0x00000DD8
	protected override void TransitionToNextScene()
	{
		this._menuTransitionSetupData.Init();
		this._gameScenesManager.ReplaceScenes(this._menuTransitionSetupData, 0f, null, null);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00002BFD File Offset: 0x00000DFD
	public override void InstallBindings()
	{
		base.Container.Bind<IAnalyticsModel>().To<NoAnalyticsModel>().AsSingle();
		this._mainSystemInit.InstallBindings(base.Container);
	}

	// Token: 0x040000D6 RID: 214
	[SerializeField]
	private MainSystemInit _mainSystemInit;

	// Token: 0x040000D7 RID: 215
	[SerializeField]
	private OculusInit _oculusInit;

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	private MenuScenesTransitionSetupDataSO _menuTransitionSetupData;

	// Token: 0x040000D9 RID: 217
	[Space]
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040000DA RID: 218
	[Inject]
	private GameScenesManager _gameScenesManager;
}
