using System;
using System.Collections;
using HMUI;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000380 RID: 896
public class MainFlowCoordinator : FlowCoordinator
{
	// Token: 0x06001018 RID: 4120 RVA: 0x0003F948 File Offset: 0x0003DB48
	protected override void DidActivate(bool firstActivation, FlowCoordinator.ActivationType activationType)
	{
		this._mainMenuViewController.didFinishEvent += this.HandleMainMenuViewControllerDidFinish;
		this._playerSettingsViewController.didFinishEvent += this.HandlePlayerSettingsViewControllerDidFinish;
		this._howToPlayViewController.didPressTutorialButtonEvent += this.HandleHowToPlayViewControllerDidPressTutorialButton;
		this._promoViewController.promoButtonWasPressedEvent += this.HandlePromoViewControllerPromoButtonWasPressed;
		if (activationType == FlowCoordinator.ActivationType.AddedToHierarchy)
		{
			this._soloFreePlayFlowCoordinator.didFinishEvent += this.HandleSoloFreePlayFlowCoordinatorDidFinish;
			this._partyFreePlayFlowCoordinator.didFinishEvent += this.HandlePartyFreePlayFlowCoordinatorDidFinish;
			this._settingsFlowCoordinator.didFinishEvent += this.HandleSettingsFlowCoordinatorDidFinish;
			this._campaignFlowCoordinator.didFinishEvent += this.HandleCampaignFlowCoordinatorDidFinish;
			ViewController releaseInfoViewController = this._releaseInfoViewController;
			base.ProvideInitialViewControllers(this._mainMenuViewController, releaseInfoViewController, this._playerStatisticsViewController, this._promoViewController, null);
		}
		this._menuLightsManager.SetColorPreset(this._defaultLightsPreset, true);
		if (firstActivation && this._menuDestinationRequest != null)
		{
			base.StartCoroutine(this.ProcessMenuDestinationRequestAfterFrameCoroutine(this._menuDestinationRequest));
			this._menuDestinationRequest = null;
		}
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0003FA68 File Offset: 0x0003DC68
	protected override void TopViewControllerWillChange(ViewController oldViewController, ViewController newViewController, bool immediately)
	{
		if (newViewController == this._mainMenuViewController)
		{
			ViewController releaseInfoViewController = this._releaseInfoViewController;
			base.SetLeftScreenViewController(releaseInfoViewController, immediately);
			base.SetRightScreenViewController(this._playerStatisticsViewController, immediately);
			base.SetBottomScreenViewController(this._promoViewController, immediately);
		}
		else
		{
			base.SetLeftScreenViewController(null, immediately);
			base.SetRightScreenViewController(null, immediately);
			base.SetBottomScreenViewController(null, immediately);
		}
		if (newViewController == this._howToPlayViewController)
		{
			base.title = Localization.Get("TITLE_HOW_TO_PLAY");
			base.showBackButton = true;
			return;
		}
		base.title = "";
		base.showBackButton = false;
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0000C4AA File Offset: 0x0000A6AA
	protected override void InitialViewControllerWasPresented()
	{
		if (MainFlowCoordinator._startWithSettings)
		{
			base.PresentFlowCoordinator(this._settingsFlowCoordinator, null, true, false);
		}
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0003FB00 File Offset: 0x0003DD00
	protected override void DidDeactivate(FlowCoordinator.DeactivationType deactivationType)
	{
		this._mainMenuViewController.didFinishEvent -= this.HandleMainMenuViewControllerDidFinish;
		this._playerSettingsViewController.didFinishEvent -= this.HandlePlayerSettingsViewControllerDidFinish;
		this._howToPlayViewController.didPressTutorialButtonEvent -= this.HandleHowToPlayViewControllerDidPressTutorialButton;
		this._promoViewController.promoButtonWasPressedEvent -= this.HandlePromoViewControllerPromoButtonWasPressed;
		if (deactivationType == FlowCoordinator.DeactivationType.RemovedFromHierarchy)
		{
			this._settingsFlowCoordinator.didFinishEvent -= this.HandleSettingsFlowCoordinatorDidFinish;
			this._soloFreePlayFlowCoordinator.didFinishEvent -= this.HandleSoloFreePlayFlowCoordinatorDidFinish;
			this._partyFreePlayFlowCoordinator.didFinishEvent -= this.HandlePartyFreePlayFlowCoordinatorDidFinish;
			this._campaignFlowCoordinator.didFinishEvent -= this.HandleCampaignFlowCoordinatorDidFinish;
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0003FBC8 File Offset: 0x0003DDC8
	private void PresentFlowCoordinatorOrAskForTutorial(FlowCoordinator flowCoordinator)
	{
		this._afterDialogPromptFlowCoordinator = flowCoordinator;
		if (this._playerDataModel.playerData.shouldShowTutorialPrompt)
		{
			this._playerDataModel.playerData.MarkTutorialAsShown();
			this._playerDataModel.Save();
			this._simpleDialogPromptViewController.Init(Localization.Get("PROMPT_INFORMATION"), Localization.Get("PROMPT_HAVENT_PLAYED_YET"), Localization.Get("PROMPT_YES"), Localization.Get("PROMPT_NO"), delegate(int buttonNumber)
			{
				if (buttonNumber == 0)
				{
					this._menuTransitionsHelper.StartTutorial(delegate
					{
						base.DismissViewController(this._simpleDialogPromptViewController, null, true);
					});
					return;
				}
				base.PresentFlowCoordinator(this._afterDialogPromptFlowCoordinator, null, false, true);
			});
			base.PresentViewController(this._simpleDialogPromptViewController, null, false);
			return;
		}
		base.PresentFlowCoordinator(flowCoordinator, null, false, false);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0003FC64 File Offset: 0x0003DE64
	private void HandleMainMenuViewControllerDidFinish(MainMenuViewController viewController, MainMenuViewController.MenuButton subMenuType)
	{
		switch (subMenuType)
		{
		case MainMenuViewController.MenuButton.SoloFreePlay:
			this.PresentFlowCoordinatorOrAskForTutorial(this._soloFreePlayFlowCoordinator);
			return;
		case MainMenuViewController.MenuButton.Party:
			this.PresentFlowCoordinatorOrAskForTutorial(this._partyFreePlayFlowCoordinator);
			return;
		case MainMenuViewController.MenuButton.BeatmapEditor:
			this._menuTransitionsHelper.StartBeatmapEditor(delegate
			{
				this._beatmapLevelsModel.ClearLoadedBeatmapLevelsCaches();
			});
			return;
		case MainMenuViewController.MenuButton.SoloCampaign:
			this.PresentFlowCoordinatorOrAskForTutorial(this._campaignFlowCoordinator);
			return;
		case MainMenuViewController.MenuButton.Settings:
			MainFlowCoordinator._startWithSettings = true;
			base.PresentFlowCoordinator(this._settingsFlowCoordinator, null, false, false);
			return;
		case MainMenuViewController.MenuButton.PlayerSettings:
			this._playerSettingsViewController.hideBackButton = false;
			base.PresentViewController(this._playerSettingsViewController, null, false);
			return;
		case MainMenuViewController.MenuButton.FloorAdjust:
			break;
		case MainMenuViewController.MenuButton.HowToPlay:
			base.PresentViewController(this._howToPlayViewController, null, false);
			return;
		case MainMenuViewController.MenuButton.Credits:
			this._menuTransitionsHelper.ShowCredits();
			return;
		case MainMenuViewController.MenuButton.Quit:
			this._fadeInOut.FadeOutInstant();
			Application.Quit();
			break;
		default:
			return;
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0000C4C2 File Offset: 0x0000A6C2
	private void HandleFloorAdjustViewControllerDidFinishEvent(FloorAdjustViewController viewController)
	{
		base.DismissViewController(viewController, null, false);
		this._mainSettingsModel.Save();
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
	private void HandlePlayerSettingsViewControllerDidFinish(PlayerSettingsViewController viewController)
	{
		base.DismissViewController(viewController, null, false);
		this._playerDataModel.Save();
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0000C4EE File Offset: 0x0000A6EE
	private void HandleHowToPlayViewControllerDidPressTutorialButton()
	{
		this._playerDataModel.playerData.MarkTutorialAsShown();
		this._menuTransitionsHelper.StartTutorial(null);
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0000C50C File Offset: 0x0000A70C
	private void HandleCampaignFlowCoordinatorDidFinish(CampaignFlowCoordinator flowCoordinator)
	{
		base.DismissFlowCoordinator(flowCoordinator, null, false);
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0000C50C File Offset: 0x0000A70C
	private void HandleSoloFreePlayFlowCoordinatorDidFinish(LevelSelectionFlowCoordinator flowCoordinator)
	{
		base.DismissFlowCoordinator(flowCoordinator, null, false);
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0000C50C File Offset: 0x0000A70C
	private void HandlePartyFreePlayFlowCoordinatorDidFinish(LevelSelectionFlowCoordinator flowCoordinator)
	{
		base.DismissFlowCoordinator(flowCoordinator, null, false);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0000C517 File Offset: 0x0000A717
	private void HandleSettingsFlowCoordinatorDidFinish(SettingsFlowCoordinator settingsFlowCoordinator, SettingsFlowCoordinator.FinishAction finishAction)
	{
		if (finishAction != SettingsFlowCoordinator.FinishAction.Apply)
		{
			MainFlowCoordinator._startWithSettings = false;
		}
		if (finishAction == SettingsFlowCoordinator.FinishAction.Cancel)
		{
			base.DismissFlowCoordinator(this._settingsFlowCoordinator, null, false);
			return;
		}
		this._menuTransitionsHelper.RestartGame();
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0000C540 File Offset: 0x0000A740
	private void HandlePromoViewControllerPromoButtonWasPressed(PromoViewController promoViewController, IAnnotatedBeatmapLevelCollection annotatedBeatmapLevelCollection)
	{
		this._soloFreePlayFlowCoordinator.Setup(annotatedBeatmapLevelCollection, null);
		this.PresentFlowCoordinatorOrAskForTutorial(this._soloFreePlayFlowCoordinator);
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0003FD3C File Offset: 0x0003DF3C
	private void ProcessMenuDestinationRequest(MenuDestination destination)
	{
		SelectLevelPackDestination selectLevelPackDestination = destination as SelectLevelPackDestination;
		if (selectLevelPackDestination != null)
		{
			this._soloFreePlayFlowCoordinator.Setup(selectLevelPackDestination.beatmapLevelPack, null);
			base.PresentFlowCoordinator(this._soloFreePlayFlowCoordinator, null, true, false);
			return;
		}
		SelectLevelDestination selectLevelDestination = destination as SelectLevelDestination;
		if (selectLevelDestination != null)
		{
			this._playerDataModel.playerData.SetLastSelectedBeatmapDifficulty(selectLevelDestination.beatmapDifficulty);
			if (selectLevelDestination.beatmapCharacteristic != null)
			{
				this._playerDataModel.playerData.SetLastSelectedBeatmapCharacteristic(selectLevelDestination.beatmapCharacteristic);
			}
			this._soloFreePlayFlowCoordinator.Setup(selectLevelDestination.beatmapLevelPack, selectLevelDestination.previewBeatmapLevel);
			base.PresentFlowCoordinator(this._soloFreePlayFlowCoordinator, null, true, false);
			return;
		}
		SelectSubMenuDestination selectSubMenuDestination = destination as SelectSubMenuDestination;
		if (selectSubMenuDestination == null)
		{
			return;
		}
		switch (selectSubMenuDestination.menuDestination)
		{
		case SelectSubMenuDestination.Destination.Campaign:
			base.PresentFlowCoordinator(this._campaignFlowCoordinator, null, true, false);
			return;
		case SelectSubMenuDestination.Destination.SoloFreePlay:
			base.PresentFlowCoordinator(this._soloFreePlayFlowCoordinator, null, true, false);
			return;
		case SelectSubMenuDestination.Destination.PartyFreePlay:
			base.PresentFlowCoordinator(this._partyFreePlayFlowCoordinator, null, true, false);
			return;
		case SelectSubMenuDestination.Destination.Settings:
			base.PresentFlowCoordinator(this._settingsFlowCoordinator, null, true, false);
			return;
		case SelectSubMenuDestination.Destination.Tutorial:
			base.PresentViewController(this._howToPlayViewController, null, true);
			return;
		default:
			return;
		}
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			base.PresentFlowCoordinator(this._soloFreePlayFlowCoordinator, null, true, false);
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			base.PresentFlowCoordinator(this._campaignFlowCoordinator, null, true, false);
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			base.PresentFlowCoordinator(this._settingsFlowCoordinator, null, true, false);
		}
		else if (Input.GetKeyDown(KeyCode.V))
		{
			base.PresentViewController(this._howToPlayViewController, null, true);
		}
		else if (Input.GetKeyDown(KeyCode.B))
		{
			this._menuTransitionsHelper.StartBeatmapEditor(delegate
			{
				this._beatmapLevelsModel.ClearLoadedBeatmapLevelsCaches();
			});
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0000C55B File Offset: 0x0000A75B
	private IEnumerator ProcessMenuDestinationRequestAfterFrameCoroutine(MenuDestination destination)
	{
		yield return null;
		this.ProcessMenuDestinationRequest(destination);
		yield break;
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0000C571 File Offset: 0x0000A771
	protected override void BackButtonWasPressed(ViewController topViewController)
	{
		if (topViewController == this._howToPlayViewController)
		{
			base.DismissViewController(topViewController, null, false);
		}
	}

	// Token: 0x04001049 RID: 4169
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x0400104A RID: 4170
	[SerializeField]
	private MenuLightsPresetSO _defaultLightsPreset;

	// Token: 0x0400104B RID: 4171
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x0400104C RID: 4172
	[Inject]
	private SoloFreePlayFlowCoordinator _soloFreePlayFlowCoordinator;

	// Token: 0x0400104D RID: 4173
	[Inject]
	private PartyFreePlayFlowCoordinator _partyFreePlayFlowCoordinator;

	// Token: 0x0400104E RID: 4174
	[Inject]
	private CampaignFlowCoordinator _campaignFlowCoordinator;

	// Token: 0x0400104F RID: 4175
	[Inject]
	private SettingsFlowCoordinator _settingsFlowCoordinator;

	// Token: 0x04001050 RID: 4176
	[Inject]
	private ReleaseInfoViewController _releaseInfoViewController;

	// Token: 0x04001051 RID: 4177
	[Inject]
	private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	// Token: 0x04001052 RID: 4178
	[Inject]
	private MainMenuViewController _mainMenuViewController;

	// Token: 0x04001053 RID: 4179
	[Inject]
	private HowToPlayViewController _howToPlayViewController;

	// Token: 0x04001054 RID: 4180
	[Inject]
	private PlayerSettingsViewController _playerSettingsViewController;

	// Token: 0x04001055 RID: 4181
	[Inject]
	private PlayerStatisticsViewController _playerStatisticsViewController;

	// Token: 0x04001056 RID: 4182
	[Inject]
	private PromoViewController _promoViewController;

	// Token: 0x04001057 RID: 4183
	[Inject]
	private MenuLightsManager _menuLightsManager;

	// Token: 0x04001058 RID: 4184
	[Inject]
	private FadeInOutController _fadeInOut;

	// Token: 0x04001059 RID: 4185
	[Inject]
	private BeatmapLevelsModel _beatmapLevelsModel;

	// Token: 0x0400105A RID: 4186
	[Inject]
	private MenuTransitionsHelper _menuTransitionsHelper;

	// Token: 0x0400105B RID: 4187
	[InjectOptional]
	private MenuDestination _menuDestinationRequest;

	// Token: 0x0400105C RID: 4188
	private FlowCoordinator _afterDialogPromptFlowCoordinator;

	// Token: 0x0400105D RID: 4189
	[DoesNotRequireDomainReloadInit]
	private static bool _startWithSettings;
}
