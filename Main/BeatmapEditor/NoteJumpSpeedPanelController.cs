using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200057A RID: 1402
	public class NoteJumpSpeedPanelController : MonoBehaviour
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x00014269 File Offset: 0x00012469
		protected void Start()
		{
			this.RefreshUI();
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
			this._noteJumpMovementSpeedInputField.onEndEdit.AddListener(new UnityAction<string>(this.HandleInputFieldDidEndEdit));
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000142A4 File Offset: 0x000124A4
		protected void OnDestroy()
		{
			if (this._editorBeatmap != null)
			{
				this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0005E5EC File Offset: 0x0005C7EC
		private void RefreshUI()
		{
			this._noteJumpMovementSpeedInputField.text = this._editorBeatmap.noteJumpMovementSpeed.ToString();
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000142CB File Offset: 0x000124CB
		private void HandleInputFieldDidEndEdit(string text)
		{
			this._editorBeatmap.noteJumpMovementSpeed = this.ConvertAndClamp(text, 0f, 7f, 20f);
			this.RefreshUI();
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0005E618 File Offset: 0x0005C818
		private float ConvertAndClamp(string s, float originalValue, float min, float max)
		{
			float num;
			if (!float.TryParse(s, out num))
			{
				return originalValue;
			}
			if (num == 0f)
			{
				return 0f;
			}
			return Mathf.Clamp(num, min, max);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000142F4 File Offset: 0x000124F4
		private void HandleEditorBeatmapDidChangeAllData()
		{
			this.RefreshUI();
		}

		// Token: 0x040019FA RID: 6650
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019FB RID: 6651
		[SerializeField]
		private InputField _noteJumpMovementSpeedInputField;
	}
}
