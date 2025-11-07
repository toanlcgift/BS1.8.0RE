using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000357 RID: 855
public class FillIndicator : MonoBehaviour
{
	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0000B96A File Offset: 0x00009B6A
	// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0000B94A File Offset: 0x00009B4A
	public float fillAmount
	{
		get
		{
			return this._image.fillAmount;
		}
		set
		{
			this._image.fillAmount = value;
			this._bgImage.fillAmount = 1f - value;
		}
	}

	// Token: 0x04000F6E RID: 3950
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F6F RID: 3951
	[SerializeField]
	private Image _image;
}
