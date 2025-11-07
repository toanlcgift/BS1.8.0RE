using System;

// Token: 0x02000164 RID: 356
public struct LevelDataAssetDownloadUpdate
{
	// Token: 0x0600058F RID: 1423 RVA: 0x00005487 File Offset: 0x00003687
	public LevelDataAssetDownloadUpdate(string levelID, uint bytesTotal, uint bytesTransferred, LevelDataAssetDownloadUpdate.AssetDownloadingState assetDownloadingState)
	{
		this.levelID = levelID;
		this.bytesTotal = bytesTotal;
		this.bytesTransferred = bytesTransferred;
		this.assetDownloadingState = assetDownloadingState;
	}

	// Token: 0x040005B8 RID: 1464
	public readonly string levelID;

	// Token: 0x040005B9 RID: 1465
	public readonly uint bytesTotal;

	// Token: 0x040005BA RID: 1466
	public readonly uint bytesTransferred;

	// Token: 0x040005BB RID: 1467
	public readonly LevelDataAssetDownloadUpdate.AssetDownloadingState assetDownloadingState;

	// Token: 0x02000165 RID: 357
	public enum AssetDownloadingState
	{
		// Token: 0x040005BD RID: 1469
		PreparingToDownload,
		// Token: 0x040005BE RID: 1470
		Downloading,
		// Token: 0x040005BF RID: 1471
		Completed
	}
}
