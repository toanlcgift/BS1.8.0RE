using System;
using Polyglot;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class PresetsSettingsController : ListSettingsController
{
	// Token: 0x06001178 RID: 4472 RVA: 0x0000D453 File Offset: 0x0000B653
	protected override bool GetInitValues(out int idx, out int numberOfElements)
	{
		idx = this._settingsValue;
		idx = Mathf.Clamp(idx, 0, this._presets.namedPresets.Length - 1);
		numberOfElements = this._presets.namedPresets.Length;
		return true;
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0000D48B File Offset: 0x0000B68B
	protected override void ApplyValue(int idx)
	{
		this._settingsValue.value = idx;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0000D499 File Offset: 0x0000B699
	protected override string TextForValue(int idx)
	{
		return Localization.Get(this._presets.namedPresets[idx].presetNameLocalizationKey);
	}

	// Token: 0x04001153 RID: 4435
	[SerializeField]
	private IntSO _settingsValue;

	// Token: 0x04001154 RID: 4436
	[SerializeField]
	private NamedPresetsSO _presets;
}
