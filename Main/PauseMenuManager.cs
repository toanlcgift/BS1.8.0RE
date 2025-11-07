using System;
using HMUI;
using Polyglot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

// Token: 0x020002EE RID: 750
public class PauseMenuManager : MonoBehaviour
{
	// Token: 0x1400005F RID: 95
	// (add) Token: 0x06000CCF RID: 3279 RVA: 0x0003736C File Offset: 0x0003556C
	// (remove) Token: 0x06000CD0 RID: 3280 RVA: 0x000373A4 File Offset: 0x000355A4
	public event Action didPressContinueButtonEvent;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x06000CD1 RID: 3281 RVA: 0x000373DC File Offset: 0x000355DC
	// (remove) Token: 0x06000CD2 RID: 3282 RVA: 0x00037414 File Offset: 0x00035614
	public event Action didPressMenuButtonEvent;

	// Token: 0x14000061 RID: 97
	// (add) Token: 0x06000CD3 RID: 3283 RVA: 0x0003744C File Offset: 0x0003564C
	// (remove) Token: 0x06000CD4 RID: 3284 RVA: 0x00037484 File Offset: 0x00035684
	public event Action didPressRestartButtonEvent;

	// Token: 0x14000062 RID: 98
	// (add) Token: 0x06000CD5 RID: 3285 RVA: 0x000374BC File Offset: 0x000356BC
	// (remove) Token: 0x06000CD6 RID: 3286 RVA: 0x000374F4 File Offset: 0x000356F4
	public event Action didFinishResumeAnimationEvent;

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0003752C File Offset: 0x0003572C
	protected void Awake()
	{
		this._pauseAnimationController.resumeFromPauseAnimationDidFinishEvent += this.HandleResumeFromPauseAnimationDidFinish;
		base.enabled = false;
		this._buttonBinder = new ButtonBinder();
		this._buttonBinder.AddBinding(this._continueButton, new Action(this.ContinueButtonPressed));
		this._buttonBinder.AddBinding(this._restartButton, new Action(this.RestartButtonPressed));
		this._buttonBinder.AddBinding(this._backButton, new Action(this.MenuButtonPressed));
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x000375BC File Offset: 0x000357BC
	protected void Start()
	{
		this._levelNameText.text = string.Format("{0}\n<size=80%>{1}</size>", this._initData.songName, this._initData.songSubName);
		if (this._initData.difficultyName != null && this._initData.difficultyName != "")
		{
			this._beatmapDifficultyText.text = Localization.Get("STATS_DIFFICULTY") + " - " + this._initData.difficultyName;
		}
		else
		{
			this._beatmapDifficultyText.text = "";
		}
		this._backButtonText.text = this._initData.backButtonText;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00009EE2 File Offset: 0x000080E2
	protected void OnDestroy()
	{
		if (this._pauseAnimationController != null)
		{
			this._pauseAnimationController.resumeFromPauseAnimationDidFinishEvent -= this.HandleResumeFromPauseAnimationDidFinish;
		}
		ButtonBinder buttonBinder = this._buttonBinder;
		if (buttonBinder == null)
		{
			return;
		}
		buttonBinder.ClearBindings();
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00009F19 File Offset: 0x00008119
	protected void Update()
	{
		if (this._ignoreFirstFrameVRControllerInteraction)
		{
			this._ignoreFirstFrameVRControllerInteraction = false;
			return;
		}
		if (this._vrControllersInputManager.MenuButtonDown())
		{
			this.ContinueButtonPressed();
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00009F3E File Offset: 0x0000813E
	public void ShowMenu()
	{
		base.enabled = true;
		this._ignoreFirstFrameVRControllerInteraction = true;
		this._pauseAnimationController.StartEnterPauseAnimation();
		this._pauseContainerTransform.eulerAngles = new Vector3(0f, this._environmentSpawnRotation.targetRotation, 0f);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00009F7E File Offset: 0x0000817E
	public void StartResumeAnimation()
	{
		base.enabled = false;
		this._pauseAnimationController.StartResumeFromPauseAnimation();
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x00009F92 File Offset: 0x00008192
	private void HandleResumeFromPauseAnimationDidFinish()
	{
		Action action = this.didFinishResumeAnimationEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x00009FA4 File Offset: 0x000081A4
	private void MenuButtonPressed()
	{
		base.enabled = false;
		Action action = this.didPressMenuButtonEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x00009FBD File Offset: 0x000081BD
	private void RestartButtonPressed()
	{
		base.enabled = false;
		Action action = this.didPressRestartButtonEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x00009FD6 File Offset: 0x000081D6
	private void ContinueButtonPressed()
	{
		base.enabled = false;
		Action action = this.didPressContinueButtonEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000D39 RID: 3385
	[SerializeField]
	private PauseAnimationController _pauseAnimationController;

	// Token: 0x04000D3A RID: 3386
	[SerializeField]
	private TextMeshProUGUI _levelNameText;

	// Token: 0x04000D3B RID: 3387
	[SerializeField]
	private TextMeshProUGUI _beatmapDifficultyText;

	// Token: 0x04000D3C RID: 3388
	[SerializeField]
	private Button _continueButton;

	// Token: 0x04000D3D RID: 3389
	[SerializeField]
	private Button _restartButton;

	// Token: 0x04000D3E RID: 3390
	[SerializeField]
	private Button _backButton;

	// Token: 0x04000D3F RID: 3391
	[SerializeField]
	private TextMeshProUGUI _backButtonText;

	// Token: 0x04000D40 RID: 3392
	[SerializeField]
	private Transform _pauseContainerTransform;

	// Token: 0x04000D41 RID: 3393
	[Inject]
	private PauseMenuManager.InitData _initData;

	// Token: 0x04000D42 RID: 3394
	[Inject]
	private VRControllersInputManager _vrControllersInputManager;

	// Token: 0x04000D43 RID: 3395
	[Inject]
	private EnvironmentSpawnRotation _environmentSpawnRotation;

	// Token: 0x04000D48 RID: 3400
	private ButtonBinder _buttonBinder;

	// Token: 0x04000D49 RID: 3401
	private bool _ignoreFirstFrameVRControllerInteraction;

	// Token: 0x020002EF RID: 751
	public class InitData
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x00009FEF File Offset: 0x000081EF
		public InitData(string backButtonText, string songName, string songSubName, string difficultyName)
		{
			this.backButtonText = backButtonText;
			this.songName = songName;
			this.songSubName = songSubName;
			this.difficultyName = difficultyName;
		}

		// Token: 0x04000D4A RID: 3402
		public readonly string backButtonText;

		// Token: 0x04000D4B RID: 3403
		public readonly string songName;

		// Token: 0x04000D4C RID: 3404
		public readonly string songSubName;

		// Token: 0x04000D4D RID: 3405
		public readonly string difficultyName;
	}
}
