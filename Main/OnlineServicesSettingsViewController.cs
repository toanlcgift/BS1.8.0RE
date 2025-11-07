using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FA RID: 1018
public class OnlineServicesSettingsViewController : ViewController
{
	// Token: 0x0600131B RID: 4891 RVA: 0x00047498 File Offset: 0x00045698
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._toggleBinder = new ToggleBinder();
			this._toggleBinder.AddBinding(this._enableOnlineServicesToggle, new Action<bool>(this.HandleEnableOnlineServicesToggleValueChanged));
		}
		this._enableOnlineServicesToggle.isOn = this._onlineServicesEnabled;
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000023E9 File Offset: 0x000005E9
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000023E9 File Offset: 0x000005E9
	private void HandleEnableOnlineServicesToggleValueChanged(bool value)
	{
	}

	// Token: 0x040012D1 RID: 4817
	[SerializeField]
	private BoolSO _onlineServicesEnabled;

	// Token: 0x040012D2 RID: 4818
	[Space]
	[SerializeField]
	private Toggle _enableOnlineServicesToggle;

	// Token: 0x040012D3 RID: 4819
	private ToggleBinder _toggleBinder;
}
