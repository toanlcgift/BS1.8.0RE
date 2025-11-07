using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
[Serializable]
public class NetEaseSceneSetupData : SceneSetupData
{
	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001543 RID: 5443 RVA: 0x0000FF5E File Offset: 0x0000E15E
	public ScenesTransitionSetupDataSO nextScenesTransitionSetupData
	{
		get
		{
			return this._nextScenesTransitionSetupData;
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0000FF66 File Offset: 0x0000E166
	public NetEaseSceneSetupData(ScenesTransitionSetupDataSO nextScenesTransitionSetupData)
	{
		this._nextScenesTransitionSetupData = nextScenesTransitionSetupData;
	}

	// Token: 0x04001538 RID: 5432
	[SerializeField]
	private ScenesTransitionSetupDataSO _nextScenesTransitionSetupData;
}
