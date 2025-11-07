using System;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x020001D9 RID: 473
public class PS4BeatmapDataAssetFileModel : IBeatmapDataAssetFileModel
{
	// Token: 0x14000015 RID: 21
	// (add) Token: 0x06000737 RID: 1847 RVA: 0x00027F74 File Offset: 0x00026174
	// (remove) Token: 0x06000738 RID: 1848 RVA: 0x00027FAC File Offset: 0x000261AC
	public event Action<LevelDataAssetDownloadUpdate> levelDataAssetDownloadUpdateEvent;

	// Token: 0x06000739 RID: 1849 RVA: 0x00027FE4 File Offset: 0x000261E4
	public async Task<GetAssetBundleFileResult> GetAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		return await Task.FromResult<GetAssetBundleFileResult>(new GetAssetBundleFileResult(true, null));
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00028024 File Offset: 0x00026224
	public async Task<bool> TryDeleteAssetBundleFileForPreviewLevelAsync(IPreviewBeatmapLevel previewBeatmapLevel, CancellationToken cancellationToken)
	{
		return await Task.FromResult<bool>(false);
	}
}
