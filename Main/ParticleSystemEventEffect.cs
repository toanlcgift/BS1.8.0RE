using System;
using UnityEngine;
using Zenject;

// Token: 0x02000290 RID: 656
public class ParticleSystemEventEffect : MonoBehaviour
{
	// Token: 0x06000B08 RID: 2824 RVA: 0x00033334 File Offset: 0x00031534
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		this._mainModule = this._particleSystem.main;
		this._particles = new ParticleSystem.Particle[this._particleSystem.main.maxParticles];
		this._lightIsOn = this._lightOnStart;
		this._offColor = this._lightColor0.color.ColorWithAlpha(0f);
		this._particleColor = (this._lightIsOn ? this._lightColor0 : this._offColor);
		this._particleSpeed = this._particleSpeedMultiplier;
		this.RefreshParticles();
		base.enabled = false;
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00008A50 File Offset: 0x00006C50
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000333E8 File Offset: 0x000315E8
	protected void Update()
	{
		if (!this._lightIsOn && this._highlightValue == 0f)
		{
			return;
		}
		this._particleColor = Color.Lerp(this._afterHighlightColor, this._highlightColor, this._highlightValue);
		this._highlightValue = Mathf.Lerp(this._highlightValue, 0f, Time.deltaTime * this.kFadeSpeed);
		if (this._highlightValue < 0.0001f)
		{
			this._highlightValue = 0f;
			this._particleColor = this._afterHighlightColor;
			base.enabled = false;
		}
		this.RefreshParticles();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0003347C File Offset: 0x0003167C
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._colorEvent)
		{
			if (beatmapEventData.value == 0)
			{
				this._lightIsOn = false;
				this._highlightValue = 0f;
				base.enabled = false;
				this._particleColor = this._offColor;
				this.RefreshParticles();
				return;
			}
			if (beatmapEventData.value == 1 || beatmapEventData.value == 5)
			{
				this._lightIsOn = true;
				this._highlightValue = 0f;
				base.enabled = false;
				Color color = (beatmapEventData.value == 1) ? this._lightColor0.color : this._lightColor1.color;
				this._particleColor = color;
				this._offColor = color.ColorWithAlpha(0f);
				this.RefreshParticles();
				return;
			}
			if (beatmapEventData.value == 2 || beatmapEventData.value == 6)
			{
				this._lightIsOn = true;
				this._highlightValue = 1f;
				base.enabled = true;
				this._highlightColor = ((beatmapEventData.value == 2) ? this._highlightColor0 : this._highlightColor1);
				this._offColor = this._highlightColor.ColorWithAlpha(0f);
				this._particleColor = this._highlightColor;
				this._afterHighlightColor = ((beatmapEventData.value == 2) ? this._lightColor0 : this._lightColor1);
				return;
			}
			if (beatmapEventData.value == 3 || beatmapEventData.value == 7 || beatmapEventData.value == -1)
			{
				this._lightIsOn = true;
				this._highlightValue = 1f;
				base.enabled = true;
				this._highlightColor = ((beatmapEventData.value == 3) ? this._highlightColor0 : this._highlightColor1);
				this._offColor = this._highlightColor.ColorWithAlpha(0f);
				this._particleColor = this._highlightColor;
				this._afterHighlightColor = this._offColor;
			}
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00033654 File Offset: 0x00031854
	private void RefreshParticles()
	{
		this._mainModule.startColor = this._particleColor;
		this._particleSystem.GetParticles(this._particles, this._particles.Length);
		for (int i = 0; i < this._particleSystem.particleCount; i++)
		{
			this._particles[i].startColor = this._particleColor;
		}
		this._particleSystem.SetParticles(this._particles, this._particleSystem.particleCount);
	}

	// Token: 0x04000B96 RID: 2966
	[SerializeField]
	private ColorSO _lightColor0;

	// Token: 0x04000B97 RID: 2967
	[SerializeField]
	private ColorSO _lightColor1;

	// Token: 0x04000B98 RID: 2968
	[SerializeField]
	private ColorSO _highlightColor0;

	// Token: 0x04000B99 RID: 2969
	[SerializeField]
	private ColorSO _highlightColor1;

	// Token: 0x04000B9A RID: 2970
	[SerializeField]
	private bool _lightOnStart;

	// Token: 0x04000B9B RID: 2971
	[SerializeField]
	private BeatmapEventType _colorEvent;

	// Token: 0x04000B9C RID: 2972
	[Space]
	[SerializeField]
	private ParticleSystem _particleSystem;

	// Token: 0x04000B9D RID: 2973
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000B9E RID: 2974
	private bool _lightIsOn;

	// Token: 0x04000B9F RID: 2975
	private Color _offColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000BA0 RID: 2976
	private float _highlightValue;

	// Token: 0x04000BA1 RID: 2977
	private Color _afterHighlightColor;

	// Token: 0x04000BA2 RID: 2978
	private Color _highlightColor;

	// Token: 0x04000BA3 RID: 2979
	private float kFadeSpeed = 2f;

	// Token: 0x04000BA4 RID: 2980
	private float _particleSpeedMultiplier = 1f;

	// Token: 0x04000BA5 RID: 2981
	private ParticleSystem.MainModule _mainModule;

	// Token: 0x04000BA6 RID: 2982
	private ParticleSystem.Particle[] _particles;

	// Token: 0x04000BA7 RID: 2983
	private Color _particleColor;

	// Token: 0x04000BA8 RID: 2984
	private float _particleSpeed;
}
