using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x0200050D RID: 1293
	public class FPSMouse : MonoBehaviour
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x00012042 File Offset: 0x00010242
		protected void Awake()
		{
			this._cameraTransform = this._camera.transform;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00012055 File Offset: 0x00010255
		protected void OnEnable()
		{
			this._lastMousePosition = Input.mousePosition;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005662C File Offset: 0x0005482C
		protected void Update()
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = (mousePosition - this._lastMousePosition) * (this._sensitivity * TimeHelper.deltaTime);
			this._lastMousePosition = mousePosition;
			this._cameraTransform.Rotate(-vector.y * Vector3.right, Space.Self);
			this._cameraTransform.Rotate(vector.x * Vector3.up, Space.World);
		}

		// Token: 0x0400180B RID: 6155
		[Inject]
		private Camera _camera;

		// Token: 0x0400180C RID: 6156
		private Transform _cameraTransform;

		// Token: 0x0400180D RID: 6157
		private float _sensitivity = 20f;

		// Token: 0x0400180E RID: 6158
		private Vector3 _lastMousePosition = Vector3.zero;
	}
}
