using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class StandardLevelRestartController : MonoBehaviour, ILevelRestartController
{
	// Token: 0x06000C20 RID: 3104 RVA: 0x00035D3C File Offset: 0x00033F3C
	public void RestartLevel()
	{
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.None, LevelCompletionResults.LevelEndAction.Restart);
		this._standardLevelSceneSetupData.Finish(levelCompletionResults);
	}

	// Token: 0x04000CC9 RID: 3273
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelSceneSetupData;

	// Token: 0x04000CCA RID: 3274
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;
}
