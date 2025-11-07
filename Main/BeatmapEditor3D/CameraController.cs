using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x02000505 RID: 1285
	public class CameraController : MonoBehaviour
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00011E64 File Offset: 0x00010064
		// (set) Token: 0x06001835 RID: 6197 RVA: 0x00011E71 File Offset: 0x00010071
		public bool isFpsCameraControllerActive
		{
			get
			{
				return this._fpsCameraController.activeSelf;
			}
			set
			{
				this._fpsCameraController.SetActive(value);
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000023E9 File Offset: 0x000005E9
		protected void Awake()
		{
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00055F74 File Offset: 0x00054174
		public void UpdateDefaultPositions(float gridRadius)
		{
			bool flag = false;
			float num = 0f;
			for (int i = 0; i < this._defaultCameraPositions.Count; i++)
			{
				Transform transform = this._defaultCameraPositions[i].transform;
				Vector3 localPosition = transform.localPosition;
				Vector3 localPosition2 = this._cameraRoot.localPosition;
				localPosition2.y = localPosition.y;
				if (!flag)
				{
					float num2 = Vector3.Distance(localPosition, localPosition2);
					float num3 = gridRadius - this._defaultCameraDistanceFromGrid;
					num = num2 - num3;
					flag = true;
					if (Mathf.Abs(num) < 0.1f)
					{
						break;
					}
				}
				Vector3 localPosition3 = Vector3.MoveTowards(localPosition, localPosition2, num);
				transform.localPosition = localPosition3;
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00056014 File Offset: 0x00054214
		public void RotateToLane(float degrees, bool animated = true)
		{
			if (animated)
			{
				this._cameraRotateAroundAnimator.AnimateRotationAround(this._cameraRoot.position, Vector3.up, degrees, 0f, false);
				return;
			}
			this._camera.transform.RotateAround(this._cameraRoot.position, Vector3.up, degrees);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00011E7F File Offset: 0x0001007F
		public void ResetPosition(bool animated = true)
		{
			this.ResetPosition(this._defaultCameraPosition, animated);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00056068 File Offset: 0x00054268
		public void ResetPosition(int positionIndex, bool animated = true)
		{
			if (positionIndex >= this._defaultCameraPositions.Count)
			{
				return;
			}
			Transform transform = this._defaultCameraPositions[positionIndex].transform;
			this.ResetPosition(transform, animated);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000560A0 File Offset: 0x000542A0
		private void ResetPosition(Transform transform, bool animated)
		{
			if (animated)
			{
				this._cameraPositionAnimator.AnimateTo(transform.position, transform.rotation, true, 0f);
				return;
			}
			this._camera.transform.position = transform.position;
			this._camera.transform.rotation = transform.rotation;
		}

		// Token: 0x040017C4 RID: 6084
		[SerializeField]
		private GameObject _fpsCameraController;

		// Token: 0x040017C5 RID: 6085
		[Inject]
		private Camera _camera;

		// Token: 0x040017C6 RID: 6086
		[Inject]
		private PositionAnimator _cameraPositionAnimator;

		// Token: 0x040017C7 RID: 6087
		[Inject]
		private RotateAroundAnimator _cameraRotateAroundAnimator;

		// Token: 0x040017C8 RID: 6088
		[SerializeField]
		private Transform _cameraRoot;

		// Token: 0x040017C9 RID: 6089
		[SerializeField]
		private Transform _defaultCameraPosition;

		// Token: 0x040017CA RID: 6090
		[SerializeField]
		private List<Transform> _defaultCameraPositions;

		// Token: 0x040017CB RID: 6091
		[SerializeField]
		private float _defaultCameraDistanceFromGrid = 2f;
	}
}
