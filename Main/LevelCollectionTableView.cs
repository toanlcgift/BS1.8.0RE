using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x02000423 RID: 1059
public class LevelCollectionTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x140000C1 RID: 193
	// (add) Token: 0x06001426 RID: 5158 RVA: 0x0004A138 File Offset: 0x00048338
	// (remove) Token: 0x06001427 RID: 5159 RVA: 0x0004A170 File Offset: 0x00048370
	public event Action<LevelCollectionTableView, IPreviewBeatmapLevel> didSelectLevelEvent;

	// Token: 0x140000C2 RID: 194
	// (add) Token: 0x06001428 RID: 5160 RVA: 0x0004A1A8 File Offset: 0x000483A8
	// (remove) Token: 0x06001429 RID: 5161 RVA: 0x0004A1E0 File Offset: 0x000483E0
	public event Action<LevelCollectionTableView> didSelectHeaderEvent;

	// Token: 0x0600142A RID: 5162 RVA: 0x0000F2E2 File Offset: 0x0000D4E2
	public void Init(string headerText, Sprite headerSprite)
	{
		this._showLevelPackHeader = !string.IsNullOrEmpty(headerText);
		this._headerText = headerText;
		this._headerSprite = headerSprite;
		this._previewBeatmapLevels = null;
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x0000F308 File Offset: 0x0000D508
	private void Init()
	{
		if (this._isInitialized)
		{
			return;
		}
		this._isInitialized = true;
		this._tableView.dataSource = this;
		this._tableView.didSelectCellWithIdxEvent += this.HandleDidSelectRowEvent;
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0004A218 File Offset: 0x00048418
	public void SetData(IPreviewBeatmapLevel[] previewBeatmapLevels, HashSet<string> favoriteLevelIds, bool beatmapLevelsAreSorted)
	{
		this.Init();
		this._previewBeatmapLevels = previewBeatmapLevels;
		this._favoriteLevelIds = favoriteLevelIds;
		RectTransform rectTransform = (RectTransform)this._tableView.transform;
		if (beatmapLevelsAreSorted && previewBeatmapLevels.Length > this._showAlphabetScrollbarLevelCountThreshold)
		{
			AlphabetScrollInfo.Data[] data = AlphabetScrollbarInfoBeatmapLevelHelper.CreateData(previewBeatmapLevels, out this._previewBeatmapLevels);
			this._alphabetScrollbar.SetData(data);
			rectTransform.offsetMin = new Vector2(((RectTransform)this._alphabetScrollbar.transform).rect.size.x + 1f, 0f);
			this._alphabetScrollbar.gameObject.SetActive(true);
		}
		else
		{
			rectTransform.offsetMin = new Vector2(0f, 0f);
			this._alphabetScrollbar.gameObject.SetActive(false);
		}
		this._tableView.ReloadData();
		this._tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, false);
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x0004A2FC File Offset: 0x000484FC
	public void RefreshFavorites(HashSet<string> favoriteLevelIds)
	{
		this._favoriteLevelIds = favoriteLevelIds;
		this._tableView.ReloadData();
		if (this._previewBeatmapLevels != null && this._previewBeatmapLevels.Length != 0 && this._selectedPreviewBeatmapLevel != null)
		{
			int num = Array.IndexOf<IPreviewBeatmapLevel>(this._previewBeatmapLevels, this._selectedPreviewBeatmapLevel);
			if (num >= 0)
			{
				this._tableView.SelectCellWithIdx(this._showLevelPackHeader ? (num + 1) : num, false);
			}
		}
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x0000F33D File Offset: 0x0000D53D
	protected void OnEnable()
	{
		this._additionalContentModel.didInvalidateDataEvent += this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x0000F356 File Offset: 0x0000D556
	protected void OnDisable()
	{
		this._additionalContentModel.didInvalidateDataEvent -= this.HandleAdditionalContentModelDidInvalidateData;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x0000F36F File Offset: 0x0000D56F
	protected void OnDestroy()
	{
		if (this._tableView != null)
		{
			this._tableView.didSelectCellWithIdxEvent -= this.HandleDidSelectRowEvent;
		}
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x0000F396 File Offset: 0x0000D596
	public float CellSize()
	{
		return this._cellHeight;
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x0000F39E File Offset: 0x0000D59E
	public int NumberOfCells()
	{
		if (this._previewBeatmapLevels == null)
		{
			return 0;
		}
		if (this._showLevelPackHeader)
		{
			return this._previewBeatmapLevels.Length + 1;
		}
		return this._previewBeatmapLevels.Length;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0004A364 File Offset: 0x00048564
	public TableCell CellForIdx(TableView tableView, int row)
	{
		if (row == 0 && this._showLevelPackHeader)
		{
			LevelPackHeaderTableCell levelPackHeaderTableCell = tableView.DequeueReusableCellForIdentifier(this._packCellsReuseIdentifier) as LevelPackHeaderTableCell;
			if (levelPackHeaderTableCell == null)
			{
				levelPackHeaderTableCell = UnityEngine.Object.Instantiate<LevelPackHeaderTableCell>(this._packCellPrefab);
				levelPackHeaderTableCell.reuseIdentifier = this._packCellsReuseIdentifier;
			}
			levelPackHeaderTableCell.SetData(this._headerText, this._headerSprite);
			return levelPackHeaderTableCell;
		}
		LevelListTableCell levelListTableCell = tableView.DequeueReusableCellForIdentifier(this._levelCellsReuseIdentifier) as LevelListTableCell;
		if (levelListTableCell == null)
		{
			levelListTableCell = UnityEngine.Object.Instantiate<LevelListTableCell>(this._levelCellPrefab);
			levelListTableCell.reuseIdentifier = this._levelCellsReuseIdentifier;
		}
		int num = this._showLevelPackHeader ? (row - 1) : row;
		IPreviewBeatmapLevel previewBeatmapLevel = this._previewBeatmapLevels[num];
		levelListTableCell.SetDataFromLevelAsync(previewBeatmapLevel, this._favoriteLevelIds.Contains(previewBeatmapLevel.levelID));
		levelListTableCell.RefreshAvailabilityAsync(this._additionalContentModel, previewBeatmapLevel.levelID);
		return levelListTableCell;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0004A43C File Offset: 0x0004863C
	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		this._selectedRow = row;
		if (row == 0 && this._showLevelPackHeader)
		{
			this._selectedPreviewBeatmapLevel = null;
			Action<LevelCollectionTableView> action = this.didSelectHeaderEvent;
			if (action == null)
			{
				return;
			}
			action(this);
			return;
		}
		else
		{
			int num = this._showLevelPackHeader ? (row - 1) : row;
			this._selectedPreviewBeatmapLevel = this._previewBeatmapLevels[num];
			Action<LevelCollectionTableView, IPreviewBeatmapLevel> action2 = this.didSelectLevelEvent;
			if (action2 == null)
			{
				return;
			}
			action2(this, this._selectedPreviewBeatmapLevel);
			return;
		}
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
	private void HandleAdditionalContentModelDidInvalidateData()
	{
		this._tableView.ReloadData();
		this._selectedRow = Math.Min(this._selectedRow, this.NumberOfCells() - 1);
		this._tableView.SelectCellWithIdx(this._selectedRow, false);
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x0004A4A8 File Offset: 0x000486A8
	public void CancelAsyncOperations()
	{
		foreach (TableCell tableCell in this._tableView.visibleCells)
		{
			LevelListTableCell levelListTableCell = tableCell as LevelListTableCell;
			if (levelListTableCell)
			{
				levelListTableCell.CancelAsyncOperations();
			}
		}
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x0004A508 File Offset: 0x00048708
	public void RefreshLevelsAvailability()
	{
		if (this._previewBeatmapLevels == null || this._previewBeatmapLevels.Length == 0)
		{
			return;
		}
		IPreviewBeatmapLevel[] previewBeatmapLevels = this._previewBeatmapLevels;
		foreach (TableCell tableCell in this._tableView.visibleCells)
		{
			LevelListTableCell levelListTableCell = tableCell as LevelListTableCell;
			if (levelListTableCell)
			{
				int num = this._showLevelPackHeader ? (levelListTableCell.idx - 1) : levelListTableCell.idx;
				levelListTableCell.RefreshAvailabilityAsync(this._additionalContentModel, previewBeatmapLevels[num].levelID);
			}
		}
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x0000F3FD File Offset: 0x0000D5FD
	public void SelectLevelPackHeaderCell()
	{
		this._selectedRow = 0;
		this._tableView.SelectCellWithIdx(0, false);
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x0000F3FD File Offset: 0x0000D5FD
	public void SelectFirstLevelCell()
	{
		this._selectedRow = 0;
		this._tableView.SelectCellWithIdx(0, false);
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x0004A5A8 File Offset: 0x000487A8
	public void SelectLevel(IPreviewBeatmapLevel beatmapLevel)
	{
		int num = -1;
		for (int i = 0; i < this._previewBeatmapLevels.Length; i++)
		{
			if (this._previewBeatmapLevels[i] == beatmapLevel)
			{
				num = (this._showLevelPackHeader ? (i + 1) : i);
				break;
			}
		}
		if (num >= 0)
		{
			this._selectedRow = num;
			this._tableView.SelectCellWithIdx(num, true);
			this._tableView.ScrollToCellWithIdx(num, TableViewScroller.ScrollPositionType.Center, false);
		}
	}

	// Token: 0x040013D9 RID: 5081
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013DA RID: 5082
	[SerializeField]
	private AlphabetScrollbar _alphabetScrollbar;

	// Token: 0x040013DB RID: 5083
	[SerializeField]
	private LevelListTableCell _levelCellPrefab;

	// Token: 0x040013DC RID: 5084
	[SerializeField]
	private string _levelCellsReuseIdentifier = "LevelCell";

	// Token: 0x040013DD RID: 5085
	[SerializeField]
	private LevelPackHeaderTableCell _packCellPrefab;

	// Token: 0x040013DE RID: 5086
	[SerializeField]
	private string _packCellsReuseIdentifier = "PackCell";

	// Token: 0x040013DF RID: 5087
	[SerializeField]
	private float _cellHeight = 8.5f;

	// Token: 0x040013E0 RID: 5088
	[SerializeField]
	private int _showAlphabetScrollbarLevelCountThreshold = 16;

	// Token: 0x040013E1 RID: 5089
	[Inject]
	private AdditionalContentModel _additionalContentModel;

	// Token: 0x040013E4 RID: 5092
	private bool _isInitialized;

	// Token: 0x040013E5 RID: 5093
	private IPreviewBeatmapLevel[] _previewBeatmapLevels;

	// Token: 0x040013E6 RID: 5094
	private Sprite _headerSprite;

	// Token: 0x040013E7 RID: 5095
	private string _headerText;

	// Token: 0x040013E8 RID: 5096
	private bool _showLevelPackHeader = true;

	// Token: 0x040013E9 RID: 5097
	private HashSet<string> _favoriteLevelIds;

	// Token: 0x040013EA RID: 5098
	private int _selectedRow = -1;

	// Token: 0x040013EB RID: 5099
	private IPreviewBeatmapLevel _selectedPreviewBeatmapLevel;
}
