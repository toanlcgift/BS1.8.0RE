using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class GameplayModifiersModelSO : PersistentScriptableObject
{
	// Token: 0x06000306 RID: 774 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
	public bool GetModifierBoolValue(GameplayModifiers gameplayModifiers, GameplayModifierParamsSO gameplayModifierParams)
	{
		if (gameplayModifierParams == this._batteryEnergy)
		{
			return gameplayModifiers.batteryEnergy;
		}
		if (gameplayModifierParams == this._noFail)
		{
			return gameplayModifiers.noFail;
		}
		if (gameplayModifierParams == this._demoNoFail)
		{
			return gameplayModifiers.demoNoFail;
		}
		if (gameplayModifierParams == this._instaFail)
		{
			return gameplayModifiers.instaFail;
		}
		if (gameplayModifierParams == this._noObstacles)
		{
			return gameplayModifiers.noObstacles;
		}
		if (gameplayModifierParams == this._demoNoObstacles)
		{
			return gameplayModifiers.demoNoObstacles;
		}
		if (gameplayModifierParams == this._noBombs)
		{
			return gameplayModifiers.noBombs;
		}
		if (gameplayModifierParams == this._fastNotes)
		{
			return gameplayModifiers.fastNotes;
		}
		if (gameplayModifierParams == this._strictAngles)
		{
			return gameplayModifiers.strictAngles;
		}
		if (gameplayModifierParams == this._disappearingArrows)
		{
			return gameplayModifiers.disappearingArrows;
		}
		if (gameplayModifierParams == this._fasterSong)
		{
			return gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Faster;
		}
		if (gameplayModifierParams == this._slowerSong)
		{
			return gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Slower;
		}
		if (gameplayModifierParams == this._noArrows)
		{
			return gameplayModifiers.noArrows;
		}
		return gameplayModifierParams == this._ghostNotes && gameplayModifiers.ghostNotes;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0001E9DC File Offset: 0x0001CBDC
	public void SetModifierBoolValue(GameplayModifiers gameplayModifiers, GameplayModifierParamsSO gameplayModifierParams, bool value)
	{
		if (value)
		{
			foreach (GameplayModifierParamsSO gameplayModifierParamsSO in gameplayModifierParams.mutuallyExclusives)
			{
				if (gameplayModifierParams != gameplayModifierParamsSO)
				{
					this.SetModifierBoolValue(gameplayModifiers, gameplayModifierParamsSO, false);
				}
			}
		}
		if (gameplayModifierParams == this._batteryEnergy)
		{
			gameplayModifiers.batteryEnergy = value;
			return;
		}
		if (gameplayModifierParams == this._noFail)
		{
			gameplayModifiers.noFail = value;
			return;
		}
		if (gameplayModifierParams == this._demoNoFail)
		{
			gameplayModifiers.demoNoFail = value;
			return;
		}
		if (gameplayModifierParams == this._instaFail)
		{
			gameplayModifiers.instaFail = value;
			return;
		}
		if (gameplayModifierParams == this._noObstacles)
		{
			gameplayModifiers.noObstacles = value;
			return;
		}
		if (gameplayModifierParams == this._demoNoObstacles)
		{
			gameplayModifiers.demoNoObstacles = value;
			return;
		}
		if (gameplayModifierParams == this._noBombs)
		{
			gameplayModifiers.noBombs = value;
			return;
		}
		if (gameplayModifierParams == this._fastNotes)
		{
			gameplayModifiers.fastNotes = value;
			return;
		}
		if (gameplayModifierParams == this._strictAngles)
		{
			gameplayModifiers.strictAngles = value;
			return;
		}
		if (gameplayModifierParams == this._disappearingArrows)
		{
			gameplayModifiers.disappearingArrows = value;
			return;
		}
		if (gameplayModifierParams == this._fasterSong)
		{
			gameplayModifiers.songSpeed = (value ? GameplayModifiers.SongSpeed.Faster : GameplayModifiers.SongSpeed.Normal);
			return;
		}
		if (gameplayModifierParams == this._slowerSong)
		{
			gameplayModifiers.songSpeed = (value ? GameplayModifiers.SongSpeed.Slower : GameplayModifiers.SongSpeed.Normal);
			return;
		}
		if (gameplayModifierParams == this._noArrows)
		{
			gameplayModifiers.noArrows = value;
			return;
		}
		if (gameplayModifierParams == this._ghostNotes)
		{
			gameplayModifiers.ghostNotes = value;
		}
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0001EB58 File Offset: 0x0001CD58
	public List<GameplayModifierParamsSO> GetModifierParams(GameplayModifiers gameplayModifiers)
	{
		List<GameplayModifierParamsSO> list = new List<GameplayModifierParamsSO>(12);
		if (gameplayModifiers.batteryEnergy)
		{
			list.Add(this._batteryEnergy);
		}
		if (gameplayModifiers.noFail)
		{
			list.Add(this._noFail);
		}
		if (gameplayModifiers.demoNoFail)
		{
			list.Add(this._demoNoFail);
		}
		if (gameplayModifiers.instaFail)
		{
			list.Add(this._instaFail);
		}
		if (gameplayModifiers.noObstacles)
		{
			list.Add(this._noObstacles);
		}
		if (gameplayModifiers.demoNoObstacles)
		{
			list.Add(this._demoNoObstacles);
		}
		if (gameplayModifiers.noBombs)
		{
			list.Add(this._noBombs);
		}
		if (gameplayModifiers.fastNotes)
		{
			list.Add(this._fastNotes);
		}
		if (gameplayModifiers.strictAngles)
		{
			list.Add(this._strictAngles);
		}
		if (gameplayModifiers.disappearingArrows)
		{
			list.Add(this._disappearingArrows);
		}
		if (gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Faster)
		{
			list.Add(this._fasterSong);
		}
		if (gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Slower)
		{
			list.Add(this._slowerSong);
		}
		if (gameplayModifiers.noArrows)
		{
			list.Add(this._noArrows);
		}
		if (gameplayModifiers.ghostNotes)
		{
			list.Add(this._ghostNotes);
		}
		return list;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0001EC88 File Offset: 0x0001CE88
	public float GetTotalMultiplier(GameplayModifiers gameplayModifiers)
	{
		float num = 1f;
		foreach (GameplayModifierParamsSO gameplayModifierParamsSO in this.GetModifierParams(gameplayModifiers))
		{
			num += gameplayModifierParamsSO.multiplier;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00003ECF File Offset: 0x000020CF
	public int MaxModifiedScoreForMaxRawScore(int maxRawScore, GameplayModifiers gameplayModifiers)
	{
		return this.GetModifiedScoreForGameplayModifiers(maxRawScore, gameplayModifiers);
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00003ECF File Offset: 0x000020CF
	public int MaxModifiedScoreForMaxRawScore(int maxRawScore, GameplayModifiers gameplayModifiers, GameplayModifiersModelSO gameplayModifiersModel)
	{
		return this.GetModifiedScoreForGameplayModifiers(maxRawScore, gameplayModifiers);
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
	private int GetModifiedScoreForGameplayModifiers(int rawScore, GameplayModifiers gameplayModifiers)
	{
		float totalMultiplier = this.GetTotalMultiplier(gameplayModifiers);
		return ScoreModel.GetModifiedScoreForGameplayModifiersScoreMultiplier(rawScore, totalMultiplier);
	}

	// Token: 0x0400037A RID: 890
	[SerializeField]
	private GameplayModifierParamsSO _batteryEnergy;

	// Token: 0x0400037B RID: 891
	[SerializeField]
	private GameplayModifierParamsSO _noFail;

	// Token: 0x0400037C RID: 892
	[SerializeField]
	private GameplayModifierParamsSO _instaFail;

	// Token: 0x0400037D RID: 893
	[SerializeField]
	private GameplayModifierParamsSO _noObstacles;

	// Token: 0x0400037E RID: 894
	[SerializeField]
	private GameplayModifierParamsSO _noBombs;

	// Token: 0x0400037F RID: 895
	[SerializeField]
	private GameplayModifierParamsSO _fastNotes;

	// Token: 0x04000380 RID: 896
	[SerializeField]
	private GameplayModifierParamsSO _strictAngles;

	// Token: 0x04000381 RID: 897
	[SerializeField]
	private GameplayModifierParamsSO _disappearingArrows;

	// Token: 0x04000382 RID: 898
	[SerializeField]
	private GameplayModifierParamsSO _fasterSong;

	// Token: 0x04000383 RID: 899
	[SerializeField]
	private GameplayModifierParamsSO _slowerSong;

	// Token: 0x04000384 RID: 900
	[SerializeField]
	private GameplayModifierParamsSO _noArrows;

	// Token: 0x04000385 RID: 901
	[SerializeField]
	private GameplayModifierParamsSO _ghostNotes;

	// Token: 0x04000386 RID: 902
	[Space]
	[SerializeField]
	private GameplayModifierParamsSO _demoNoObstacles;

	// Token: 0x04000387 RID: 903
	[SerializeField]
	private GameplayModifierParamsSO _demoNoFail;
}
