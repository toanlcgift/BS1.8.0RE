using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class PreviewBeatmapLevelSO : PersistentScriptableObject, IPreviewBeatmapLevel
{
	// Token: 0x1700015E RID: 350
	// (get) Token: 0x0600056B RID: 1387 RVA: 0x00005374 File Offset: 0x00003574
	public string levelID
	{
		get
		{
			return this._levelID;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000537C File Offset: 0x0000357C
	public string songName
	{
		get
		{
			return this._songName;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x0600056D RID: 1389 RVA: 0x00005384 File Offset: 0x00003584
	public string songSubName
	{
		get
		{
			return this._songSubName;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000538C File Offset: 0x0000358C
	public string songAuthorName
	{
		get
		{
			return this._songAuthorName;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x0600056F RID: 1391 RVA: 0x00005394 File Offset: 0x00003594
	public string levelAuthorName
	{
		get
		{
			return this._levelAuthorName;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000539C File Offset: 0x0000359C
	public float beatsPerMinute
	{
		get
		{
			return this._beatsPerMinute;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000571 RID: 1393 RVA: 0x000053A4 File Offset: 0x000035A4
	public float songTimeOffset
	{
		get
		{
			return this._songTimeOffset;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000572 RID: 1394 RVA: 0x000053AC File Offset: 0x000035AC
	public float songDuration
	{
		get
		{
			return this._songDuration;
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000573 RID: 1395 RVA: 0x000053B4 File Offset: 0x000035B4
	public float shuffle
	{
		get
		{
			return this._shuffle;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000574 RID: 1396 RVA: 0x000053BC File Offset: 0x000035BC
	public float shufflePeriod
	{
		get
		{
			return this._shufflePeriod;
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000575 RID: 1397 RVA: 0x000053C4 File Offset: 0x000035C4
	public float previewStartTime
	{
		get
		{
			return this._previewStartTime;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x000053CC File Offset: 0x000035CC
	public float previewDuration
	{
		get
		{
			return this._previewDuration;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x000053D4 File Offset: 0x000035D4
	public EnvironmentInfoSO environmentInfo
	{
		get
		{
			return this._environmentInfo;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000578 RID: 1400 RVA: 0x000053DC File Offset: 0x000035DC
	public EnvironmentInfoSO allDirectionsEnvironmentInfo
	{
		get
		{
			return this._allDirectionsEnvironmentInfo;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000579 RID: 1401 RVA: 0x000053E4 File Offset: 0x000035E4
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

	// Token: 0x0600057A RID: 1402 RVA: 0x000053FB File Offset: 0x000035FB
	protected override void OnEnable()
	{
		base.OnEnable();
		this.InitData();
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00005409 File Offset: 0x00003609
	private void InitData()
	{
		this._no360MovementPreviewDifficultyBeatmapSets = this._previewDifficultyBeatmapSets.GetPreviewDifficultyBeatmapSetWithout360Movement();
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x00023440 File Offset: 0x00021640
	public async Task<AudioClip> GetPreviewAudioClipAsync(CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return await Task.FromResult<AudioClip>(this._previewAudioClip);
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00023490 File Offset: 0x00021690
	public async Task<Texture2D> GetCoverImageTexture2DAsync(CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();
		return await Task.FromResult<Texture2D>(this._coverImageTexture2D);
	}

	// Token: 0x04000592 RID: 1426
	[SerializeField]
	private string _levelID;

	// Token: 0x04000593 RID: 1427
	[SerializeField]
	private string _songName;

	// Token: 0x04000594 RID: 1428
	[SerializeField]
	private string _songSubName;

	// Token: 0x04000595 RID: 1429
	[SerializeField]
	private string _songAuthorName;

	// Token: 0x04000596 RID: 1430
	[SerializeField]
	private string _levelAuthorName;

	// Token: 0x04000597 RID: 1431
	[SerializeField]
	private AudioClip _previewAudioClip;

	// Token: 0x04000598 RID: 1432
	[SerializeField]
	private float _beatsPerMinute;

	// Token: 0x04000599 RID: 1433
	[SerializeField]
	private float _songTimeOffset;

	// Token: 0x0400059A RID: 1434
	[SerializeField]
	private float _shuffle;

	// Token: 0x0400059B RID: 1435
	[SerializeField]
	private float _shufflePeriod;

	// Token: 0x0400059C RID: 1436
	[SerializeField]
	private float _previewStartTime;

	// Token: 0x0400059D RID: 1437
	[SerializeField]
	private float _previewDuration;

	// Token: 0x0400059E RID: 1438
	[SerializeField]
	private float _songDuration;

	// Token: 0x0400059F RID: 1439
	[SerializeField]
	private Texture2D _coverImageTexture2D;

	// Token: 0x040005A0 RID: 1440
	[SerializeField]
	private EnvironmentInfoSO _environmentInfo;

	// Token: 0x040005A1 RID: 1441
	[SerializeField]
	private EnvironmentInfoSO _allDirectionsEnvironmentInfo;

	// Token: 0x040005A2 RID: 1442
	[SerializeField]
	private PreviewDifficultyBeatmapSet[] _previewDifficultyBeatmapSets;

	// Token: 0x040005A3 RID: 1443
	public bool _ignore360MovementBeatmaps;

	// Token: 0x040005A4 RID: 1444
	private PreviewDifficultyBeatmapSet[] _no360MovementPreviewDifficultyBeatmapSets;
}
