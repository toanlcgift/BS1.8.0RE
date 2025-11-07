using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003CF RID: 975
public class EnterTextViewController : ViewController
{
	// Token: 0x14000096 RID: 150
	// (add) Token: 0x060011FA RID: 4602 RVA: 0x00043BD4 File Offset: 0x00041DD4
	// (remove) Token: 0x060011FB RID: 4603 RVA: 0x00043C0C File Offset: 0x00041E0C
	public event Action<EnterTextViewController, string> didFinishEvent;

	// Token: 0x060011FC RID: 4604 RVA: 0x0000DB79 File Offset: 0x0000BD79
	public void Init(string titleText)
	{
		this._titleText.text = titleText;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x0000DB87 File Offset: 0x0000BD87
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._okButton, new Action(this.OkButtonPressed));
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._textEntryController.text = "";
		}
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x00043C44 File Offset: 0x00041E44
	public void OkButtonPressed()
	{
		if (this.didFinishEvent != null)
		{
			string text = this._textEntryController.text;
			if (text.Length == 0)
			{
				text = "PLAYER";
			}
			this.didFinishEvent(this, text);
		}
	}

	// Token: 0x040011BB RID: 4539
	[SerializeField]
	private VRTextEntryController _textEntryController;

	// Token: 0x040011BC RID: 4540
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x040011BD RID: 4541
	[SerializeField]
	private Button _okButton;
}
