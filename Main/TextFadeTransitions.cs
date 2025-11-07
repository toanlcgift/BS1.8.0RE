using System;
using TMPro;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class TextFadeTransitions : MonoBehaviour
{
	// Token: 0x06000DED RID: 3565 RVA: 0x0000AC01 File Offset: 0x00008E01
	protected void Awake()
	{
		base.enabled = false;
		this._state = TextFadeTransitions.State.NotInTransition;
		this._fade = 0f;
		this._textLabel.text = "";
		this.RefreshTextAlpha();
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x0000AC32 File Offset: 0x00008E32
	protected void Update()
	{
		this.RefreshState();
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00039DA8 File Offset: 0x00037FA8
	private void RefreshState()
	{
		switch (this._state)
		{
		case TextFadeTransitions.State.NotInTransition:
			if (this._nextText == null)
			{
				base.enabled = false;
				return;
			}
			if (this._fade == 0f)
			{
				this._textLabel.text = this._nextText;
				this._nextText = null;
				this._state = TextFadeTransitions.State.FadingIn;
				return;
			}
			this._state = TextFadeTransitions.State.FadingOut;
			return;
		case TextFadeTransitions.State.FadingOut:
			this.RefreshTextAlpha();
			this._fade = Mathf.Max(this._fade - Time.deltaTime / this._fadeDuration, 0f);
			if (this._fade == 0f)
			{
				this._state = TextFadeTransitions.State.NotInTransition;
			}
			break;
		case TextFadeTransitions.State.FadingIn:
			this._fade = Mathf.Min(this._fade + Time.deltaTime / this._fadeDuration, 1f);
			this.RefreshTextAlpha();
			if (this._fade == 1f)
			{
				this._state = TextFadeTransitions.State.NotInTransition;
				return;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00039E90 File Offset: 0x00038090
	private void RefreshTextAlpha()
	{
		if (this._canvasGroup != null)
		{
			this._canvasGroup.alpha = this._fade;
		}
		else
		{
			Color color = this._textLabel.color;
			color.a = this._fade;
			this._textLabel.color = color;
		}
		this._textLabel.enabled = (this._fade != 0f);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0000AC3A File Offset: 0x00008E3A
	public void ShowText(string text)
	{
		if (this._nextText == text || this._textLabel.text == text)
		{
			return;
		}
		this._nextText = text;
		base.enabled = true;
	}

	// Token: 0x04000E44 RID: 3652
	[SerializeField]
	private TextMeshProUGUI _textLabel;

	// Token: 0x04000E45 RID: 3653
	[Tooltip("If Canvas Group is specified, it is used for fadeing instead of Text Label color.")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	// Token: 0x04000E46 RID: 3654
	[SerializeField]
	private float _fadeDuration = 0.4f;

	// Token: 0x04000E47 RID: 3655
	private TextFadeTransitions.State _state;

	// Token: 0x04000E48 RID: 3656
	private string _nextText;

	// Token: 0x04000E49 RID: 3657
	private float _fade;

	// Token: 0x0200031E RID: 798
	private enum State
	{
		// Token: 0x04000E4B RID: 3659
		NotInTransition,
		// Token: 0x04000E4C RID: 3660
		FadingOut,
		// Token: 0x04000E4D RID: 3661
		FadingIn
	}
}
