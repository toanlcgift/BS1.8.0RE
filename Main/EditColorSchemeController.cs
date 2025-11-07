using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034F RID: 847
public class EditColorSchemeController : MonoBehaviour
{
	// Token: 0x1400007A RID: 122
	// (add) Token: 0x06000ED2 RID: 3794 RVA: 0x0003C250 File Offset: 0x0003A450
	// (remove) Token: 0x06000ED3 RID: 3795 RVA: 0x0003C288 File Offset: 0x0003A488
	public event Action didFinishEvent;

	// Token: 0x1400007B RID: 123
	// (add) Token: 0x06000ED4 RID: 3796 RVA: 0x0003C2C0 File Offset: 0x0003A4C0
	// (remove) Token: 0x06000ED5 RID: 3797 RVA: 0x0003C2F8 File Offset: 0x0003A4F8
	public event Action<ColorScheme> didChangeColorSchemeEvent;

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0000B61F File Offset: 0x0000981F
	public void SetColorScheme(ColorScheme colorScheme)
	{
		this._colorSchemeColorsToggleGroup.SetColorScheme(colorScheme);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0003C330 File Offset: 0x0003A530
	protected void Start()
	{
		this._colorSchemeColorsToggleGroup.selectedColorDidChangeEvent += this.HandleColorSchemeColorsToggleGroupSelectedColorDidChange;
		this._rgbPanelController.colorDidChangeEvent += this.HandleRGBPanelControllerColorDidChange;
		this._hsvPanelController.colorDidChangeEvent += this.HandleHSVPanelControllerColorDidChange;
		this._previousColorPanelController.colorWasSelectedEvent += this.HandlePreviousColorPanelControllerColorWasSelected;
		this._rgbPanelController.color = this._colorSchemeColorsToggleGroup.color;
		this._hsvPanelController.color = this._colorSchemeColorsToggleGroup.color;
		this._previousColorPanelController.AddColor(this._colorSchemeColorsToggleGroup.color);
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._closeButton, delegate
		{
			Action action = this.didFinishEvent;
			if (action == null)
			{
				return;
			}
			action();
		});
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0003C404 File Offset: 0x0003A604
	protected void OnDestroy()
	{
		if (this._colorSchemeColorsToggleGroup != null)
		{
			this._colorSchemeColorsToggleGroup.selectedColorDidChangeEvent -= this.HandleColorSchemeColorsToggleGroupSelectedColorDidChange;
		}
		if (this._rgbPanelController != null)
		{
			this._rgbPanelController.colorDidChangeEvent -= this.HandleRGBPanelControllerColorDidChange;
		}
		if (this._previousColorPanelController != null)
		{
			this._previousColorPanelController.colorWasSelectedEvent -= this.HandlePreviousColorPanelControllerColorWasSelected;
		}
		this._buttonBinder.ClearBindings();
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0000B62D File Offset: 0x0000982D
	private void HandleColorSchemeColorsToggleGroupSelectedColorDidChange(Color color)
	{
		this._rgbPanelController.color = color;
		this._hsvPanelController.color = color;
		this._previousColorPanelController.AddColor(color);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0003C48C File Offset: 0x0003A68C
	private void HandleRGBPanelControllerColorDidChange(Color color, ColorChangeUIEventType colorChangeUIEventType)
	{
		this._colorSchemeColorsToggleGroup.color = color;
		this._hsvPanelController.color = color;
		if (colorChangeUIEventType == ColorChangeUIEventType.PointerUp)
		{
			this._previousColorPanelController.AddColor(color);
			ColorScheme obj = this._colorSchemeColorsToggleGroup.CreateColorSchemeFromEditedColors();
			Action<ColorScheme> action = this.didChangeColorSchemeEvent;
			if (action == null)
			{
				return;
			}
			action(obj);
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0003C4E0 File Offset: 0x0003A6E0
	private void HandleHSVPanelControllerColorDidChange(Color color, ColorChangeUIEventType colorChangeUIEventType)
	{
		this._rgbPanelController.color = color;
		this._colorSchemeColorsToggleGroup.color = color;
		if (colorChangeUIEventType == ColorChangeUIEventType.PointerUp)
		{
			this._previousColorPanelController.AddColor(color);
			ColorScheme obj = this._colorSchemeColorsToggleGroup.CreateColorSchemeFromEditedColors();
			Action<ColorScheme> action = this.didChangeColorSchemeEvent;
			if (action == null)
			{
				return;
			}
			action(obj);
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0003C534 File Offset: 0x0003A734
	private void HandlePreviousColorPanelControllerColorWasSelected(Color color)
	{
		this._colorSchemeColorsToggleGroup.color = color;
		this._rgbPanelController.color = color;
		this._hsvPanelController.color = color;
		ColorScheme obj = this._colorSchemeColorsToggleGroup.CreateColorSchemeFromEditedColors();
		Action<ColorScheme> action = this.didChangeColorSchemeEvent;
		if (action == null)
		{
			return;
		}
		action(obj);
	}

	// Token: 0x04000F3C RID: 3900
	[SerializeField]
	private ColorSchemeColorsToggleGroup _colorSchemeColorsToggleGroup;

	// Token: 0x04000F3D RID: 3901
	[SerializeField]
	private RGBPanelController _rgbPanelController;

	// Token: 0x04000F3E RID: 3902
	[SerializeField]
	private HSVPanelController _hsvPanelController;

	// Token: 0x04000F3F RID: 3903
	[SerializeField]
	private PreviousColorPanelController _previousColorPanelController;

	// Token: 0x04000F40 RID: 3904
	[SerializeField]
	private Button _closeButton;

	// Token: 0x04000F43 RID: 3907
	private ButtonBinder _buttonBinder;
}
