using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000394 RID: 916
public class GuestNameButtonsListItem : MonoBehaviour
{
	// Token: 0x1700036F RID: 879
	// (set) Token: 0x060010A2 RID: 4258 RVA: 0x0000CA5A File Offset: 0x0000AC5A
	public string nameText
	{
		set
		{
			this._nameText.text = value;
		}
	}

	// Token: 0x17000370 RID: 880
	// (set) Token: 0x060010A3 RID: 4259 RVA: 0x0000CA68 File Offset: 0x0000AC68
	public Action buttonPressed
	{
		set
		{
			this._buttonPressed = value;
		}
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0000CA71 File Offset: 0x0000AC71
	private void Awake()
	{
		this._button.onClick.AddListener(delegate()
		{
			Action buttonPressed = this._buttonPressed;
			if (buttonPressed == null)
			{
				return;
			}
			buttonPressed();
		});
	}

	// Token: 0x040010BF RID: 4287
	[SerializeField]
	private TextMeshProUGUI _nameText;

	// Token: 0x040010C0 RID: 4288
	[SerializeField]
	private Button _button;

	// Token: 0x040010C1 RID: 4289
	private Action _buttonPressed;
}
