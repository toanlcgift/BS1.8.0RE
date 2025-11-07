using System;

// Token: 0x020001C1 RID: 449
public interface IPlatformUserHandler
{
	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060006E0 RID: 1760
	PlatformInfo platformInfo { get; }

	// Token: 0x060006E1 RID: 1761
	HMAsyncRequest GetUserInfo(PlatformUserModelSO.GetUserInfoCompletionHandler completionHandler);

	// Token: 0x060006E2 RID: 1762
	HMAsyncRequest GetUserFriendsUserIds(bool cached, PlatformUserModelSO.GetUserFriendsUserIdsCompletionHandler completionHandler);

	// Token: 0x060006E3 RID: 1763
	HMAsyncRequest GetUserAuthToken(PlatformUserModelSO.GetUserAuthTokenCompletionHandler completionHandler);

	// Token: 0x060006E4 RID: 1764
	HMAsyncRequest GetUserNamesForUserIds(string[] userIds, PlatformUserModelSO.GetUserNamesForUserIdsCompletionHandler completionHandler);
}
