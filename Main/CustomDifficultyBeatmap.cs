using System;

// Token: 0x0200014D RID: 333
public class CustomDifficultyBeatmap : IDifficultyBeatmap
{
	// Token: 0x17000120 RID: 288
	// (get) Token: 0x0600051A RID: 1306 RVA: 0x00005202 File Offset: 0x00003402
	public IBeatmapLevel level
	{
		get
		{
			return this._level;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000520A File Offset: 0x0000340A
	public IDifficultyBeatmapSet parentDifficultyBeatmapSet
	{
		get
		{
			return this._parentDifficultyBeatmapSet;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600051C RID: 1308 RVA: 0x00005212 File Offset: 0x00003412
	public BeatmapDifficulty difficulty
	{
		get
		{
			return this._difficulty;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000521A File Offset: 0x0000341A
	public int difficultyRank
	{
		get
		{
			return this._difficultyRank;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x00005222 File Offset: 0x00003422
	public float noteJumpMovementSpeed
	{
		get
		{
			return this._noteJumpMovementSpeed;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000522A File Offset: 0x0000342A
	public float noteJumpStartBeatOffset
	{
		get
		{
			return this._noteJumpStartBeatOffset;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000520 RID: 1312 RVA: 0x00005232 File Offset: 0x00003432
	public BeatmapData beatmapData
	{
		get
		{
			return this._beatmapData;
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0000523A File Offset: 0x0000343A
	public CustomDifficultyBeatmap(IBeatmapLevel level, IDifficultyBeatmapSet parentDifficultyBeatmapSet, BeatmapDifficulty difficulty, int difficultyRank, float noteJumpMovementSpeed, float noteJumpStartBeatOffset, BeatmapData beatmapData)
	{
		this._level = level;
		this._parentDifficultyBeatmapSet = parentDifficultyBeatmapSet;
		this._difficulty = difficulty;
		this._difficultyRank = difficultyRank;
		this._noteJumpMovementSpeed = noteJumpMovementSpeed;
		this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
		this._beatmapData = beatmapData;
	}

	// Token: 0x04000564 RID: 1380
	private IBeatmapLevel _level;

	// Token: 0x04000565 RID: 1381
	private IDifficultyBeatmapSet _parentDifficultyBeatmapSet;

	// Token: 0x04000566 RID: 1382
	private BeatmapDifficulty _difficulty;

	// Token: 0x04000567 RID: 1383
	private int _difficultyRank;

	// Token: 0x04000568 RID: 1384
	private float _noteJumpMovementSpeed;

	// Token: 0x04000569 RID: 1385
	private float _noteJumpStartBeatOffset;

	// Token: 0x0400056A RID: 1386
	private BeatmapData _beatmapData;
}
