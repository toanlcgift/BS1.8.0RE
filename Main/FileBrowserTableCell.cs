using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000374 RID: 884
public class FileBrowserTableCell : TableCell
{
	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0000BF79 File Offset: 0x0000A179
	// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0000BF6B File Offset: 0x0000A16B
	public string text
	{
		get
		{
			return this._text.text;
		}
		set
		{
			this._text.text = value;
		}
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x0003E838 File Offset: 0x0003CA38
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._text.color = Color.black;
			return;
		}
		this._bgImage.enabled = false;
		this._text.color = Color.white;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x0000BF86 File Offset: 0x0000A186
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			return;
		}
		this._highlightImage.enabled = base.highlighted;
	}

	// Token: 0x04001003 RID: 4099
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04001004 RID: 4100
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04001005 RID: 4101
	[SerializeField]
	private Image _highlightImage;
}
