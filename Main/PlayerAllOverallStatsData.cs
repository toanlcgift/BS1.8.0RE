using System;

// Token: 0x020001F4 RID: 500
public class PlayerAllOverallStatsData
{
	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000787 RID: 1927 RVA: 0x000064FE File Offset: 0x000046FE
	public PlayerAllOverallStatsData.PlayerOverallStatsData allOverallStatsData
	{
		get
		{
			return this.campaignOverallStatsData + this.soloFreePlayOverallStatsData + this.partyFreePlayOverallStatsData;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000788 RID: 1928 RVA: 0x0000651C File Offset: 0x0000471C
	// (set) Token: 0x06000789 RID: 1929 RVA: 0x00006524 File Offset: 0x00004724
	public PlayerAllOverallStatsData.PlayerOverallStatsData campaignOverallStatsData { get; private set; }

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x0600078A RID: 1930 RVA: 0x0000652D File Offset: 0x0000472D
	// (set) Token: 0x0600078B RID: 1931 RVA: 0x00006535 File Offset: 0x00004735
	public PlayerAllOverallStatsData.PlayerOverallStatsData soloFreePlayOverallStatsData { get; private set; }

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x0600078C RID: 1932 RVA: 0x0000653E File Offset: 0x0000473E
	// (set) Token: 0x0600078D RID: 1933 RVA: 0x00006546 File Offset: 0x00004746
	public PlayerAllOverallStatsData.PlayerOverallStatsData partyFreePlayOverallStatsData { get; private set; }

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x0600078E RID: 1934 RVA: 0x0002863C File Offset: 0x0002683C
	// (remove) Token: 0x0600078F RID: 1935 RVA: 0x00028674 File Offset: 0x00026874
	public event Action<LevelCompletionResults, IDifficultyBeatmap> didUpdateSoloFreePlayOverallStatsDataEvent;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000790 RID: 1936 RVA: 0x000286AC File Offset: 0x000268AC
	// (remove) Token: 0x06000791 RID: 1937 RVA: 0x000286E4 File Offset: 0x000268E4
	public event Action<LevelCompletionResults, IDifficultyBeatmap> didUpdatePartyFreePlayOverallStatsDataEvent;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x06000792 RID: 1938 RVA: 0x0002871C File Offset: 0x0002691C
	// (remove) Token: 0x06000793 RID: 1939 RVA: 0x00028754 File Offset: 0x00026954
	public event Action<MissionCompletionResults, MissionNode> didUpdateCampaignOverallStatsDataEvent;

	// Token: 0x06000794 RID: 1940 RVA: 0x0000654F File Offset: 0x0000474F
	public PlayerAllOverallStatsData()
	{
		this.campaignOverallStatsData = new PlayerAllOverallStatsData.PlayerOverallStatsData();
		this.soloFreePlayOverallStatsData = new PlayerAllOverallStatsData.PlayerOverallStatsData();
		this.partyFreePlayOverallStatsData = new PlayerAllOverallStatsData.PlayerOverallStatsData();
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00006578 File Offset: 0x00004778
	public PlayerAllOverallStatsData(PlayerAllOverallStatsData.PlayerOverallStatsData campaignOverallStatsData, PlayerAllOverallStatsData.PlayerOverallStatsData soloFreePlayOverallStatsData, PlayerAllOverallStatsData.PlayerOverallStatsData partyFreePlayOverallStatsData)
	{
		this.campaignOverallStatsData = campaignOverallStatsData;
		this.soloFreePlayOverallStatsData = soloFreePlayOverallStatsData;
		this.partyFreePlayOverallStatsData = partyFreePlayOverallStatsData;
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00006595 File Offset: 0x00004795
	public void UpdateSoloFreePlayOverallStatsData(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap)
	{
		this.soloFreePlayOverallStatsData.UpdateWithLevelCompletionResults(levelCompletionResults);
		Action<LevelCompletionResults, IDifficultyBeatmap> action = this.didUpdateSoloFreePlayOverallStatsDataEvent;
		if (action == null)
		{
			return;
		}
		action(levelCompletionResults, difficultyBeatmap);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x000065B5 File Offset: 0x000047B5
	public void UpdatePartyFreePlayOverallStatsData(LevelCompletionResults levelCompletionResults, IDifficultyBeatmap difficultyBeatmap)
	{
		this.partyFreePlayOverallStatsData.UpdateWithLevelCompletionResults(levelCompletionResults);
		Action<LevelCompletionResults, IDifficultyBeatmap> action = this.didUpdatePartyFreePlayOverallStatsDataEvent;
		if (action == null)
		{
			return;
		}
		action(levelCompletionResults, difficultyBeatmap);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x000065D5 File Offset: 0x000047D5
	public void UpdateCampaignOverallStatsData(MissionCompletionResults missionCompletionResults, MissionNode missionNode)
	{
		this.campaignOverallStatsData.UpdateWithLevelCompletionResults(missionCompletionResults.levelCompletionResults);
		Action<MissionCompletionResults, MissionNode> action = this.didUpdateCampaignOverallStatsDataEvent;
		if (action == null)
		{
			return;
		}
		action(missionCompletionResults, missionNode);
	}

	// Token: 0x020001F5 RID: 501
	public class PlayerOverallStatsData
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000065FA File Offset: 0x000047FA
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00006602 File Offset: 0x00004802
		public int goodCutsCount { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000660B File Offset: 0x0000480B
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00006613 File Offset: 0x00004813
		public int badCutsCount { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000661C File Offset: 0x0000481C
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00006624 File Offset: 0x00004824
		public int missedCutsCount { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000662D File Offset: 0x0000482D
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00006635 File Offset: 0x00004835
		public long totalScore { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000663E File Offset: 0x0000483E
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00006646 File Offset: 0x00004846
		public int playedLevelsCount { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000664F File Offset: 0x0000484F
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00006657 File Offset: 0x00004857
		public int cleardLevelsCount { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00006660 File Offset: 0x00004860
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00006668 File Offset: 0x00004868
		public int failedLevelsCount { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00006671 File Offset: 0x00004871
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00006679 File Offset: 0x00004879
		public int fullComboCount { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00006682 File Offset: 0x00004882
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000668A File Offset: 0x0000488A
		public float timePlayed { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00006693 File Offset: 0x00004893
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000669B File Offset: 0x0000489B
		public int handDistanceTravelled { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x000066AC File Offset: 0x000048AC
		public long cummulativeCutScoreWithoutMultiplier { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x000066B5 File Offset: 0x000048B5
		public int averageCutScore
		{
			get
			{
				if (this.goodCutsCount <= 0)
				{
					return 0;
				}
				return (int)(this.cummulativeCutScoreWithoutMultiplier / (long)this.goodCutsCount);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00002198 File Offset: 0x00000398
		public PlayerOverallStatsData()
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002878C File Offset: 0x0002698C
		public PlayerOverallStatsData(int goodCutsCount, int badCutsCount, int missedCutsCount, long totalScore, int playedLevelsCount, int cleardLevelsCount, int failedLevelsCount, int fullComboCount, float timePlayed, int handDistanceTravelled, long cummulativeCutScoreWithoutMultiplier)
		{
			this.goodCutsCount = goodCutsCount;
			this.badCutsCount = badCutsCount;
			this.missedCutsCount = missedCutsCount;
			this.totalScore = totalScore;
			this.playedLevelsCount = playedLevelsCount;
			this.cleardLevelsCount = cleardLevelsCount;
			this.failedLevelsCount = failedLevelsCount;
			this.fullComboCount = fullComboCount;
			this.timePlayed = timePlayed;
			this.handDistanceTravelled = handDistanceTravelled;
			this.cummulativeCutScoreWithoutMultiplier = cummulativeCutScoreWithoutMultiplier;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000287F4 File Offset: 0x000269F4
		public static PlayerAllOverallStatsData.PlayerOverallStatsData operator +(PlayerAllOverallStatsData.PlayerOverallStatsData a, PlayerAllOverallStatsData.PlayerOverallStatsData b)
		{
			return new PlayerAllOverallStatsData.PlayerOverallStatsData(a.goodCutsCount + b.goodCutsCount, a.badCutsCount + b.badCutsCount, a.missedCutsCount + b.missedCutsCount, a.totalScore + b.totalScore, a.playedLevelsCount + b.playedLevelsCount, a.cleardLevelsCount + b.cleardLevelsCount, a.failedLevelsCount + b.failedLevelsCount, a.fullComboCount + b.fullComboCount, a.timePlayed + b.timePlayed, a.handDistanceTravelled + b.handDistanceTravelled, a.cummulativeCutScoreWithoutMultiplier + b.cummulativeCutScoreWithoutMultiplier);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00028898 File Offset: 0x00026A98
		public void UpdateWithLevelCompletionResults(LevelCompletionResults levelCompletionResults)
		{
			this.goodCutsCount += levelCompletionResults.goodCutsCount;
			this.badCutsCount += levelCompletionResults.badCutsCount;
			this.missedCutsCount += levelCompletionResults.missedCount;
			this.playedLevelsCount++;
			if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
			{
				int num = this.cleardLevelsCount;
				this.cleardLevelsCount = num + 1;
				this.totalScore += (long)levelCompletionResults.modifiedScore;
			}
			else if (levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
			{
				int num = this.failedLevelsCount;
				this.failedLevelsCount = num + 1;
			}
			if (levelCompletionResults.fullCombo)
			{
				int num = this.fullComboCount;
				this.fullComboCount = num + 1;
			}
			this.timePlayed += levelCompletionResults.endSongTime * levelCompletionResults.gameplayModifiers.songSpeedMul;
			this.handDistanceTravelled += (int)(levelCompletionResults.leftHandMovementDistance + levelCompletionResults.rightHandMovementDistance);
			this.cummulativeCutScoreWithoutMultiplier += (long)(levelCompletionResults.averageCutScore * levelCompletionResults.goodCutsCount);
		}
	}
}
