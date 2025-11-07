using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x0200031F RID: 799
public class TutorialController : MonoBehaviour
{
	// Token: 0x06000DF3 RID: 3571 RVA: 0x00039F00 File Offset: 0x00038100
	protected void Start()
	{
		this._introTutorialController.introTutorialDidFinishEvent += this.HandleIntroTutorialDidFinishEvent;
		this._tutorialSongController.songDidFinishEvent += this.HandleTutorialSongControllerSongDidFinishEvent;
		this._pauseController.canPauseEvent += this.HandlePauseControllerCanPause;
		this._pauseController.didPauseEvent += this.HandlePauseControllerDidPause;
		this._pauseController.didResumeEvent += this.HandlePauseControllerDidResume;
		this._tutorialIntroStartedSignal.Raise();
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00039F8C File Offset: 0x0003818C
	protected void OnDestroy()
	{
		if (this._introTutorialController != null)
		{
			this._introTutorialController.introTutorialDidFinishEvent -= this.HandleIntroTutorialDidFinishEvent;
		}
		if (this._tutorialSongController != null)
		{
			this._tutorialSongController.songDidFinishEvent -= this.HandleTutorialSongControllerSongDidFinishEvent;
		}
		if (this._pauseController != null)
		{
			this._pauseController.canPauseEvent -= this.HandlePauseControllerCanPause;
			this._pauseController.didPauseEvent -= this.HandlePauseControllerDidPause;
			this._pauseController.didResumeEvent -= this.HandlePauseControllerDidResume;
		}
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0000AC7F File Offset: 0x00008E7F
	private void HandleIntroTutorialDidFinishEvent()
	{
		this._tutorialSongController.StartSong();
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0000AC8C File Offset: 0x00008E8C
	private void HandleTutorialSongControllerSongDidFinishEvent()
	{
		this._doingOutroTransition = true;
		this._audioFading.FadeOut();
		base.StartCoroutine(this.OutroCoroutine());
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0000ACAD File Offset: 0x00008EAD
	private IEnumerator OutroCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		this._tutorialFinishedSignal.Raise();
		yield return new WaitForSeconds(2.7f);
		this._tutorialSceneSetupData.Finish(TutorialScenesTransitionSetupDataSO.TutorialEndStateType.Completed);
		yield break;
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0000ACBC File Offset: 0x00008EBC
	private void HandlePauseControllerCanPause(Action<bool> canPause)
	{
		if (canPause != null)
		{
			canPause(!this._paused && !this._doingOutroTransition);
		}
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0000ACDB File Offset: 0x00008EDB
	private void HandlePauseControllerDidPause()
	{
		this._paused = true;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0000ACE4 File Offset: 0x00008EE4
	private void HandlePauseControllerDidResume()
	{
		this._paused = false;
	}

	// Token: 0x04000E4E RID: 3662
	[SerializeField]
	private TutorialSongController _tutorialSongController;

	// Token: 0x04000E4F RID: 3663
	[SerializeField]
	private IntroTutorialController _introTutorialController;

	// Token: 0x04000E50 RID: 3664
	[SerializeField]
	private AudioFading _audioFading;

	// Token: 0x04000E51 RID: 3665
	[SerializeField]
	private TutorialScenesTransitionSetupDataSO _tutorialSceneSetupData;

	// Token: 0x04000E52 RID: 3666
	[Space]
	[SerializeField]
	[SignalSender]
	private Signal _tutorialIntroStartedSignal;

	// Token: 0x04000E53 RID: 3667
	[SerializeField]
	[SignalSender]
	private Signal _tutorialFinishedSignal;

	// Token: 0x04000E54 RID: 3668
	[Inject]
	private PauseController _pauseController;

	// Token: 0x04000E55 RID: 3669
	private bool _paused;

	// Token: 0x04000E56 RID: 3670
	private bool _doingOutroTransition;
}
