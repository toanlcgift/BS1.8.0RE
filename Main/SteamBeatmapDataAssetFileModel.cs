using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Steamworks;

// Token: 0x020001CE RID: 462
public class SteamBeatmapDataAssetFileModel : IBeatmapDataAssetFileModel
{
	// Token: 0x14000014 RID: 20
	// (add) Token: 0x0600070C RID: 1804 RVA: 0x0002788C File Offset: 0x00025A8C
	// (remove) Token: 0x0600070D RID: 1805 RVA: 0x000278C4 File Offset: 0x00025AC4
	public event Action<LevelDataAssetDownloadUpdate> levelDataAssetDownloadUpdateEvent;

	// Token: 0x0600070E RID: 1806 RVA: 0x00006096 File Offset: 0x00004296
	public SteamBeatmapDataAssetFileModel(SteamLevelProductsModelSO steamLevelProductsModel)
	{
		this._steamLevelProductsModel = steamLevelProductsModel;
		this._beatmapAssetDataPath = Path.Combine("DLC", "Levels");
		Action<LevelDataAssetDownloadUpdate> action = this.levelDataAssetDownloadUpdateEvent;
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000278FC File Offset: 0x00025AFC
	public async Task<bool> TryDeleteAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		return await Task.FromResult<bool>(false);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002793C File Offset: 0x00025B3C
	public async Task<GetAssetBundleFileResult> GetAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		SteamLevelProductsModelSO.LevelProductData levelProductData = this._steamLevelProductsModel.GetLevelProductData(previewBeatmapLevel.levelID);
		GetAssetBundleFileResult result;
		if (levelProductData != null && SteamApps.BIsDlcInstalled((AppId_t)levelProductData.appId))
		{
			string assetBundlePath = Path.Combine(Path.Combine(this._beatmapAssetDataPath, previewBeatmapLevel.levelID), BeatmapDataAssetsModel.AssetBundleNameForBeatmapLevel(previewBeatmapLevel.levelID));
			result = await Task.FromResult<GetAssetBundleFileResult>(new GetAssetBundleFileResult(false, assetBundlePath));
		}
		else
		{
			result = await Task.FromResult<GetAssetBundleFileResult>(new GetAssetBundleFileResult(true, null));
		}
		return result;
	}

	// Token: 0x04000795 RID: 1941
	private SteamLevelProductsModelSO _steamLevelProductsModel;

	// Token: 0x04000796 RID: 1942
	private string _beatmapAssetDataPath;
}
