using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004D4 RID: 1236
	public class BeatmapObjectSizeIndex
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00010E1D File Offset: 0x0000F01D
		public bool hasSize
		{
			get
			{
				return this.beats != 0f || this.columns != 0f || this.rows != 0f;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00010E4B File Offset: 0x0000F04B
		public BeatmapObjectSizeIndex(float columns = 0f, float rows = 0f, float beats = 0f)
		{
			this.columns = columns;
			this.rows = rows;
			this.beats = beats;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00010E68 File Offset: 0x0000F068
		public override string ToString()
		{
			return string.Format("Size ({0}, {1}, {2})", this.columns, this.rows, this.beats);
		}

		// Token: 0x040016E2 RID: 5858
		public float columns;

		// Token: 0x040016E3 RID: 5859
		public float rows;

		// Token: 0x040016E4 RID: 5860
		public float beats;
	}
}
