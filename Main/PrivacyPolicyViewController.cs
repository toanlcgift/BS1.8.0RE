using System;
using HMUI;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class PrivacyPolicyViewController : ViewController
{
	// Token: 0x06001355 RID: 4949 RVA: 0x0000E93D File Offset: 0x0000CB3D
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			this._textPageScrollView.SetText(this._privacyPolicyTextAsset.text);
		}
	}

	// Token: 0x04001308 RID: 4872
	[SerializeField]
	private TextPageScrollView _textPageScrollView;

	// Token: 0x04001309 RID: 4873
	[SerializeField]
	private TextAsset _privacyPolicyTextAsset;
}
