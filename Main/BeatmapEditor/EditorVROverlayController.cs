using System;
using TMPro;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000560 RID: 1376
	public class EditorVROverlayController : MonoBehaviour
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x00013C8C File Offset: 0x00011E8C
		protected void Start()
		{
			this.RefreshCanTestStartText();
			this._editorAudio.didChangeAudioEvent += this.HandleEditorAudioDidChangeAudio;
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00013CC2 File Offset: 0x00011EC2
		protected void OnDestroy()
		{
			this._editorAudio.didChangeAudioEvent -= this.HandleEditorAudioDidChangeAudio;
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00013CF2 File Offset: 0x00011EF2
		private void HandleEditorBeatmapDidChangeAllData()
		{
			this.RefreshCanTestStartText();
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00013CF2 File Offset: 0x00011EF2
		private void HandleEditorAudioDidChangeAudio(AudioClip AudioClip)
		{
			this.RefreshCanTestStartText();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00013CFA File Offset: 0x00011EFA
		private void RefreshCanTestStartText()
		{
			this._canTestStartText.enabled = (this._editorAudio.isAudioLoaded && this._editorBeatmap.hasBeatsData);
		}

		// Token: 0x0400199E RID: 6558
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x0400199F RID: 6559
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019A0 RID: 6560
		[Space]
		[SerializeField]
		private TextMeshProUGUI _canTestStartText;
	}
}
