using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class BloomPrePassGraphicsSettingsPresetsSO : NamedPresetsSO
{
	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x00005728 File Offset: 0x00003928
	public BloomPrePassGraphicsSettingsPresetsSO.Preset[] presets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060005ED RID: 1517 RVA: 0x00023FCC File Offset: 0x000221CC
	public override NamedPreset[] namedPresets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x0400060B RID: 1547
	[SerializeField]
	private BloomPrePassGraphicsSettingsPresetsSO.Preset[] _presets;

	// Token: 0x02000177 RID: 375
	[Serializable]
	public class Preset : NamedPreset
	{
		// Token: 0x0400060C RID: 1548
		public BloomPrePassEffectSO bloomPrePassEffect;
	}
}
