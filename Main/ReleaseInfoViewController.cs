using System;
using HMUI;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class ReleaseInfoViewController : ViewController
{
	// Token: 0x0600135F RID: 4959 RVA: 0x0000E980 File Offset: 0x0000CB80
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			if (this._mainSettingsModel.playingForTheFirstTime)
			{
				this._textPageScrollView.SetText(this._firstTextAsset.text);
				return;
			}
			this._textPageScrollView.SetText(this._releaseNotesTextAsset.text);
		}
	}

	// Token: 0x04001313 RID: 4883
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x04001314 RID: 4884
	[SerializeField]
	private TextPageScrollView _textPageScrollView;

	// Token: 0x04001315 RID: 4885
	[SerializeField]
	private TextAsset _releaseNotesTextAsset;

	// Token: 0x04001316 RID: 4886
	[SerializeField]
	private TextAsset _firstTextAsset;
}
