using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
[Serializable]
public class PlayerLevelStatsData
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x000069D2 File Offset: 0x00004BD2
	public string levelID
	{
		get
		{
			return this._levelID;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000804 RID: 2052 RVA: 0x000069DA File Offset: 0x00004BDA
	public BeatmapDifficulty difficulty
	{
		get
		{
			return this._difficulty;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000805 RID: 2053 RVA: 0x000069E2 File Offset: 0x00004BE2
	public BeatmapCharacteristicSO beatmapCharacteristic
	{
		get
		{
			return this._beatmapCharacteristic;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000806 RID: 2054 RVA: 0x000069EA File Offset: 0x00004BEA
	public int highScore
	{
		get
		{
			return this._highScore;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000807 RID: 2055 RVA: 0x000069F2 File Offset: 0x00004BF2
	public int maxCombo
	{
		get
		{
			return this._maxCombo;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000808 RID: 2056 RVA: 0x000069FA File Offset: 0x00004BFA
	public bool fullCombo
	{
		get
		{
			return this._fullCombo;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000809 RID: 2057 RVA: 0x00006A02 File Offset: 0x00004C02
	public RankModel.Rank maxRank
	{
		get
		{
			return this._maxRank;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x0600080A RID: 2058 RVA: 0x00006A0A File Offset: 0x00004C0A
	public bool validScore
	{
		get
		{
			return this._validScore;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x0600080B RID: 2059 RVA: 0x00006A12 File Offset: 0x00004C12
	public int playCount
	{
		get
		{
			return this._playCount;
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00006A1A File Offset: 0x00004C1A
	public PlayerLevelStatsData(string levelID, BeatmapDifficulty difficulty, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this._levelID = levelID;
		this._difficulty = difficulty;
		this._beatmapCharacteristic = beatmapCharacteristic;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00029E58 File Offset: 0x00028058
	public PlayerLevelStatsData(string levelID, BeatmapDifficulty difficulty, BeatmapCharacteristicSO beatmapCharacteristic, int highScore, int maxCombo, bool fullCombo, RankModel.Rank maxRank, bool validScore, int playCount)
	{
		this._levelID = levelID;
		this._difficulty = difficulty;
		this._beatmapCharacteristic = beatmapCharacteristic;
		this._highScore = highScore;
		this._maxCombo = maxCombo;
		this._fullCombo = fullCombo;
		this._maxRank = maxRank;
		this._validScore = validScore;
		this._playCount = playCount;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00029EB0 File Offset: 0x000280B0
	public void UpdateScoreData(int score, int maxCombo, bool fullCombo, RankModel.Rank rank)
	{
		this._highScore = Mathf.Max(this._highScore, score);
		this._maxCombo = Mathf.Max(this._maxCombo, maxCombo);
		this._fullCombo = (this._fullCombo || fullCombo);
		this._maxRank = (RankModel.Rank)Mathf.Max((int)this._maxRank, (int)rank);
		this._validScore = true;
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00006A37 File Offset: 0x00004C37
	public void IncreaseNumberOfGameplays()
	{
		this._playCount++;
	}

	// Token: 0x04000853 RID: 2131
	[SerializeField]
	private int _highScore;

	// Token: 0x04000854 RID: 2132
	[SerializeField]
	private int _maxCombo;

	// Token: 0x04000855 RID: 2133
	[SerializeField]
	private bool _fullCombo;

	// Token: 0x04000856 RID: 2134
	[SerializeField]
	private RankModel.Rank _maxRank;

	// Token: 0x04000857 RID: 2135
	[SerializeField]
	private bool _validScore;

	// Token: 0x04000858 RID: 2136
	[SerializeField]
	private int _playCount;

	// Token: 0x04000859 RID: 2137
	[SerializeField]
	private string _levelID;

	// Token: 0x0400085A RID: 2138
	[SerializeField]
	private BeatmapDifficulty _difficulty;

	// Token: 0x0400085B RID: 2139
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;
}
