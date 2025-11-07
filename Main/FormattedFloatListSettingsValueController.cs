using System;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class FormattedFloatListSettingsValueController : ListSettingsController
{
	// Token: 0x0600115D RID: 4445 RVA: 0x000425A4 File Offset: 0x000407A4
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		idx = 0;
		numberOfElements = this._values.Length;
		for (int i = 0; i < this._values.Length; i++)
		{
			if (this._settingsValue == this._values[i])
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

	// Token: 0x0600115E RID: 4446 RVA: 0x0000D23E File Offset: 0x0000B43E
	protected override void ApplyValue(int idx)
	{
		this._settingsValue.value = this._values[idx];
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x00042628 File Offset: 0x00040828
	protected override string TextForValue(int idx)
	{
		float num = this._values[idx];
		if (this._min != this._max)
		{
			if (this.valueType == FormattedFloatListSettingsValueController.ValueType.Normalized)
			{
				num = (num - this._min) / (this._max - this._min);
			}
			else if (this.valueType == FormattedFloatListSettingsValueController.ValueType.InvertedNormalized)
			{
				num = 1f - (num - this._min) / (this._max - this._min);
			}
		}
		return string.Format(this._formattingString, num);
	}

	// Token: 0x04001140 RID: 4416
	[SerializeField]
	private FloatSO _settingsValue;

	// Token: 0x04001141 RID: 4417
	[SerializeField]
	private float[] _values;

	// Token: 0x04001142 RID: 4418
	[SerializeField]
	private string _formattingString = "{0:0.0}";

	// Token: 0x04001143 RID: 4419
	[SerializeField]
	private FormattedFloatListSettingsValueController.ValueType valueType;

	// Token: 0x04001144 RID: 4420
	private float _min = float.MaxValue;

	// Token: 0x04001145 RID: 4421
	private float _max = float.MinValue;

	// Token: 0x020003B2 RID: 946
	public enum ValueType
	{
		// Token: 0x04001147 RID: 4423
		Normal,
		// Token: 0x04001148 RID: 4424
		Normalized,
		// Token: 0x04001149 RID: 4425
		InvertedNormalized
	}
}
