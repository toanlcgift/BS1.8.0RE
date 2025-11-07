using System;
using System.Collections.Generic;

// Token: 0x0200012C RID: 300
public class BeatmapLevelCollection : IBeatmapLevelCollection
{
	// Token: 0x170000DB RID: 219
	// (get) Token: 0x0600048F RID: 1167 RVA: 0x00004CFE File Offset: 0x00002EFE
	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._levels;
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00004D06 File Offset: 0x00002F06
	public BeatmapLevelCollection(IPreviewBeatmapLevel[] levels)
	{
		this._levels = levels;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00021DF4 File Offset: 0x0001FFF4
	public static BeatmapLevelCollection CreateBeatmapLevelCollectionByUsingBeatmapCharacteristicFiltering(IBeatmapLevelCollection beatmapLevelCollection, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		IPreviewBeatmapLevel[] beatmapLevels = beatmapLevelCollection.beatmapLevels;
		List<IPreviewBeatmapLevel> list = new List<IPreviewBeatmapLevel>();
		foreach (IPreviewBeatmapLevel previewBeatmapLevel in beatmapLevels)
		{
			PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets = previewBeatmapLevel.previewDifficultyBeatmapSets;
			for (int j = 0; j < previewDifficultyBeatmapSets.Length; j++)
			{
				if (previewDifficultyBeatmapSets[j].beatmapCharacteristic == beatmapCharacteristic)
				{
					list.Add(previewBeatmapLevel);
				}
			}
		}
		return new BeatmapLevelCollection(list.ToArray());
	}

	// Token: 0x040004E4 RID: 1252
	private IPreviewBeatmapLevel[] _levels;
}
