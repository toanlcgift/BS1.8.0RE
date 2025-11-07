using System;
using HMUI;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class ControllersTransformSettingsViewController : ViewController
{
	// Token: 0x060011E6 RID: 4582 RVA: 0x000436C0 File Offset: 0x000418C0
	protected override void DidActivate(bool firstActivation, ViewController.ActivationType activationType)
	{
		if (firstActivation)
		{
			float num = 10f;
			float num2 = -num;
			int numberOfSteps = Mathf.RoundToInt((num - num2) / 0.1f) + 1;
			this._posXSlider.minValue = num2;
			this._posXSlider.maxValue = num;
			this._posXSlider.numberOfSteps = numberOfSteps;
			this._posYSlider.minValue = num2;
			this._posYSlider.maxValue = num;
			this._posYSlider.numberOfSteps = numberOfSteps;
			this._posZSlider.minValue = num2;
			this._posZSlider.maxValue = num;
			this._posZSlider.numberOfSteps = numberOfSteps;
			this._posXSlider.valueDidChangeEvent += this.HandlePositionSliderValueDidChange;
			this._posYSlider.valueDidChangeEvent += this.HandlePositionSliderValueDidChange;
			this._posZSlider.valueDidChangeEvent += this.HandlePositionSliderValueDidChange;
			float num3 = 180f;
			float num4 = -num3;
			int numberOfSteps2 = Mathf.RoundToInt((num3 - num4) / 1f) + 1;
			this._rotXSlider.minValue = num4;
			this._rotXSlider.maxValue = num3;
			this._rotXSlider.numberOfSteps = numberOfSteps2;
			this._rotYSlider.minValue = num4;
			this._rotYSlider.maxValue = num3;
			this._rotYSlider.numberOfSteps = numberOfSteps2;
			this._rotZSlider.minValue = num4;
			this._rotZSlider.maxValue = num3;
			this._rotZSlider.numberOfSteps = numberOfSteps2;
			this._rotXSlider.valueDidChangeEvent += this.HandleRotationSliderValueDidChange;
			this._rotYSlider.valueDidChangeEvent += this.HandleRotationSliderValueDidChange;
			this._rotZSlider.valueDidChangeEvent += this.HandleRotationSliderValueDidChange;
		}
		if (activationType == ViewController.ActivationType.AddedToHierarchy)
		{
			this._posXSlider.value = this._controllerPosition.value.x * 100f;
			this._posYSlider.value = this._controllerPosition.value.y * 100f;
			this._posZSlider.value = this._controllerPosition.value.z * 100f;
			this._rotXSlider.value = this._controllerRotation.value.x;
			this._rotYSlider.value = this._controllerRotation.value.y;
			this._rotZSlider.value = this._controllerRotation.value.z;
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0004392C File Offset: 0x00041B2C
	protected override void OnDestroy()
	{
		if (this._posXSlider)
		{
			this._posXSlider.valueDidChangeEvent -= this.HandlePositionSliderValueDidChange;
		}
		if (this._posYSlider)
		{
			this._posXSlider.valueDidChangeEvent -= this.HandlePositionSliderValueDidChange;
		}
		if (this._posZSlider)
		{
			this._posXSlider.valueDidChangeEvent -= this.HandlePositionSliderValueDidChange;
		}
		if (this._rotXSlider)
		{
			this._rotXSlider.valueDidChangeEvent -= this.HandleRotationSliderValueDidChange;
		}
		if (this._rotYSlider)
		{
			this._rotXSlider.valueDidChangeEvent -= this.HandleRotationSliderValueDidChange;
		}
		if (this._rotZSlider)
		{
			this._rotXSlider.valueDidChangeEvent -= this.HandleRotationSliderValueDidChange;
		}
		base.OnDestroy();
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x00043A18 File Offset: 0x00041C18
	private void HandlePositionSliderValueDidChange(RangeValuesTextSlider slider, float value)
	{
		this._controllerPosition.value = new Vector3(this._posXSlider.value / 100f, this._posYSlider.value / 100f, this._posZSlider.value / 100f);
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
	private void HandleRotationSliderValueDidChange(RangeValuesTextSlider slider, float value)
	{
		this._controllerRotation.value = new Vector3(this._rotXSlider.value, this._rotYSlider.value, this._rotZSlider.value);
	}

	// Token: 0x040011A3 RID: 4515
	[SerializeField]
	private Vector3SO _controllerPosition;

	// Token: 0x040011A4 RID: 4516
	[SerializeField]
	private Vector3SO _controllerRotation;

	// Token: 0x040011A5 RID: 4517
	[Space]
	[SerializeField]
	private RangeValuesTextSlider _posXSlider;

	// Token: 0x040011A6 RID: 4518
	[SerializeField]
	private RangeValuesTextSlider _posYSlider;

	// Token: 0x040011A7 RID: 4519
	[SerializeField]
	private RangeValuesTextSlider _posZSlider;

	// Token: 0x040011A8 RID: 4520
	[SerializeField]
	private RangeValuesTextSlider _rotXSlider;

	// Token: 0x040011A9 RID: 4521
	[SerializeField]
	private RangeValuesTextSlider _rotYSlider;

	// Token: 0x040011AA RID: 4522
	[SerializeField]
	private RangeValuesTextSlider _rotZSlider;

	// Token: 0x040011AB RID: 4523
	private const float kPositionStep = 0.1f;

	// Token: 0x040011AC RID: 4524
	private const float kPositionMul = 100f;

	// Token: 0x040011AD RID: 4525
	private const float kRotationStep = 1f;
}
