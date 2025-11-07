using System;
using HMUI;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class DifficultyTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x140000C0 RID: 192
	// (add) Token: 0x0600140C RID: 5132 RVA: 0x00049EE0 File Offset: 0x000480E0
	// (remove) Token: 0x0600140D RID: 5133 RVA: 0x00049F18 File Offset: 0x00048118
	public event Action<DifficultyTableView, int> didSelectRow;

	// Token: 0x0600140E RID: 5134 RVA: 0x00049F50 File Offset: 0x00048150
	public void Init(IDifficultyBeatmap[] difficultyBeatmaps)
	{
		this._difficultyBeatmaps = difficultyBeatmaps;
		this._tableView.dataSource = this;
		this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectRowEvent;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectRowEvent;
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0000F176 File Offset: 0x0000D376
	public void SetDifficultyBeatmaps(IDifficultyBeatmap[] difficultyBeatmaps)
	{
		this._difficultyBeatmaps = difficultyBeatmaps;
		this._tableView.ReloadData();
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0000F18A File Offset: 0x0000D38A
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0000F192 File Offset: 0x0000D392
	public int NumberOfCells()
	{
		if (this._difficultyBeatmaps == null || this._difficultyBeatmaps.Length == 0)
		{
			return 1;
		}
		return this._difficultyBeatmaps.Length;
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x00049FA0 File Offset: 0x000481A0
	public TableCell CellForIdx(TableView tableView, int row)
	{
		if (this._difficultyBeatmaps == null || this._difficultyBeatmaps.Length == 0)
		{
			DifficultyTableCell difficultyTableCell = tableView.DequeueReusableCellForIdentifier("NonSelectableCell") as DifficultyTableCell;
			if (difficultyTableCell == null)
			{
				difficultyTableCell = UnityEngine.Object.Instantiate<DifficultyTableCell>(this._nonSelectableCellPrefab);
				difficultyTableCell.reuseIdentifier = "NonSelectableCell";
			}
			difficultyTableCell.difficultyText = "Empty";
			difficultyTableCell.difficultyValue = 0;
			return difficultyTableCell;
		}
		DifficultyTableCell difficultyTableCell2 = this._tableView.DequeueReusableCellForIdentifier("Cell") as DifficultyTableCell;
		if (difficultyTableCell2 == null)
		{
			difficultyTableCell2 = UnityEngine.Object.Instantiate<DifficultyTableCell>(this._cellPrefab);
			difficultyTableCell2.reuseIdentifier = "Cell";
		}
		IDifficultyBeatmap difficultyBeatmap = this._difficultyBeatmaps[row];
		difficultyTableCell2.difficultyText = difficultyBeatmap.difficulty.Name();
		difficultyTableCell2.difficultyValue = difficultyBeatmap.difficultyRank;
		return difficultyTableCell2;
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x0000F1AF File Offset: 0x0000D3AF
	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (this._difficultyBeatmaps == null || this._difficultyBeatmaps.Length == 0)
		{
			this._tableView.ClearSelection();
			return;
		}
		if (this.didSelectRow != null)
		{
			this.didSelectRow(this, row);
		}
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0000F1E3 File Offset: 0x0000D3E3
	public void SelectRow(int row, bool callbackTable)
	{
		this._tableView.SelectCellWithIdx(row, callbackTable);
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0004A060 File Offset: 0x00048260
	public void SelectRow(IDifficultyBeatmap difficultyBeatmap, bool callbackTable)
	{
		for (int i = 0; i < this._difficultyBeatmaps.Length; i++)
		{
			if (difficultyBeatmap == this._difficultyBeatmaps[i])
			{
				this.SelectRow(i, callbackTable);
			}
		}
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0000F1F2 File Offset: 0x0000D3F2
	public void ClearSelection()
	{
		this._tableView.ClearSelection();
	}

	// Token: 0x040013C7 RID: 5063
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013C8 RID: 5064
	[SerializeField]
	private DifficultyTableCell _cellPrefab;

	// Token: 0x040013C9 RID: 5065
	[SerializeField]
	private DifficultyTableCell _nonSelectableCellPrefab;

	// Token: 0x040013CA RID: 5066
	[SerializeField]
	private float _cellHeight = 8f;

	// Token: 0x040013CC RID: 5068
	private const string kCellIdentifier = "Cell";

	// Token: 0x040013CD RID: 5069
	private const string kNonSelectableCellIdentifier = "NonSelectableCell";

	// Token: 0x040013CE RID: 5070
	private IDifficultyBeatmap[] _difficultyBeatmaps;
}
