using System;
using TMPro;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class ObjectiveListItem : MonoBehaviour
{
	// Token: 0x170003D7 RID: 983
	// (set) Token: 0x0600144F RID: 5199 RVA: 0x0000F538 File Offset: 0x0000D738
	public string title
	{
		set
		{
			this._titleText.text = value;
		}
	}

	// Token: 0x170003D8 RID: 984
	// (set) Token: 0x06001450 RID: 5200 RVA: 0x0000F546 File Offset: 0x0000D746
	public string conditionText
	{
		set
		{
			this._conditionText.text = value;
		}
	}

	// Token: 0x170003D9 RID: 985
	// (set) Token: 0x06001451 RID: 5201 RVA: 0x0000F554 File Offset: 0x0000D754
	public bool hideCondition
	{
		set
		{
			this._conditionText.gameObject.SetActive(!value);
		}
	}

	// Token: 0x040013F8 RID: 5112
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x040013F9 RID: 5113
	[SerializeField]
	private TextMeshProUGUI _conditionText;
}
