using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class StandardLevelReturnToMenuController : MonoBehaviour, IReturnToMenuController
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x00035D64 File Offset: 0x00033F64
	public void ReturnToMenu()
	{
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.None, LevelCompletionResults.LevelEndAction.Quit);
		this._standardLevelSceneSetupData.Finish(levelCompletionResults);
	}

	// Token: 0x04000CCB RID: 3275
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelSceneSetupData;

	// Token: 0x04000CCC RID: 3276
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;
}
