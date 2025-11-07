using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000368 RID: 872
public class TextMeshProButton : MonoBehaviour
{
	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0000BCC6 File Offset: 0x00009EC6
	public TextMeshProUGUI text
	{
		get
		{
			return this._text;
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0000BCCE File Offset: 0x00009ECE
	public Button button
	{
		get
		{
			return this._button;
		}
	}

	// Token: 0x04000FC2 RID: 4034
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000FC3 RID: 4035
	[SerializeField]
	private Button _button;
}
