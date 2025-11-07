using System;
using UnityEngine;
using Zenject;

// Token: 0x020002CC RID: 716
public class StandardLevelFinishedController : MonoBehaviour
{
	// Token: 0x06000C1B RID: 3099 RVA: 0x000097B8 File Offset: 0x000079B8
	protected void Start()
	{
		this._gameplayManager.levelFinishedEvent += this.HandleLevelFinished;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x000097D1 File Offset: 0x000079D1
	protected void OnDestroy()
	{
		if (this._gameplayManager != null)
		{
			this._gameplayManager.levelFinishedEvent -= this.HandleLevelFinished;
		}
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x000097F2 File Offset: 0x000079F2
	private void HandleLevelFinished()
	{
		this.StartLevelFinished();
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00035D14 File Offset: 0x00033F14
	private void StartLevelFinished()
	{
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Cleared, LevelCompletionResults.LevelEndAction.None);
		this._standardLevelSceneSetupData.Finish(levelCompletionResults);
	}

	// Token: 0x04000CC6 RID: 3270
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;

	// Token: 0x04000CC7 RID: 3271
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelSceneSetupData;

	// Token: 0x04000CC8 RID: 3272
	[Inject]
	private ILevelEndActions _gameplayManager;
}
