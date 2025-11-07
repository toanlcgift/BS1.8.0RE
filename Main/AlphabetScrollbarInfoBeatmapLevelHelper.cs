using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000160 RID: 352
public static class AlphabetScrollbarInfoBeatmapLevelHelper
{
	// Token: 0x06000586 RID: 1414 RVA: 0x00023670 File Offset: 0x00021870
	public static AlphabetScrollInfo.Data[] CreateData(IPreviewBeatmapLevel[] previewBeatmapLevels, out IPreviewBeatmapLevel[] sortedPreviewBeatmapLevels)
	{
		List<AlphabetScrollInfo.Data> list = new List<AlphabetScrollInfo.Data>();
		if (previewBeatmapLevels == null || previewBeatmapLevels.Length == 0)
		{
			sortedPreviewBeatmapLevels = null;
			return null;
		}
		sortedPreviewBeatmapLevels = (from x in previewBeatmapLevels
		orderby x.songName.ToUpperInvariant()
		select x).ToArray<IPreviewBeatmapLevel>();
		string text = sortedPreviewBeatmapLevels[0].songName.ToUpperInvariant().Substring(0, 1);
		if (string.Compare(text, "A") < 0)
		{
			list.Add(new AlphabetScrollInfo.Data('#', 0));
		}
		else
		{
			list.Add(new AlphabetScrollInfo.Data(text[0], 0));
		}
		for (int i = 1; i < sortedPreviewBeatmapLevels.Length; i++)
		{
			string text2 = sortedPreviewBeatmapLevels[i].songName.ToUpperInvariant().Substring(0, 1);
			if (string.Compare(text2, "A") >= 0 && text != text2)
			{
				text = text2;
				if (list.Count >= 27)
				{
					list.Add(new AlphabetScrollInfo.Data(text2[0], i));
					break;
				}
				list.Add(new AlphabetScrollInfo.Data(text2[0], i));
			}
		}
		return list.ToArray();
	}

	// Token: 0x040005B1 RID: 1457
	private const string kFirstAlphabet = "A";

	// Token: 0x040005B2 RID: 1458
	private const char kNonAlphabetChar = '#';

	// Token: 0x040005B3 RID: 1459
	private const int kMaxCharactersCount = 28;
}
