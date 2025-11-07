using System;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x020003FE RID: 1022
public class PlayerStatisticsViewController : ViewController
{
	// Token: 0x0600133B RID: 4923 RVA: 0x00047A1C File Offset: 0x00045C1C
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._statsScopeDatas = new PlayerStatisticsViewController.StatsScopeData[]
			{
				new PlayerStatisticsViewController.StatsScopeData(Localization.Get("STATS_ALL"), () => this._playerDataModel.playerData.playerAllOverallStatsData.allOverallStatsData),
				new PlayerStatisticsViewController.StatsScopeData(Localization.Get("STATS_CAMPAIGN"), () => this._playerDataModel.playerData.playerAllOverallStatsData.campaignOverallStatsData),
				new PlayerStatisticsViewController.StatsScopeData(Localization.Get("STATS_SOLO"), () => this._playerDataModel.playerData.playerAllOverallStatsData.soloFreePlayOverallStatsData),
				new PlayerStatisticsViewController.StatsScopeData(Localization.Get("STATS_PARTY"), () => this._playerDataModel.playerData.playerAllOverallStatsData.partyFreePlayOverallStatsData)
			};
			string[] array = new string[this._statsScopeDatas.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._statsScopeDatas[i].text;
			}
			this._statsScopeSegmentedControl.SetTexts(array);
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._statsScopeSegmentedControl.didSelectCellEvent += this.HandleStatsScopeSegmentedControlDidSelectCell;
		}
		Func<PlayerAllOverallStatsData.PlayerOverallStatsData> playerOverallStatsDataFunc = this._statsScopeDatas[this._statsScopeSegmentedControl.selectedCellNumber].playerOverallStatsDataFunc;
		this.UpdateView(playerOverallStatsDataFunc());
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x0000E7C1 File Offset: 0x0000C9C1
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._statsScopeSegmentedControl.didSelectCellEvent -= this.HandleStatsScopeSegmentedControlDidSelectCell;
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x00047B44 File Offset: 0x00045D44
	private void UpdateView(PlayerAllOverallStatsData.PlayerOverallStatsData playerOverallStatsData)
	{
		this._playedLevelsCountText.text = playerOverallStatsData.playedLevelsCount.ToString("# ### ### ##0");
		this._clearedLevelsCountText.text = playerOverallStatsData.cleardLevelsCount.ToString("# ### ### ##0");
		this._failedLevelsCountText.text = playerOverallStatsData.failedLevelsCount.ToString("# ### ### ##0");
		float timePlayed = playerOverallStatsData.timePlayed;
		int num = timePlayed.TotalHours();
		int num2 = timePlayed.Minutes();
		int num3 = timePlayed.Seconds();
		if (num > 0)
		{
			this._timePlayedText.text = string.Concat(new object[]
			{
				num,
				"h ",
				num2,
				"m"
			});
		}
		else
		{
			this._timePlayedText.text = string.Concat(new object[]
			{
				num2,
				"m ",
				num3,
				"s"
			});
		}
		this._goodCutsCountText.text = playerOverallStatsData.goodCutsCount.ToString("# ### ### ##0");
		this._badCutsCountCountText.text = playerOverallStatsData.badCutsCount.ToString("# ### ### ##0");
		this._missedCountText.text = playerOverallStatsData.missedCutsCount.ToString("# ### ### ##0");
		this._averageCutScoreText.text = playerOverallStatsData.averageCutScore.ToString("# ### ### ##0");
		this._totalScoreText.text = playerOverallStatsData.totalScore.ToString("# ### ### ##0");
		this._fullComboCountText.text = playerOverallStatsData.fullComboCount.ToString("# ### ### ##0");
		if (playerOverallStatsData.handDistanceTravelled > 1000)
		{
			this._handDistanceTravelledText.text = ((float)playerOverallStatsData.handDistanceTravelled / 1000f).ToString("### ### ###.0") + "km";
			return;
		}
		this._handDistanceTravelledText.text = playerOverallStatsData.handDistanceTravelled.ToString() + "m";
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x00047D54 File Offset: 0x00045F54
	private void HandleStatsScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int cellIdx)
	{
		Func<PlayerAllOverallStatsData.PlayerOverallStatsData> playerOverallStatsDataFunc = this._statsScopeDatas[cellIdx].playerOverallStatsDataFunc;
		this.UpdateView(playerOverallStatsDataFunc());
	}

	// Token: 0x040012EB RID: 4843
	[SerializeField]
	private TextSegmentedControl _statsScopeSegmentedControl;

	// Token: 0x040012EC RID: 4844
	[Space]
	[SerializeField]
	private TextMeshProUGUI _playedLevelsCountText;

	// Token: 0x040012ED RID: 4845
	[SerializeField]
	private TextMeshProUGUI _clearedLevelsCountText;

	// Token: 0x040012EE RID: 4846
	[SerializeField]
	private TextMeshProUGUI _failedLevelsCountText;

	// Token: 0x040012EF RID: 4847
	[SerializeField]
	private TextMeshProUGUI _timePlayedText;

	// Token: 0x040012F0 RID: 4848
	[SerializeField]
	private TextMeshProUGUI _goodCutsCountText;

	// Token: 0x040012F1 RID: 4849
	[SerializeField]
	private TextMeshProUGUI _badCutsCountCountText;

	// Token: 0x040012F2 RID: 4850
	[SerializeField]
	private TextMeshProUGUI _missedCountText;

	// Token: 0x040012F3 RID: 4851
	[SerializeField]
	private TextMeshProUGUI _averageCutScoreText;

	// Token: 0x040012F4 RID: 4852
	[SerializeField]
	private TextMeshProUGUI _totalScoreText;

	// Token: 0x040012F5 RID: 4853
	[SerializeField]
	private TextMeshProUGUI _fullComboCountText;

	// Token: 0x040012F6 RID: 4854
	[SerializeField]
	private TextMeshProUGUI _handDistanceTravelledText;

	// Token: 0x040012F7 RID: 4855
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x040012F8 RID: 4856
	private PlayerStatisticsViewController.StatsScopeData[] _statsScopeDatas;

	// Token: 0x020003FF RID: 1023
	private struct StatsScopeData
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0000E839 File Offset: 0x0000CA39
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x0000E841 File Offset: 0x0000CA41
		public string text { get; private set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0000E84A File Offset: 0x0000CA4A
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0000E852 File Offset: 0x0000CA52
		public Func<PlayerAllOverallStatsData.PlayerOverallStatsData> playerOverallStatsDataFunc { get; private set; }

		// Token: 0x06001348 RID: 4936 RVA: 0x0000E85B File Offset: 0x0000CA5B
		public StatsScopeData(string text, Func<PlayerAllOverallStatsData.PlayerOverallStatsData> playerOverallStatsDataFunc)
		{
			this.text = text;
			this.playerOverallStatsDataFunc = playerOverallStatsDataFunc;
		}
	}
}
