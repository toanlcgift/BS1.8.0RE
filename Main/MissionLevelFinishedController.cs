using System;
using UnityEngine;
using Zenject;

// Token: 0x020002C6 RID: 710
public class MissionLevelFinishedController : MonoBehaviour
{
	// Token: 0x06000C06 RID: 3078 RVA: 0x000096F8 File Offset: 0x000078F8
	protected void Start()
	{
		this._gameplayManager.levelFinishedEvent += this.HandleLevelFinished;
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00009711 File Offset: 0x00007911
	protected void OnDestroy()
	{
		if (this._gameplayManager != null)
		{
			this._gameplayManager.levelFinishedEvent -= this.HandleLevelFinished;
		}
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00009732 File Offset: 0x00007932
	private void HandleLevelFinished()
	{
		this.StartLevelFinished();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00035BA0 File Offset: 0x00033DA0
	private void StartLevelFinished()
	{
		LevelCompletionResults levelCompletionResults = this._prepareLevelCompletionResults.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Cleared, LevelCompletionResults.LevelEndAction.None);
		MissionObjectiveResult[] results = this._missionObjectiveCheckersManager.GetResults();
		MissionCompletionResults levelCompletionResults2 = new MissionCompletionResults(levelCompletionResults, results);
		this._missionLevelSceneSetupData.Finish(levelCompletionResults2);
	}

	// Token: 0x04000CB0 RID: 3248
	[SerializeField]
	private PrepareLevelCompletionResults _prepareLevelCompletionResults;

	// Token: 0x04000CB1 RID: 3249
	[SerializeField]
	private MissionLevelScenesTransitionSetupDataSO _missionLevelSceneSetupData;

	// Token: 0x04000CB2 RID: 3250
	[SerializeField]
	private MissionObjectiveCheckersManager _missionObjectiveCheckersManager;

	// Token: 0x04000CB3 RID: 3251
	[Inject]
	private ILevelEndActions _gameplayManager;
}
