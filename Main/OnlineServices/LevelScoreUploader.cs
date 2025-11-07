using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineServices
{
	// Token: 0x02000492 RID: 1170
	public class LevelScoreUploader
	{
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x060015AB RID: 5547 RVA: 0x0004F124 File Offset: 0x0004D324
		// (remove) Token: 0x060015AC RID: 5548 RVA: 0x0004F15C File Offset: 0x0004D35C
		public event Action<string> scoreForLeaderboardDidUploadEvent;

		// Token: 0x060015AD RID: 5549 RVA: 0x00010374 File Offset: 0x0000E574
		public LevelScoreUploader(HTTPLeaderboardsModel leaderboardsModel, PlatformOnlineServicesAvailabilityModel platformOnlineServicesAvailabilityModel)
		{
			this._leaderboardsModel = leaderboardsModel;
			this._platformOnlineServicesAvailabilityModel = platformOnlineServicesAvailabilityModel;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004F194 File Offset: 0x0004D394
		public void SendLevelScoreResult(LevelScoreResultsData levelScoreResultsData)
		{
			LevelScoreUploader.LevelScoreResultsDataUploadInfo item = new LevelScoreUploader.LevelScoreResultsDataUploadInfo
			{
				levelScoreResultsData = levelScoreResultsData
			};
			if (this._cancellationTokenSource != null && this._levelScoreResultsDataUploadInfos.Count > 0)
			{
				this._levelScoreResultsDataUploadInfos.Insert(1, item);
				return;
			}
			this._levelScoreResultsDataUploadInfos.Insert(0, item);
			this.SendLevelScoreResultAsync();
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000103A0 File Offset: 0x0000E5A0
		public void TrySendPreviouslyUnsuccessfullySentResults()
		{
			this.AddUnsuccessfullySentResults();
			this.SendLevelScoreResultAsync();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0004F1E8 File Offset: 0x0004D3E8
		private async void SendLevelScoreResultAsync()
		{
			if (this._cancellationTokenSource == null)
			{
				this._cancellationTokenSource = new CancellationTokenSource();
				CancellationToken cancellationToken = this._cancellationTokenSource.Token;
				try
				{
					TaskAwaiter<PlatformServicesAvailabilityInfo> taskAwaiter = this._platformOnlineServicesAvailabilityModel.GetPlatformServicesAvailabilityInfo(cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						taskAwaiter.GetResult();
						TaskAwaiter<PlatformServicesAvailabilityInfo> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<PlatformServicesAvailabilityInfo>);
					}
					if (taskAwaiter.GetResult().availability == PlatformServicesAvailabilityInfo.OnlineServicesAvailability.Available)
					{
						while (this._levelScoreResultsDataUploadInfos.Count > 0)
						{
							LevelScoreUploader.LevelScoreResultsDataUploadInfo levelScoreResultsDataUploadInfo = this._levelScoreResultsDataUploadInfos[0];
							this._levelScoreResultsDataUploadInfos.RemoveAt(0);
							LevelScoreResultsData levelScoreResultToUpload = levelScoreResultsDataUploadInfo.levelScoreResultsData;
							TaskAwaiter<SendLeaderboardEntryResult> taskAwaiter3 = this._leaderboardsModel.SendLevelScoreResultAsync(levelScoreResultToUpload, this._cancellationTokenSource.Token).GetAwaiter();
							if (!taskAwaiter3.IsCompleted)
							{
								taskAwaiter3.GetResult();
								TaskAwaiter<SendLeaderboardEntryResult> taskAwaiter4;
								taskAwaiter3 = taskAwaiter4;
								taskAwaiter4 = default(TaskAwaiter<SendLeaderboardEntryResult>);
							}
							if (taskAwaiter3.GetResult() == SendLeaderboardEntryResult.OK)
							{
								Action<string> action = this.scoreForLeaderboardDidUploadEvent;
								if (action != null)
								{
									action(this._leaderboardsModel.GetLeaderboardId(levelScoreResultToUpload.difficultyBeatmap));
								}
								this.AddUnsuccessfullySentResults();
							}
							else
							{
								levelScoreResultsDataUploadInfo.uploadAttemptCountLeft--;
								if (levelScoreResultsDataUploadInfo.uploadAttemptCountLeft > 0)
								{
									this._levelScoreResultsDataUploadInfos.Add(levelScoreResultsDataUploadInfo);
									await Task.Delay(Math.Max(1, 3 - levelScoreResultsDataUploadInfo.uploadAttemptCountLeft + 1) * 10 * 1000);
								}
								else
								{
									this._unsuccessfullySentLevelScoreResultsDataUploadInfos.Add(levelScoreResultsDataUploadInfo);
									levelScoreResultsDataUploadInfo.uploadAttemptCountLeft = 1;
								}
							}
							cancellationToken.ThrowIfCancellationRequested();
							levelScoreResultsDataUploadInfo = null;
							levelScoreResultToUpload = default(LevelScoreResultsData);
						}
					}
				}
				catch (OperationCanceledException)
				{
				}
				finally
				{
					this._cancellationTokenSource = null;
				}
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000103AE File Offset: 0x0000E5AE
		private void AddUnsuccessfullySentResults()
		{
			this._levelScoreResultsDataUploadInfos.AddRange(this._unsuccessfullySentLevelScoreResultsDataUploadInfos);
			this._unsuccessfullySentLevelScoreResultsDataUploadInfos.Clear();
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000103CC File Offset: 0x0000E5CC
		protected void OnDestroy()
		{
			CancellationTokenSource cancellationTokenSource = this._cancellationTokenSource;
			if (cancellationTokenSource == null)
			{
				return;
			}
			cancellationTokenSource.Cancel();
		}

		// Token: 0x040015DB RID: 5595
		private const int kMaxUploadAttempts = 3;

		// Token: 0x040015DC RID: 5596
		private CancellationTokenSource _cancellationTokenSource;

		// Token: 0x040015DD RID: 5597
		private HTTPLeaderboardsModel _leaderboardsModel;

		// Token: 0x040015DE RID: 5598
		private List<LevelScoreUploader.LevelScoreResultsDataUploadInfo> _unsuccessfullySentLevelScoreResultsDataUploadInfos = new List<LevelScoreUploader.LevelScoreResultsDataUploadInfo>();

		// Token: 0x040015DF RID: 5599
		private List<LevelScoreUploader.LevelScoreResultsDataUploadInfo> _levelScoreResultsDataUploadInfos = new List<LevelScoreUploader.LevelScoreResultsDataUploadInfo>();

		// Token: 0x040015E0 RID: 5600
		private PlatformOnlineServicesAvailabilityModel _platformOnlineServicesAvailabilityModel;

		// Token: 0x02000493 RID: 1171
		private class LevelScoreResultsDataUploadInfo
		{
			// Token: 0x040015E1 RID: 5601
			public LevelScoreResultsData levelScoreResultsData;

			// Token: 0x040015E2 RID: 5602
			public int uploadAttemptCountLeft = 3;
		}
	}
}
