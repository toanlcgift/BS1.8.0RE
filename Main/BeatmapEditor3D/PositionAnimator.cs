using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004F0 RID: 1264
	public class PositionAnimator : MonoBehaviour
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x0001168F File Offset: 0x0000F88F
		protected void Awake()
		{
			this._transform = base.transform;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005511C File Offset: 0x0005331C
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

		// Token: 0x060017A9 RID: 6057 RVA: 0x00055174 File Offset: 0x00053374
		private void AnimateStep(float elapsedTime)
		{
			float t = Mathf.Clamp01(this._animationCurve.Evaluate(this._elapsedTime / this._duration));
			Vector3 vector = Vector3.Lerp(this._startPosition, this._targetPosition, t);
			Quaternion quaternion = Quaternion.Lerp(this._startRotation, this._targetRotation, t);
			if (this._useLocalSpace)
			{
				this._transform.localPosition = vector;
				this._transform.localRotation = quaternion;
				return;
			}
			this._transform.position = vector;
			this._transform.rotation = quaternion;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00055200 File Offset: 0x00053400
		public void AnimateTo(Vector3 targetPosition, Quaternion targetRotation, bool localSpace = true, float duration = 0f)
		{
			if (this._isAnimating && this._useLocalSpace != localSpace)
			{
				this.FinishAnimation();
			}
			this._useLocalSpace = localSpace;
			this._startPosition = (localSpace ? this._transform.localPosition : this._transform.position);
			this._startRotation = (localSpace ? this._transform.localRotation : this._transform.rotation);
			this._targetPosition = targetPosition;
			this._targetRotation = targetRotation;
			this._duration = ((duration == 0f) ? this._defaultDration : duration);
			this._elapsedTime = 0f;
			this._isAnimating = true;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0001169D File Offset: 0x0000F89D
		public void PauseAnimation()
		{
			this._isAnimating = false;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000116A6 File Offset: 0x0000F8A6
		public void ResumeAnimation()
		{
			this._isAnimating = true;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000552A8 File Offset: 0x000534A8
		public void FinishAnimation()
		{
			this._isAnimating = false;
			this._elapsedTime = this._duration;
			if (this._useLocalSpace)
			{
				this._transform.localPosition = this._targetPosition;
				this._transform.localRotation = this._targetRotation;
				return;
			}
			this._transform.position = this._targetPosition;
			this._transform.rotation = this._targetRotation;
		}

		// Token: 0x04001769 RID: 5993
		[SerializeField]
		private float _defaultDration = 0.4f;

		// Token: 0x0400176A RID: 5994
		[SerializeField]
		private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x0400176B RID: 5995
		private Transform _transform;

		// Token: 0x0400176C RID: 5996
		private bool _isAnimating;

		// Token: 0x0400176D RID: 5997
		private bool _useUnscaledTime;

		// Token: 0x0400176E RID: 5998
		private float _duration;

		// Token: 0x0400176F RID: 5999
		private float _elapsedTime;

		// Token: 0x04001770 RID: 6000
		private Vector3 _targetPosition = Vector3.zero;

		// Token: 0x04001771 RID: 6001
		private Quaternion _targetRotation = Quaternion.identity;

		// Token: 0x04001772 RID: 6002
		private bool _useLocalSpace = true;

		// Token: 0x04001773 RID: 6003
		private Vector3 _startPosition = Vector3.zero;

		// Token: 0x04001774 RID: 6004
		private Quaternion _startRotation = Quaternion.identity;
	}
}
