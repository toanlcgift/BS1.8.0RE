using System;
using System.Collections.Generic;
using Steamworks;

// Token: 0x020001D5 RID: 469
public class SteamPlatformAchievementHandler : PlatformAchievementsHandler
{
	// Token: 0x0600072B RID: 1835 RVA: 0x000061BF File Offset: 0x000043BF
	public SteamPlatformAchievementHandler(AchievementIdsModelSO achievementIdsModel)
	{
		this._achievementIdsModel = achievementIdsModel;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x000061CE File Offset: 0x000043CE
	public override HMAsyncRequest UnlockAchievement(string achievementId, PlatformAchievementsModelSO.UnlockAchievementCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			return null;
		}
		SteamUserStats.SetAchievement(achievementId);
		if (SteamUserStats.StoreStats())
		{
			completionHandler(PlatformAchievementsModelSO.UnlockAchievementResult.OK);
		}
		else
		{
			completionHandler(PlatformAchievementsModelSO.UnlockAchievementResult.Failed);
		}
		return null;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00027E4C File Offset: 0x0002604C
	public override HMAsyncRequest GetUnlockedAchievements(PlatformAchievementsModelSO.GetUnlockedAchievementsCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			return null;
		}
		List<string> list = new List<string>();
		foreach (AchievementSO achievementSO in this._achievementIdsModel.achievementsIds)
		{
			bool flag;
			SteamUserStats.GetAchievement(achievementSO.achievementId, out flag);
			if (SteamUserStats.GetAchievement(achievementSO.achievementId, out flag) && flag)
			{
				list.Add(achievementSO.achievementId);
			}
		}
		completionHandler(PlatformAchievementsModelSO.GetUnlockedAchievementsResult.OK, list.ToArray());
		return null;
	}

	// Token: 0x040007AA RID: 1962
	private readonly AchievementIdsModelSO _achievementIdsModel;

	// Token: 0x040007AB RID: 1963
	protected Callback<UserAchievementStored_t> _userAchievementStored;
}
