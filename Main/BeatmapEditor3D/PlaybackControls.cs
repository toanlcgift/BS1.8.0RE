using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004EF RID: 1263
	public class PlaybackControls : MonoBehaviour
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x00011570 File Offset: 0x0000F770
		private bool isPlaying
		{
			get
			{
				return this._songAudioController.isPlaying;
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00054E94 File Offset: 0x00053094
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._goToStartButton,
					new Action(this.GoToSongStart)
				},
				{
					this._playButton,
					new Action(this.PlayOrPauseSong)
				},
				{
					this._goToEndButton,
					new Action(this.GoToSongEnd)
				}
			});
			this._songAudioController.didChangePlayStateEvent += this.HandleSongDidChangeState;
			this._songVolumeSlider.slider.didChangeValueEvent += this.HandleSongVolumeSliderDidChangeValue;
			this._songVolumeSlider.toggleDidChangeValueEvent += this.HandleSongVolumeToggleDidChangeValue;
			this._songSpeedSlider.slider.didChangeValueEvent += this.HandleSongSpeedSliderDidChangeValue;
			this._songSpeedSlider.toggleDidChangeValueEvent += this.HandleSongSpeedToggleDidChangeValue;
			this._metronomeVolumeSlider.slider.didChangeValueEvent += this.HandleMetronomeVolumeSliderDidChangeValue;
			this._metronomeVolumeSlider.toggleDidChangeValueEvent += this.HandleMetronomeVolumeToggleDidChangeValue;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0001157D File Offset: 0x0000F77D
		protected void Start()
		{
			this.RefreshSongVolume();
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00054FAC File Offset: 0x000531AC
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			this._songAudioController.didChangePlayStateEvent -= this.HandleSongDidChangeState;
			this._songVolumeSlider.slider.didChangeValueEvent -= this.HandleSongVolumeSliderDidChangeValue;
			this._songVolumeSlider.toggleDidChangeValueEvent -= this.HandleSongVolumeToggleDidChangeValue;
			this._songSpeedSlider.slider.didChangeValueEvent -= this.HandleSongSpeedSliderDidChangeValue;
			this._songSpeedSlider.toggleDidChangeValueEvent -= this.HandleSongSpeedToggleDidChangeValue;
			this._metronomeVolumeSlider.slider.didChangeValueEvent -= this.HandleMetronomeVolumeSliderDidChangeValue;
			this._metronomeVolumeSlider.toggleDidChangeValueEvent -= this.HandleMetronomeVolumeToggleDidChangeValue;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00011585 File Offset: 0x0000F785
		protected void Update()
		{
			if (this.isPlaying)
			{
				this.RefreshSongTime();
				this.RefreshBeatTime();
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0001159B File Offset: 0x0000F79B
		private void HandleSongVolumeSliderDidChangeValue(float value)
		{
			this._songAudioController.volume = value;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000115A9 File Offset: 0x0000F7A9
		private void HandleSongVolumeToggleDidChangeValue(bool isOn)
		{
			this._songAudioController.mute = !isOn;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000115BA File Offset: 0x0000F7BA
		private void RefreshSongVolume()
		{
			this._songVolumeSlider.slider.value = this._songAudioController.volume;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000115D7 File Offset: 0x0000F7D7
		private void HandleSongSpeedSliderDidChangeValue(float value)
		{
			this._songAudioController.pitch = value;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000115E5 File Offset: 0x0000F7E5
		private void HandleSongSpeedToggleDidChangeValue(bool isOn)
		{
			if (isOn)
			{
				this.HandleSongSpeedSliderDidChangeValue(this._songSpeedSlider.slider.value);
				return;
			}
			this.HandleSongSpeedSliderDidChangeValue(1f);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000023E9 File Offset: 0x000005E9
		private void HandleMetronomeVolumeSliderDidChangeValue(float value)
		{
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000023E9 File Offset: 0x000005E9
		private void HandleMetronomeVolumeToggleDidChangeValue(bool isOn)
		{
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0001160C File Offset: 0x0000F80C
		private void HandleSongDidChangeState()
		{
			this.RefreshUI();
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0005507C File Offset: 0x0005327C
		private void RefreshUI()
		{
			bool isPlaying = this.isPlaying;
			this._playButtonImage.enabled = !isPlaying;
			this._pauseButtonImage.enabled = isPlaying;
			this.RefreshSongTime();
			this.RefreshBeatTime();
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00011614 File Offset: 0x0000F814
		private void RefreshSongTime()
		{
			this._songTimeTitleValue.valueText = this._songAudioController.time.MinSecMillisecDurationText();
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000550B8 File Offset: 0x000532B8
		private void RefreshBeatTime()
		{
			BeatmapData beatmapData = this._projectController.beatmapData;
			BeatmapObjectBeatIndex beatmapObjectBeatIndex = (beatmapData != null) ? beatmapData.BeatIndexFromSongTime(this._songAudioController.time) : new BeatmapObjectBeatIndex(0, 0);
			this._beatTimeTitleValue.valueText = string.Format("{0}:{1:000}", beatmapObjectBeatIndex.beat, beatmapObjectBeatIndex.milliseconds);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00011631 File Offset: 0x0000F831
		public void PlaySong()
		{
			if (!this.isPlaying)
			{
				this._songAudioController.Play();
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00011646 File Offset: 0x0000F846
		public void PauseSong()
		{
			if (this.isPlaying)
			{
				this._songAudioController.Pause();
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0001165B File Offset: 0x0000F85B
		public void PlayOrPauseSong()
		{
			this._songAudioController.PlayOrPause();
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00011668 File Offset: 0x0000F868
		public void StopSong()
		{
			this._songAudioController.Stop();
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00011675 File Offset: 0x0000F875
		public void GoToSongStart()
		{
			this._songAudioController.GoToStart();
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00011682 File Offset: 0x0000F882
		public void GoToSongEnd()
		{
			this._songAudioController.GoToEnd();
		}

		// Token: 0x0400175C RID: 5980
		[SerializeField]
		private Button _goToStartButton;

		// Token: 0x0400175D RID: 5981
		[SerializeField]
		private Button _goToEndButton;

		// Token: 0x0400175E RID: 5982
		[SerializeField]
		private Button _playButton;

		// Token: 0x0400175F RID: 5983
		[SerializeField]
		private Image _playButtonImage;

		// Token: 0x04001760 RID: 5984
		[SerializeField]
		private Image _pauseButtonImage;

		// Token: 0x04001761 RID: 5985
		[Space]
		[SerializeField]
		private UIToggleSlider _songSpeedSlider;

		// Token: 0x04001762 RID: 5986
		[SerializeField]
		private UIToggleSlider _songVolumeSlider;

		// Token: 0x04001763 RID: 5987
		[SerializeField]
		private UIToggleSlider _metronomeVolumeSlider;

		// Token: 0x04001764 RID: 5988
		[Space]
		[SerializeField]
		private UITitleValue _songTimeTitleValue;

		// Token: 0x04001765 RID: 5989
		[SerializeField]
		private UITitleValue _beatTimeTitleValue;

		// Token: 0x04001766 RID: 5990
		[Inject]
		private SongAudioController _songAudioController;

		// Token: 0x04001767 RID: 5991
		[Inject]
		private ProjectController _projectController;

		// Token: 0x04001768 RID: 5992
		private ButtonBinder _buttonBinder;
	}
}
