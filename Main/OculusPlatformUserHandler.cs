using System;
using System.Collections.Generic;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class OculusPlatformUserHandler : IPlatformUserHandler
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000673 RID: 1651 RVA: 0x00005C9D File Offset: 0x00003E9D
	public PlatformInfo platformInfo
	{
		get
		{
			return PlatformInfo.oculusPlatformInfo;
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00024C10 File Offset: 0x00022E10
	public HMAsyncRequest GetUserInfo(PlatformUserModelSO.GetUserInfoCompletionHandler completionHandler)
	{
		if (this._userInfo != null)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserInfoResult.OK, this._userInfo);
			}
			return null;
		}
		HMAsyncRequest asyncRequest = new HMAsyncRequest();
		Users.GetLoggedInUser().OnComplete(delegate(Message<User> message)
		{
			if (asyncRequest.cancelled)
			{
				return;
			}
			if (message.IsError)
			{
				if (completionHandler != null)
				{
					completionHandler(PlatformUserModelSO.GetUserInfoResult.Failed, null);
					return;
				}
			}
			else
			{
				this._userInfo = new PlatformUserModelSO.UserInfo();
				this._userInfo.userId = message.Data.ID.ToString();
				this._userInfo.userName = message.Data.OculusID;
				if (completionHandler != null)
				{
					completionHandler(PlatformUserModelSO.GetUserInfoResult.OK, this._userInfo);
				}
			}
		});
		return asyncRequest;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00024C80 File Offset: 0x00022E80
	public HMAsyncRequest GetUserFriendsUserIds(bool cached, PlatformUserModelSO.GetUserFriendsUserIdsCompletionHandler completionHandler)
	{
		if (this._friendsUserIds != null && cached)
		{
			if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.OK, this._friendsUserIds);
			}
			return null;
		}
		HMAsyncRequest asyncRequest = new HMAsyncRequest();
		Users.GetLoggedInUserFriends().OnComplete(delegate(Message<UserList> message)
		{
			if (asyncRequest.cancelled)
			{
				return;
			}
			if (message.IsError)
			{
				if (completionHandler != null)
				{
					completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.Failed, null);
					return;
				}
			}
			else
			{
				List<string> list = new List<string>(message.Data.Count);
				foreach (User user in message.Data)
				{
					list.Add(user.ID.ToString());
				}
				this._friendsUserIds = list.ToArray();
				if (completionHandler != null)
				{
					completionHandler(PlatformUserModelSO.GetUserFriendsUserIdsResult.OK, this._friendsUserIds);
				}
			}
		});
		return asyncRequest;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00024CF4 File Offset: 0x00022EF4
	public HMAsyncRequest GetUserAuthToken(PlatformUserModelSO.GetUserAuthTokenCompletionHandler completionHandler)
	{
		HMAsyncRequest asyncRequest = new HMAsyncRequest();
		Users.GetUserProof().OnComplete(delegate(Message<UserProof> message)
		{
			if (asyncRequest.cancelled)
			{
				return;
			}
			if (message.IsError)
			{
				if (completionHandler != null)
				{
					completionHandler(PlatformUserModelSO.GetUserAuthTokenResult.Failed, null);
					return;
				}
			}
			else if (completionHandler != null)
			{
				completionHandler(PlatformUserModelSO.GetUserAuthTokenResult.OK, new PlatformTokenInfo(message.Data.Value, PlatformTokenInfo.PlatformEnvironmentInfo.LivePlatformEnvironmentInfo()));
			}
		});
		return asyncRequest;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00005CA4 File Offset: 0x00003EA4
	public HMAsyncRequest GetUserNamesForUserIds(string[] userIds, PlatformUserModelSO.GetUserNamesForUserIdsCompletionHandler completionHandler)
	{
		Debug.Log("GetUserNamesForUserIds not implemented for Oculus platform.");
		if (completionHandler != null)
		{
			completionHandler(PlatformUserModelSO.GetUserNamesForUserIdsResult.Failed, null);
		}
		return null;
	}

	// Token: 0x040006CF RID: 1743
	private string[] _friendsUserIds;

	// Token: 0x040006D0 RID: 1744
	private PlatformUserModelSO.UserInfo _userInfo;
}
