using System;
using Polyglot;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000369 RID: 873
public class UIKeyboard : MonoBehaviour
{
	// Token: 0x14000080 RID: 128
	// (add) Token: 0x06000F5C RID: 3932 RVA: 0x0003D9D4 File Offset: 0x0003BBD4
	// (remove) Token: 0x06000F5D RID: 3933 RVA: 0x0003DA0C File Offset: 0x0003BC0C
	public event Action<char> textKeyWasPressedEvent;

	// Token: 0x14000081 RID: 129
	// (add) Token: 0x06000F5E RID: 3934 RVA: 0x0003DA44 File Offset: 0x0003BC44
	// (remove) Token: 0x06000F5F RID: 3935 RVA: 0x0003DA7C File Offset: 0x0003BC7C
	public event Action deleteButtonWasPressedEvent;

	// Token: 0x14000082 RID: 130
	// (add) Token: 0x06000F60 RID: 3936 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
	// (remove) Token: 0x06000F61 RID: 3937 RVA: 0x0003DAEC File Offset: 0x0003BCEC
	public event Action okButtonWasPressedEvent;

	// Token: 0x14000083 RID: 131
	// (add) Token: 0x06000F62 RID: 3938 RVA: 0x0003DB24 File Offset: 0x0003BD24
	// (remove) Token: 0x06000F63 RID: 3939 RVA: 0x0003DB5C File Offset: 0x0003BD5C
	public event Action cancelButtonWasPressedEvent;

	// Token: 0x1700033F RID: 831
	// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0000BCD6 File Offset: 0x00009ED6
	public bool enableOkButtonInteractivity
	{
		set
		{
			this._okButtonInteractivity = value;
			if (this._okButton != null)
			{
				this._okButton.interactable = value;
			}
		}
	}

	// Token: 0x17000340 RID: 832
	// (set) Token: 0x06000F65 RID: 3941 RVA: 0x0000BCF9 File Offset: 0x00009EF9
	public bool hideCancelButton
	{
		set
		{
			this._hideCancelButton = value;
			if (this._cancelButton != null)
			{
				this._cancelButton.gameObject.SetActive(!value);
			}
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0003DB94 File Offset: 0x0003BD94
	protected void Awake()
	{
		string[] array = new string[]
		{
			"q",
			"w",
			"e",
			"r",
			"t",
			"y",
			"u",
			"i",
			"o",
			"p",
			"a",
			"s",
			"d",
			"f",
			"g",
			"h",
			"j",
			"k",
			"l",
			"z",
			"x",
			"c",
			"v",
			"b",
			"n",
			"m",
			"<-",
			"space",
			Localization.Get("BUTTON_OK"),
			Localization.Get("BUTTON_CANCEL")
		};
		for (int i = 0; i < array.Length; i++)
		{
			RectTransform parent = base.transform.GetChild(i) as RectTransform;
			TextMeshProButton textMeshProButton = UnityEngine.Object.Instantiate<TextMeshProButton>(this._keyButtonPrefab, parent);
			textMeshProButton.text.text = array[i];
			RectTransform rectTransform = textMeshProButton.transform as RectTransform;
			rectTransform.localPosition = Vector2.zero;
			rectTransform.localScale = Vector3.one;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			Navigation navigation = textMeshProButton.button.navigation;
			navigation.mode = Navigation.Mode.None;
			textMeshProButton.button.navigation = navigation;
			if (i < array.Length - 4)
			{
				string key = array[i];
				textMeshProButton.button.onClick.AddListener(delegate()
				{
					Action<char> action = this.textKeyWasPressedEvent;
					if (action == null)
					{
						return;
					}
					action(key[0]);
				});
			}
			else if (i == array.Length - 4)
			{
				textMeshProButton.button.onClick.AddListener(delegate()
				{
					Action action = this.deleteButtonWasPressedEvent;
					if (action == null)
					{
						return;
					}
					action();
				});
			}
			else if (i == array.Length - 1)
			{
				this._cancelButton = textMeshProButton.button;
				this._cancelButton.gameObject.SetActive(!this._hideCancelButton);
				textMeshProButton.button.onClick.AddListener(delegate()
				{
					this.cancelButtonWasPressedEvent();
				});
			}
			else if (i == array.Length - 2)
			{
				this._okButton = textMeshProButton.button;
				this._okButton.interactable = this._okButtonInteractivity;
				textMeshProButton.button.onClick.AddListener(delegate()
				{
					this.okButtonWasPressedEvent();
				});
			}
			else
			{
				textMeshProButton.button.onClick.AddListener(delegate()
				{
					Action<char> action = this.textKeyWasPressedEvent;
					if (action == null)
					{
						return;
					}
					action(' ');
				});
			}
		}
	}

	// Token: 0x04000FC4 RID: 4036
	[SerializeField]
	private TextMeshProButton _keyButtonPrefab;

	// Token: 0x04000FC9 RID: 4041
	private Button _okButton;

	// Token: 0x04000FCA RID: 4042
	private Button _cancelButton;

	// Token: 0x04000FCB RID: 4043
	private bool _okButtonInteractivity;

	// Token: 0x04000FCC RID: 4044
	private bool _hideCancelButton;
}
