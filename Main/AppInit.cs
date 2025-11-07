using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Zenject;

// Token: 0x02000033 RID: 51
public abstract class AppInit : MonoInstaller
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x060000D1 RID: 209 RVA: 0x000029DE File Offset: 0x00000BDE
	protected GameScenesManager gameScenesManager
	{
		get
		{
			return this._gameScenesManager;
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000167AC File Offset: 0x000149AC
	public override void Start()
	{
		base.Start();
		File.WriteAllText("ahihi.txt", "AppInit");
		AppInit.AppStartType appStartType = this.GetAppStartType();
		if (appStartType == AppInit.AppStartType.AppStart || appStartType == AppInit.AppStartType.AppRestart)
		{
			this._cameraGO.SetActive(true);
		}
		this._gameScenesManager.beforeDismissingScenesEvent += this.HandleBeforeDismissingScenes;
		base.StartCoroutine(this.StartCoroutine());
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x000029E6 File Offset: 0x00000BE6
	private IEnumerator StartCoroutine()
	{
		AppInit.AppStartType startType = this.GetAppStartType();
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		if (startType == AppInit.AppStartType.AppStart || startType == AppInit.AppStartType.MultiSceneEditor)
		{
			this.AppStartAndMultiSceneEditorSetup();
		}
		this.RepeatableSetup();
		yield return null;
		if (startType != AppInit.AppStartType.MultiSceneEditor)
		{
			WaitUntil waitUntil = new WaitUntil(() => SplashScreen.isFinished);
			yield return waitUntil;
			this.TransitionToNextScene();
		}
		yield break;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000029F5 File Offset: 0x00000BF5
	protected void OnDestroy()
	{
		if (this._gameScenesManager != null)
		{
			this._gameScenesManager.beforeDismissingScenesEvent -= this.HandleBeforeDismissingScenes;
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00002A1C File Offset: 0x00000C1C
	private void HandleBeforeDismissingScenes()
	{
		this._gameScenesManager.beforeDismissingScenesEvent -= this.HandleBeforeDismissingScenes;
		this._cameraGO.SetActive(false);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0001680C File Offset: 0x00014A0C
	protected AppInit.AppStartType GetAppStartType()
	{
		if (this._sceneSetupData.appInitOverrideStartType != AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.DoNotOverride)
		{
			switch (this._sceneSetupData.appInitOverrideStartType)
			{
			case AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.AppStart:
				return AppInit.AppStartType.AppStart;
			case AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.AppRestart:
				return AppInit.AppStartType.AppRestart;
			case AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.MultiSceneEditor:
				return AppInit.AppStartType.MultiSceneEditor;
			default:
				return AppInit.AppStartType.AppRestart;
			}
		}
		else
		{
			if (SceneManager.sceneCount != 1)
			{
				return AppInit.AppStartType.MultiSceneEditor;
			}
			return AppInit.AppStartType.AppStart;
		}
	}

	// Token: 0x060000D7 RID: 215
	protected abstract void AppStartAndMultiSceneEditorSetup();

	// Token: 0x060000D8 RID: 216
	protected abstract void RepeatableSetup();

	// Token: 0x060000D9 RID: 217
	protected abstract void TransitionToNextScene();

	// Token: 0x040000A7 RID: 167
	[SerializeField]
	private GameObject _cameraGO;

	// Token: 0x040000A8 RID: 168
	[InjectOptional]
	private AppInitScenesTransitionSetupDataSO.AppInitSceneSetupData _sceneSetupData = new AppInitScenesTransitionSetupDataSO.AppInitSceneSetupData(AppInitScenesTransitionSetupDataSO.AppInitOverrideStartType.DoNotOverride);

	// Token: 0x040000A9 RID: 169
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x02000034 RID: 52
	public enum AppStartType
	{
		// Token: 0x040000AB RID: 171
		AppStart,
		// Token: 0x040000AC RID: 172
		AppRestart,
		// Token: 0x040000AD RID: 173
		MultiSceneEditor
	}
}
