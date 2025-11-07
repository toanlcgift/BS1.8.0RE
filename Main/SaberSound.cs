using System;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class SaberSound : MonoBehaviour
{
	// Token: 0x06000D5E RID: 3422 RVA: 0x0000A589 File Offset: 0x00008789
	protected void Start()
	{
		this._prevPos = this._saberTop.position;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00038A44 File Offset: 0x00036C44
	protected void Update()
	{
		Vector3 position = this._saberTop.position;
		if ((this._prevPos - position).sqrMagnitude > this._noSoundTopThresholdSqr)
		{
			this._prevPos = position;
		}
		float num;
		if (Time.deltaTime == 0f)
		{
			num = 0f;
		}
		else
		{
			num = this._speedMultiplier * Vector3.Distance(position, this._prevPos) / Time.deltaTime;
		}
		if (num < this._speed)
		{
			this._speed = Mathf.Clamp01(Mathf.Lerp(this._speed, num, Time.deltaTime * this._downSmooth));
		}
		else
		{
			this._speed = Mathf.Clamp01(Mathf.Lerp(this._speed, num, Time.deltaTime * this._upSmooth));
		}
		this._audioSource.pitch = this._pitchBySpeedCurve.Evaluate(this._speed);
		this._audioSource.volume = this._gainBySpeedCurve.Evaluate(this._speed);
		this._prevPos = position;
	}

	// Token: 0x04000DC7 RID: 3527
	[SerializeField]
	private Transform _saberTop;

	// Token: 0x04000DC8 RID: 3528
	[SerializeField]
	private AudioSource _audioSource;

	// Token: 0x04000DC9 RID: 3529
	[SerializeField]
	private AnimationCurve _pitchBySpeedCurve;

	// Token: 0x04000DCA RID: 3530
	[SerializeField]
	private AnimationCurve _gainBySpeedCurve;

	// Token: 0x04000DCB RID: 3531
	[SerializeField]
	private float _speedMultiplier = 0.05f;

	// Token: 0x04000DCC RID: 3532
	[SerializeField]
	private float _upSmooth = 4f;

	// Token: 0x04000DCD RID: 3533
	[SerializeField]
	private float _downSmooth = 4f;

	// Token: 0x04000DCE RID: 3534
	[Tooltip("No sound is produced if saber point moves more than this distance in one frame. This basically fixes the start sound problem.")]
	[SerializeField]
	private float _noSoundTopThresholdSqr = 1f;

	// Token: 0x04000DCF RID: 3535
	private Vector3 _prevPos;

	// Token: 0x04000DD0 RID: 3536
	private float _speed;
}
