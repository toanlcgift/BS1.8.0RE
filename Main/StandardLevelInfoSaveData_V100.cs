using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000173 RID: 371
public class StandardLevelInfoSaveData_V100
{
	// Token: 0x17000198 RID: 408
	// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000564A File Offset: 0x0000384A
	public string version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00005652 File Offset: 0x00003852
	public string songName
	{
		get
		{
			return this._songName;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000565A File Offset: 0x0000385A
	public string songSubName
	{
		get
		{
			return this._songSubName;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00005662 File Offset: 0x00003862
	public string songAuthorName
	{
		get
		{
			return this._songAuthorName;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000566A File Offset: 0x0000386A
	public string levelAuthorName
	{
		get
		{
			return this._levelAuthorName;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00005672 File Offset: 0x00003872
	public float beatsPerMinute
	{
		get
		{
			return this._beatsPerMinute;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000567A File Offset: 0x0000387A
	public float songTimeOffset
	{
		get
		{
			return this._songTimeOffset;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00005682 File Offset: 0x00003882
	public float shuffle
	{
		get
		{
			return this._shuffle;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000568A File Offset: 0x0000388A
	public float shufflePeriod
	{
		get
		{
			return this._shufflePeriod;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00005692 File Offset: 0x00003892
	public float previewStartTime
	{
		get
		{
			return this._previewStartTime;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000569A File Offset: 0x0000389A
	public float previewDuration
	{
		get
		{
			return this._previewDuration;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060005DB RID: 1499 RVA: 0x000056A2 File Offset: 0x000038A2
	public string songFilename
	{
		get
		{
			return this._songFilename;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x000056AA File Offset: 0x000038AA
	public string coverImageFilename
	{
		get
		{
			return this._coverImageFilename;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060005DD RID: 1501 RVA: 0x000056B2 File Offset: 0x000038B2
	public string environmentName
	{
		get
		{
			return this._environmentName;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x060005DE RID: 1502 RVA: 0x000056BA File Offset: 0x000038BA
	public StandardLevelInfoSaveData_V100.DifficultyBeatmap[] difficultyBeatmaps
	{
		get
		{
			return this._difficultyBeatmaps;
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00023E68 File Offset: 0x00022068
	public StandardLevelInfoSaveData_V100(string songName, string songSubName, string songAuthorName, string levelAuthorName, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, string songFilename, string coverImageFilename, string environmentName, StandardLevelInfoSaveData_V100.DifficultyBeatmap[] difficultyBeatmaps)
	{
		this._version = "1.0.0";
		this._songName = songName;
		this._songSubName = songSubName;
		this._songAuthorName = songAuthorName;
		this._levelAuthorName = levelAuthorName;
		this._beatsPerMinute = beatsPerMinute;
		this._songTimeOffset = songTimeOffset;
		this._shuffle = shuffle;
		this._shufflePeriod = shufflePeriod;
		this._previewStartTime = previewStartTime;
		this._previewDuration = previewDuration;
		this._songFilename = songFilename;
		this._coverImageFilename = coverImageFilename;
		this._environmentName = environmentName;
		this._difficultyBeatmaps = difficultyBeatmaps;
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00023EF4 File Offset: 0x000220F4
	public bool hasAllData
	{
		get
		{
			return this._version != null && this._songName != null && this._songSubName != null && this._songAuthorName != null && this._levelAuthorName != null && this._beatsPerMinute != 0f && this._songFilename != null && this._coverImageFilename != null && this._environmentName != null && this._difficultyBeatmaps != null;
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x000056C2 File Offset: 0x000038C2
	public void SetSongFilename(string songFilename)
	{
		this._songFilename = songFilename;
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x000046E7 File Offset: 0x000028E7
	public string SerializeToJSONString()
	{
		return JsonUtility.ToJson(this);
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x00023F6C File Offset: 0x0002216C
	public static StandardLevelInfoSaveData DeserializeFromJSONString(string stringData)
	{
		StandardLevelInfoSaveData_V100.VersionCheck versionCheck = null;
		try
		{
			versionCheck = JsonUtility.FromJson<StandardLevelInfoSaveData_V100.VersionCheck>(stringData);
		}
		catch
		{
		}
		if (versionCheck == null)
		{
			return null;
		}
		StandardLevelInfoSaveData result = null;
		if (versionCheck.version == "1.0.0")
		{
			try
			{
				result = JsonUtility.FromJson<StandardLevelInfoSaveData>(stringData);
			}
			catch
			{
			}
		}
		return result;
	}

	// Token: 0x040005F5 RID: 1525
	public const string kCurrentVersion = "1.0.0";

	// Token: 0x040005F6 RID: 1526
	[SerializeField]
	private string _version;

	// Token: 0x040005F7 RID: 1527
	[SerializeField]
	private string _songName;

	// Token: 0x040005F8 RID: 1528
	[SerializeField]
	private string _songSubName;

	// Token: 0x040005F9 RID: 1529
	[FormerlySerializedAs("_authorName")]
	[SerializeField]
	private string _songAuthorName;

	// Token: 0x040005FA RID: 1530
	[SerializeField]
	private string _levelAuthorName;

	// Token: 0x040005FB RID: 1531
	[SerializeField]
	private float _beatsPerMinute;

	// Token: 0x040005FC RID: 1532
	[SerializeField]
	private float _songTimeOffset;

	// Token: 0x040005FD RID: 1533
	[SerializeField]
	private float _shuffle;

	// Token: 0x040005FE RID: 1534
	[SerializeField]
	private float _shufflePeriod;

	// Token: 0x040005FF RID: 1535
	[SerializeField]
	private float _previewStartTime;

	// Token: 0x04000600 RID: 1536
	[SerializeField]
	private float _previewDuration;

	// Token: 0x04000601 RID: 1537
	[FormerlySerializedAs("_songFileName")]
	[SerializeField]
	private string _songFilename;

	// Token: 0x04000602 RID: 1538
	[FormerlySerializedAs("_coverImageFileName")]
	[SerializeField]
	private string _coverImageFilename;

	// Token: 0x04000603 RID: 1539
	[SerializeField]
	private string _environmentName;

	// Token: 0x04000604 RID: 1540
	[SerializeField]
	private StandardLevelInfoSaveData_V100.DifficultyBeatmap[] _difficultyBeatmaps;

	// Token: 0x02000174 RID: 372
	[Serializable]
	public class DifficultyBeatmap
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000056CB File Offset: 0x000038CB
		public string difficulty
		{
			get
			{
				return this._difficulty;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000056D3 File Offset: 0x000038D3
		public int difficultyRank
		{
			get
			{
				return this._difficultyRank;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000056DB File Offset: 0x000038DB
		public string beatmapFilename
		{
			get
			{
				return this._beatmapFilename;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000056E3 File Offset: 0x000038E3
		public float noteJumpMovementSpeed
		{
			get
			{
				return this._noteJumpMovementSpeed;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000056EB File Offset: 0x000038EB
		public int noteJumpStartBeatOffset
		{
			get
			{
				return this._noteJumpStartBeatOffset;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000056F3 File Offset: 0x000038F3
		public DifficultyBeatmap(string difficultyName, int difficultyRank, string beatmapFilename, float noteJumpMovementSpeed, int noteJumpStartBeatOffset)
		{
			this._difficulty = difficultyName;
			this._difficultyRank = difficultyRank;
			this._beatmapFilename = beatmapFilename;
			this._noteJumpMovementSpeed = noteJumpMovementSpeed;
			this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
		}

		// Token: 0x04000605 RID: 1541
		[SerializeField]
		private string _difficulty;

		// Token: 0x04000606 RID: 1542
		[SerializeField]
		private int _difficultyRank;

		// Token: 0x04000607 RID: 1543
		[SerializeField]
		private string _beatmapFilename;

		// Token: 0x04000608 RID: 1544
		[SerializeField]
		private float _noteJumpMovementSpeed;

		// Token: 0x04000609 RID: 1545
		[SerializeField]
		private int _noteJumpStartBeatOffset;
	}

	// Token: 0x02000175 RID: 373
	private class VersionCheck
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00005720 File Offset: 0x00003920
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0400060A RID: 1546
		[SerializeField]
		private string _version;
	}
}
