using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004F9 RID: 1273
	public class RotateAroundAnimator : MonoBehaviour
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x00011903 File Offset: 0x0000FB03
		protected void Awake()
		{
			this._transform = base.transform;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00055A50 File Offset: 0x00053C50
		protected void Update()
		{
			if (!this._isAnimating)
			{
				return;
			}
			this._elapsedTime += (this._useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
			if (this._elapsedTime < this._duration)
			{
				this.AnimateStep(this._elapsedTime);
				return;
			}
			this.FinishAnimation();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00055AA8 File Offset: 0x00053CA8
		private void AnimateStep(float elapsedTime)
		{
			float t = Mathf.Clamp01(this._animationCurve.Evaluate(this._elapsedTime / this._duration));
			float num = Mathf.Lerp(this._startAngle, this._targetAngle, t);
			this._transform.RotateAround(this._anchorPoint, this._anchorAxis, num - this._currentAngle);
			this._currentAngle = num;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00055B0C File Offset: 0x00053D0C
		public void AnimateRotationAround(Vector3 anchorPoint, Vector3 anchorAxis, float degrees, float duration = 0f, bool useUnscaledTime = false)
		{
			if (this._useUnscaledTime != useUnscaledTime)
			{
				this.FinishAnimation();
			}
			if (this._isAnimating)
			{
				return;
			}
			this._anchorPoint = anchorPoint;
			this._anchorAxis = anchorAxis;
			this._startAngle = 0f;
			this._currentAngle = 0f;
			this._targetAngle = degrees;
			this._useUnscaledTime = useUnscaledTime;
			this._duration = ((duration == 0f) ? this._defaultDration : duration);
			this._elapsedTime = 0f;
			this._isAnimating = true;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00011911 File Offset: 0x0000FB11
		public void PauseAnimation()
		{
			this._isAnimating = false;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0001191A File Offset: 0x0000FB1A
		public void ResumeAnimation()
		{
			this._isAnimating = true;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00011923 File Offset: 0x0000FB23
		public void FinishAnimation()
		{
			this._isAnimating = false;
			this._elapsedTime = this._duration;
			this._startAngle = 0f;
			this._currentAngle = 0f;
			this._targetAngle = 0f;
		}

		// Token: 0x0400179A RID: 6042
		[SerializeField]
		private float _defaultDration = 0.3f;

		// Token: 0x0400179B RID: 6043
		[SerializeField]
		private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x0400179C RID: 6044
		private Transform _transform;

		// Token: 0x0400179D RID: 6045
		private bool _isAnimating;

		// Token: 0x0400179E RID: 6046
		private bool _useUnscaledTime;

		// Token: 0x0400179F RID: 6047
		private float _duration;

		// Token: 0x040017A0 RID: 6048
		private float _elapsedTime;

		// Token: 0x040017A1 RID: 6049
		private Vector3 _anchorPoint = Vector3.zero;

		// Token: 0x040017A2 RID: 6050
		private Vector3 _anchorAxis = Vector3.zero;

		// Token: 0x040017A3 RID: 6051
		private float _startAngle;

		// Token: 0x040017A4 RID: 6052
		private float _currentAngle;

		// Token: 0x040017A5 RID: 6053
		private float _targetAngle;
	}
}
