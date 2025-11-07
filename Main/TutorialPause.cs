using System;
using UnityEngine;
using Zenject;

// Token: 0x02000322 RID: 802
public class TutorialPause : MonoBehaviour, IGamePause
{
	// Token: 0x14000075 RID: 117
	// (add) Token: 0x06000E0B RID: 3595 RVA: 0x0003A278 File Offset: 0x00038478
	// (remove) Token: 0x06000E0C RID: 3596 RVA: 0x0003A2B0 File Offset: 0x000384B0
	public event Action didPauseEvent;

	// Token: 0x14000076 RID: 118
	// (add) Token: 0x06000E0D RID: 3597 RVA: 0x0003A2E8 File Offset: 0x000384E8
	// (remove) Token: 0x06000E0E RID: 3598 RVA: 0x0003A320 File Offset: 0x00038520
	public event Action didResumeEvent;

	// Token: 0x06000E0F RID: 3599 RVA: 0x0000AD9E File Offset: 0x00008F9E
	public void Pause()
	{
		if (this._pause)
		{
			return;
		}
		this._pause = true;
		this._playerController.disableSabers = true;
		this._tutorialSongController.PauseSong();
		Action action = this.didPauseEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0000ADD7 File Offset: 0x00008FD7
	public void Resume()
	{
		if (!this._pause)
		{
			return;
		}
		this._pause = false;
		this._playerController.disableSabers = false;
		this._tutorialSongController.ResumeSong();
		Action action = this.didResumeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000E65 RID: 3685
	[Inject]
	private TutorialSongController _tutorialSongController;

	// Token: 0x04000E66 RID: 3686
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000E67 RID: 3687
	private bool _pause;
}
