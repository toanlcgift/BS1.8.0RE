using System;

// Token: 0x020001B7 RID: 439
public class PlatformUserModelSO : PersistentScriptableObject
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00005F1F File Offset: 0x0000411F
	public PlatformInfo platformInfo
	{
		get
		{
			return this._platformUserHandler.platformInfo;
		}
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00005F2C File Offset: 0x0000412C
	protected override void OnEnable()
	{
		base.OnEnable();
		this._platformUserHandler = new SteamPlatformUserHandler();
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00005F3F File Offset: 0x0000413F
	public HMAsyncRequest GetUserInfo(PlatformUserModelSO.GetUserInfoCompletionHandler completionHandler)
	{
		return this._platformUserHandler.GetUserInfo(completionHandler);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00005F4D File Offset: 0x0000414D
	public HMAsyncRequest GetUserFriendsUserIds(bool cached, PlatformUserModelSO.GetUserFriendsUserIdsCompletionHandler completionHandler)
	{
		return this._platformUserHandler.GetUserFriendsUserIds(cached, completionHandler);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00005F5C File Offset: 0x0000415C
	public HMAsyncRequest GetUserAuthToken(PlatformUserModelSO.GetUserAuthTokenCompletionHandler completionHandler)
	{
		return this._platformUserHandler.GetUserAuthToken(completionHandler);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00005F6A File Offset: 0x0000416A
	public HMAsyncRequest GetUserNamesForUserIds(string[] userIds, PlatformUserModelSO.GetUserNamesForUserIdsCompletionHandler completionHandler)
	{
		return this._platformUserHandler.GetUserNamesForUserIds(userIds, completionHandler);
	}

	// Token: 0x04000748 RID: 1864
	private IPlatformUserHandler _platformUserHandler;

	// Token: 0x020001B8 RID: 440
	public enum GetUserInfoResult
	{
		// Token: 0x0400074A RID: 1866
		OK,
		// Token: 0x0400074B RID: 1867
		Failed
	}

	// Token: 0x020001B9 RID: 441
	public enum GetUserFriendsUserIdsResult
	{
		// Token: 0x0400074D RID: 1869
		OK,
		// Token: 0x0400074E RID: 1870
		Failed
	}

	// Token: 0x020001BA RID: 442
	public enum GetUserAuthTokenResult
	{
		// Token: 0x04000750 RID: 1872
		OK,
		// Token: 0x04000751 RID: 1873
		Failed
	}

	// Token: 0x020001BB RID: 443
	public enum GetUserNamesForUserIdsResult
	{
		// Token: 0x04000753 RID: 1875
		OK,
		// Token: 0x04000754 RID: 1876
		Failed
	}

	// Token: 0x020001BC RID: 444
	// (Invoke) Token: 0x060006D0 RID: 1744
	public delegate void GetUserInfoCompletionHandler(PlatformUserModelSO.GetUserInfoResult result, PlatformUserModelSO.UserInfo userInfo);

	// Token: 0x020001BD RID: 445
	// (Invoke) Token: 0x060006D4 RID: 1748
	public delegate void GetUserFriendsUserIdsCompletionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult result, string[] friendsUserIds);

	// Token: 0x020001BE RID: 446
	// (Invoke) Token: 0x060006D8 RID: 1752
	public delegate void GetUserAuthTokenCompletionHandler(PlatformUserModelSO.GetUserAuthTokenResult result, PlatformTokenInfo platformTokenInfo);

	// Token: 0x020001BF RID: 447
	// (Invoke) Token: 0x060006DC RID: 1756
	public delegate void GetUserNamesForUserIdsCompletionHandler(PlatformUserModelSO.GetUserNamesForUserIdsResult result, string[] userNames);

	// Token: 0x020001C0 RID: 448
	public class UserInfo
	{
		// Token: 0x04000755 RID: 1877
		public string userId;

		// Token: 0x04000756 RID: 1878
		public string userName;
	}
}
