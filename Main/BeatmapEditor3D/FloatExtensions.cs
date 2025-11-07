using System;

namespace BeatmapEditor3D
{
	// Token: 0x020004DC RID: 1244
	public static class FloatExtensions
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x000534AC File Offset: 0x000516AC
		public static float AngleClampedTo360(this float angle)
		{
			float num = angle % 360f;
			if (num < 0f)
			{
				num = 360f + num;
			}
			return num;
		}
	}
}
