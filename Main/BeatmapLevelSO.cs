using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class BeatmapLevelSO : PersistentScriptableObject, IBeatmapLevel, IPreviewBeatmapLevel
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00004F01 File Offset: 0x00003101
	public string levelID
	{
		get
		{
			return this._levelID;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00004F09 File Offset: 0x00003109
	public string songName
	{
		get
		{
			return this._songName;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00004F11 File Offset: 0x00003111
	public string songSubName
	{
		get
		{
			return this._songSubName;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00004F19 File Offset: 0x00003119
	public string songAuthorName
	{
		get
		{
			return this._songAuthorName;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00004F21 File Offset: 0x00003121
	public string levelAuthorName
	{
		get
		{
			return this._levelAuthorName;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00004F29 File Offset: 0x00003129
	public float beatsPerMinute
	{
		get
		{
			return this._beatsPerMinute;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00004F31 File Offset: 0x00003131
	public float songTimeOffset
	{
		get
		{
			return this._songTimeOffset;
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060004CA RID: 1226 RVA: 0x00004F39 File Offset: 0x00003139
	public float shuffle
	{
		get
		{
			return this._shuffle;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x00004F41 File Offset: 0x00003141
	public float shufflePeriod
	{
		get
		{
			return this._shufflePeriod;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x00004F49 File Offset: 0x00003149
	public AudioClip previewAudioClip
	{
		get
		{
			return this._audioClip;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060004CD RID: 1229 RVA: 0x00004F51 File Offset: 0x00003151
	public float previewStartTime
	{
		get
		{
			return this._previewStartTime;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x00004F59 File Offset: 0x00003159
	public float previewDuration
	{
		get
		{
			return this._previewDuration;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060004CF RID: 1231 RVA: 0x00004F61 File Offset: 0x00003161
	public Texture2D coverImageTexture2D
	{
		get
		{
			return this._coverImageTexture2D;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00004F69 File Offset: 0x00003169
	public EnvironmentInfoSO environmentInfo
	{
		get
		{
			return this._environmentInfo;
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00004F71 File Offset: 0x00003171
	public EnvironmentInfoSO allDirectionsEnvironmentInfo
	{
		get
		{
			return this._allDirectionsEnvironmentInfo;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00022030 File Offset: 0x00020230
	public IDifficultyBeatmapSet[] difficultyBeatmapSets
	{
		get
		{
			return this._ignore360MovementBeatmaps ? this._no360MovementDifficultyBeatmapSets : this._difficultyBeatmapSets;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00004F79 File Offset: 0x00003179
	public float songDuration
	{
		get
		{
			return this._audioClip.length;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00004F86 File Offset: 0x00003186
	public PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets
	{
		get
		{
			if (!this._ignore360MovementBeatmaps)
			{
				return this._previewDifficultyBeatmapSets;
			}
			return this._no360MovementPreviewDifficultyBeatmapSets;
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00004F9D File Offset: 0x0000319D
	protected override void OnEnable()
	{
		base.OnEnable();
		this.InitData();
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00022058 File Offset: 0x00020258
	public void InitFull(string levelID, string songName, string songSubName, string songAuthorName, string levelAuthorName, AudioClip audioClip, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, Texture2D coverImageTexture2D, EnvironmentInfoSO environmentInfo, EnvironmentInfoSO allDirectionsEnvironmentInfo, BeatmapLevelSO.DifficultyBeatmapSet[] difficultyBeatmapSets)
	{
		this._levelID = levelID;
		this._songName = songName;
		this._songSubName = songSubName;
		this._songAuthorName = songAuthorName;
		this._levelAuthorName = levelAuthorName;
		this._audioClip = audioClip;
		this._beatsPerMinute = beatsPerMinute;
		this._songTimeOffset = songTimeOffset;
		this._shuffle = shuffle;
		this._shufflePeriod = shufflePeriod;
		this._previewStartTime = previewStartTime;
		this._previewDuration = previewDuration;
		this._coverImageTexture2D = coverImageTexture2D;
		this._environmentInfo = environmentInfo;
		this._allDirectionsEnvironmentInfo = allDirectionsEnvironmentInfo;
		this._difficultyBeatmapSets = difficultyBeatmapSets;
		this.InitData();
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000220E8 File Offset: 0x000202E8
	private void InitData()
	{
		if (this._difficultyBeatmapSets == null)
		{
			return;
		}
		BeatmapLevelSO.DifficultyBeatmapSet[] difficultyBeatmapSets = this._difficultyBeatmapSets;
		for (int i = 0; i < difficultyBeatmapSets.Length; i++)
		{
			difficultyBeatmapSets[i].SetParentLevel(this);
		}
		this._previewDifficultyBeatmapSets = this._difficultyBeatmapSets.GetPreviewDifficultyBeatmapSets<BeatmapLevelSO.DifficultyBeatmapSet>();
		this._no360MovementPreviewDifficultyBeatmapSets = this._previewDifficultyBeatmapSets.GetPreviewDifficultyBeatmapSetWithout360Movement();
		this._no360MovementDifficultyBeatmapSets = this._difficultyBeatmapSets.GetDifficultyBeatmapSetsWithout360Movement<BeatmapLevelSO.DifficultyBeatmapSet>();
		this._beatmapLevelData = new BeatmapLevelData(this._audioClip, this.difficultyBeatmapSets);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00022168 File Offset: 0x00020368
	public async Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return await Task.FromResult<AudioClip>(this._audioClip);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x000221B8 File Offset: 0x000203B8
	public async Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return await Task.FromResult<Texture2D>(this._coverImageTexture2D);
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060004DA RID: 1242 RVA: 0x00022208 File Offset: 0x00020408
	public IBeatmapLevelData beatmapLevelData
	{
		get
		{
			if (this._beatmapLevelData == null)
			{
				AudioClip audioClip = this._audioClip;
				IDifficultyBeatmapSet[] difficultyBeatmapSets = this._difficultyBeatmapSets;
				this._beatmapLevelData = new BeatmapLevelData(audioClip, difficultyBeatmapSets);
			}
			return this._beatmapLevelData;
		}
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0002223C File Offset: 0x0002043C
	public async Task<BeatmapLevelSO.GetBeatmapLevelDataResult> GetBeatmapLevelDataAsync(CancellationToken token)
	{
		if (this._beatmapLevelData == null)
		{
			AudioClip audioClip = this._audioClip;
			IDifficultyBeatmapSet[] difficultyBeatmapSets = this._difficultyBeatmapSets;
			this._beatmapLevelData = new BeatmapLevelData(audioClip, difficultyBeatmapSets);
			this._getBeatmapLevelDataResult = new BeatmapLevelSO.GetBeatmapLevelDataResult(BeatmapLevelSO.GetBeatmapLevelDataResult.Result.OK, this._beatmapLevelData);
		}
		return await Task.FromResult<BeatmapLevelSO.GetBeatmapLevelDataResult>(this._getBeatmapLevelDataResult);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00022284 File Offset: 0x00020484
	public IDifficultyBeatmap GetDifficultyBeatmap(BeatmapCharacteristicSO characteristic, BeatmapDifficulty difficulty)
	{
		foreach (IDifficultyBeatmapSet difficultyBeatmapSet in this.difficultyBeatmapSets)
		{
			if (difficultyBeatmapSet.beatmapCharacteristic == characteristic)
			{
				foreach (IDifficultyBeatmap difficultyBeatmap in difficultyBeatmapSet.difficultyBeatmaps)
				{
					if (difficultyBeatmap.difficulty == difficulty)
					{
						return difficultyBeatmap;
					}
				}
				return null;
			}
		}
		return null;
	}

	// Token: 0x04000504 RID: 1284
	[SerializeField]
	private string _levelID;

	// Token: 0x04000505 RID: 1285
	[SerializeField]
	private string _songName;

	// Token: 0x04000506 RID: 1286
	[SerializeField]
	private string _songSubName;

	// Token: 0x04000507 RID: 1287
	[SerializeField]
	private string _songAuthorName;

	// Token: 0x04000508 RID: 1288
	[SerializeField]
	private string _levelAuthorName;

	// Token: 0x04000509 RID: 1289
	[SerializeField]
	private AudioClip _audioClip;

	// Token: 0x0400050A RID: 1290
	[SerializeField]
	private float _beatsPerMinute;

	// Token: 0x0400050B RID: 1291
	[SerializeField]
	private float _songTimeOffset;

	// Token: 0x0400050C RID: 1292
	[SerializeField]
	private float _shuffle;

	// Token: 0x0400050D RID: 1293
	[SerializeField]
	private float _shufflePeriod;

	// Token: 0x0400050E RID: 1294
	[SerializeField]
	private float _previewStartTime;

	// Token: 0x0400050F RID: 1295
	[SerializeField]
	private float _previewDuration;

	// Token: 0x04000510 RID: 1296
	[SerializeField]
	private Texture2D _coverImageTexture2D;

	// Token: 0x04000511 RID: 1297
	[SerializeField]
	private EnvironmentInfoSO _environmentInfo;

	// Token: 0x04000512 RID: 1298
	[SerializeField]
	private EnvironmentInfoSO _allDirectionsEnvironmentInfo;

	// Token: 0x04000513 RID: 1299
	[SerializeField]
	private BeatmapLevelSO.DifficultyBeatmapSet[] _difficultyBeatmapSets;

	// Token: 0x04000514 RID: 1300
	public bool _ignore360MovementBeatmaps;

	// Token: 0x04000515 RID: 1301
	private BeatmapLevelSO.DifficultyBeatmapSet[] _no360MovementDifficultyBeatmapSets;

	// Token: 0x04000516 RID: 1302
	private PreviewDifficultyBeatmapSet[] _previewDifficultyBeatmapSets;

	// Token: 0x04000517 RID: 1303
	private PreviewDifficultyBeatmapSet[] _no360MovementPreviewDifficultyBeatmapSets;

	// Token: 0x04000518 RID: 1304
	private IBeatmapLevelData _beatmapLevelData;

	// Token: 0x04000519 RID: 1305
	private BeatmapLevelSO.GetBeatmapLevelDataResult _getBeatmapLevelDataResult;

	// Token: 0x0200013D RID: 317
	[Serializable]
	public class DifficultyBeatmapSet : IDifficultyBeatmapSet
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00004FAB File Offset: 0x000031AB
		public BeatmapCharacteristicSO beatmapCharacteristic
		{
			get
			{
				return this._beatmapCharacteristic;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000222E8 File Offset: 0x000204E8
		public IDifficultyBeatmap[] difficultyBeatmaps
		{
			get
			{
				return this._difficultyBeatmaps;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00004FB3 File Offset: 0x000031B3
		public DifficultyBeatmapSet(BeatmapCharacteristicSO beatmapCharacteristic, BeatmapLevelSO.DifficultyBeatmap[] difficultyBeatmaps)
		{
			this._beatmapCharacteristic = beatmapCharacteristic;
			this._difficultyBeatmaps = difficultyBeatmaps;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00022300 File Offset: 0x00020500
		public void SetParentLevel(IBeatmapLevel level)
		{
			BeatmapLevelSO.DifficultyBeatmap[] difficultyBeatmaps = this._difficultyBeatmaps;
			for (int i = 0; i < difficultyBeatmaps.Length; i++)
			{
				difficultyBeatmaps[i].SetParents(level, this);
			}
		}

		// Token: 0x0400051A RID: 1306
		[SerializeField]
		private BeatmapCharacteristicSO _beatmapCharacteristic;

		// Token: 0x0400051B RID: 1307
		[SerializeField]
		private BeatmapLevelSO.DifficultyBeatmap[] _difficultyBeatmaps;
	}

	// Token: 0x0200013E RID: 318
	[Serializable]
	public class DifficultyBeatmap : IDifficultyBeatmap
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00004FC9 File Offset: 0x000031C9
		public BeatmapDifficulty difficulty
		{
			get
			{
				return this._difficulty;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00004FD1 File Offset: 0x000031D1
		public int difficultyRank
		{
			get
			{
				return this._difficultyRank;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00004FD9 File Offset: 0x000031D9
		public float noteJumpMovementSpeed
		{
			get
			{
				return this._noteJumpMovementSpeed;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00004FE1 File Offset: 0x000031E1
		public float noteJumpStartBeatOffset
		{
			get
			{
				return this._noteJumpStartBeatOffset;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00004FE9 File Offset: 0x000031E9
		public BeatmapData beatmapData
		{
			get
			{
				return this._beatmapData.beatmapData;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00004FF6 File Offset: 0x000031F6
		public IBeatmapLevel level
		{
			get
			{
				return this._parentLevel;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00004FFE File Offset: 0x000031FE
		public IDifficultyBeatmapSet parentDifficultyBeatmapSet
		{
			get
			{
				return this._parentDifficultyBeatmapSet;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002232C File Offset: 0x0002052C
		public void SetParents(IBeatmapLevel parentLevel, IDifficultyBeatmapSet parentDifficultyBeatmapSet)
		{
			this._parentLevel = parentLevel;
			this._parentDifficultyBeatmapSet = parentDifficultyBeatmapSet;
			if (this._difficulty == BeatmapDifficulty.ExpertPlus)
			{
				this._beatmapData.SetRequiredDataForLoad(this._parentLevel.beatsPerMinute, 0f, 0f);
				return;
			}
			this._beatmapData.SetRequiredDataForLoad(this._parentLevel.beatsPerMinute, parentLevel.shuffle, parentLevel.shufflePeriod);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00022394 File Offset: 0x00020594
		public DifficultyBeatmap(IBeatmapLevel parentLevel, BeatmapDifficulty difficulty, int difficultyRank, float noteJumpMovementSpeed, float noteJumpStartBeatOffset, BeatmapDataSO beatmapData)
		{
			this._parentLevel = parentLevel;
			this._difficulty = difficulty;
			this._difficultyRank = difficultyRank;
			this._noteJumpMovementSpeed = noteJumpMovementSpeed;
			this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
			this._beatmapData = beatmapData;
			if (this._difficulty == BeatmapDifficulty.ExpertPlus)
			{
				this._beatmapData.SetRequiredDataForLoad(this._parentLevel.beatsPerMinute, 0f, 0f);
				return;
			}
			this._beatmapData.SetRequiredDataForLoad(this._parentLevel.beatsPerMinute, parentLevel.shuffle, parentLevel.shufflePeriod);
		}

		// Token: 0x0400051C RID: 1308
		[SerializeField]
		private BeatmapDifficulty _difficulty;

		// Token: 0x0400051D RID: 1309
		[SerializeField]
		private int _difficultyRank;

		// Token: 0x0400051E RID: 1310
		[SerializeField]
		private float _noteJumpMovementSpeed;

		// Token: 0x0400051F RID: 1311
		[SerializeField]
		private float _noteJumpStartBeatOffset;

		// Token: 0x04000520 RID: 1312
		[SerializeField]
		private BeatmapDataSO _beatmapData;

		// Token: 0x04000521 RID: 1313
		private IBeatmapLevel _parentLevel;

		// Token: 0x04000522 RID: 1314
		private IDifficultyBeatmapSet _parentDifficultyBeatmapSet;
	}

	// Token: 0x0200013F RID: 319
	public struct GetBeatmapLevelDataResult
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00005006 File Offset: 0x00003206
		public GetBeatmapLevelDataResult(BeatmapLevelSO.GetBeatmapLevelDataResult.Result result, IBeatmapLevelData beatmapLevelData)
		{
			this.result = result;
			this.beatmapLevelData = beatmapLevelData;
		}

		// Token: 0x04000523 RID: 1315
		public readonly BeatmapLevelSO.GetBeatmapLevelDataResult.Result result;

		// Token: 0x04000524 RID: 1316
		public readonly IBeatmapLevelData beatmapLevelData;

		// Token: 0x02000140 RID: 320
		public enum Result
		{
			// Token: 0x04000526 RID: 1318
			OK,
			// Token: 0x04000527 RID: 1319
			NotOwned,
			// Token: 0x04000528 RID: 1320
			Fail
		}
	}
}
