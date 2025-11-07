using System;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class GameplayModifierParamsSO : PersistentScriptableObject
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060002DA RID: 730 RVA: 0x00003D79 File Offset: 0x00001F79
	public string modifierNameLocalizationKey
	{
		get
		{
			return this._modifierNameLocalizationKey;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060002DB RID: 731 RVA: 0x00003D81 File Offset: 0x00001F81
	public string descriptionLocalizationKey
	{
		get
		{
			return this._descriptionLocalizationKey;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060002DC RID: 732 RVA: 0x00003D89 File Offset: 0x00001F89
	public float multiplier
	{
		get
		{
			return this._multiplier;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060002DD RID: 733 RVA: 0x00003D91 File Offset: 0x00001F91
	public Sprite icon
	{
		get
		{
			return this._icon;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060002DE RID: 734 RVA: 0x00003D99 File Offset: 0x00001F99
	public GameplayModifierParamsSO[] mutuallyExclusives
	{
		get
		{
			return this._mutuallyExclusives;
		}
	}

	// Token: 0x0400035C RID: 860
	[SerializeField]
	[LocalizationKey]
	private string _modifierNameLocalizationKey;

	// Token: 0x0400035D RID: 861
	[SerializeField]
	[LocalizationKey]
	private string _descriptionLocalizationKey;

	// Token: 0x0400035E RID: 862
	[SerializeField]
	private float _multiplier;

	// Token: 0x0400035F RID: 863
	[SerializeField]
	private Sprite _icon;

	// Token: 0x04000360 RID: 864
	[SerializeField]
	private GameplayModifierParamsSO[] _mutuallyExclusives;
}
