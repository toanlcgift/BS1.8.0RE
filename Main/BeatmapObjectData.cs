using System;

// Token: 0x02000102 RID: 258
public abstract class BeatmapObjectData
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004624 File Offset: 0x00002824
	// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000462C File Offset: 0x0000282C
	public BeatmapObjectType beatmapObjectType { get; private set; }

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060003ED RID: 1005 RVA: 0x00004635 File Offset: 0x00002835
	// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000463D File Offset: 0x0000283D
	public float time { get; private set; }

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060003EF RID: 1007 RVA: 0x00004646 File Offset: 0x00002846
	// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000464E File Offset: 0x0000284E
	public int lineIndex { get; protected set; }

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00004657 File Offset: 0x00002857
	// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000465F File Offset: 0x0000285F
	public int id { get; private set; }

	// Token: 0x060003F3 RID: 1011 RVA: 0x00004668 File Offset: 0x00002868
	public BeatmapObjectData(BeatmapObjectType beatmapObjectType, int id, float time, int lineIndex)
	{
		this.beatmapObjectType = beatmapObjectType;
		this.id = id;
		this.time = time;
		this.lineIndex = lineIndex;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0000468D File Offset: 0x0000288D
	public virtual void MirrorLineIndex(int lineCount)
	{
		this.lineIndex = lineCount - 1 - this.lineIndex;
	}

	// Token: 0x060003F5 RID: 1013
	public abstract BeatmapObjectData GetCopy();
}
