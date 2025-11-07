using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineServices
{
	// Token: 0x0200048B RID: 1163
	public interface ILeaderboardsModel
	{
		// Token: 0x060015A1 RID: 5537
		string GetLeaderboardId(IDifficultyBeatmap difficultyBeatmap);

		// Token: 0x060015A2 RID: 5538
		Task<GetLeaderboardEntriesResult> GetLeaderboardEntriesAsync(GetLeaderboardFilterData leaderboardFilterData, CancellationToken cancellationToken);

		// Token: 0x060015A3 RID: 5539
		Task<SendLeaderboardEntryResult> SendLevelScoreResultAsync(LevelScoreResultsData levelScoreResultsData, CancellationToken cancellationToken);
	}
}
