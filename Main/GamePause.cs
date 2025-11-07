using System;
using UnityEngine;
using Zenject;

// Token: 0x020002E9 RID: 745
public class GamePause : MonoBehaviour, IGamePause
{
	// Token: 0x14000057 RID: 87
	// (add) Token: 0x06000CA5 RID: 3237 RVA: 0x00036DC4 File Offset: 0x00034FC4
	// (remove) Token: 0x06000CA6 RID: 3238 RVA: 0x00036DFC File Offset: 0x00034FFC
	public event Action didPauseEvent;

	// Token: 0x14000058 RID: 88
	// (add) Token: 0x06000CA7 RID: 3239 RVA: 0x00036E34 File Offset: 0x00035034
	// (remove) Token: 0x06000CA8 RID: 3240 RVA: 0x00036E6C File Offset: 0x0003506C
	public event Action didResumeEvent;

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00036EA4 File Offset: 0x000350A4
	public void Pause()
	{
		if (this._pause)
		{
			return;
		}
		this._pause = true;
		this._playerController.disableSabers = true;
		this._gameEnergyCounter.enabled = false;
		this._scoreController.enabled = false;
		this._beatmapObjectExecutionRatingsRecorder.enabled = false;
		this._noteCutSoundEffectManager.Pause();
		this._songController.PauseSong();
		Action action = this.didPauseEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00036F18 File Offset: 0x00035118
	public void Resume()
	{
		if (!this._pause)
		{
			return;
		}
		this._pause = false;
		this._playerController.disableSabers = false;
		this._gameEnergyCounter.enabled = true;
		this._scoreController.enabled = true;
		this._beatmapObjectExecutionRatingsRecorder.enabled = true;
		this._noteCutSoundEffectManager.Resume();
		this._songController.ResumeSong();
		Action action = this.didResumeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000D24 RID: 3364
	[Inject]
	private GameEnergyCounter _gameEnergyCounter;

	// Token: 0x04000D25 RID: 3365
	[Inject]
	private ScoreController _scoreController;

	// Token: 0x04000D26 RID: 3366
	[Inject]
	private BeatmapObjectExecutionRatingsRecorder _beatmapObjectExecutionRatingsRecorder;

	// Token: 0x04000D27 RID: 3367
	[Inject]
	private SongController _songController;

	// Token: 0x04000D28 RID: 3368
	[Inject]
	private NoteCutSoundEffectManager _noteCutSoundEffectManager;

	// Token: 0x04000D29 RID: 3369
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000D2A RID: 3370
	private bool _pause;
}
