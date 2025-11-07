using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class BeatmapLevelDataSO : PersistentScriptableObject
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000497 RID: 1175 RVA: 0x00004D3B File Offset: 0x00002F3B
	public AudioClip audioClip
	{
		get
		{
			return this._audioClip;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000498 RID: 1176 RVA: 0x00004D43 File Offset: 0x00002F43
	public BeatmapLevelDataSO.DifficultyBeatmapSet[] difficultyBeatmapSets
	{
		get
		{
			return this._difficultyBeatmapSets;
		}
	}

	// Token: 0x040004E8 RID: 1256
	[SerializeField]
	private AudioClip _audioClip;

	// Token: 0x040004E9 RID: 1257
	[SerializeField]
	private BeatmapLevelDataSO.DifficultyBeatmapSet[] _difficultyBeatmapSets;

	// Token: 0x040004EA RID: 1258
	private BeatmapLevelDataSO.DifficultyBeatmapSet[] _no360MovementDifficultyBeatmapSets;

	// Token: 0x02000130 RID: 304
	[Serializable]
	public class DifficultyBeatmapSet
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00004D4B File Offset: 0x00002F4B
		public string beatmapCharacteristicSerializedName
		{
			get
			{
				return this._beatmapCharacteristicSerializedName;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00004D53 File Offset: 0x00002F53
		public BeatmapLevelSO.DifficultyBeatmap[] difficultyBeatmaps
		{
			get
			{
				return this._difficultyBeatmaps;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00004D5B File Offset: 0x00002F5B
		public DifficultyBeatmapSet(string beatmapCharacteristicSerializedName, BeatmapLevelSO.DifficultyBeatmap[] difficultyBeatmaps)
		{
			this._beatmapCharacteristicSerializedName = beatmapCharacteristicSerializedName;
			this._difficultyBeatmaps = difficultyBeatmaps;
		}

		// Token: 0x040004EB RID: 1259
		[SerializeField]
		private string _beatmapCharacteristicSerializedName;

		// Token: 0x040004EC RID: 1260
		[SerializeField]
		private BeatmapLevelSO.DifficultyBeatmap[] _difficultyBeatmaps;
	}
}
