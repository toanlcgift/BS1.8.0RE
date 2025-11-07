using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class BeatmapLevelPack : IBeatmapLevelPack, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060004AC RID: 1196 RVA: 0x00004E14 File Offset: 0x00003014
	public string packID
	{
		get
		{
			return this._levelPackID;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060004AD RID: 1197 RVA: 0x00004E1C File Offset: 0x0000301C
	public string packName
	{
		get
		{
			return this._packName;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060004AE RID: 1198 RVA: 0x00004E24 File Offset: 0x00003024
	public string shortPackName
	{
		get
		{
			return this._shortPackName;
		}
	}

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060004AF RID: 1199 RVA: 0x00004E2C File Offset: 0x0000302C
	public string collectionName
	{
		get
		{
			return this.shortPackName;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00004E34 File Offset: 0x00003034
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00004E3C File Offset: 0x0000303C
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00004E44 File Offset: 0x00003044
	public BeatmapLevelPack(string levelPackID, string packName, string shortPackName, Sprite coverImage, IBeatmapLevelCollection levelCollection)
	{
		this._levelPackID = levelPackID;
		this._packName = packName;
		this._shortPackName = shortPackName;
		this._coverImage = coverImage;
		this._beatmapLevelCollection = levelCollection;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00004E71 File Offset: 0x00003071
	public static BeatmapLevelPack CreateBeatmapLevelPackByUsingBeatmapCharacteristicFiltering(IBeatmapLevelPack beatmapLevelPack, BeatmapCharacteristicSO beatmapCharacteristic)
	{
		return new BeatmapLevelPack(beatmapLevelPack.packID, beatmapLevelPack.packName, beatmapLevelPack.shortPackName, beatmapLevelPack.coverImage, BeatmapLevelCollection.CreateBeatmapLevelCollectionByUsingBeatmapCharacteristicFiltering(beatmapLevelPack.beatmapLevelCollection, beatmapCharacteristic));
	}

	// Token: 0x040004F5 RID: 1269
	private string _levelPackID;

	// Token: 0x040004F6 RID: 1270
	private string _packName;

	// Token: 0x040004F7 RID: 1271
	private string _shortPackName;

	// Token: 0x040004F8 RID: 1272
	private Sprite _coverImage;

	// Token: 0x040004F9 RID: 1273
	private IBeatmapLevelCollection _beatmapLevelCollection;
}
