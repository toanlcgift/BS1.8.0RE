using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000367 RID: 871
public class TextButton : MonoBehaviour
{
	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0000BCB6 File Offset: 0x00009EB6
	public Text text
	{
		get
		{
			return this._text;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0000BCBE File Offset: 0x00009EBE
	public Button button
	{
		get
		{
			return this._button;
		}
	}

	// Token: 0x04000FC0 RID: 4032
	[SerializeField]
	private Text _text;

	// Token: 0x04000FC1 RID: 4033
	[SerializeField]
	private Button _button;
}
