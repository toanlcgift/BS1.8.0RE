using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x020002B9 RID: 697
public class MissionLevelGameplayManager : MonoBehaviour, ILevelEndActions
{
	// Token: 0x1400004C RID: 76
	// (add) Token: 0x06000BC9 RID: 3017 RVA: 0x0003550C File Offset: 0x0003370C
	// (remove) Token: 0x06000BCA RID: 3018 RVA: 0x00035544 File Offset: 0x00033744
	public event Action levelFailedEvent;

	// Token: 0x1400004D RID: 77
	// (add) Token: 0x06000BCB RID: 3019 RVA: 0x0003557C File Offset: 0x0003377C
	// (remove) Token: 0x06000BCC RID: 3020 RVA: 0x000355B4 File Offset: 0x000337B4
	public event Action levelFinishedEvent;

	// Token: 0x06000BCD RID: 3021 RVA: 0x000094D6 File Offset: 0x000076D6
	protected void Awake()
	{
		this._gameState = MissionLevelGameplayManager.GameState.Intro;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000094DF File Offset: 0x000076DF
	private IEnumerator Start()
	{
		this._gameSongController.songDidFinishEvent += this.HandleSongDidFinish;
		this._gameEnergyCounter.gameEnergyDidReach0Event += this.HandleGameEnergyDidReach0;
		this._missionObjectiveCheckersManager.objectiveDidFailEvent += this.HandleMissionObjectiveCheckersManagerObjectiveDidFail;
		this._pauseController.canPauseEvent += this.HandlePauseControllerCanPause;
		this._pauseController.didPauseEvent += this.HandlePauseControllerDidPause;
		this._pauseController.didResumeEvent += this.HandlePauseControllerDidResume;
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		yield return null;
		this._gameState = MissionLevelGameplayManager.GameState.Playing;
		this._gameSongController.StartSong();
		yield break;
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x000355EC File Offset: 0x000337EC
	protected void OnDestroy()
	{
		if (this._gameSongController != null)
		{
			this._gameSongController.songDidFinishEvent -= this.HandleSongDidFinish;
		}
		if (this._gameEnergyCounter != null)
		{
			this._gameEnergyCounter.gameEnergyDidReach0Event -= this.HandleGameEnergyDidReach0;
		}
		if (this._missionObjectiveCheckersManager != null)
		{
			this._missionObjectiveCheckersManager.objectiveDidFailEvent -= this.HandleMissionObjectiveCheckersManagerObjectiveDidFail;
		}
		if (this._pauseController != null)
		{
			this._pauseController.canPauseEvent -= this.HandlePauseControllerCanPause;
			this._pauseController.didPauseEvent -= this.HandlePauseControllerDidPause;
			this._pauseController.didResumeEvent -= this.HandlePauseControllerDidResume;
		}
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000094EE File Offset: 0x000076EE
	private void HandleGameEnergyDidReach0()
	{
		if (this._gameState == MissionLevelGameplayManager.GameState.Failed || this._gameState == MissionLevelGameplayManager.GameState.Finished)
		{
			return;
		}
		this._gameState = MissionLevelGameplayManager.GameState.Failed;
		Action action = this.levelFailedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x000094EE File Offset: 0x000076EE
	private void HandleMissionObjectiveCheckersManagerObjectiveDidFail()
	{
		if (this._gameState == MissionLevelGameplayManager.GameState.Failed || this._gameState == MissionLevelGameplayManager.GameState.Finished)
		{
			return;
		}
		this._gameState = MissionLevelGameplayManager.GameState.Failed;
		Action action = this.levelFailedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0000951A File Offset: 0x0000771A
	private void HandleSongDidFinish()
	{
		if (this._gameState == MissionLevelGameplayManager.GameState.Failed || this._gameState == MissionLevelGameplayManager.GameState.Finished)
		{
			return;
		}
		this._gameState = MissionLevelGameplayManager.GameState.Finished;
		Action action = this.levelFinishedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00009546 File Offset: 0x00007746
	private void HandlePauseControllerCanPause(Action<bool> canPause)
	{
		if (canPause != null)
		{
			canPause(this._gameState == MissionLevelGameplayManager.GameState.Playing);
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0000955A File Offset: 0x0000775A
	private void HandlePauseControllerDidPause()
	{
		if (this._gameState == MissionLevelGameplayManager.GameState.Playing)
		{
			this._gameState = MissionLevelGameplayManager.GameState.Paused;
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0000956C File Offset: 0x0000776C
	private void HandlePauseControllerDidResume()
	{
		if (this._gameState == MissionLevelGameplayManager.GameState.Paused)
		{
			this._gameState = MissionLevelGameplayManager.GameState.Playing;
		}
	}

	// Token: 0x04000C7A RID: 3194
	[SerializeField]
	private MissionObjectiveCheckersManager _missionObjectiveCheckersManager;

	// Token: 0x04000C7B RID: 3195
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000C7C RID: 3196
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000C7D RID: 3197
	[Inject]
	private GameEnergyCounter _gameEnergyCounter;

	// Token: 0x04000C7E RID: 3198
	[Inject]
	private PauseController _pauseController;

	// Token: 0x04000C81 RID: 3201
	private MissionLevelGameplayManager.GameState _gameState;

	// Token: 0x020002BA RID: 698
	public enum GameState
	{
		// Token: 0x04000C83 RID: 3203
		Intro,
		// Token: 0x04000C84 RID: 3204
		Playing,
		// Token: 0x04000C85 RID: 3205
		Paused,
		// Token: 0x04000C86 RID: 3206
		Finished,
		// Token: 0x04000C87 RID: 3207
		Failed
	}
}
