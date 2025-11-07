using System;

// Token: 0x020000C0 RID: 192
public sealed class SelectLevelPackDestination : MenuDestination
{
	// Token: 0x060002AE RID: 686 RVA: 0x00003C46 File Offset: 0x00001E46
	public SelectLevelPackDestination(IBeatmapLevelPack beatmapLevelPack)
	{
		this.beatmapLevelPack = beatmapLevelPack;
	}

	// Token: 0x04000344 RID: 836
	public readonly IBeatmapLevelPack beatmapLevelPack;
}
