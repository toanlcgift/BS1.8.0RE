using System;
using TMPro;
using UnityEngine;
using Zenject;

// Token: 0x020002A8 RID: 680
public class ComboUIController : MonoBehaviour
{
	// Token: 0x06000B76 RID: 2934 RVA: 0x00009079 File Offset: 0x00007279
	protected void Start()
	{
		this._comboLostID = Animator.StringToHash("ComboLost");
		this.RegisterForEvents();
		this._comboText.text = "0";
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x000090A1 File Offset: 0x000072A1
	protected void OnEnable()
	{
		this.RegisterForEvents();
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x000090A9 File Offset: 0x000072A9
	protected void OnDisable()
	{
		this.UnregisterFromEvents();
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000345B0 File Offset: 0x000327B0
	private void RegisterForEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.comboDidChangeEvent -= this.HandleComboDidChange;
		this._scoreController.comboDidChangeEvent += this.HandleComboDidChange;
		this._scoreController.comboBreakingEventHappenedEvent -= this.HandleComboBreakingEventHappened;
		this._scoreController.comboBreakingEventHappenedEvent += this.HandleComboBreakingEventHappened;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x000090B1 File Offset: 0x000072B1
	private void UnregisterFromEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.comboDidChangeEvent -= this.HandleComboDidChange;
		this._scoreController.comboBreakingEventHappenedEvent -= this.HandleComboBreakingEventHappened;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x000090F0 File Offset: 0x000072F0
	private void HandleComboDidChange(int combo)
	{
		this._comboText.text = combo.ToString();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x00009104 File Offset: 0x00007304
	private void HandleComboBreakingEventHappened()
	{
		if (!this._comboLost)
		{
			this._comboLost = true;
			this._animator.SetTrigger(this._comboLostID);
		}
	}

	// Token: 0x04000C22 RID: 3106
	[SerializeField]
	private TextMeshProUGUI _comboText;

	// Token: 0x04000C23 RID: 3107
	[SerializeField]
	private Animator _animator;

	// Token: 0x04000C24 RID: 3108
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000C25 RID: 3109
	private int _comboLostID;

	// Token: 0x04000C26 RID: 3110
	private bool _comboLost;
}
