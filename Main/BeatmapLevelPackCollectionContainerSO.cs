using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class BeatmapLevelPackCollectionContainerSO : PersistentScriptableObject
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00004EB3 File Offset: 0x000030B3
	public BeatmapLevelPackCollectionSO beatmapLevelPackCollection
	{
		get
		{
			return this._beatmapLevelPackCollection;
		}
	}

	// Token: 0x040004FB RID: 1275
	[SerializeField]
	private BeatmapLevelPackCollectionSO _beatmapLevelPackCollection;
}
