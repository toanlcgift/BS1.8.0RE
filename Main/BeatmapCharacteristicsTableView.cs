using System;
using HMUI;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class BeatmapCharacteristicsTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x140000BE RID: 190
	// (add) Token: 0x060013F7 RID: 5111 RVA: 0x00049D38 File Offset: 0x00047F38
	// (remove) Token: 0x060013F8 RID: 5112 RVA: 0x00049D70 File Offset: 0x00047F70
	public event Action<BeatmapCharacteristicSO> didSelectCharacteristic;

	// Token: 0x060013F9 RID: 5113 RVA: 0x0000F004 File Offset: 0x0000D204
	private void Init()
	{
		if (this._isInitialized)
		{
			return;
		}
		this._isInitialized = true;
		this._tableView.dataSource = this;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectColumnEvent;
	}

	// Token: 0x060013FA RID: 5114 RVA: 0x0000F039 File Offset: 0x0000D239
	public void SetData(BeatmapCharacteristicCollectionSO beatmapCharacteristicCollection)
	{
		this.Init();
		this._beatmapCharacteristicCollection = beatmapCharacteristicCollection;
		this._tableView.ReloadData();
		this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x0000F061 File Offset: 0x0000D261
	protected void OnDestroy()
	{
		this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectColumnEvent;
	}

	// Token: 0x060013FC RID: 5116 RVA: 0x0000F07A File Offset: 0x0000D27A
	public float CellSize()
	{
		return this._cellWidth;
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x0000F082 File Offset: 0x0000D282
	public int NumberOfCells()
	{
		if (this._beatmapCharacteristicCollection == null)
		{
			return 0;
		}
		return this._beatmapCharacteristicCollection.beatmapCharacteristics.Length;
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x00049DA8 File Offset: 0x00047FA8
	public TableCell CellForIdx(TableView tableView, int idx)
	{
		BeatmapCharacteristicTableCell beatmapCharacteristicTableCell = tableView.DequeueReusableCellForIdentifier(this._cellReuseIdentifier) as BeatmapCharacteristicTableCell;
		if (beatmapCharacteristicTableCell == null)
		{
			beatmapCharacteristicTableCell = UnityEngine.Object.Instantiate<BeatmapCharacteristicTableCell>(this._cellPrefab);
			beatmapCharacteristicTableCell.reuseIdentifier = this._cellReuseIdentifier;
		}
		BeatmapCharacteristicSO data = this._beatmapCharacteristicCollection.beatmapCharacteristics[idx];
		beatmapCharacteristicTableCell.SetData(data);
		return beatmapCharacteristicTableCell;
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x0000F0A1 File Offset: 0x0000D2A1
	private void HandleDidSelectColumnEvent(TableView tableView, int column)
	{
		this._selectedColumn = column;
		Action<BeatmapCharacteristicSO> action = this.didSelectCharacteristic;
		if (action == null)
		{
			return;
		}
		action(this._beatmapCharacteristicCollection.beatmapCharacteristics[column]);
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this._tableView.ReloadData();
		this._selectedColumn = Math.Min(this._selectedColumn, this.NumberOfCells() - 1);
		this._tableView.SelectCellWithIdx(this._selectedColumn, false);
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x0000F0FF File Offset: 0x0000D2FF
	public void SelectCellWithIdx(int idx)
	{
		this._tableView.SelectCellWithIdx(idx, false);
	}

	// Token: 0x040013B6 RID: 5046
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013B7 RID: 5047
	[SerializeField]
	private BeatmapCharacteristicTableCell _cellPrefab;

	// Token: 0x040013B8 RID: 5048
	[SerializeField]
	private string _cellReuseIdentifier = "Cell";

	// Token: 0x040013B9 RID: 5049
	[SerializeField]
	private float _cellWidth = 40f;

	// Token: 0x040013BB RID: 5051
	private BeatmapCharacteristicCollectionSO _beatmapCharacteristicCollection;

	// Token: 0x040013BC RID: 5052
	private bool _isInitialized;

	// Token: 0x040013BD RID: 5053
	private int _selectedColumn;
}
