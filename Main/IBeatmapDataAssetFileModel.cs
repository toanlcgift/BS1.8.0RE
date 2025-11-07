using System;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x02000162 RID: 354
public interface IBeatmapDataAssetFileModel
{
	// Token: 0x1400000F RID: 15
	// (add) Token: 0x0600058A RID: 1418
	// (remove) Token: 0x0600058B RID: 1419
	event Action<LevelDataAssetDownloadUpdate> levelDataAssetDownloadUpdateEvent;

	// Token: 0x0600058C RID: 1420
	Task<GetAssetBundleFileResult> GetAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken);

	// Token: 0x0600058D RID: 1421
	Task<bool> TryDeleteAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken);
}
