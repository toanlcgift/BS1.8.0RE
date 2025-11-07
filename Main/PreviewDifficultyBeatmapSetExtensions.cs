using System;
using System.Collections.Generic;

// Token: 0x020000FB RID: 251
public static class PreviewDifficultyBeatmapSetExtensions
{
	// Token: 0x060003CB RID: 971 RVA: 0x00020318 File Offset: 0x0001E518
	public static BeatmapCharacteristicSO[] GetBeatmapCharacteristics(this PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSet)
	{
		if (previewDifficultyBeatmapSet == null)
		{
			return null;
		}
		BeatmapCharacteristicSO[] array = new BeatmapCharacteristicSO[previewDifficultyBeatmapSet.Length];
		for (int i = 0; i < previewDifficultyBeatmapSet.Length; i++)
		{
			array[i] = previewDifficultyBeatmapSet[i].beatmapCharacteristic;
		}
		return array;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00020350 File Offset: 0x0001E550
	public static PreviewDifficultyBeatmapSet[] GetPreviewDifficultyBeatmapSetWithout360Movement(this PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSet)
	{
		if (previewDifficultyBeatmapSet == null)
		{
			return null;
		}
		List<PreviewDifficultyBeatmapSet> list = new List<PreviewDifficultyBeatmapSet>(previewDifficultyBeatmapSet);
		int i = 0;
		while (i < list.Count)
		{
			if (list[i].beatmapCharacteristic.requires360Movement)
			{
				list.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		return list.ToArray();
	}
}
