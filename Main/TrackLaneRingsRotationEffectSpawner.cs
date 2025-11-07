using System;
using UnityEngine;
using Zenject;

// Token: 0x020002A5 RID: 677
public class TrackLaneRingsRotationEffectSpawner : MonoBehaviour
{
	// Token: 0x06000B70 RID: 2928 RVA: 0x00008FF5 File Offset: 0x000071F5
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0000900E File Offset: 0x0000720E
	protected void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000344F4 File Offset: 0x000326F4
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type != this._beatmapEventType)
		{
			return;
		}
		float step = 0f;
		switch (this._rotationStepType)
		{
		case TrackLaneRingsRotationEffectSpawner.RotationStepType.Range0ToMax:
			step = UnityEngine.Random.Range(0f, this._rotationStep);
			break;
		case TrackLaneRingsRotationEffectSpawner.RotationStepType.Range:
			step = UnityEngine.Random.Range(-this._rotationStep, this._rotationStep);
			break;
		case TrackLaneRingsRotationEffectSpawner.RotationStepType.MaxOr0:
			step = ((UnityEngine.Random.value < 0.5f) ? this._rotationStep : 0f);
			break;
		}
		this._trackLaneRingsRotationEffect.AddRingRotationEffect(this._trackLaneRingsRotationEffect.GetFirstRingDestinationRotationAngle() + this._rotation * (float)((UnityEngine.Random.value < 0.5f) ? 1 : -1), step, this._rotationPropagationSpeed, this._rotationFlexySpeed);
	}

	// Token: 0x04000C14 RID: 3092
	[SerializeField]
	private TrackLaneRingsRotationEffect _trackLaneRingsRotationEffect;

	// Token: 0x04000C15 RID: 3093
	[Space]
	[SerializeField]
	private BeatmapEventType _beatmapEventType;

	// Token: 0x04000C16 RID: 3094
	[Space]
	[SerializeField]
	private float _rotation = 90f;

	// Token: 0x04000C17 RID: 3095
	[SerializeField]
	private float _rotationStep = 5f;

	// Token: 0x04000C18 RID: 3096
	[SerializeField]
	private TrackLaneRingsRotationEffectSpawner.RotationStepType _rotationStepType = TrackLaneRingsRotationEffectSpawner.RotationStepType.Range;

	// Token: 0x04000C19 RID: 3097
	[SerializeField]
	private int _rotationPropagationSpeed = 1;

	// Token: 0x04000C1A RID: 3098
	[SerializeField]
	private float _rotationFlexySpeed = 1f;

	// Token: 0x04000C1B RID: 3099
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x020002A6 RID: 678
	public enum RotationStepType
	{
		// Token: 0x04000C1D RID: 3101
		Range0ToMax,
		// Token: 0x04000C1E RID: 3102
		Range,
		// Token: 0x04000C1F RID: 3103
		MaxOr0
	}
}
