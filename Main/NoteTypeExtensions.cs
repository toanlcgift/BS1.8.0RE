using System;

// Token: 0x0200010F RID: 271
public static class NoteTypeExtensions
{
	// Token: 0x0600043B RID: 1083 RVA: 0x000049A1 File Offset: 0x00002BA1
	public static bool IsBasicNote(this NoteType noteType)
	{
		return noteType == NoteType.NoteA || noteType == NoteType.NoteB;
	}
}
