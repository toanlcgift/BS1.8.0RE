using System;
using HMUI;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class MainSettingsTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x140000C3 RID: 195
	// (add) Token: 0x06001445 RID: 5189 RVA: 0x0004A694 File Offset: 0x00048894
	// (remove) Token: 0x06001446 RID: 5190 RVA: 0x0004A6CC File Offset: 0x000488CC
	public event Action<MainSettingsTableView, int> didSelectRowEvent;

	// Token: 0x06001447 RID: 5191 RVA: 0x0004A704 File Offset: 0x00048904
	public void Init(SettingsSubMenuInfo[] settingsSubMenuInfos)
	{
		this._settingsSubMenuInfos = settingsSubMenuInfos;
		this._tableView.dataSource = this;
		this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectRow;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectRow;
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0000F4D6 File Offset: 0x0000D6D6
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0000F4DE File Offset: 0x0000D6DE
	public int NumberOfCells()
	{
		if (this._settingsSubMenuInfos == null)
		{
			return 0;
		}
		return this._settingsSubMenuInfos.Length;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0004A754 File Offset: 0x00048954
	public TableCell CellForIdx(TableView tableView, int row)
	{
		MainSettingsTableCell mainSettingsTableCell = tableView.DequeueReusableCellForIdentifier("Cell") as MainSettingsTableCell;
		if (mainSettingsTableCell == null)
		{
			mainSettingsTableCell = UnityEngine.Object.Instantiate<MainSettingsTableCell>(this._cellPrefab);
			mainSettingsTableCell.reuseIdentifier = "Cell";
		}
		SettingsSubMenuInfo settingsSubMenuInfo = this._settingsSubMenuInfos[row];
		mainSettingsTableCell.settingsSubMenuText = settingsSubMenuInfo.localizedMenuName;
		return mainSettingsTableCell;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0000F4F2 File Offset: 0x0000D6F2
	private void HandleDidSelectRow(TableView tableView, int row)
	{
		if (this.didSelectRowEvent != null)
		{
			this.didSelectRowEvent(this, row);
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0000F509 File Offset: 0x0000D709
	public void SelectRow(int row)
	{
		this._tableView.SelectCellWithIdx(row, false);
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0000F518 File Offset: 0x0000D718
	public void ClearSelection()
	{
		this._tableView.ClearSelection();
	}

	// Token: 0x040013F2 RID: 5106
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013F3 RID: 5107
	[SerializeField]
	private MainSettingsTableCell _cellPrefab;

	// Token: 0x040013F4 RID: 5108
	[SerializeField]
	private float _cellHeight = 8f;

	// Token: 0x040013F6 RID: 5110
	private const string kCellIdentifier = "Cell";

	// Token: 0x040013F7 RID: 5111
	private SettingsSubMenuInfo[] _settingsSubMenuInfos;
}
