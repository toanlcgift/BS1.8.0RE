using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class TutorialRestartController : MonoBehaviour, ILevelRestartController
{
	// Token: 0x06000C24 RID: 3108 RVA: 0x000097FA File Offset: 0x000079FA
	public void RestartLevel()
	{
		this._tutorialSceneSetupData.Finish(TutorialScenesTransitionSetupDataSO.TutorialEndStateType.Restart);
	}

	// Token: 0x04000CCD RID: 3277
	[SerializeField]
	private TutorialScenesTransitionSetupDataSO _tutorialSceneSetupData;
}
