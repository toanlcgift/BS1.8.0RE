using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;
using OnlineServices;
using OnlineServices.API;
using UnityEngine.XR;

// Token: 0x0200019D RID: 413
public class HTTPLeaderboardsModel : ILeaderboardsModel
{
	// Token: 0x14000011 RID: 17
	// (add) Token: 0x06000681 RID: 1665 RVA: 0x00025124 File Offset: 0x00023324
	// (remove) Token: 0x06000682 RID: 1666 RVA: 0x0002515C File Offset: 0x0002335C
	public event Action<string> scoreForLeaderboardDidUploadEvent;

	// Token: 0x06000683 RID: 1667 RVA: 0x00025194 File Offset: 0x00023394
	public HTTPLeaderboardsModel(string hostName, PlatformUserModelSO platformUserModel, PlatformUserNamesLoader platformUserNamesLoader)
	{
		this._userLoginDataSource = new UserLoginDtoDataSource(platformUserModel);
		this._platformUserNamesLoader = platformUserNamesLoader;
		this._apiLeaderboardsModel = new HTTPApiLeaderboardsModel(hostName, this._userLoginDataSource);
		this._guid = Guid.NewGuid().ToString();
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00005CBC File Offset: 0x00003EBC
	public void LogoutAsync()
	{
		this._apiLeaderboardsModel.LogoutAsync();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x000251E8 File Offset: 0x000233E8
	public string GetLeaderboardId(IDifficultyBeatmap difficultyBeatmap)
	{
		string str = "Unknown";
		switch (difficultyBeatmap.difficulty)
		{
		case BeatmapDifficulty.Easy:
			str = "Easy";
			break;
		case BeatmapDifficulty.Normal:
			str = "Normal";
			break;
		case BeatmapDifficulty.Hard:
			str = "Hard";
			break;
		case BeatmapDifficulty.Expert:
			str = "Expert";
			break;
		case BeatmapDifficulty.ExpertPlus:
			str = "ExpertPlus";
			break;
		}
		return difficultyBeatmap.level.levelID + difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.compoundIdPartName + str;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00025268 File Offset: 0x00023468
	public async Task<GetLeaderboardEntriesResult> GetLeaderboardEntriesAsync(GetLeaderboardFilterData leaderboardFilterData, CancellationToken cancellationToken)
	{
		string leaderboardId = this.GetLeaderboardId(leaderboardFilterData.beatmap);
		if (this._friendsUserIds == null)
		{
			string[] friendsUserIds = await this._userLoginDataSource.GetUserFriendsUserIds(cancellationToken);
			this._friendsUserIds = friendsUserIds;
		}
		if (this._platformUserId == null)
		{
			this._platformUserId = await this._userLoginDataSource.GetPlatformUserIdAsync(cancellationToken);
		}
		ScoresScope scope = leaderboardFilterData.scope;
		LeaderboardQueryDTO.ScoresScope scope2;
		if (scope != ScoresScope.Global)
		{
			if (scope == ScoresScope.Friends)
			{
				scope2 = LeaderboardQueryDTO.ScoresScope.Friends;
			}
			else
			{
				scope2 = LeaderboardQueryDTO.ScoresScope.Global;
			}
		}
		else
		{
			scope2 = LeaderboardQueryDTO.ScoresScope.Global;
		}
		LeaderboardQueryDTO leaderboardQueryDTO = new LeaderboardQueryDTO
		{
			leaderboardId = leaderboardId,
			count = leaderboardFilterData.count,
			fromRank = leaderboardFilterData.fromRank,
			includedScoreWithModifiers = leaderboardFilterData.includeScoreWithModifiers,
			scope = scope2,
			friendsUserIds = this._friendsUserIds
		};
		ApiResponse<LeaderboardEntriesDTO> apiResponse = await this._apiLeaderboardsModel.GetLeaderboardEntriesAsync(leaderboardQueryDTO, cancellationToken);
		GetLeaderboardEntriesResult result;
		if (apiResponse.isError)
		{
			result = new GetLeaderboardEntriesResult(true, null, -1);
		}
		else
		{
			string[] array = (from x in apiResponse.responseDto.entries
			select x.userDisplayName).ToArray<string>();
			List<LeaderboardEntryData> list = new List<LeaderboardEntryData>();
			LeaderboardEntryDTO[] entries = apiResponse.responseDto.entries;
			int referencePlayerScoreIndex = -1;
			if (entries != null)
			{
				for (int i = 0; i < entries.Length; i++)
				{
					LeaderboardEntryDTO leaderboardEntryDTO = entries[i];
					list.Add(new LeaderboardEntryData(leaderboardEntryDTO.score, leaderboardEntryDTO.rank, array[i], leaderboardEntryDTO.platformUserId, null));
					if (leaderboardEntryDTO.platformUserId == this._platformUserId)
					{
						referencePlayerScoreIndex = i;
					}
				}
			}
			result = new GetLeaderboardEntriesResult(apiResponse.isError, list.ToArray(), referencePlayerScoreIndex);
		}
		return result;
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000252C0 File Offset: 0x000234C0
	public async Task<SendLeaderboardEntryResult> SendLevelScoreResultAsync(LevelScoreResultsData levelResultsData, CancellationToken cancellationToken)
	{
		string leaderboardId = this.GetLeaderboardId(levelResultsData.difficultyBeatmap);
		GameplayModifiersDTO[] gameplayModifiers = GameplayModifiersHelper.ToDTO(levelResultsData.gameplayModifiers);
		LevelScoreResultDTO levelScoreResultDto = new LevelScoreResultDTO
		{
			leaderboardId = leaderboardId,
			rawScore = levelResultsData.rawScore,
			modifiedScore = levelResultsData.modifiedScore,
			fullCombo = levelResultsData.fullCombo,
			goodCutsCount = levelResultsData.goodCutsCount,
			badCutsCount = levelResultsData.badCutsCount,
			missedCount = levelResultsData.missedCount,
			maxCombo = levelResultsData.maxCombo,
			gameplayModifiers = gameplayModifiers,
			deviceModel = XRDevice.model,
			guid = this._guid
		};
		Response response = await this._apiLeaderboardsModel.SendLevelScoreResultAsync(levelScoreResultDto, cancellationToken);
		if (response == Response.Success)
		{
			Action<string> action = this.scoreForLeaderboardDidUploadEvent;
			if (action != null)
			{
				action(leaderboardId);
			}
		}
		return (response != Response.Success) ? SendLeaderboardEntryResult.Failed : SendLeaderboardEntryResult.OK;
	}

	// Token: 0x040006DA RID: 1754
	private IApiLeaderboardsModel _apiLeaderboardsModel;

	// Token: 0x040006DB RID: 1755
	private IUserLoginDtoDataSource _userLoginDataSource;

	// Token: 0x040006DC RID: 1756
	private string _guid;

	// Token: 0x040006DD RID: 1757
	private string[] _friendsUserIds;

	// Token: 0x040006DE RID: 1758
	private string _platformUserId;

	// Token: 0x040006DF RID: 1759
	private PlatformUserNamesLoader _platformUserNamesLoader;
}
