using System;
using UnityEngine;
using Zenject;

// Token: 0x020002A2 RID: 674
public class TrackLaneRingsPositionStepEffectSpawner : MonoBehaviour
{
	// Token: 0x06000B62 RID: 2914 RVA: 0x00008F07 File Offset: 0x00007107
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00008F20 File Offset: 0x00007120
	protected void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000342E8 File Offset: 0x000324E8
	public void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type != this._beatmapEventType)
		{
			return;
		}
		float num = this._prevWasMinStep ? this._maxPositionStep : this._minPositionStep;
		this._prevWasMinStep = !this._prevWasMinStep;
		TrackLaneRing[] rings = this._trackLaneRingsManager.Rings;
		for (int i = 0; i < rings.Length; i++)
		{
			float destPosZ = (float)i * num;
			rings[i].SetPosition(destPosZ, this._moveSpeed);
		}
	}

	// Token: 0x04000C00 RID: 3072
	[SerializeField]
	private TrackLaneRingsManager _trackLaneRingsManager;

	// Token: 0x04000C01 RID: 3073
	[Space]
	[SerializeField]
	private BeatmapEventType _beatmapEventType;

	// Token: 0x04000C02 RID: 3074
	[Space]
	[SerializeField]
	private float _minPositionStep = 1f;

	// Token: 0x04000C03 RID: 3075
	[SerializeField]
	private float _maxPositionStep = 2f;

	// Token: 0x04000C04 RID: 3076
	[SerializeField]
	private float _moveSpeed = 1f;

	// Token: 0x04000C05 RID: 3077
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000C06 RID: 3078
	private bool _prevWasMinStep;
}
