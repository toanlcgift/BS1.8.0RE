using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class CustomBeatmapLevelPack : IBeatmapLevelPack, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x17000116 RID: 278
	// (get) Token: 0x0600050C RID: 1292 RVA: 0x00005176 File Offset: 0x00003376
	public string packID
	{
		get
		{
			return this._packID;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000517E File Offset: 0x0000337E
	public string packName
	{
		get
		{
			return this._packName;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x0600050E RID: 1294 RVA: 0x00005186 File Offset: 0x00003386
	public string shortPackName
	{
		get
		{
			return this._shortPackName;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000518E File Offset: 0x0000338E
	public string collectionName
	{
		get
		{
			return this.shortPackName;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000510 RID: 1296 RVA: 0x00005196 File Offset: 0x00003396
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000519E File Offset: 0x0000339E
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._customBeatmapLevelCollection;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000512 RID: 1298 RVA: 0x00002969 File Offset: 0x00000B69
	public bool isPackAlwaysOwned
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x000051A6 File Offset: 0x000033A6
	public CustomBeatmapLevelPack(string packID, string packName, string shortPackName, Sprite coverImage, CustomBeatmapLevelCollection customBeatmapLevelCollection)
	{
		this._packID = packID;
		this._packName = packName;
		this._shortPackName = shortPackName;
		this._coverImage = coverImage;
		this._customBeatmapLevelCollection = customBeatmapLevelCollection;
	}

	// Token: 0x0400055C RID: 1372
	private string _packID;

	// Token: 0x0400055D RID: 1373
	private string _packName;

	// Token: 0x0400055E RID: 1374
	private string _shortPackName;

	// Token: 0x0400055F RID: 1375
	private Sprite _coverImage;

	// Token: 0x04000560 RID: 1376
	private CustomBeatmapLevelCollection _customBeatmapLevelCollection;
}
