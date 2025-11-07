using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class BeatmapLevelPackSO : PersistentScriptableObject, IBeatmapLevelPack, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060004BC RID: 1212 RVA: 0x00004ED1 File Offset: 0x000030D1
	public string packID
	{
		get
		{
			return this._packID;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x00004ED9 File Offset: 0x000030D9
	public string packName
	{
		get
		{
			return this._packName;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x060004BE RID: 1214 RVA: 0x00004EE1 File Offset: 0x000030E1
	public string shortPackName
	{
		get
		{
			return this._shortPackName;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x060004BF RID: 1215 RVA: 0x00004EE9 File Offset: 0x000030E9
	public string collectionName
	{
		get
		{
			return this.shortPackName;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00004EF1 File Offset: 0x000030F1
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00004EF9 File Offset: 0x000030F9
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x040004FF RID: 1279
	[SerializeField]
	private string _packID;

	// Token: 0x04000500 RID: 1280
	[SerializeField]
	private string _packName;

	// Token: 0x04000501 RID: 1281
	[SerializeField]
	private string _shortPackName;

	// Token: 0x04000502 RID: 1282
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x04000503 RID: 1283
	[Space]
	[SerializeField]
	private BeatmapLevelCollectionSO _beatmapLevelCollection;
}
