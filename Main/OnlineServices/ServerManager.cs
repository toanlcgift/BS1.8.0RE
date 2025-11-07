using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OnlineServices
{
	// Token: 0x0200049C RID: 1180
	public class ServerManager : MonoBehaviour
	{
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x060015C5 RID: 5573 RVA: 0x0004F80C File Offset: 0x0004DA0C
		// (remove) Token: 0x060015C6 RID: 5574 RVA: 0x0004F844 File Offset: 0x0004DA44
		public event Action<PlatformServicesAvailabilityInfo> platformServicesAvailabilityInfoChangedEvent;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x060015C7 RID: 5575 RVA: 0x0004F87C File Offset: 0x0004DA7C
		// (remove) Token: 0x060015C8 RID: 5576 RVA: 0x0004F8B4 File Offset: 0x0004DAB4
		public event Action<string> scoreForLeaderboardDidUploadEvent;

		// Token: 0x060015C9 RID: 5577 RVA: 0x0004F8EC File Offset: 0x0004DAEC
		public void Init(string hostname)
		{
			PlatformUserNamesLoader platformUserNamesLoader = new PlatformUserNamesLoader(this.platformUserModel);
			this._leaderboardsModel = new HTTPLeaderboardsModel(hostname, this.platformUserModel, platformUserNamesLoader);
			this._platformOnlineServicesAvailabilityModel = new PlatformOnlineServicesAvailabilityModel();
			this._platformOnlineServicesAvailabilityModel.platformServicesAvailabilityInfoChangedEvent += delegate(PlatformServicesAvailabilityInfo availabilityInfo)
			{
				Action<PlatformServicesAvailabilityInfo> action = this.platformServicesAvailabilityInfoChangedEvent;
				if (action == null)
				{
					return;
				}
				action(availabilityInfo);
			};
			this._levelScoreUploader = new LevelScoreUploader(this._leaderboardsModel, this._platformOnlineServicesAvailabilityModel);
			this._levelScoreUploader.scoreForLeaderboardDidUploadEvent += delegate(string leaderboardId)
			{
				Action<string> action = this.scoreForLeaderboardDidUploadEvent;
				if (action == null)
				{
					return;
				}
				action(leaderboardId);
			};
			this._initialized = true;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00010492 File Offset: 0x0000E692
		public string GetLeaderboardId(IDifficultyBeatmap difficultyBeatmap)
		{
			if (!this._initialized)
			{
				return null;
			}
			return this._leaderboardsModel.GetLeaderboardId(difficultyBeatmap);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004F970 File Offset: 0x0004DB70
		public async Task<LeaderboardEntriesResult> GetLeaderboardEntriesAsync(GetLeaderboardFilterData leaderboardFilterData, CancellationToken cancellationToken)
		{
			LeaderboardEntriesResult result;
			if (!this._initialized)
			{
				result = LeaderboardEntriesResult.notInicializedError;
			}
			else
			{
				TaskAwaiter<PlatformServicesAvailabilityInfo> taskAwaiter = this._platformOnlineServicesAvailabilityModel.GetPlatformServicesAvailabilityInfo(cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					taskAwaiter.GetResult();
					TaskAwaiter<PlatformServicesAvailabilityInfo> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<PlatformServicesAvailabilityInfo>);
				}
				if (taskAwaiter.GetResult().availability != PlatformServicesAvailabilityInfo.OnlineServicesAvailability.Available)
				{
					result = LeaderboardEntriesResult.onlineServicesUnavailableError;
				}
				else
				{
					GetLeaderboardEntriesResult getLeaderboardEntriesResult = await this._leaderboardsModel.GetLeaderboardEntriesAsync(leaderboardFilterData, cancellationToken);
					if (getLeaderboardEntriesResult.isError)
					{
						result = LeaderboardEntriesResult.somethingWentWrongError;
					}
					else
					{
						this._levelScoreUploader.TrySendPreviouslyUnsuccessfullySentResults();
						result = LeaderboardEntriesResult.FromGetLeaderboardEntriesResult(getLeaderboardEntriesResult);
					}
				}
			}
			return result;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000104AA File Offset: 0x0000E6AA
		public void SendLevelScoreResult(LevelScoreResultsData levelScoreResultsData)
		{
			if (!this._initialized)
			{
				return;
			}
			this._levelScoreUploader.SendLevelScoreResult(levelScoreResultsData);
		}

		// Token: 0x040015FE RID: 5630
		[SerializeField]
		private PlatformUserModelSO platformUserModel;

		// Token: 0x04001601 RID: 5633
		private HTTPLeaderboardsModel _leaderboardsModel;

		// Token: 0x04001602 RID: 5634
		private bool _initialized;

		// Token: 0x04001603 RID: 5635
		private LevelScoreUploader _levelScoreUploader;

		// Token: 0x04001604 RID: 5636
		private PlatformOnlineServicesAvailabilityModel _platformOnlineServicesAvailabilityModel;
	}
}
