using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

// Token: 0x0200034D RID: 845
[Serializable]
public class ColorSchemesTableViewDataSource : TableView.IDataSource
{
	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0000B55E File Offset: 0x0000975E
	public List<ColorScheme> colorSchemes
	{
		get
		{
			return this._colorSchemes;
		}
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0003BE8C File Offset: 0x0003A08C
	public ColorSchemesTableViewDataSource(ColorSchemesTableViewDataSource dataSource)
	{
		this._colorSchemeCellPrefab = dataSource._colorSchemeCellPrefab;
		this._cellReuseIdentifier = dataSource._cellReuseIdentifier;
		this._cellHeight = dataSource._cellHeight;
		this._colorSchemes = dataSource._colorSchemes;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0000B566 File Offset: 0x00009766
	public void SetData(List<ColorScheme> colorSchemes)
	{
		this._colorSchemes = colorSchemes;
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0000B56F File Offset: 0x0000976F
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0000B577 File Offset: 0x00009777
	public int NumberOfCells()
	{
		if (this._colorSchemes == null)
		{
			return 0;
		}
		return this._colorSchemes.Count;
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0003BEE8 File Offset: 0x0003A0E8
	public TableCell CellForIdx(TableView tableView, int idx)
	{
		ColorSchemeTableCell colorSchemeTableCell = tableView.DequeueReusableCellForIdentifier(this._cellReuseIdentifier) as ColorSchemeTableCell;
		if (colorSchemeTableCell == null)
		{
			colorSchemeTableCell = UnityEngine.Object.Instantiate<ColorSchemeTableCell>(this._colorSchemeCellPrefab);
			colorSchemeTableCell.reuseIdentifier = this._cellReuseIdentifier;
		}
		ColorScheme colorScheme = this._colorSchemes[idx];
		colorSchemeTableCell.showEditIcon = colorScheme.isEditable;
		colorSchemeTableCell.text = colorScheme.colorSchemeName;
		colorSchemeTableCell.SetColors(colorScheme.saberAColor, colorScheme.saberBColor, colorScheme.environmentColor0, colorScheme.environmentColor1, colorScheme.obstaclesColor);
		return colorSchemeTableCell;
	}

	// Token: 0x04000F2C RID: 3884
	[SerializeField]
	private ColorSchemeTableCell _colorSchemeCellPrefab;

	// Token: 0x04000F2D RID: 3885
	[SerializeField]
	private string _cellReuseIdentifier = "Cell";

	// Token: 0x04000F2E RID: 3886
	[SerializeField]
	private float _cellHeight = 7f;

	// Token: 0x04000F2F RID: 3887
	private List<ColorScheme> _colorSchemes;
}
