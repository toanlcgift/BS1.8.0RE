using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040F RID: 1039
public class SoloModeSelectionViewController : ViewController
{
	// Token: 0x140000B6 RID: 182
	// (add) Token: 0x0600139C RID: 5020 RVA: 0x000488E0 File Offset: 0x00046AE0
	// (remove) Token: 0x0600139D RID: 5021 RVA: 0x00048918 File Offset: 0x00046B18
	public event Action<SoloModeSelectionViewController, SoloModeSelectionViewController.MenuType> didFinishEvent;

	// Token: 0x0600139E RID: 5022 RVA: 0x00048950 File Offset: 0x00046B50
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._freePlayModeButton, delegate
			{
				this.HandleMenuButton(SoloModeSelectionViewController.MenuType.FreePlayMode);
			});
			base.buttonBinder.AddBinding(this._oneSaberModeButton, delegate
			{
				this.HandleMenuButton(SoloModeSelectionViewController.MenuType.OneSaberMode);
			});
			base.buttonBinder.AddBinding(this._noArrowsModeButton, delegate
			{
				this.HandleMenuButton(SoloModeSelectionViewController.MenuType.NoArrowsMode);
			});
			base.buttonBinder.AddBinding(this._dismissButton, delegate
			{
				this.HandleMenuButton(SoloModeSelectionViewController.MenuType.Back);
			});
		}
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x0000EC6A File Offset: 0x0000CE6A
	private void HandleMenuButton(SoloModeSelectionViewController.MenuType subMenuType)
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, subMenuType);
		}
	}

	// Token: 0x0400135E RID: 4958
	[SerializeField]
	private Button _freePlayModeButton;

	// Token: 0x0400135F RID: 4959
	[SerializeField]
	private Button _oneSaberModeButton;

	// Token: 0x04001360 RID: 4960
	[SerializeField]
	private Button _noArrowsModeButton;

	// Token: 0x04001361 RID: 4961
	[SerializeField]
	private Button _dismissButton;

	// Token: 0x02000410 RID: 1040
	public enum MenuType
	{
		// Token: 0x04001364 RID: 4964
		FreePlayMode,
		// Token: 0x04001365 RID: 4965
		NoArrowsMode,
		// Token: 0x04001366 RID: 4966
		OneSaberMode,
		// Token: 0x04001367 RID: 4967
		Back
	}
}
