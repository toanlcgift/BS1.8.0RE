using System;
using Zenject;

// Token: 0x020002D3 RID: 723
public class EnergyMissionObjectiveChecker : MissionObjectiveChecker
{
	// Token: 0x06000C30 RID: 3120 RVA: 0x00009884 File Offset: 0x00007A84
	protected void OnDestroy()
	{
		if (this._energyCounter)
		{
			this._energyCounter.gameEnergyDidChangeEvent -= this.HandleEnergyDidChange;
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x000098AA File Offset: 0x00007AAA
	private void HandleEnergyDidChange(float energy)
	{
		base.checkedValue = (int)(energy * 100f);
		this.CheckAndUpdateStatus();
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00035E9C File Offset: 0x0003409C
	protected override void Init()
	{
		this._energyCounter.gameEnergyDidChangeEvent -= this.HandleEnergyDidChange;
		this._energyCounter.gameEnergyDidChangeEvent += this.HandleEnergyDidChange;
		base.checkedValue = (int)(this._energyCounter.energy * 100f);
		this.CheckAndUpdateStatus();
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00035EF8 File Offset: 0x000340F8
	private void CheckAndUpdateStatus()
	{
		if ((this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min && base.checkedValue >= this._missionObjective.referenceValue) || (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Max && base.checkedValue <= this._missionObjective.referenceValue) || (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal && base.checkedValue == this._missionObjective.referenceValue))
		{
			base.status = MissionObjectiveChecker.Status.NotFailedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotClearedYet;
	}

	// Token: 0x04000CD1 RID: 3281
	[Inject]
	private GameEnergyCounter _energyCounter;
}
