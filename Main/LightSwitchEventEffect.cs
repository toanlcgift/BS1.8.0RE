using System;
using UnityEngine;
using Zenject;

// Token: 0x0200028C RID: 652
public class LightSwitchEventEffect : MonoBehaviour
{
	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00008881 File Offset: 0x00006A81
	public int LightsID
	{
		get
		{
			return this._lightsID;
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00008889 File Offset: 0x00006A89
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x000088A2 File Offset: 0x00006AA2
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00032E38 File Offset: 0x00031038
	protected void Update()
	{
		if (!this._initialized)
		{
			this._lightIsOn = this._lightOnStart;
			this._offColor = this._lightColor0.color.ColorWithAlpha(this._offColorIntensity);
			this.SetColor(this._lightIsOn ? this._lightColor0 : this._offColor);
			this._initialized = true;
		}
		if (!this._lightIsOn && this._highlightValue == 0f)
		{
			base.enabled = false;
			return;
		}
		this.SetColor(Color.Lerp(this._afterHighlightColor, this._highlightColor, this._highlightValue));
		this._highlightValue = Mathf.Lerp(this._highlightValue, 0f, Time.deltaTime * this.kFadeSpeed);
		if (this._highlightValue < 0.0001f)
		{
			this._highlightValue = 0f;
			this.SetColor(this._afterHighlightColor);
			base.enabled = false;
		}
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00032F24 File Offset: 0x00031124
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._event)
		{
			if (beatmapEventData.value == 0)
			{
				this._lightIsOn = false;
				this._highlightValue = 0f;
				base.enabled = false;
				this.SetColor(this._offColor);
			}
			else if (beatmapEventData.value == 1 || beatmapEventData.value == 5)
			{
				this._lightIsOn = true;
				this._highlightValue = 0f;
				base.enabled = false;
				Color color = (beatmapEventData.value == 1) ? this._lightColor0.color : this._lightColor1.color;
				this.SetColor(color);
				this._offColor = color.ColorWithAlpha(this._offColorIntensity);
			}
			else if (beatmapEventData.value == 2 || beatmapEventData.value == 6)
			{
				this._lightIsOn = true;
				this._highlightValue = 1f;
				base.enabled = true;
				this._highlightColor = ((beatmapEventData.value == 2) ? this._highlightColor0 : this._highlightColor1);
				this._offColor = this._highlightColor.ColorWithAlpha(this._offColorIntensity);
				this.SetColor(this._highlightColor);
				this._afterHighlightColor = ((beatmapEventData.value == 2) ? this._lightColor0 : this._lightColor1);
			}
			else if (beatmapEventData.value == 3 || beatmapEventData.value == 7 || beatmapEventData.value == -1)
			{
				this._lightIsOn = true;
				this._highlightValue = 1f;
				base.enabled = true;
				this._highlightColor = ((beatmapEventData.value == 3) ? this._highlightColor0 : this._highlightColor1);
				this._offColor = this._highlightColor.ColorWithAlpha(this._offColorIntensity);
				this.SetColor(this._highlightColor);
				this._afterHighlightColor = this._offColor;
			}
		}
		else if (!this._didProcessFirstEvent)
		{
			this._lightIsOn = false;
			this._highlightValue = 0f;
			base.enabled = false;
			if (beatmapEventData.value == 1 || beatmapEventData.value == 5)
			{
				Color color2 = (beatmapEventData.value == 1) ? this._lightColor0.color : this._lightColor1.color;
				this._offColor = color2.ColorWithAlpha(this._offColorIntensity);
			}
			else if (beatmapEventData.value == 2 || beatmapEventData.value == 6)
			{
				Color color3 = (beatmapEventData.value == 2) ? this._highlightColor0.color : this._highlightColor1.color;
				this._offColor = color3.ColorWithAlpha(this._offColorIntensity);
			}
			else if (beatmapEventData.value == 3 || beatmapEventData.value == 7 || beatmapEventData.value == -1)
			{
				Color color4 = (beatmapEventData.value == 3) ? this._highlightColor0.color : this._highlightColor1.color;
				this._offColor = color4.ColorWithAlpha(this._offColorIntensity);
			}
			this.SetColor(this._offColor);
		}
		this._didProcessFirstEvent = true;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x000088C8 File Offset: 0x00006AC8
	private void SetColor(Color color)
	{
		this._lightManager.SetColorForId(this._lightsID, color);
	}

	// Token: 0x04000B7A RID: 2938
	[SerializeField]
	private ColorSO _lightColor0;

	// Token: 0x04000B7B RID: 2939
	[SerializeField]
	private ColorSO _lightColor1;

	// Token: 0x04000B7C RID: 2940
	[SerializeField]
	private ColorSO _highlightColor0;

	// Token: 0x04000B7D RID: 2941
	[SerializeField]
	private ColorSO _highlightColor1;

	// Token: 0x04000B7E RID: 2942
	[SerializeField]
	private float _offColorIntensity;

	// Token: 0x04000B7F RID: 2943
	[SerializeField]
	private bool _lightOnStart;

	// Token: 0x04000B80 RID: 2944
	[SerializeField]
	private int _lightsID;

	// Token: 0x04000B81 RID: 2945
	[SerializeField]
	private BeatmapEventType _event;

	// Token: 0x04000B82 RID: 2946
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000B83 RID: 2947
	[Inject]
	private LightWithIdManager _lightManager;

	// Token: 0x04000B84 RID: 2948
	private bool _lightIsOn;

	// Token: 0x04000B85 RID: 2949
	private Color _offColor = new Color(0f, 0f, 0f, 0f);

	// Token: 0x04000B86 RID: 2950
	private float _highlightValue;

	// Token: 0x04000B87 RID: 2951
	private Color _afterHighlightColor;

	// Token: 0x04000B88 RID: 2952
	private Color _highlightColor;

	// Token: 0x04000B89 RID: 2953
	private float kFadeSpeed = 2f;

	// Token: 0x04000B8A RID: 2954
	private bool _didProcessFirstEvent;

	// Token: 0x04000B8B RID: 2955
	private bool _initialized;
}
