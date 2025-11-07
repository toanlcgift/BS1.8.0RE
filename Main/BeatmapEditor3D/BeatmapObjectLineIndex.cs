using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004D2 RID: 1234
	public class BeatmapObjectLineIndex
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x00010CF2 File Offset: 0x0000EEF2
		public BeatmapObjectLineIndex(int x = 0, int y = 0)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00010D08 File Offset: 0x0000EF08
		public BeatmapObjectLineIndex(Vector2Int position)
		{
			this.x = position.x;
			this.y = position.y;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00010D2A File Offset: 0x0000EF2A
		public bool Equals(BeatmapObjectLineIndex other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00010D4A File Offset: 0x0000EF4A
		public override string ToString()
		{
			return string.Format("Line ({0}, {1})", this.x, this.y);
		}

		// Token: 0x040016DD RID: 5853
		public int x;

		// Token: 0x040016DE RID: 5854
		public int y;
	}
}
