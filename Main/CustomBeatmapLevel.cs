using System;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class CustomBeatmapLevel : CustomPreviewBeatmapLevel, IBeatmapLevel, IPreviewBeatmapLevel
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x00005277 File Offset: 0x00003477
	public IBeatmapLevelData beatmapLevelData
	{
		get
		{
			return this._beatmapLevelData;
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00022E68 File Offset: 0x00021068
	public CustomBeatmapLevel(CustomPreviewBeatmapLevel customPreviewBeatmapLevel, AudioClip previewAudioClip, Texture2D coverImageTexture2D) : base(customPreviewBeatmapLevel.defaultCoverImageTexture2D, customPreviewBeatmapLevel.standardLevelInfoSaveData, customPreviewBeatmapLevel.customLevelPath, customPreviewBeatmapLevel.audioClipAsyncLoader, customPreviewBeatmapLevel.imageAsyncLoader, customPreviewBeatmapLevel.levelID, customPreviewBeatmapLevel.songName, customPreviewBeatmapLevel.songSubName, customPreviewBeatmapLevel.songAuthorName, customPreviewBeatmapLevel.levelAuthorName, customPreviewBeatmapLevel.beatsPerMinute, customPreviewBeatmapLevel.songTimeOffset, customPreviewBeatmapLevel.shuffle, customPreviewBeatmapLevel.shufflePeriod, customPreviewBeatmapLevel.previewStartTime, customPreviewBeatmapLevel.previewDuration, customPreviewBeatmapLevel.environmentInfo, customPreviewBeatmapLevel.allDirectionsEnvironmentInfo, customPreviewBeatmapLevel.previewDifficultyBeatmapSets)
	{
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0000527F File Offset: 0x0000347F
	public void SetBeatmapLevelData(BeatmapLevelData beatmapLevelData)
	{
		this._beatmapLevelData = beatmapLevelData;
	}

	// Token: 0x0400056B RID: 1387
	private BeatmapLevelData _beatmapLevelData;
}
