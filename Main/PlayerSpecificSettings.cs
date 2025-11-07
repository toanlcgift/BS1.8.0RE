using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
[Serializable]
public class PlayerSpecificSettings
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000823 RID: 2083 RVA: 0x00006B67 File Offset: 0x00004D67
	// (set) Token: 0x06000824 RID: 2084 RVA: 0x00006B6F File Offset: 0x00004D6F
	public bool staticLights
	{
		get
		{
			return this._staticLights;
		}
		set
		{
			this._staticLights = value;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000825 RID: 2085 RVA: 0x00006B78 File Offset: 0x00004D78
	// (set) Token: 0x06000826 RID: 2086 RVA: 0x00006B80 File Offset: 0x00004D80
	public bool leftHanded
	{
		get
		{
			return this._leftHanded;
		}
		set
		{
			this._leftHanded = value;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000827 RID: 2087 RVA: 0x00006B89 File Offset: 0x00004D89
	// (set) Token: 0x06000828 RID: 2088 RVA: 0x00006B91 File Offset: 0x00004D91
	public float playerHeight
	{
		get
		{
			return this._playerHeight;
		}
		set
		{
			this._playerHeight = value;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000829 RID: 2089 RVA: 0x00006B9A File Offset: 0x00004D9A
	// (set) Token: 0x0600082A RID: 2090 RVA: 0x00006BA2 File Offset: 0x00004DA2
	public bool automaticPlayerHeight
	{
		get
		{
			return this._automaticPlayerHeight;
		}
		set
		{
			this._automaticPlayerHeight = value;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x0600082B RID: 2091 RVA: 0x00006BAB File Offset: 0x00004DAB
	// (set) Token: 0x0600082C RID: 2092 RVA: 0x00006BB3 File Offset: 0x00004DB3
	public float sfxVolume
	{
		get
		{
			return this._sfxVolume;
		}
		set
		{
			this._sfxVolume = value;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x0600082D RID: 2093 RVA: 0x00006BBC File Offset: 0x00004DBC
	// (set) Token: 0x0600082E RID: 2094 RVA: 0x00006BC4 File Offset: 0x00004DC4
	public bool reduceDebris
	{
		get
		{
			return this._reduceDebris;
		}
		set
		{
			this._reduceDebris = value;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x0600082F RID: 2095 RVA: 0x00006BCD File Offset: 0x00004DCD
	// (set) Token: 0x06000830 RID: 2096 RVA: 0x00006BD5 File Offset: 0x00004DD5
	public bool noTextsAndHuds
	{
		get
		{
			return this._noTextsAndHuds;
		}
		set
		{
			this._noTextsAndHuds = value;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000831 RID: 2097 RVA: 0x00006BDE File Offset: 0x00004DDE
	// (set) Token: 0x06000832 RID: 2098 RVA: 0x00006BE6 File Offset: 0x00004DE6
	public bool noFailEffects
	{
		get
		{
			return this._noFailEffects;
		}
		set
		{
			this._noFailEffects = value;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000833 RID: 2099 RVA: 0x00006BEF File Offset: 0x00004DEF
	// (set) Token: 0x06000834 RID: 2100 RVA: 0x00006BF7 File Offset: 0x00004DF7
	public bool advancedHud
	{
		get
		{
			return this._advancedHud;
		}
		set
		{
			this._advancedHud = value;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000835 RID: 2101 RVA: 0x00006C00 File Offset: 0x00004E00
	// (set) Token: 0x06000836 RID: 2102 RVA: 0x00006C08 File Offset: 0x00004E08
	public bool autoRestart
	{
		get
		{
			return this._autoRestart;
		}
		set
		{
			this._autoRestart = value;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000837 RID: 2103 RVA: 0x00006C11 File Offset: 0x00004E11
	// (set) Token: 0x06000838 RID: 2104 RVA: 0x00006C19 File Offset: 0x00004E19
	public float saberTrailIntensity
	{
		get
		{
			return this._saberTrailIntensity;
		}
		set
		{
			this._saberTrailIntensity = value;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000839 RID: 2105 RVA: 0x00006C22 File Offset: 0x00004E22
	public static PlayerSpecificSettings defaultSettings
	{
		get
		{
			return new PlayerSpecificSettings();
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00006C29 File Offset: 0x00004E29
	public PlayerSpecificSettings()
	{
		this.ResetToDefault();
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00029FA4 File Offset: 0x000281A4
	public void ResetToDefault()
	{
		this._staticLights = false;
		this._leftHanded = false;
		this._playerHeight = 1.7f;
		this._automaticPlayerHeight = true;
		this._sfxVolume = 0.7f;
		this._reduceDebris = false;
		this._noTextsAndHuds = false;
		this._noFailEffects = false;
		this._advancedHud = false;
		this._autoRestart = false;
		this._saberTrailIntensity = 0.5f;
	}

	// Token: 0x040008B9 RID: 2233
	[SerializeField]
	private bool _staticLights;

	// Token: 0x040008BA RID: 2234
	[SerializeField]
	private bool _leftHanded;

	// Token: 0x040008BB RID: 2235
	[SerializeField]
	private float _playerHeight;

	// Token: 0x040008BC RID: 2236
	[SerializeField]
	private bool _automaticPlayerHeight;

	// Token: 0x040008BD RID: 2237
	[SerializeField]
	private float _sfxVolume;

	// Token: 0x040008BE RID: 2238
	[SerializeField]
	private bool _reduceDebris;

	// Token: 0x040008BF RID: 2239
	[SerializeField]
	private bool _noTextsAndHuds;

	// Token: 0x040008C0 RID: 2240
	[SerializeField]
	private bool _noFailEffects;

	// Token: 0x040008C1 RID: 2241
	[SerializeField]
	private bool _advancedHud;

	// Token: 0x040008C2 RID: 2242
	[SerializeField]
	private bool _autoRestart;

	// Token: 0x040008C3 RID: 2243
	[SerializeField]
	private float _saberTrailIntensity;
}
