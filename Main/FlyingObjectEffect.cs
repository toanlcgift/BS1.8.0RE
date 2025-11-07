using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public abstract class FlyingObjectEffect : MonoBehaviour
{
	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06000A8F RID: 2703 RVA: 0x0003140C File Offset: 0x0002F60C
	// (remove) Token: 0x06000A90 RID: 2704 RVA: 0x00031444 File Offset: 0x0002F644
	public event Action<FlyingObjectEffect> didFinishEvent;

	// Token: 0x06000A91 RID: 2705 RVA: 0x0003147C File Offset: 0x0002F67C
	public void InitAndPresent(float duration, Vector3 targetPos, Quaternion rotation, bool shake)
	{
		this._rotation = rotation;
		base.transform.localRotation = rotation;
		this._duration = duration;
		this._targetPos = targetPos;
		this._shake = shake;
		this._elapsedTime = 0f;
		this._startPos = base.transform.position;
		this._initialized = true;
		base.enabled = true;
		this.ManualUpdate(0f);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x000314E8 File Offset: 0x0002F6E8
	protected void Update()
	{
		if (!this._initialized)
		{
			base.enabled = false;
			return;
		}
		if (this._elapsedTime < this._duration)
		{
			float num = this._elapsedTime / this._duration;
			this.ManualUpdate(num);
			base.transform.localPosition = Vector3.Lerp(this._startPos, this._targetPos, this._moveAnimationCurve.Evaluate(num));
			if (this._shake)
			{
				this._shakeRotation.eulerAngles = new Vector3(0f, 0f, Mathf.Sin(num * 3.1415927f * this._shakeFrequency) * this._shakeStrength * this._shakeStrengthAnimationCurve.Evaluate(num));
				base.transform.localRotation = this._rotation * this._shakeRotation;
			}
			this._elapsedTime += Time.deltaTime;
			return;
		}
		Action<FlyingObjectEffect> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this);
	}

	// Token: 0x06000A93 RID: 2707
	protected abstract void ManualUpdate(float t);

	// Token: 0x04000AF1 RID: 2801
	[SerializeField]
	private AnimationCurve _moveAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04000AF2 RID: 2802
	[SerializeField]
	private float _shakeFrequency = 1f;

	// Token: 0x04000AF3 RID: 2803
	[SerializeField]
	private float _shakeStrength = 20f;

	// Token: 0x04000AF4 RID: 2804
	[SerializeField]
	private AnimationCurve _shakeStrengthAnimationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x04000AF6 RID: 2806
	private bool _initialized;

	// Token: 0x04000AF7 RID: 2807
	private Quaternion _shakeRotation;

	// Token: 0x04000AF8 RID: 2808
	private Quaternion _rotation;

	// Token: 0x04000AF9 RID: 2809
	private float _elapsedTime;

	// Token: 0x04000AFA RID: 2810
	private Vector3 _startPos;

	// Token: 0x04000AFB RID: 2811
	private Vector3 _targetPos;

	// Token: 0x04000AFC RID: 2812
	private float _duration;

	// Token: 0x04000AFD RID: 2813
	private bool _shake;
}
