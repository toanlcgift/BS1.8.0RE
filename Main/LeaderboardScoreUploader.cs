using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class LeaderboardScoreUploader : MonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000329 RID: 809 RVA: 0x0001ED78 File Offset: 0x0001CF78
	// (remove) Token: 0x0600032A RID: 810 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
	public event Action allScoresDidUploadEvent;

	// Token: 0x0600032B RID: 811 RVA: 0x00004024 File Offset: 0x00002224
	public void Init(LeaderboardScoreUploader.UploadScoreCallback uploadScoreCallback, string playerId)
	{
		if (uploadScoreCallback == null || playerId == null)
		{
			return;
		}
		this._uploadScoreCallback = uploadScoreCallback;
		this._playerId = playerId;
		base.StartCoroutine(this.UploadScoresCoroutine());
	}

	// Token: 0x0600032C RID: 812 RVA: 0x000023E9 File Offset: 0x000005E9
	private void OnApplicationQuit()
	{
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00004048 File Offset: 0x00002248
	private IEnumerator UploadScoresCoroutine()
	{
		while (this._scoresToUploadForCurrentPlayer.Count > 0)
		{
			LeaderboardScoreUploader.ScoreData scoreData = this._scoresToUploadForCurrentPlayer[0];
			scoreData.uploadAttemptCount++;
			scoreData.currentUploadAttemptCount++;
			this._uploading = true;
			this._uploadScoreCallback(scoreData, delegate(PlatformLeaderboardsModel.UploadScoreResult result)
			{
				this._uploading = false;
				if (result == PlatformLeaderboardsModel.UploadScoreResult.OK)
				{
					this._scoresToUploadForCurrentPlayer.RemoveAt(0);
				}
				else
				{
					scoreData = this._scoresToUploadForCurrentPlayer[0];
					this._scoresToUploadForCurrentPlayer.RemoveAt(0);
					if (scoreData.currentUploadAttemptCount < 3)
					{
						this._scoresToUploadForCurrentPlayer.Add(scoreData);
					}
					else
					{
						this._scoresToUpload.Add(scoreData);
					}
				}
				if (this._scoresToUploadForCurrentPlayer.Count == 0 && this.allScoresDidUploadEvent != null)
				{
					this.allScoresDidUploadEvent();
				}
			});
			WaitUntil waitUntil = new WaitUntil(() => !this._uploading);
			yield return waitUntil;
		}
		yield break;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
	private void LoadScoresToUploadFromFile()
	{
		this._scoresToUpload = null;
		string path = Application.persistentDataPath + "/ScoresToUpload.dat";
		if (File.Exists(path))
		{
			string text = File.ReadAllText(path);
			if (!string.IsNullOrEmpty(text))
			{
				LeaderboardScoreUploader.ScoresToUploadData scoresToUploadData = JsonUtility.FromJson<LeaderboardScoreUploader.ScoresToUploadData>(text);
				if (scoresToUploadData != null)
				{
					this._scoresToUpload = scoresToUploadData.scores;
				}
			}
		}
		if (this._scoresToUpload == null)
		{
			this._scoresToUpload = new List<LeaderboardScoreUploader.ScoreData>();
		}
		this._scoresToUploadForCurrentPlayer = new List<LeaderboardScoreUploader.ScoreData>(this._scoresToUpload.Count);
		for (int i = this._scoresToUpload.Count - 1; i >= 0; i--)
		{
			LeaderboardScoreUploader.ScoreData scoreData = this._scoresToUpload[i];
			if (scoreData.playerId == this._playerId && this._playerId != null)
			{
				this._scoresToUploadForCurrentPlayer.Add(scoreData);
				this._scoresToUpload.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
	private void SaveScoresToUploadToFile()
	{
		if (this._scoresToUpload == null)
		{
			return;
		}
		this._scoresToUpload.AddRange(this._scoresToUploadForCurrentPlayer);
		string path = Application.persistentDataPath + "/ScoresToUpload.dat";
		if (this._scoresToUpload.Count > 0)
		{
			string contents = JsonUtility.ToJson(new LeaderboardScoreUploader.ScoresToUploadData
			{
				scores = this._scoresToUpload
			});
			File.WriteAllText(path, contents);
			return;
		}
		File.Delete(path);
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0001EF2C File Offset: 0x0001D12C
	public void AddScore(LeaderboardScoreUploader.ScoreData scoreData)
	{
		if (this._uploadScoreCallback == null || this._playerId == null)
		{
			return;
		}
		if (this._uploading)
		{
			this._scoresToUploadForCurrentPlayer.Insert(1, scoreData);
		}
		else
		{
			this._scoresToUploadForCurrentPlayer.Insert(0, scoreData);
		}
		if (this._scoresToUploadForCurrentPlayer.Count == 1)
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.UploadScoresCoroutine());
		}
	}

	// Token: 0x0400039E RID: 926
	private const string kScoresToUploadFileName = "ScoresToUpload.dat";

	// Token: 0x0400039F RID: 927
	private List<LeaderboardScoreUploader.ScoreData> _scoresToUpload = new List<LeaderboardScoreUploader.ScoreData>();

	// Token: 0x040003A0 RID: 928
	private List<LeaderboardScoreUploader.ScoreData> _scoresToUploadForCurrentPlayer = new List<LeaderboardScoreUploader.ScoreData>();

	// Token: 0x040003A1 RID: 929
	private LeaderboardScoreUploader.UploadScoreCallback _uploadScoreCallback;

	// Token: 0x040003A2 RID: 930
	private string _playerId;

	// Token: 0x040003A3 RID: 931
	private bool _uploading;

	// Token: 0x020000D7 RID: 215
	[Serializable]
	public class ScoreData
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00004080 File Offset: 0x00002280
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00004088 File Offset: 0x00002288
		public string playerId { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00004091 File Offset: 0x00002291
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00004099 File Offset: 0x00002299
		public IDifficultyBeatmap beatmap { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000040A2 File Offset: 0x000022A2
		// (set) Token: 0x06000338 RID: 824 RVA: 0x000040AA File Offset: 0x000022AA
		public GameplayModifiers gameplayModifiers { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000040B3 File Offset: 0x000022B3
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000040BB File Offset: 0x000022BB
		public int rawScore { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000040C4 File Offset: 0x000022C4
		// (set) Token: 0x0600033C RID: 828 RVA: 0x000040CC File Offset: 0x000022CC
		public int modifiedScore { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000040D5 File Offset: 0x000022D5
		// (set) Token: 0x0600033E RID: 830 RVA: 0x000040DD File Offset: 0x000022DD
		public bool fullCombo { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000040E6 File Offset: 0x000022E6
		// (set) Token: 0x06000340 RID: 832 RVA: 0x000040EE File Offset: 0x000022EE
		public int goodCutsCount { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000040F7 File Offset: 0x000022F7
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000040FF File Offset: 0x000022FF
		public int badCutsCount { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00004108 File Offset: 0x00002308
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00004110 File Offset: 0x00002310
		public int missedCount { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00004119 File Offset: 0x00002319
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00004121 File Offset: 0x00002321
		public int maxCombo { get; private set; }

		// Token: 0x06000347 RID: 839 RVA: 0x0001EF90 File Offset: 0x0001D190
		public ScoreData(string playerId, IDifficultyBeatmap beatmap, int rawScore, int modifiedScore, bool fullCombo, int goodCutsCount, int badCutsCount, int missedCount, int maxCombo, GameplayModifiers gameplayModifiers)
		{
			this.playerId = playerId;
			this.beatmap = beatmap;
			this.rawScore = rawScore;
			this.modifiedScore = modifiedScore;
			this.gameplayModifiers = gameplayModifiers;
			this.fullCombo = fullCombo;
			this.goodCutsCount = goodCutsCount;
			this.badCutsCount = badCutsCount;
			this.missedCount = missedCount;
			this.maxCombo = maxCombo;
			this.uploadAttemptCount = 0;
			this.currentUploadAttemptCount = 0;
		}

		// Token: 0x040003AE RID: 942
		public int uploadAttemptCount;

		// Token: 0x040003AF RID: 943
		[NonSerialized]
		public int currentUploadAttemptCount;
	}

	// Token: 0x020000D8 RID: 216
	[Serializable]
	private class ScoresToUploadData
	{
		// Token: 0x040003B0 RID: 944
		public List<LeaderboardScoreUploader.ScoreData> scores;
	}

	// Token: 0x020000D9 RID: 217
	// (Invoke) Token: 0x0600034A RID: 842
	public delegate HMAsyncRequest UploadScoreCallback(LeaderboardScoreUploader.ScoreData scoreData, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler);
}
