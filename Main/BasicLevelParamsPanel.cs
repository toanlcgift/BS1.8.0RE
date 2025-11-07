using System;
using TMPro;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class BasicLevelParamsPanel : MonoBehaviour
{
	// Token: 0x170003CB RID: 971
	// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0000EFDD File Offset: 0x0000D1DD
	public float duration
	{
		set
		{
			this._durationText.text = value.MinSecDurationText();
		}
	}

	// Token: 0x170003CC RID: 972
	// (set) Token: 0x060013F5 RID: 5109 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
	public float bpm
	{
		set
		{
			this._bpmText.text = value.ToString();
		}
	}

	// Token: 0x040013B4 RID: 5044
	[SerializeField]
	private TextMeshProUGUI _durationText;

	// Token: 0x040013B5 RID: 5045
	[SerializeField]
	private TextMeshProUGUI _bpmText;
}
