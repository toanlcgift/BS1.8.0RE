using System;
using UnityEngine;
using Zenject;

// Token: 0x020002AB RID: 683
public class FeverModeUIPanel : MonoBehaviour
{
	// Token: 0x06000B81 RID: 2945 RVA: 0x00034690 File Offset: 0x00032890
	protected void Start()
	{
		this._feverTextRectWidth = this._feverBGTextRectTransform.sizeDelta.x;
		this._scoreController.feverDidStartEvent += this.HandleFeverModeDidStart;
		this._scoreController.feverDidFinishEvent += this.HandleFeverModeDidFinish;
		this._scoreController.feverModeChargeProgressDidChangeEvent += this.HandleFeverModeChargeProgressDidChange;
		this.SetProgress(0f);
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00034704 File Offset: 0x00032904
	protected void OnDestroy()
	{
		if (this._scoreController)
		{
			this._scoreController.feverDidStartEvent -= this.HandleFeverModeDidStart;
			this._scoreController.feverDidFinishEvent -= this.HandleFeverModeDidFinish;
			this._scoreController.feverModeChargeProgressDidChangeEvent -= this.HandleFeverModeChargeProgressDidChange;
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0000913C File Offset: 0x0000733C
	protected void Update()
	{
		if (this._scoreController.feverModeActive)
		{
			this.SetProgress(1f - this._scoreController.feverModeDrainProgress);
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00009162 File Offset: 0x00007362
	private void SetProgress(float progress)
	{
		this._feverBGTextRectTransform.sizeDelta = new Vector2(this._feverTextRectWidth * progress, this._feverBGTextRectTransform.sizeDelta.y);
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0000918C File Offset: 0x0000738C
	private void HandleFeverModeDidStart()
	{
		this.SetProgress(0f);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0000918C File Offset: 0x0000738C
	private void HandleFeverModeDidFinish()
	{
		this.SetProgress(0f);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00009199 File Offset: 0x00007399
	private void HandleFeverModeChargeProgressDidChange(float progress)
	{
		this.SetProgress(progress);
	}

	// Token: 0x04000C2E RID: 3118
	[SerializeField]
	private RectTransform _feverBGTextRectTransform;

	// Token: 0x04000C2F RID: 3119
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000C30 RID: 3120
	private float _feverTextRectWidth;
}
