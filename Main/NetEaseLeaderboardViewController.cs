using System;
using System.Collections;
using System.Collections.Generic;
using HMUI;
using NetEase.Docker;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000014 RID: 20
public class NetEaseLeaderboardViewController : LeaderboardViewController
{
	// Token: 0x06000051 RID: 81 RVA: 0x00002465 File Offset: 0x00000665
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

	// Token: 0x06000052 RID: 82 RVA: 0x00002496 File Offset: 0x00000696
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._loadingControl.didPressRefreshButtonEvent += this.HandleLoadingControlDidPressRefreshButton;
		}
		if (this._refreshIsNeeded || activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this.Refresh(true, true);
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000024C5 File Offset: 0x000006C5
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (!this._hasScoresData)
		{
			this._refreshIsNeeded = true;
		}
		this._loadingControl.Hide();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000024E1 File Offset: 0x000006E1
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._loadingControl)
		{
			this._loadingControl.didPressRefreshButtonEvent -= this.HandleLoadingControlDidPressRefreshButton;
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000250D File Offset: 0x0000070D
	private void HandleLoadingControlDidPressRefreshButton()
	{
		this.Refresh(true, true);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000250D File Offset: 0x0000070D
	public void Refresh()
	{
		this.Refresh(true, true);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00015360 File Offset: 0x00013560
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

	// Token: 0x06000058 RID: 88 RVA: 0x00002517 File Offset: 0x00000717
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
		this.LoadScoresAsync();
		yield break;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000153D4 File Offset: 0x000135D4
	private async void LoadScoresAsync()
	{
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap);
		try
		{
			ReceivedHighscoreData receivedHighscoreData = await this._netEaseManager.RequestHighscoreDataAsync(leaderboardID);
			if (!(receivedHighscoreData.Track != LeaderboardsModel.GetLeaderboardID(this._difficultyBeatmap)))
			{
				if (!receivedHighscoreData.FetchingSuccessful)
				{
					this.ClearContent();
					this._loadingControl.ShowText(Localization.Get("LEADERBOARDS_LOADING_FAILED"), true);
				}
				else
				{
					this._loadingControl.Hide();
					this._hasScoresData = true;
					this._scores.Clear();
					int num = 0;
					while (num < 10 && num < receivedHighscoreData.Scores.Count)
					{
						ScoreData scoreData = receivedHighscoreData.Scores[num];
						this._scores.Add(new LeaderboardTableView.ScoreData(scoreData.Score, scoreData.Name, 0, false));
						num++;
					}
					this._leaderboardTableView.SetScores(this._scores, -1);
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002534 File Offset: 0x00000734
	private void ClearContent()
	{
		this._hasScoresData = false;
		this._scores.Clear();
		this._leaderboardTableView.SetScores(this._scores, -1);
	}

	// Token: 0x04000045 RID: 69
	[SerializeField]
	private LeaderboardTableView _leaderboardTableView;

	// Token: 0x04000046 RID: 70
	[SerializeField]
	private LoadingControl _loadingControl;

	// Token: 0x04000047 RID: 71
	[Inject]
	private NetEaseManager _netEaseManager;

	// Token: 0x04000048 RID: 72
	private List<LeaderboardTableView.ScoreData> _scores = new List<LeaderboardTableView.ScoreData>(10);

	// Token: 0x04000049 RID: 73
	private IDifficultyBeatmap _difficultyBeatmap;

	// Token: 0x0400004A RID: 74
	private bool _refreshIsNeeded;

	// Token: 0x0400004B RID: 75
	private bool _hasScoresData;
}
