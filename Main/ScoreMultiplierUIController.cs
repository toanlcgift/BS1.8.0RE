using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020002B2 RID: 690
public class ScoreMultiplierUIController : MonoBehaviour
{
	// Token: 0x06000BAD RID: 2989 RVA: 0x00034F68 File Offset: 0x00033168
	protected void Start()
	{
		this.RegisterForEvents();
		this._prevMultiplier = 1;
		for (int i = 0; i < this._multiplierTexts.Length; i++)
		{
			this._multiplierTexts[i].text = "1";
		}
		this._multiplierProgressImage.fillAmount = 0f;
		this._multiplierIncreasedTriggerId = Animator.StringToHash("MultiplierIncreased");
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x00009363 File Offset: 0x00007563
	protected void OnEnable()
	{
		this.RegisterForEvents();
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0000936B File Offset: 0x0000756B
	protected void OnDisable()
	{
		this.UnregisterFromEvents();
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00009373 File Offset: 0x00007573
	private void RegisterForEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.multiplierDidChangeEvent -= this.HandleMultiplierDidChange;
		this._scoreController.multiplierDidChangeEvent += this.HandleMultiplierDidChange;
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000093B2 File Offset: 0x000075B2
	private void UnregisterFromEvents()
	{
		if (this._scoreController == null)
		{
			return;
		}
		this._scoreController.multiplierDidChangeEvent -= this.HandleMultiplierDidChange;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00034FC8 File Offset: 0x000331C8
	protected void Update()
	{
		if (Mathf.Abs(this._progressTarget - this._multiplierProgressImage.fillAmount) > 0.001f)
		{
			this._multiplierProgressImage.fillAmount = Mathf.Lerp(this._multiplierProgressImage.fillAmount, this._progressTarget, Time.deltaTime * 4f);
		}
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00035020 File Offset: 0x00033220
	private void HandleMultiplierDidChange(int multiplier, float progress)
	{
		if (this._prevMultiplier < multiplier)
		{
			this._multiplierAnimator.SetTrigger(this._multiplierIncreasedTriggerId);
			this._multiplierProgressImage.fillAmount = 0f;
		}
		this._prevMultiplier = multiplier;
		string text = multiplier.ToString();
		for (int i = 0; i < this._multiplierTexts.Length; i++)
		{
			this._multiplierTexts[i].text = text;
		}
		this._progressTarget = progress;
	}

	// Token: 0x04000C5B RID: 3163
	[SerializeField]
	private TextMeshProUGUI[] _multiplierTexts;

	// Token: 0x04000C5C RID: 3164
	[SerializeField]
	private Image _multiplierProgressImage;

	// Token: 0x04000C5D RID: 3165
	[SerializeField]
	private Animator _multiplierAnimator;

	// Token: 0x04000C5E RID: 3166
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000C5F RID: 3167
	private int _prevMultiplier;

	// Token: 0x04000C60 RID: 3168
	private int _multiplierIncreasedTriggerId;

	// Token: 0x04000C61 RID: 3169
	private float _progressTarget;
}
