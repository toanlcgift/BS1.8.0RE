using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
[Serializable]
public class NamedPreset
{
	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000607 RID: 1543 RVA: 0x000057A5 File Offset: 0x000039A5
	public string presetNameLocalizationKey
	{
		get
		{
			return this._presetNameLocalizationKey;
		}
	}

	// Token: 0x0400068F RID: 1679
	[SerializeField]
	[LocalizationKey]
	private string _presetNameLocalizationKey;
}
