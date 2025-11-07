using System;
using UnityEngine;
using Zenject;

// Token: 0x020002EC RID: 748
public class PauseController : MonoBehaviour
{
	// Token: 0x1400005C RID: 92
	// (add) Token: 0x06000CBA RID: 3258 RVA: 0x00036FFC File Offset: 0x000351FC
	// (remove) Token: 0x06000CBB RID: 3259 RVA: 0x00037034 File Offset: 0x00035234
	public event Action didPauseEvent;

	// Token: 0x1400005D RID: 93
	// (add) Token: 0x06000CBC RID: 3260 RVA: 0x0003706C File Offset: 0x0003526C
	// (remove) Token: 0x06000CBD RID: 3261 RVA: 0x000370A4 File Offset: 0x000352A4
	public event Action didResumeEvent;

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x06000CBE RID: 3262 RVA: 0x000370DC File Offset: 0x000352DC
	// (remove) Token: 0x06000CBF RID: 3263 RVA: 0x00037114 File Offset: 0x00035314
	public event Action<Action<bool>> canPauseEvent;

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0003714C File Offset: 0x0003534C
	private bool canPause
	{
		get
		{
			bool value = true;
			Action<Action<bool>> action = this.canPauseEvent;
			if (action != null)
			{
				action(delegate(bool newValue)
				{
					value = (value && newValue);
				});
			}
			return value && !this._paused;
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00037198 File Offset: 0x00035398
	private void Start()
	{
		this._vrPlatformHelper.inputFocusWasCapturedEvent += this.HandleInputFocusWasCaptured;
		this._vrPlatformHelper.hmdUnmountedEvent += this.HandleHMDUnmounted;
		this._pauseMenuManager.didFinishResumeAnimationEvent += this.HandlePauseMenuManagerDidFinishResumeAnimation;
		this._pauseMenuManager.didPressContinueButtonEvent += this.HandlePauseMenuManagerDidPressContinueButton;
		this._pauseMenuManager.didPressRestartButtonEvent += this.HandlePauseMenuManagerDidPressRestartButton;
		this._pauseMenuManager.didPressMenuButtonEvent += this.HandlePauseMenuManagerDidPressMenuButton;
		this._pauseTrigger.pauseTriggeredEvent += this.HandlePauseTriggered;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00037248 File Offset: 0x00035448
	protected void OnDestroy()
	{
		if (this._vrPlatformHelper != null)
		{
			this._vrPlatformHelper.inputFocusWasCapturedEvent -= this.HandleInputFocusWasCaptured;
			this._vrPlatformHelper.hmdUnmountedEvent -= this.HandleHMDUnmounted;
		}
		if (this._pauseTrigger != null)
		{
			this._pauseTrigger.pauseTriggeredEvent -= this.HandlePauseTriggered;
		}
		if (this._pauseMenuManager != null)
		{
			this._pauseMenuManager.didFinishResumeAnimationEvent -= this.HandlePauseMenuManagerDidFinishResumeAnimation;
			this._pauseMenuManager.didPressContinueButtonEvent -= this.HandlePauseMenuManagerDidPressContinueButton;
			this._pauseMenuManager.didPressRestartButtonEvent -= this.HandlePauseMenuManagerDidPressRestartButton;
			this._pauseMenuManager.didPressMenuButtonEvent -= this.HandlePauseMenuManagerDidPressMenuButton;
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00009E68 File Offset: 0x00008068
	protected void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			this.Pause();
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0003731C File Offset: 0x0003551C
	public void Pause()
	{
		if (!this.canPause)
		{
			return;
		}
		this._paused = true;
		this._gamePause.Pause();
		this._pauseMenuManager.ShowMenu();
		this._beatmapObjectManager.HideAllBeatmapObjects(true);
		Action action = this.didPauseEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00009E73 File Offset: 0x00008073
	private void HandlePauseTriggered()
	{
		this.Pause();
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00009E73 File Offset: 0x00008073
	private void HandleInputFocusWasCaptured()
	{
		this.Pause();
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00009E73 File Offset: 0x00008073
	private void HandleHMDUnmounted()
	{
		this.Pause();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00009E7B File Offset: 0x0000807B
	private void HandlePauseMenuManagerDidFinishResumeAnimation()
	{
		this._paused = false;
		this._gamePause.Resume();
		Action action = this.didResumeEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00009E9F File Offset: 0x0000809F
	private void HandlePauseMenuManagerDidPressContinueButton()
	{
		this._beatmapObjectManager.HideAllBeatmapObjects(false);
		this._pauseMenuManager.StartResumeAnimation();
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00009EB8 File Offset: 0x000080B8
	private void HandlePauseMenuManagerDidPressRestartButton()
	{
		this._levelRestartController.RestartLevel();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00009EC5 File Offset: 0x000080C5
	private void HandlePauseMenuManagerDidPressMenuButton()
	{
		this._returnToMenuController.ReturnToMenu();
	}

	// Token: 0x04000D2D RID: 3373
	[Inject]
	private PauseMenuManager _pauseMenuManager;

	// Token: 0x04000D2E RID: 3374
	[Inject]
	private IGamePause _gamePause;

	// Token: 0x04000D2F RID: 3375
	[Inject]
	private IPauseTrigger _pauseTrigger;

	// Token: 0x04000D30 RID: 3376
	[Inject]
	private BeatmapObjectManager _beatmapObjectManager;

	// Token: 0x04000D31 RID: 3377
	[Inject]
	private ILevelRestartController _levelRestartController;

	// Token: 0x04000D32 RID: 3378
	[Inject]
	private IReturnToMenuController _returnToMenuController;

	// Token: 0x04000D33 RID: 3379
	[Inject]
	private VRPlatformHelper _vrPlatformHelper;

	// Token: 0x04000D37 RID: 3383
	private bool _paused;
}
