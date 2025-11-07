using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class MissionDataSO : PersistentScriptableObject
{
	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x000058EE File Offset: 0x00003AEE
	public MissionObjective[] missionObjectives
	{
		get
		{
			return this._missionObjectives;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000622 RID: 1570 RVA: 0x000058F6 File Offset: 0x00003AF6
	public BeatmapLevelSO level
	{
		get
		{
			return this._level;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x000058FE File Offset: 0x00003AFE
	public BeatmapCharacteristicSO beatmapCharacteristic
	{
		get
		{
			return this._beatmapCharacteristic;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000624 RID: 1572 RVA: 0x00005906 File Offset: 0x00003B06
	public BeatmapDifficulty beatmapDifficulty
	{
		get
		{
			return this._beatmapDifficulty;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000590E File Offset: 0x00003B0E
	public GameplayModifiers gameplayModifiers
	{
		get
		{
			return this._gameplayModifiers;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x00005916 File Offset: 0x00003B16
	public MissionHelpSO missionHelp
	{
		get
		{
			return this._missionHelp;
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0000591E File Offset: 0x00003B1E
	private void OnValidate()
	{
		if (this._level != null)
		{
			this._level.beatmapLevelData.GetDifficultyBeatmap(this._beatmapCharacteristic, this._beatmapDifficulty);
		}
	}

	// Token: 0x0400069D RID: 1693
	[SerializeField]
	private BeatmapLevelSO _level;

	// Token: 0x0400069E RID: 1694
	[SerializeField]
	private BeatmapCharacteristicSO _beatmapCharacteristic;

	// Token: 0x0400069F RID: 1695
	[SerializeField]
	private BeatmapDifficulty _beatmapDifficulty;

	// Token: 0x040006A0 RID: 1696
	[SerializeField]
	private MissionObjective[] _missionObjectives;

	// Token: 0x040006A1 RID: 1697
	[SerializeField]
	private GameplayModifiers _gameplayModifiers;

	// Token: 0x040006A2 RID: 1698
	[Space]
	[SerializeField]
	[NullAllowed]
	private MissionHelpSO _missionHelp;
}
