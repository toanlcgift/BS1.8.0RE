using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class MainEffectGraphicsSettingsPresetsSO : NamedPresetsSO
{
	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00005740 File Offset: 0x00003940
	public MainEffectGraphicsSettingsPresetsSO.Preset[] presets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00023FE4 File Offset: 0x000221E4
	public override NamedPreset[] namedPresets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x0400060D RID: 1549
	[SerializeField]
	private MainEffectGraphicsSettingsPresetsSO.Preset[] _presets;

	// Token: 0x02000179 RID: 377
	[Serializable]
	public class Preset : NamedPreset
	{
		// Token: 0x0400060E RID: 1550
		public MainEffectSO mainEffect;
	}
}
