using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x02000125 RID: 293
public class BeatmapLevelLoader
{
	// Token: 0x06000483 RID: 1155 RVA: 0x00004C39 File Offset: 0x00002E39
	public BeatmapLevelLoader(BeatmapLevelDataLoaderSO beatmapLevelDataLoader, IBeatmapDataAssetFileModel beatmapDataAssetFileModel)
	{
		this._beatmapLevelDataLoader = beatmapLevelDataLoader;
		this._beatmapDataAssetFileModel = beatmapDataAssetFileModel;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x000219B0 File Offset: 0x0001FBB0
	public async Task<BeatmapLevelLoader.LoadBeatmapLevelResult> LoadBeatmapLevelAsync(IPreviewBeatmapLevel previewLevel, CancellationToken cancellationToken)
	{
		GetAssetBundleFileResult getAssetBundleFileResult = await this._beatmapDataAssetFileModel.GetAssetBundleFileForPreviewLevelAsync(previewLevel, cancellationToken);
		BeatmapLevelLoader.LoadBeatmapLevelResult result;
		if (getAssetBundleFileResult.isError)
		{
			result = new BeatmapLevelLoader.LoadBeatmapLevelResult(true, null);
		}
		else if (!File.Exists(getAssetBundleFileResult.assetBundlePath))
		{
			await this._beatmapDataAssetFileModel.TryDeleteAssetBundleFileForPreviewLevelAsync(previewLevel, cancellationToken);
			result = new BeatmapLevelLoader.LoadBeatmapLevelResult(true, null);
		}
		else
		{
			string assetBundlePath = getAssetBundleFileResult.assetBundlePath;
			string levelDataAssetName = BeatmapDataAssetsModel.BeatmapLevelDataAssetNameForBeatmapLevel(previewLevel.levelID);
			IBeatmapLevel level = await this._beatmapLevelDataLoader.LoadBeatmapLevelFormAssetBundleAsync(previewLevel, assetBundlePath, levelDataAssetName, cancellationToken);
			if (level == null)
			{
				await this._beatmapDataAssetFileModel.TryDeleteAssetBundleFileForPreviewLevelAsync(previewLevel, cancellationToken);
			}
			result = new BeatmapLevelLoader.LoadBeatmapLevelResult(level == null, level);
		}
		return result;
	}

	// Token: 0x040004CA RID: 1226
	private BeatmapLevelDataLoaderSO _beatmapLevelDataLoader;

	// Token: 0x040004CB RID: 1227
	private IBeatmapDataAssetFileModel _beatmapDataAssetFileModel;

	// Token: 0x02000126 RID: 294
	public struct LoadBeatmapLevelResult
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00004C4F File Offset: 0x00002E4F
		public LoadBeatmapLevelResult(bool isError, IBeatmapLevel beatmapLevel)
		{
			this.isError = isError;
			this.beatmapLevel = beatmapLevel;
		}

		// Token: 0x040004CC RID: 1228
		public readonly bool isError;

		// Token: 0x040004CD RID: 1229
		public readonly IBeatmapLevel beatmapLevel;
	}
}
