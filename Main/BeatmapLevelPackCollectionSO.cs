using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class BeatmapLevelPackCollectionSO : PersistentScriptableObject, IBeatmapLevelPackCollection
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00004EBB File Offset: 0x000030BB
	public IBeatmapLevelPack[] beatmapLevelPacks
	{
		get
		{
			if (this._allBeatmapLevelPacks == null)
			{
				this.LoadAllBeatmapLevelPacksAsync();
			}
			return this._allBeatmapLevelPacks;
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00021FB0 File Offset: 0x000201B0
	public void LoadAllBeatmapLevelPacksAsync()
	{
		if (this._allBeatmapLevelPacks != null)
		{
			return;
		}
		List<IBeatmapLevelPack> list = new List<IBeatmapLevelPack>();
		if (this._beatmapLevelPacks != null)
		{
			foreach (BeatmapLevelPackSO item in this._beatmapLevelPacks)
			{
				list.Add(item);
			}
		}
		if (this._previewBeatmapLevelPack != null)
		{
			foreach (PreviewBeatmapLevelPackSO item2 in this._previewBeatmapLevelPack)
			{
				list.Add(item2);
			}
		}
		this._allBeatmapLevelPacks = list.ToArray();
	}

	// Token: 0x040004FC RID: 1276
	[SerializeField]
	private BeatmapLevelPackSO[] _beatmapLevelPacks;

	// Token: 0x040004FD RID: 1277
	[SerializeField]
	private PreviewBeatmapLevelPackSO[] _previewBeatmapLevelPack;

	// Token: 0x040004FE RID: 1278
	private IBeatmapLevelPack[] _allBeatmapLevelPacks;
}
