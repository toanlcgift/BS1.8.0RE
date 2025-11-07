using System;
using Polyglot;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class MissionObjectiveTypeSO : PersistentScriptableObject
{
	// Token: 0x170001CB RID: 459
	// (get) Token: 0x0600063B RID: 1595 RVA: 0x00005A49 File Offset: 0x00003C49
	public string objectiveName
	{
		get
		{
			return this._objectiveName;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x00005A51 File Offset: 0x00003C51
	public string objectiveNameLocalized
	{
		get
		{
			return Localization.Get(this._objectiveName);
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x0600063D RID: 1597 RVA: 0x00005A5E File Offset: 0x00003C5E
	public bool noConditionValue
	{
		get
		{
			return this._noConditionValue;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x0600063E RID: 1598 RVA: 0x00005A66 File Offset: 0x00003C66
	public ObjectiveValueFormatterSO objectiveValueFormater
	{
		get
		{
			return this._objectiveValueFormater;
		}
	}

	// Token: 0x040006AF RID: 1711
	[SerializeField]
	private string _objectiveName;

	// Token: 0x040006B0 RID: 1712
	[SerializeField]
	private bool _noConditionValue;

	// Token: 0x040006B1 RID: 1713
	[SerializeField]
	private ObjectiveValueFormatterSO _objectiveValueFormater;
}
