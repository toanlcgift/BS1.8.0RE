using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000429 RID: 1065
public class ResultObjectiveListItem : MonoBehaviour
{
	// Token: 0x170003DA RID: 986
	// (set) Token: 0x06001454 RID: 5204 RVA: 0x0000F572 File Offset: 0x0000D772
	public Color iconColor
	{
		set
		{
			this._iconGlow.color = value;
		}
	}

	// Token: 0x170003DB RID: 987
	// (set) Token: 0x06001455 RID: 5205 RVA: 0x0000F580 File Offset: 0x0000D780
	public Sprite icon
	{
		set
		{
			this._icon.sprite = value;
		}
	}

	// Token: 0x170003DC RID: 988
	// (set) Token: 0x06001456 RID: 5206 RVA: 0x0000F58E File Offset: 0x0000D78E
	public Sprite iconGlow
	{
		set
		{
			this._iconGlow.sprite = value;
		}
	}

	// Token: 0x170003DD RID: 989
	// (set) Token: 0x06001457 RID: 5207 RVA: 0x0000F59C File Offset: 0x0000D79C
	public string title
	{
		set
		{
			this._titleText.text = value;
		}
	}

	// Token: 0x170003DE RID: 990
	// (set) Token: 0x06001458 RID: 5208 RVA: 0x0000F5AA File Offset: 0x0000D7AA
	public string conditionText
	{
		set
		{
			this._conditionText.text = value;
		}
	}

	// Token: 0x170003DF RID: 991
	// (set) Token: 0x06001459 RID: 5209 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
	public bool hideConditionText
	{
		set
		{
			this._conditionText.gameObject.SetActive(!value);
		}
	}

	// Token: 0x170003E0 RID: 992
	// (set) Token: 0x0600145A RID: 5210 RVA: 0x0000F5CE File Offset: 0x0000D7CE
	public string valueText
	{
		set
		{
			this._valueText.text = value;
		}
	}

	// Token: 0x170003E1 RID: 993
	// (set) Token: 0x0600145B RID: 5211 RVA: 0x0000F5DC File Offset: 0x0000D7DC
	public bool hideValueText
	{
		set
		{
			this._valueText.gameObject.SetActive(!value);
		}
	}

	// Token: 0x040013FA RID: 5114
	[SerializeField]
	private Image _icon;

	// Token: 0x040013FB RID: 5115
	[SerializeField]
	private Image _iconGlow;

	// Token: 0x040013FC RID: 5116
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x040013FD RID: 5117
	[SerializeField]
	private TextMeshProUGUI _conditionText;

	// Token: 0x040013FE RID: 5118
	[SerializeField]
	private TextMeshProUGUI _valueText;
}
