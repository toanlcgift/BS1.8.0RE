using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000553 RID: 1363
	public class BeatmapEditorScrollViewDataSetter : MonoBehaviour
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x0005BE14 File Offset: 0x0005A014
		protected void Start()
		{
			this.RefreshParams();
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
			this._editorAudio.didChangeAudioEvent += this.HandleEditorAudioDidChangeAudio;
			this._beatsPerMinute.didChangeEvent += this.HandleBeatsPerMinutDidChange;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0005BE6C File Offset: 0x0005A06C
		protected void OnDestroy()
		{
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			this._editorAudio.didChangeAudioEvent -= this.HandleEditorAudioDidChangeAudio;
			this._beatsPerMinute.didChangeEvent -= this.HandleBeatsPerMinutDidChange;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		private void RefreshParams()
		{
			float num = this._editorAudio.songDuration;
			if (num < 0.001f)
			{
				float num2 = this._editorBeatmap.BeatDuration(this._beatsPerMinute);
				num = (float)this._editorBeatmap.beatsDataLength * num2;
			}
			this._beatmapEditorScrollView.ChangeParams(this._beatsPerMinute, this._editorBeatmap.beatsPerBpmBeat, num);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000138B3 File Offset: 0x00011AB3
		private void HandleBeatsPerMinutDidChange()
		{
			this.RefreshParams();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000138B3 File Offset: 0x00011AB3
		private void HandleEditorBeatmapDidChangeAllData()
		{
			this.RefreshParams();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000138B3 File Offset: 0x00011AB3
		private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
		{
			this.RefreshParams();
		}

		// Token: 0x04001954 RID: 6484
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001955 RID: 6485
		[SerializeField]
		private FloatSO _beatsPerMinute;

		// Token: 0x04001956 RID: 6486
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x04001957 RID: 6487
		[Space]
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;
	}
}
