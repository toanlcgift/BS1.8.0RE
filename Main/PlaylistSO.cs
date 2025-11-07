using System;
using Polyglot;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class PlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00005504 File Offset: 0x00003704
	public string collectionName
	{
		get
		{
			return Localization.Get(this._playListLocalizedName);
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00005511 File Offset: 0x00003711
	public Sprite coverImage
	{
		get
		{
			return this._coverImage;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00005519 File Offset: 0x00003719
	public IBeatmapLevelCollection beatmapLevelCollection
	{
		get
		{
			return this._beatmapLevelCollection;
		}
	}

	// Token: 0x040005D5 RID: 1493
	[SerializeField]
	private string _playListLocalizedName;

	// Token: 0x040005D6 RID: 1494
	[SerializeField]
	private Sprite _coverImage;

	// Token: 0x040005D7 RID: 1495
	[SerializeField]
	private BeatmapLevelCollectionSO _beatmapLevelCollection;
}
