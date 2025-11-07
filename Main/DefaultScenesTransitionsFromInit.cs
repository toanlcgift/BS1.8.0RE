using System;
using UnityEngine;
using Zenject;

// Token: 0x02000037 RID: 55
public class DefaultScenesTransitionsFromInit : MonoBehaviour
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002A7F File Offset: 0x00000C7F
	public MenuScenesTransitionSetupDataSO mainMenuScenesTransitionSetupData
	{
		get
		{
			return this._mainMenuScenesTransitionSetupData;
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00016944 File Offset: 0x00014B44
	public void TransitionToNextScene(bool goStraightToMenu)
	{
		this._mainMenuScenesTransitionSetupData.Init();
		ScenesTransitionSetupDataSO mainMenuScenesTransitionSetupData = this._mainMenuScenesTransitionSetupData;
		if (goStraightToMenu)
		{
			this._gameScenesManager.ReplaceScenes(mainMenuScenesTransitionSetupData, 0f, null, null);
			return;
		}
		HealthWarningSceneSetupData healthWarningSceneSetupData = new HealthWarningSceneSetupData(mainMenuScenesTransitionSetupData);
		this._healthWarningScenesTransitionSetupData.Init(healthWarningSceneSetupData);
		this._gameScenesManager.ReplaceScenes(this._healthWarningScenesTransitionSetupData, 0f, null, null);
	}

	// Token: 0x040000B4 RID: 180
	[SerializeField]
	private HealthWarningScenesTransitionSetupDataSO _healthWarningScenesTransitionSetupData;

	// Token: 0x040000B5 RID: 181
	[SerializeField]
	private MenuScenesTransitionSetupDataSO _mainMenuScenesTransitionSetupData;

	// Token: 0x040000B6 RID: 182
	[SerializeField]
	private ShaderWarmupScenesTransitionSetupDataSO _shaderWarmupScenesTransitionSetupData;

	// Token: 0x040000B7 RID: 183
	[Inject]
	private GameScenesManager _gameScenesManager;
}
