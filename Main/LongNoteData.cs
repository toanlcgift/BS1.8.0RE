using System;

// Token: 0x02000109 RID: 265
public class LongNoteData : BeatmapObjectData
{
	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000414 RID: 1044 RVA: 0x000047FE File Offset: 0x000029FE
	// (set) Token: 0x06000415 RID: 1045 RVA: 0x00004806 File Offset: 0x00002A06
	public NoteLineLayer noteLineLayer { get; private set; }

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000480F File Offset: 0x00002A0F
	// (set) Token: 0x06000417 RID: 1047 RVA: 0x00004817 File Offset: 0x00002A17
	public NoteType noteType { get; private set; }

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000418 RID: 1048 RVA: 0x00004820 File Offset: 0x00002A20
	// (set) Token: 0x06000419 RID: 1049 RVA: 0x00004828 File Offset: 0x00002A28
	public float duration { get; private set; }

	// Token: 0x0600041A RID: 1050 RVA: 0x00004831 File Offset: 0x00002A31
	public LongNoteData(int id, float time, int lineIndex, NoteLineLayer noteLineLayer, NoteType noteType, float duration) : base(BeatmapObjectType.LongNote, id, time, lineIndex)
	{
		this.duration = duration;
		this.noteLineLayer = noteLineLayer;
		this.noteType = noteType;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00004855 File Offset: 0x00002A55
	public void UpdateDuration(float duration)
	{
		this.duration = duration;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0000485E File Offset: 0x00002A5E
	public override BeatmapObjectData GetCopy()
	{
		return new LongNoteData(base.id, base.time, base.lineIndex, this.noteLineLayer, this.noteType, this.duration);
	}
}
