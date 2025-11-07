using System;

namespace BeatmapEditor
{
	// Token: 0x02000520 RID: 1312
	public class EditorEventData
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x00012A01 File Offset: 0x00010C01
		public EditorEventData(int value, bool isPreviousValidValue)
		{
			this.value = value;
			this.isPreviousValidValue = isPreviousValidValue;
		}

		// Token: 0x0400186E RID: 6254
		public readonly int value;

		// Token: 0x0400186F RID: 6255
		public readonly bool isPreviousValidValue;
	}
}
