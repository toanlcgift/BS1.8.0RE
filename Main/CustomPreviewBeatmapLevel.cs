using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class CustomPreviewBeatmapLevel : IPreviewBeatmapLevel
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000525 RID: 1317 RVA: 0x00005288 File Offset: 0x00003488
	public StandardLevelInfoSaveData standardLevelInfoSaveData
	{
		get
		{
			return this._standardLevelInfoSaveData;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000526 RID: 1318 RVA: 0x00005290 File Offset: 0x00003490
	public string customLevelPath
	{
		get
		{
			return this._customLevelPath;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000527 RID: 1319 RVA: 0x00005298 File Offset: 0x00003498
	public IAudioClipAsyncLoader audioClipAsyncLoader
	{
		get
		{
			return this._audioClipAsyncLoader;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000528 RID: 1320 RVA: 0x000052A0 File Offset: 0x000034A0
	public IImageAsyncLoader imageAsyncLoader
	{
		get
		{
			return this._imageAsyncLoader;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000529 RID: 1321 RVA: 0x000052A8 File Offset: 0x000034A8
	public string levelID
	{
		get
		{
			return this._levelID;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x0600052A RID: 1322 RVA: 0x000052B0 File Offset: 0x000034B0
	public string songName
	{
		get
		{
			return this._songName;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x0600052B RID: 1323 RVA: 0x000052B8 File Offset: 0x000034B8
	public string songSubName
	{
		get
		{
			return this._songSubName;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x0600052C RID: 1324 RVA: 0x000052C0 File Offset: 0x000034C0
	public string songAuthorName
	{
		get
		{
			return this._songAuthorName;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600052D RID: 1325 RVA: 0x000052C8 File Offset: 0x000034C8
	public string levelAuthorName
	{
		get
		{
			return this._levelAuthorName;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x0600052E RID: 1326 RVA: 0x000052D0 File Offset: 0x000034D0
	public float beatsPerMinute
	{
		get
		{
			return this._beatsPerMinute;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x0600052F RID: 1327 RVA: 0x000052D8 File Offset: 0x000034D8
	public float songTimeOffset
	{
		get
		{
			return this._songTimeOffset;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x000052E0 File Offset: 0x000034E0
	public float songDuration
	{
		get
		{
			return this._songDuration;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000531 RID: 1329 RVA: 0x000052E8 File Offset: 0x000034E8
	public float shuffle
	{
		get
		{
			return this._shuffle;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x000052F0 File Offset: 0x000034F0
	public float shufflePeriod
	{
		get
		{
			return this._shufflePeriod;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000533 RID: 1331 RVA: 0x000052F8 File Offset: 0x000034F8
	public float previewStartTime
	{
		get
		{
			return this._previewStartTime;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x00005300 File Offset: 0x00003500
	public Texture2D defaultCoverImageTexture2D
	{
		get
		{
			return this._defaultCoverImageTexture2D;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x00005308 File Offset: 0x00003508
	public float previewDuration
	{
		get
		{
			return this._previewDuration;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x00005310 File Offset: 0x00003510
	public EnvironmentInfoSO environmentInfo
	{
		get
		{
			return this._environmentInfo;
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x00005318 File Offset: 0x00003518
	public EnvironmentInfoSO allDirectionsEnvironmentInfo
	{
		get
		{
			return this._allDirectionsEnvironmentInfo;
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00022EF0 File Offset: 0x000210F0
	public async Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken)
	{
		if (this._previewAudioClip == null && !string.IsNullOrEmpty(this.standardLevelInfoSaveData.songFilename))
		{
			string path = Path.Combine(this.customLevelPath, this.standardLevelInfoSaveData.songFilename);
			AudioClip previewAudioClip = await this._audioClipAsyncLoader.LoadAudioClipAsync(path, cancellationToken);
			this._previewAudioClip = previewAudioClip;
		}
		return this._previewAudioClip;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00022F40 File Offset: 0x00021140
	public async Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken)
	{
		if (this._coverImageTexture2D == null)
		{
			Texture2D texture2D = null;
			if (!string.IsNullOrEmpty(this.standardLevelInfoSaveData.coverImageFilename))
			{
				string path = Path.Combine(this.customLevelPath, this.standardLevelInfoSaveData.coverImageFilename);
				texture2D = await this._imageAsyncLoader.LoadImageAsync(path, cancellationToken);
			}
			this._coverImageTexture2D = ((texture2D != null) ? texture2D : this._defaultCoverImageTexture2D);
		}
		return this._coverImageTexture2D;
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x0600053A RID: 1338 RVA: 0x00005320 File Offset: 0x00003520
	public PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets
	{
		get
		{
			return this._previewDifficultyBeatmapSets;
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00022F90 File Offset: 0x00021190
	public CustomPreviewBeatmapLevel(Texture2D defaultCoverImageTexture2D, StandardLevelInfoSaveData standardLevelInfoSaveData, string customLevelPath, IAudioClipAsyncLoader audioClipAsyncLoader, IImageAsyncLoader imageAsyncLoader, string levelID, string songName, string songSubName, string songAuthorName, string levelAuthorName, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, EnvironmentInfoSO environmentInfo, EnvironmentInfoSO allDirectionsEnvironmentInfo, PreviewDifficultyBeatmapSet[] previewDifficultyBeatmapSets)
	{
		this._defaultCoverImageTexture2D = defaultCoverImageTexture2D;
		this._standardLevelInfoSaveData = standardLevelInfoSaveData;
		this._customLevelPath = customLevelPath;
		this._audioClipAsyncLoader = audioClipAsyncLoader;
		this._imageAsyncLoader = imageAsyncLoader;
		this._levelID = levelID;
		this._songName = songName;
		this._songSubName = songSubName;
		this._songAuthorName = songAuthorName;
		this._levelAuthorName = levelAuthorName;
		this._beatsPerMinute = beatsPerMinute;
		this._songTimeOffset = songTimeOffset;
		this._songDuration = this.songDuration;
		this._shuffle = shuffle;
		this._shufflePeriod = shufflePeriod;
		this._previewStartTime = previewStartTime;
		this._previewDuration = previewDuration;
		this._environmentInfo = environmentInfo;
		this._allDirectionsEnvironmentInfo = allDirectionsEnvironmentInfo;
		this._previewDifficultyBeatmapSets = previewDifficultyBeatmapSets;
	}

	// Token: 0x0400056C RID: 1388
	private StandardLevelInfoSaveData _standardLevelInfoSaveData;

	// Token: 0x0400056D RID: 1389
	private string _customLevelPath;

	// Token: 0x0400056E RID: 1390
	private IAudioClipAsyncLoader _audioClipAsyncLoader;

	// Token: 0x0400056F RID: 1391
	private IImageAsyncLoader _imageAsyncLoader;

	// Token: 0x04000570 RID: 1392
	private string _levelID;

	// Token: 0x04000571 RID: 1393
	private string _songName;

	// Token: 0x04000572 RID: 1394
	private string _songSubName;

	// Token: 0x04000573 RID: 1395
	private string _songAuthorName;

	// Token: 0x04000574 RID: 1396
	private string _levelAuthorName;

	// Token: 0x04000575 RID: 1397
	private AudioClip _previewAudioClip;

	// Token: 0x04000576 RID: 1398
	private float _beatsPerMinute;

	// Token: 0x04000577 RID: 1399
	private float _songTimeOffset;

	// Token: 0x04000578 RID: 1400
	private float _shuffle;

	// Token: 0x04000579 RID: 1401
	private float _shufflePeriod;

	// Token: 0x0400057A RID: 1402
	private float _previewStartTime;

	// Token: 0x0400057B RID: 1403
	private float _previewDuration;

	// Token: 0x0400057C RID: 1404
	private float _songDuration;

	// Token: 0x0400057D RID: 1405
	private Texture2D _defaultCoverImageTexture2D;

	// Token: 0x0400057E RID: 1406
	private Texture2D _coverImageTexture2D;

	// Token: 0x0400057F RID: 1407
	private EnvironmentInfoSO _environmentInfo;

	// Token: 0x04000580 RID: 1408
	private EnvironmentInfoSO _allDirectionsEnvironmentInfo;

	// Token: 0x04000581 RID: 1409
	private PreviewDifficultyBeatmapSet[] _previewDifficultyBeatmapSets;
}
