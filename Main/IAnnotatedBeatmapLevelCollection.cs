using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
public interface IAnnotatedBeatmapLevelCollection
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x060005A4 RID: 1444
	string collectionName { get; }

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x060005A5 RID: 1445
	Sprite coverImage { get; }

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x060005A6 RID: 1446
	IBeatmapLevelCollection beatmapLevelCollection { get; }
}
