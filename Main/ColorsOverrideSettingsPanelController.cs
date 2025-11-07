using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200034E RID: 846
public class ColorsOverrideSettingsPanelController : MonoBehaviour, IRefreshable
{
	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0000B58E File Offset: 0x0000978E
	public ColorSchemesSettings colorSchemesSettings
	{
		get
		{
			return this._colorSchemesSettings;
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0003BF74 File Offset: 0x0003A174
	public void SetData(ColorSchemesSettings colorSchemesSettings)
	{
		if (!this._initialized)
		{
			this._buttonBinder = new ButtonBinder();
			this._buttonBinder.AddBinding(this._editColorSchemeButton, new Action(this.HandleEditColorSchemeButtonWasPressed));
			this._colorSchemeDropDown.didSelectCellWithIdxEvent += this.HandleDropDownDidSelectCellWithIdx;
			this._editColorSchemeController.didFinishEvent += this.HandleEditColorSchemeControllerDidFinish;
			this._editColorSchemeController.didChangeColorSchemeEvent += this.HandleEditColorSchemeControllerDidChangeColorScheme;
			this._overrideColorsToggle.onValueChanged.AddListener(new UnityAction<bool>(this.HandleOverrideColorsToggleValueChanged));
			this._initialized = true;
		}
		this._colorSchemesSettings = colorSchemesSettings;
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0003C024 File Offset: 0x0003A224
	protected void OnDestroy()
	{
		if (this._colorSchemeDropDown != null)
		{
			this._colorSchemeDropDown.didSelectCellWithIdxEvent -= this.HandleDropDownDidSelectCellWithIdx;
		}
		if (this._editColorSchemeController != null)
		{
			this._editColorSchemeController.didFinishEvent -= this.HandleEditColorSchemeControllerDidFinish;
			this._editColorSchemeController.didChangeColorSchemeEvent -= this.HandleEditColorSchemeControllerDidChangeColorScheme;
		}
		this._overrideColorsToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleOverrideColorsToggleValueChanged));
		this._buttonBinder.ClearBindings();
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0000B596 File Offset: 0x00009796
	protected void OnDisable()
	{
		this._editColorSchemeModalView.Hide(false, null);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0003C0BC File Offset: 0x0003A2BC
	public void Refresh()
	{
		if (!this._initialized)
		{
			return;
		}
		this._overrideColorsToggle.isOn = this._colorSchemesSettings.overrideDefaultColors;
		this._detailsPanelGO.SetActive(this._colorSchemesSettings.overrideDefaultColors);
		int numberOfColorSchemes = this._colorSchemesSettings.GetNumberOfColorSchemes();
		List<ColorScheme> list = new List<ColorScheme>(numberOfColorSchemes);
		for (int i = 0; i < numberOfColorSchemes; i++)
		{
			list.Add(this._colorSchemesSettings.GetColorSchemeForIdx(i));
		}
		ColorScheme selectedColorScheme = this._colorSchemesSettings.GetSelectedColorScheme();
		ColorSchemesTableViewDataSource colorSchemesTableViewDataSource = new ColorSchemesTableViewDataSource(this._colorSchemesTableViewDataSourceTemplate);
		colorSchemesTableViewDataSource.SetData(list);
		this._colorSchemeDropDown.Init(colorSchemesTableViewDataSource);
		this._colorSchemeDropDown.SetData(selectedColorScheme);
		this._colorSchemeDropDown.SelectCellWithIdx(this._colorSchemesSettings.GetSelectedColorSchemeIdx());
		this._editColorSchemeButton.interactable = selectedColorScheme.isEditable;
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0003C194 File Offset: 0x0003A394
	private void HandleDropDownDidSelectCellWithIdx(DropdownWithTableView dropDownWithTableView, int idx)
	{
		ColorScheme colorSchemeForIdx = this._colorSchemesSettings.GetColorSchemeForIdx(idx);
		this._colorSchemeDropDown.SetData(colorSchemeForIdx);
		this._colorSchemesSettings.selectedColorSchemeId = colorSchemeForIdx.colorSchemeId;
		this._editColorSchemeButton.interactable = colorSchemeForIdx.isEditable;
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0003C1DC File Offset: 0x0003A3DC
	private void HandleOverrideColorsToggleValueChanged(bool isOn)
	{
		this._colorSchemesSettings.overrideDefaultColors = isOn;
		if (isOn && !this._detailsPanelGO.activeSelf)
		{
			this._detailsPanelGO.SetActive(true);
			this._presentPanelAnimation.ExecuteAnimation(this._detailsPanelGO);
			return;
		}
		if (!isOn && this._detailsPanelGO.activeSelf)
		{
			this._dismissPanelAnimation.ExecuteAnimation(this._detailsPanelGO, delegate()
			{
				this._detailsPanelGO.SetActive(false);
			});
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0000B5A5 File Offset: 0x000097A5
	private void HandleEditColorSchemeButtonWasPressed()
	{
		this._editColorSchemeController.gameObject.SetActive(true);
		this._editColorSchemeController.SetColorScheme(this._colorSchemesSettings.GetSelectedColorScheme());
		this._editColorSchemeModalView.Show(true, true, null);
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0000B5DC File Offset: 0x000097DC
	private void HandleEditColorSchemeControllerDidFinish()
	{
		if (this._editColorSchemeController.gameObject.activeSelf)
		{
			this._editColorSchemeModalView.Hide(true, null);
		}
		this.Refresh();
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0000B603 File Offset: 0x00009803
	private void HandleEditColorSchemeControllerDidChangeColorScheme(ColorScheme colorScheme)
	{
		this._colorSchemesSettings.SetColorSchemeForId(colorScheme);
	}

	// Token: 0x04000F30 RID: 3888
	[SerializeField]
	private Toggle _overrideColorsToggle;

	// Token: 0x04000F31 RID: 3889
	[SerializeField]
	private GameObject _detailsPanelGO;

	// Token: 0x04000F32 RID: 3890
	[SerializeField]
	private ColorSchemeDropdownWithTableView _colorSchemeDropDown;

	// Token: 0x04000F33 RID: 3891
	[SerializeField]
	private EditColorSchemeController _editColorSchemeController;

	// Token: 0x04000F34 RID: 3892
	[SerializeField]
	private ModalView _editColorSchemeModalView;

	// Token: 0x04000F35 RID: 3893
	[SerializeField]
	private Button _editColorSchemeButton;

	// Token: 0x04000F36 RID: 3894
	[Space]
	[SerializeField]
	private PanelAnimationSO _presentPanelAnimation;

	// Token: 0x04000F37 RID: 3895
	[SerializeField]
	private PanelAnimationSO _dismissPanelAnimation;

	// Token: 0x04000F38 RID: 3896
	[Space]
	[SerializeField]
	private ColorSchemesTableViewDataSource _colorSchemesTableViewDataSourceTemplate;

	// Token: 0x04000F39 RID: 3897
	private ColorSchemesSettings _colorSchemesSettings;

	// Token: 0x04000F3A RID: 3898
	private bool _initialized;

	// Token: 0x04000F3B RID: 3899
	private ButtonBinder _buttonBinder;
}
