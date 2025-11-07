using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000409 RID: 1033
public class SettingsNavigationController : NavigationController
{
	// Token: 0x140000B4 RID: 180
	// (add) Token: 0x06001383 RID: 4995 RVA: 0x00048604 File Offset: 0x00046804
	// (remove) Token: 0x06001384 RID: 4996 RVA: 0x0004863C File Offset: 0x0004683C
	public event Action<SettingsNavigationController.FinishAction> didFinishEvent;

	// Token: 0x06001385 RID: 4997 RVA: 0x00048674 File Offset: 0x00046874
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._okButton, delegate
			{
				this.HandleFinishButton(SettingsNavigationController.FinishAction.Ok);
			});
			base.buttonBinder.AddBinding(this._cancelButton, delegate
			{
				this.HandleFinishButton(SettingsNavigationController.FinishAction.Cancel);
			});
			base.buttonBinder.AddBinding(this._applyButton, delegate
			{
				this.HandleFinishButton(SettingsNavigationController.FinishAction.Apply);
			});
		}
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x0000EBAB File Offset: 0x0000CDAB
	private void HandleFinishButton(SettingsNavigationController.FinishAction finishAction)
	{
		Action<SettingsNavigationController.FinishAction> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(finishAction);
	}

	// Token: 0x04001344 RID: 4932
	[SerializeField]
	private Button _okButton;

	// Token: 0x04001345 RID: 4933
	[SerializeField]
	private Button _applyButton;

	// Token: 0x04001346 RID: 4934
	[SerializeField]
	private Button _cancelButton;

	// Token: 0x0200040A RID: 1034
	public enum FinishAction
	{
		// Token: 0x04001349 RID: 4937
		Ok,
		// Token: 0x0400134A RID: 4938
		Cancel,
		// Token: 0x0400134B RID: 4939
		Apply
	}
}
