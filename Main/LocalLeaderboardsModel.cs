using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class LocalLeaderboardsModel : PersistentScriptableObject
{
	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000355 RID: 853 RVA: 0x0001F19C File Offset: 0x0001D39C
	// (remove) Token: 0x06000356 RID: 854 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
	public event Action<string, LocalLeaderboardsModel.LeaderboardType> newScoreWasAddedToLeaderboardEvent;

	// Token: 0x06000357 RID: 855 RVA: 0x00004141 File Offset: 0x00002341
	protected override void OnEnable()
	{
		base.OnEnable();
		this.Load();
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0000414F File Offset: 0x0000234F
	protected void OnDisable()
	{
		this.Save();
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001F20C File Offset: 0x0001D40C
	private static void LoadLeaderboardsData(string filename, out List<LocalLeaderboardsModel.LeaderboardData> leaderboardsData)
	{
		leaderboardsData = null;
		string path = Application.persistentDataPath + "/" + filename;
		if (File.Exists(path))
		{
			string text = File.ReadAllText(path);
			if (!string.IsNullOrEmpty(text))
			{
				LocalLeaderboardsModel.SavedLeaderboardsData savedLeaderboardsData = JsonUtility.FromJson<LocalLeaderboardsModel.SavedLeaderboardsData>(text);
				if (savedLeaderboardsData != null)
				{
					leaderboardsData = savedLeaderboardsData._leaderboardsData;
				}
			}
		}
		if (leaderboardsData == null)
		{
			leaderboardsData = new List<LocalLeaderboardsModel.LeaderboardData>();
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001F264 File Offset: 0x0001D464
	private static void SaveLeaderboardsData(string filename, List<LocalLeaderboardsModel.LeaderboardData> leaderboardsData)
	{
		if (leaderboardsData == null)
		{
			return;
		}
		string contents = JsonUtility.ToJson(new LocalLeaderboardsModel.SavedLeaderboardsData
		{
			_leaderboardsData = leaderboardsData
		});
		File.WriteAllText(Application.persistentDataPath + "/" + filename, contents);
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
	public void Load()
	{
		LocalLeaderboardsModel.LoadLeaderboardsData("LocalLeaderboards.dat", out this._leaderboardsData);
		LocalLeaderboardsModel.LoadLeaderboardsData("LocalDailyLeaderboards.dat", out this._dailyLeaderboardsData);
		for (int i = 0; i < this._dailyLeaderboardsData.Count; i++)
		{
			LocalLeaderboardsModel.LeaderboardData leaderboardData = this._dailyLeaderboardsData[i];
			this.UpdateDailyLeaderboard(leaderboardData._leaderboardId);
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00004157 File Offset: 0x00002357
	public void Save()
	{
		LocalLeaderboardsModel.SaveLeaderboardsData("LocalLeaderboards.dat", this._leaderboardsData);
		LocalLeaderboardsModel.SaveLeaderboardsData("LocalDailyLeaderboards.dat", this._dailyLeaderboardsData);
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001F2FC File Offset: 0x0001D4FC
	private List<LocalLeaderboardsModel.LeaderboardData> GetLeaderboardsData(LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		List<LocalLeaderboardsModel.LeaderboardData> result = null;
		if (leaderboardType == LocalLeaderboardsModel.LeaderboardType.AllTime)
		{
			result = this._leaderboardsData;
		}
		else if (leaderboardType == LocalLeaderboardsModel.LeaderboardType.Daily)
		{
			result = this._dailyLeaderboardsData;
		}
		return result;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001F324 File Offset: 0x0001D524
	private LocalLeaderboardsModel.LeaderboardData GetLeaderboardData(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		List<LocalLeaderboardsModel.LeaderboardData> leaderboardsData = this.GetLeaderboardsData(leaderboardType);
		for (int i = 0; i < leaderboardsData.Count; i++)
		{
			LocalLeaderboardsModel.LeaderboardData leaderboardData = leaderboardsData[i];
			if (leaderboardData._leaderboardId == leaderboardId)
			{
				return leaderboardData;
			}
		}
		return null;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0001F364 File Offset: 0x0001D564
	private long GetCurrentTimestamp()
	{
		DateTime dateTime = DateTime.Now.ToUniversalTime();
		DateTime value = new DateTime(1970, 1, 1);
		return (long)dateTime.Subtract(value).TotalSeconds;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
	protected void UpdateDailyLeaderboard(string leaderboardId)
	{
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, LocalLeaderboardsModel.LeaderboardType.Daily);
		long num = this.GetCurrentTimestamp() - 86400L;
		if (leaderboardData != null)
		{
			for (int i = leaderboardData._scores.Count - 1; i >= 0; i--)
			{
				if (leaderboardData._scores[i]._timestamp < num)
				{
					leaderboardData._scores.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001F400 File Offset: 0x0001D600
	private void AddScore(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType, string playerName, int score, bool fullCombo)
	{
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, leaderboardType);
		int i = 0;
		if (leaderboardData != null)
		{
			List<LocalLeaderboardsModel.ScoreData> scores = leaderboardData._scores;
			for (i = 0; i < scores.Count; i++)
			{
				if (scores[i]._score < score)
				{
					break;
				}
			}
		}
		else
		{
			leaderboardData = new LocalLeaderboardsModel.LeaderboardData();
			leaderboardData._leaderboardId = leaderboardId;
			leaderboardData._scores = new List<LocalLeaderboardsModel.ScoreData>(this._maxNumberOfScoresInLeaderboard);
			this.GetLeaderboardsData(leaderboardType).Add(leaderboardData);
		}
		if (i < this._maxNumberOfScoresInLeaderboard)
		{
			LocalLeaderboardsModel.ScoreData scoreData = new LocalLeaderboardsModel.ScoreData();
			scoreData._score = score;
			scoreData._playerName = playerName;
			scoreData._fullCombo = fullCombo;
			scoreData._timestamp = this.GetCurrentTimestamp();
			List<LocalLeaderboardsModel.ScoreData> scores2 = leaderboardData._scores;
			scores2.Insert(i, scoreData);
			if (scores2.Count > this._maxNumberOfScoresInLeaderboard)
			{
				scores2.RemoveAt(scores2.Count - 1);
			}
		}
		this._lastScorePositions[leaderboardType] = i;
		this._lastScoreLeaderboardId = leaderboardId;
		if (this.newScoreWasAddedToLeaderboardEvent != null)
		{
			this.newScoreWasAddedToLeaderboardEvent(leaderboardId, leaderboardType);
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001F4FC File Offset: 0x0001D6FC
	private bool WillScoreGoIntoLeaderboard(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType, int score)
	{
		if (leaderboardType == LocalLeaderboardsModel.LeaderboardType.Daily)
		{
			this.UpdateDailyLeaderboard(leaderboardId);
		}
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null)
		{
			List<LocalLeaderboardsModel.ScoreData> scores = leaderboardData._scores;
			return scores.Count < this._maxNumberOfScoresInLeaderboard || scores[scores.Count - 1]._score < score;
		}
		return true;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001F550 File Offset: 0x0001D750
	public List<LocalLeaderboardsModel.ScoreData> GetScores(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null)
		{
			return leaderboardData._scores;
		}
		return null;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001F574 File Offset: 0x0001D774
	public int GetHighScore(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null && leaderboardData._scores.Count > 0)
		{
			return leaderboardData._scores[0]._score;
		}
		return 0;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
	public int GetPositionInLeaderboard(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType, int score)
	{
		LocalLeaderboardsModel.LeaderboardData leaderboardData = this.GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData == null)
		{
			return 0;
		}
		List<LocalLeaderboardsModel.ScoreData> scores = leaderboardData._scores;
		int num = 0;
		while (num < scores.Count && scores[num]._score >= score)
		{
			num++;
		}
		if (num < this._maxNumberOfScoresInLeaderboard)
		{
			return num;
		}
		return -1;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001F600 File Offset: 0x0001D800
	public int GetLastScorePosition(string leaderboardId, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		int result;
		if (this._lastScoreLeaderboardId == leaderboardId && this._lastScorePositions.TryGetValue(leaderboardType, out result))
		{
			return result;
		}
		return -1;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00004179 File Offset: 0x00002379
	public void ClearLastScorePosition()
	{
		this._lastScorePositions.Clear();
		this._lastScoreLeaderboardId = null;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0000418D File Offset: 0x0000238D
	public void AddScore(string leaderboardId, string playerName, int score, bool fullCombo)
	{
		this.AddScore(leaderboardId, LocalLeaderboardsModel.LeaderboardType.AllTime, playerName, score, fullCombo);
		this.AddScore(leaderboardId, LocalLeaderboardsModel.LeaderboardType.Daily, playerName, score, fullCombo);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000041A7 File Offset: 0x000023A7
	public bool WillScoreGoIntoLeaderboard(string leaderboardId, int score)
	{
		return this.WillScoreGoIntoLeaderboard(leaderboardId, LocalLeaderboardsModel.LeaderboardType.AllTime, score) || this.WillScoreGoIntoLeaderboard(leaderboardId, LocalLeaderboardsModel.LeaderboardType.Daily, score);
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0001F630 File Offset: 0x0001D830
	public void ClearLeaderboard(string leaderboardId)
	{
		for (int i = 0; i < this._leaderboardsData.Count; i++)
		{
			if (this._leaderboardsData[i]._leaderboardId == leaderboardId)
			{
				this._leaderboardsData.RemoveAt(i);
				return;
			}
		}
		for (int j = 0; j < this._dailyLeaderboardsData.Count; j++)
		{
			if (this._dailyLeaderboardsData[j]._leaderboardId == leaderboardId)
			{
				this._dailyLeaderboardsData.RemoveAt(j);
				return;
			}
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
	public void ClearAllLeaderboards(bool deleteLeaderboardFile)
	{
		this._leaderboardsData.Clear();
		this._dailyLeaderboardsData.Clear();
		if (deleteLeaderboardFile)
		{
			File.Delete(Application.persistentDataPath + "/LocalLeaderboards.dat");
			File.Delete(Application.persistentDataPath + "/LocalDailyLeaderboards.dat");
		}
	}

	// Token: 0x040003B6 RID: 950
	[SerializeField]
	private int _maxNumberOfScoresInLeaderboard = 10;

	// Token: 0x040003B8 RID: 952
	private const string kLocalLeaderboardsFileName = "LocalLeaderboards.dat";

	// Token: 0x040003B9 RID: 953
	private const string kLocalDailyLeaderboardsFileName = "LocalDailyLeaderboards.dat";

	// Token: 0x040003BA RID: 954
	private Dictionary<LocalLeaderboardsModel.LeaderboardType, int> _lastScorePositions = new Dictionary<LocalLeaderboardsModel.LeaderboardType, int>();

	// Token: 0x040003BB RID: 955
	private string _lastScoreLeaderboardId;

	// Token: 0x040003BC RID: 956
	private List<LocalLeaderboardsModel.LeaderboardData> _leaderboardsData;

	// Token: 0x040003BD RID: 957
	private List<LocalLeaderboardsModel.LeaderboardData> _dailyLeaderboardsData;

	// Token: 0x020000DD RID: 221
	public enum LeaderboardType
	{
		// Token: 0x040003BF RID: 959
		AllTime,
		// Token: 0x040003C0 RID: 960
		Daily
	}

	// Token: 0x020000DE RID: 222
	[Serializable]
	public class ScoreData
	{
		// Token: 0x040003C1 RID: 961
		public int _score;

		// Token: 0x040003C2 RID: 962
		public string _playerName;

		// Token: 0x040003C3 RID: 963
		public bool _fullCombo;

		// Token: 0x040003C4 RID: 964
		public long _timestamp;
	}

	// Token: 0x020000DF RID: 223
	[Serializable]
	private class LeaderboardData
	{
		// Token: 0x040003C5 RID: 965
		public string _leaderboardId;

		// Token: 0x040003C6 RID: 966
		public List<LocalLeaderboardsModel.ScoreData> _scores;
	}

	// Token: 0x020000E0 RID: 224
	[Serializable]
	private class SavedLeaderboardsData
	{
		// Token: 0x040003C7 RID: 967
		public List<LocalLeaderboardsModel.LeaderboardData> _leaderboardsData;
	}
}
