using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000348 RID: 840
public class ColorSchemeColorToggleController : MonoBehaviour
{
	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0000B3B1 File Offset: 0x000095B1
	public Toggle toggle
	{
		get
		{
			return this._toggle;
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0000B3B9 File Offset: 0x000095B9
	// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0003BBA4 File Offset: 0x00039DA4
	public Color color
	{
		get
		{
			return this._colorGraphics[0].color;
		}
		set
		{
			Graphic[] colorGraphics = this._colorGraphics;
			for (int i = 0; i < colorGraphics.Length; i++)
			{
				colorGraphics[i].color = value;
			}
		}
	}

	// Token: 0x04000F14 RID: 3860
	[SerializeField]
	private Graphic[] _colorGraphics;

	// Token: 0x04000F15 RID: 3861
	[SerializeField]
	private Toggle _toggle;
}
