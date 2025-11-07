using System;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000355 RID: 853
public class BeatmapCharacteristicTableCell : TableCell
{
	// Token: 0x06000F07 RID: 3847 RVA: 0x0000B860 File Offset: 0x00009A60
	public void SetData(BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this._nameText.text = Localization.Get(beatmapCharacteristic.characteristicNameLocalizationKey);
		this._iconImage.sprite = beatmapCharacteristic.icon;
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0000B889 File Offset: 0x00009A89
	protected override void SelectionDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0000B889 File Offset: 0x00009A89
	protected override void HighlightDidChange(TableCell.TransitionType transitionType)
	{
		this.RefreshVisuals();
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0000B891 File Offset: 0x00009A91
	private void RefreshVisuals()
	{
		this._bgImage.color = (base.highlighted ? this._bgHighlightColor : this._bgNormalColor);
		this._selectionImage.enabled = base.selected;
	}

	// Token: 0x04000F64 RID: 3940
	[SerializeField]
	private TextMeshProUGUI _nameText;

	// Token: 0x04000F65 RID: 3941
	[SerializeField]
	private Image _iconImage;

	// Token: 0x04000F66 RID: 3942
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000F67 RID: 3943
	[SerializeField]
	private Image _selectionImage;

	// Token: 0x04000F68 RID: 3944
	[Space]
	[SerializeField]
	private Color _bgNormalColor = Color.black;

	// Token: 0x04000F69 RID: 3945
	[SerializeField]
	private Color _bgHighlightColor = Color.white;
}
