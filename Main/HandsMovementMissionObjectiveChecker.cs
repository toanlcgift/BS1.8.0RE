using System;
using Zenject;

// Token: 0x020002D6 RID: 726
public class HandsMovementMissionObjectiveChecker : SimpleValueMissionObjectiveChecker
{
	// Token: 0x06000C3D RID: 3133 RVA: 0x00009956 File Offset: 0x00007B56
	protected void OnDestroy()
	{
		if (this._saberActivityCounter != null)
		{
			this._saberActivityCounter.totalDistanceDidChangeEvent -= this.HandleTotalDistanceDidChange;
		}
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x0000997D File Offset: 0x00007B7D
	private void HandleTotalDistanceDidChange(float distance)
	{
		base.checkedValue = (int)distance;
		base.CheckAndUpdateStatus();
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00036020 File Offset: 0x00034220
	protected override void Init()
	{
		this._saberActivityCounter.totalDistanceDidChangeEvent += this.HandleTotalDistanceDidChange;
		if (this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Min || this._missionObjective.referenceValueComparisonType == MissionObjective.ReferenceValueComparisonType.Equal)
		{
			base.status = MissionObjectiveChecker.Status.NotClearedYet;
			return;
		}
		base.status = MissionObjectiveChecker.Status.NotFailedYet;
	}

	// Token: 0x04000CD4 RID: 3284
	[Inject]
	private SaberActivityCounter _saberActivityCounter;
}
