using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class AchievementsModelSO : PersistentScriptableObject
{
	// Token: 0x060001A6 RID: 422 RVA: 0x000034F1 File Offset: 0x000016F1
	public void Initialize()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._platformAchievementsModel.Initialize();
		this._platformAchievementsModel.GetUnlockedAchievements(delegate(PlatformAchievementsModelSO.GetUnlockedAchievementsResult result, string[] achievementIds)
		{
			if (result == PlatformAchievementsModelSO.GetUnlockedAchievementsResult.OK)
			{
				foreach (string item in achievementIds)
				{
					this._unlockedAchievementIds.Add(item);
				}
			}
		});
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00018EC8 File Offset: 0x000170C8
	public void UnlockAchievement(AchievementSO achievement)
	{
		if (!this._initialized)
		{
			this.Initialize();
		}
		string achievementId = achievement.achievementId;
		if (this._unlockedAchievementIds.Contains(achievementId))
		{
			return;
		}
		this._platformAchievementsModel.UnlockAchievement(achievementId, delegate(PlatformAchievementsModelSO.UnlockAchievementResult result)
		{
			if (result == PlatformAchievementsModelSO.UnlockAchievementResult.OK)
			{
				this._unlockedAchievementIds.Add(achievementId);
			}
		});
	}

	// Token: 0x040001AC RID: 428
	[SerializeField]
	private PlatformAchievementsModelSO _platformAchievementsModel;

	// Token: 0x040001AD RID: 429
	private HashSet<string> _unlockedAchievementIds = new HashSet<string>();

	// Token: 0x040001AE RID: 430
	private bool _initialized;
}
