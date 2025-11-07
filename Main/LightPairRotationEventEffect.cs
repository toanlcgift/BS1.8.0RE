using System;
using UnityEngine;
using Zenject;

// Token: 0x02000289 RID: 649
public class LightPairRotationEventEffect : MonoBehaviour
{
	// Token: 0x06000AE5 RID: 2789 RVA: 0x00032A5C File Offset: 0x00030C5C
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		this._transformL.transform.Rotate(this._rotationVector, this._startRotation, Space.Self);
		this._transformR.transform.Rotate(this._rotationVector, -this._startRotation, Space.Self);
		this._rotationDataL = new LightPairRotationEventEffect.RotationData
		{
			enabled = false,
			rotationSpeed = 0f,
			startRotation = this._transformL.rotation,
			transform = this._transformL
		};
		this._rotationDataR = new LightPairRotationEventEffect.RotationData
		{
			enabled = false,
			rotationSpeed = 0f,
			startRotation = this._transformR.rotation,
			transform = this._transformR
		};
		base.enabled = false;
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00032B38 File Offset: 0x00030D38
	protected void Update()
	{
		if (this._rotationDataL.enabled)
		{
			this._rotationDataL.transform.Rotate(this._rotationVector, Time.deltaTime * this._rotationDataL.rotationSpeed, Space.Self);
		}
		if (this._rotationDataR.enabled)
		{
			this._rotationDataR.transform.Rotate(this._rotationVector, Time.deltaTime * this._rotationDataR.rotationSpeed, Space.Self);
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x000087A0 File Offset: 0x000069A0
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x00032BB0 File Offset: 0x00030DB0
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._eventL || beatmapEventData.type == this._eventR)
		{
			int frameCount = Time.frameCount;
			if (this._randomGenerationFrameNum != frameCount)
			{
				if (this._overrideRandomValues)
				{
					this._randomDirection = ((beatmapEventData.type == this._eventL) ? 1f : -1f);
					this._randomStartRotation = (float)((beatmapEventData.type == this._eventL) ? frameCount : (-(float)frameCount));
					if (this._useZPositionForAngleOffset)
					{
						this._randomStartRotation += base.transform.position.z * this._zPositionAngleOffsetScale;
					}
				}
				else
				{
					this._randomDirection = ((UnityEngine.Random.value > 0.5f) ? 1f : -1f);
					this._randomStartRotation = UnityEngine.Random.Range(0f, 360f);
				}
				this._randomGenerationFrameNum = Time.frameCount;
			}
			if (beatmapEventData.type == this._eventL)
			{
				this.UpdateRotationData(beatmapEventData.value, this._rotationDataL, this._randomStartRotation, this._randomDirection);
			}
			else if (beatmapEventData.type == this._eventR)
			{
				this.UpdateRotationData(beatmapEventData.value, this._rotationDataR, -this._randomStartRotation, -this._randomDirection);
			}
			base.enabled = (this._rotationDataL.enabled || this._rotationDataR.enabled);
		}
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00032D1C File Offset: 0x00030F1C
	private void UpdateRotationData(int beatmapEventDataValue, LightPairRotationEventEffect.RotationData rotationData, float startRotationOffset, float direction)
	{
		if (beatmapEventDataValue == 0)
		{
			rotationData.enabled = false;
			rotationData.transform.localRotation = rotationData.startRotation;
			return;
		}
		if (beatmapEventDataValue > 0)
		{
			rotationData.enabled = true;
			rotationData.transform.localRotation = rotationData.startRotation;
			rotationData.transform.Rotate(this._rotationVector, startRotationOffset, Space.Self);
			rotationData.rotationSpeed = (float)beatmapEventDataValue * 20f * direction;
		}
	}

	// Token: 0x04000B5F RID: 2911
	[SerializeField]
	private BeatmapEventType _eventL;

	// Token: 0x04000B60 RID: 2912
	[SerializeField]
	private BeatmapEventType _eventR;

	// Token: 0x04000B61 RID: 2913
	[SerializeField]
	private Vector3 _rotationVector = Vector3.up;

	// Token: 0x04000B62 RID: 2914
	[Space]
	[SerializeField]
	private bool _overrideRandomValues;

	// Token: 0x04000B63 RID: 2915
	[SerializeField]
	private bool _useZPositionForAngleOffset;

	// Token: 0x04000B64 RID: 2916
	[SerializeField]
	private float _zPositionAngleOffsetScale = 1f;

	// Token: 0x04000B65 RID: 2917
	[SerializeField]
	private float _startRotation;

	// Token: 0x04000B66 RID: 2918
	[Space]
	[SerializeField]
	private Transform _transformL;

	// Token: 0x04000B67 RID: 2919
	[SerializeField]
	private Transform _transformR;

	// Token: 0x04000B68 RID: 2920
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000B69 RID: 2921
	private const float kSpeedMultiplier = 20f;

	// Token: 0x04000B6A RID: 2922
	private LightPairRotationEventEffect.RotationData _rotationDataL;

	// Token: 0x04000B6B RID: 2923
	private LightPairRotationEventEffect.RotationData _rotationDataR;

	// Token: 0x04000B6C RID: 2924
	private int _randomGenerationFrameNum = -1;

	// Token: 0x04000B6D RID: 2925
	private float _randomStartRotation;

	// Token: 0x04000B6E RID: 2926
	private float _randomDirection;

	// Token: 0x0200028A RID: 650
	private class RotationData
	{
		// Token: 0x04000B6F RID: 2927
		public bool enabled;

		// Token: 0x04000B70 RID: 2928
		public float rotationSpeed;

		// Token: 0x04000B71 RID: 2929
		public Quaternion startRotation;

		// Token: 0x04000B72 RID: 2930
		public Transform transform;
	}
}
