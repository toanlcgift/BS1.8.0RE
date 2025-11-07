using System;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x02000388 RID: 904
public class SettingsFlowCoordinator : FlowCoordinator
{
	// Token: 0x1400008A RID: 138
	// (add) Token: 0x0600105B RID: 4187 RVA: 0x00040560 File Offset: 0x0003E760
	// (remove) Token: 0x0600105C RID: 4188 RVA: 0x00040598 File Offset: 0x0003E798
	public event Action<SettingsFlowCoordinator, SettingsFlowCoordinator.FinishAction> didFinishEvent;

	// Token: 0x0600105D RID: 4189 RVA: 0x000405D0 File Offset: 0x0003E7D0
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._settingsNavigationController.didFinishEvent += this.HandleSettingsNavigationControllerDidFinish;
			this._mainSettingsMenuViewController.didSelectSettingsSubMenuEvent += this.HandleDidSelectSettingsSubMenu;
			this._mainSettingsMenuViewController.Init(SettingsFlowCoordinator._selectedSettingsSubMenuInfoIdx);
			if (this._mainSettingsMenuViewController.numberOfSubMenus == 1)
			{
				base.SetViewControllersToNavigationController(this._settingsNavigationController, new ViewController[]
				{
					this._mainSettingsMenuViewController.selectedSubMenuInfo.viewController
				});
			}
			else
			{
				base.SetViewControllersToNavigationController(this._settingsNavigationController, new ViewController[]
				{
					this._mainSettingsMenuViewController,
					this._mainSettingsMenuViewController.selectedSubMenuInfo.viewController
				});
			}
			base.ProvideInitialViewControllers(this._settingsNavigationController, null, null, null, null);
		}
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0000C82C File Offset: 0x0000AA2C
	protected override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._settingsNavigationController.didFinishEvent -= this.HandleSettingsNavigationControllerDidFinish;
			this._mainSettingsMenuViewController.didSelectSettingsSubMenuEvent -= this.HandleDidSelectSettingsSubMenu;
		}
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x00040694 File Offset: 0x0003E894
	private void HandleDidSelectSettingsSubMenu(SettingsSubMenuInfo settingsSubMenuInfo, int idx)
	{
		SettingsFlowCoordinator._selectedSettingsSubMenuInfoIdx = idx;
		bool flag = this._mainSettingsMenuViewController.selectedSubMenuInfo == null;
		if (this._mainSettingsMenuViewController.selectedSubMenuInfo != null)
		{
			base.PopViewControllerFromNavigationController(this._settingsNavigationController, null, true);
		}
		base.PushViewControllerToNavigationController(this._settingsNavigationController, settingsSubMenuInfo.viewController, null, !flag);
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x000406E8 File Offset: 0x0003E8E8
	private void HandleSettingsNavigationControllerDidFinish(SettingsNavigationController.FinishAction finishAction)
	{
		switch (finishAction)
		{
		case SettingsNavigationController.FinishAction.Ok:
		{
			this.ApplySettings();
			Action<SettingsFlowCoordinator, SettingsFlowCoordinator.FinishAction> action = this.didFinishEvent;
			if (action == null)
			{
				return;
			}
			action(this, SettingsFlowCoordinator.FinishAction.Ok);
			return;
		}
		case SettingsNavigationController.FinishAction.Cancel:
		{
			this.CancelSettings();
			Action<SettingsFlowCoordinator, SettingsFlowCoordinator.FinishAction> action2 = this.didFinishEvent;
			if (action2 == null)
			{
				return;
			}
			action2(this, SettingsFlowCoordinator.FinishAction.Cancel);
			return;
		}
		case SettingsNavigationController.FinishAction.Apply:
		{
			this.ApplySettings();
			Action<SettingsFlowCoordinator, SettingsFlowCoordinator.FinishAction> action3 = this.didFinishEvent;
			if (action3 == null)
			{
				return;
			}
			action3(this, SettingsFlowCoordinator.FinishAction.Apply);
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0000C85F File Offset: 0x0000AA5F
	private void ApplySettings()
	{
		this._mainSettingsModel.Save();
		this._mainSettingsModel.Load(true);
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0000C878 File Offset: 0x0000AA78
	private void CancelSettings()
	{
		this._mainSettingsModel.Load(true);
	}

	// Token: 0x0400107E RID: 4222
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x0400107F RID: 4223
	[Inject]
	private MainSettingsMenuViewController _mainSettingsMenuViewController;

	// Token: 0x04001080 RID: 4224
	[Inject]
	private SettingsNavigationController _settingsNavigationController;

	// Token: 0x04001082 RID: 4226
	[DoesNotRequireDomainReloadInit]
	private static int _selectedSettingsSubMenuInfoIdx;

	// Token: 0x02000389 RID: 905
	public enum FinishAction
	{
		// Token: 0x04001084 RID: 4228
		Cancel,
		// Token: 0x04001085 RID: 4229
		Ok,
		// Token: 0x04001086 RID: 4230
		Apply
	}
}
