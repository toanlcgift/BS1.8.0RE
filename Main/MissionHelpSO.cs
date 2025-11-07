using System;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class MissionHelpSO : PersistentScriptableObject
{
	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000594B File Offset: 0x00003B4B
	public string missionHelpId
	{
		get
		{
			return this._missionHelpId;
		}
	}

	// Token: 0x040006A3 RID: 1699
	[SerializeField]
	private string _missionHelpId;
}
