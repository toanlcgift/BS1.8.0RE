using System;
using UnityEngine;

// Token: 0x02000467 RID: 1127
[Serializable]
public class HealthWarningSceneSetupData : SceneSetupData
{
	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001540 RID: 5440 RVA: 0x0000FF12 File Offset: 0x0000E112
	public ScenesTransitionSetupDataSO nextScenesTransitionSetupData
	{
		get
		{
			return this._nextScenesTransitionSetupData;
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0000FF1A File Offset: 0x0000E11A
	public HealthWarningSceneSetupData(ScenesTransitionSetupDataSO nextScenesTransitionSetupData)
	{
		this._nextScenesTransitionSetupData = nextScenesTransitionSetupData;
	}

	// Token: 0x04001531 RID: 5425
	[SerializeField]
	private ScenesTransitionSetupDataSO _nextScenesTransitionSetupData;
}
