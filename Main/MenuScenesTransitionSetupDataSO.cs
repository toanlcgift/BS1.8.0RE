using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class MenuScenesTransitionSetupDataSO : ScenesTransitionSetupDataSO
{
	// Token: 0x0600155B RID: 5467 RVA: 0x0001004C File Offset: 0x0000E24C
	public void Init()
	{
		base.Init(new SceneInfo[]
		{
			this._menuSceneInfo,
			this._menuCoreSceneInfo,
			this._menuEnvironmentSceneInfo,
			this._menuViewControllersSceneInfo
		}, null);
	}

	// Token: 0x04001548 RID: 5448
	[SerializeField]
	private SceneInfo _menuSceneInfo;

	// Token: 0x04001549 RID: 5449
	[SerializeField]
	private SceneInfo _menuCoreSceneInfo;

	// Token: 0x0400154A RID: 5450
	[SerializeField]
	private SceneInfo _menuEnvironmentSceneInfo;

	// Token: 0x0400154B RID: 5451
	[SerializeField]
	private SceneInfo _menuViewControllersSceneInfo;
}
