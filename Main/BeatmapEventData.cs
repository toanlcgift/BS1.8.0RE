using System;

// Token: 0x020000FE RID: 254
public class BeatmapEventData
{
	// Token: 0x060003E3 RID: 995 RVA: 0x000045C4 File Offset: 0x000027C4
	public BeatmapEventData(float time, BeatmapEventType type, int value)
	{
		this.time = time;
		this.type = type;
		this.value = value;
	}

	// Token: 0x0400043A RID: 1082
	public readonly BeatmapEventType type;

	// Token: 0x0400043B RID: 1083
	public readonly float time;

	// Token: 0x0400043C RID: 1084
	public readonly int value;
}
