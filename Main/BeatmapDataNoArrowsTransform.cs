using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class BeatmapDataNoArrowsTransform
{
	// Token: 0x0600022B RID: 555 RVA: 0x0001B3DC File Offset: 0x000195DC
	public static BeatmapData CreateTransformedData(BeatmapData beatmapData, bool randomColors)
	{
		beatmapData = beatmapData.GetCopy();
		BeatmapLineData[] beatmapLinesData = beatmapData.beatmapLinesData;
		int[] array = new int[beatmapLinesData.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
		}
		UnityEngine.Random.InitState(0);
		bool flag;
		do
		{
			flag = false;
			float num = 999999f;
			int num2 = 0;
			for (int j = 0; j < beatmapLinesData.Length; j++)
			{
				BeatmapObjectData[] beatmapObjectsData = beatmapLinesData[j].beatmapObjectsData;
				int num3 = array[j];
				while (num3 < beatmapObjectsData.Length && beatmapObjectsData[num3].time < num + 0.001f)
				{
					flag = true;
					BeatmapObjectData beatmapObjectData = beatmapObjectsData[num3];
					float time = beatmapObjectData.time;
					if (Mathf.Abs(time - num) < 0.001f)
					{
						if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
						{
							num2++;
						}
					}
					else if (time < num)
					{
						num = time;
						if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
						{
							num2 = 1;
						}
						else
						{
							num2 = 0;
						}
					}
					num3++;
				}
			}
			for (int k = 0; k < beatmapLinesData.Length; k++)
			{
				BeatmapObjectData[] beatmapObjectsData2 = beatmapLinesData[k].beatmapObjectsData;
				int num4 = array[k];
				while (num4 < beatmapObjectsData2.Length && beatmapObjectsData2[num4].time < num + 0.001f)
				{
					BeatmapObjectData beatmapObjectData2 = beatmapObjectsData2[num4];
					if (beatmapObjectData2.beatmapObjectType == BeatmapObjectType.Note)
					{
						NoteData noteData = beatmapObjectData2 as NoteData;
						if (noteData != null)
						{
							noteData.SetNoteToAnyCutDirection();
							if (randomColors && num2 <= 2)
							{
								noteData.TransformNoteAOrBToRandomType();
							}
						}
					}
					array[k]++;
					num4++;
				}
			}
		}
		while (flag);
		return beatmapData;
	}
}
