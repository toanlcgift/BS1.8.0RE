using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class BeatmapCharacteristicCollectionSO : PersistentScriptableObject
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060003BD RID: 957 RVA: 0x00004481 File Offset: 0x00002681
	// (set) Token: 0x060003BE RID: 958 RVA: 0x00004489 File Offset: 0x00002689
	public BeatmapCharacteristicSO[] beatmapCharacteristics
	{
		get
		{
			return this._beatmapCharacteristics;
		}
		set
		{
			this._beatmapCharacteristics = value;
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000202B0 File Offset: 0x0001E4B0
	public BeatmapCharacteristicSO GetBeatmapCharacteristicBySerializedName(string serializedName)
	{
		foreach (BeatmapCharacteristicSO beatmapCharacteristicSO in this._beatmapCharacteristics)
		{
			if (beatmapCharacteristicSO.serializedName == serializedName)
			{
				return beatmapCharacteristicSO;
			}
		}
		return null;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x000202E8 File Offset: 0x0001E4E8
	public bool ContainsBeatmapCharacteristic(BeatmapCharacteristicSO beatmapCharacteristic)
	{
		BeatmapCharacteristicSO[] beatmapCharacteristics = this._beatmapCharacteristics;
		for (int i = 0; i < beatmapCharacteristics.Length; i++)
		{
			if (beatmapCharacteristics[i] == beatmapCharacteristic)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000424 RID: 1060
	[SerializeField]
	private BeatmapCharacteristicSO[] _beatmapCharacteristics;
}
