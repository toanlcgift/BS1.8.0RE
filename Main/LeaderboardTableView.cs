using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class LeaderboardTableView : MonoBehaviour, TableView.IDataSource
{
	// Token: 0x06001418 RID: 5144 RVA: 0x0000F212 File Offset: 0x0000D412
	public float CellSize()
	{
		return this._rowHeight;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x0000F21A File Offset: 0x0000D41A
	public int NumberOfCells()
	{
		if (this._scores == null)
		{
			return 0;
		}
		return this._scores.Count;
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0004A094 File Offset: 0x00048294
	public TableCell CellForIdx(TableView tableView, int row)
	{
		LeaderboardTableCell leaderboardTableCell = tableView.DequeueReusableCellForIdentifier("Cell") as LeaderboardTableCell;
		if (leaderboardTableCell == null)
		{
			leaderboardTableCell = UnityEngine.Object.Instantiate<LeaderboardTableCell>(this._cellPrefab);
			leaderboardTableCell.reuseIdentifier = "Cell";
		}
		LeaderboardTableView.ScoreData scoreData = this._scores[row];
		leaderboardTableCell.rank = scoreData.rank;
		leaderboardTableCell.playerName = scoreData.playerName;
		leaderboardTableCell.score = scoreData.score;
		leaderboardTableCell.showFullCombo = scoreData.fullCombo;
		leaderboardTableCell.showSeparator = (row != this._scores.Count - 1);
		leaderboardTableCell.specialScore = (this._specialScorePos == row);
		return leaderboardTableCell;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0000F231 File Offset: 0x0000D431
	public void SetScores(List<LeaderboardTableView.ScoreData> scores, int specialScorePos)
	{
		this._scores = scores;
		this._specialScorePos = specialScorePos;
		if (this._tableView.dataSource == null)
		{
			this._tableView.dataSource = this;
			return;
		}
		this._tableView.ReloadData();
	}

	// Token: 0x040013CF RID: 5071
	[SerializeField]
	private TableView _tableView;

	// Token: 0x040013D0 RID: 5072
	[SerializeField]
	private LeaderboardTableCell _cellPrefab;

	// Token: 0x040013D1 RID: 5073
	[SerializeField]
	private float _rowHeight = 7f;

	// Token: 0x040013D2 RID: 5074
	private const string kCellIdentifier = "Cell";

	// Token: 0x040013D3 RID: 5075
	private List<LeaderboardTableView.ScoreData> _scores;

	// Token: 0x040013D4 RID: 5076
	private int _specialScorePos;

	// Token: 0x02000422 RID: 1058
	public class ScoreData
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0000F279 File Offset: 0x0000D479
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0000F281 File Offset: 0x0000D481
		public int score { get; private set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0000F28A File Offset: 0x0000D48A
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0000F292 File Offset: 0x0000D492
		public string playerName { get; private set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0000F29B File Offset: 0x0000D49B
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0000F2A3 File Offset: 0x0000D4A3
		public int rank { get; private set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public bool fullCombo { get; private set; }

		// Token: 0x06001425 RID: 5157 RVA: 0x0000F2BD File Offset: 0x0000D4BD
		public ScoreData(int score, string playerName, int rank, bool fullCombo)
		{
			this.score = score;
			this.playerName = playerName;
			this.rank = rank;
			this.fullCombo = fullCombo;
		}
	}
}
