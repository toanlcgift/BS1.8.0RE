using System;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class TrackLaneRingsManager : MonoBehaviour
{
	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00008EDC File Offset: 0x000070DC
	public float ringPositionStep
	{
		get
		{
			return this._ringPositionStep;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00008EE4 File Offset: 0x000070E4
	public TrackLaneRing[] Rings
	{
		get
		{
			return this._rings;
		}
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00034130 File Offset: 0x00032330
	protected void Awake()
	{
		Vector3 forward = base.transform.forward;
		this._rings = new TrackLaneRing[this._ringCount];
		for (int i = 0; i < this._rings.Length; i++)
		{
			this._rings[i] = UnityEngine.Object.Instantiate<TrackLaneRing>(this._trackLaneRingPrefab);
			Vector3 position = forward * ((float)i * this._ringPositionStep);
			this._rings[i].Init(position, base.transform.position);
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x000341AC File Offset: 0x000323AC
	protected void FixedUpdate()
	{
		float fixedDeltaTime = TimeHelper.fixedDeltaTime;
		for (int i = 0; i < this._rings.Length; i++)
		{
			this._rings[i].FixedUpdateRing(fixedDeltaTime);
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x000341E0 File Offset: 0x000323E0
	protected void LateUpdate()
	{
		float interpolationFactor = TimeHelper.interpolationFactor;
		for (int i = 0; i < this._rings.Length; i++)
		{
			this._rings[i].LateUpdateRing(interpolationFactor);
		}
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00034214 File Offset: 0x00032414
	protected void OnDrawGizmosSelected()
	{
		Vector3 forward = base.transform.forward;
		Vector3 position = base.transform.position;
		float d = 0.5f;
		float num = 45f;
		Gizmos.DrawRay(position, forward);
		Vector3 a = Quaternion.LookRotation(forward) * Quaternion.Euler(0f, 180f + num, 0f) * new Vector3(0f, 0f, 1f);
		Vector3 a2 = Quaternion.LookRotation(forward) * Quaternion.Euler(0f, 180f - num, 0f) * new Vector3(0f, 0f, 1f);
		Gizmos.DrawRay(position + forward, a * d);
		Gizmos.DrawRay(position + forward, a2 * d);
	}

	// Token: 0x04000BFC RID: 3068
	[SerializeField]
	private TrackLaneRing _trackLaneRingPrefab;

	// Token: 0x04000BFD RID: 3069
	[SerializeField]
	private int _ringCount = 10;

	// Token: 0x04000BFE RID: 3070
	[SerializeField]
	private float _ringPositionStep = 2f;

	// Token: 0x04000BFF RID: 3071
	private TrackLaneRing[] _rings;
}
