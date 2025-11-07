using System;

// Token: 0x02000135 RID: 309
public interface IBeatmapLevelPack : IAnnotatedBeatmapLevelCollection
{
	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060004A9 RID: 1193
	string packID { get; }

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x060004AA RID: 1194
	string packName { get; }

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060004AB RID: 1195
	string shortPackName { get; }
}
