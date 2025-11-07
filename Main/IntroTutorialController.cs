using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x0200031A RID: 794
public class IntroTutorialController : MonoBehaviour
{
	// Token: 0x14000074 RID: 116
	// (add) Token: 0x06000DCF RID: 3535 RVA: 0x00039988 File Offset: 0x00037B88
	// (remove) Token: 0x06000DD0 RID: 3536 RVA: 0x000399C0 File Offset: 0x00037BC0
	public event Action introTutorialDidFinishEvent;

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0000AA6C File Offset: 0x00008C6C
	protected void Start()
	{
		this._gamePause.didPauseEvent += this.HandleGameDidPause;
		this._gamePause.didResumeEvent += this.HandlegameDidResume;
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0000AA9C File Offset: 0x00008C9C
	protected void OnDestroy()
	{
		this.CleanUp();
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x0000AAA4 File Offset: 0x00008CA4
	protected void Update()
	{
		if (this._redRing.enabled && this._redRing.fullyActivated && this._blueRing.enabled && this._blueRing.fullyActivated)
		{
			this.ShowFinishAnimation();
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0000AAE0 File Offset: 0x00008CE0
	private void CleanUp()
	{
		if (this._gamePause != null)
		{
			this._gamePause.didPauseEvent -= this.HandleGameDidPause;
			this._gamePause.didResumeEvent -= this.HandlegameDidResume;
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0000AB18 File Offset: 0x00008D18
	private void HandleGameDidPause()
	{
		this._redRingWrapperActive = this._redRingWrapper.activeSelf;
		this._blueRingWrapperActive = this._redRingWrapper.activeSelf;
		this._redRingWrapper.SetActive(false);
		this._blueRingWrapper.SetActive(false);
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0000AB54 File Offset: 0x00008D54
	private void HandlegameDidResume()
	{
		this._redRingWrapper.SetActive(this._redRingWrapperActive);
		this._blueRingWrapper.SetActive(this._blueRingWrapperActive);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x000399F8 File Offset: 0x00037BF8
	private void ShowFinishAnimation()
	{
		if (this._showingFinishAnimation)
		{
			return;
		}
		this._showingFinishAnimation = true;
		this._redRing.enabled = false;
		this._blueRing.enabled = false;
		this._shockWavePS.Emit(1);
		base.StartCoroutine(this.ShowFinishAnimationCoroutine());
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0000AB78 File Offset: 0x00008D78
	private IEnumerator ShowFinishAnimationCoroutine()
	{
		float elapsedTime = 0f;
		float duration = 0.2f;
		while (elapsedTime < duration)
		{
			float finishAnimationParams = elapsedTime / duration;
			this.SetFinishAnimationParams(finishAnimationParams);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.SetFinishAnimationParams(1f);
		this._redRingWrapper.SetActive(false);
		this._blueRingWrapper.SetActive(false);
		this._textCanvasGroup.gameObject.SetActive(false);
		Action action = this.introTutorialDidFinishEvent;
		if (action != null)
		{
			action();
		}
		this.CleanUp();
		yield break;
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00039A48 File Offset: 0x00037C48
	private void SetFinishAnimationParams(float progress)
	{
		this._bloomFog.transition = progress;
		this._textCanvasGroup.alpha = 1f - progress;
		this._redRing.alpha = 1f - progress;
		this._blueRing.alpha = 1f - progress;
	}

	// Token: 0x04000E27 RID: 3623
	[SerializeField]
	private BloomFogSO _bloomFog;

	// Token: 0x04000E28 RID: 3624
	[SerializeField]
	private IntroTutorialRing _redRing;

	// Token: 0x04000E29 RID: 3625
	[SerializeField]
	private IntroTutorialRing _blueRing;

	// Token: 0x04000E2A RID: 3626
	[SerializeField]
	private GameObject _redRingWrapper;

	// Token: 0x04000E2B RID: 3627
	[SerializeField]
	private GameObject _blueRingWrapper;

	// Token: 0x04000E2C RID: 3628
	[SerializeField]
	private CanvasGroup _textCanvasGroup;

	// Token: 0x04000E2D RID: 3629
	[SerializeField]
	private ParticleSystem _shockWavePS;

	// Token: 0x04000E2E RID: 3630
	[Inject]
	private IGamePause _gamePause;

	// Token: 0x04000E30 RID: 3632
	private bool _showingFinishAnimation;

	// Token: 0x04000E31 RID: 3633
	private bool _redRingWrapperActive;

	// Token: 0x04000E32 RID: 3634
	private bool _blueRingWrapperActive;
}
