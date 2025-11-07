using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200058A RID: 1418
	public class PlaybackController : MonoBehaviour
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00014729 File Offset: 0x00012929
		public float songTime
		{
			get
			{
				return this._beatmapEditorScrollView.scrollPositionSongTime;
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00014736 File Offset: 0x00012936
		protected void Awake()
		{
			this._beatmapEditorScrollView.scrollDragDidBeginEvent += this.HandleScrollDidBeginDrag;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0001474F File Offset: 0x0001294F
		protected void OnDestroy()
		{
			if (this._beatmapEditorScrollView != null)
			{
				this._beatmapEditorScrollView.scrollDragDidBeginEvent -= this.HandleScrollDidBeginDrag;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00014776 File Offset: 0x00012976
		protected void Update()
		{
			if (this._beatmapEditorSongController.isPlaying)
			{
				this.SyncSrollPositionWithSongControllerSongTime();
			}
			if (Input.GetKeyDown(KeyCode.Space) && !EventSystemHelpers.IsInputFieldSelected())
			{
				this.PlayOrPauseSong();
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000147A1 File Offset: 0x000129A1
		private void HandleScrollDidBeginDrag()
		{
			this.PauseSong();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0005F2E4 File Offset: 0x0005D4E4
		private void SyncSrollPositionWithSongControllerSongTime()
		{
			float songTime = this._beatmapEditorSongController.songTime;
			this._beatmapEditorScrollView.SetPositionToSongTime(songTime, false);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0005F30C File Offset: 0x0005D50C
		private void RefreshUI()
		{
			bool isPlaying = this._beatmapEditorSongController.isPlaying;
			this._playButtonImage.enabled = !isPlaying;
			this._pauseButtonImage.enabled = isPlaying;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000147A9 File Offset: 0x000129A9
		public void PauseSong()
		{
			if (this._beatmapEditorSongController.isPlaying)
			{
				this._beatmapEditorSongController.PauseSong();
				this._beatmapEditorScrollView.SnapPositionToBeat();
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000147CE File Offset: 0x000129CE
		public void PlayOrPauseSong()
		{
			if (this._beatmapEditorSongController.isPlaying)
			{
				this.PauseSong();
			}
			else
			{
				this._beatmapEditorSongController.PlaySong(this._beatmapEditorScrollView.scrollPositionSongTime + this._songTimeOffset);
			}
			this.RefreshUI();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0001480D File Offset: 0x00012A0D
		public void StopSong()
		{
			this._beatmapEditorSongController.StopSong();
			this.RefreshUI();
			this.SyncSrollPositionWithSongControllerSongTime();
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00014826 File Offset: 0x00012A26
		public void RewindSong()
		{
			this._beatmapEditorSongController.RewindSong();
			this.RefreshUI();
			this.SyncSrollPositionWithSongControllerSongTime();
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0001483F File Offset: 0x00012A3F
		public void ResumeSavedPosition()
		{
			this._beatmapEditorScrollView.MoveToSavedBeatPosition();
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0001484C File Offset: 0x00012A4C
		public void PlayOrPauseButtonPressed()
		{
			this.PlayOrPauseSong();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00014854 File Offset: 0x00012A54
		public void StopButtonPressed()
		{
			this.StopSong();
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0001485C File Offset: 0x00012A5C
		public void RewindButtonPressed()
		{
			this.RewindSong();
		}

		// Token: 0x04001A41 RID: 6721
		[SerializeField]
		private FloatSO _songTimeOffset;

		// Token: 0x04001A42 RID: 6722
		[Space]
		[SerializeField]
		private BeatmapEditorSongController _beatmapEditorSongController;

		// Token: 0x04001A43 RID: 6723
		[SerializeField]
		private BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x04001A44 RID: 6724
		[SerializeField]
		private Image _playButtonImage;

		// Token: 0x04001A45 RID: 6725
		[SerializeField]
		private Image _pauseButtonImage;
	}
}
