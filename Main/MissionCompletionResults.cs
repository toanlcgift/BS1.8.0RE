using System;

// Token: 0x02000185 RID: 389
public class MissionCompletionResults
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x000249A8 File Offset: 0x00022BA8
	public bool IsMissionComplete
	{
		get
		{
			if (this.levelCompletionResults.levelEndStateType != LevelCompletionResults.LevelEndStateType.Cleared)
			{
				return false;
			}
			MissionObjectiveResult[] array = this.missionObjectiveResults;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].cleared)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x000057F0 File Offset: 0x000039F0
	public MissionCompletionResults(LevelCompletionResults levelCompletionResults, MissionObjectiveResult[] missionObjectiveResults)
	{
		this.levelCompletionResults = levelCompletionResults;
		this.missionObjectiveResults = missionObjectiveResults;
	}

	// Token: 0x04000696 RID: 1686
	public readonly LevelCompletionResults levelCompletionResults;

	// Token: 0x04000697 RID: 1687
	public readonly MissionObjectiveResult[] missionObjectiveResults;
}
