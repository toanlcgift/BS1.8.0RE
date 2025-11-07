using System;
using HMUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020003D0 RID: 976
public class EulaViewController : ViewController
{
	// Token: 0x14000097 RID: 151
	// (add) Token: 0x06001200 RID: 4608 RVA: 0x00043C80 File Offset: 0x00041E80
	// (remove) Token: 0x06001201 RID: 4609 RVA: 0x00043CB8 File Offset: 0x00041EB8
	public event Action<bool> didFinishEvent;

	// Token: 0x06001202 RID: 4610 RVA: 0x00043CF0 File Offset: 0x00041EF0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._agreeButton, delegate
			{
				Action<bool> action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action(true);
			});
			base.buttonBinder.AddBinding(this._doNotAgreeButton, delegate
			{
				Action<bool> action = this.didFinishEvent;
				if (action == null)
				{
					return;
				}
				action(false);
			});
			this._doNotAgreeButton.gameObject.SetActive(this._initData.showDoNotAgreeButton);
			this._textPageScrollView.SetText(this._eulaTextAsset.text);
		}
	}

	// Token: 0x040011BF RID: 4543
	[SerializeField]
	private Button _agreeButton;

	// Token: 0x040011C0 RID: 4544
	[SerializeField]
	private Button _doNotAgreeButton;

	// Token: 0x040011C1 RID: 4545
	[SerializeField]
	private TextPageScrollView _textPageScrollView;

	// Token: 0x040011C2 RID: 4546
	[SerializeField]
	private TextAsset _eulaTextAsset;

	// Token: 0x040011C4 RID: 4548
	[Inject]
	private EulaViewController.InitData _initData;

	// Token: 0x020003D1 RID: 977
	public class InitData
	{
		// Token: 0x06001206 RID: 4614 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
		public InitData(bool showDoNotAgreeButton)
		{
			this.showDoNotAgreeButton = showDoNotAgreeButton;
		}

		// Token: 0x040011C5 RID: 4549
		public readonly bool showDoNotAgreeButton;
	}
}
