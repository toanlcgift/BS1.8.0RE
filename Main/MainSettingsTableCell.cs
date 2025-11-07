using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000361 RID: 865
public class MainSettingsTableCell : TableCell
{
	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0000BB7C File Offset: 0x00009D7C
	// (set) Token: 0x06000F40 RID: 3904 RVA: 0x0000BB6E File Offset: 0x00009D6E
	public string settingsSubMenuText
	{
		get
		{
			return this._settingsSubMenuText.text;
		}
		set
		{
			this._settingsSubMenuText.text = value;
		}
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x0003D7A8 File Offset: 0x0003B9A8
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._settingsSubMenuText.color = Color.black;
			return;
		}
		this._bgImage.enabled = false;
		this._settingsSubMenuText.color = Color.white;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0000BB89 File Offset: 0x00009D89
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			return;
		}
		this._highlightImage.enabled = base.highlighted;
	}

	// Token: 0x04000FA8 RID: 4008
	[SerializeField]
	private TextMeshProUGUI _settingsSubMenuText;

	// Token: 0x04000FA9 RID: 4009
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000FAA RID: 4010
	[SerializeField]
	private Image _highlightImage;
}
