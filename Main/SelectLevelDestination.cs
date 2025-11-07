using System;

// Token: 0x020000C1 RID: 193
public sealed class SelectLevelDestination : MenuDestination
{
	// Token: 0x060002AF RID: 687 RVA: 0x00003C55 File Offset: 0x00001E55
	public SelectLevelDestination(IBeatmapLevelPack beatmapLevelPack, IPreviewBeatmapLevel previewBeatmapLevel, BeatmapDifficulty beatmapDifficulty, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this.beatmapLevelPack = beatmapLevelPack;
		this.previewBeatmapLevel = previewBeatmapLevel;
		this.beatmapDifficulty = beatmapDifficulty;
		this.beatmapCharacteristic = beatmapCharacteristic;
	}

	// Token: 0x04000345 RID: 837
	public readonly IBeatmapLevelPack beatmapLevelPack;

	// Token: 0x04000346 RID: 838
	public readonly IPreviewBeatmapLevel previewBeatmapLevel;

	// Token: 0x04000347 RID: 839
	public readonly BeatmapDifficulty beatmapDifficulty;

	// Token: 0x04000348 RID: 840
	public readonly BeatmapCharacteristicSO beatmapCharacteristic;
}
