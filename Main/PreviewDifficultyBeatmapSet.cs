using System;
using UnityEngine;

// Token: 0x0200015F RID: 351
[Serializable]
public class PreviewDifficultyBeatmapSet
{
	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06000583 RID: 1411 RVA: 0x00005438 File Offset: 0x00003638
	public BeatmapCharacteristicSO beatmapCharacteristic
	{
		get
		{
			return this._beatmapCharacteristic;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000584 RID: 1412 RVA: 0x00005440 File Offset: 0x00003640
	public BeatmapDifficulty[] beatmapDifficulties
	{
		get
		{
			return this._beatmapDifficulties;
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00005448 File Offset: 0x00003648
	public PreviewDifficultyBeatmapSet(BeatmapCharacteristicSO beatmapCharacteristic, BeatmapDifficulty[] beatmapDifficulties)
	{
		this._beatmapCharacteristic = beatmapCharacteristic;
		this._beatmapDifficulties = beatmapDifficulties;
	}

	// Token: 0x040005AF RID: 1455
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x040005B0 RID: 1456
	[SerializeField]
	private BeatmapDifficulty[] _beatmapDifficulties;
}
