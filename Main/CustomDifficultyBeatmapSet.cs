using System;

// Token: 0x0200014C RID: 332
public class CustomDifficultyBeatmapSet : IDifficultyBeatmapSet
{
	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x000051E2 File Offset: 0x000033E2
	public BeatmapCharacteristicSO beatmapCharacteristic
	{
		get
		{
			return this._beatmapCharacteristic;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000517 RID: 1303 RVA: 0x00022E50 File Offset: 0x00021050
	public IDifficultyBeatmap[] difficultyBeatmaps
	{
		get
		{
			return this._difficultyBeatmaps;
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x000051EA File Offset: 0x000033EA
	public CustomDifficultyBeatmapSet(BeatmapCharacteristicSO beatmapCharacteristic)
	{
		this._beatmapCharacteristic = beatmapCharacteristic;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000051F9 File Offset: 0x000033F9
	public void SetCustomDifficultyBeatmaps(CustomDifficultyBeatmap[] difficultyBeatmaps)
	{
		this._difficultyBeatmaps = difficultyBeatmaps;
	}

	// Token: 0x04000562 RID: 1378
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x04000563 RID: 1379
	private CustomDifficultyBeatmap[] _difficultyBeatmaps;
}
