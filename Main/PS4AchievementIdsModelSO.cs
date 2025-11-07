using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class PS4AchievementIdsModelSO : PersistentScriptableObject
{
	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600072E RID: 1838 RVA: 0x000061F8 File Offset: 0x000043F8
	public List<PS4AchievementIdsModelSO.AchievementIdData> achievementsIds
	{
		get
		{
			return this._achievementsIds;
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00027EE8 File Offset: 0x000260E8
	protected override void OnEnable()
	{
		base.OnEnable();
		this._achievementIdToTrophyId.Clear();
		foreach (PS4AchievementIdsModelSO.AchievementIdData achievementIdData in this._achievementsIds)
		{
			this._achievementIdToTrophyId.Add(achievementIdData.achievementId, achievementIdData.ps4TrophyId);
			this._trophyIdToAchievementId.Add(achievementIdData.ps4TrophyId, achievementIdData.achievementId);
		}
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00006200 File Offset: 0x00004400
	public bool GetTrophyId(string achievementId, out int trophyId)
	{
		trophyId = 0;
		return this._achievementIdToTrophyId.TryGetValue(achievementId, out trophyId);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00006212 File Offset: 0x00004412
	public bool GetAchievementId(int trophyId, out string achievementId)
	{
		achievementId = "";
		return this._trophyIdToAchievementId.TryGetValue(trophyId, out achievementId);
	}

	// Token: 0x040007AC RID: 1964
	[SerializeField]
	private List<PS4AchievementIdsModelSO.AchievementIdData> _achievementsIds = new List<PS4AchievementIdsModelSO.AchievementIdData>();

	// Token: 0x040007AD RID: 1965
	private Dictionary<string, int> _achievementIdToTrophyId = new Dictionary<string, int>();

	// Token: 0x040007AE RID: 1966
	private Dictionary<int, string> _trophyIdToAchievementId = new Dictionary<int, string>();

	// Token: 0x020001D7 RID: 471
	[Serializable]
	public class AchievementIdData
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00006251 File Offset: 0x00004451
		public int ps4TrophyId
		{
			get
			{
				return this._trophyId;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00006259 File Offset: 0x00004459
		public string achievementId
		{
			get
			{
				return this._achievement.achievementId;
			}
		}

		// Token: 0x040007AF RID: 1967
		[SerializeField]
		private int _trophyId;

		// Token: 0x040007B0 RID: 1968
		[SerializeField]
		private AchievementSO _achievement;
	}
}
