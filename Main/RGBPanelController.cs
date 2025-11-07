using System;
using HMUI;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class RGBPanelController : MonoBehaviour
{
	// Token: 0x1400007E RID: 126
	// (add) Token: 0x06000EF1 RID: 3825 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
	// (remove) Token: 0x06000EF2 RID: 3826 RVA: 0x0003C800 File Offset: 0x0003AA00
	public event Action<Color, ColorChangeUIEventType> colorDidChangeEvent;

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0000B7B5 File Offset: 0x000099B5
	// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0000B7BD File Offset: 0x000099BD
	public Color color
	{
		get
		{
			return this._color;
		}
		set
		{
			this._color = value;
			this.RefreshSlidersColors();
			this.RefreshSlidersValues();
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0003C838 File Offset: 0x0003AA38
	protected void Awake()
	{
		this._redSlider.colorDidChangeEvent += this.HandleSliderColorDidChange;
		this._greenSlider.colorDidChangeEvent += this.HandleSliderColorDidChange;
		this._blueSlider.colorDidChangeEvent += this.HandleSliderColorDidChange;
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x0003C88C File Offset: 0x0003AA8C
	protected void OnDestroy()
	{
		if (this._redSlider != null)
		{
			this._redSlider.colorDidChangeEvent -= this.HandleSliderColorDidChange;
		}
		if (this._greenSlider != null)
		{
			this._greenSlider.colorDidChangeEvent -= this.HandleSliderColorDidChange;
		}
		if (this._blueSlider != null)
		{
			this._blueSlider.colorDidChangeEvent -= this.HandleSliderColorDidChange;
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0000B7D2 File Offset: 0x000099D2
	private void HandleSliderColorDidChange(ColorGradientSlider slider, Color color, ColorChangeUIEventType colorChangeUIEventType)
	{
		this._color = color;
		Action<Color, ColorChangeUIEventType> action = this.colorDidChangeEvent;
		if (action != null)
		{
			action(this._color, colorChangeUIEventType);
		}
		this.RefreshSlidersColors();
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0003C908 File Offset: 0x0003AB08
	private void RefreshSlidersValues()
	{
		this._redSlider.normalizedValue = this._color.r;
		this._greenSlider.normalizedValue = this._color.g;
		this._blueSlider.normalizedValue = this._color.b;
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0003C958 File Offset: 0x0003AB58
	private void RefreshSlidersColors()
	{
		this._redSlider.SetColors(this._color.ColorWithR(0f), this._color.ColorWithR(1f));
		this._greenSlider.SetColors(this._color.ColorWithG(0f), this._color.ColorWithG(1f));
		this._blueSlider.SetColors(this._color.ColorWithB(0f), this._color.ColorWithB(1f));
	}

	// Token: 0x04000F4F RID: 3919
	[SerializeField]
	private ColorGradientSlider _redSlider;

	// Token: 0x04000F50 RID: 3920
	[SerializeField]
	private ColorGradientSlider _greenSlider;

	// Token: 0x04000F51 RID: 3921
	[SerializeField]
	private ColorGradientSlider _blueSlider;

	// Token: 0x04000F53 RID: 3923
	private Color _color;
}
