using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class LoadingIndicator : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x00002241 File Offset: 0x00000441
	public void ShowLoading(string text = "WORKING ...")
	{
		this._text.text = text;
		this._loadingIndicator.SetActive(true);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000225B File Offset: 0x0000045B
	public void HideLoading()
	{
		this._loadingIndicator.SetActive(false);
	}

	// Token: 0x0400001A RID: 26
	[SerializeField]
	private GameObject _loadingIndicator;

	// Token: 0x0400001B RID: 27
	[SerializeField]
	private Text _text;
}
