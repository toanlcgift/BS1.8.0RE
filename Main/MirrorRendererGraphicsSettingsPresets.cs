using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class MirrorRendererGraphicsSettingsPresets : NamedPresetsSO
{
	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000579D File Offset: 0x0000399D
	public MirrorRendererGraphicsSettingsPresets.Preset[] presets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0002493C File Offset: 0x00022B3C
	public override NamedPreset[] namedPresets
	{
		get
		{
			return this._presets;
		}
	}

	// Token: 0x04000687 RID: 1671
	[SerializeField]
	private MirrorRendererGraphicsSettingsPresets.Preset[] _presets;

	// Token: 0x02000180 RID: 384
	[Serializable]
	public class Preset : NamedPreset
	{
		// Token: 0x04000688 RID: 1672
		public LayerMask reflectLayers = -1;

		// Token: 0x04000689 RID: 1673
		public int stereoTextureWidth = 2048;

		// Token: 0x0400068A RID: 1674
		public int stereoTextureHeight = 1024;

		// Token: 0x0400068B RID: 1675
		public int monoTextureWidth = 256;

		// Token: 0x0400068C RID: 1676
		public int monoTextureHeight = 256;

		// Token: 0x0400068D RID: 1677
		public int maxAntiAliasing = 1;

		// Token: 0x0400068E RID: 1678
		public bool enableBloomPrePassFog;
	}
}
