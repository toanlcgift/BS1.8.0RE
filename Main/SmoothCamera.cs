using System;
using UnityEngine;
using Zenject;

// Token: 0x02000315 RID: 789
public class SmoothCamera : MonoBehaviour
{
	// Token: 0x06000DAC RID: 3500 RVA: 0x00039644 File Offset: 0x00037844
	public void Init(float fieldOfView, float positionSmooth, float rotationSmooth, bool thirdPersonEnabled, Vector3 thirdPersonPosition, Vector3 thirdPersonEulerAngles)
	{
		this._camera.fieldOfView = fieldOfView / this._camera.aspect;
		this._camera.depthTextureMode = this._mainCamera.camera.depthTextureMode;
		this._camera.nearClipPlane = this._mainCamera.camera.nearClipPlane;
		this._camera.farClipPlane = this._mainCamera.camera.farClipPlane;
		this._thirdPersonPosition = thirdPersonPosition;
		this._thirdPersonEnabled = thirdPersonEnabled;
		this._rotationSmooth = rotationSmooth;
		this._positionSmooth = positionSmooth;
		this._thirdPersonEulerAngles = thirdPersonEulerAngles;
		base.transform.SetPositionAndRotation(this._mainCamera.position, this._mainCamera.rotation);
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0000A8EF File Offset: 0x00008AEF
	protected void OnEnable()
	{
		this._camera.enabled = true;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0000A8FD File Offset: 0x00008AFD
	protected void OnDisable()
	{
		this._camera.enabled = false;
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00039704 File Offset: 0x00037904
	protected void LateUpdate()
	{
		if (this._thirdPersonEnabled)
		{
			Quaternion rotation = default(Quaternion);
			rotation.eulerAngles = this._thirdPersonEulerAngles;
			base.transform.SetPositionAndRotation(this._thirdPersonPosition, rotation);
			return;
		}
		Vector3 position = Vector3.Lerp(base.transform.position, this._mainCamera.position, Time.deltaTime * this._positionSmooth);
		Quaternion rotation2 = Quaternion.Slerp(base.transform.rotation, this._mainCamera.rotation, Time.deltaTime * this._rotationSmooth);
		base.transform.SetPositionAndRotation(position, rotation2);
	}

	// Token: 0x04000E15 RID: 3605
	[SerializeField]
	private Camera _camera;

	// Token: 0x04000E16 RID: 3606
	[Inject]
	private MainCamera _mainCamera;

	// Token: 0x04000E17 RID: 3607
	private Vector3 _thirdPersonPosition;

	// Token: 0x04000E18 RID: 3608
	private Vector3 _thirdPersonEulerAngles;

	// Token: 0x04000E19 RID: 3609
	private bool _thirdPersonEnabled;

	// Token: 0x04000E1A RID: 3610
	private float _rotationSmooth;

	// Token: 0x04000E1B RID: 3611
	private float _positionSmooth;
}
