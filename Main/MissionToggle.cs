using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003AC RID: 940
public class MissionToggle : UIBehaviour, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x1400008F RID: 143
	// (add) Token: 0x0600113C RID: 4412 RVA: 0x00042204 File Offset: 0x00040404
	// (remove) Token: 0x0600113D RID: 4413 RVA: 0x0004223C File Offset: 0x0004043C
	public event Action<MissionToggle> selectionDidChangeEvent;

	// Token: 0x17000397 RID: 919
	// (set) Token: 0x0600113E RID: 4414 RVA: 0x0000CFFA File Offset: 0x0000B1FA
	public bool missionCleared
	{
		set
		{
			this._missionCleared = value;
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x0600113F RID: 4415 RVA: 0x0000D003 File Offset: 0x0000B203
	// (set) Token: 0x06001140 RID: 4416 RVA: 0x0000D00B File Offset: 0x0000B20B
	public bool selected
	{
		get
		{
			return this._selected;
		}
		set
		{
			this.ChangeSelection(value, true, false);
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06001141 RID: 4417 RVA: 0x0000D016 File Offset: 0x0000B216
	// (set) Token: 0x06001142 RID: 4418 RVA: 0x0000D01E File Offset: 0x0000B21E
	public bool interactable
	{
		get
		{
			return this._interactable;
		}
		set
		{
			this._interactable = value;
			this.RefreshUI();
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06001143 RID: 4419 RVA: 0x0000D02D File Offset: 0x0000B22D
	public bool highlighted
	{
		get
		{
			return this._highlighted;
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0000D035 File Offset: 0x0000B235
	protected override void Start()
	{
		base.Start();
		this.RefreshUI();
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0000D043 File Offset: 0x0000B243
	public void ChangeSelection(bool value, bool callSelectionDidChange, bool ignoreCurrentValue)
	{
		if (!ignoreCurrentValue && this._selected == value)
		{
			return;
		}
		this._selected = value;
		this.RefreshUI();
		if (callSelectionDidChange && this.selectionDidChangeEvent != null)
		{
			this.selectionDidChangeEvent(this);
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0000D076 File Offset: 0x0000B276
	public void ChangeHighlight(bool value, bool ignoreCurrentValue)
	{
		if (!ignoreCurrentValue && this._highlighted == value)
		{
			return;
		}
		this._highlighted = value;
		this.RefreshUI();
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0000D092 File Offset: 0x0000B292
	public void SetText(string text)
	{
		this._text.text = text;
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
	private void InternalToggle()
	{
		if (!this.selected)
		{
			this.selected = true;
		}
		this.RefreshUI();
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x00042274 File Offset: 0x00040474
	private void RefreshUI()
	{
		this._vrInteractable.interactable = this._interactable;
		if (!this._interactable)
		{
			this._bgImage.enabled = false;
			this._lockedImage.enabled = true;
			this._clearedImage.enabled = false;
			this._text.enabled = false;
			this._lockedImage.color = this._disabledColor;
			this._strokeImage.color = this._disabledColor;
			this._strokeGlowImage.enabled = false;
			this._strokeImage.rectTransform.sizeDelta = new Vector2(0f, 0f);
			return;
		}
		this._lockedImage.enabled = false;
		this._clearedImage.enabled = this._missionCleared;
		this._text.enabled = true;
		this._text.color = (this._selected ? this._invertColor : this._normalColor);
		this._clearedImage.color = this._text.color;
		this._strokeGlowImage.enabled = !this._missionCleared;
		this._strokeImage.color = this._normalColor;
		this._strokeImage.rectTransform.sizeDelta = ((this._highlighted || this._selected) ? new Vector2(1f, 1f) : new Vector2(0f, 0f));
		this._bgImage.enabled = (this._selected || this._highlighted);
		this._bgImage.color = ((this._highlighted && !this._selected) ? this._highlightColor : this._normalColor);
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (!this._interactable)
		{
			return;
		}
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		this.InternalToggle();
		if (this._missionToggleWasPressedSignal != null)
		{
			this._missionToggleWasPressedSignal.Raise();
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0000D0EA File Offset: 0x0000B2EA
	public virtual void OnSubmit(BaseEventData eventData)
	{
		if (!this._interactable)
		{
			return;
		}
		this.InternalToggle();
		if (this._missionToggleWasPressedSignal != null)
		{
			this._missionToggleWasPressedSignal.Raise();
		}
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0000D114 File Offset: 0x0000B314
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (!this._interactable)
		{
			return;
		}
		this.ChangeHighlight(true, false);
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0000D127 File Offset: 0x0000B327
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (!this._interactable)
		{
			return;
		}
		this.ChangeHighlight(false, false);
	}

	// Token: 0x0400111E RID: 4382
	[SerializeField]
	[SignalSender]
	private Signal _missionToggleWasPressedSignal;

	// Token: 0x0400111F RID: 4383
	[Space]
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04001120 RID: 4384
	[SerializeField]
	private Image _lockedImage;

	// Token: 0x04001121 RID: 4385
	[SerializeField]
	private Image _clearedImage;

	// Token: 0x04001122 RID: 4386
	[SerializeField]
	private Image _bgImage;

	// Token: 0x04001123 RID: 4387
	[SerializeField]
	private Image _strokeImage;

	// Token: 0x04001124 RID: 4388
	[SerializeField]
	private Image _strokeGlowImage;

	// Token: 0x04001125 RID: 4389
	[Space]
	[SerializeField]
	private Interactable _vrInteractable;

	// Token: 0x04001126 RID: 4390
	[Space]
	[SerializeField]
	private Color _disabledColor = Color.white.ColorWithAlpha(0.1f);

	// Token: 0x04001127 RID: 4391
	[SerializeField]
	private Color _normalColor = Color.white;

	// Token: 0x04001128 RID: 4392
	[SerializeField]
	private Color _invertColor = Color.black;

	// Token: 0x04001129 RID: 4393
	[SerializeField]
	private Color _highlightColor = Color.blue;

	// Token: 0x0400112B RID: 4395
	private bool _selected;

	// Token: 0x0400112C RID: 4396
	private bool _highlighted;

	// Token: 0x0400112D RID: 4397
	private bool _interactable;

	// Token: 0x0400112E RID: 4398
	private bool _missionCleared;
}
