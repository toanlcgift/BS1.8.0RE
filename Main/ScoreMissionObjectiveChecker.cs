using System;
using Zenject;

// Token: 0x020002DC RID: 732
public class ScoreMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C65 RID: 3173 RVA: 0x00009AEC File Offset: 0x00007CEC
	protected void OnDestroy()
	{
		if (this._scoreController != null)
		{
			this._scoreController.scoreDidChangeEvent -= this.HandleScoreDidChange;
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00009B13 File Offset: 0x00007D13
	private void HandleScoreDidChange(int rawScore, int modifiedScore)
	{
		base.checkedValue = rawScore;
		base.CheckAndUpdateStatus();
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0003657C File Offset: 0x0003477C
	protected override void Init()
	{
		this._scoreController.scoreDidChangeEvent -= this.HandleScoreDidChange;
		this._scoreController.scoreDidChangeEvent += this.HandleScoreDidChange;
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min || this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			base.status = MissionObjectiveChecker.Status.NotClearedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
	}

	// Token: 0x04000CEB RID: 3307
	[Inject]
	private ScoreController _scoreController;
}
