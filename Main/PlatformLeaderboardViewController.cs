using System;
using System.Collections;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x020003FC RID: 1020
public class PlatformLeaderboardViewController : LeaderboardViewController
{
	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001325 RID: 4901 RVA: 0x0000E6C6 File Offset: 0x0000C8C6
	public PlatformLeaderboardsModel leaderboardsModel
	{
		get
		{
			return this._leaderboardsModel;
		}
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0000E6CE File Offset: 0x0000C8CE
	public override void SetData(IDifficultyBeatmap difficultyBeatmap)
	{
		if (this._difficultyBeatmap != difficultyBeatmap)
		{
			this._refreshIsNeeded = true;
		}
		this._difficultyBeatmap = difficultyBeatmap;
		if (base.isActivated && this._refreshIsNeeded)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00047558 File Offset: 0x00045758
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._playerScorePos = new int[]
			{
				-1,
				-1,
				-1
			};
			this._scopeSegmentedControl.didSelectCellEvent += this.HandleScopeSegmentedControlDidSelectCell;
			base.rectTransform.anchorMin = Vector2.zero;
			base.rectTransform.anchorMax = Vector2.one;
			base.rectTransform.offsetMin = Vector2.zero;
			base.rectTransform.offsetMax = Vector2.zero;
			this._scoreScopes = new PlatformLeaderboardsModel.ScoresScope[]
			{
				PlatformLeaderboardsModel.ScoresScope.Global,
				PlatformLeaderboardsModel.ScoresScope.AroundPlayer,
				PlatformLeaderboardsModel.ScoresScope.Friends
			};
			this._scopeSegmentedControl.SetData(new IconSegmentedControl.DataItem[]
			{
				new IconSegmentedControl.DataItem(this._globalLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_GLOBAL")),
				new IconSegmentedControl.DataItem(this._aroundPlayerLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_AROUND_YOU")),
				new IconSegmentedControl.DataItem(this._friendsLeaderboardIcon, Localization.Get("BUTTON_HIGHSCORES_FRIENDS"))
			});
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._scopeSegmentedControl.SelectCellWithNumber(this.ScoreScopeToScoreScopeIndex(PlatformLeaderboardViewController._scoresScope));
		}
		this._leaderboardsModel.allScoresDidUploadEvent += this.HandlePlatformLeaderboardsModelAllScoresDidUpload;
		this._loadingControl.didPressRefreshButtonEvent += this.HandleDidPressRefreshButton;
		if (this._refreshIsNeeded || activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000476A0 File Offset: 0x000458A0
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		this._leaderboardsModel.allScoresDidUploadEvent -= this.HandlePlatformLeaderboardsModelAllScoresDidUpload;
		this._loadingControl.didPressRefreshButtonEvent -= this.HandleDidPressRefreshButton;
		if (this._getScoresAsyncRequest != null)
		{
			this._getScoresAsyncRequest.Cancel();
			this._getScoresAsyncRequest = null;
			this._refreshIsNeeded = true;
		}
		if (!this._hasScoresData)
		{
			this._refreshIsNeeded = true;
		}
		this._loadingControl.Hide();
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00047718 File Offset: 0x00045918
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._scopeSegmentedControl != null)
		{
			this._scopeSegmentedControl.didSelectCellEvent -= this.HandleScopeSegmentedControlDidSelectCell;
		}
		if (this._loadingControl != null)
		{
			this._loadingControl.didPressRefreshButtonEvent -= this.HandleDidPressRefreshButton;
		}
		this._leaderboardsModel.allScoresDidUploadEvent -= this.HandlePlatformLeaderboardsModelAllScoresDidUpload;
		if (this._getScoresAsyncRequest != null)
		{
			this._getScoresAsyncRequest.Cancel();
			this._getScoresAsyncRequest = null;
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000477A8 File Offset: 0x000459A8
	private int ScoreScopeToScoreScopeIndex(PlatformLeaderboardsModel.ScoresScope scoresScope)
	{
		for (int i = 0; i < this._scoreScopes.Length; i++)
		{
			if (this._scoreScopes[i] == PlatformLeaderboardViewController._scoresScope)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0000E6FF File Offset: 0x0000C8FF
	private PlatformLeaderboardsModel.ScoresScope ScopeScopeIndexToScoreScope(int scoreScopeIndex)
	{
		if (scoreScopeIndex < this._scoreScopes.Length)
		{
			return this._scoreScopes[scoreScopeIndex];
		}
		return this._scoreScopes[0];
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0000E71D File Offset: 0x0000C91D
	private void HandleDidPressRefreshButton()
	{
		this.Refresh(true, true);
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000477DC File Offset: 0x000459DC
	private void HandleLeaderboardsResultsReturned(PlatformLeaderboardsModel.GetScoresResult result, PlatformLeaderboardsModel.LeaderboardScore[] scores, int playerScoreIndex)
	{
		this._loadingControl.Hide();
		this._getScoresAsyncRequest = null;
		if (result == PlatformLeaderboardsModel.GetScoresResult.OK)
		{
			this._hasScoresData = true;
			this._scores.Clear();
			for (int i = 0; i < 10; i++)
			{
				if (i < scores.Length)
				{
					PlatformLeaderboardsModel.LeaderboardScore leaderboardScore = scores[i];
					this._scores.Add(new LeaderboardTableView.ScoreData(leaderboardScore.score, leaderboardScore.playerName, leaderboardScore.rank, false));
				}
			}
			this._playerScorePos[(int)PlatformLeaderboardViewController._scoresScope] = playerScoreIndex;
			this._leaderboardTableView.SetScores(this._scores, this._playerScorePos[(int)PlatformLeaderboardViewController._scoresScope]);
			return;
		}
		this._loadingControl.ShowText("Oops something went wrong. You may want to check your internet connection.", false);
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0000E727 File Offset: 0x0000C927
	private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellNumber)
	{
		PlatformLeaderboardViewController._scoresScope = this.ScopeScopeIndexToScoreScope(cellNumber);
		this.Refresh(true, true);
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x0000E73D File Offset: 0x0000C93D
	private void HandlePlatformLeaderboardsModelAllScoresDidUpload()
	{
		this.Refresh(false, false);
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x00047888 File Offset: 0x00045A88
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

	// Token: 0x06001331 RID: 4913 RVA: 0x0000E747 File Offset: 0x0000C947
	private IEnumerator RefreshDelayed(bool showLoadingIndicator, bool clear)
	{
		this._refreshIsNeeded = false;
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
		if (this._getScoresAsyncRequest != null)
		{
			this._getScoresAsyncRequest.Cancel();
		}
		if (PlatformLeaderboardViewController._scoresScope == PlatformLeaderboardsModel.ScoresScope.Global)
		{
			this._getScoresAsyncRequest = this._leaderboardsModel.GetScores(this._difficultyBeatmap, 10, 1, new PlatformLeaderboardsModel.GetScoresCompletionHandler(this.HandleLeaderboardsResultsReturned));
		}
		else if (PlatformLeaderboardViewController._scoresScope == PlatformLeaderboardsModel.ScoresScope.Friends)
		{
			this._getScoresAsyncRequest = this._leaderboardsModel.GetFriendsScores(this._difficultyBeatmap, 10, 1, new PlatformLeaderboardsModel.GetScoresCompletionHandler(this.HandleLeaderboardsResultsReturned));
		}
		else if (PlatformLeaderboardViewController._scoresScope == PlatformLeaderboardsModel.ScoresScope.AroundPlayer)
		{
			this._getScoresAsyncRequest = this._leaderboardsModel.GetScoresAroundPlayer(this._difficultyBeatmap, 10, new PlatformLeaderboardsModel.GetScoresCompletionHandler(this.HandleLeaderboardsResultsReturned));
		}
		yield break;
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0000E764 File Offset: 0x0000C964
	private void ClearContent()
	{
		this._hasScoresData = false;
		this._scores.Clear();
		this._leaderboardTableView.SetScores(this._scores, this._playerScorePos[(int)PlatformLeaderboardViewController._scoresScope]);
	}

	// Token: 0x040012D7 RID: 4823
	[SerializeField]
	private LeaderboardTableView _leaderboardTableView;

	// Token: 0x040012D8 RID: 4824
	[SerializeField]
	private IconSegmentedControl _scopeSegmentedControl;

	// Token: 0x040012D9 RID: 4825
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x040012DA RID: 4826
	[Space]
	[SerializeField]
	private Sprite _globalLeaderboardIcon;

	// Token: 0x040012DB RID: 4827
	[SerializeField]
	private Sprite _aroundPlayerLeaderboardIcon;

	// Token: 0x040012DC RID: 4828
	[SerializeField]
	private Sprite _friendsLeaderboardIcon;

	// Token: 0x040012DD RID: 4829
	[Inject]
	private PlatformLeaderboardsModel _leaderboardsModel;

	// Token: 0x040012DE RID: 4830
	[DoesNotRequireDomainReloadInit]
	private static PlatformLeaderboardsModel.ScoresScope _scoresScope;

	// Token: 0x040012DF RID: 4831
	private HMAsyncRequest _getScoresAsyncRequest;

	// Token: 0x040012E0 RID: 4832
	private int[] _playerScorePos;

	// Token: 0x040012E1 RID: 4833
	private List<LeaderboardTableView.ScoreData> _scores = new List<LeaderboardTableView.ScoreData>(10);

	// Token: 0x040012E2 RID: 4834
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x040012E3 RID: 4835
	private bool _refreshIsNeeded;

	// Token: 0x040012E4 RID: 4836
	private bool _hasScoresData;

	// Token: 0x040012E5 RID: 4837
	private PlatformLeaderboardsModel.ScoresScope[] _scoreScopes;
}
