using System;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class PreviewBeatmapLevelPackSO : PersistentScriptableObject, IBeatmapLevelPack, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000564 RID: 1380 RVA: 0x00005344 File Offset: 0x00003544
	public string packID
	{
		get
		{
			return this._packID;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000534C File Offset: 0x0000354C
	public string packName
	{
		get
		{
			return this._packName;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06000566 RID: 1382 RVA: 0x00005354 File Offset: 0x00003554
	public string shortPackName
	{
		get
		{
			return this._shortPackName;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000535C File Offset: 0x0000355C
	public string collectionName
	{
		get
		{
			return this.shortPackName;
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000568 RID: 1384 RVA: 0x00005364 File Offset: 0x00003564
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000536C File Offset: 0x0000356C
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._previewBeatmapLevelCollection;
		}
	}

	// Token: 0x0400058D RID: 1421
	[SerializeField]
	private string _packID;

	// Token: 0x0400058E RID: 1422
	[SerializeField]
	private string _packName;

	// Token: 0x0400058F RID: 1423
	[SerializeField]
	private string _shortPackName;

	// Token: 0x04000590 RID: 1424
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x04000591 RID: 1425
	[Space]
	[SerializeField]
	private PreviewBeatmapLevelCollectionSO _previewBeatmapLevelCollection;
}
