using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020003D9 RID: 985
public class HealthWarningViewController : ViewController
{
	// Token: 0x14000099 RID: 153
	// (add) Token: 0x06001238 RID: 4664 RVA: 0x00044688 File Offset: 0x00042888
	// (remove) Token: 0x06001239 RID: 4665 RVA: 0x000446C0 File Offset: 0x000428C0
	public event Action privacyPolicyButtonPressedEvent;

	// Token: 0x1400009A RID: 154
	// (add) Token: 0x0600123A RID: 4666 RVA: 0x000446F8 File Offset: 0x000428F8
	// (remove) Token: 0x0600123B RID: 4667 RVA: 0x00044730 File Offset: 0x00042930
	public event Action openDataPrivacyPageButtonPressedEvent;

	// Token: 0x1400009B RID: 155
	// (add) Token: 0x0600123C RID: 4668 RVA: 0x00044768 File Offset: 0x00042968
	// (remove) Token: 0x0600123D RID: 4669 RVA: 0x000447A0 File Offset: 0x000429A0
	public event Action didFinishEvent;

	// Token: 0x0600123E RID: 4670 RVA: 0x000447D8 File Offset: 0x000429D8
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._openDataPrivacyPageButton.gameObject.SetActive(this._analyticsModel.supportsOpenDataPrivacyPage);
			bool active = this._appStaticSettings.requirePrivacyPolicy | this._analyticsModel.supportsOpenDataPrivacyPage;
			this._privacyPolicyButton.gameObject.SetActive(active);
			this._privacyAgreeToPrivacyPolicyLabel.SetActive(active);
			base.buttonBinder.AddBinding(this._continueButton, delegate
			{
				Action action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action();
			});
			base.buttonBinder.AddBinding(this._privacyPolicyButton, delegate
			{
				Action action = this.privacyPolicyButtonPressedEvent;
				if (action == null)
				{
					return;
				}
				action();
			});
			base.buttonBinder.AddBinding(this._openDataPrivacyPageButton, delegate
			{
				Action action = this.openDataPrivacyPageButtonPressedEvent;
				if (action != null)
				{
					action();
				}
				this._analyticsModel.OpenDataPrivacyPage();
			});
		}
	}

	// Token: 0x040011FA RID: 4602
	[SerializeField]
	private Button _continueButton;

	// Token: 0x040011FB RID: 4603
	[SerializeField]
	private Button _privacyPolicyButton;

	// Token: 0x040011FC RID: 4604
	[SerializeField]
	private Button _openDataPrivacyPageButton;

	// Token: 0x040011FD RID: 4605
	[SerializeField]
	private GameObject _privacyAgreeToPrivacyPolicyLabel;

	// Token: 0x040011FE RID: 4606
	[Inject]
	private IAnalyticsModel _analyticsModel;

	// Token: 0x040011FF RID: 4607
	[Inject]
	private AppStaticSettingsSO _appStaticSettings;
}
