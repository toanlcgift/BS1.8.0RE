using System;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class NoteVisualDebuger : MonoBehaviour
{
	// Token: 0x060009F4 RID: 2548 RVA: 0x0002F1AC File Offset: 0x0002D3AC
	protected void Update()
	{
		this._text.text = string.Concat(new object[]
		{
			this._noteController.noteData.id,
			" - ",
			this._noteController.noteData.time,
			" | ",
			this._noteController.noteData.timeToPrevBasicNote,
			" | ",
			this._noteController.noteData.timeToNextBasicNote
		});
	}

	// Token: 0x04000A2B RID: 2603
	[SerializeField]
	private NoteController _noteController;

	// Token: 0x04000A2C RID: 2604
	[SerializeField]
	private TextMesh _text;
}
