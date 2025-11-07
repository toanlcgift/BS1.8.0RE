using System;
using HMUI;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class ColorSchemeColorsToggleGroup : MonoBehaviour
{
	// Token: 0x14000079 RID: 121
	// (add) Token: 0x06000EA4 RID: 3748 RVA: 0x0003BBD0 File Offset: 0x00039DD0
	// (remove) Token: 0x06000EA5 RID: 3749 RVA: 0x0003BC08 File Offset: 0x00039E08
	public event Action<Color> selectedColorDidChangeEvent;

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0000B3C8 File Offset: 0x000095C8
	// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0000B3D5 File Offset: 0x000095D5
	public Color color
	{
		get
		{
			return this._selectedColorToggleController.color;
		}
		set
		{
			this._selectedColorToggleController.color = value;
		}
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0003BC40 File Offset: 0x00039E40
	public void SetColorScheme(ColorScheme colorScheme)
	{
		this._colorScheme = colorScheme;
		this._saberAColorToggleController.color = colorScheme.saberAColor;
		this._saberBColorToggleController.color = colorScheme.saberBColor;
		this._environmentColor0ToggleController.color = colorScheme.environmentColor0;
		this._environmentColor1ToggleController.color = colorScheme.environmentColor1;
		this._obstaclesColorToggleController.color = colorScheme.obstaclesColor;
		Action<Color> action = this.selectedColorDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this.color);
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0003BCC0 File Offset: 0x00039EC0
	protected void Awake()
	{
		this._selectedColorToggleController = this._saberAColorToggleController;
		this._saberAColorToggleController.toggle.isOn = true;
		this._toggleBinder = new ToggleBinder();
		this._toggleBinder.AddBinding(this._saberAColorToggleController.toggle, delegate(bool isOn)
		{
			this.HandleToggleWasSelected(this._saberAColorToggleController, isOn);
		});
		this._toggleBinder.AddBinding(this._saberBColorToggleController.toggle, delegate(bool isOn)
		{
			this.HandleToggleWasSelected(this._saberBColorToggleController, isOn);
		});
		this._toggleBinder.AddBinding(this._environmentColor0ToggleController.toggle, delegate(bool isOn)
		{
			this.HandleToggleWasSelected(this._environmentColor0ToggleController, isOn);
		});
		this._toggleBinder.AddBinding(this._environmentColor1ToggleController.toggle, delegate(bool isOn)
		{
			this.HandleToggleWasSelected(this._environmentColor1ToggleController, isOn);
		});
		this._toggleBinder.AddBinding(this._obstaclesColorToggleController.toggle, delegate(bool isOn)
		{
			this.HandleToggleWasSelected(this._obstaclesColorToggleController, isOn);
		});
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0000B3E3 File Offset: 0x000095E3
	protected void OnDestroy()
	{
		this._toggleBinder.ClearBindings();
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0000B3F0 File Offset: 0x000095F0
	private void HandleToggleWasSelected(ColorSchemeColorToggleController toggleController, bool isOn)
	{
		if (isOn)
		{
			this._selectedColorToggleController = toggleController;
			Action<Color> action = this.selectedColorDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action(toggleController.color);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0003BDA0 File Offset: 0x00039FA0
	public ColorScheme CreateColorSchemeFromEditedColors()
	{
		return new ColorScheme(this._colorScheme, this._saberAColorToggleController.color, this._saberBColorToggleController.color, this._environmentColor0ToggleController.color, this._environmentColor1ToggleController.color, this._obstaclesColorToggleController.color);
	}

	// Token: 0x04000F16 RID: 3862
	[SerializeField]
	private ColorSchemeColorToggleController _saberAColorToggleController;

	// Token: 0x04000F17 RID: 3863
	[SerializeField]
	private ColorSchemeColorToggleController _saberBColorToggleController;

	// Token: 0x04000F18 RID: 3864
	[SerializeField]
	private ColorSchemeColorToggleController _environmentColor0ToggleController;

	// Token: 0x04000F19 RID: 3865
	[SerializeField]
	private ColorSchemeColorToggleController _environmentColor1ToggleController;

	// Token: 0x04000F1A RID: 3866
	[SerializeField]
	private ColorSchemeColorToggleController _obstaclesColorToggleController;

	// Token: 0x04000F1C RID: 3868
	private ToggleBinder _toggleBinder;

	// Token: 0x04000F1D RID: 3869
	private ColorSchemeColorToggleController _selectedColorToggleController;

	// Token: 0x04000F1E RID: 3870
	private ColorScheme _colorScheme;
}
