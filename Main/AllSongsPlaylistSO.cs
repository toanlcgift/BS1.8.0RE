using System;
using Polyglot;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class AllSongsPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000599 RID: 1433 RVA: 0x000054C2 File Offset: 0x000036C2
	public string collectionName
	{
		get
		{
			return Localization.Get(this._playListLocalizedName);
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x0600059A RID: 1434 RVA: 0x000054CF File Offset: 0x000036CF
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600059B RID: 1435 RVA: 0x000054D7 File Offset: 0x000036D7
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00023B84 File Offset: 0x00021D84
	public void SetupFromLevelPackCollection(IBeatmapLevelPackCollection beatmapLevelPackCollection)
	{
		BeatmapLevelFilterModel.LevelFilterParams levelFilterParams = BeatmapLevelFilterModel.LevelFilterParams.NoFilter();
		this._beatmapLevelCollection = BeatmapLevelFilterModel.FilerBeatmapLevelPackCollection(beatmapLevelPackCollection, levelFilterParams);
	}

	// Token: 0x040005CE RID: 1486
	[SerializeField]
	private string _playListLocalizedName;

	// Token: 0x040005CF RID: 1487
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x040005D0 RID: 1488
	private IBeatmapLevelCollection _beatmapLevelCollection;
}
