using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class AchievementIdsModelSO : PersistentScriptableObject
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000199 RID: 409 RVA: 0x000034B2 File Offset: 0x000016B2
	public List<AchievementSO> achievementsIds
	{
		get
		{
			return this._achievementsIds;
		}
	}

	// Token: 0x0400018E RID: 398
	[SerializeField]
	private List<AchievementSO> _achievementsIds = new List<AchievementSO>();
}
