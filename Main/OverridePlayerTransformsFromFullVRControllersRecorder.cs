using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x0200043A RID: 1082
public class OverridePlayerTransformsFromFullVRControllersRecorder : MonoBehaviour
{
	// Token: 0x060014B8 RID: 5304 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
	protected void Start()
	{
		this._fullVRControllersRecorder.didSetControllerTransformEvent += this.HandleFullVRControllersRecorderDidSetControllerTransform;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0000FA0F File Offset: 0x0000DC0F
	protected void OnDestroy()
	{
		if (this._fullVRControllersRecorder != null)
		{
			this._fullVRControllersRecorder.didSetControllerTransformEvent -= this.HandleFullVRControllersRecorderDidSetControllerTransform;
		}
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x0004BDC4 File Offset: 0x00049FC4
	private void HandleFullVRControllersRecorderDidSetControllerTransform(VRController controller)
	{
		if (controller.node == XRNode.Head)
		{
			this._playerController.OverrideHeadPos(controller.transform.position);
			return;
		}
		if (controller.node == XRNode.LeftHand)
		{
			this._playerController.leftSaber.OverridePositionAndRotation(controller.position, controller.rotation);
			return;
		}
		if (controller.node == XRNode.RightHand)
		{
			this._playerController.rightSaber.OverridePositionAndRotation(controller.position, controller.rotation);
		}
	}

	// Token: 0x0400146E RID: 5230
	[SerializeField]
	private FullVRControllersRecorder _fullVRControllersRecorder;

	// Token: 0x0400146F RID: 5231
	[Inject]
	private PlayerController _playerController;
}
