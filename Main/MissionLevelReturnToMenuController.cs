using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class MissionLevelReturnToMenuController : MonoBehaviour, IReturnToMenuController
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x00035C0C File Offset: 0x00033E0C
	public void ReturnToMenu()
	{
		MissionCompletionResults levelCompletionResults = new MissionCompletionResults(this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.None, LevelCompletionResults.LevelEndAction.Quit), null);
		this._missionLevelSceneSetupData.Finish(levelCompletionResults);
	}

	// Token: 0x04000CB6 RID: 3254
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelSceneSetupData;

	// Token: 0x04000CB7 RID: 3255
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;
}
