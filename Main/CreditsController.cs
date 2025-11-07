using System;
using System.Collections;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200036D RID: 877
public class CreditsController : MonoBehaviour
{
	// Token: 0x06000F86 RID: 3974 RVA: 0x0003E270 File Offset: 0x0003C470
	protected void Start()
	{
		this._buttonBinder = new ButtonBinder(this._continueButton, new Action(this.Finish));
		this._songPreviewPlayer.CrossfadeTo(this._creditsAudioClip, 0f, this._creditsAudioClip.length, 1.5f);
		base.StartCoroutine(this.ScrollCoroutine());
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x0000BE1B File Offset: 0x0000A01B
	protected void OnDestroy()
	{
		if (this._buttonBinder != null)
		{
			this._buttonBinder.ClearBindings();
		}
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0000BE30 File Offset: 0x0000A030
	private void Finish()
	{
		if (this._didFinish)
		{
			return;
		}
		this._didFinish = true;
		this._creditsSceneSetupDataSO.Finish();
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0000BE4D File Offset: 0x0000A04D
	private IEnumerator ScrollCoroutine()
	{
		yield return null;
		float contentHeight = this._contentRectTransform.rect.height;
		float posY = -contentHeight;
		float textHeight = this._textRectTransform.rect.height;
		for (;;)
		{
			this._textRectTransform.anchoredPosition = new Vector2(0f, posY);
			posY += Time.deltaTime * this._scrollingSpeed;
			yield return null;
			if (!this._didFinish && posY > -contentHeight + textHeight + this._overflowHeight)
			{
				this.Finish();
			}
		}
		yield break;
	}

	// Token: 0x04000FDC RID: 4060
	[SerializeField]
	private CreditsScenesTransitionSetupDataSO _creditsSceneSetupDataSO;

	// Token: 0x04000FDD RID: 4061
	[SerializeField]
	private SongPreviewPlayer _songPreviewPlayer;

	// Token: 0x04000FDE RID: 4062
	[SerializeField]
	private AudioClip _creditsAudioClip;

	// Token: 0x04000FDF RID: 4063
	[SerializeField]
	private Button _continueButton;

	// Token: 0x04000FE0 RID: 4064
	[SerializeField]
	private RectTransform _contentRectTransform;

	// Token: 0x04000FE1 RID: 4065
	[SerializeField]
	private RectTransform _textRectTransform;

	// Token: 0x04000FE2 RID: 4066
	[SerializeField]
	private float _scrollingSpeed = 1f;

	// Token: 0x04000FE3 RID: 4067
	[SerializeField]
	private float _overflowHeight = 60f;

	// Token: 0x04000FE4 RID: 4068
	private ButtonBinder _buttonBinder;

	// Token: 0x04000FE5 RID: 4069
	private bool _didFinish;
}
