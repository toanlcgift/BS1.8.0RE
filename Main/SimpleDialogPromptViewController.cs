using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040B RID: 1035
public class SimpleDialogPromptViewController : ViewController
{
	// Token: 0x0600138B RID: 5003 RVA: 0x000486DC File Offset: 0x000468DC
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			for (int i = 0; i < this._buttons.Length; i++)
			{
				Button button = this._buttons[i];
				int buttonNum = i;
				base.buttonBinder.AddBinding(button, delegate
				{
					this._interactionBlocker.SetActive(true);
					Action<int> didFinishAction = this._didFinishAction;
					if (didFinishAction == null)
					{
						return;
					}
					didFinishAction(buttonNum);
				});
			}
		}
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
	protected override void DidDeactivate(ViewController.DeactivationType deactivationType)
	{
		if (deactivationType == ViewController.DeactivationType.RemovedFromHierarchy)
		{
			this._didFinishAction = null;
		}
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
	public void Init(string title, string message, string buttonText, Action<int> didFinishAction)
	{
		this.Init(title, message, buttonText, null, null, didFinishAction);
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
	public void Init(string title, string message, string firstButtonText, string secondButtonText, Action<int> didFinishAction)
	{
		this.Init(title, message, firstButtonText, secondButtonText, null, didFinishAction);
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00048734 File Offset: 0x00046934
	public void Init(string title, string message, string firstButtonText, string secondButtonText, string thirdButtonText, Action<int> didFinishAction)
	{
		this._interactionBlocker.SetActive(false);
		this._didFinishAction = didFinishAction;
		this._titleText.text = title;
		this._messageText.text = message;
		this._buttonTexts[0].text = firstButtonText;
		this._buttons[0].gameObject.SetActive(!string.IsNullOrEmpty(firstButtonText));
		this._buttonTexts[1].text = secondButtonText;
		this._buttons[1].gameObject.SetActive(!string.IsNullOrEmpty(secondButtonText));
		this._buttonTexts[2].text = thirdButtonText;
		this._buttons[2].gameObject.SetActive(!string.IsNullOrEmpty(thirdButtonText));
	}

	// Token: 0x0400134C RID: 4940
	[SerializeField]
	private TextMeshProUGUI _titleText;

	// Token: 0x0400134D RID: 4941
	[SerializeField]
	private TextMeshProUGUI _messageText;

	// Token: 0x0400134E RID: 4942
	[SerializeField]
	private Button[] _buttons;

	// Token: 0x0400134F RID: 4943
	[SerializeField]
	private TextMeshProUGUI[] _buttonTexts;

	// Token: 0x04001350 RID: 4944
	[SerializeField]
	private GameObject _interactionBlocker;

	// Token: 0x04001351 RID: 4945
	private Action<int> _didFinishAction;
}
