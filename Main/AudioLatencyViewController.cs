using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BD RID: 957
public class AudioLatencyViewController : ViewController
{
	// Token: 0x0600119E RID: 4510 RVA: 0x00042ADC File Offset: 0x00040CDC
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._setupCanvasGroup.blocksRaycasts = false;
			this._slider.valueDidChangeEvent += this.SliderValueDidChange;
			this._toggleBinder = new ToggleBinder();
			this._toggleBinder.AddBinding(this._overrideAudioLatencyToggle, new Action<bool>(this.HandleOverrideAudioLatencyToggleValueChanged));
		}
		this._visualMetronome.zeroOffset = this._audioLatency;
		this._overrideAudioLatencyToggle.isOn = this._overrideAudioLatency;
		this._slider.value = (this._overrideAudioLatency ? this._audioLatency : 0f);
		this.RefreshVisuals(this._overrideAudioLatency);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0000D6AA File Offset: 0x0000B8AA
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this._slider != null)
		{
			this._slider.valueDidChangeEvent -= this.SliderValueDidChange;
		}
		this._toggleBinder.ClearBindings();
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
	private void SliderValueDidChange(RangeValuesTextSlider slider, float value)
	{
		this._audioLatency.value = value;
		this._visualMetronome.zeroOffset = value;
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0000D6FC File Offset: 0x0000B8FC
	private void HandleOverrideAudioLatencyToggleValueChanged(bool isOn)
	{
		this._overrideAudioLatency.value = isOn;
		this.RefreshVisuals(isOn);
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x00042BA0 File Offset: 0x00040DA0
	public void RefreshVisuals(bool overrideAutioLatencyIsEnabled)
	{
		if (overrideAutioLatencyIsEnabled)
		{
			this._setupCanvasGroup.blocksRaycasts = true;
			this._setupCanvasGroup.alpha = 1f;
			this._visualMetronome.enabled = true;
			this._slider.value = this._audioLatency;
			return;
		}
		this._setupCanvasGroup.blocksRaycasts = false;
		this._setupCanvasGroup.alpha = this._disabledAlpha;
		this._visualMetronome.enabled = false;
		this._slider.value = 0f;
	}

	// Token: 0x04001168 RID: 4456
	[SerializeField]
	private FloatSO _audioLatency;

	// Token: 0x04001169 RID: 4457
	[SerializeField]
	private BoolSO _overrideAudioLatency;

	// Token: 0x0400116A RID: 4458
	[Space]
	[SerializeField]
	private CanvasGroup _setupCanvasGroup;

	// Token: 0x0400116B RID: 4459
	[SerializeField]
	private Toggle _overrideAudioLatencyToggle;

	// Token: 0x0400116C RID: 4460
	[SerializeField]
	private RangeValuesTextSlider _slider;

	// Token: 0x0400116D RID: 4461
	[SerializeField]
	private VisualMetronome _visualMetronome;

	// Token: 0x0400116E RID: 4462
	[Space]
	[SerializeField]
	private float _disabledAlpha = 0.5f;

	// Token: 0x0400116F RID: 4463
	private ToggleBinder _toggleBinder;
}
