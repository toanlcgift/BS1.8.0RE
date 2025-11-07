using System;
using HMUI;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class MainSettingsMenuViewController : ViewController
{
	// Token: 0x140000A8 RID: 168
	// (add) Token: 0x060012D1 RID: 4817 RVA: 0x00046764 File Offset: 0x00044964
	// (remove) Token: 0x060012D2 RID: 4818 RVA: 0x0004679C File Offset: 0x0004499C
	public event Action<SettingsSubMenuInfo, int> didSelectSettingsSubMenuEvent;

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
	public int numberOfSubMenus
	{
		get
		{
			return this._settingsSubMenuInfos.Length;
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0000E3CE File Offset: 0x0000C5CE
	public SettingsSubMenuInfo selectedSubMenuInfo
	{
		get
		{
			return this._selectedSubMenuInfo;
		}
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
	public void Init(int selectedSubMenuInfoIdx)
	{
		this._selectedSubMenuInfoIdx = selectedSubMenuInfoIdx;
		this._selectedSubMenuInfo = this._settingsSubMenuInfos[this._selectedSubMenuInfoIdx];
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000467D4 File Offset: 0x000449D4
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._mainSettingsTableView.didSelectRowEvent += this.HandleMainSettingsTableViewDidSelectRow;
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._selectedSubMenuInfo = null;
			this._mainSettingsTableView.Init(this._settingsSubMenuInfos);
			this._mainSettingsTableView.SelectRow(this._selectedSubMenuInfoIdx);
		}
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
	private void HandleMainSettingsTableViewDidSelectRow(MainSettingsTableView tableView, int row)
	{
		this._selectedSubMenuInfoIdx = row;
		this._selectedSubMenuInfo = this._settingsSubMenuInfos[row];
		Action<SettingsSubMenuInfo, int> action = this.didSelectSettingsSubMenuEvent;
		if (action == null)
		{
			return;
		}
		action(this._selectedSubMenuInfo, row);
	}

	// Token: 0x0400128C RID: 4748
	[SerializeField]
	private MainSettingsTableView _mainSettingsTableView;

	// Token: 0x0400128D RID: 4749
	[SerializeField]
	private SettingsSubMenuInfo[] _settingsSubMenuInfos;

	// Token: 0x0400128E RID: 4750
	private SettingsSubMenuInfo _selectedSubMenuInfo;

	// Token: 0x0400128F RID: 4751
	private int _selectedSubMenuInfoIdx;
}
