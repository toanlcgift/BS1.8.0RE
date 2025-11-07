using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034B RID: 843
public class ColorSchemeTableCell : TableCell
{
	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0000B4C0 File Offset: 0x000096C0
	// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0000B4B2 File Offset: 0x000096B2
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

	// Token: 0x17000325 RID: 805
	// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0000B4CD File Offset: 0x000096CD
	public bool showEditIcon
	{
		set
		{
			this._editIcon.enabled = value;
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0000B4DB File Offset: 0x000096DB
	public void SetColors(Color saberAColor, Color saberBColor, Color environment0Color, Color environment1Color, Color obstacleColor)
	{
		this._colorSchemeView.SetColors(saberAColor, saberBColor, environment0Color, environment1Color, obstacleColor);
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0000B4EF File Offset: 0x000096EF
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0000B4EF File Offset: 0x000096EF
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0003BDF0 File Offset: 0x00039FF0
	private void RefreshVisuals()
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._text.color = (base.highlighted ? this._selectedHighlightElementsColor : Color.black);
			this._editIcon.color = Color.black;
			return;
		}
		this._bgImage.enabled = false;
		this._text.color = Color.white;
		this._highlightImage.enabled = base.highlighted;
		this._editIcon.color = Color.white;
	}

	// Token: 0x04000F21 RID: 3873
	[SerializeField]
	private Color _selectedHighlightElementsColor = new Color(0f, 0.7529412f, 1f, 1f);

	// Token: 0x04000F22 RID: 3874
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000F23 RID: 3875
	[SerializeField]
	private ColorSchemeView _colorSchemeView;

	// Token: 0x04000F24 RID: 3876
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F25 RID: 3877
	[SerializeField]
	private Image _highlightImage;

	// Token: 0x04000F26 RID: 3878
	[SerializeField]
	private Image _editIcon;
}
