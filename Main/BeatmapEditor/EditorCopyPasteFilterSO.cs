using System;

namespace BeatmapEditor
{
	// Token: 0x0200052C RID: 1324
	public class EditorCopyPasteFilterSO : PersistentScriptableObject
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x00012E5F File Offset: 0x0001105F
		protected override void OnEnable()
		{
			base.OnEnable();
			this.copyBaseNotes = true;
		}

		// Token: 0x0400188B RID: 6283
		[NonSerialized]
		public bool copyBaseNotes;

		// Token: 0x0400188C RID: 6284
		[NonSerialized]
		public bool copyUpperNotes;

		// Token: 0x0400188D RID: 6285
		[NonSerialized]
		public bool copyTopNotes;

		// Token: 0x0400188E RID: 6286
		[NonSerialized]
		public bool copyEvents;

		// Token: 0x0400188F RID: 6287
		[NonSerialized]
		public bool copyObstacles;
	}
}
