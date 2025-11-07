using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class BeatmapLevelCollectionSO : PersistentScriptableObject, IBeatmapLevelCollection
{
	// Token: 0x170000DC RID: 220
	// (get) Token: 0x06000492 RID: 1170 RVA: 0x00021E60 File Offset: 0x00020060
	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._beatmapLevels;
		}
	}

	// Token: 0x040004E5 RID: 1253
	[SerializeField]
	private BeatmapLevelSO[] _beatmapLevels;
}
