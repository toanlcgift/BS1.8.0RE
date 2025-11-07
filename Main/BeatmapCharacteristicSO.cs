using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class BeatmapCharacteristicSO : PersistentScriptableObject
{
	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x00004492 File Offset: 0x00002692
	public Sprite icon
	{
		get
		{
			return this._icon;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000449A File Offset: 0x0000269A
	public string descriptionLocalizationKey
	{
		get
		{
			return this._descriptionLocalizationKey;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060003C4 RID: 964 RVA: 0x000044A2 File Offset: 0x000026A2
	public string characteristicNameLocalizationKey
	{
		get
		{
			return this._characteristicNameLocalizationKey;
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060003C5 RID: 965 RVA: 0x000044AA File Offset: 0x000026AA
	public string serializedName
	{
		get
		{
			return this._serializedName;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060003C6 RID: 966 RVA: 0x000044B2 File Offset: 0x000026B2
	public string compoundIdPartName
	{
		get
		{
			return this._compoundIdPartName;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x060003C7 RID: 967 RVA: 0x000044BA File Offset: 0x000026BA
	public int sortingOrder
	{
		get
		{
			return this._sortingOrder;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060003C8 RID: 968 RVA: 0x000044C2 File Offset: 0x000026C2
	public bool containsRotationEvents
	{
		get
		{
			return this._containsRotationEvents;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060003C9 RID: 969 RVA: 0x000044CA File Offset: 0x000026CA
	public bool requires360Movement
	{
		get
		{
			return this._requires360Movement;
		}
	}

	// Token: 0x04000425 RID: 1061
	[SerializeField]
	private Sprite _icon;

	// Token: 0x04000426 RID: 1062
	[SerializeField]
	[LocalizationKey]
	private string _descriptionLocalizationKey;

	// Token: 0x04000427 RID: 1063
	[SerializeField]
	[LocalizationKey]
	private string _characteristicNameLocalizationKey;

	// Token: 0x04000428 RID: 1064
	[SerializeField]
	private string _serializedName;

	// Token: 0x04000429 RID: 1065
	[SerializeField]
	private string _compoundIdPartName;

	// Token: 0x0400042A RID: 1066
	[SerializeField]
	private int _sortingOrder;

	// Token: 0x0400042B RID: 1067
	[SerializeField]
	private bool _containsRotationEvents;

	// Token: 0x0400042C RID: 1068
	[SerializeField]
	private bool _requires360Movement;
}
