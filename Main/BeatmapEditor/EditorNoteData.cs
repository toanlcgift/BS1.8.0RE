using System;

namespace BeatmapEditor
{
	// Token: 0x02000521 RID: 1313
	public class EditorNoteData
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00012A17 File Offset: 0x00010C17
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x00012A1F File Offset: 0x00010C1F
		public NoteType type { get; private set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00012A28 File Offset: 0x00010C28
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x00012A30 File Offset: 0x00010C30
		public NoteCutDirection cutDirection { get; private set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00012A39 File Offset: 0x00010C39
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x00012A41 File Offset: 0x00010C41
		public NoteLineLayer noteLineLayer { get; private set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00012A4A File Offset: 0x00010C4A
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x00012A52 File Offset: 0x00010C52
		public int noteLineIndex { get; private set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00012A5B File Offset: 0x00010C5B
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x00012A63 File Offset: 0x00010C63
		public int beatIndex { get; private set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00002907 File Offset: 0x00000B07
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x000023E9 File Offset: 0x000005E9
		public bool highlight
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00012A6C File Offset: 0x00010C6C
		public EditorNoteData(NoteType type, NoteCutDirection cutDirection, NoteLineLayer noteLineLayer, int noteLineIndex, int beatIndex, bool highlight)
		{
			this.type = type;
			this.cutDirection = cutDirection;
			this.noteLineIndex = noteLineIndex;
			this.noteLineLayer = noteLineLayer;
			this.beatIndex = beatIndex;
			this.highlight = highlight;
		}
	}
}
