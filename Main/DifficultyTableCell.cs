using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000356 RID: 854
public class DifficultyTableCell : TableCell
{
	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0000B8F1 File Offset: 0x00009AF1
	// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0000B8E3 File Offset: 0x00009AE3
	public string difficultyText
	{
		get
		{
			return this._difficultyText.text;
		}
		set
		{
			this._difficultyText.text = value;
		}
	}

	// Token: 0x1700032C RID: 812
	// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0000B8FE File Offset: 0x00009AFE
	public int difficultyValue
	{
		set
		{
			this._fillIndicator.fillAmount = Mathf.Clamp((float)value / 10f, 0f, 1f);
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0003CD5C File Offset: 0x0003AF5C
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			this._bgImage.enabled = true;
			this._difficultyText.color = Color.black;
			return;
		}
		this._bgImage.enabled = false;
		this._difficultyText.color = Color.white;
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0000B922 File Offset: 0x00009B22
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		if (base.selected)
		{
			this._highlightImage.enabled = false;
			return;
		}
		this._highlightImage.enabled = base.highlighted;
	}

	// Token: 0x04000F6A RID: 3946
	[SerializeField]
	private TextMeshProUGUI _difficultyText;

	// Token: 0x04000F6B RID: 3947
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F6C RID: 3948
	[SerializeField]
	private Image _highlightImage;

	// Token: 0x04000F6D RID: 3949
	[SerializeField]
	private FillIndicator _fillIndicator;
}
