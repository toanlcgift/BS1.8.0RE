using System;
using System.Threading;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000362 RID: 866
public class PackLevelListHeaderTableCell : TableCell
{
	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0000BBBF File Offset: 0x00009DBF
	// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0000BBB1 File Offset: 0x00009DB1
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

	// Token: 0x06000F47 RID: 3911 RVA: 0x0000BBCC File Offset: 0x00009DCC
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0000BBCC File Offset: 0x00009DCC
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0003D804 File Offset: 0x0003BA04
	private void RefreshVisuals()
	{
		this._highlightImage.enabled = base.highlighted;
		this._bgImage.enabled = !base.highlighted;
		this._text.color = Color.white;
		this._arrowImage.color = this._selectedHighlightElementsColor;
	}

	// Token: 0x04000FAB RID: 4011
	[SerializeField]
	private Color _selectedHighlightElementsColor = new Color(0f, 0.7529412f, 1f, 1f);

	// Token: 0x04000FAC RID: 4012
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000FAD RID: 4013
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000FAE RID: 4014
	[SerializeField]
	private Image _highlightImage;

	// Token: 0x04000FAF RID: 4015
	[SerializeField]
	private Image _arrowImage;

	// Token: 0x04000FB0 RID: 4016
	private CancellationTokenSource _cancellationTokenSource;
}
