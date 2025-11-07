using System;
using System.IO;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003EA RID: 1002
public class MainMenuViewController : ViewController
{
	// Token: 0x140000A7 RID: 167
	// (add) Token: 0x060012C0 RID: 4800 RVA: 0x000465A4 File Offset: 0x000447A4
	// (remove) Token: 0x060012C1 RID: 4801 RVA: 0x000465DC File Offset: 0x000447DC
	public event Action<MainMenuViewController, MainMenuViewController.MenuButton> didFinishEvent;

	// Token: 0x060012C2 RID: 4802 RVA: 0x00046614 File Offset: 0x00044814
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._soloButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.SoloFreePlay);
			});
			base.buttonBinder.AddBinding(this._partyButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.Party);
			});
			base.buttonBinder.AddBinding(this._campaignButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.SoloCampaign);
			});
			base.buttonBinder.AddBinding(this._settingsButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.Settings);
			});
			base.buttonBinder.AddBinding(this._playerSettingsButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.PlayerSettings);
			});
			base.buttonBinder.AddBinding(this._howToPlayButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.HowToPlay);
			});
			base.buttonBinder.AddBinding(this._creditsButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.Credits);
			});
			base.buttonBinder.AddBinding(this._beatmapEditorButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.BeatmapEditor);
			});
			base.buttonBinder.AddBinding(this._quitButton, delegate
			{
				this.HandleMenuButton(MainMenuViewController.MenuButton.Quit);
			});
		}
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x0004672C File Offset: 0x0004492C
	private void HandleMenuButton(MainMenuViewController.MenuButton menuButton)
	{
		File.WriteAllText("ahihi.txt", menuButton.ToString());
		Action<MainMenuViewController, MainMenuViewController.MenuButton> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this, menuButton);
	}

	// Token: 0x04001274 RID: 4724
	[SerializeField]
	private Button _soloButton;

	// Token: 0x04001275 RID: 4725
	[SerializeField]
	private Button _partyButton;

	// Token: 0x04001276 RID: 4726
	[SerializeField]
	private Button _campaignButton;

	// Token: 0x04001277 RID: 4727
	[SerializeField]
	private Button _settingsButton;

	// Token: 0x04001278 RID: 4728
	[SerializeField]
	private Button _playerSettingsButton;

	// Token: 0x04001279 RID: 4729
	[SerializeField]
	private Button _quitButton;

	// Token: 0x0400127A RID: 4730
	[SerializeField]
	private Button _howToPlayButton;

	// Token: 0x0400127B RID: 4731
	[SerializeField]
	private Button _beatmapEditorButton;

	// Token: 0x0400127C RID: 4732
	[SerializeField]
	private Button _creditsButton;

	// Token: 0x020003EB RID: 1003
	public enum MenuButton
	{
		// Token: 0x0400127F RID: 4735
		SoloFreePlay,
		// Token: 0x04001280 RID: 4736
		Party,
		// Token: 0x04001281 RID: 4737
		BeatmapEditor,
		// Token: 0x04001282 RID: 4738
		SoloCampaign,
		// Token: 0x04001283 RID: 4739
		Settings,
		// Token: 0x04001284 RID: 4740
		PlayerSettings,
		// Token: 0x04001285 RID: 4741
		FloorAdjust,
		// Token: 0x04001286 RID: 4742
		HowToPlay,
		// Token: 0x04001287 RID: 4743
		Credits,
		// Token: 0x04001288 RID: 4744
		Quit
	}
}
