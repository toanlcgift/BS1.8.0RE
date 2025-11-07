using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000360 RID: 864
public class LoadingControl : MonoBehaviour
{
	// Token: 0x1400007F RID: 127
	// (add) Token: 0x06000F35 RID: 3893 RVA: 0x0003D684 File Offset: 0x0003B884
	// (remove) Token: 0x06000F36 RID: 3894 RVA: 0x0003D6BC File Offset: 0x0003B8BC
	public event Action didPressRefreshButtonEvent;

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0000BAE6 File Offset: 0x00009CE6
	public bool isLoading
	{
		get
		{
			return this._loadingIndicator.activeSelf;
		}
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0000BAF3 File Offset: 0x00009CF3
	protected void Awake()
	{
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._refreshButton, delegate
		{
			Action action = this.didPressRefreshButtonEvent;
			if (action == null)
			{
				return;
			}
			action();
		});
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0000BB1D File Offset: 0x00009D1D
	protected void OnDestroy()
	{
		this._buttonBinder.ClearBindings();
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x0000BB2A File Offset: 0x00009D2A
	public void ShowLoading()
	{
		base.gameObject.SetActive(true);
		this._loadingIndicator.SetActive(true);
		this._textContainer.SetActive(false);
		this._downloadingContainer.SetActive(false);
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
	public void ShowText(string text, bool showRefreshButton)
	{
		base.gameObject.SetActive(true);
		this._loadingIndicator.SetActive(false);
		this._textContainer.SetActive(true);
		this._downloadingContainer.SetActive(false);
		this._refreshButton.gameObject.SetActive(showRefreshButton);
		this._text.text = text;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x0003D750 File Offset: 0x0003B950
	public void ShowDownloadingProgress(string text, float downloadingProgress)
	{
		base.gameObject.SetActive(true);
		this._loadingIndicator.SetActive(false);
		this._textContainer.SetActive(false);
		this._downloadingContainer.SetActive(true);
		this._downloadingText.text = text;
		this._donwloadingProgressImage.fillAmount = downloadingProgress;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0000906B File Offset: 0x0000726B
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000F9F RID: 3999
	[SerializeField]
	private GameObject _loadingIndicator;

	// Token: 0x04000FA0 RID: 4000
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000FA1 RID: 4001
	[SerializeField]
	private Button _refreshButton;

	// Token: 0x04000FA2 RID: 4002
	[SerializeField]
	private GameObject _textContainer;

	// Token: 0x04000FA3 RID: 4003
	[SerializeField]
	private GameObject _downloadingContainer;

	// Token: 0x04000FA4 RID: 4004
	[SerializeField]
	private TextMeshProUGUI _downloadingText;

	// Token: 0x04000FA5 RID: 4005
	[SerializeField]
	private Image _donwloadingProgressImage;

	// Token: 0x04000FA7 RID: 4007
	private ButtonBinder _buttonBinder;
}
