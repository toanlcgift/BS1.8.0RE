using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004D6 RID: 1238
	public static class BeatmapObjectTypeExtension
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x000049A1 File Offset: 0x00002BA1
		public static bool IsNote(this BeatmapObjectType type)
		{
			return type == BeatmapObjectType.NoteA || type == BeatmapObjectType.NoteB;
		}
	}
}
