using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A3 RID: 675
public class TrackLaneRingsRotationEffect : MonoBehaviour
{
	// Token: 0x06000B66 RID: 2918 RVA: 0x00034358 File Offset: 0x00032558
	protected void Awake()
	{
		this._activeRingRotationEffects = new List<TrackLaneRingsRotationEffect.RingRotationEffect>(20);
		this._ringRotationEffectsPool = new List<TrackLaneRingsRotationEffect.RingRotationEffect>(20);
		for (int i = 0; i < this._ringRotationEffectsPool.Capacity; i++)
		{
			this._ringRotationEffectsPool.Add(new TrackLaneRingsRotationEffect.RingRotationEffect());
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00008F6F File Offset: 0x0000716F
	protected void Start()
	{
		this.AddRingRotationEffect(this._startupRotationAngle, this._startupRotationStep, this._startupRotationPropagationSpeed, this._startupRotationFlexySpeed);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000343A8 File Offset: 0x000325A8
	protected void FixedUpdate()
	{
		TrackLaneRing[] rings = this._trackLaneRingsManager.Rings;
		float fixedDeltaTime = Time.fixedDeltaTime;
		for (int i = this._activeRingRotationEffects.Count - 1; i >= 0; i--)
		{
			TrackLaneRingsRotationEffect.RingRotationEffect ringRotationEffect = this._activeRingRotationEffects[i];
			int num = ringRotationEffect.progressPos;
			while (num < ringRotationEffect.progressPos + ringRotationEffect.rotationPropagationSpeed && num < rings.Length)
			{
				rings[num].SetDestRotation(ringRotationEffect.rotationAngle + (float)num * ringRotationEffect.rotationStep, ringRotationEffect.rotationFlexySpeed);
				num++;
			}
			ringRotationEffect.progressPos += ringRotationEffect.rotationPropagationSpeed;
			if (ringRotationEffect.progressPos >= rings.Length)
			{
				this.RecycleRingRotationEffect(this._activeRingRotationEffects[i]);
				this._activeRingRotationEffects.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00034470 File Offset: 0x00032670
	public void AddRingRotationEffect(float angle, float step, int propagationSpeed, float flexySpeed)
	{
		TrackLaneRingsRotationEffect.RingRotationEffect ringRotationEffect = this.SpawnRingRotationEffect();
		ringRotationEffect.progressPos = 0;
		ringRotationEffect.rotationAngle = angle;
		ringRotationEffect.rotationStep = step;
		ringRotationEffect.rotationPropagationSpeed = propagationSpeed;
		ringRotationEffect.rotationFlexySpeed = flexySpeed;
		this._activeRingRotationEffects.Add(ringRotationEffect);
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00008F8F File Offset: 0x0000718F
	public float GetFirstRingRotationAngle()
	{
		return this._trackLaneRingsManager.Rings[0].GetRotation();
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00008FA3 File Offset: 0x000071A3
	public float GetFirstRingDestinationRotationAngle()
	{
		return this._trackLaneRingsManager.Rings[0].GetDestinationRotation();
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000344B4 File Offset: 0x000326B4
	private TrackLaneRingsRotationEffect.RingRotationEffect SpawnRingRotationEffect()
	{
		TrackLaneRingsRotationEffect.RingRotationEffect result;
		if (this._ringRotationEffectsPool.Count > 0)
		{
			result = this._ringRotationEffectsPool[0];
			this._ringRotationEffectsPool.RemoveAt(0);
		}
		else
		{
			result = new TrackLaneRingsRotationEffect.RingRotationEffect();
		}
		return result;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00008FB7 File Offset: 0x000071B7
	private void RecycleRingRotationEffect(TrackLaneRingsRotationEffect.RingRotationEffect ringRotationEffect)
	{
		this._ringRotationEffectsPool.Add(ringRotationEffect);
	}

	// Token: 0x04000C07 RID: 3079
	[SerializeField]
	private TrackLaneRingsManager _trackLaneRingsManager;

	// Token: 0x04000C08 RID: 3080
	[Header("Startup buildup")]
	[SerializeField]
	private float _startupRotationAngle;

	// Token: 0x04000C09 RID: 3081
	[SerializeField]
	private float _startupRotationStep = 10f;

	// Token: 0x04000C0A RID: 3082
	[SerializeField]
	private int _startupRotationPropagationSpeed = 1;

	// Token: 0x04000C0B RID: 3083
	[SerializeField]
	private float _startupRotationFlexySpeed = 0.5f;

	// Token: 0x04000C0C RID: 3084
	private List<TrackLaneRingsRotationEffect.RingRotationEffect> _activeRingRotationEffects;

	// Token: 0x04000C0D RID: 3085
	private List<TrackLaneRingsRotationEffect.RingRotationEffect> _ringRotationEffectsPool;

	// Token: 0x04000C0E RID: 3086
	private List<int> ringRotationEffectsToDelete = new List<int>();

	// Token: 0x020002A4 RID: 676
	private class RingRotationEffect
	{
		// Token: 0x04000C0F RID: 3087
		public float rotationAngle;

		// Token: 0x04000C10 RID: 3088
		public float rotationStep;

		// Token: 0x04000C11 RID: 3089
		public float rotationFlexySpeed;

		// Token: 0x04000C12 RID: 3090
		public int rotationPropagationSpeed;

		// Token: 0x04000C13 RID: 3091
		public int progressPos;
	}
}
