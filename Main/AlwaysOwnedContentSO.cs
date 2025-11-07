using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class AlwaysOwnedContentSO : PersistentScriptableObject
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000369A File Offset: 0x0000189A
	public BeatmapLevelPackSO[] alwaysOwnedPacks
	{
		get
		{
			return this._alwaysOwnedPacks;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060001D8 RID: 472 RVA: 0x000036A2 File Offset: 0x000018A2
	public BeatmapLevelSO[] alwaysOwnedBeatmapLevels
	{
		get
		{
			return this._alwaysOwnedBeatmapLevels;
		}
	}

	// Token: 0x040001DC RID: 476
	[SerializeField]
	private BeatmapLevelPackSO[] _alwaysOwnedPacks;

	// Token: 0x040001DD RID: 477
	[SerializeField]
	private BeatmapLevelSO[] _alwaysOwnedBeatmapLevels;
}
