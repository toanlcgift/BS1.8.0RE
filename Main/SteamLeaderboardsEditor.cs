using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002A RID: 42
public class SteamLeaderboardsEditor : MonoBehaviour
{
	// Token: 0x060000B2 RID: 178 RVA: 0x000028DC File Offset: 0x00000ADC
	protected void Awake()
	{
		if (SteamManager.Initialized)
		{
			Debug.Log("Steamworks initialized");
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000163C8 File Offset: 0x000145C8
	private void CreateLeaderboard(string leaderboardId, Action<bool> completionHandler)
	{
		this._steamRequest = new SteamAsyncRequest<LeaderboardFindResult_t>();
		SteamAPICall_t apiCall = SteamUserStats.FindOrCreateLeaderboard(leaderboardId, ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric);
		this._steamRequest.MakeRequest(apiCall, delegate(LeaderboardFindResult_t result, bool ioFailure)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[",
				1106,
				" - FindOrCreateLeaderboard] - ",
				result.m_bLeaderboardFound,
				" -- ",
				result.m_hSteamLeaderboard,
				" -- ",
				ioFailure.ToString()
			}));
			this._steamRequest = null;
			if (completionHandler != null)
			{
				bool obj = !ioFailure && result.m_bLeaderboardFound != 0 && result.m_hSteamLeaderboard.m_SteamLeaderboard > 0UL;
				completionHandler(obj);
			}
		});
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00016418 File Offset: 0x00014618
	private void CreateNextLeaderboard()
	{
		if (this._leaderboardIds.Count == 0)
		{
			Debug.Log("Finished.");
			return;
		}
		string leaderboardId = this._leaderboardIds[this._leaderboardIds.Count - 1];
		Debug.Log("Creating " + leaderboardId);
		this.CreateLeaderboard(leaderboardId, delegate(bool ok)
		{
			if (ok)
			{
				this._leaderboardIds.RemoveAt(this._leaderboardIds.Count - 1);
				this.CreateNextLeaderboard();
				return;
			}
			Debug.LogError("Error creating leaderboard: " + leaderboardId + ". Aborted.");
		});
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00016498 File Offset: 0x00014698
	public void CreateLeaderboardsButtonPressed()
	{
		if (this._steamRequest != null)
		{
			Debug.Log("Cancelled.");
			this._createLeaderboardsButtonText.text = "Create leaderboards";
			this._steamRequest.Cancel();
			this._steamRequest = null;
			return;
		}
		Debug.Log("Creating leaderboards...");
		this._createLeaderboardsButtonText.text = "Cancel";
		if (this._levelCollection != null)
		{
			IPreviewBeatmapLevel[] beatmapLevels = this._levelCollection.beatmapLevels;
			for (int i = 0; i < beatmapLevels.Length; i++)
			{
				IDifficultyBeatmapSet[] difficultyBeatmapSets = ((IBeatmapLevel)beatmapLevels[i]).beatmapLevelData.difficultyBeatmapSets;
				for (int j = 0; j < difficultyBeatmapSets.Length; j++)
				{
					IDifficultyBeatmap[] difficultyBeatmaps = difficultyBeatmapSets[j].difficultyBeatmaps;
					for (int k = 0; k < difficultyBeatmaps.Length; k++)
					{
						string leaderboardID = LeaderboardsModel.GetLeaderboardID(difficultyBeatmaps[k]);
						this._leaderboardIds.Add(leaderboardID);
					}
				}
			}
		}
		List<string> list = new List<string>(this._leaderboardIds.Count);
		for (int l = 0; l < this._leaderboardIds.Count; l++)
		{
			list.Add(this._leaderboardIds[this._leaderboardIds.Count - l - 1]);
		}
		this._leaderboardIds = list;
		this.CreateNextLeaderboard();
	}

	// Token: 0x0400009A RID: 154
	[SerializeField]
	private BeatmapLevelCollectionSO _levelCollection;

	// Token: 0x0400009B RID: 155
	[SerializeField]
	private Text _createLeaderboardsButtonText;

	// Token: 0x0400009C RID: 156
	private SteamAsyncRequest<LeaderboardFindResult_t> _steamRequest;

	// Token: 0x0400009D RID: 157
	private List<string> _leaderboardIds = new List<string>(1000);
}
