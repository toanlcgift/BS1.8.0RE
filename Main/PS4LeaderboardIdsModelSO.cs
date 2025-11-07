using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class PS4LeaderboardIdsModelSO : PersistentScriptableObject
{
	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000746 RID: 1862 RVA: 0x0000628A File Offset: 0x0000448A
	public List<PS4LeaderboardIdsModelSO.LeaderboardIdData> leaderboardIds
	{
		get
		{
			return this._leaderboardIds;
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x000282F8 File Offset: 0x000264F8
	protected override void OnEnable()
	{
		base.OnEnable();
		this._leaderboardIdToPs4Id.Clear();
		foreach (PS4LeaderboardIdsModelSO.LeaderboardIdData leaderboardIdData in this._leaderboardIds)
		{
			this._leaderboardIdToPs4Id.Add(leaderboardIdData.leaderboardId, leaderboardIdData.ps4LeaderboardId);
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00006292 File Offset: 0x00004492
	public bool GetPS4LeaderboardId(string leaderboardId, out uint ps4LeaderboardId)
	{
		ps4LeaderboardId = 0U;
		return this._leaderboardIdToPs4Id.TryGetValue(leaderboardId, out ps4LeaderboardId);
	}

	// Token: 0x040007BC RID: 1980
	[SerializeField]
	private List<PS4LeaderboardIdsModelSO.LeaderboardIdData> _leaderboardIds = new List<PS4LeaderboardIdsModelSO.LeaderboardIdData>();

	// Token: 0x040007BD RID: 1981
	private Dictionary<string, uint> _leaderboardIdToPs4Id = new Dictionary<string, uint>();

	// Token: 0x020001DE RID: 478
	[Serializable]
	public class LeaderboardIdData
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000062C2 File Offset: 0x000044C2
		public uint ps4LeaderboardId
		{
			get
			{
				return this._ps4LeaderboardId;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000062CA File Offset: 0x000044CA
		public string leaderboardId
		{
			get
			{
				return this._leaderboardId;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000062D2 File Offset: 0x000044D2
		public LeaderboardIdData(uint ps4LeaderboardId, string leaderboardId)
		{
			this._ps4LeaderboardId = ps4LeaderboardId;
			this._leaderboardId = leaderboardId;
		}

		// Token: 0x040007BE RID: 1982
		[SerializeField]
		private uint _ps4LeaderboardId;

		// Token: 0x040007BF RID: 1983
		[SerializeField]
		private string _leaderboardId;
	}
}
