using System;

// Token: 0x020002DD RID: 733
public abstract class SimpleValueMissionObjectiveChecker : MissionObjectiveChecker
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x000365E4 File Offset: 0x000347E4
	protected void CheckAndUpdateStatus()
	{
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min)
		{
			if (base.checkedValue >= this._missionObjective.referenceValue)
			{
				base.status = MissionObjectiveChecker.Status.Cleared;
				return;
			}
		}
		else if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Max)
		{
			if (base.checkedValue > this._missionObjective.referenceValue)
			{
				base.status = MissionObjectiveChecker.Status.Failed;
				return;
			}
		}
		else if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			if (base.status == MissionObjectiveChecker.Status.NotClearedYet)
			{
				if (base.checkedValue == this._missionObjective.referenceValue)
				{
					base.status = MissionObjectiveChecker.Status.NotFailedYet;
					return;
				}
			}
			else if (base.status == MissionObjectiveChecker.Status.NotFailedYet && base.checkedValue != this._missionObjective.referenceValue)
			{
				base.status = MissionObjectiveChecker.Status.Failed;
			}
		}
	}
}
