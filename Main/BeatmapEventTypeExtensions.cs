using System;

// Token: 0x02000100 RID: 256
public static class BeatmapEventTypeExtensions
{
	// Token: 0x060003E4 RID: 996 RVA: 0x000045E1 File Offset: 0x000027E1
	public static bool IsBPMChangeEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType == BeatmapEventType.Event10;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000045E8 File Offset: 0x000027E8
	public static bool IsRotationEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType == BeatmapEventType.Event14 || beatmapEventType == BeatmapEventType.Event15;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000045F6 File Offset: 0x000027F6
	public static bool IsEarlyRotationEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType == BeatmapEventType.Event14;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000045FD File Offset: 0x000027FD
	public static bool IsLateRotationEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType == BeatmapEventType.Event15;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00004604 File Offset: 0x00002804
	public static bool IsEarlyEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType == BeatmapEventType.Event10 || beatmapEventType == BeatmapEventType.Event14;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00004612 File Offset: 0x00002812
	public static bool IsSpawnAffectingEvent(this BeatmapEventType beatmapEventType)
	{
		return beatmapEventType.IsRotationEvent() || beatmapEventType.IsBPMChangeEvent();
	}

	// Token: 0x0400044F RID: 1103
	public const BeatmapEventType kEarlyRotationEvent = BeatmapEventType.Event14;

	// Token: 0x04000450 RID: 1104
	public const BeatmapEventType kLateRotationEvent = BeatmapEventType.Event15;

	// Token: 0x04000451 RID: 1105
	public const BeatmapEventType kBPMChangeEvent = BeatmapEventType.Event10;
}
