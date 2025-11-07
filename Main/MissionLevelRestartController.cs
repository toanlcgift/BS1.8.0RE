using System;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class MissionLevelRestartController : MonoBehaviour, ILevelRestartController
{
	// Token: 0x06000C0B RID: 3083 RVA: 0x00035BDC File Offset: 0x00033DDC
	public void RestartLevel()
	{
		MissionCompletionResults levelCompletionResults = new MissionCompletionResults(this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.None, LevelCompletionResults.LevelEndAction.Restart), null);
		this._missionLevelSceneSetupData.Finish(levelCompletionResults);
	}

	// Token: 0x04000CB4 RID: 3252
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelSceneSetupData;

	// Token: 0x04000CB5 RID: 3253
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;
}
