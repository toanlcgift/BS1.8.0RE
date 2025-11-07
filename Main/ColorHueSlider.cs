using System;
using HMUI;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000346 RID: 838
public class ColorHueSlider : CircleSlider, IPointerUpHandler, IEventSystemHandler
{
	// Token: 0x14000077 RID: 119
	// (add) Token: 0x06000E8F RID: 3727 RVA: 0x0003B9E4 File Offset: 0x00039BE4
	// (remove) Token: 0x06000E90 RID: 3728 RVA: 0x0003BA1C File Offset: 0x00039C1C
	public event Action<ColorHueSlider, float, ColorChangeUIEventType> colorHueDidChangeEvent;

	// Token: 0x06000E91 RID: 3729 RVA: 0x0000B2BE File Offset: 0x000094BE
	protected override void Awake()
	{
		base.Awake();
		base.normalizedValueDidChangeEvent += this.HandleNormalizedValueDidChange;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0000B2D8 File Offset: 0x000094D8
	protected override void OnDestroy()
	{
		base.normalizedValueDidChangeEvent -= this.HandleNormalizedValueDidChange;
		base.OnDestroy();
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0003BA54 File Offset: 0x00039C54
	protected override void UpdateVisuals()
	{
		base.UpdateVisuals();
		if (Color.HSVToRGB(base.normalizedValue, 1f, 1f).grayscale > 0.7f)
		{
			base.handleColor = this._darkColor;
			return;
		}
		base.handleColor = this._lightColor;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0000B2F2 File Offset: 0x000094F2
	private void HandleNormalizedValueDidChange(CircleSlider slider, float normalizedValue)
	{
		Action<ColorHueSlider, float, ColorChangeUIEventType> action = this.colorHueDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this, normalizedValue, ColorChangeUIEventType.Drag);
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0000B307 File Offset: 0x00009507
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Action<ColorHueSlider, float, ColorChangeUIEventType> action = this.colorHueDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this, base.normalizedValue, ColorChangeUIEventType.PointerUp);
	}

	// Token: 0x04000F0C RID: 3852
	[SerializeField]
	private Color _darkColor;

	// Token: 0x04000F0D RID: 3853
	[SerializeField]
	private Color _lightColor;
}
