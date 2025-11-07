using System;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000588 RID: 1416
	public class OpenLevelTableController : MonoBehaviour, TableView.IDataSource
	{
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06001B9A RID: 7066 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		// (remove) Token: 0x06001B9B RID: 7067 RVA: 0x0005F11C File Offset: 0x0005D31C
		public event Action<int> didSelectRowAction;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06001B9C RID: 7068 RVA: 0x0005F154 File Offset: 0x0005D354
		// (remove) Token: 0x06001B9D RID: 7069 RVA: 0x0005F18C File Offset: 0x0005D38C
		public event Action<int> didRequestDeleteAction;

		// Token: 0x06001B9E RID: 7070 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
		public void Init(string[] levelNames, Action<int> didSelectRowAction, Action<int> didRequestDeleteAction)
		{
			this._levelNames = levelNames;
			this._tableView.dataSource = this;
			this.didSelectRowAction = didSelectRowAction;
			this.didRequestDeleteAction = didRequestDeleteAction;
			this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectRowEvent;
			this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectRowEvent;
			this._tableView.ReloadData();
			this.ClearSelection();
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00014678 File Offset: 0x00012878
		public void SetContent(string[] levelNames)
		{
			this._levelNames = levelNames;
			this._tableView.ReloadData();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0001468C File Offset: 0x0001288C
		public float CellSize()
		{
			return this._cellHeight;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00014694 File Offset: 0x00012894
		public int NumberOfCells()
		{
			if (this._levelNames == null)
			{
				return 0;
			}
			return this._levelNames.Length;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0005F234 File Offset: 0x0005D434
		public TableCell CellForIdx(TableView tableView, int row)
		{
			OpenLevelTableCell openLevelTableCell = tableView.DequeueReusableCellForIdentifier("Cell") as OpenLevelTableCell;
			if (openLevelTableCell == null)
			{
				openLevelTableCell = UnityEngine.Object.Instantiate<OpenLevelTableCell>(this._cellPrefab);
				openLevelTableCell.reuseIdentifier = "Cell";
				openLevelTableCell.deleteButtonPressedEvent += this.HandleCellDeleteButtonPressed;
			}
			openLevelTableCell.text = this._levelNames[row];
			return openLevelTableCell;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000146A8 File Offset: 0x000128A8
		private void HandleCellDeleteButtonPressed(int row)
		{
			Action<int> action = this.didRequestDeleteAction;
			if (action == null)
			{
				return;
			}
			action(row);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000146BB File Offset: 0x000128BB
		private void HandleDidSelectRowEvent(TableView tableView, int row)
		{
			Action<int> action = this.didSelectRowAction;
			if (action == null)
			{
				return;
			}
			action(row);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000146CE File Offset: 0x000128CE
		public void SelectRow(int row, bool callbackTable)
		{
			this._tableView.SelectCellWithIdx(row, callbackTable);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000146DD File Offset: 0x000128DD
		public void ClearSelection()
		{
			this._tableView.ClearSelection();
		}

		// Token: 0x04001A36 RID: 6710
		[SerializeField]
		private TableView _tableView;

		// Token: 0x04001A37 RID: 6711
		[SerializeField]
		private OpenLevelTableCell _cellPrefab;

		// Token: 0x04001A38 RID: 6712
		[SerializeField]
		private float _cellHeight = 8f;

		// Token: 0x04001A3B RID: 6715
		private const string kCellIdentifier = "Cell";

		// Token: 0x04001A3C RID: 6716
		private string[] _levelNames;
	}
}
