using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class SteamPlatformUserHandler : IPlatformUserHandler
{
	// Token: 0x1700025B RID: 603
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00006FC4 File Offset: 0x000051C4
	public PlatformInfo platformInfo
	{
		get
		{
			return PlatformInfo.steamPlatformInfo;
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0002AD34 File Offset: 0x00028F34
	public HMAsyncRequest GetUserInfo(PlatformUserModelSO.GetUserInfoCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserInfoResult.Failed, null);
			}
			return null;
		}
		if (this._userInfo != null)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserInfoResult.OK, this._userInfo);
			}
			return null;
		}
		this._userInfo = new PlatformUserModelSO.UserInfo();
		this._userInfo.userId = SteamUser.GetSteamID().m_SteamID.ToString();
		this._userInfo.userName = SteamFriends.GetPersonaName();
		if (completionHandler != null)
		{
			completionHandler(PlatformUserModelSO.GetUserInfoResult.OK, this._userInfo);
		}
		return null;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0002ADB8 File Offset: 0x00028FB8
	public HMAsyncRequest GetUserFriendsUserIds(bool cached, PlatformUserModelSO.GetUserFriendsUserIdsCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.Failed, null);
			}
			return null;
		}
		if (this._friendsUserIds != null && cached)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.OK, this._friendsUserIds);
			}
			return null;
		}
		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
		List<string> list = new List<string>(friendCount);
		for (int i = 0; i < friendCount; i++)
		{
			list.Add(SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate).m_SteamID.ToString());
		}
		this._friendsUserIds = list.ToArray();
		if (completionHandler != null)
		{
			completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.OK, this._friendsUserIds);
		}
		return null;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0002AE50 File Offset: 0x00029050
	public HMAsyncRequest GetUserAuthToken(PlatformUserModelSO.GetUserAuthTokenCompletionHandler completionHandler)
	{
		if (!SteamManager.Initialized)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserAuthTokenResult.Failed, null);
			}
			return null;
		}
		string text = null;
		byte[] array = new byte[1024];
		uint length = 0U;
		if (SteamUser.GetAuthSessionTicket(array, array.Length, out length) != HAuthTicket.Invalid)
		{
			text = BitConverter.ToString(array, 0, (int)length);
		}
		if (completionHandler != null)
		{
			completionHandler((text != null) ? PlatformUserModelSO.GetUserAuthTokenResult.OK : PlatformUserModelSO.GetUserAuthTokenResult.Failed, new PlatformTokenInfo(text, PlatformTokenInfo.PlatformEnvironmentInfo.LivePlatformEnvironmentInfo()));
		}
		return null;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00006FCB File Offset: 0x000051CB
	public HMAsyncRequest GetUserNamesForUserIds(string[] userIds, PlatformUserModelSO.GetUserNamesForUserIdsCompletionHandler completionHandler)
	{
		Debug.Log("GetUserNamesForUserIds not implemented for Steam platform.");
		if (completionHandler != null)
		{
			completionHandler(PlatformUserModelSO.GetUserNamesForUserIdsResult.Failed, null);
		}
		return null;
	}

	// Token: 0x04000917 RID: 2327
	private string[] _friendsUserIds;

	// Token: 0x04000918 RID: 2328
	private PlatformUserModelSO.UserInfo _userInfo;
}
