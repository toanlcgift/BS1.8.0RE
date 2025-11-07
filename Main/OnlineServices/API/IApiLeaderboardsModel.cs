using System;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;

namespace OnlineServices.API
{
	// Token: 0x020004B1 RID: 1201
	public interface IApiLeaderboardsModel
	{
		// Token: 0x060015FE RID: 5630
		Task<ApiResponse<LeaderboardEntriesDTO>> GetLeaderboardEntriesAsync(LeaderboardQueryDTO leaderboardQueryDTO, CancellationToken cancellationToken);

		// Token: 0x060015FF RID: 5631
		Task<Response> SendLevelScoreResultAsync(LevelScoreResultDTO levelScoreResultDto, CancellationToken cancellationToken);

		// Token: 0x06001600 RID: 5632
		void LogoutAsync();
	}
}
