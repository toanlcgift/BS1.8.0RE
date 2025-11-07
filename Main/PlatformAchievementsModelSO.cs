using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class PlatformAchievementsModelSO : PersistentScriptableObject
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000359A File Offset: 0x0000179A
	private PlatformAchievementsHandler platformAchievementsHandler
	{
		get
		{
			if (this._platformAchievementsHandler != null)
			{
				return this._platformAchievementsHandler;
			}
			this.CreatePlatformAchievementsHandler();
			return this._platformAchievementsHandler;
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000035B7 File Offset: 0x000017B7
	public void Initialize()
	{
		this.CreatePlatformAchievementsHandler();
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x000035BF File Offset: 0x000017BF
	private void CreatePlatformAchievementsHandler()
	{
		if (this._platformAchievementsHandler != null)
		{
			return;
		}
		this._platformAchievementsHandler = new SteamPlatformAchievementHandler(this._achievementIdsModel);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000035DB File Offset: 0x000017DB
	public HMAsyncRequest UnlockAchievement(string achievementId, PlatformAchievementsModelSO.UnlockAchievementCompletionHandler completionHandler)
	{
		if (this.platformAchievementsHandler != null)
		{
			return this._platformAchievementsHandler.UnlockAchievement(achievementId, completionHandler);
		}
		if (completionHandler != null)
		{
			completionHandler(PlatformAchievementsModelSO.UnlockAchievementResult.Failed);
		}
		return null;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000035FE File Offset: 0x000017FE
	public HMAsyncRequest GetUnlockedAchievements(PlatformAchievementsModelSO.GetUnlockedAchievementsCompletionHandler completionHandler)
	{
		if (this.platformAchievementsHandler != null)
		{
			return this._platformAchievementsHandler.GetUnlockedAchievements(completionHandler);
		}
		if (completionHandler != null)
		{
			completionHandler(PlatformAchievementsModelSO.GetUnlockedAchievementsResult.Failed, null);
		}
		return null;
	}

	// Token: 0x040001B4 RID: 436
	[SerializeField]
	private PS4AchievementIdsModelSO _ps4AchievementIdsModel;

	// Token: 0x040001B5 RID: 437
	[SerializeField]
	private AchievementIdsModelSO _achievementIdsModel;

	// Token: 0x040001B6 RID: 438
	private PlatformAchievementsHandler _platformAchievementsHandler;

	// Token: 0x02000064 RID: 100
	public enum UnlockAchievementResult
	{
		// Token: 0x040001B8 RID: 440
		OK,
		// Token: 0x040001B9 RID: 441
		Failed
	}

	// Token: 0x02000065 RID: 101
	public enum GetUnlockedAchievementsResult
	{
		// Token: 0x040001BB RID: 443
		OK,
		// Token: 0x040001BC RID: 444
		Failed
	}

	// Token: 0x02000066 RID: 102
	// (Invoke) Token: 0x060001BA RID: 442
	public delegate void UnlockAchievementCompletionHandler(PlatformAchievementsModelSO.UnlockAchievementResult result);

	// Token: 0x02000067 RID: 103
	// (Invoke) Token: 0x060001BE RID: 446
	public delegate void GetUnlockedAchievementsCompletionHandler(PlatformAchievementsModelSO.GetUnlockedAchievementsResult result, string[] unlockedAchievementsIds);
}
