using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040D RID: 1037
public class SimpleRetailDemoViewController : ViewController
{
	// Token: 0x140000B5 RID: 181
	// (add) Token: 0x06001393 RID: 5011 RVA: 0x000487EC File Offset: 0x000469EC
	// (remove) Token: 0x06001394 RID: 5012 RVA: 0x00048824 File Offset: 0x00046A24
	public event Action<SimpleRetailDemoViewController, SimpleRetailDemoViewController.MenuButton> didFinishEvent;

	// Token: 0x06001395 RID: 5013 RVA: 0x0004885C File Offset: 0x00046A5C
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._tutorialButton, delegate
			{
				this.HandleMenuButton(SimpleRetailDemoViewController.MenuButton.PlayTutorial);
			});
			base.buttonBinder.AddBinding(this._playLevel1Button, delegate
			{
				this.HandleMenuButton(SimpleRetailDemoViewController.MenuButton.PlayLevel1);
			});
			base.buttonBinder.AddBinding(this._playLevel2Button, delegate
			{
				this.HandleMenuButton(SimpleRetailDemoViewController.MenuButton.PlayLevel2);
			});
			base.buttonBinder.AddBinding(this._exitButton, delegate
			{
				this.HandleMenuButton(SimpleRetailDemoViewController.MenuButton.Exit);
			});
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x0000EC32 File Offset: 0x0000CE32
	private void HandleMenuButton(SimpleRetailDemoViewController.MenuButton menuButton)
	{
		Action<SimpleRetailDemoViewController, SimpleRetailDemoViewController.MenuButton> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this, menuButton);
	}

	// Token: 0x04001354 RID: 4948
	[SerializeField]
	private Button _tutorialButton;

	// Token: 0x04001355 RID: 4949
	[SerializeField]
	private Button _playLevel1Button;

	// Token: 0x04001356 RID: 4950
	[SerializeField]
	private Button _playLevel2Button;

	// Token: 0x04001357 RID: 4951
	[SerializeField]
	private Button _exitButton;

	// Token: 0x0200040E RID: 1038
	public enum MenuButton
	{
		// Token: 0x0400135A RID: 4954
		PlayTutorial,
		// Token: 0x0400135B RID: 4955
		PlayLevel1,
		// Token: 0x0400135C RID: 4956
		PlayLevel2,
		// Token: 0x0400135D RID: 4957
		Exit
	}
}
