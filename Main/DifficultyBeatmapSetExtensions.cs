using System;
using System.Collections.Generic;

// Token: 0x02000158 RID: 344
public static class DifficultyBeatmapSetExtensions
{
	// Token: 0x0600054F RID: 1359 RVA: 0x00023344 File Offset: 0x00021544
	public static T[] GetDifficultyBeatmapSetsWithout360Movement<T>(this T[] difficultyBeatmapSets) where T : IDifficultyBeatmapSet
	{
		List<T> list = new List<T>(difficultyBeatmapSets);
		int i = 0;
		while (i < list.Count)
		{
			T t = list[i];
			if (t.beatmapCharacteristic.requires360Movement)
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

	// Token: 0x06000550 RID: 1360 RVA: 0x00023394 File Offset: 0x00021594
	public static PreviewDifficultyBeatmapSet[] GetPreviewDifficultyBeatmapSets<T>(this T[] difficultyBeatmapSets) where T : IDifficultyBeatmapSet
	{
		if (difficultyBeatmapSets == null)
		{
			return null;
		}
		PreviewDifficultyBeatmapSet[] array = new PreviewDifficultyBeatmapSet[difficultyBeatmapSets.Length];
		for (int i = 0; i < difficultyBeatmapSets.Length; i++)
		{
			T t = difficultyBeatmapSets[i];
			BeatmapDifficulty[] array2 = new BeatmapDifficulty[t.difficultyBeatmaps.Length];
			for (int j = 0; j < t.difficultyBeatmaps.Length; j++)
			{
				array2[j] = t.difficultyBeatmaps[j].difficulty;
			}
			array[i] = new PreviewDifficultyBeatmapSet(t.beatmapCharacteristic, array2);
		}
		return array;
	}
}
