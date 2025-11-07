using System;
using System.Collections.Generic;

// Token: 0x0200008D RID: 141
public class BeatmapDataObstaclesAndBombsTransform
{
	// Token: 0x0600022D RID: 557 RVA: 0x0001B554 File Offset: 0x00019754
	public static BeatmapData CreateTransformedData(BeatmapData beatmapData, GameplayModifiers.EnabledObstacleType enabledObstaclesType, bool noBombs)
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
				if (BeatmapDataObstaclesAndBombsTransform.ShouldUseBeatmapObject(beatmapLinesData[num3].beatmapObjectsData[array[num3]], enabledObstaclesType, noBombs))
				{
					list.Add(beatmapLinesData[num3].beatmapObjectsData[array[num3]].GetCopy());
				}
				array[num3]++;
			}
		}
		while (flag);
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
			array4[num5] = beatmapEventData;
		}
		return new BeatmapData(array3, array4);
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00003865 File Offset: 0x00001A65
	private static bool ShouldUseBeatmapObject(BeatmapObjectData beatmapObjectData, GameplayModifiers.EnabledObstacleType enabledObstaclesType, bool noBombs)
	{
		if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
		{
			if (enabledObstaclesType == GameplayModifiers.EnabledObstacleType.NoObstacles)
			{
				return false;
			}
			if (enabledObstaclesType == GameplayModifiers.EnabledObstacleType.FullHeightOnly)
			{
				return (beatmapObjectData as ObstacleData).obstacleType == ObstacleType.FullHeight;
			}
		}
		else if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note && (beatmapObjectData as NoteData).noteType == NoteType.Bomb)
		{
			return !noBombs;
		}
		return true;
	}
}
