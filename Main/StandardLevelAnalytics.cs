using System;
using UnityEngine;
using Zenject;

// Token: 0x0200002F RID: 47
public class StandardLevelAnalytics : MonoBehaviour
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00002937 File Offset: 0x00000B37
	protected void Start()
	{
		this._standardLevelScenesTransitionSetupData.didFinishEvent += this.HandleStandardLevelDidFinishEvent;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00002950 File Offset: 0x00000B50
	protected void OnDestroy()
	{
		this._standardLevelScenesTransitionSetupData.didFinishEvent -= this.HandleStandardLevelDidFinishEvent;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000166DC File Offset: 0x000148DC
	private void HandleStandardLevelDidFinishEvent(StandardLevelScenesTransitionSetupDataSO standardLevelScenesTransitionSetupData, LevelCompletionResults levelCompletionResults)
	{
		if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Restart)
		{
			this._analyticsModel.LogEvent("Standard Level", "Restart", LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap), (long)levelCompletionResults.modifiedScore);
			return;
		}
		if (levelCompletionResults.levelEndAction == LevelCompletionResults.LevelEndAction.Quit)
		{
			this._analyticsModel.LogEvent("Standard Level", "Quit", LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap), (long)levelCompletionResults.modifiedScore);
			return;
		}
		if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			this._analyticsModel.LogEvent("Standard Level", "Cleared", LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap), (long)levelCompletionResults.modifiedScore);
			return;
		}
		if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
		{
			this._analyticsModel.LogEvent("Standard Level", "Failed", LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap), (long)levelCompletionResults.modifiedScore);
		}
	}

	// Token: 0x040000A2 RID: 162
	[SerializeField]
	private StandardLevelScenesTransitionSetupDataSO _standardLevelScenesTransitionSetupData;

	// Token: 0x040000A3 RID: 163
	[Inject]
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x040000A4 RID: 164
	[Inject]
	private IAnalyticsModel _analyticsModel;
}
