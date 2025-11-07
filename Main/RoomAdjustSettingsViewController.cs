using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000408 RID: 1032
public class RoomAdjustSettingsViewController : ViewController
{
	// Token: 0x06001374 RID: 4980 RVA: 0x00048438 File Offset: 0x00046638
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			base.buttonBinder.AddBinding(this._xIncButton, delegate
			{
				this.Move(new Vector3(-0.1f, 0f, 0f));
			});
			base.buttonBinder.AddBinding(this._xDecButton, delegate
			{
				this.Move(new Vector3(0.1f, 0f, 0f));
			});
			base.buttonBinder.AddBinding(this._yIncButton, delegate
			{
				this.Move(new Vector3(0f, -0.05f, 0f));
			});
			base.buttonBinder.AddBinding(this._yDecButton, delegate
			{
				this.Move(new Vector3(0f, 0.05f, 0f));
			});
			base.buttonBinder.AddBinding(this._zIncButton, delegate
			{
				this.Move(new Vector3(0f, 0f, -0.1f));
			});
			base.buttonBinder.AddBinding(this._zDecButton, delegate
			{
				this.Move(new Vector3(0f, 0f, 0.1f));
			});
			base.buttonBinder.AddBinding(this._rotIncButton, delegate
			{
				this.Rotate(-5f);
			});
			base.buttonBinder.AddBinding(this._rotDecButton, delegate
			{
				this.Rotate(5f);
			});
			base.buttonBinder.AddBinding(this._resetButton, delegate
			{
				this.ResetRoom();
			});
		}
		this.RefreshTexts();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0000EA70 File Offset: 0x0000CC70
	private void Move(Vector3 move)
	{
		this._roomCenter.value += move;
		this.RefreshTexts();
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0000EA8F File Offset: 0x0000CC8F
	private void Rotate(float rotation)
	{
		this._roomRotation.value += rotation;
		this.RefreshTexts();
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0000EAAA File Offset: 0x0000CCAA
	private void ResetRoom()
	{
		this._roomCenter.value = new Vector3(0f, 0f, 0f);
		this._roomRotation.value = 0f;
		this.RefreshTexts();
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x00048558 File Offset: 0x00046758
	private void RefreshTexts()
	{
		this._xText.text = (-this._roomCenter.value.x).ToString("F2");
		this._yText.text = (-this._roomCenter.value.y).ToString("F2");
		this._zText.text = (-this._roomCenter.value.z).ToString("F2");
		this._rotText.text = Mathf.RoundToInt(-this._roomRotation.value).ToString();
	}

	// Token: 0x04001332 RID: 4914
	[SerializeField]
	private Vector3SO _roomCenter;

	// Token: 0x04001333 RID: 4915
	[SerializeField]
	private FloatSO _roomRotation;

	// Token: 0x04001334 RID: 4916
	[Space]
	[SerializeField]
	private Button _xIncButton;

	// Token: 0x04001335 RID: 4917
	[SerializeField]
	private Button _xDecButton;

	// Token: 0x04001336 RID: 4918
	[SerializeField]
	private Button _yIncButton;

	// Token: 0x04001337 RID: 4919
	[SerializeField]
	private Button _yDecButton;

	// Token: 0x04001338 RID: 4920
	[SerializeField]
	private Button _zIncButton;

	// Token: 0x04001339 RID: 4921
	[SerializeField]
	private Button _zDecButton;

	// Token: 0x0400133A RID: 4922
	[SerializeField]
	private Button _rotIncButton;

	// Token: 0x0400133B RID: 4923
	[SerializeField]
	private Button _rotDecButton;

	// Token: 0x0400133C RID: 4924
	[SerializeField]
	private Button _resetButton;

	// Token: 0x0400133D RID: 4925
	[Space]
	[SerializeField]
	private TextMeshProUGUI _xText;

	// Token: 0x0400133E RID: 4926
	[SerializeField]
	private TextMeshProUGUI _yText;

	// Token: 0x0400133F RID: 4927
	[SerializeField]
	private TextMeshProUGUI _zText;

	// Token: 0x04001340 RID: 4928
	[SerializeField]
	private TextMeshProUGUI _rotText;

	// Token: 0x04001341 RID: 4929
	private const float kHorizontalMoveStep = 0.1f;

	// Token: 0x04001342 RID: 4930
	private const float kVerticalMoveStep = 0.05f;

	// Token: 0x04001343 RID: 4931
	private const float kRotationStep = 5f;
}
