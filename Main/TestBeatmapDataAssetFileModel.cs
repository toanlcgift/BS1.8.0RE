using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x02000166 RID: 358
public class TestBeatmapDataAssetFileModel : IBeatmapDataAssetFileModel
{
	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000590 RID: 1424 RVA: 0x0002377C File Offset: 0x0002197C
	// (remove) Token: 0x06000591 RID: 1425 RVA: 0x000237B4 File Offset: 0x000219B4
	public event Action<LevelDataAssetDownloadUpdate> levelDataAssetDownloadUpdateEvent;

	// Token: 0x06000592 RID: 1426 RVA: 0x000237EC File Offset: 0x000219EC
	public async Task<GetAssetBundleFileResult> GetAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		string path2 = previewBeatmapLevel.levelID.ToLower();
		string path = Path.Combine("BeatmapDataAssets", path2);
		Action<LevelDataAssetDownloadUpdate> action = this.levelDataAssetDownloadUpdateEvent;
		if (action != null)
		{
			action(new LevelDataAssetDownloadUpdate(previewBeatmapLevel.levelID, 0U, 0U, LevelDataAssetDownloadUpdate.AssetDownloadingState.PreparingToDownload));
		}
		await Task.Delay(50);
		for (uint i = 0U; i < 20U; i += 1U)
		{
			await Task.Delay(50);
			Action<LevelDataAssetDownloadUpdate> action2 = this.levelDataAssetDownloadUpdateEvent;
			if (action2 != null)
			{
				action2(new LevelDataAssetDownloadUpdate(previewBeatmapLevel.levelID, 20U, i, LevelDataAssetDownloadUpdate.AssetDownloadingState.Downloading));
			}
			cancellationToken.ThrowIfCancellationRequested();
		}
		cancellationToken.ThrowIfCancellationRequested();
		return await Task.FromResult<GetAssetBundleFileResult>(new GetAssetBundleFileResult(false, path));
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00023844 File Offset: 0x00021A44
	public async Task<bool> TryDeleteAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		return await Task.FromResult<bool>(false);
	}

	// Token: 0x040005C0 RID: 1472
	private const string kAssetsDir = "BeatmapDataAssets";
}
