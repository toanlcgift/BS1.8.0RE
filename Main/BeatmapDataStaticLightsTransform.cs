using System;
using System.Collections.Generic;

// Token: 0x0200008E RID: 142
public class BeatmapDataStaticLightsTransform
{
	// Token: 0x06000230 RID: 560 RVA: 0x0001B754 File Offset: 0x00019954
	public static BeatmapData CreateTransformedData(BeatmapData beatmapData)
	{
		BeatmapEventData[] beatmapEventData = beatmapData.beatmapEventData;
		List<BeatmapEventData> list = new List<BeatmapEventData>(beatmapEventData.Length);
		list.Add(new BeatmapEventData(0f, BeatmapEventType.Event0, 1));
		list.Add(new BeatmapEventData(0f, BeatmapEventType.Event4, 1));
		foreach (BeatmapEventData beatmapEventData2 in beatmapEventData)
		{
			if (beatmapEventData2.type.IsRotationEvent())
			{
				list.Add(beatmapEventData2);
			}
		}
		return new BeatmapData(beatmapData.GetBeatmapLineDataCopy(), list.ToArray());
	}
}
