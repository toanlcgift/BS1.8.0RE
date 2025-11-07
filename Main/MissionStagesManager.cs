using System;
using System.Linq;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class MissionStagesManager : MonoBehaviour
{
	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06001132 RID: 4402 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
	public MissionStage firstLockedMissionStage
	{
		get
		{
			return this._firstLockedMissionStage;
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000420D0 File Offset: 0x000402D0
	public void UpdateFirtsLockedMissionStage(int numberOfClearedMissions)
	{
		if (this._missionStages == null)
		{
			this.InitStages();
		}
		this._firstLockedMissionStage = null;
		foreach (MissionStage missionStage in this._missionStages)
		{
			this._firstLockedMissionStage = missionStage;
			if (missionStage.minimumMissionsToUnlock > numberOfClearedMissions)
			{
				break;
			}
		}
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0004211C File Offset: 0x0004031C
	public void InitStages()
	{
		this._missionStages = base.GetComponentsInChildren<MissionStage>();
		this._missionStages = (from stage in this._missionStages
		orderby stage.minimumMissionsToUnlock
		select stage).ToArray<MissionStage>();
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
	public void UpdateStageLockPosition()
	{
		this.UpdateStageLockPositionAnimated(false, 0f);
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0004216C File Offset: 0x0004036C
	public void UpdateStageLockPositionAnimated(bool animated, float animationDuration)
	{
		if (this.firstLockedMissionStage != null)
		{
			this._missionStageLockView.gameObject.SetActive(true);
			this._missionStageLockView.UpdateLocalPositionY(this.firstLockedMissionStage.position.y, animated, animationDuration);
			return;
		}
		this._missionStageLockView.gameObject.SetActive(false);
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000421C8 File Offset: 0x000403C8
	public void UpdateStageLockText(int numberOfClearedMissions)
	{
		string text = numberOfClearedMissions + " / " + this.firstLockedMissionStage.minimumMissionsToUnlock;
		this._missionStageLockView.UpdateStageLockText(text);
	}

	// Token: 0x04001119 RID: 4377
	[SerializeField]
	private MissionStageLockView _missionStageLockView;

	// Token: 0x0400111A RID: 4378
	private MissionStage[] _missionStages;

	// Token: 0x0400111B RID: 4379
	private MissionStage _firstLockedMissionStage;
}
