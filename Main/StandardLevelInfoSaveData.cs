using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200016F RID: 367
public class StandardLevelInfoSaveData
{
	// Token: 0x1700017F RID: 383
	// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000553E File Offset: 0x0000373E
	public string version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00005546 File Offset: 0x00003746
	public string songName
	{
		get
		{
			return this._songName;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000554E File Offset: 0x0000374E
	public string songSubName
	{
		get
		{
			return this._songSubName;
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00005556 File Offset: 0x00003756
	public string songAuthorName
	{
		get
		{
			return this._songAuthorName;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000555E File Offset: 0x0000375E
	public string levelAuthorName
	{
		get
		{
			return this._levelAuthorName;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00005566 File Offset: 0x00003766
	public float beatsPerMinute
	{
		get
		{
			return this._beatsPerMinute;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000556E File Offset: 0x0000376E
	public float songTimeOffset
	{
		get
		{
			return this._songTimeOffset;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00005576 File Offset: 0x00003776
	public float shuffle
	{
		get
		{
			return this._shuffle;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000557E File Offset: 0x0000377E
	public float shufflePeriod
	{
		get
		{
			return this._shufflePeriod;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00005586 File Offset: 0x00003786
	public float previewStartTime
	{
		get
		{
			return this._previewStartTime;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000558E File Offset: 0x0000378E
	public float previewDuration
	{
		get
		{
			return this._previewDuration;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060005BB RID: 1467 RVA: 0x00005596 File Offset: 0x00003796
	public string songFilename
	{
		get
		{
			return this._songFilename;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000559E File Offset: 0x0000379E
	public string coverImageFilename
	{
		get
		{
			return this._coverImageFilename;
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060005BD RID: 1469 RVA: 0x000055A6 File Offset: 0x000037A6
	public string environmentName
	{
		get
		{
			return this._environmentName;
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x060005BE RID: 1470 RVA: 0x000055AE File Offset: 0x000037AE
	public string allDirectionsEnvironmentName
	{
		get
		{
			return this._allDirectionsEnvironmentName;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x060005BF RID: 1471 RVA: 0x000055B6 File Offset: 0x000037B6
	public StandardLevelInfoSaveData.DifficultyBeatmapSet[] difficultyBeatmapSets
	{
		get
		{
			return this._difficultyBeatmapSets;
		}
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00023BF0 File Offset: 0x00021DF0
	public StandardLevelInfoSaveData(string songName, string songSubName, string songAuthorName, string levelAuthorName, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, string songFilename, string coverImageFilename, string environmentName, string allDirectionsEnvironmentName, StandardLevelInfoSaveData.DifficultyBeatmapSet[] difficultyBeatmapSets)
	{
		this._version = "2.0.0";
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
		this._allDirectionsEnvironmentName = allDirectionsEnvironmentName;
		this._difficultyBeatmapSets = difficultyBeatmapSets;
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00023C84 File Offset: 0x00021E84
	public bool hasAllData
	{
		get
		{
			return this._version != null && this._songName != null && this._songSubName != null && this._songAuthorName != null && this._levelAuthorName != null && this._beatsPerMinute != 0f && this._songFilename != null && this._coverImageFilename != null && this._environmentName != null && this._difficultyBeatmapSets != null;
		}
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x000055BE File Offset: 0x000037BE
	public void SetSongFilename(string songFilename)
	{
		this._songFilename = songFilename;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x000046E7 File Offset: 0x000028E7
	public string SerializeToJSONString()
	{
		return JsonUtility.ToJson(this);
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00023CFC File Offset: 0x00021EFC
	public static StandardLevelInfoSaveData DeserializeFromJSONString(string stringData)
	{
		StandardLevelInfoSaveData.VersionCheck versionCheck = null;
		try
		{
			versionCheck = JsonUtility.FromJson<StandardLevelInfoSaveData.VersionCheck>(stringData);
		}
		catch
		{
		}
		if (versionCheck == null)
		{
			return null;
		}
		StandardLevelInfoSaveData result = null;
		if (versionCheck.version == "2.0.0")
		{
			try
			{
				return JsonUtility.FromJson<StandardLevelInfoSaveData>(stringData);
			}
			catch
			{
				return null;
			}
		}
		if (versionCheck.version == "1.0.0")
		{
			StandardLevelInfoSaveData_V100 standardLevelInfoSaveData_V = JsonUtility.FromJson<StandardLevelInfoSaveData_V100>(stringData);
			if (standardLevelInfoSaveData_V != null)
			{
				StandardLevelInfoSaveData.DifficultyBeatmapSet[] array = new StandardLevelInfoSaveData.DifficultyBeatmapSet[1];
				StandardLevelInfoSaveData.DifficultyBeatmap[] array2 = new StandardLevelInfoSaveData.DifficultyBeatmap[standardLevelInfoSaveData_V.difficultyBeatmaps.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = new StandardLevelInfoSaveData.DifficultyBeatmap(standardLevelInfoSaveData_V.difficultyBeatmaps[i].difficulty, standardLevelInfoSaveData_V.difficultyBeatmaps[i].difficultyRank, standardLevelInfoSaveData_V.difficultyBeatmaps[i].beatmapFilename, standardLevelInfoSaveData_V.difficultyBeatmaps[i].noteJumpMovementSpeed, (float)standardLevelInfoSaveData_V.difficultyBeatmaps[i].noteJumpStartBeatOffset);
				}
				array[0] = new StandardLevelInfoSaveData.DifficultyBeatmapSet("Standard", array2);
				result = new StandardLevelInfoSaveData(standardLevelInfoSaveData_V.songName, standardLevelInfoSaveData_V.songSubName, standardLevelInfoSaveData_V.songAuthorName, standardLevelInfoSaveData_V.levelAuthorName, standardLevelInfoSaveData_V.beatsPerMinute, standardLevelInfoSaveData_V.songTimeOffset, standardLevelInfoSaveData_V.shuffle, standardLevelInfoSaveData_V.shufflePeriod, standardLevelInfoSaveData_V.previewStartTime, standardLevelInfoSaveData_V.previewDuration, standardLevelInfoSaveData_V.songFilename, standardLevelInfoSaveData_V.coverImageFilename, standardLevelInfoSaveData_V.environmentName, null, array);
			}
		}
		return result;
	}

	// Token: 0x040005DB RID: 1499
	private const string kCurrentVersion = "2.0.0";

	// Token: 0x040005DC RID: 1500
	private const string kDefaultBeatmapCharacteristicName = "Standard";

	// Token: 0x040005DD RID: 1501
	[SerializeField]
	private string _version;

	// Token: 0x040005DE RID: 1502
	[SerializeField]
	private string _songName;

	// Token: 0x040005DF RID: 1503
	[SerializeField]
	private string _songSubName;

	// Token: 0x040005E0 RID: 1504
	[FormerlySerializedAs("_authorName")]
	[SerializeField]
	private string _songAuthorName;

	// Token: 0x040005E1 RID: 1505
	[SerializeField]
	private string _levelAuthorName;

	// Token: 0x040005E2 RID: 1506
	[SerializeField]
	private float _beatsPerMinute;

	// Token: 0x040005E3 RID: 1507
	[SerializeField]
	private float _songTimeOffset;

	// Token: 0x040005E4 RID: 1508
	[SerializeField]
	private float _shuffle;

	// Token: 0x040005E5 RID: 1509
	[SerializeField]
	private float _shufflePeriod;

	// Token: 0x040005E6 RID: 1510
	[SerializeField]
	private float _previewStartTime;

	// Token: 0x040005E7 RID: 1511
	[SerializeField]
	private float _previewDuration;

	// Token: 0x040005E8 RID: 1512
	[FormerlySerializedAs("_songFileName")]
	[SerializeField]
	private string _songFilename;

	// Token: 0x040005E9 RID: 1513
	[FormerlySerializedAs("_coverImageFileName")]
	[SerializeField]
	private string _coverImageFilename;

	// Token: 0x040005EA RID: 1514
	[SerializeField]
	private string _environmentName;

	// Token: 0x040005EB RID: 1515
	[SerializeField]
	private string _allDirectionsEnvironmentName;

	// Token: 0x040005EC RID: 1516
	[SerializeField]
	private StandardLevelInfoSaveData.DifficultyBeatmapSet[] _difficultyBeatmapSets;

	// Token: 0x02000170 RID: 368
	[Serializable]
	public class DifficultyBeatmapSet
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000055C7 File Offset: 0x000037C7
		public string beatmapCharacteristicName
		{
			get
			{
				return this._beatmapCharacteristicName;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000055CF File Offset: 0x000037CF
		public StandardLevelInfoSaveData.DifficultyBeatmap[] difficultyBeatmaps
		{
			get
			{
				return this._difficultyBeatmaps;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000055D7 File Offset: 0x000037D7
		public DifficultyBeatmapSet(string beatmapCharacteristicName, StandardLevelInfoSaveData.DifficultyBeatmap[] difficultyBeatmaps)
		{
			this._beatmapCharacteristicName = beatmapCharacteristicName;
			this._difficultyBeatmaps = difficultyBeatmaps;
		}

		// Token: 0x040005ED RID: 1517
		[SerializeField]
		private string _beatmapCharacteristicName;

		// Token: 0x040005EE RID: 1518
		[SerializeField]
		private StandardLevelInfoSaveData.DifficultyBeatmap[] _difficultyBeatmaps;
	}

	// Token: 0x02000171 RID: 369
	[Serializable]
	public class DifficultyBeatmap
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000055ED File Offset: 0x000037ED
		public string difficulty
		{
			get
			{
				return this._difficulty;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000055F5 File Offset: 0x000037F5
		public int difficultyRank
		{
			get
			{
				return this._difficultyRank;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000055FD File Offset: 0x000037FD
		public string beatmapFilename
		{
			get
			{
				return this._beatmapFilename;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00005605 File Offset: 0x00003805
		public float noteJumpMovementSpeed
		{
			get
			{
				return this._noteJumpMovementSpeed;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000560D File Offset: 0x0000380D
		public float noteJumpStartBeatOffset
		{
			get
			{
				return this._noteJumpStartBeatOffset;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00005615 File Offset: 0x00003815
		public DifficultyBeatmap(string difficultyName, int difficultyRank, string beatmapFilename, float noteJumpMovementSpeed, float noteJumpStartBeatOffset)
		{
			this._difficulty = difficultyName;
			this._difficultyRank = difficultyRank;
			this._beatmapFilename = beatmapFilename;
			this._noteJumpMovementSpeed = noteJumpMovementSpeed;
			this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
		}

		// Token: 0x040005EF RID: 1519
		[SerializeField]
		private string _difficulty;

		// Token: 0x040005F0 RID: 1520
		[SerializeField]
		private int _difficultyRank;

		// Token: 0x040005F1 RID: 1521
		[SerializeField]
		private string _beatmapFilename;

		// Token: 0x040005F2 RID: 1522
		[SerializeField]
		private float _noteJumpMovementSpeed;

		// Token: 0x040005F3 RID: 1523
		[SerializeField]
		private float _noteJumpStartBeatOffset;
	}

	// Token: 0x02000172 RID: 370
	private class VersionCheck
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00005642 File Offset: 0x00003842
		public string version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x040005F4 RID: 1524
		[SerializeField]
		private string _version;
	}
}
