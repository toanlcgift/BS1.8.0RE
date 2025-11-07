using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004D1 RID: 1233
	public class BeatmapObjectBeatIndex
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00010C51 File Offset: 0x0000EE51
		public int milliseconds
		{
			get
			{
				return this.fraction / 1000;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00010C5F File Offset: 0x0000EE5F
		public float beatTime
		{
			get
			{
				return (float)this.beat + (float)this.fraction / 1000000f;
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00010C76 File Offset: 0x0000EE76
		public BeatmapObjectBeatIndex(int beat = 0, int fraction = 0)
		{
			this.beat = beat;
			this.fraction = fraction;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public BeatmapObjectBeatIndex(float beatTime)
		{
			this.beat = (int)beatTime;
			this.fraction = (int)(beatTime % 1f * 1000000f);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		public bool Equals(BeatmapObjectBeatIndex other)
		{
			return this.beat == other.beat && this.fraction == other.fraction;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00010CD0 File Offset: 0x0000EED0
		public override string ToString()
		{
			return string.Format("Beat ({0}, {1})", this.beat, this.fraction);
		}

		// Token: 0x040016DA RID: 5850
		private const int fractionPrecision = 1000000;

		// Token: 0x040016DB RID: 5851
		public int beat;

		// Token: 0x040016DC RID: 5852
		public int fraction;
	}
}
