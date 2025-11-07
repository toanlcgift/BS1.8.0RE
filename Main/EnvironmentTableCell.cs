using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000372 RID: 882
public class EnvironmentTableCell : TableCell
{
	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0000BEFF File Offset: 0x0000A0FF
	// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0000BEF1 File Offset: 0x0000A0F1
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

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0000BF0C File Offset: 0x0000A10C
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0000BF0C File Offset: 0x0000A10C
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0003E708 File Offset: 0x0003C908
	private void RefreshVisuals()
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._text.color = (base.highlighted ? this._selectedHighlightElementsColor : Color.black);
			return;
		}
		this._bgImage.enabled = false;
		this._text.color = Color.white;
		this._highlightImage.enabled = base.highlighted;
	}

	// Token: 0x04000FFB RID: 4091
	[SerializeField]
	private Color _selectedHighlightElementsColor = new Color(0f, 0.7529412f, 1f, 1f);

	// Token: 0x04000FFC RID: 4092
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000FFD RID: 4093
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000FFE RID: 4094
	[SerializeField]
	private Image _highlightImage;
}
