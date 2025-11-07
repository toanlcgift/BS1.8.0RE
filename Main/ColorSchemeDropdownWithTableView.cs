using System;
using HMUI;
using TMPro;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class ColorSchemeDropdownWithTableView : DropdownWithTableView
{
	// Token: 0x06000EB3 RID: 3763 RVA: 0x0000B45D File Offset: 0x0000965D
	public void SetData(string schemeName, Color saberAColor, Color saberBColor, Color environment0Color, Color environment1Color, Color obstacleColor)
	{
		this._schemeNameText.text = schemeName;
		this._colorSchemeView.SetColors(saberAColor, saberBColor, environment0Color, environment1Color, obstacleColor);
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0000B47E File Offset: 0x0000967E
	public void SetData(ColorScheme colorScheme)
	{
		this.SetData(colorScheme.colorSchemeName, colorScheme.saberAColor, colorScheme.saberBColor, colorScheme.environmentColor0, colorScheme.environmentColor1, colorScheme.obstaclesColor);
	}

	// Token: 0x04000F1F RID: 3871
	[SerializeField]
	private TextMeshProUGUI _schemeNameText;

	// Token: 0x04000F20 RID: 3872
	[SerializeField]
	private ColorSchemeView _colorSchemeView;
}
