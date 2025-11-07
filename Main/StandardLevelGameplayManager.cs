using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x020002BC RID: 700
public class StandardLevelGameplayManager : MonoBehaviour, ILevelEndActions
{
	// Token: 0x1400004E RID: 78
	// (add) Token: 0x06000BDD RID: 3037 RVA: 0x000357C8 File Offset: 0x000339C8
	// (remove) Token: 0x06000BDE RID: 3038 RVA: 0x00035800 File Offset: 0x00033A00
	public event Action levelFailedEvent;

	// Token: 0x1400004F RID: 79
	// (add) Token: 0x06000BDF RID: 3039 RVA: 0x00035838 File Offset: 0x00033A38
	// (remove) Token: 0x06000BE0 RID: 3040 RVA: 0x00035870 File Offset: 0x00033A70
	public event Action levelFinishedEvent;

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00009595 File Offset: 0x00007795
	protected void Awake()
	{
		this._gameState = StandardLevelGameplayManager.GameState.Intro;
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x0000959E File Offset: 0x0000779E
	private IEnumerator Start()
	{
		this._gameSongController.songDidFinishEvent += this.HandleSongDidFinish;
		this._gameEnergyCounter.gameEnergyDidReach0Event += this.HandleGameEnergyDidReach0;
		this._pauseController.canPauseEvent += this.HandlePauseControllerCanPause;
		this._pauseController.didPauseEvent += this.HandlePauseControllerDidPause;
		this._pauseController.didResumeEvent += this.HandlePauseControllerDidResume;
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		yield return null;
		this._gameState = StandardLevelGameplayManager.GameState.Playing;
		this._gameSongController.StartSong();
		yield break;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000358A8 File Offset: 0x00033AA8
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
		if (this._pauseController != null)
		{
			this._pauseController.canPauseEvent -= this.HandlePauseControllerCanPause;
			this._pauseController.didPauseEvent -= this.HandlePauseControllerDidPause;
			this._pauseController.didResumeEvent -= this.HandlePauseControllerDidResume;
		}
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000095AD File Offset: 0x000077AD
	private void HandleGameEnergyDidReach0()
	{
		if (this._gameState == StandardLevelGameplayManager.GameState.Failed || this._gameState == StandardLevelGameplayManager.GameState.Finished)
		{
			return;
		}
		this._gameState = StandardLevelGameplayManager.GameState.Failed;
		Action action = this.levelFailedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000095D9 File Offset: 0x000077D9
	private void HandleSongDidFinish()
	{
		if (this._gameState == StandardLevelGameplayManager.GameState.Failed || this._gameState == StandardLevelGameplayManager.GameState.Finished)
		{
			return;
		}
		this._gameState = StandardLevelGameplayManager.GameState.Finished;
		Action action = this.levelFinishedEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00009605 File Offset: 0x00007805
	private void HandlePauseControllerCanPause(Action<bool> canPause)
	{
		if (canPause != null)
		{
			canPause(this._gameState == StandardLevelGameplayManager.GameState.Playing);
		}
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00009619 File Offset: 0x00007819
	private void HandlePauseControllerDidPause()
	{
		if (this._gameState == StandardLevelGameplayManager.GameState.Playing)
		{
			this._gameState = StandardLevelGameplayManager.GameState.Paused;
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0000962B File Offset: 0x0000782B
	private void HandlePauseControllerDidResume()
	{
		if (this._gameState == StandardLevelGameplayManager.GameState.Paused)
		{
			this._gameState = StandardLevelGameplayManager.GameState.Playing;
		}
	}

	// Token: 0x04000C8B RID: 3211
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000C8C RID: 3212
	[Inject]
	private GameSongController _gameSongController;

	// Token: 0x04000C8D RID: 3213
	[Inject]
	private GameEnergyCounter _gameEnergyCounter;

	// Token: 0x04000C8E RID: 3214
	[Inject]
	private PauseController _pauseController;

	// Token: 0x04000C91 RID: 3217
	private StandardLevelGameplayManager.GameState _gameState;

	// Token: 0x020002BD RID: 701
	public enum GameState
	{
		// Token: 0x04000C93 RID: 3219
		Intro,
		// Token: 0x04000C94 RID: 3220
		Playing,
		// Token: 0x04000C95 RID: 3221
		Paused,
		// Token: 0x04000C96 RID: 3222
		Finished,
		// Token: 0x04000C97 RID: 3223
		Failed
	}
}
