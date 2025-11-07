using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200057B RID: 1403
	public class NoteJumpStartBeatOffsetPanelController : MonoBehaviour
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x000142FC File Offset: 0x000124FC
		protected void Start()
		{
			this.RefreshUI();
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
			this._noteJumpStartBeatOffsetPanelControllerInputField.onEndEdit.AddListener(new UnityAction<string>(this.HandleInputFieldDidEndEdit));
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00014337 File Offset: 0x00012537
		protected void OnDestroy()
		{
			if (this._editorBeatmap != null)
			{
				this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0005E648 File Offset: 0x0005C848
		private void RefreshUI()
		{
			this._noteJumpStartBeatOffsetPanelControllerInputField.text = this._editorBeatmap.noteJumpStartBeatOffset.ToString();
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0001435E File Offset: 0x0001255E
		private void HandleInputFieldDidEndEdit(string text)
		{
			this._editorBeatmap.noteJumpStartBeatOffset = this.Convert(text, 0f);
			this.RefreshUI();
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0005E674 File Offset: 0x0005C874
		private float Convert(string s, float originalValue)
		{
			float result;
			if (float.TryParse(s, out result))
			{
				return result;
			}
			return originalValue;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0001437D File Offset: 0x0001257D
		private void HandleEditorBeatmapDidChangeAllData()
		{
			this.RefreshUI();
		}

		// Token: 0x040019FC RID: 6652
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019FD RID: 6653
		[SerializeField]
		private InputField _noteJumpStartBeatOffsetPanelControllerInputField;
	}
}
