using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineServices
{
	// Token: 0x02000499 RID: 1177
	public class PlatformUserNamesLoader
	{
		// Token: 0x060015BF RID: 5567 RVA: 0x0001043A File Offset: 0x0000E63A
		public PlatformUserNamesLoader(PlatformUserModelSO platformUserModel)
		{
			this._platformUserModel = platformUserModel;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0004F6B8 File Offset: 0x0004D8B8
		public async Task<string[]> GetUserNamesForUserIds(string[] userPlatfromIds, CancellationToken cancellationToken)
		{
			TaskCompletionSource<string[]> tcs = new TaskCompletionSource<string[]>();
			this._platformUserModel.GetUserNamesForUserIds(userPlatfromIds, delegate(PlatformUserModelSO.GetUserNamesForUserIdsResult result, string[] names)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					tcs.TrySetCanceled();
					return;
				}
				if (result == PlatformUserModelSO.GetUserNamesForUserIdsResult.Failed)
				{
					tcs.TrySetResult(null);
					return;
				}
				tcs.TrySetResult(names);
			});
			return await tcs.Task;
		}

		// Token: 0x040015F5 RID: 5621
		private PlatformUserModelSO _platformUserModel;
	}
}
