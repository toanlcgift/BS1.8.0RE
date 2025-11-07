using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;
using UnityEngine;

namespace OnlineServices.API
{
	// Token: 0x020004B9 RID: 1209
	public class HTTPApiLeaderboardsModel : IApiLeaderboardsModel
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x000512B8 File Offset: 0x0004F4B8
		public HTTPApiLeaderboardsModel(string hostName, IUserLoginDtoDataSource userLoginDtoDataSource)
		{
			UriBuilder uriBuilder = new UriBuilder("https", "localhost", 5001);
			this._httpLeaderboardsOathHelper = new HTTPLeaderboardsOathHelper(userLoginDtoDataSource, uriBuilder);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000512F0 File Offset: 0x0004F4F0
		public async void LogoutAsync()
		{
			await this._httpLeaderboardsOathHelper.LogOut();
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0005132C File Offset: 0x0004F52C
		public async Task<ApiResponse<LeaderboardEntriesDTO>> GetLeaderboardEntriesAsync(LeaderboardQueryDTO leaderboardQueryDTO, CancellationToken cancellationToken)
		{
			ApiResponse<LeaderboardEntriesDTO> result;
			try
			{
				string path = "/api/v1/Leaderboard/Filter";
				LeaderboardEntriesDTO responseDto = JsonUtility.FromJson<LeaderboardEntriesDTO>(await this._httpLeaderboardsOathHelper.SendWebRequestWithOathAsync(path, "POST", leaderboardQueryDTO, cancellationToken));
				result = new ApiResponse<LeaderboardEntriesDTO>(Response.Success, responseDto);
			}
			catch (NullReferenceException)
			{
				result = new ApiResponse<LeaderboardEntriesDTO>(Response.UnknownError, default(LeaderboardEntriesDTO));
			}
			return result;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00051384 File Offset: 0x0004F584
		public async Task<Response> SendLevelScoreResultAsync(LevelScoreResultDTO levelScoreResultDto, CancellationToken cancellationToken)
		{
			Response result;
			try
			{
				TaskAwaiter<string> taskAwaiter = this._httpLeaderboardsOathHelper.SendWebRequestWithOathAsync("/api/v1/Leaderboard/Add", "POST", levelScoreResultDto, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					taskAwaiter.GetResult();
					TaskAwaiter<string> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<string>);
				}
				result = ((JsonUtility.FromJson<LeaderboardEntryDTO>(taskAwaiter.GetResult()) != null) ? Response.Success : Response.UnknownError);
			}
			catch (NullReferenceException)
			{
				result = Response.UnknownError;
			}
			return result;
		}

		// Token: 0x04001674 RID: 5748
		private const string kSendLevelScoreResultPath = "/api/v1/Leaderboard/Add";

		// Token: 0x04001675 RID: 5749
		private const string kGetLeaderboardEntriesPath = "/api/v1/Leaderboard/Filter";

		// Token: 0x04001676 RID: 5750
		private HTTPLeaderboardsOathHelper _httpLeaderboardsOathHelper;
	}
}
