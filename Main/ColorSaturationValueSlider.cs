using System;
using HMUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000347 RID: 839
public class ColorSaturationValueSlider : Slider2D, IPointerUpHandler, IEventSystemHandler
{
	// Token: 0x14000078 RID: 120
	// (add) Token: 0x06000E97 RID: 3735 RVA: 0x0003BAA4 File Offset: 0x00039CA4
	// (remove) Token: 0x06000E98 RID: 3736 RVA: 0x0003BADC File Offset: 0x00039CDC
	public event Action<ColorSaturationValueSlider, Vector2, ColorChangeUIEventType> colorSaturationOrValueDidChangeEvent;

	// Token: 0x06000E99 RID: 3737 RVA: 0x0000B330 File Offset: 0x00009530
	protected override void Awake()
	{
		base.Awake();
		base.normalizedValueDidChangeEvent += this.HandleNormalizedValueDidChange;
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0000B34A File Offset: 0x0000954A
	protected override void OnDestroy()
	{
		base.normalizedValueDidChangeEvent -= this.HandleNormalizedValueDidChange;
		base.OnDestroy();
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0000B364 File Offset: 0x00009564
	public void SetHue(float hue)
	{
		this._hue = hue;
		this.UpdateVisuals();
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0003BB14 File Offset: 0x00039D14
	protected override void UpdateVisuals()
	{
		base.UpdateVisuals();
		if (Color.HSVToRGB(this._hue, base.normalizedValue.x, base.normalizedValue.y).grayscale > 0.7f)
		{
			base.handleColor = this._darkColor;
		}
		else
		{
			base.handleColor = this._lightColor;
		}
		Graphic[] graphics = this._graphics;
		for (int i = 0; i < graphics.Length; i++)
		{
			graphics[i].color = Color.HSVToRGB(this._hue, 1f, 1f);
		}
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000B373 File Offset: 0x00009573
	private void HandleNormalizedValueDidChange(Slider2D slider, Vector2 normalizedValue)
	{
		Action<ColorSaturationValueSlider, Vector2, ColorChangeUIEventType> action = this.colorSaturationOrValueDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this, normalizedValue, ColorChangeUIEventType.Drag);
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000B388 File Offset: 0x00009588
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Action<ColorSaturationValueSlider, Vector2, ColorChangeUIEventType> action = this.colorSaturationOrValueDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this, base.normalizedValue, ColorChangeUIEventType.PointerUp);
	}

	// Token: 0x04000F0F RID: 3855
	[SerializeField]
	private float _hue;

	// Token: 0x04000F10 RID: 3856
	[SerializeField]
	private Graphic[] _graphics;

	// Token: 0x04000F11 RID: 3857
	[SerializeField]
	private Color _darkColor;

	// Token: 0x04000F12 RID: 3858
	[SerializeField]
	private Color _lightColor;
}
