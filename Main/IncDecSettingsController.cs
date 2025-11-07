using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003B3 RID: 947
public abstract class IncDecSettingsController : MonoBehaviour
{
	// Token: 0x1700039C RID: 924
	// (set) Token: 0x06001161 RID: 4449 RVA: 0x0000D27C File Offset: 0x0000B47C
	protected bool enableDec
	{
		set
		{
			this._decButton.interactable = value;
		}
	}

	// Token: 0x1700039D RID: 925
	// (set) Token: 0x06001162 RID: 4450 RVA: 0x0000D28A File Offset: 0x0000B48A
	protected bool enableInc
	{
		set
		{
			this._incButton.interactable = value;
		}
	}

	// Token: 0x1700039E RID: 926
	// (set) Token: 0x06001163 RID: 4451 RVA: 0x0000D298 File Offset: 0x0000B498
	protected string text
	{
		set
		{
			this._text.text = value;
		}
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0000D2A6 File Offset: 0x0000B4A6
	protected virtual void OnEnable()
	{
		this._incButton.onClick.AddListener(new UnityAction(this.IncButtonPressed));
		this._decButton.onClick.AddListener(new UnityAction(this.DecButtonPressed));
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0000D2E2 File Offset: 0x0000B4E2
	protected void OnDisable()
	{
		this._incButton.onClick.RemoveListener(new UnityAction(this.IncButtonPressed));
		this._decButton.onClick.RemoveListener(new UnityAction(this.DecButtonPressed));
	}

	// Token: 0x06001166 RID: 4454
	public abstract void IncButtonPressed();

	// Token: 0x06001167 RID: 4455
	public abstract void DecButtonPressed();

	// Token: 0x0400114A RID: 4426
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x0400114B RID: 4427
	[SerializeField]
	private Button _decButton;

	// Token: 0x0400114C RID: 4428
	[SerializeField]
	private Button _incButton;
}
