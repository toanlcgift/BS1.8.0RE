using System;
using HMUI;
using Polyglot;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000370 RID: 880
public class EnvironmentOverrideSettingsPanelController : MonoBehaviour, IRefreshable
{
	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0000BEDB File Offset: 0x0000A0DB
	public OverrideEnvironmentSettings overrideEnvironmentSettings
	{
		get
		{
			return this._overrideEnvironmentSettings;
		}
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0003E44C File Offset: 0x0003C64C
	public void SetData(OverrideEnvironmentSettings overrideEnvironmentSettings)
	{
		if (!this._initialized)
		{
			foreach (EnvironmentOverrideSettingsPanelController.Elements elements2 in this._elements)
			{
				EnvironmentsTableViewDataSource environmentsTableViewDataSource = new EnvironmentsTableViewDataSource(this._environmentsTableViewDataSourceTemplate);
				environmentsTableViewDataSource.SetData(this._allEnvironments.GetAllEnvironmentInfosWithType(elements2.environmentType));
				elements2.overrideEnvironmentDropDown.Init(environmentsTableViewDataSource);
				elements2.overrideEnvironmentDropDown.didSelectCellWithIdxEvent += this.HandleDropDownDidSelectCellWithIdx;
			}
			this._overrideEnvironmentsToggle.onValueChanged.AddListener(new UnityAction<bool>(this.HandleOverrideEnvironmentsToggleValueChanged));
			this._initialized = true;
		}
		this._overrideEnvironmentSettings = overrideEnvironmentSettings;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
	protected void OnDestroy()
	{
		foreach (EnvironmentOverrideSettingsPanelController.Elements elements2 in this._elements)
		{
			if (elements2.overrideEnvironmentDropDown != null)
			{
				elements2.overrideEnvironmentDropDown.didSelectCellWithIdxEvent -= this.HandleDropDownDidSelectCellWithIdx;
			}
		}
		this._overrideEnvironmentsToggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleOverrideEnvironmentsToggleValueChanged));
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0003E558 File Offset: 0x0003C758
	public void Refresh()
	{
		if (!this._initialized)
		{
			return;
		}
		foreach (EnvironmentOverrideSettingsPanelController.Elements elements2 in this._elements)
		{
			EnvironmentsTableViewDataSource environmentsTableViewDataSource = elements2.overrideEnvironmentDropDown.tableViewDataSource as EnvironmentsTableViewDataSource;
			EnvironmentInfoSO overrideEnvironmentInfoForType = this._overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(elements2.environmentType);
			int idx = environmentsTableViewDataSource.environmentInfos.IndexOf(overrideEnvironmentInfoForType);
			elements2.overrideEnvironmentDropDown.SelectCellWithIdx(idx);
			elements2.overrideEnvironmentDropDown.SetValueText(overrideEnvironmentInfoForType.environmentName);
			elements2.overrideEnvironmentDropDown.SetLabelText(Localization.Get(elements2.environmentType.typeNameLocalizationKey));
		}
		this._overrideEnvironmentsToggle.isOn = this._overrideEnvironmentSettings.overrideEnvironments;
		this._elementsGO.SetActive(this._overrideEnvironmentSettings.overrideEnvironments);
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0003E61C File Offset: 0x0003C81C
	private void HandleDropDownDidSelectCellWithIdx(DropdownWithTableView dropDownWithTableView, int idx)
	{
		LabelAndValueDropdownWithTableView labelAndValueDropdownWithTableView = dropDownWithTableView as LabelAndValueDropdownWithTableView;
		EnvironmentInfoSO environmentInfoSO = (dropDownWithTableView.tableViewDataSource as EnvironmentsTableViewDataSource).environmentInfos[idx];
		labelAndValueDropdownWithTableView.SetValueText(environmentInfoSO.environmentName);
		EnvironmentTypeSO environmentType = null;
		foreach (EnvironmentOverrideSettingsPanelController.Elements elements2 in this._elements)
		{
			if (elements2.overrideEnvironmentDropDown == dropDownWithTableView)
			{
				environmentType = elements2.environmentType;
			}
		}
		this._overrideEnvironmentSettings.SetEnvironmentInfoForType(environmentType, environmentInfoSO);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0003E694 File Offset: 0x0003C894
	private void HandleOverrideEnvironmentsToggleValueChanged(bool isOn)
	{
		this._overrideEnvironmentSettings.overrideEnvironments = isOn;
		if (isOn && !this._elementsGO.activeSelf)
		{
			this._elementsGO.SetActive(true);
			this._presentPanelAnimation.ExecuteAnimation(this._elementsGO);
			return;
		}
		if (!isOn && this._elementsGO.activeSelf)
		{
			this._dismissPanelAnimation.ExecuteAnimation(this._elementsGO, delegate()
			{
				this._elementsGO.SetActive(false);
			});
		}
	}

	// Token: 0x04000FF0 RID: 4080
	[SerializeField]
	private Toggle _overrideEnvironmentsToggle;

	// Token: 0x04000FF1 RID: 4081
	[SerializeField]
	private GameObject _elementsGO;

	// Token: 0x04000FF2 RID: 4082
	[SerializeField]
	private EnvironmentOverrideSettingsPanelController.Elements[] _elements;

	// Token: 0x04000FF3 RID: 4083
	[Space]
	[SerializeField]
	private PanelAnimationSO _presentPanelAnimation;

	// Token: 0x04000FF4 RID: 4084
	[SerializeField]
	private PanelAnimationSO _dismissPanelAnimation;

	// Token: 0x04000FF5 RID: 4085
	[Space]
	[SerializeField]
	private EnvironmentsListSO _allEnvironments;

	// Token: 0x04000FF6 RID: 4086
	[SerializeField]
	private EnvironmentsTableViewDataSource _environmentsTableViewDataSourceTemplate;

	// Token: 0x04000FF7 RID: 4087
	private OverrideEnvironmentSettings _overrideEnvironmentSettings;

	// Token: 0x04000FF8 RID: 4088
	private bool _initialized;

	// Token: 0x02000371 RID: 881
	[Serializable]
	public class Elements
	{
		// Token: 0x04000FF9 RID: 4089
		public LabelAndValueDropdownWithTableView overrideEnvironmentDropDown;

		// Token: 0x04000FFA RID: 4090
		public EnvironmentTypeSO environmentType;
	}
}
