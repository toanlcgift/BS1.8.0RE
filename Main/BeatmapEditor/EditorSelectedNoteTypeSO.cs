using System;

namespace BeatmapEditor
{
	// Token: 0x02000535 RID: 1333
	public class EditorSelectedNoteTypeSO : ObservableVariableSO<NoteType>
	{
		// Token: 0x06001992 RID: 6546 RVA: 0x00012F33 File Offset: 0x00011133
		protected override void OnEnable()
		{
			base.OnEnable();
			base.value = NoteType.NoteA;
		}
	}
}
