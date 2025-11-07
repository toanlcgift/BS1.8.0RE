using System;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class HSVPanelController : MonoBehaviour
{
	// Token: 0x1400007C RID: 124
	// (add) Token: 0x06000EDF RID: 3807 RVA: 0x0003C584 File Offset: 0x0003A784
	// (remove) Token: 0x06000EE0 RID: 3808 RVA: 0x0003C5BC File Offset: 0x0003A7BC
	public event Action<Color, ColorChangeUIEventType> colorDidChangeEvent;

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0000B665 File Offset: 0x00009865
	// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0000B68D File Offset: 0x0000988D
	public Color color
	{
		get
		{
			return Color.HSVToRGB(this._hsvColor.x, this._hsvColor.y, this._hsvColor.z);
		}
		set
		{
			Color.RGBToHSV(value, out this._hsvColor.x, out this._hsvColor.y, out this._hsvColor.z);
			this.RefreshSlidersColors();
			this.RefreshSlidersValues();
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0000B6C2 File Offset: 0x000098C2
	protected void Awake()
	{
		this._colorSaturationValueSlider.colorSaturationOrValueDidChangeEvent += this.HandleColorSaturationOrValueDidChange;
		this._colorHueSlider.colorHueDidChangeEvent += this.HandleColorHueDidChange;
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0003C5F4 File Offset: 0x0003A7F4
	protected void OnDestroy()
	{
		if (this._colorSaturationValueSlider != null)
		{
			this._colorSaturationValueSlider.colorSaturationOrValueDidChangeEvent -= this.HandleColorSaturationOrValueDidChange;
		}
		if (this._colorHueSlider != null)
		{
			this._colorHueSlider.colorHueDidChangeEvent -= this.HandleColorHueDidChange;
		}
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0003C64C File Offset: 0x0003A84C
	private void HandleColorSaturationOrValueDidChange(ColorSaturationValueSlider slider, Vector2 colorSaturationAndValue, ColorChangeUIEventType colorChangeUIEventType)
	{
		this._hsvColor.y = colorSaturationAndValue.x;
		this._hsvColor.z = colorSaturationAndValue.y;
		Action<Color, ColorChangeUIEventType> action = this.colorDidChangeEvent;
		if (action != null)
		{
			action(Color.HSVToRGB(this._hsvColor.x, this._hsvColor.y, this._hsvColor.z), colorChangeUIEventType);
		}
		this.RefreshSlidersColors();
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0003C6BC File Offset: 0x0003A8BC
	private void HandleColorHueDidChange(ColorHueSlider slider, float hue, ColorChangeUIEventType colorChangeUIEventType)
	{
		this._hsvColor.x = hue;
		Action<Color, ColorChangeUIEventType> action = this.colorDidChangeEvent;
		if (action != null)
		{
			action(Color.HSVToRGB(this._hsvColor.x, this._hsvColor.y, this._hsvColor.z), colorChangeUIEventType);
		}
		this.RefreshSlidersColors();
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0000B6F2 File Offset: 0x000098F2
	private void RefreshSlidersValues()
	{
		this._colorSaturationValueSlider.normalizedValue = new Vector2(this._hsvColor.y, this._hsvColor.z);
		this._colorHueSlider.normalizedValue = this._hsvColor.x;
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0000B730 File Offset: 0x00009930
	private void RefreshSlidersColors()
	{
		this._colorSaturationValueSlider.SetHue(this._hsvColor.x);
	}

	// Token: 0x04000F44 RID: 3908
	[SerializeField]
	private ColorSaturationValueSlider _colorSaturationValueSlider;

	// Token: 0x04000F45 RID: 3909
	[SerializeField]
	private ColorHueSlider _colorHueSlider;

	// Token: 0x04000F47 RID: 3911
	private Vector3 _hsvColor;
}
