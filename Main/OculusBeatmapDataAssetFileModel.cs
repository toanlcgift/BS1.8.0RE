using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class OculusBeatmapDataAssetFileModel : IBeatmapDataAssetFileModel
{
	// Token: 0x14000013 RID: 19
	// (add) Token: 0x060006E5 RID: 1765 RVA: 0x00026844 File Offset: 0x00024A44
	// (remove) Token: 0x060006E6 RID: 1766 RVA: 0x0002687C File Offset: 0x00024A7C
	public event Action<LevelDataAssetDownloadUpdate> levelDataAssetDownloadUpdateEvent;

	// Token: 0x060006E7 RID: 1767 RVA: 0x000268B4 File Offset: 0x00024AB4
	public OculusBeatmapDataAssetFileModel(OculusLevelProductsModelSO oculusLevelProductsModel)
	{
		this._oculusLevelProductsModel = oculusLevelProductsModel;
		AssetFile.SetDownloadUpdateNotificationCallback(delegate(Message<AssetFileDownloadUpdate> msg)
		{
			this.HandleAssetFileDownloadUpdate(msg);
		});
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00026918 File Offset: 0x00024B18
	public async Task<bool> TryDeleteAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		string levelId = previewBeatmapLevel.levelID;
		string assetFileName = BeatmapDataAssetsModel.AssetBundleNameForBeatmapLevel(levelId);
		OculusLevelProductsModelSO.LevelProductData levelProductData = this._oculusLevelProductsModel.GetLevelProductData(levelId);
		if (levelProductData != null)
		{
			string assetFile = levelProductData.assetFile;
			this._assetFileToAssetDetails.ContainsKey(assetFile);
		}
		TaskCompletionSource<bool> taskSource = new TaskCompletionSource<bool>();
		AssetFile.DeleteByName(assetFileName).OnComplete(delegate(Message<AssetFileDeleteResult> msg)
		{
			if (msg.Data != null && this._lastAssetFileDownloadUpdateForAssetIds.ContainsKey(msg.Data.AssetFileId))
			{
				this._lastAssetFileDownloadUpdateForAssetIds.Remove(msg.Data.AssetFileId);
			}
			this._downloadedAssetBundleFiles.Remove(levelId);
			if (cancellationToken.IsCancellationRequested)
			{
				taskSource.TrySetCanceled();
				return;
			}
			taskSource.TrySetResult(!msg.IsError);
		});
		bool flag = await taskSource.Task;
		bool deleted = flag;
		await this.ReloadAssetDetailsForAllLevelsAsync(cancellationToken);
		return deleted;
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00026970 File Offset: 0x00024B70
	public async Task<GetAssetBundleFileResult> GetAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		string levelId = previewBeatmapLevel.levelID;
		OculusLevelProductsModelSO.LevelProductData levelProductData = this._oculusLevelProductsModel.GetLevelProductData(levelId);
		GetAssetBundleFileResult result;
		if (levelProductData == null)
		{
			result = new GetAssetBundleFileResult(true, null);
		}
		else
		{
			string assetFile = levelProductData.assetFile;
			BeatmapDataAssetsModel.AssetBundleNameForBeatmapLevel(previewBeatmapLevel.levelID);
			if (this._downloadedAssetBundleFiles.ContainsKey(levelId))
			{
				result = new GetAssetBundleFileResult(false, this._downloadedAssetBundleFiles[levelId]);
			}
			else
			{
				if (Time.realtimeSinceStartup - this._lastAssetFileDownloadUpdateTime > 120f)
				{
					this._assetFileToAssetDetails.Clear();
					bool flag = false;
					foreach (KeyValuePair<ulong, OculusBeatmapDataAssetFileModel.LevelDownloadingData> keyValuePair in this._assetIdToDownloadinData)
					{
						keyValuePair.Value.downloadAssetBundleFileTCS.TrySetResult(new GetAssetBundleFileResult(true, null));
						if (keyValuePair.Value.levelId == levelId)
						{
							flag = true;
						}
					}
					this._assetIdToDownloadinData.Clear();
					this._lastAssetFileDownloadUpdateForAssetIds.Clear();
					this._lastAssetFileDownloadUpdateTime = Time.realtimeSinceStartup;
					if (flag)
					{
						return new GetAssetBundleFileResult(true, null);
					}
				}
				if (!this._assetFileToAssetDetails.ContainsKey(assetFile))
				{
					bool flag2 = false;
					await this._semaphoreSlim.WaitAsync(cancellationToken);
					try
					{
						flag2 = await this.ReloadAssetDetailsForAllLevelsAsync(cancellationToken);
					}
					finally
					{
						this._semaphoreSlim.Release();
					}
					if (!flag2 || !this._assetFileToAssetDetails.ContainsKey(assetFile))
					{
						return new GetAssetBundleFileResult(true, null);
					}
				}
				AssetDetails assetDetails = this._assetFileToAssetDetails[assetFile];
				cancellationToken.ThrowIfCancellationRequested();
				GetAssetBundleFileResult getAssetBundleFileResult = await this.GetDownloadAssetBundleFileAsync(levelId, assetDetails, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				result = getAssetBundleFileResult;
			}
		}
		return result;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x000269C8 File Offset: 0x00024BC8
	private async Task<bool> ReloadAssetDetailsForAllLevelsAsync(CancellationToken cancellationToken)
	{
		TaskCompletionSource<bool> taskSource = new TaskCompletionSource<bool>();
		AssetFile.GetList().OnComplete(delegate(Message<AssetDetailsList> getListMsg)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				taskSource.TrySetCanceled();
				return;
			}
			if (getListMsg.IsError)
			{
				taskSource.TrySetResult(false);
				return;
			}
			foreach (AssetDetails assetDetails in getListMsg.Data)
			{
				string fileName = Path.GetFileName(assetDetails.Filepath);
				this._assetFileToAssetDetails[fileName] = assetDetails;
			}
			taskSource.TrySetResult(true);
		});
		return await taskSource.Task;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00026A18 File Offset: 0x00024C18
	private async Task<GetAssetBundleFileResult> GetDownloadAssetBundleFileAsync(string levelId, AssetDetails assetDetails, CancellationToken cancellationToken)
	{
		GetAssetBundleFileResult result;
		if (assetDetails.DownloadStatus == "installed")
		{
			this._downloadedAssetBundleFiles[levelId] = assetDetails.Filepath;
			if (this._assetIdToDownloadinData.ContainsKey(assetDetails.AssetId))
			{
				OculusBeatmapDataAssetFileModel.LevelDownloadingData levelDownloadingData = this._assetIdToDownloadinData[assetDetails.AssetId];
				levelDownloadingData.downloadAssetBundleFileTCS.TrySetResult(new GetAssetBundleFileResult(false, levelDownloadingData.assetBundlePath));
				this._assetIdToDownloadinData.Remove(assetDetails.AssetId);
			}
			result = new GetAssetBundleFileResult(false, assetDetails.Filepath);
		}
		else
		{
			bool flag = !this._assetIdToDownloadinData.ContainsKey(assetDetails.AssetId);
			TaskCompletionSource<GetAssetBundleFileResult> taskSource = this.GetTaskCompletionSourceForDownload(levelId, assetDetails);
			if (this._lastAssetFileDownloadUpdateForAssetIds.ContainsKey(assetDetails.AssetId))
			{
				AssetFileDownloadUpdate assetFileDownloadUpdate = this._lastAssetFileDownloadUpdateForAssetIds[assetDetails.AssetId];
				if (assetFileDownloadUpdate.Completed)
				{
					result = new GetAssetBundleFileResult(false, assetDetails.Filepath);
				}
				else
				{
					LevelDataAssetDownloadUpdate.AssetDownloadingState assetDownloadingState = assetFileDownloadUpdate.Completed ? LevelDataAssetDownloadUpdate.AssetDownloadingState.Completed : LevelDataAssetDownloadUpdate.AssetDownloadingState.Downloading;
					Action<LevelDataAssetDownloadUpdate> action = this.levelDataAssetDownloadUpdateEvent;
					if (action != null)
					{
						action(new LevelDataAssetDownloadUpdate(levelId, assetFileDownloadUpdate.BytesTotal, (uint)assetFileDownloadUpdate.BytesTransferred, assetDownloadingState));
					}
					result = await taskSource.Task;
				}
			}
			else
			{
				Action<LevelDataAssetDownloadUpdate> action2 = this.levelDataAssetDownloadUpdateEvent;
				if (action2 != null)
				{
					action2(new LevelDataAssetDownloadUpdate(levelId, 0U, 0U, LevelDataAssetDownloadUpdate.AssetDownloadingState.PreparingToDownload));
				}
				if (flag)
				{
					AssetFile.DownloadById(assetDetails.AssetId).OnComplete(delegate(Message<AssetFileDownloadResult> msg)
					{
						if (msg.IsError)
						{
							taskSource.TrySetResult(new GetAssetBundleFileResult(true, null));
							this._assetIdToDownloadinData.Remove(assetDetails.AssetId);
							return;
						}
					});
				}
				result = await taskSource.Task;
			}
		}
		return result;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00026A70 File Offset: 0x00024C70
	private TaskCompletionSource<GetAssetBundleFileResult> GetTaskCompletionSourceForDownload(string levelId, AssetDetails assetDetail)
	{
		if (!this._assetIdToDownloadinData.ContainsKey(assetDetail.AssetId))
		{
			this._assetIdToDownloadinData[assetDetail.AssetId] = new OculusBeatmapDataAssetFileModel.LevelDownloadingData(levelId, assetDetail.Filepath);
		}
		return this._assetIdToDownloadinData[assetDetail.AssetId].downloadAssetBundleFileTCS;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00026AC4 File Offset: 0x00024CC4
	private void HandleAssetFileDownloadUpdate(Message<AssetFileDownloadUpdate> msg)
	{
		this._lastAssetFileDownloadUpdateTime = Time.realtimeSinceStartup;
		bool isError = msg.IsError;
		if (msg.Data.AssetId == 0UL)
		{
			return;
		}
		if (msg.IsError)
		{
			this._assetFileToAssetDetails.Clear();
			foreach (KeyValuePair<ulong, OculusBeatmapDataAssetFileModel.LevelDownloadingData> keyValuePair in this._assetIdToDownloadinData)
			{
				keyValuePair.Value.downloadAssetBundleFileTCS.TrySetResult(new GetAssetBundleFileResult(true, null));
			}
			this._assetIdToDownloadinData.Clear();
			this._lastAssetFileDownloadUpdateForAssetIds.Clear();
			this._lastAssetFileDownloadUpdateTime = Time.realtimeSinceStartup;
			return;
		}
		if (!msg.IsError)
		{
			ulong assetId = msg.Data.AssetId;
			this._lastAssetFileDownloadUpdateForAssetIds[assetId] = msg.Data;
			if (this._assetIdToDownloadinData.ContainsKey(assetId))
			{
				OculusBeatmapDataAssetFileModel.LevelDownloadingData levelDownloadingData = this._assetIdToDownloadinData[assetId];
				if (msg.Data.AssetId != 0UL && (msg.Data.BytesTransferred == -1 || msg.Data.BytesTotal == 4294967295U))
				{
					levelDownloadingData.downloadAssetBundleFileTCS.TrySetResult(new GetAssetBundleFileResult(true, null));
					this._assetIdToDownloadinData.Remove(assetId);
					return;
				}
				LevelDataAssetDownloadUpdate.AssetDownloadingState assetDownloadingState = msg.Data.Completed ? LevelDataAssetDownloadUpdate.AssetDownloadingState.Completed : LevelDataAssetDownloadUpdate.AssetDownloadingState.Downloading;
				Action<LevelDataAssetDownloadUpdate> action = this.levelDataAssetDownloadUpdateEvent;
				if (action != null)
				{
					action(new LevelDataAssetDownloadUpdate(levelDownloadingData.levelId, msg.Data.BytesTotal, (uint)msg.Data.BytesTransferred, assetDownloadingState));
				}
				if (msg.Data.Completed)
				{
					this._downloadedAssetBundleFiles[levelDownloadingData.levelId] = levelDownloadingData.assetBundlePath;
					levelDownloadingData.downloadAssetBundleFileTCS.TrySetResult(new GetAssetBundleFileResult(false, levelDownloadingData.assetBundlePath));
					this._assetIdToDownloadinData.Remove(assetId);
				}
			}
		}
	}

	// Token: 0x04000758 RID: 1880
	private Dictionary<ulong, OculusBeatmapDataAssetFileModel.LevelDownloadingData> _assetIdToDownloadinData = new Dictionary<ulong, OculusBeatmapDataAssetFileModel.LevelDownloadingData>();

	// Token: 0x04000759 RID: 1881
	private Dictionary<string, string> _downloadedAssetBundleFiles = new Dictionary<string, string>();

	// Token: 0x0400075A RID: 1882
	private Dictionary<ulong, AssetFileDownloadUpdate> _lastAssetFileDownloadUpdateForAssetIds = new Dictionary<ulong, AssetFileDownloadUpdate>();

	// Token: 0x0400075B RID: 1883
	private const float kMaxTimeOutBeforeFail = 120f;

	// Token: 0x0400075C RID: 1884
	private float _lastAssetFileDownloadUpdateTime;

	// Token: 0x0400075D RID: 1885
	private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

	// Token: 0x0400075E RID: 1886
	private Dictionary<string, AssetDetails> _assetFileToAssetDetails = new Dictionary<string, AssetDetails>();

	// Token: 0x0400075F RID: 1887
	private OculusLevelProductsModelSO _oculusLevelProductsModel;

	// Token: 0x020001C3 RID: 451
	private class LevelDownloadingData
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00005F82 File Offset: 0x00004182
		public LevelDownloadingData(string levelId, string assetBundlePath)
		{
			this.levelId = levelId;
			this.assetBundlePath = assetBundlePath;
			this.downloadAssetBundleFileTCS = new TaskCompletionSource<GetAssetBundleFileResult>();
		}

		// Token: 0x04000760 RID: 1888
		public readonly string levelId;

		// Token: 0x04000761 RID: 1889
		public readonly string assetBundlePath;

		// Token: 0x04000762 RID: 1890
		public readonly TaskCompletionSource<GetAssetBundleFileResult> downloadAssetBundleFileTCS;
	}
}
