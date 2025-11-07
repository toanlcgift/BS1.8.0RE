using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class NoteData : BeatmapObjectData
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x000048C3 File Offset: 0x00002AC3
	// (set) Token: 0x06000422 RID: 1058 RVA: 0x000048CB File Offset: 0x00002ACB
	public NoteType noteType { get; private set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x000048D4 File Offset: 0x00002AD4
	// (set) Token: 0x06000424 RID: 1060 RVA: 0x000048DC File Offset: 0x00002ADC
	public NoteCutDirection cutDirection { get; private set; }

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x000048E5 File Offset: 0x00002AE5
	// (set) Token: 0x06000426 RID: 1062 RVA: 0x000048ED File Offset: 0x00002AED
	public NoteLineLayer noteLineLayer { get; private set; }

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x000048F6 File Offset: 0x00002AF6
	// (set) Token: 0x06000428 RID: 1064 RVA: 0x000048FE File Offset: 0x00002AFE
	public NoteLineLayer startNoteLineLayer { get; private set; }

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x00004907 File Offset: 0x00002B07
	// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000490F File Offset: 0x00002B0F
	public int flipLineIndex { get; private set; }

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x00004918 File Offset: 0x00002B18
	// (set) Token: 0x0600042C RID: 1068 RVA: 0x00004920 File Offset: 0x00002B20
	public float flipYSide { get; private set; }

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x00004929 File Offset: 0x00002B29
	// (set) Token: 0x0600042E RID: 1070 RVA: 0x00004931 File Offset: 0x00002B31
	public float timeToNextBasicNote { get; set; }

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000493A File Offset: 0x00002B3A
	// (set) Token: 0x06000430 RID: 1072 RVA: 0x00004942 File Offset: 0x00002B42
	public float timeToPrevBasicNote { get; private set; }

	// Token: 0x06000431 RID: 1073 RVA: 0x00020760 File Offset: 0x0001E960
	public override BeatmapObjectData GetCopy()
	{
		return new NoteData(base.id, base.time, base.lineIndex, this.noteLineLayer, this.startNoteLineLayer, this.noteType, this.cutDirection, this.timeToNextBasicNote, this.timeToPrevBasicNote, this.flipLineIndex, this.flipYSide);
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000207B4 File Offset: 0x0001E9B4
	public NoteData(int id, float time, int lineIndex, NoteLineLayer noteLineLayer, NoteLineLayer startNoteLineLayer, NoteType noteType, NoteCutDirection cutDirection, float timeToNextBasicNote, float timeToPrevBasicNote) : base(BeatmapObjectType.Note, id, time, lineIndex)
	{
		this.noteLineLayer = noteLineLayer;
		this.startNoteLineLayer = startNoteLineLayer;
		this.noteType = noteType;
		this.cutDirection = cutDirection;
		this.flipLineIndex = lineIndex;
		this.flipYSide = 0f;
		this.timeToNextBasicNote = timeToNextBasicNote;
		this.timeToPrevBasicNote = timeToPrevBasicNote;
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00020810 File Offset: 0x0001EA10
	public NoteData(int id, float time, int lineIndex, NoteLineLayer noteLineLayer, NoteLineLayer startNoteLineLayer, NoteType noteType, NoteCutDirection cutDirection, float timeToNextBasicNote, float timeToPrevBasicNote, int flipLineIndex, float flipYSide) : this(id, time, lineIndex, noteLineLayer, startNoteLineLayer, noteType, cutDirection, timeToNextBasicNote, timeToPrevBasicNote)
	{
		this.flipLineIndex = flipLineIndex;
		this.flipYSide = flipYSide;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00020844 File Offset: 0x0001EA44
	public void SetNoteFlipToNote(NoteData targetNote)
	{
		this.flipLineIndex = targetNote.lineIndex;
		this.flipYSide = (float)((base.lineIndex > targetNote.lineIndex) ? 1 : -1);
		if ((base.lineIndex > targetNote.lineIndex && this.noteLineLayer < targetNote.noteLineLayer) || (base.lineIndex < targetNote.lineIndex && this.noteLineLayer > targetNote.noteLineLayer))
		{
			this.flipYSide *= -1f;
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0000494B File Offset: 0x00002B4B
	public void SwitchNoteType()
	{
		if (this.noteType == NoteType.NoteA)
		{
			this.noteType = NoteType.NoteB;
			return;
		}
		if (this.noteType == NoteType.NoteB)
		{
			this.noteType = NoteType.NoteA;
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x000208C0 File Offset: 0x0001EAC0
	public void MirrorTransformCutDirection()
	{
		if (this.cutDirection == NoteCutDirection.Left)
		{
			this.cutDirection = NoteCutDirection.Right;
			return;
		}
		if (this.cutDirection == NoteCutDirection.Right)
		{
			this.cutDirection = NoteCutDirection.Left;
			return;
		}
		if (this.cutDirection == NoteCutDirection.UpLeft)
		{
			this.cutDirection = NoteCutDirection.UpRight;
			return;
		}
		if (this.cutDirection == NoteCutDirection.UpRight)
		{
			this.cutDirection = NoteCutDirection.UpLeft;
			return;
		}
		if (this.cutDirection == NoteCutDirection.DownLeft)
		{
			this.cutDirection = NoteCutDirection.DownRight;
			return;
		}
		if (this.cutDirection == NoteCutDirection.DownRight)
		{
			this.cutDirection = NoteCutDirection.DownLeft;
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0000496D File Offset: 0x00002B6D
	public void SetNoteToAnyCutDirection()
	{
		this.cutDirection = NoteCutDirection.Any;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00020934 File Offset: 0x0001EB34
	public void TransformNoteAOrBToRandomType()
	{
		if (this.noteType != NoteType.NoteA && this.noteType != NoteType.NoteB)
		{
			return;
		}
		if (UnityEngine.Random.Range(0f, 1f) > 0.6f)
		{
			this.noteType = ((this.noteType == NoteType.NoteA) ? NoteType.NoteB : NoteType.NoteA);
		}
		this.flipLineIndex = base.lineIndex;
		this.flipYSide = 0f;
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00004976 File Offset: 0x00002B76
	public void SetNoteStartLineLayer(NoteLineLayer lineLayer)
	{
		this.startNoteLineLayer = lineLayer;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0000497F File Offset: 0x00002B7F
	public override void MirrorLineIndex(int lineCount)
	{
		base.lineIndex = lineCount - 1 - base.lineIndex;
		this.flipLineIndex = lineCount - 1 - this.flipLineIndex;
	}
}
