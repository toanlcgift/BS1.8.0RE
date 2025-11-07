using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x02000286 RID: 646
public class FogSwitchEventEffect : MonoBehaviour
{
	// Token: 0x06000AD7 RID: 2775 RVA: 0x00008716 File Offset: 0x00006916
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0000872F File Offset: 0x0000692F
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x000328EC File Offset: 0x00030AEC
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._event)
		{
			if (beatmapEventData.value == 0)
			{
				this.AnimateToFog(2f, 0f);
				return;
			}
			if (beatmapEventData.value > 0 || beatmapEventData.value == -1)
			{
				this.AnimateToFog(2f, 1f);
			}
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00008755 File Offset: 0x00006955
	private void AnimateToFog(float duration, float value)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.AnimateToFogCoroutine(duration, value));
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0000876C File Offset: 0x0000696C
	private IEnumerator AnimateToFogCoroutine(float duration, float value)
	{
		float startTransition = this._bloomFog.transition;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			this._bloomFog.transition = Mathf.Lerp(startTransition, value, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this._bloomFog.transition = value;
		yield break;
	}

	// Token: 0x04000B50 RID: 2896
	[SerializeField]
	private BloomFogSO _bloomFog;

	// Token: 0x04000B51 RID: 2897
	[SerializeField]
	private BeatmapEventType _event;

	// Token: 0x04000B52 RID: 2898
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000B53 RID: 2899
	private const float kTransitionDuration = 2f;
}
