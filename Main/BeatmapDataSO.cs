using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class BeatmapDataSO : PersistentScriptableObject
{
	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060003DD RID: 989 RVA: 0x00004538 File Offset: 0x00002738
	// (set) Token: 0x060003DE RID: 990 RVA: 0x0000454E File Offset: 0x0000274E
	public BeatmapData beatmapData
	{
		get
		{
			if (this._beatmapData == null)
			{
				this.Load();
			}
			return this._beatmapData;
		}
		set
		{
			this._beatmapData = value;
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00004557 File Offset: 0x00002757
	public void SetJsonData(string jsonData)
	{
		this._jsonData = jsonData;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00004560 File Offset: 0x00002760
	public void SetRequiredDataForLoad(float beatsPerMinute, float shuffle, float shufflePeriod)
	{
		this._beatsPerMinute = beatsPerMinute;
		this._shuffle = shuffle;
		this._shufflePeriod = shufflePeriod;
		this._hasRequiredDataForLoad = true;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0000457E File Offset: 0x0000277E
	public void Load()
	{
		if (this._hasRequiredDataForLoad)
		{
			this._beatmapData = this._beatmapDataLoader.GetBeatmapDataFromJson(this._jsonData, this._beatsPerMinute, this._shuffle, this._shufflePeriod);
		}
	}

	// Token: 0x04000433 RID: 1075
	[HideInInspector]
	[SerializeField]
	public string _jsonData;

	// Token: 0x04000434 RID: 1076
	private BeatmapData _beatmapData;

	// Token: 0x04000435 RID: 1077
	private float _beatsPerMinute;

	// Token: 0x04000436 RID: 1078
	private float _shuffle;

	// Token: 0x04000437 RID: 1079
	private float _shufflePeriod;

	// Token: 0x04000438 RID: 1080
	private bool _hasRequiredDataForLoad;

	// Token: 0x04000439 RID: 1081
	private BeatmapDataLoader _beatmapDataLoader = new BeatmapDataLoader();
}
