using System;
using System.Collections.Generic;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x0200037C RID: 892
public class HealthWarningFlowCoordinator : FlowCoordinator
{
	// Token: 0x06000FE4 RID: 4068 RVA: 0x0003F1D4 File Offset: 0x0003D3D4
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._viewControllerTitles = new Dictionary<ViewController, string>
			{
				{
					this._healthWarningViewContoller,
					Localization.Get("HEALTH_AND_SAFETY_WARNING_TITLE")
				},
				{
					this._eulaViewController,
					Localization.Get("EULA_TITLE")
				},
				{
					this._privacyPolicyViewController,
					Localization.Get("PRIVACY_POLICY_TITLE")
				},
				{
					this._onlineServicesViewController,
					Localization.Get("ONLINE_SERVICES_TITLE")
				}
			};
		}
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._healthWarningViewContoller.didFinishEvent += this.HandleHealthWarningViewControllerdidFinish;
			this._healthWarningViewContoller.privacyPolicyButtonPressedEvent += this.HandleHealthWarningViewControllerPrivacyPolicyButtonPressed;
			this._healthWarningViewContoller.openDataPrivacyPageButtonPressedEvent += this.HandleHealthWarningViewControllerOpenDataPrivacyPolicyButtonPressed;
			this._eulaViewController.didFinishEvent += this.HandleEulaViewControllerdidFinish;
			this._onlineServicesViewController.didFinishEvent += this.HandleOnlineServicesViewControllerDidFinish;
			ViewController healthWarningViewContoller = this._healthWarningViewContoller;
			base.title = this._viewControllerTitles[healthWarningViewContoller];
			base.ProvideInitialViewControllers(healthWarningViewContoller, null, null, null, null);
		}
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0003F2E4 File Offset: 0x0003D4E4
	protected override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._healthWarningViewContoller.didFinishEvent -= this.HandleHealthWarningViewControllerdidFinish;
			this._eulaViewController.didFinishEvent -= this.HandleEulaViewControllerdidFinish;
			this._healthWarningViewContoller.privacyPolicyButtonPressedEvent -= this.HandleHealthWarningViewControllerPrivacyPolicyButtonPressed;
			this._healthWarningViewContoller.openDataPrivacyPageButtonPressedEvent -= this.HandleHealthWarningViewControllerOpenDataPrivacyPolicyButtonPressed;
			this._onlineServicesViewController.didFinishEvent -= this.HandleOnlineServicesViewControllerDidFinish;
		}
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0003F368 File Offset: 0x0003D568
	protected override void TopViewControllerWillChange(ViewController oldViewController, ViewController newViewController, bool immediately)
	{
		if (newViewController == this._privacyPolicyViewController)
		{
			base.showBackButton = true;
		}
		else
		{
			base.showBackButton = false;
		}
		string title = null;
		this._viewControllerTitles.TryGetValue(newViewController, out title);
		base.title = title;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0000C23A File Offset: 0x0000A43A
	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			this.HandleHealthWarningViewControllerdidFinish();
		}
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0000C24B File Offset: 0x0000A44B
	private void HandleEulaViewControllerdidFinish(bool agreed)
	{
		if (agreed)
		{
			this._playerDataModel.playerData.MarkEulaAsAgreed();
			base.ReplaceTopViewController(this._healthWarningViewContoller, null, false, ViewController.SlideAnimationDirection.Down);
			return;
		}
		this._fadeInOut.FadeOutInstant();
		Application.Quit();
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0000C280 File Offset: 0x0000A480
	private void HandleHealthWarningViewControllerPrivacyPolicyButtonPressed()
	{
		base.PresentViewController(this._privacyPolicyViewController, null, false);
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0000C290 File Offset: 0x0000A490
	private void HandleHealthWarningViewControllerOpenDataPrivacyPolicyButtonPressed()
	{
		this._simpleDialogPromptViewController.Init("Data Privacy Policy", "Data Privacy Policy page was opened in your desktop web browser.", "Ok", delegate(int button)
		{
			base.DismissViewController(this._simpleDialogPromptViewController, null, false);
		});
		base.PresentViewController(this._simpleDialogPromptViewController, null, false);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
	private void HandleHealthWarningViewControllerdidFinish()
	{
		this.GoToNextScene();
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0000C2CE File Offset: 0x0000A4CE
	private void HandleOnlineServicesViewControllerDidFinish(bool value)
	{
		this._onlineServicesEnabled.value = value;
		this.GoToNextScene();
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
	private void GoToNextScene()
	{
		this._gameScenesManager.ReplaceScenes(this._initData.nextScenesTransitionSetupData, 0.7f, null, null);
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0000C301 File Offset: 0x0000A501
	protected override void BackButtonWasPressed(ViewController topViewController)
	{
		if (topViewController == this._privacyPolicyViewController)
		{
			base.DismissViewController(topViewController, null, false);
		}
	}

	// Token: 0x0400102C RID: 4140
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x0400102D RID: 4141
	[SerializeField]
	private BoolSO _onlineServicesEnabled;

	// Token: 0x0400102E RID: 4142
	[Space]
	[SerializeField]
	private EulaViewController _eulaViewController;

	// Token: 0x0400102F RID: 4143
	[SerializeField]
	private HealthWarningViewController _healthWarningViewContoller;

	// Token: 0x04001030 RID: 4144
	[SerializeField]
	private PrivacyPolicyViewController _privacyPolicyViewController;

	// Token: 0x04001031 RID: 4145
	[SerializeField]
	private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	// Token: 0x04001032 RID: 4146
	[SerializeField]
	private OnlineServicesViewController _onlineServicesViewController;

	// Token: 0x04001033 RID: 4147
	[Inject]
	private FadeInOutController _fadeInOut;

	// Token: 0x04001034 RID: 4148
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04001035 RID: 4149
	[Inject]
	private HealthWarningFlowCoordinator.InitData _initData;

	// Token: 0x04001036 RID: 4150
	private Dictionary<ViewController, string> _viewControllerTitles;

	// Token: 0x0200037D RID: 893
	public class InitData
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x0000C32A File Offset: 0x0000A52A
		public InitData(ScenesTransitionSetupDataSO nextScenesTransitionSetupData)
		{
			this.nextScenesTransitionSetupData = nextScenesTransitionSetupData;
		}

		// Token: 0x04001037 RID: 4151
		public readonly ScenesTransitionSetupDataSO nextScenesTransitionSetupData;
	}
}
