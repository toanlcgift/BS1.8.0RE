using System;

// Token: 0x02000138 RID: 312
public class BeatmapLevelPackCollection : IBeatmapLevelPackCollection
{
	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00004E9C File Offset: 0x0000309C
	public IBeatmapLevelPack[] beatmapLevelPacks
	{
		get
		{
			return this._beatmapLevelPacks;
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00004EA4 File Offset: 0x000030A4
	public BeatmapLevelPackCollection(IBeatmapLevelPack[] beatmapLevelPacks)
	{
		this._beatmapLevelPacks = beatmapLevelPacks;
	}

	// Token: 0x040004FA RID: 1274
	private IBeatmapLevelPack[] _beatmapLevelPacks;
}
