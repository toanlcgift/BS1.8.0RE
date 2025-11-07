using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034C RID: 844
public class ColorSchemeView : MonoBehaviour
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x0000B51E File Offset: 0x0000971E
	public void SetColors(Color saberAColor, Color saberBColor, Color environment0Color, Color environment1Color, Color obstacleColor)
	{
		this._saberAColorImage.color = saberAColor;
		this._saberBColorImage.color = saberBColor;
		this._environment0ColorImage.color = environment0Color;
		this._environment1ColorImage.color = environment1Color;
		this._obstacleColorImage.color = obstacleColor;
	}

	// Token: 0x04000F27 RID: 3879
	[SerializeField]
	private Image _saberAColorImage;

	// Token: 0x04000F28 RID: 3880
	[SerializeField]
	private Image _saberBColorImage;

	// Token: 0x04000F29 RID: 3881
	[SerializeField]
	private Image _environment0ColorImage;

	// Token: 0x04000F2A RID: 3882
	[SerializeField]
	private Image _environment1ColorImage;

	// Token: 0x04000F2B RID: 3883
	[SerializeField]
	private Image _obstacleColorImage;
}
