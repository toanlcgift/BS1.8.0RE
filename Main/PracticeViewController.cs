using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x02000400 RID: 1024
public class PracticeViewController : ViewController
{
	// Token: 0x140000B0 RID: 176
	// (add) Token: 0x06001349 RID: 4937 RVA: 0x00047D80 File Offset: 0x00045F80
	// (remove) Token: 0x0600134A RID: 4938 RVA: 0x00047DB8 File Offset: 0x00045FB8
	public event Action didPressPlayButtonEvent;

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x0600134B RID: 4939 RVA: 0x0000E86B File Offset: 0x0000CA6B
	public PracticeSettings practiceSettings
	{
		get
		{
			return this._practiceSettings;
		}
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x00047DF0 File Offset: 0x00045FF0
	public void Init(IBeatmapLevel level)
	{
		this._level = level;
		this._practiceSettings = this._playerDataModel.playerData.practiceSettings;
		this._maxStartSongTime = Mathf.Max(this._level.beatmapLevelData.audioClip.length - 1f, 0f);
		this._songStartSlider.minValue = 0f;
		this._songStartSlider.maxValue = this._maxStartSongTime;
		this._practiceSettings.songSpeedMul = Mathf.Clamp(this._practiceSettings.songSpeedMul, this._speedSlider.minValue, this._speedSlider.maxValue);
		this._practiceSettings.startSongTime = 0f;
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00047EA8 File Offset: 0x000460A8
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._playButton, new Action(this.PlayButtonPressed));
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._speedSlider.valueDidChangeEvent += this.HandleSpeedSliderValueDidChange;
			this._songStartSlider.valueDidChangeEvent += this.HandleSongStartSliderValueDidChange;
			this._songNameText.text = this._level.songName;
			this.RefreshUI();
		}
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x0000E873 File Offset: 0x0000CA73
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._speedSlider.valueDidChangeEvent -= this.HandleSpeedSliderValueDidChange;
			this._songStartSlider.valueDidChangeEvent -= this.HandleSongStartSliderValueDidChange;
		}
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x00047F24 File Offset: 0x00046124
	private void PlayPreview()
	{
		this._songPreviewPlayer.CrossfadeTo(this._level.beatmapLevelData.audioClip, this._practiceSettings.startSongTime, 5f, 1f);
		this._currentPlayingStartTime = this._practiceSettings.startSongTime;
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x0000E8A6 File Offset: 0x0000CAA6
	private void RefreshUI()
	{
		this._songStartSlider.value = this._practiceSettings.startSongTime;
		this._speedSlider.value = this._practiceSettings.songSpeedMul;
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
	private void HandleSpeedSliderValueDidChange(RangeValuesTextSlider slider, float value)
	{
		this._practiceSettings.songSpeedMul = value;
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
	private void HandleSongStartSliderValueDidChange(RangeValuesTextSlider slider, float value)
	{
		this._practiceSettings.startSongTime = value;
		if (Mathf.Abs(this._currentPlayingStartTime - value) > 3f)
		{
			this.PlayPreview();
		}
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x0000E90A File Offset: 0x0000CB0A
	private void PlayButtonPressed()
	{
		this._practiceSettings.startSongTime = Mathf.Min(this._practiceSettings.startSongTime, this._maxStartSongTime);
		Action action = this.didPressPlayButtonEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x040012FB RID: 4859
	[SerializeField]
	private TimeSlider _songStartSlider;

	// Token: 0x040012FC RID: 4860
	[SerializeField]
	private PercentSlider _speedSlider;

	// Token: 0x040012FD RID: 4861
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	// Token: 0x040012FE RID: 4862
	[Space]
	[SerializeField]
	private Button _playButton;

	// Token: 0x040012FF RID: 4863
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04001300 RID: 4864
	[Inject]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x04001301 RID: 4865
	private const float kWaitBeforePlayPreviewAfterPreviewStartValueChanged = 1f;

	// Token: 0x04001302 RID: 4866
	private const float kMinValueChangeToInstantPlayPreview = 3f;

	// Token: 0x04001304 RID: 4868
	private PracticeSettings _practiceSettings;

	// Token: 0x04001305 RID: 4869
	private float _currentPlayingStartTime;

	// Token: 0x04001306 RID: 4870
	private float _maxStartSongTime;

	// Token: 0x04001307 RID: 4871
	private IBeatmapLevel _level;
}
