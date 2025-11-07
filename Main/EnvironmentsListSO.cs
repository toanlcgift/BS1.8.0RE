using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class EnvironmentsListSO : PersistentScriptableObject
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060002C8 RID: 712 RVA: 0x00003CF5 File Offset: 0x00001EF5
	public EnvironmentInfoSO[] environmentInfos
	{
		get
		{
			return this._environmentInfos;
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0001E480 File Offset: 0x0001C680
	public EnvironmentInfoSO GetEnviromentInfoBySerializedName(string environmentSerializedName)
	{
		foreach (EnvironmentInfoSO environmentInfoSO in this._environmentInfos)
		{
			if (environmentInfoSO.serializedName == environmentSerializedName)
			{
				return environmentInfoSO;
			}
		}
		return null;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
	public List<EnvironmentInfoSO> GetAllEnvironmentInfosWithType(EnvironmentTypeSO environmentType)
	{
		List<EnvironmentInfoSO> list = new List<EnvironmentInfoSO>();
		foreach (EnvironmentInfoSO environmentInfoSO in this._environmentInfos)
		{
			if (environmentInfoSO.environmentType == environmentType)
			{
				list.Add(environmentInfoSO);
			}
		}
		return list;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0001E4FC File Offset: 0x0001C6FC
	public EnvironmentInfoSO GetFirstEnvironmentInfoWithType(EnvironmentTypeSO environmentType)
	{
		List<EnvironmentInfoSO> allEnvironmentInfosWithType = this.GetAllEnvironmentInfosWithType(environmentType);
		if (allEnvironmentInfosWithType != null && allEnvironmentInfosWithType.Count > 0)
		{
			return allEnvironmentInfosWithType[0];
		}
		return null;
	}

	// Token: 0x04000354 RID: 852
	[SerializeField]
	private EnvironmentInfoSO[] _environmentInfos;
}
