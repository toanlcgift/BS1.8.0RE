using System;
using System.Collections.Generic;

// Token: 0x020001E5 RID: 485
public class ColorSchemesSettings
{
	// Token: 0x1700020D RID: 525
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000641F File Offset: 0x0000461F
	// (set) Token: 0x0600076D RID: 1901 RVA: 0x00006416 File Offset: 0x00004616
	public string selectedColorSchemeId
	{
		get
		{
			return this._selectedColorSchemeId;
		}
		set
		{
			this._selectedColorSchemeId = value;
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002843C File Offset: 0x0002663C
	public ColorSchemesSettings(ColorScheme[] colorSchemes)
	{
		this._colorSchemesList = new List<ColorScheme>(colorSchemes);
		this._colorSchemesDict = new Dictionary<string, ColorScheme>(this._colorSchemesList.Count);
		foreach (ColorScheme colorScheme in colorSchemes)
		{
			this._colorSchemesDict[colorScheme.colorSchemeId] = colorScheme;
		}
		this._selectedColorSchemeId = colorSchemes[0].colorSchemeId;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00006427 File Offset: 0x00004627
	public ColorSchemesSettings(ColorSchemeSO[] colorSchemeSOs) : this(ColorSchemesSettings.ConvertColorSchemeSOs(colorSchemeSOs))
	{
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x000284A8 File Offset: 0x000266A8
	private static ColorScheme[] ConvertColorSchemeSOs(ColorSchemeSO[] colorSchemeSOs)
	{
		ColorScheme[] array = new ColorScheme[colorSchemeSOs.Length];
		for (int i = 0; i < colorSchemeSOs.Length; i++)
		{
			array[i] = new ColorScheme(colorSchemeSOs[i]);
		}
		return array;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00006435 File Offset: 0x00004635
	public int GetNumberOfColorSchemes()
	{
		return this._colorSchemesList.Count;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00006442 File Offset: 0x00004642
	public ColorScheme GetColorSchemeForIdx(int idx)
	{
		return this._colorSchemesList[idx];
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00006450 File Offset: 0x00004650
	public ColorScheme GetColorSchemeForId(string id)
	{
		return this._colorSchemesDict[id];
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x000284D8 File Offset: 0x000266D8
	public void SetColorSchemeForId(ColorScheme colorScheme)
	{
		this._colorSchemesDict[colorScheme.colorSchemeId] = colorScheme;
		for (int i = 0; i < this._colorSchemesList.Count; i++)
		{
			if (this._colorSchemesList[i].colorSchemeId == colorScheme.colorSchemeId)
			{
				this._colorSchemesList[i] = colorScheme;
				return;
			}
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0000645E File Offset: 0x0000465E
	public ColorScheme GetSelectedColorScheme()
	{
		return this._colorSchemesDict[this._selectedColorSchemeId];
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0002853C File Offset: 0x0002673C
	public int GetSelectedColorSchemeIdx()
	{
		for (int i = 0; i < this._colorSchemesList.Count; i++)
		{
			if (this._colorSchemesList[i].colorSchemeId == this._selectedColorSchemeId)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x040007D3 RID: 2003
	public bool overrideDefaultColors;

	// Token: 0x040007D4 RID: 2004
	private List<ColorScheme> _colorSchemesList;

	// Token: 0x040007D5 RID: 2005
	private Dictionary<string, ColorScheme> _colorSchemesDict;

	// Token: 0x040007D6 RID: 2006
	private string _selectedColorSchemeId;
}
