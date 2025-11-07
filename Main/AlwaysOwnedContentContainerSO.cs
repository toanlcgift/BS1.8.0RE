using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class AlwaysOwnedContentContainerSO : PersistentScriptableObject
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003660 File Offset: 0x00001860
	public HashSet<string> alwaysOwnedBeatmapLevelIds
	{
		get
		{
			if (this._alwaysOwnedBeatmapLevelIds == null)
			{
				this.InitAlwaysOwnedItems();
			}
			return this._alwaysOwnedBeatmapLevelIds;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003676 File Offset: 0x00001876
	public HashSet<string> alwaysOwnedPacksIds
	{
		get
		{
			if (this._alwaysOwnedPacksIds == null)
			{
				this.InitAlwaysOwnedItems();
			}
			return this._alwaysOwnedPacksIds;
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000368C File Offset: 0x0000188C
	protected override void OnEnable()
	{
		base.OnEnable();
		this.InitAlwaysOwnedItems();
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0001927C File Offset: 0x0001747C
	private void InitAlwaysOwnedItems()
	{
		this._alwaysOwnedBeatmapLevelIds = new HashSet<string>();
		this._alwaysOwnedPacksIds = new HashSet<string>();
		foreach (BeatmapLevelPackSO beatmapLevelPackSO in this._alwaysOwnedContent.alwaysOwnedPacks)
		{
			this._alwaysOwnedPacksIds.Add(beatmapLevelPackSO.packID);
			foreach (IPreviewBeatmapLevel previewBeatmapLevel in beatmapLevelPackSO.beatmapLevelCollection.beatmapLevels)
			{
				this._alwaysOwnedBeatmapLevelIds.Add(previewBeatmapLevel.levelID);
			}
		}
		foreach (BeatmapLevelSO beatmapLevelSO in this._alwaysOwnedContent.alwaysOwnedBeatmapLevels)
		{
			this._alwaysOwnedBeatmapLevelIds.Add(beatmapLevelSO.levelID);
		}
	}

	// Token: 0x040001D9 RID: 473
	[SerializeField]
	private AlwaysOwnedContentSO _alwaysOwnedContent;

	// Token: 0x040001DA RID: 474
	private HashSet<string> _alwaysOwnedBeatmapLevelIds;

	// Token: 0x040001DB RID: 475
	private HashSet<string> _alwaysOwnedPacksIds;
}
