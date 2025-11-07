using System;
using System.Collections.Generic;

// Token: 0x020001F3 RID: 499
public class OverrideEnvironmentSettings
{
	// Token: 0x06000784 RID: 1924 RVA: 0x000064DC File Offset: 0x000046DC
	public void SetEnvironmentInfoForType(EnvironmentTypeSO environmentType, EnvironmentInfoSO environmentInfo)
	{
		this._data[environmentType] = environmentInfo;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00028618 File Offset: 0x00026818
	public EnvironmentInfoSO GetOverrideEnvironmentInfoForType(EnvironmentTypeSO environmentType)
	{
		EnvironmentInfoSO result = null;
		if (this._data.TryGetValue(environmentType, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x04000821 RID: 2081
	public bool overrideEnvironments;

	// Token: 0x04000822 RID: 2082
	private Dictionary<EnvironmentTypeSO, EnvironmentInfoSO> _data = new Dictionary<EnvironmentTypeSO, EnvironmentInfoSO>();
}
