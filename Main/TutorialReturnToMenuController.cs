using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class TutorialReturnToMenuController : MonoBehaviour, IReturnToMenuController
{
	// Token: 0x06000C26 RID: 3110 RVA: 0x00009808 File Offset: 0x00007A08
	public void ReturnToMenu()
	{
		this._tutorialSceneSetupData.Finish(TutorialScenesTransitionSetupDataSO.TutorialEndStateType.ReturnToMenu);
	}

	// Token: 0x04000CCE RID: 3278
	[SerializeField]
	private TutorialScenesTransitionSetupDataSO _tutorialSceneSetupData;
}
