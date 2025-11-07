using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x020003E9 RID: 1001
public class LocalLeaderboardViewController : LeaderboardViewController
{
	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0000E232 File Offset: 0x0000C432
	public LocalLeaderboardsModel leaderboardsModel
	{
		get
		{
			return this._localLeaderboardsModel;
		}
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x0000E23A File Offset: 0x0000C43A
	public void Setup(bool enableClear)
	{
		this._enableClear = enableClear;
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x0000E243 File Offset: 0x0000C443
	public override void SetData(IDifficultyBeatmap difficultyBeatmap)
	{
		this._refreshIsNeeded = (this._difficultyBeatmap != difficultyBeatmap);
		this._difficultyBeatmap = difficultyBeatmap;
		if (base.isActivated && this._refreshIsNeeded)
		{
			this.Refresh();
		}
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x000463A8 File Offset: 0x000445A8
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._clearLeaderboardsButton, delegate
			{
				this.ClearLeaderboards();
				this._scopeSegmentedControl.SelectCellWithNumber(0);
				this.HandleScopeSegmentedControlDidSelectCell(this._scopeSegmentedControl, 0);
			});
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.RefreshScopeSegmentedControl();
			this._localLeaderboardsModel.newScoreWasAddedToLeaderboardEvent += this.HandleNewScoreWasAddedToLeaderboard;
			this._scopeSegmentedControl.didSelectCellEvent += this.HandleScopeSegmentedControlDidSelectCell;
			this._scopeSegmentedControl.SelectCellWithNumber((int)LocalLeaderboardViewController._leaderboardType);
		}
		if (this._refreshIsNeeded || activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.Refresh();
		}
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x0000E274 File Offset: 0x0000C474
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._localLeaderboardsModel.newScoreWasAddedToLeaderboardEvent -= this.HandleNewScoreWasAddedToLeaderboard;
			this._scopeSegmentedControl.didSelectCellEvent -= this.HandleScopeSegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
	protected override void OnDestroy()
	{
		base.OnDestroy();
		this._localLeaderboardsModel.newScoreWasAddedToLeaderboardEvent -= this.HandleNewScoreWasAddedToLeaderboard;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x00046430 File Offset: 0x00044630
	private void RefreshScopeSegmentedControl()
	{
		List<IconSegmentedControl.DataItem> list = new List<IconSegmentedControl.DataItem>(3);
		list.Add(new IconSegmentedControl.DataItem(this._allTimeLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_ALL_TIME")));
		list.Add(new IconSegmentedControl.DataItem(this._todayLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_TODAY")));
		if (this._enableClear)
		{
			list.Add(new IconSegmentedControl.DataItem(this._clearLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_CLEAR_ALL")));
		}
		this._scopeSegmentedControl.SetData(list.ToArray());
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000464B0 File Offset: 0x000446B0
	private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellNumber)
	{
		if (cellNumber == 0)
		{
			this._leaderboardTableView.gameObject.SetActive(true);
			this._clearLeaderboardsWrapper.SetActive(false);
			LocalLeaderboardViewController._leaderboardType = LocalLeaderboardsModel.LeaderboardType.AllTime;
			this.Refresh();
			return;
		}
		if (cellNumber == 1)
		{
			this._leaderboardTableView.gameObject.SetActive(true);
			this._clearLeaderboardsWrapper.SetActive(false);
			LocalLeaderboardViewController._leaderboardType = LocalLeaderboardsModel.LeaderboardType.Daily;
			this.Refresh();
			return;
		}
		if (cellNumber == 2)
		{
			this._leaderboardTableView.gameObject.SetActive(false);
			this._clearLeaderboardsWrapper.SetActive(true);
		}
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x0000E2C6 File Offset: 0x0000C4C6
	private void ClearLeaderboards()
	{
		this._localLeaderboardsModel.ClearAllLeaderboards(true);
		this._localLeaderboardsModel.ClearLastScorePosition();
		this._playerDataModel.playerData.DeleteAllGuestPlayers();
		this._playerDataModel.Save();
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x0004653C File Offset: 0x0004473C
	private void SetContent(string leaderboardID, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		List<LocalLeaderboardsModel.ScoreData> scores = this._localLeaderboardsModel.GetScores(leaderboardID, leaderboardType);
		int lastScorePosition = this._localLeaderboardsModel.GetLastScorePosition(leaderboardID, leaderboardType);
		this._leaderboardTableView.SetScores(scores, lastScorePosition, this._maxNumberOfCells);
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x0000E2FA File Offset: 0x0000C4FA
	private void HandleNewScoreWasAddedToLeaderboard(string leaderboardID, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		if (LocalLeaderboardViewController._leaderboardType == leaderboardType && leaderboardID == LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap))
		{
			if (base.isActivated)
			{
				this.Refresh();
				return;
			}
			this._refreshIsNeeded = true;
		}
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x00046578 File Offset: 0x00044778
	private void Refresh()
	{
		this._refreshIsNeeded = false;
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap);
		this.SetContent(leaderboardID, LocalLeaderboardViewController._leaderboardType);
	}

	// Token: 0x04001266 RID: 4710
	[SerializeField]
	private int _maxNumberOfCells = 7;

	// Token: 0x04001267 RID: 4711
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardsModel;

	// Token: 0x04001268 RID: 4712
	[Space]
	[SerializeField]
	private LocalLeaderboardTableView _leaderboardTableView;

	// Token: 0x04001269 RID: 4713
	[SerializeField]
	private GameObject _clearLeaderboardsWrapper;

	// Token: 0x0400126A RID: 4714
	[SerializeField]
	private NoTransitionsButton _clearLeaderboardsButton;

	// Token: 0x0400126B RID: 4715
	[SerializeField]
	private IconSegmentedControl _scopeSegmentedControl;

	// Token: 0x0400126C RID: 4716
	[Space]
	[SerializeField]
	private Sprite _allTimeLeaderboardIcon;

	// Token: 0x0400126D RID: 4717
	[SerializeField]
	private Sprite _todayLeaderboardIcon;

	// Token: 0x0400126E RID: 4718
	[SerializeField]
	private Sprite _clearLeaderboardIcon;

	// Token: 0x0400126F RID: 4719
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001270 RID: 4720
	[DoesNotRequireDomainReloadInit]
	private static LocalLeaderboardsModel.LeaderboardType _leaderboardType;

	// Token: 0x04001271 RID: 4721
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x04001272 RID: 4722
	private bool _refreshIsNeeded;

	// Token: 0x04001273 RID: 4723
	private bool _enableClear;
}
