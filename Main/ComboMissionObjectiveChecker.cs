using System;
using Zenject;

// Token: 0x020002D2 RID: 722
public class ComboMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C2C RID: 3116 RVA: 0x00035E34 File Offset: 0x00034034
	protected override void Init()
	{
		this._scoreController.comboDidChangeEvent -= this.HandleComboDidChange;
		this._scoreController.comboDidChangeEvent += this.HandleComboDidChange;
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min || this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			base.status = MissionObjectiveChecker.Status.NotClearedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00009845 File Offset: 0x00007A45
	protected void OnDestroy()
	{
		if (this._scoreController != null)
		{
			this._scoreController.comboDidChangeEvent -= this.HandleComboDidChange;
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0000986C File Offset: 0x00007A6C
	private void HandleComboDidChange(int combo)
	{
		if (combo > base.checkedValue)
		{
			base.checkedValue = combo;
			base.CheckAndUpdateStatus();
		}
	}

	// Token: 0x04000CD0 RID: 3280
	[Inject]
	private ScoreController _scoreController;
}
