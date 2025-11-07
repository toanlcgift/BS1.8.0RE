using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

// Token: 0x02000009 RID: 9
public class EventsPanelButton : MonoBehaviour
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000014 RID: 20 RVA: 0x000021C8 File Offset: 0x000003C8
	// (set) Token: 0x06000015 RID: 21 RVA: 0x000021D5 File Offset: 0x000003D5
	public bool isOn
	{
		get
		{
			return this._toggle.isOn;
		}
		set
		{
			this._toggle.isOn = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000016 RID: 22 RVA: 0x000021E3 File Offset: 0x000003E3
	// (set) Token: 0x06000017 RID: 23 RVA: 0x000021EB File Offset: 0x000003EB
	public bool callChangeCallback { get; set; }

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000018 RID: 24 RVA: 0x000021F4 File Offset: 0x000003F4
	public int eventValue
	{
		get
		{
			return this._eventValue;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000019 RID: 25 RVA: 0x000021FC File Offset: 0x000003FC
	public string eventID
	{
		get
		{
			return this._eventID;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00014B98 File Offset: 0x00012D98
	public void Init(string eventID, int eventValue, Sprite sprite, Color color, string hintText, Action<bool> onValueChangedCallback, ToggleGroup toggleGroup)
	{
		this._eventID = eventID;
		this._eventValue = eventValue;
		this._text.enabled = false;
		this._image.sprite = sprite;
		this._bgImage.color = color;
		this._onValueChangedCallback = onValueChangedCallback;
		this._toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleToggleValueChanged));
		this._toggle.onValueChanged.AddListener(new UnityAction<bool>(this.HandleToggleValueChanged));
		this._toggle.group = toggleGroup;
		this._hoverHint.text = hintText;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00014C34 File Offset: 0x00012E34
	public void Init(string eventID, int eventValue, string text, Color color, string hintText, Action<bool> onValueChangedCallback, ToggleGroup toggleGroup)
	{
		this._eventID = eventID;
		this._eventValue = eventValue;
		this._image.enabled = false;
		this._text.text = text;
		this._bgImage.color = color;
		this._onValueChangedCallback = onValueChangedCallback;
		this._toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleToggleValueChanged));
		this._toggle.onValueChanged.AddListener(new UnityAction<bool>(this.HandleToggleValueChanged));
		this._toggle.group = toggleGroup;
		this._hoverHint.text = hintText;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002204 File Offset: 0x00000404
	protected void OnDestroy()
	{
		this._toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleToggleValueChanged));
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002222 File Offset: 0x00000422
	private void HandleToggleValueChanged(bool isOn)
	{
		if (!this.callChangeCallback)
		{
			return;
		}
		this._onValueChangedCallback(isOn);
	}

	// Token: 0x04000011 RID: 17
	[SerializeField]
	private Toggle _toggle;

	// Token: 0x04000012 RID: 18
	[SerializeField]
	private Image _image;

	// Token: 0x04000013 RID: 19
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04000014 RID: 20
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000015 RID: 21
	[SerializeField]
	private HoverHint _hoverHint;

	// Token: 0x04000017 RID: 23
	private Action<bool> _onValueChangedCallback;

	// Token: 0x04000018 RID: 24
	private int _eventValue;

	// Token: 0x04000019 RID: 25
	private string _eventID;

	// Token: 0x0200000A RID: 10
	public class Factory : PlaceholderFactory<EventsPanelButton>
	{
	}
}
