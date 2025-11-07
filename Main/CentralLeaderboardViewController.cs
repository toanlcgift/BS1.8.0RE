using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HMUI;
using OnlineServices;
using Polyglot;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class CentralLeaderboardViewController : LeaderboardViewController
{
	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0000D95A File Offset: 0x0000BB5A
	private bool _hasScoresData
	{
		get
		{
			return this._scores != null && this._scores.Count != 0;
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x0000D974 File Offset: 0x0000BB74
	public override void SetData(IDifficultyBeatmap difficultyBeatmap)
	{
		if (this._difficultyBeatmap == difficultyBeatmap)
		{
			return;
		}
		this._difficultyBeatmap = difficultyBeatmap;
		this.ClearContent();
		if (base.isActivated)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000430A0 File Offset: 0x000412A0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._leaderboardPanels = new CentralLeaderboardViewController.LeaderboardPanel[]
			{
				new CentralLeaderboardViewController.LeaderboardPanel(Localization.Get("LEADERBOARDS_NO_MODIFIERS_TITLE"), false),
				new CentralLeaderboardViewController.LeaderboardPanel(Localization.Get("LEADERBOARDS_MIXED_LEADERBOARDS_TITLE"), true)
			};
			this._includeScoreWithModifiers = false;
			this._leaderboardTypeSegmentedControl.SetTexts((from x in this._leaderboardPanels
			select x.title).ToArray<string>());
			this._scoreScopeInfos = new CentralLeaderboardViewController.ScoreScopeInfo[]
			{
				new CentralLeaderboardViewController.ScoreScopeInfo(ScoresScope.Global, this._globalLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_GLOBAL")),
				new CentralLeaderboardViewController.ScoreScopeInfo(ScoresScope.Friends, this._friendsLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_FRIENDS"))
			};
			this._scopeSegmentedControl.SetData((from x in this._scoreScopeInfos
			select new IconSegmentedControl.DataItem(x.icon, x.localizedTitle)).ToArray<IconSegmentedControl.DataItem>());
		}
		this._serverManager.scoreForLeaderboardDidUploadEvent += this.HandleScoreForLeaderboardDidUpload;
		this._serverManager.platformServicesAvailabilityInfoChangedEvent += this.HandlelatformServicesAvailabilityInfoChanged;
		this._scopeSegmentedControl.didSelectCellEvent += this.HandleScopeSegmentedControlDidSelectCell;
		this._loadingControl.didPressRefreshButtonEvent += this.HandleDidPressRefreshButton;
		this._leaderboardTypeSegmentedControl.didSelectCellEvent += this.HanldeLeaderboardTypeSegmentedControlDidSelectCell;
		if (!this._hasScoresData && activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x00043224 File Offset: 0x00041424
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		this._serverManager.scoreForLeaderboardDidUploadEvent -= this.HandleScoreForLeaderboardDidUpload;
		this._serverManager.platformServicesAvailabilityInfoChangedEvent -= this.HandlelatformServicesAvailabilityInfoChanged;
		this._scopeSegmentedControl.didSelectCellEvent -= this.HandleScopeSegmentedControlDidSelectCell;
		this._loadingControl.didPressRefreshButtonEvent -= this.HandleDidPressRefreshButton;
		this._leaderboardTypeSegmentedControl.didSelectCellEvent -= this.HanldeLeaderboardTypeSegmentedControlDidSelectCell;
		this._loadingControl.Hide();
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000432B0 File Offset: 0x000414B0
	protected override void OnDestroy()
	{
		base.OnDestroy();
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = null;
		if (this._scopeSegmentedControl != null)
		{
			this._scopeSegmentedControl.didSelectCellEvent -= this.HandleScopeSegmentedControlDidSelectCell;
		}
		if (this._loadingControl != null)
		{
			this._loadingControl.didPressRefreshButtonEvent -= this.HandleDidPressRefreshButton;
		}
		this._serverManager.scoreForLeaderboardDidUploadEvent -= this.HandleScoreForLeaderboardDidUpload;
		if (this._leaderboardTypeSegmentedControl != null)
		{
			this._leaderboardTypeSegmentedControl.didSelectCellEvent -= this.HanldeLeaderboardTypeSegmentedControlDidSelectCell;
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x0000D99D File Offset: 0x0000BB9D
	private void HandleDidPressRefreshButton()
	{
		this.Refresh(true, true);
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0000D9A7 File Offset: 0x0000BBA7
	private void HandlelatformServicesAvailabilityInfoChanged(PlatformServicesAvailabilityInfo availabilityInfo)
	{
		if (availabilityInfo.availability == PlatformServicesAvailabilityInfo.OnlineServicesAvailability.Available)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0000D9B9 File Offset: 0x0000BBB9
	private void HanldeLeaderboardTypeSegmentedControlDidSelectCell(SegmentedControl control, int index)
	{
		this._includeScoreWithModifiers = this._leaderboardPanels[index].includeScoreWithModifiers;
		this.Refresh(true, true);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0000D99D File Offset: 0x0000BB9D
	private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellNumber)
	{
		this.Refresh(true, true);
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x0000D9D6 File Offset: 0x0000BBD6
	private void HandleScoreForLeaderboardDidUpload(string leaderboardId)
	{
		if (this._serverManager.GetLeaderboardId(this._difficultyBeatmap) == leaderboardId)
		{
			this.Refresh(false, false);
		}
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x00043364 File Offset: 0x00041564
	private void Refresh(bool showLoadingIndicator, bool clear)
	{
		if (this._difficultyBeatmap.level is CustomBeatmapLevel)
		{
			base.StopAllCoroutines();
			this.ClearContent();
			this._loadingControl.ShowText(Localization.Get("CUSTOM_LEVELS_LEADERBOARDS_NOT_SUPPORTED"), false);
			return;
		}
		if (showLoadingIndicator)
		{
			this._loadingControl.ShowLoading();
		}
		else
		{
			this._loadingControl.Hide();
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.RefreshDelayed(showLoadingIndicator, clear));
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0000D9F9 File Offset: 0x0000BBF9
	private IEnumerator RefreshDelayed(bool showLoadingIndicator, bool clear)
	{
		if (clear)
		{
			this.ClearContent();
		}
		if (showLoadingIndicator)
		{
			this._loadingControl.ShowLoading();
		}
		else
		{
			this._loadingControl.Hide();
		}
		yield return new WaitForSeconds(0.4f);
		this.LoadScoresAsync();
		yield break;
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000433D8 File Offset: 0x000415D8
	private async void LoadScoresAsync()
	{
		LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap);
		LeaderboardEntriesResult leaderboardEntriesResult = null;
		IDifficultyBeatmap loadingFordifficultyBeatmap = this._difficultyBeatmap;
		CentralLeaderboardViewController.ScoreScopeInfo scoreScopeInfo = this._scoreScopeInfos[this._scopeSegmentedControl.selectedCellNumber];
		GetLeaderboardFilterData leaderboardFilterData = new GetLeaderboardFilterData(loadingFordifficultyBeatmap, 10, 1, scoreScopeInfo.scoreScope, this._includeScoreWithModifiers);
		CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
		if (cancellationTokenSource != null)
		{
			cancellationTokenSource.Cancel();
		}
		this._cancellationTokenSource = new CancellationTokenSource();
		try
		{
			CancellationToken cancellationToken = this._cancellationTokenSource.Token;
			leaderboardEntriesResult = await this._serverManager.GetLeaderboardEntriesAsync(leaderboardFilterData, cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
			this._loadingControl.Hide();
			if (this._difficultyBeatmap != loadingFordifficultyBeatmap)
			{
				return;
			}
			cancellationToken = default(CancellationToken);
		}
		catch (OperationCanceledException)
		{
			return;
		}
		if (leaderboardEntriesResult.isError)
		{
			this._loadingControl.ShowText(leaderboardEntriesResult.localizedErrorMessage, true);
		}
		else
		{
			this._scores.Clear();
			for (int i = 0; i < leaderboardEntriesResult.leaderboardEntries.Length; i++)
			{
				LeaderboardEntryData leaderboardEntryData = leaderboardEntriesResult.leaderboardEntries[i];
				this._scores.Add(new LeaderboardTableView.ScoreData(leaderboardEntryData.score, leaderboardEntryData.displayName, leaderboardEntryData.rank, false));
			}
			this._leaderboardTableView.SetScores(this._scores, leaderboardEntriesResult.referencePlayerScoreIndex);
		}
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x0000DA16 File Offset: 0x0000BC16
	private void ClearContent()
	{
		this._scores.Clear();
		this._leaderboardTableView.SetScores(this._scores, -1);
	}

	// Token: 0x04001182 RID: 4482
	[SerializeField]
	private LeaderboardTableView _leaderboardTableView;

	// Token: 0x04001183 RID: 4483
	[SerializeField]
	private IconSegmentedControl _scopeSegmentedControl;

	// Token: 0x04001184 RID: 4484
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x04001185 RID: 4485
	[Space]
	[SerializeField]
	private TextSegmentedControl _leaderboardTypeSegmentedControl;

	// Token: 0x04001186 RID: 4486
	[SerializeField]
	private Sprite _globalLeaderboardIcon;

	// Token: 0x04001187 RID: 4487
	[SerializeField]
	private Sprite _friendsLeaderboardIcon;

	// Token: 0x04001188 RID: 4488
	private List<LeaderboardTableView.ScoreData> _scores = new List<LeaderboardTableView.ScoreData>(10);

	// Token: 0x04001189 RID: 4489
	private ServerManager _serverManager;

	// Token: 0x0400118A RID: 4490
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x0400118B RID: 4491
	private CancellationTokenSource _cancellationTokenSource;

	// Token: 0x0400118C RID: 4492
	private CentralLeaderboardViewController.LeaderboardPanel[] _leaderboardPanels;

	// Token: 0x0400118D RID: 4493
	private CentralLeaderboardViewController.ScoreScopeInfo[] _scoreScopeInfos;

	// Token: 0x0400118E RID: 4494
	private bool _includeScoreWithModifiers;

	// Token: 0x020003C4 RID: 964
	private class LeaderboardPanel
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		public LeaderboardPanel(string title, bool includeScoreWithModifiers)
		{
			this.title = title;
			this.includeScoreWithModifiers = includeScoreWithModifiers;
		}

		// Token: 0x0400118F RID: 4495
		public readonly string title;

		// Token: 0x04001190 RID: 4496
		public readonly bool includeScoreWithModifiers;
	}

	// Token: 0x020003C5 RID: 965
	private class ScoreScopeInfo
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x0000DA60 File Offset: 0x0000BC60
		public ScoreScopeInfo(ScoresScope scoreScope, Sprite icon, string localizedTitle)
		{
			this.scoreScope = scoreScope;
			this.icon = icon;
			this.localizedTitle = localizedTitle;
		}

		// Token: 0x04001191 RID: 4497
		public ScoresScope scoreScope;

		// Token: 0x04001192 RID: 4498
		public string localizedTitle;

		// Token: 0x04001193 RID: 4499
		public Sprite icon;

		// Token: 0x04001194 RID: 4500
		public int playerScorePos = -1;
	}
}
