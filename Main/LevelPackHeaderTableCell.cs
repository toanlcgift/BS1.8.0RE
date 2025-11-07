using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200035F RID: 863
public class LevelPackHeaderTableCell : TableCell
{
	// Token: 0x06000F2F RID: 3887 RVA: 0x0000BA79 File Offset: 0x00009C79
	public void SetDataFromPack(IBeatmapLevelPack pack)
	{
		this._nameText.text = pack.packName;
		this._coverImage.sprite = pack.coverImage;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x0000BA9D File Offset: 0x00009C9D
	public void SetData(string headerText, Sprite headerSprite)
	{
		this._nameText.text = headerText;
		this._coverImage.sprite = headerSprite;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0000BAB7 File Offset: 0x00009CB7
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x0000BAB7 File Offset: 0x00009CB7
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
	private void RefreshVisuals()
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._bgImage.color = Color.white;
			this._nameText.color = (base.highlighted ? this._selectedHighlightElementsColor : Color.black);
			return;
		}
		this._highlightImage.enabled = base.highlighted;
		this._bgImage.enabled = true;
		this._bgImage.color = new Color(0f, 0f, 0f, 0.25f);
		this._nameText.color = Color.white;
	}

	// Token: 0x04000F9A RID: 3994
	[SerializeField]
	private Color _selectedHighlightElementsColor = new Color(0f, 0.7529412f, 1f, 1f);

	// Token: 0x04000F9B RID: 3995
	[SerializeField]
	private TextMeshProUGUI _nameText;

	// Token: 0x04000F9C RID: 3996
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F9D RID: 3997
	[SerializeField]
	private Image _highlightImage;

	// Token: 0x04000F9E RID: 3998
	[SerializeField]
	private Image _coverImage;
}
