using System;
using BeatmapEditor;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004FA RID: 1274
	public class SongAudioController : MonoBehaviour, IAudioController
	{
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x060017E8 RID: 6120 RVA: 0x00055BE4 File Offset: 0x00053DE4
		// (remove) Token: 0x060017E9 RID: 6121 RVA: 0x00055C1C File Offset: 0x00053E1C
		public event Action didChangePlayStateEvent;

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00011959 File Offset: 0x0000FB59
		public bool isPlaying
		{
			get
			{
				return this._audioSource.isPlaying;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00011966 File Offset: 0x0000FB66
		public float duration
		{
			get
			{
				if (!(this._audioSource.clip != null))
				{
					return 0f;
				}
				return this._audioSource.clip.length;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00011991 File Offset: 0x0000FB91
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public float time
		{
			get
			{
				return Mathf.Max(0f, this._audioSource.time - this._startTimeOffset.value);
			}
			set
			{
				this._audioSource.time = Mathf.Clamp(value, -this._startTimeOffset.value, this.duration - this._endOffsetCorrection);
				this.HandleDidChangePlayStateEvent();
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000119E6 File Offset: 0x0000FBE6
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x000119F3 File Offset: 0x0000FBF3
		public float volume
		{
			get
			{
				return this._audioSource.volume;
			}
			set
			{
				this._audioSource.volume = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00011A01 File Offset: 0x0000FC01
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x00011A0E File Offset: 0x0000FC0E
		public bool mute
		{
			get
			{
				return this._audioSource.mute;
			}
			set
			{
				this._audioSource.mute = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00011A1C File Offset: 0x0000FC1C
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00011A29 File Offset: 0x0000FC29
		public float pitch
		{
			get
			{
				return this._audioSource.pitch;
			}
			set
			{
				this._audioSource.pitch = value;
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00011A37 File Offset: 0x0000FC37
		protected void Awake()
		{
			this._audioSource.clip = this._editorAudio.audioClip;
			this._editorAudio.didChangeAudioEvent += this.HandleEditorAudioDidChangeAudioClip;
			this.volume = this._defaultSongVolume;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00011A72 File Offset: 0x0000FC72
		protected void OnDestroy()
		{
			if (this._editorAudio)
			{
				this._editorAudio.didChangeAudioEvent -= this.HandleEditorAudioDidChangeAudioClip;
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00011A98 File Offset: 0x0000FC98
		protected void Update()
		{
			if (this._wasPlaying != this.isPlaying)
			{
				this._wasPlaying = this.isPlaying;
				this.HandleDidChangePlayStateEvent();
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00011ABA File Offset: 0x0000FCBA
		public void Play()
		{
			this._audioSource.Play();
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00011AC7 File Offset: 0x0000FCC7
		public void Play(float time)
		{
			this.time = time;
			this.Play();
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00011AD6 File Offset: 0x0000FCD6
		public void Pause()
		{
			this._audioSource.Pause();
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00011AE3 File Offset: 0x0000FCE3
		public void PlayOrPause()
		{
			if (this.isPlaying)
			{
				this.Pause();
				return;
			}
			this.Play();
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00011AFA File Offset: 0x0000FCFA
		public void Stop()
		{
			this.GoToStart();
			this._audioSource.Stop();
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00011B0D File Offset: 0x0000FD0D
		public void GoToStart()
		{
			this.Pause();
			this.time = -this._startTimeOffset;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00011B27 File Offset: 0x0000FD27
		public void GoToEnd()
		{
			this.Pause();
			this.time = this.duration;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00011B3B File Offset: 0x0000FD3B
		private void HandleDidChangePlayStateEvent()
		{
			Action action = this.didChangePlayStateEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00011B4D File Offset: 0x0000FD4D
		private void HandleEditorAudioDidChangeAudioClip(AudioClip audioClip)
		{
			this._audioSource.Stop();
			this._audioSource.clip = audioClip;
			this._audioSource.Play();
			this._audioSource.Pause();
			this.GoToStart();
		}

		// Token: 0x040017A6 RID: 6054
		[SerializeField]
		private AudioSource _audioSource;

		// Token: 0x040017A7 RID: 6055
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x040017A8 RID: 6056
		[SerializeField]
		private FloatSO _startTimeOffset;

		// Token: 0x040017A9 RID: 6057
		private float _defaultSongVolume = 0.5f;

		// Token: 0x040017AB RID: 6059
		private float _endOffsetCorrection = 0.1f;

		// Token: 0x040017AC RID: 6060
		private bool _wasPlaying;
	}
}
