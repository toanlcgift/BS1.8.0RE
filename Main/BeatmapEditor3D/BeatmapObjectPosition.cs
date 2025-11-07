using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004D3 RID: 1235
	public class BeatmapObjectPosition
	{
		// Token: 0x060016BC RID: 5820 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public BeatmapObjectPosition()
		{
			this.beatIndex = new BeatmapObjectBeatIndex(0, 0);
			this.lineIndex = new BeatmapObjectLineIndex(0, 0);
			this.sizeIndex = new BeatmapObjectSizeIndex(0f, 0f, 0f);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public BeatmapObjectPosition(BeatmapObjectBeatIndex beatIndex, BeatmapObjectLineIndex lineIndex)
		{
			this.beatIndex = beatIndex;
			this.lineIndex = lineIndex;
			this.sizeIndex = new BeatmapObjectSizeIndex(0f, 0f, 0f);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public BeatmapObjectPosition(BeatmapObjectBeatIndex beatIndex, BeatmapObjectLineIndex lineIndex, BeatmapObjectSizeIndex sizeIndex)
		{
			this.beatIndex = beatIndex;
			this.lineIndex = lineIndex;
			this.sizeIndex = sizeIndex;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00010DF5 File Offset: 0x0000EFF5
		public bool Equals(BeatmapObjectPosition other)
		{
			return this.beatIndex.Equals(other.beatIndex) && this.lineIndex.Equals(other.lineIndex);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00053120 File Offset: 0x00051320
		public override string ToString()
		{
			if (this.sizeIndex.hasSize)
			{
				return string.Format("Position ({0}, {1}, {2})", this.beatIndex, this.lineIndex, this.sizeIndex);
			}
			return string.Format("Position ({0}, {1})", this.beatIndex, this.lineIndex);
		}

		// Token: 0x040016DF RID: 5855
		public BeatmapObjectBeatIndex beatIndex;

		// Token: 0x040016E0 RID: 5856
		public BeatmapObjectLineIndex lineIndex;

		// Token: 0x040016E1 RID: 5857
		public BeatmapObjectSizeIndex sizeIndex;
	}
}
