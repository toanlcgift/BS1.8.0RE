using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
[Serializable]
public class GameplayModifiers
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x00003DA1 File Offset: 0x00001FA1
	// (set) Token: 0x060002E1 RID: 737 RVA: 0x00003DA9 File Offset: 0x00001FA9
	public GameplayModifiers.EnergyType energyType
	{
		get
		{
			return this._energyType;
		}
		set
		{
			this._energyType = value;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003DB2 File Offset: 0x00001FB2
	// (set) Token: 0x060002E3 RID: 739 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
	public bool batteryEnergy
	{
		get
		{
			return this.energyType == GameplayModifiers.EnergyType.Battery;
		}
		set
		{
			this.energyType = (value ? GameplayModifiers.EnergyType.Battery : (this.energyType = GameplayModifiers.EnergyType.Bar));
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060002E4 RID: 740 RVA: 0x00003DBD File Offset: 0x00001FBD
	// (set) Token: 0x060002E5 RID: 741 RVA: 0x00003DC5 File Offset: 0x00001FC5
	public bool noFail
	{
		get
		{
			return this._noFail;
		}
		set
		{
			this._noFail = value;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060002E6 RID: 742 RVA: 0x00003DCE File Offset: 0x00001FCE
	// (set) Token: 0x060002E7 RID: 743 RVA: 0x00003DD6 File Offset: 0x00001FD6
	public bool demoNoFail
	{
		get
		{
			return this._demoNoFail;
		}
		set
		{
			this._demoNoFail = value;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003DDF File Offset: 0x00001FDF
	// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003DE7 File Offset: 0x00001FE7
	public bool instaFail
	{
		get
		{
			return this._instaFail;
		}
		set
		{
			this._instaFail = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060002EA RID: 746 RVA: 0x00003DF0 File Offset: 0x00001FF0
	// (set) Token: 0x060002EB RID: 747 RVA: 0x00003DF8 File Offset: 0x00001FF8
	public bool failOnSaberClash
	{
		get
		{
			return this._failOnSaberClash;
		}
		set
		{
			this._failOnSaberClash = value;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060002EC RID: 748 RVA: 0x00003E01 File Offset: 0x00002001
	// (set) Token: 0x060002ED RID: 749 RVA: 0x00003E09 File Offset: 0x00002009
	public GameplayModifiers.EnabledObstacleType enabledObstacleType
	{
		get
		{
			return this._enabledObstacleType;
		}
		set
		{
			this._enabledObstacleType = value;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060002EE RID: 750 RVA: 0x00003E12 File Offset: 0x00002012
	// (set) Token: 0x060002EF RID: 751 RVA: 0x00003E1D File Offset: 0x0000201D
	public bool noObstacles
	{
		get
		{
			return this.enabledObstacleType > GameplayModifiers.EnabledObstacleType.All;
		}
		set
		{
			this.enabledObstacleType = (value ? GameplayModifiers.EnabledObstacleType.NoObstacles : GameplayModifiers.EnabledObstacleType.All);
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060002F0 RID: 752 RVA: 0x00003E2C File Offset: 0x0000202C
	// (set) Token: 0x060002F1 RID: 753 RVA: 0x00003E34 File Offset: 0x00002034
	public bool demoNoObstacles
	{
		get
		{
			return this._demoNoObstacles;
		}
		set
		{
			this._demoNoObstacles = value;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060002F2 RID: 754 RVA: 0x00003E3D File Offset: 0x0000203D
	// (set) Token: 0x060002F3 RID: 755 RVA: 0x00003E45 File Offset: 0x00002045
	public bool fastNotes
	{
		get
		{
			return this._fastNotes;
		}
		set
		{
			this._fastNotes = value;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x060002F4 RID: 756 RVA: 0x00003E4E File Offset: 0x0000204E
	// (set) Token: 0x060002F5 RID: 757 RVA: 0x00003E56 File Offset: 0x00002056
	public bool strictAngles
	{
		get
		{
			return this._strictAngles;
		}
		set
		{
			this._strictAngles = value;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060002F6 RID: 758 RVA: 0x00003E5F File Offset: 0x0000205F
	// (set) Token: 0x060002F7 RID: 759 RVA: 0x00003E67 File Offset: 0x00002067
	public bool disappearingArrows
	{
		get
		{
			return this._disappearingArrows;
		}
		set
		{
			this._disappearingArrows = value;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003E70 File Offset: 0x00002070
	// (set) Token: 0x060002F9 RID: 761 RVA: 0x00003E78 File Offset: 0x00002078
	public bool ghostNotes
	{
		get
		{
			return this._ghostNotes;
		}
		set
		{
			this._ghostNotes = value;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060002FA RID: 762 RVA: 0x00003E81 File Offset: 0x00002081
	// (set) Token: 0x060002FB RID: 763 RVA: 0x00003E89 File Offset: 0x00002089
	public bool noBombs
	{
		get
		{
			return this._noBombs;
		}
		set
		{
			this._noBombs = value;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060002FC RID: 764 RVA: 0x00003E92 File Offset: 0x00002092
	// (set) Token: 0x060002FD RID: 765 RVA: 0x00003E9A File Offset: 0x0000209A
	public GameplayModifiers.SongSpeed songSpeed
	{
		get
		{
			return this._songSpeed;
		}
		set
		{
			this._songSpeed = value;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060002FE RID: 766 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
	public float songSpeedMul
	{
		get
		{
			GameplayModifiers.SongSpeed songSpeed = this._songSpeed;
			if (songSpeed == GameplayModifiers.SongSpeed.Faster)
			{
				return 1.2f;
			}
			if (songSpeed == GameplayModifiers.SongSpeed.Slower)
			{
				return 0.85f;
			}
			return 1f;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060002FF RID: 767 RVA: 0x00003EA3 File Offset: 0x000020A3
	// (set) Token: 0x06000300 RID: 768 RVA: 0x00003EAB File Offset: 0x000020AB
	public bool noArrows
	{
		get
		{
			return this._noArrows;
		}
		set
		{
			this._noArrows = value;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000301 RID: 769 RVA: 0x00003EB4 File Offset: 0x000020B4
	public static GameplayModifiers defaultModifiers
	{
		get
		{
			GameplayModifiers gameplayModifiers = new GameplayModifiers();
			gameplayModifiers.ResetToDefault();
			return gameplayModifiers;
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00003EC1 File Offset: 0x000020C1
	public GameplayModifiers()
	{
		this.ResetToDefault();
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
	public GameplayModifiers(GameplayModifiers gameplayModifiers)
	{
		this._demoNoFail = gameplayModifiers._demoNoFail;
		this._demoNoObstacles = false;
		this._noFail = gameplayModifiers._noFail;
		this._instaFail = gameplayModifiers._instaFail;
		this._failOnSaberClash = gameplayModifiers._failOnSaberClash;
		this._fastNotes = gameplayModifiers._fastNotes;
		this._disappearingArrows = gameplayModifiers._disappearingArrows;
		this._noBombs = gameplayModifiers._noBombs;
		this._songSpeed = gameplayModifiers._songSpeed;
		this._enabledObstacleType = gameplayModifiers._enabledObstacleType;
		this._energyType = gameplayModifiers._energyType;
		this._strictAngles = gameplayModifiers._strictAngles;
		this._noArrows = gameplayModifiers._noArrows;
		this._ghostNotes = gameplayModifiers._ghostNotes;
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
	public void ResetToDefault()
	{
		this._demoNoFail = false;
		this._demoNoFail = false;
		this._energyType = GameplayModifiers.EnergyType.Bar;
		this._noFail = false;
		this._instaFail = false;
		this._failOnSaberClash = false;
		this._enabledObstacleType = GameplayModifiers.EnabledObstacleType.All;
		this._noBombs = false;
		this._fastNotes = false;
		this._strictAngles = false;
		this._disappearingArrows = false;
		this._songSpeed = GameplayModifiers.SongSpeed.Normal;
		this._noArrows = false;
		this._ghostNotes = false;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001E820 File Offset: 0x0001CA20
	public bool IsWithoutModifiers()
	{
		return !this._demoNoFail && !this._demoNoObstacles && this._energyType == GameplayModifiers.EnergyType.Bar && !this._noFail && !this._instaFail && !this._failOnSaberClash && this._enabledObstacleType == GameplayModifiers.EnabledObstacleType.All && !this._noBombs && !this._fastNotes && !this._strictAngles && !this._disappearingArrows && this._songSpeed == GameplayModifiers.SongSpeed.Normal && !this._noArrows && !this._ghostNotes;
	}

	// Token: 0x04000361 RID: 865
	[SerializeField]
	private GameplayModifiers.EnergyType _energyType;

	// Token: 0x04000362 RID: 866
	[SerializeField]
	private bool _noFail;

	// Token: 0x04000363 RID: 867
	[SerializeField]
	private bool _demoNoFail;

	// Token: 0x04000364 RID: 868
	[SerializeField]
	private bool _instaFail;

	// Token: 0x04000365 RID: 869
	[SerializeField]
	private bool _failOnSaberClash;

	// Token: 0x04000366 RID: 870
	[SerializeField]
	private GameplayModifiers.EnabledObstacleType _enabledObstacleType;

	// Token: 0x04000367 RID: 871
	[SerializeField]
	private bool _demoNoObstacles;

	// Token: 0x04000368 RID: 872
	[SerializeField]
	private bool _noBombs;

	// Token: 0x04000369 RID: 873
	[SerializeField]
	private bool _fastNotes;

	// Token: 0x0400036A RID: 874
	[SerializeField]
	private bool _strictAngles;

	// Token: 0x0400036B RID: 875
	[SerializeField]
	private bool _disappearingArrows;

	// Token: 0x0400036C RID: 876
	[SerializeField]
	private bool _ghostNotes;

	// Token: 0x0400036D RID: 877
	[SerializeField]
	private GameplayModifiers.SongSpeed _songSpeed;

	// Token: 0x0400036E RID: 878
	[SerializeField]
	private bool _noArrows;

	// Token: 0x020000CC RID: 204
	public enum EnabledObstacleType
	{
		// Token: 0x04000370 RID: 880
		All,
		// Token: 0x04000371 RID: 881
		FullHeightOnly,
		// Token: 0x04000372 RID: 882
		NoObstacles
	}

	// Token: 0x020000CD RID: 205
	public enum EnergyType
	{
		// Token: 0x04000374 RID: 884
		Bar,
		// Token: 0x04000375 RID: 885
		Battery
	}

	// Token: 0x020000CE RID: 206
	public enum SongSpeed
	{
		// Token: 0x04000377 RID: 887
		Normal,
		// Token: 0x04000378 RID: 888
		Faster,
		// Token: 0x04000379 RID: 889
		Slower
	}
}
