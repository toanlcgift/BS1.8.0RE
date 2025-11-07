using System;
using Polyglot;
using UnityEngine;

// Token: 0x020003AE RID: 942
public class BoolSettingsController : SwitchSettingsController
{
	// Token: 0x06001151 RID: 4433 RVA: 0x0000D190 File Offset: 0x0000B390
	protected override bool GetInitValue()
	{
		return this._settingsValue;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0000D19D File Offset: 0x0000B39D
	protected override void ApplyValue(bool value)
	{
		this._settingsValue.value = value;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0000D1AB File Offset: 0x0000B3AB
	protected override string TextForValue(bool value)
	{
		if (!value)
		{
			return Localization.Get(this._offLocalizedKey);
		}
		return Localization.Get(this._onLocalizedKey);
	}

	// Token: 0x04001131 RID: 4401
	[SerializeField]
	private BoolSO _settingsValue;

	// Token: 0x04001132 RID: 4402
	[SerializeField]
	[LocalizationKey]
	private string _onLocalizedKey;

	// Token: 0x04001133 RID: 4403
	[SerializeField]
	[LocalizationKey]
	private string _offLocalizedKey;
}
