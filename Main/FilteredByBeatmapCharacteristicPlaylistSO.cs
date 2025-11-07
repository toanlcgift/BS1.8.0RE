using System;
using Polyglot;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class FilteredByBeatmapCharacteristicPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600059E RID: 1438 RVA: 0x000054DF File Offset: 0x000036DF
	public BeatmapCharacteristicSO beatmapCharacteristic
	{
		get
		{
			return this._beatmapCharacteristic;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600059F RID: 1439 RVA: 0x000054E7 File Offset: 0x000036E7
	public string collectionName
	{
		get
		{
			return Localization.Get(this._playListNameLocalizationKey);
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000054F4 File Offset: 0x000036F4
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000054FC File Offset: 0x000036FC
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x00023BA4 File Offset: 0x00021DA4
	public void SetupFromLevelPackCollection(IBeatmapLevelPackCollection beatmapLevelPackCollection)
	{
		BeatmapLevelFilterModel.LevelFilterParams levelFilterParams = BeatmapLevelFilterModel.LevelFilterParams.ByBeatmapCharacteristic(this._beatmapCharacteristic);
		this._beatmapLevelCollection = BeatmapLevelFilterModel.FilerBeatmapLevelPackCollection(beatmapLevelPackCollection, levelFilterParams);
	}

	// Token: 0x040005D1 RID: 1489
	[SerializeField]
	private string _playListNameLocalizationKey;

	// Token: 0x040005D2 RID: 1490
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x040005D3 RID: 1491
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x040005D4 RID: 1492
	private IBeatmapLevelCollection _beatmapLevelCollection;
}
