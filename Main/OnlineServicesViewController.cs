using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FB RID: 1019
public class OnlineServicesViewController : ViewController
{
	// Token: 0x140000AF RID: 175
	// (add) Token: 0x0600131F RID: 4895 RVA: 0x000474E8 File Offset: 0x000456E8
	// (remove) Token: 0x06001320 RID: 4896 RVA: 0x00047520 File Offset: 0x00045720
	public event Action<bool> didFinishEvent;

	// Token: 0x06001321 RID: 4897 RVA: 0x0000E661 File Offset: 0x0000C861
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._enableButton, delegate
			{
				Action<bool> action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action(true);
			});
			base.buttonBinder.AddBinding(this._dontEnableButton, delegate
			{
				Action<bool> action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action(false);
			});
		}
	}

	// Token: 0x040012D4 RID: 4820
	[SerializeField]
	private Button _enableButton;

	// Token: 0x040012D5 RID: 4821
	[SerializeField]
	private Button _dontEnableButton;
}
