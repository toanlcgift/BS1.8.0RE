using System;
using HMUI;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class FileBrowserTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x14000087 RID: 135
	// (add) Token: 0x06000FB1 RID: 4017 RVA: 0x0003E894 File Offset: 0x0003CA94
	// (remove) Token: 0x06000FB2 RID: 4018 RVA: 0x0003E8CC File Offset: 0x0003CACC
	public event Action<FileBrowserTableView, FileBrowserItem> didSelectRow;

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0003E904 File Offset: 0x0003CB04
	public void Init(FileBrowserItem[] items)
	{
		this._items = items;
		this._tableView.dataSource = this;
		this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
		this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectRowEvent;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectRowEvent;
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000BFAE File Offset: 0x0000A1AE
	public void SetItems(FileBrowserItem[] items)
	{
		this._items = items;
		this._tableView.ReloadData();
		this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0003E960 File Offset: 0x0003CB60
	public bool SelectAndScrollRowToItemWithPath(string folderPath)
	{
		int num = 0;
		foreach (FileBrowserItem fileBrowserItem in this._items)
		{
			if (folderPath == fileBrowserItem.fullPath)
			{
				this.SelectAndScrollRow(num);
				return true;
			}
			num++;
		}
		return false;
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
	public int NumberOfCells()
	{
		if (this._items == null)
		{
			return 0;
		}
		return this._items.Length;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
	public TableCell CellForIdx(TableView tableView, int row)
	{
		FileBrowserTableCell fileBrowserTableCell = tableView.DequeueReusableCellForIdentifier("Cell") as FileBrowserTableCell;
		if (fileBrowserTableCell == null)
		{
			fileBrowserTableCell = UnityEngine.Object.Instantiate<FileBrowserTableCell>(this._cellPrefab);
			fileBrowserTableCell.reuseIdentifier = "Cell";
		}
		FileBrowserItem fileBrowserItem = this._items[row];
		fileBrowserTableCell.text = fileBrowserItem.displayName;
		return fileBrowserTableCell;
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x0000BFEC File Offset: 0x0000A1EC
	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (this.didSelectRow != null)
		{
			this.didSelectRow(this, this._items[row]);
		}
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0000C00A File Offset: 0x0000A20A
	public void SelectAndScrollRow(int row)
	{
		this._tableView.SelectCellWithIdx(row, true);
		this._tableView.ScrollToCellWithIdx(row, TableViewScroller.ScrollPositionType.Beginning, false);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0000C027 File Offset: 0x0000A227
	public void ClearSelection(bool animated = false, bool scrollToRow0 = true)
	{
		this._tableView.ClearSelection();
		if (scrollToRow0)
		{
			this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, animated);
		}
	}

	// Token: 0x04001006 RID: 4102
	[SerializeField]
	private TableView _tableView;

	// Token: 0x04001007 RID: 4103
	[SerializeField]
	private FileBrowserTableCell _cellPrefab;

	// Token: 0x04001008 RID: 4104
	[SerializeField]
	private float _cellHeight = 12f;

	// Token: 0x0400100A RID: 4106
	private const string kCellIdentifier = "Cell";

	// Token: 0x0400100B RID: 4107
	private FileBrowserItem[] _items;
}
