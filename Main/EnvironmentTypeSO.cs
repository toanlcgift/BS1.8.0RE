using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class EnvironmentTypeSO : ScriptableObject
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003CED File Offset: 0x00001EED
	public string typeNameLocalizationKey
	{
		get
		{
			return this._typeNameLocalizationKey;
		}
	}

	// Token: 0x04000353 RID: 851
	[SerializeField]
	private string _typeNameLocalizationKey;
}
