using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;
using OnlineServices.API;

// Token: 0x020001A9 RID: 425
public class UserLoginDtoDataSource : IUserLoginDtoDataSource
{
	// Token: 0x060006AA RID: 1706 RVA: 0x00005DA3 File Offset: 0x00003FA3
	public UserLoginDtoDataSource(PlatformUserModelSO platformUserModel)
	{
		this._platformUserModel = platformUserModel;
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00026080 File Offset: 0x00024280
	public async Task<string> GetPlatformUserIdAsync(CancellationToken cancellationToken)
	{
		PlatformUserModelSO.UserInfo userInfo = await this.GetUserInfo(cancellationToken);
		string result;
		if (userInfo == null)
		{
			result = null;
		}
		else
		{
			result = userInfo.userId;
		}
		return result;
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x000260D0 File Offset: 0x000242D0
	public async Task<LoginRequestDTO> GetLoginRequestDTOAsync(CancellationToken cancellationToken)
	{
		PlatformUserModelSO.UserInfo userInfo2 = await this.GetUserInfo(cancellationToken);
		PlatformUserModelSO.UserInfo userInfo = userInfo2;
		LoginRequestDTO result;
		if (userInfo == null)
		{
			result = null;
		}
		else
		{
			TaskAwaiter<string[]> taskAwaiter = this.GetUserFriendsUserIds(cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				taskAwaiter.GetResult();
				TaskAwaiter<string[]> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<string[]>);
			}
			if (taskAwaiter.GetResult() == null)
			{
				result = null;
			}
			else
			{
				PlatformTokenInfo platformTokenInfo = await this.GetUserAuthToken(cancellationToken);
				if (platformTokenInfo == null)
				{
					result = null;
				}
				else
				{
					result = new LoginRequestDTO
					{
						buildVersion = "0.0.1",
						platform = this._platformUserModel.platformInfo.serialzedName,
						platformUserId = userInfo.userId,
						publicUserDisplayName = userInfo.userName,
						platformAuthToken = platformTokenInfo.platformToken,
						platformEnviroment = platformTokenInfo.platformEnvironmentInfo.serializedName
					};
				}
			}
		}
		return result;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00026120 File Offset: 0x00024320
	private async Task<PlatformUserModelSO.UserInfo> GetUserInfo(CancellationToken cancellationToken)
	{
		TaskCompletionSource<PlatformUserModelSO.UserInfo> tcs = new TaskCompletionSource<PlatformUserModelSO.UserInfo>();
		this._platformUserModel.GetUserInfo(delegate(PlatformUserModelSO.GetUserInfoResult result, PlatformUserModelSO.UserInfo userInfo)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				tcs.TrySetCanceled();
				return;
			}
			if (result == PlatformUserModelSO.GetUserInfoResult.Failed)
			{
				tcs.TrySetResult(null);
				return;
			}
			tcs.TrySetResult(userInfo);
		});
		return await tcs.Task;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00026170 File Offset: 0x00024370
	public async Task<string[]> GetUserFriendsUserIds(CancellationToken cancellationToken)
	{
		TaskCompletionSource<string[]> tcs = new TaskCompletionSource<string[]>();
		this._platformUserModel.GetUserFriendsUserIds(true, delegate(PlatformUserModelSO.GetUserFriendsUserIdsResult result, string[] friendsUserIds)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				tcs.TrySetCanceled();
				return;
			}
			if (result == PlatformUserModelSO.GetUserFriendsUserIdsResult.Failed)
			{
				tcs.TrySetResult(null);
				return;
			}
			tcs.TrySetResult(friendsUserIds);
		});
		return await tcs.Task;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x000261C0 File Offset: 0x000243C0
	private async Task<PlatformTokenInfo> GetUserAuthToken(CancellationToken cancellationToken)
	{
		TaskCompletionSource<PlatformTokenInfo> tcs = new TaskCompletionSource<PlatformTokenInfo>();
		this._platformUserModel.GetUserAuthToken(delegate(PlatformUserModelSO.GetUserAuthTokenResult result, PlatformTokenInfo platformTokenInfo)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				tcs.TrySetCanceled();
				return;
			}
			if (result == PlatformUserModelSO.GetUserAuthTokenResult.Failed)
			{
				tcs.TrySetResult(null);
				return;
			}
			tcs.TrySetResult(platformTokenInfo);
		});
		return await tcs.Task;
	}

	// Token: 0x04000710 RID: 1808
	private const string kVersion = "0.0.1";

	// Token: 0x04000711 RID: 1809
	private PlatformUserModelSO _platformUserModel;
}
