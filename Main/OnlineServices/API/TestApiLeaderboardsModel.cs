using System;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;
using UnityEngine;

namespace OnlineServices.API
{
	// Token: 0x020004B5 RID: 1205
	public class TestApiLeaderboardsModel : IApiLeaderboardsModel
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x00050F44 File Offset: 0x0004F144
		public async void LogoutAsync()
		{
			await Task.Delay(10);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00050F78 File Offset: 0x0004F178
		public async Task<ApiResponse<LeaderboardEntriesDTO>> GetLeaderboardEntriesAsync(LeaderboardQueryDTO leaderboardQueryDTO, CancellationToken cancellationToken)
		{
			LeaderboardEntryDTO[] array = new LeaderboardEntryDTO[10];
			int num = UnityEngine.Random.Range(0, 10);
			for (int i = 0; i < array.Length; i++)
			{
				string userDisplayName;
				if (num == i)
				{
					userDisplayName = "YOU ";
				}
				else
				{
					userDisplayName = " P " + UnityEngine.Random.Range(100000, 999999);
				}
				array[i] = new LeaderboardEntryDTO
				{
					score = 10000 / (i + 1) + UnityEngine.Random.Range(0, 100),
					rank = i,
					userDisplayName = userDisplayName
				};
			}
			LeaderboardEntriesDTO responseDto = new LeaderboardEntriesDTO
			{
				entries = array
			};
			return await Task.FromResult<ApiResponse<LeaderboardEntriesDTO>>(new ApiResponse<LeaderboardEntriesDTO>(Response.Success, responseDto));
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		public async Task<Response> SendLevelScoreResultAsync(LevelScoreResultDTO levelScoreResultDto, CancellationToken cancellationToken)
		{
			return await Task.FromResult<Response>(Response.UnknownError);
		}
	}
}
