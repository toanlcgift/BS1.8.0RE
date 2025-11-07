using System;
using System.Collections.Generic;

// Token: 0x0200008B RID: 139
public class BeatDataMirrorTransform
{
	// Token: 0x06000228 RID: 552 RVA: 0x0001B164 File Offset: 0x00019364
	public static BeatmapData CreateTransformedData(BeatmapData beatmapData)
	{
		BeatmapLineData[] beatmapLinesData = beatmapData.beatmapLinesData;
		int[] array = new int[beatmapLinesData.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
		}
		int num = 0;
		for (int j = 0; j < beatmapLinesData.Length; j++)
		{
			num += beatmapLinesData[j].beatmapObjectsData.Length;
		}
		List<BeatmapObjectData> list = new List<BeatmapObjectData>(num);
		bool flag;
		do
		{
			flag = false;
			float num2 = 999999f;
			int num3 = 0;
			for (int k = 0; k < beatmapLinesData.Length; k++)
			{
				BeatmapObjectData[] beatmapObjectsData = beatmapLinesData[k].beatmapObjectsData;
				int num4 = array[k];
				if (num4 < beatmapObjectsData.Length)
				{
					flag = true;
					float time = beatmapObjectsData[num4].time;
					if (time < num2)
					{
						num2 = time;
						num3 = k;
					}
				}
			}
			if (flag)
			{
				list.Add(beatmapLinesData[num3].beatmapObjectsData[array[num3]].GetCopy());
				array[num3]++;
			}
		}
		while (flag);
		BeatDataMirrorTransform.MirrorTransformBeatmapObjects(list, beatmapData.beatmapLinesData.Length);
		int[] array2 = new int[beatmapLinesData.Length];
		for (int l = 0; l < list.Count; l++)
		{
			BeatmapObjectData beatmapObjectData = list[l];
			array2[beatmapObjectData.lineIndex]++;
		}
		BeatmapLineData[] array3 = new BeatmapLineData[beatmapLinesData.Length];
		for (int m = 0; m < beatmapLinesData.Length; m++)
		{
			array3[m] = new BeatmapLineData();
			array3[m].beatmapObjectsData = new BeatmapObjectData[array2[m]];
			array[m] = 0;
		}
		for (int n = 0; n < list.Count; n++)
		{
			BeatmapObjectData beatmapObjectData2 = list[n];
			int lineIndex = beatmapObjectData2.lineIndex;
			array3[lineIndex].beatmapObjectsData[array[lineIndex]] = beatmapObjectData2;
			array[lineIndex]++;
		}
		BeatmapEventData[] array4 = new BeatmapEventData[beatmapData.beatmapEventData.Length];
		for (int num5 = 0; num5 < beatmapData.beatmapEventData.Length; num5++)
		{
			BeatmapEventData beatmapEventData = beatmapData.beatmapEventData[num5];
			if (beatmapEventData.type.IsRotationEvent())
			{
				int value = 7 - beatmapEventData.value;
				array4[num5] = new BeatmapEventData(beatmapEventData.time, beatmapEventData.type, value);
			}
			else
			{
				array4[num5] = beatmapEventData;
			}
		}
		return new BeatmapData(array3, array4);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0001B390 File Offset: 0x00019590
	private static void MirrorTransformBeatmapObjects(List<BeatmapObjectData> beatmapObjects, int beatmapLineCount)
	{
		for (int i = 0; i < beatmapObjects.Count; i++)
		{
			BeatmapObjectData beatmapObjectData = beatmapObjects[i];
			beatmapObjectData.MirrorLineIndex(beatmapLineCount);
			if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
			{
				NoteData noteData = beatmapObjectData as NoteData;
				if (noteData != null)
				{
					noteData.SwitchNoteType();
					noteData.MirrorTransformCutDirection();
				}
			}
		}
	}
}
