using System;

// Token: 0x02000062 RID: 98
public abstract class PlatformAchievementsHandler
{
	// Token: 0x060001B0 RID: 432
	public abstract HMAsyncRequest UnlockAchievement(string achievementId, PlatformAchievementsModelSO.UnlockAchievementCompletionHandler completionHandler);

	// Token: 0x060001B1 RID: 433
	public abstract HMAsyncRequest GetUnlockedAchievements(PlatformAchievementsModelSO.GetUnlockedAchievementsCompletionHandler completionHandler);
}
