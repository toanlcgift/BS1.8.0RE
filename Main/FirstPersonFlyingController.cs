using System;
using UnityEngine;
using VRUIControls;

// Token: 0x02000433 RID: 1075
public class FirstPersonFlyingController : MonoBehaviour
{
	// Token: 0x06001490 RID: 5264 RVA: 0x0004B32C File Offset: 0x0004952C
	protected void Start()
	{
		this._cameraTransform = this._camera.transform;
		this._mouseLook.Init(base.transform, this._cameraTransform);
		this._controller0.enabled = false;
		this._controller1.enabled = false;
		this._camera.stereoTargetEye = StereoTargetEyeMask.None;
		this._camera.fieldOfView = this._cameraFov;
		this._camera.aspect = (float)Screen.width / (float)Screen.height;
		this._centerAdjust.ResetRoom();
		this._centerAdjust.enabled = false;
		base.transform.position = new Vector3(0f, 1.7f, 0f);
		foreach (GameObject gameObject in this._controllerModels)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0000F7EA File Offset: 0x0000D9EA
	protected void OnEnable()
	{
		this._vrInputModule.useMouseForPressInput = true;
		this._mouseLook.SetCursorLock(true);
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x0000F804 File Offset: 0x0000DA04
	protected void OnDisable()
	{
		this._vrInputModule.useMouseForPressInput = false;
		this._mouseLook.SetCursorLock(false);
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0004B410 File Offset: 0x00049610
	protected void Update()
	{
		this._mouseLook.LookRotation(base.transform, this._cameraTransform);
		this._controller0.transform.SetPositionAndRotation(this._cameraTransform.position, this._cameraTransform.rotation);
		this._controller1.transform.SetPositionAndRotation(this._cameraTransform.position, this._cameraTransform.rotation);
		Vector3 vector = base.transform.position;
		Vector3 a = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
			a = this._cameraTransform.forward;
		}
		if (Input.GetKey(KeyCode.S))
		{
			a = -this._cameraTransform.forward;
		}
		Vector3 b = Vector3.zero;
		if (Input.GetKey(KeyCode.D))
		{
			b = this._cameraTransform.right;
		}
		if (Input.GetKey(KeyCode.A))
		{
			b = -this._cameraTransform.right;
		}
		vector += (a + b) * this._moveSensitivity;
		base.transform.position = vector;
	}

	// Token: 0x0400143B RID: 5179
	[SerializeField]
	private float _moveSensitivity = 0.1f;

	// Token: 0x0400143C RID: 5180
	[SerializeField]
	private Camera _camera;

	// Token: 0x0400143D RID: 5181
	[SerializeField]
	private float _cameraFov = 90f;

	// Token: 0x0400143E RID: 5182
	[SerializeField]
	private VRCenterAdjust _centerAdjust;

	// Token: 0x0400143F RID: 5183
	[SerializeField]
	private VRController _controller0;

	// Token: 0x04001440 RID: 5184
	[SerializeField]
	private VRController _controller1;

	// Token: 0x04001441 RID: 5185
	[SerializeField]
	private VRInputModule _vrInputModule;

	// Token: 0x04001442 RID: 5186
	[SerializeField]
	private GameObject[] _controllerModels;

	// Token: 0x04001443 RID: 5187
	[SerializeField]
	private MouseLook _mouseLook = new MouseLook();

	// Token: 0x04001444 RID: 5188
	private Transform _cameraTransform;
}
