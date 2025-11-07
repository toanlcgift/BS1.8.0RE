using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class BeatmapLevelData : IBeatmapLevelData
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000494 RID: 1172 RVA: 0x00004D15 File Offset: 0x00002F15
	public AudioClip audioClip
	{
		get
		{
			return this._audioClip;
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000495 RID: 1173 RVA: 0x00004D1D File Offset: 0x00002F1D
	public IDifficultyBeatmapSet[] difficultyBeatmapSets
	{
		get
		{
			return this._difficultyBeatmapSets;
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00004D25 File Offset: 0x00002F25
	public BeatmapLevelData(AudioClip audioClip, IDifficultyBeatmapSet[] difficultyBeatmapSets)
	{
		this._audioClip = audioClip;
		this._difficultyBeatmapSets = difficultyBeatmapSets;
	}

	// Token: 0x040004E6 RID: 1254
	private AudioClip _audioClip;

	// Token: 0x040004E7 RID: 1255
	private IDifficultyBeatmapSet[] _difficultyBeatmapSets;
}
