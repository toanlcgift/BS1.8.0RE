using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x0200027B RID: 635
public class MenuLightsManager : MonoBehaviour
{
	// Token: 0x06000AA8 RID: 2728 RVA: 0x0000854D File Offset: 0x0000674D
	protected IEnumerator Start()
	{
		base.enabled = false;
		yield return null;
		if (this._preset == null)
		{
			this.SetColorPreset(this._defaultPreset, false);
		}
		yield break;
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0000855C File Offset: 0x0000675C
	protected void Update()
	{
		if (!this.SetColorsFromPreset(this._preset, Time.deltaTime * this._smooth))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x00031770 File Offset: 0x0002F970
	private bool IsColorVeryCloseToColor(Color color0, Color color1)
	{
		return Mathf.Abs(color0.r - color1.r) < 0.002f && Mathf.Abs(color0.g - color1.g) < 0.002f && Mathf.Abs(color0.b - color1.b) < 0.002f && Mathf.Abs(color0.a - color1.a) < 0.002f;
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0000857F File Offset: 0x0000677F
	private void SetColor(int lightId, Color color)
	{
		this._lightManager.SetColorForId(lightId, color);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0000858E File Offset: 0x0000678E
	private Color CurrentColorForID(int lightId)
	{
		return this._lightManager.GetColorForId(lightId);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x000317E4 File Offset: 0x0002F9E4
	private bool SetColorsFromPreset(MenuLightsPresetSO preset, float interpolationFactor = 1f)
	{
		bool flag = true;
		for (int i = 0; i < this._preset.lightIdColorPairs.Length; i++)
		{
			MenuLightsPresetSO.LightIdColorPair lightIdColorPair = this._preset.lightIdColorPairs[i];
			Color a = this.CurrentColorForID(lightIdColorPair.lightId);
			Color lightColor = lightIdColorPair.lightColor;
			Color color = Color.Lerp(a, lightColor, interpolationFactor);
			this.SetColor(lightIdColorPair.lightId, color);
			if (!this.IsColorVeryCloseToColor(color, lightColor))
			{
				flag = false;
			}
		}
		return !flag;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00031858 File Offset: 0x0002FA58
	private void RefreshLightsDictForPreset(MenuLightsPresetSO preset)
	{
		HashSet<int> hashSet = new HashSet<int>();
		foreach (MenuLightsPresetSO.LightIdColorPair lightIdColorPair in preset.lightIdColorPairs)
		{
			hashSet.Add(lightIdColorPair.lightId);
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0000859C File Offset: 0x0000679C
	public void SetColorPreset(MenuLightsPresetSO preset, bool animated)
	{
		if (this._preset == preset)
		{
			return;
		}
		this.RefreshLightsDictForPreset(preset);
		this._preset = preset;
		if (animated)
		{
			base.enabled = true;
			return;
		}
		this.SetColorsFromPreset(this._preset, 1f);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x000085D8 File Offset: 0x000067D8
	public void RefreshColors()
	{
		if (this._preset != this._defaultPreset)
		{
			this.SetColorPreset(this._defaultPreset, false);
			return;
		}
		this.SetColorsFromPreset(this._preset, 1f);
	}

	// Token: 0x04000B0E RID: 2830
	[SerializeField]
	private MenuLightsPresetSO _defaultPreset;

	// Token: 0x04000B0F RID: 2831
	[SerializeField]
	private float _smooth = 8f;

	// Token: 0x04000B10 RID: 2832
	[Inject]
	private LightWithIdManager _lightManager;

	// Token: 0x04000B11 RID: 2833
	private MenuLightsPresetSO _preset;
}
