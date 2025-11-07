using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class PreviewBeatmapLevelCollectionSO : PersistentScriptableObject, IBeatmapLevelCollection
{
	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x00023428 File Offset: 0x00021628
	public IPreviewBeatmapLevel[] beatmapLevels
	{
		get
		{
			return this._beatmapLevels;
		}
	}

	// Token: 0x0400058C RID: 1420
	[SerializeField]
	private PreviewBeatmapLevelSO[] _beatmapLevels;
}
