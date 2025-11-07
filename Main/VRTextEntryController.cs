using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class VRTextEntryController : MonoBehaviour
{
	// Token: 0x14000084 RID: 132
	// (add) Token: 0x06000F6E RID: 3950 RVA: 0x0003DE94 File Offset: 0x0003C094
	// (remove) Token: 0x06000F6F RID: 3951 RVA: 0x0003DECC File Offset: 0x0003C0CC
	public event Action<string> textDidChangeEvent;

	// Token: 0x14000085 RID: 133
	// (add) Token: 0x06000F70 RID: 3952 RVA: 0x0003DF04 File Offset: 0x0003C104
	// (remove) Token: 0x06000F71 RID: 3953 RVA: 0x0003DF3C File Offset: 0x0003C13C
	public event Action okButtonWasPressedEvent;

	// Token: 0x14000086 RID: 134
	// (add) Token: 0x06000F72 RID: 3954 RVA: 0x0003DF74 File Offset: 0x0003C174
	// (remove) Token: 0x06000F73 RID: 3955 RVA: 0x0003DFAC File Offset: 0x0003C1AC
	public event Action cancelButtonWasPressedEvent;

	// Token: 0x17000341 RID: 833
	// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0000BD87 File Offset: 0x00009F87
	public bool hideCancelButton
	{
		set
		{
			this._uiKeyboard.hideCancelButton = value;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0000BD95 File Offset: 0x00009F95
	// (set) Token: 0x06000F76 RID: 3958 RVA: 0x0003DFE4 File Offset: 0x0003C1E4
	public string text
	{
		get
		{
			return this._text.text;
		}
		set
		{
			this._text.text = value;
			Action<string> action = this.textDidChangeEvent;
			if (action != null)
			{
				action(this._text.text);
			}
			if (!this._allowBlank)
			{
				this._uiKeyboard.enableOkButtonInteractivity = (this._text.text.Length > 0);
			}
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x0003E040 File Offset: 0x0003C240
	protected void Awake()
	{
		this._uiKeyboard.textKeyWasPressedEvent += this.HandleUIKeyboardTextKeyWasPressed;
		this._uiKeyboard.deleteButtonWasPressedEvent += this.HandleUIKeyboardDeleteButtonWasPressed;
		this._uiKeyboard.okButtonWasPressedEvent += delegate()
		{
			Action action = this.okButtonWasPressedEvent;
			if (action == null)
			{
				return;
			}
			action();
		};
		this._uiKeyboard.cancelButtonWasPressedEvent += delegate()
		{
			Action action = this.cancelButtonWasPressedEvent;
			if (action == null)
			{
				return;
			}
			action();
		};
		this._uiKeyboard.enableOkButtonInteractivity = this._allowBlank;
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0000BDA2 File Offset: 0x00009FA2
	protected void OnEnable()
	{
		this._stopBlinkingCursor = false;
		base.StartCoroutine(this.BlinkCursor());
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0000BDB8 File Offset: 0x00009FB8
	protected void OnDisable()
	{
		this._stopBlinkingCursor = true;
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0000BDC1 File Offset: 0x00009FC1
	private IEnumerator BlinkCursor()
	{
		Color cursorColor = this._cursorText.color;
		while (!this._stopBlinkingCursor)
		{
			this._cursorText.color = Color.clear;
			yield return new WaitForSeconds(0.4f);
			this._cursorText.color = cursorColor;
			yield return new WaitForSeconds(0.4f);
		}
		yield break;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0003E0BC File Offset: 0x0003C2BC
	private void HandleUIKeyboardTextKeyWasPressed(char key)
	{
		if (this._text.text.Length < this._maxLength)
		{
			TextMeshProUGUI text = this._text;
			text.text += key.ToString().ToUpper();
			Action<string> action = this.textDidChangeEvent;
			if (action != null)
			{
				action(this._text.text);
			}
		}
		this._uiKeyboard.enableOkButtonInteractivity = true;
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0003E12C File Offset: 0x0003C32C
	private void HandleUIKeyboardDeleteButtonWasPressed()
	{
		if (this._text.text.Length > 0)
		{
			this._text.text = this._text.text.Remove(this._text.text.Length - 1);
			Action<string> action = this.textDidChangeEvent;
			if (action != null)
			{
				action(this._text.text);
			}
		}
		if (!this._allowBlank)
		{
			this._uiKeyboard.enableOkButtonInteractivity = (this._text.text.Length > 0);
		}
	}

	// Token: 0x04000FCF RID: 4047
	[SerializeField]
	private UIKeyboard _uiKeyboard;

	// Token: 0x04000FD0 RID: 4048
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000FD1 RID: 4049
	[SerializeField]
	private TextMeshProUGUI _cursorText;

	// Token: 0x04000FD2 RID: 4050
	[SerializeField]
	private int _maxLength = 20;

	// Token: 0x04000FD3 RID: 4051
	[SerializeField]
	private bool _allowBlank;

	// Token: 0x04000FD7 RID: 4055
	private bool _stopBlinkingCursor;
}
