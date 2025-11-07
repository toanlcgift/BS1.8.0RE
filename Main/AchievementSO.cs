using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class AchievementSO : PersistentScriptableObject
{
	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600019B RID: 411 RVA: 0x000034CD File Offset: 0x000016CD
	public string achievementId
	{
		get
		{
			return this._achievementId;
		}
	}

	// Token: 0x0400018F RID: 399
	[SerializeField]
	private string _achievementId;
}
