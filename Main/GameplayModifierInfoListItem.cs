using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000358 RID: 856
public class GameplayModifierInfoListItem : MonoBehaviour
{
	// Token: 0x1700032E RID: 814
	// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0000B977 File Offset: 0x00009B77
	public string modifierName
	{
		set
		{
			this._modifierNameText.text = value;
		}
	}

	// Token: 0x1700032F RID: 815
	// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0000B985 File Offset: 0x00009B85
	public string modifierDescription
	{
		set
		{
			this._modifierDescriptionText.text = value;
		}
	}

	// Token: 0x17000330 RID: 816
	// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0000B993 File Offset: 0x00009B93
	public Sprite modifierIcon
	{
		set
		{
			this._iconImage.sprite = value;
		}
	}

	// Token: 0x17000331 RID: 817
	// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0000B9A1 File Offset: 0x00009BA1
	public bool showSeparator
	{
		set
		{
			this._separatorImage.enabled = value;
		}
	}

	// Token: 0x04000F70 RID: 3952
	[SerializeField]
	private TextMeshProUGUI _modifierNameText;

	// Token: 0x04000F71 RID: 3953
	[SerializeField]
	private TextMeshProUGUI _modifierDescriptionText;

	// Token: 0x04000F72 RID: 3954
	[SerializeField]
	private Image _iconImage;

	// Token: 0x04000F73 RID: 3955
	[SerializeField]
	private Image _separatorImage;
}
