using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000515 RID: 1301
	public class BeatmapEditorSongController : MonoBehaviour
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00012437 File Offset: 0x00010637
		public float songDuration
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

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00012462 File Offset: 0x00010662
		public bool isPlaying
		{
			get
			{
				return this._audioSource.isPlaying;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0001246F File Offset: 0x0001066F
		public float songTimeOffset
		{
			get
			{
				return this._songTimeOffset.value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0001247C File Offset: 0x0001067C
		public AudioClip audioClip
		{
			get
			{
				return this._audioSource.clip;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00012489 File Offset: 0x00010689
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0005704C File Offset: 0x0005524C
		public float songTime
		{
			get
			{
				return this._songTime;
			}
			set
			{
				if (this._audioSource.clip == null)
				{
					this._songTime = 0f;
					return;
				}
				this._songTime = Mathf.Clamp(value, 0f, this._audioSource.clip.length - this._songEndOffset);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00012491 File Offset: 0x00010691
		protected void Awake()
		{
			this._sampleData = new float[2];
			this._sampleData[0] = 0f;
			this._sampleData[1] = 0f;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000124B9 File Offset: 0x000106B9
		protected void Start()
		{
			this._audioSource.clip = this._editorAudio.audioClip;
			this._editorAudio.didChangeAudioEvent += this.HandleEditorAudioDidChangeAudio;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000124E8 File Offset: 0x000106E8
		protected void OnDestroy()
		{
			if (this._editorAudio)
			{
				this._editorAudio.didChangeAudioEvent -= this.HandleEditorAudioDidChangeAudio;
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0001250E File Offset: 0x0001070E
		protected void Update()
		{
			this._songTime = Mathf.Max(0f, this._audioSource.time - this._songTimeOffset);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00012537 File Offset: 0x00010737
		public void PlaySong()
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			this._audioSource.Play();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000570A0 File Offset: 0x000552A0
		public void PlaySong(float startTime)
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			startTime = Mathf.Clamp(startTime, 0f, this.songDuration - this._songEndOffset);
			this._startTime = startTime;
			this._audioSource.time = startTime;
			this._songTime = startTime;
			this._audioSource.Play();
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00012558 File Offset: 0x00010758
		public void PauseSong()
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			this._audioSource.Pause();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00057100 File Offset: 0x00055300
		public void StopSong()
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			this._audioSource.Stop();
			this._audioSource.time = this._startTime - this._songTimeOffset;
			this._songTime = this._startTime - this._songTimeOffset;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00057164 File Offset: 0x00055364
		public void RewindSong()
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			this._audioSource.Stop();
			this._audioSource.time = -this._songTimeOffset;
			this._songTime = -this._songTimeOffset;
			this._startTime = 0f;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000571C4 File Offset: 0x000553C4
		public void GoToSongEnd()
		{
			if (this._audioSource.clip == null)
			{
				return;
			}
			float num = this.songDuration - this._songEndOffset;
			this._audioSource.Stop();
			this._audioSource.time = num;
			this._songTime = num;
			this._startTime = num;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00012579 File Offset: 0x00010779
		private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
		{
			this._audioSource.Stop();
			this._audioSource.clip = audioClip;
		}

		// Token: 0x04001830 RID: 6192
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x04001831 RID: 6193
		[SerializeField]
		private EditorAudioSO _editorAudio;

		// Token: 0x04001832 RID: 6194
		[SerializeField]
		private AudioSource _audioSource;

		// Token: 0x04001833 RID: 6195
		private float _songEndOffset = 0.1f;

		// Token: 0x04001834 RID: 6196
		private float _songTime;

		// Token: 0x04001835 RID: 6197
		private float _startTime;

		// Token: 0x04001836 RID: 6198
		private float[] _sampleData;
	}
}
