using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000594 RID: 1428
	public class WaveformImageDataSetter : MonoBehaviour
	{
		// Token: 0x06001BF9 RID: 7161 RVA: 0x0005FBB0 File Offset: 0x0005DDB0
		protected void Start()
		{
			this._waveFormImage.ChangeParams(this._songTimeOffset);
			this._waveFormImage.SetDataFromAudioClip(this._editorAudio.audioClip);
			this._editorAudio.didChangeAudioEvent += this.HandleEditorAudioDidChangeAudio;
			this._songTimeOffset.didChangeEvent += this.HandleSongTimeOffsetDidChange;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00014B38 File Offset: 0x00012D38
		protected void OnDestroy()
		{
			this._editorAudio.didChangeAudioEvent -= this.HandleEditorAudioDidChangeAudio;
			this._songTimeOffset.didChangeEvent -= this.HandleSongTimeOffsetDidChange;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00014B68 File Offset: 0x00012D68
		private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
		{
			this._waveFormImage.SetDataFromAudioClip(this._editorAudio.audioClip);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00014B80 File Offset: 0x00012D80
		private void HandleSongTimeOffsetDidChange()
		{
			this._waveFormImage.ChangeParams(this._songTimeOffset);
		}

		// Token: 0x04001A78 RID: 6776
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x04001A79 RID: 6777
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x04001A7A RID: 6778
		[Space]
		[SerializeField]
		private WaveformImage _waveFormImage;
	}
}
