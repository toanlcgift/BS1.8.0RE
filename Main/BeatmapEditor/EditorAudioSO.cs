using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000527 RID: 1319
	public class EditorAudioSO : PersistentScriptableObject
	{
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x0600193E RID: 6462 RVA: 0x000583A0 File Offset: 0x000565A0
		// (remove) Token: 0x0600193F RID: 6463 RVA: 0x000583D8 File Offset: 0x000565D8
		public event Action<AudioClip> didChangeAudioEvent;

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00012BCE File Offset: 0x00010DCE
		public AudioClip audioClip
		{
			get
			{
				return this._audioClip;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00012BD6 File Offset: 0x00010DD6
		public bool isAudioLoaded
		{
			get
			{
				return this._audioClip != null;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public float songDuration
		{
			get
			{
				if (!(this._audioClip != null))
				{
					return 0f;
				}
				return this._audioClip.length;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00012C05 File Offset: 0x00010E05
		public string audioFilePath
		{
			get
			{
				return this._audioFilePath;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00012C0D File Offset: 0x00010E0D
		public string audioFileName
		{
			get
			{
				return Path.GetFileName(this._audioFilePath);
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00058410 File Offset: 0x00056610
		public IEnumerator LoadAudioCoroutine(string filePath, Action didLoadAction)
		{
			return this._audioClipLoader.LoadAudioFileCoroutine(filePath, delegate(AudioClip audioClip)
			{
				this.HandleLoadAudio(audioClip, filePath, didLoadAction);
			});
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00058458 File Offset: 0x00056658
		public void LoadAudio(string filePath, Action didLoadAction)
		{
			this._audioClipLoader.LoadAudioFile(filePath, delegate(AudioClip audioClip)
			{
				this.HandleLoadAudio(audioClip, filePath, didLoadAction);
			});
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00012C1A File Offset: 0x00010E1A
		private void HandleLoadAudio(AudioClip audioClip, string filePath, Action didLoadAction)
		{
			this._audioFilePath = filePath;
			this._audioClip = audioClip;
			Action<AudioClip> action = this.didChangeAudioEvent;
			if (action != null)
			{
				action(audioClip);
			}
			if (didLoadAction != null)
			{
				didLoadAction();
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00012C45 File Offset: 0x00010E45
		public void Clear()
		{
			this._audioClip = null;
			this._audioFilePath = null;
			Action<AudioClip> action = this.didChangeAudioEvent;
			if (action == null)
			{
				return;
			}
			action(null);
		}

		// Token: 0x0400187D RID: 6269
		[SerializeField]
		private AudioClipLoaderSO _audioClipLoader;

		// Token: 0x0400187F RID: 6271
		private AudioClip _audioClip;

		// Token: 0x04001880 RID: 6272
		private string _audioFilePath;
	}
}
