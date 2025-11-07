using System;
using UnityEngine;
using Zenject;

// Token: 0x0200028B RID: 651
public class LightRotationEventEffect : MonoBehaviour
{
	// Token: 0x06000AEC RID: 2796 RVA: 0x000087EB File Offset: 0x000069EB
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		this._transform = base.transform;
		this._startRotation = this._transform.rotation;
		base.enabled = false;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00008828 File Offset: 0x00006A28
	protected void Update()
	{
		this._transform.Rotate(this._rotationVector, Time.deltaTime * this._rotationSpeed, Space.Self);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00008848 File Offset: 0x00006A48
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00032D88 File Offset: 0x00030F88
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._event)
		{
			if (beatmapEventData.value == 0)
			{
				base.enabled = false;
				this._transform.localRotation = this._startRotation;
				return;
			}
			if (beatmapEventData.value > 0)
			{
				this._transform.localRotation = this._startRotation;
				this._transform.Rotate(this._rotationVector, UnityEngine.Random.Range(0f, 180f), Space.Self);
				base.enabled = true;
				this._rotationSpeed = (float)beatmapEventData.value * 20f * ((UnityEngine.Random.value > 0.5f) ? 1f : -1f);
			}
		}
	}

	// Token: 0x04000B73 RID: 2931
	[SerializeField]
	private BeatmapEventType _event;

	// Token: 0x04000B74 RID: 2932
	[SerializeField]
	private Vector3 _rotationVector = Vector3.up;

	// Token: 0x04000B75 RID: 2933
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000B76 RID: 2934
	private const float kSpeedMultiplier = 20f;

	// Token: 0x04000B77 RID: 2935
	private Transform _transform;

	// Token: 0x04000B78 RID: 2936
	private Quaternion _startRotation;

	// Token: 0x04000B79 RID: 2937
	private float _rotationSpeed;
}
