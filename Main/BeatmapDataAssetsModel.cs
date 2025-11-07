using System;

// Token: 0x02000117 RID: 279
public static class BeatmapDataAssetsModel
{
	// Token: 0x06000453 RID: 1107 RVA: 0x00004ABA File Offset: 0x00002CBA
	public static string BeatmapLevelDataAssetNameForBeatmapLevel(string levelID)
	{
		return levelID + "BeatmapLevelData.asset";
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00004AC7 File Offset: 0x00002CC7
	public static string AssetBundleNameForBeatmapLevel(string levelID)
	{
		return levelID.ToLower();
	}
}
