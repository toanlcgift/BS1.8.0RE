using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class FormattedFloatListSettingsController : ListSettingsController
{
	// Token: 0x14000090 RID: 144
	// (add) Token: 0x06001155 RID: 4437 RVA: 0x00042424 File Offset: 0x00040624
	// (remove) Token: 0x06001156 RID: 4438 RVA: 0x0004245C File Offset: 0x0004065C
	public event Action<FormattedFloatListSettingsController, float> valueDidChangeEvent;

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06001157 RID: 4439 RVA: 0x0000D1CF File Offset: 0x0000B3CF
	public float value
	{
		get
		{
			return this._value;
		}
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
	public void SetValue(float value, bool callCallback = false)
	{
		this._hasValue = true;
		this._value = value;
		base.Refresh(callCallback);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x00042494 File Offset: 0x00040694
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		idx = 0;
		if (!this._hasValue)
		{
			numberOfElements = 0;
			return false;
		}
		numberOfElements = this._values.Length;
		for (int i = 0; i < this._values.Length; i++)
		{
			if (Mathf.Approximately(this._value, this._values[i]))
			{
				idx = i;
			}
			if (this._values[i] < this._min)
			{
				this._min = this._values[i];
			}
			if (this._values[i] > this._max)
			{
				this._max = this._values[i];
			}
		}
		return true;
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0000D1EE File Offset: 0x0000B3EE
	protected override void ApplyValue(int idx)
	{
		this._value = this._values[idx];
		Action<FormattedFloatListSettingsController, float> action = this.valueDidChangeEvent;
		if (action == null)
		{
			return;
		}
		action(this, this._value);
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x00042524 File Offset: 0x00040724
	protected override string TextForValue(int idx)
	{
		float num = this._values[idx];
		if (this._min != this._max)
		{
			if (this.valueType == FormattedFloatListSettingsController.ValueType.Normalized)
			{
				num = (num - this._min) / (this._max - this._min);
			}
			else if (this.valueType == FormattedFloatListSettingsController.ValueType.InvertedNormalized)
			{
				num = 1f - (num - this._min) / (this._max - this._min);
			}
		}
		return string.Format(this._formattingString, num);
	}

	// Token: 0x04001134 RID: 4404
	[SerializeField]
	private float[] _values;

	// Token: 0x04001135 RID: 4405
	[SerializeField]
	private string _formattingString = "{0:0.0}";

	// Token: 0x04001136 RID: 4406
	[SerializeField]
	private FormattedFloatListSettingsController.ValueType valueType;

	// Token: 0x04001138 RID: 4408
	private float _value;

	// Token: 0x04001139 RID: 4409
	private float _min = float.MaxValue;

	// Token: 0x0400113A RID: 4410
	private float _max = float.MinValue;

	// Token: 0x0400113B RID: 4411
	private bool _hasValue;

	// Token: 0x020003B0 RID: 944
	public enum ValueType
	{
		// Token: 0x0400113D RID: 4413
		Normal,
		// Token: 0x0400113E RID: 4414
		Normalized,
		// Token: 0x0400113F RID: 4415
		InvertedNormalized
	}
}
