using System;
using System.Collections.Generic;
using LeaderboardsDTO;

// Token: 0x0200019C RID: 412
public static class GameplayModifiersHelper
{
	// Token: 0x0600067F RID: 1663 RVA: 0x00024F10 File Offset: 0x00023110
	public static GameplayModifiersDTO[] ToDTO(GameplayModifiers gameplayModifiers)
	{
		List<GameplayModifiersDTO> list = new List<GameplayModifiersDTO>();
		if (gameplayModifiers.noFail)
		{
			list.Add(GameplayModifiersDTO.NoFail);
		}
		if (gameplayModifiers.instaFail)
		{
			list.Add(GameplayModifiersDTO.InstaFail);
		}
		if (gameplayModifiers.failOnSaberClash)
		{
			list.Add(GameplayModifiersDTO.FailOnSaberClash);
		}
		if (gameplayModifiers.fastNotes)
		{
			list.Add(GameplayModifiersDTO.FastNotes);
		}
		if (gameplayModifiers.disappearingArrows)
		{
			list.Add(GameplayModifiersDTO.DisappearingArrows);
		}
		if (gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Faster)
		{
			list.Add(GameplayModifiersDTO.SongSpeedFaster);
		}
		else if (gameplayModifiers.songSpeed == GameplayModifiers.SongSpeed.Slower)
		{
			list.Add(GameplayModifiersDTO.SongSpeedSlower);
		}
		if (gameplayModifiers.enabledObstacleType == GameplayModifiers.EnabledObstacleType.FullHeightOnly)
		{
			list.Add(GameplayModifiersDTO.EnabledObstacleTypeFullHeightOnly);
		}
		else if (gameplayModifiers.enabledObstacleType == GameplayModifiers.EnabledObstacleType.NoObstacles)
		{
			list.Add(GameplayModifiersDTO.EnabledObstacleTypeNoObstacles);
		}
		if (gameplayModifiers.energyType == GameplayModifiers.EnergyType.Battery)
		{
			list.Add(GameplayModifiersDTO.EnergyTypeBattery);
		}
		if (gameplayModifiers.strictAngles)
		{
			list.Add(GameplayModifiersDTO.StrictAngles);
		}
		if (gameplayModifiers.noArrows)
		{
			list.Add(GameplayModifiersDTO.NoArrows);
		}
		if (gameplayModifiers.ghostNotes)
		{
			list.Add(GameplayModifiersDTO.GhostNotes);
		}
		return list.ToArray();
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x00025014 File Offset: 0x00023214
	public static GameplayModifiers FromDTO(GameplayModifiersDTO[] gameplayModifiersDTOs)
	{
		GameplayModifiers gameplayModifiers = new GameplayModifiers();
		List<GameplayModifiersDTO> list = new List<GameplayModifiersDTO>(gameplayModifiersDTOs);
		if (list.Contains(GameplayModifiersDTO.NoFail))
		{
			gameplayModifiers.noFail = true;
		}
		if (list.Contains(GameplayModifiersDTO.InstaFail))
		{
			gameplayModifiers.instaFail = true;
		}
		if (list.Contains(GameplayModifiersDTO.FailOnSaberClash))
		{
			gameplayModifiers.failOnSaberClash = true;
		}
		if (list.Contains(GameplayModifiersDTO.FastNotes))
		{
			gameplayModifiers.fastNotes = true;
		}
		if (list.Contains(GameplayModifiersDTO.DisappearingArrows))
		{
			gameplayModifiers.disappearingArrows = true;
		}
		if (list.Contains(GameplayModifiersDTO.SongSpeedFaster))
		{
			gameplayModifiers.songSpeed = GameplayModifiers.SongSpeed.Faster;
		}
		else if (list.Contains(GameplayModifiersDTO.SongSpeedSlower))
		{
			gameplayModifiers.songSpeed = GameplayModifiers.SongSpeed.Slower;
		}
		if (list.Contains(GameplayModifiersDTO.EnabledObstacleTypeFullHeightOnly))
		{
			gameplayModifiers.enabledObstacleType = GameplayModifiers.EnabledObstacleType.FullHeightOnly;
		}
		else if (list.Contains(GameplayModifiersDTO.EnabledObstacleTypeNoObstacles))
		{
			gameplayModifiers.enabledObstacleType = GameplayModifiers.EnabledObstacleType.NoObstacles;
		}
		if (list.Contains(GameplayModifiersDTO.EnergyTypeBattery))
		{
			gameplayModifiers.energyType = GameplayModifiers.EnergyType.Battery;
		}
		if (list.Contains(GameplayModifiersDTO.StrictAngles))
		{
			gameplayModifiers.strictAngles = true;
		}
		if (list.Contains(GameplayModifiersDTO.NoArrows))
		{
			gameplayModifiers.noArrows = true;
		}
		if (list.Contains(GameplayModifiersDTO.GhostNotes))
		{
			gameplayModifiers.ghostNotes = true;
		}
		return gameplayModifiers;
	}
}
