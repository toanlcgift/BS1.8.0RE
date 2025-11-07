using System;
using System.Collections.Generic;
using Polyglot;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class UserFavoritesPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x1700017C RID: 380
	// (get) Token: 0x060005AB RID: 1451 RVA: 0x00005521 File Offset: 0x00003721
	public string collectionName
	{
		get
		{
			return Localization.Get(this._playListLocalizedName);
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000552E File Offset: 0x0000372E
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x060005AD RID: 1453 RVA: 0x00005536 File Offset: 0x00003736
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00023BCC File Offset: 0x00021DCC
	public void SetupFromLevelPackCollection(HashSet<string> userFavoritesBeatmapLevelIds, IBeatmapLevelPackCollection beatmapLevelPackCollection)
	{
		BeatmapLevelFilterModel.LevelFilterParams levelFilterParams = BeatmapLevelFilterModel.LevelFilterParams.ByBeatmapLevelIds(userFavoritesBeatmapLevelIds);
		this._beatmapLevelCollection = BeatmapLevelFilterModel.FilerBeatmapLevelPackCollection(beatmapLevelPackCollection, levelFilterParams);
	}

	// Token: 0x040005D8 RID: 1496
	[SerializeField]
	private string _playListLocalizedName;

	// Token: 0x040005D9 RID: 1497
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x040005DA RID: 1498
	private IBeatmapLevelCollection _beatmapLevelCollection;
}
