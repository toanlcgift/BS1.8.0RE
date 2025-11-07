using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200042B RID: 1067
public class StandardLevelBuyInfoView : MonoBehaviour
{
	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x0600145E RID: 5214 RVA: 0x0000F5FA File Offset: 0x0000D7FA
	public Button buyLevelButton
	{
		get
		{
			return this._buyLevelButton;
		}
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x0600145F RID: 5215 RVA: 0x0000F602 File Offset: 0x0000D802
	public Button openPackButton
	{
		get
		{
			return this._openPackButton;
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06001460 RID: 5216 RVA: 0x0000F60A File Offset: 0x0000D80A
	public Button buyPackButton
	{
		get
		{
			return this._buyPackButton;
		}
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x0000F612 File Offset: 0x0000D812
	public void RefreshView(string infoText, bool canBuyPack)
	{
		this._text.text = infoText;
		this._buyPackButton.gameObject.SetActive(canBuyPack);
		this._openPackButton.gameObject.SetActive(!canBuyPack);
	}

	// Token: 0x040013FF RID: 5119
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04001400 RID: 5120
	[SerializeField]
	private Button _buyLevelButton;

	// Token: 0x04001401 RID: 5121
	[SerializeField]
	private Button _openPackButton;

	// Token: 0x04001402 RID: 5122
	[SerializeField]
	private Button _buyPackButton;
}
