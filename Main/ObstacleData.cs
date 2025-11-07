using System;

// Token: 0x02000110 RID: 272
public class ObstacleData : BeatmapObjectData
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x0600043C RID: 1084 RVA: 0x000049AC File Offset: 0x00002BAC
	// (set) Token: 0x0600043D RID: 1085 RVA: 0x000049B4 File Offset: 0x00002BB4
	public ObstacleType obstacleType { get; private set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600043E RID: 1086 RVA: 0x000049BD File Offset: 0x00002BBD
	// (set) Token: 0x0600043F RID: 1087 RVA: 0x000049C5 File Offset: 0x00002BC5
	public float duration { get; private set; }

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x000049CE File Offset: 0x00002BCE
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x000049D6 File Offset: 0x00002BD6
	public int width { get; private set; }

	// Token: 0x06000442 RID: 1090 RVA: 0x000049DF File Offset: 0x00002BDF
	public ObstacleData(int id, float time, int lineIndex, ObstacleType obstacleType, float duration, int width) : base(BeatmapObjectType.Obstacle, id, time, lineIndex)
	{
		this.obstacleType = obstacleType;
		this.duration = duration;
		this.width = width;
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00004A03 File Offset: 0x00002C03
	public void UpdateDuration(float duration)
	{
		this.duration = duration;
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00004A0C File Offset: 0x00002C0C
	public override BeatmapObjectData GetCopy()
	{
		return new ObstacleData(base.id, base.time, base.lineIndex, this.obstacleType, this.duration, this.width);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00004A37 File Offset: 0x00002C37
	public override void MirrorLineIndex(int lineCount)
	{
		base.lineIndex = lineCount - this.width - base.lineIndex;
	}
}
