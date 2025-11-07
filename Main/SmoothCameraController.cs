using System;
using LIV.SDK.Unity;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class SmoothCameraController : MonoBehaviour
{
	// Token: 0x06000DB1 RID: 3505 RVA: 0x0000A90B File Offset: 0x00008B0B
	protected void Start()
	{
		this._liv.didActivateEvent += this.HandleLIVDidActivate;
		this._liv.didDeactivateEvent += this.HandleLIVDidDeactivate;
		this.ActivateSmoothCameraIfNeeded();
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0000A941 File Offset: 0x00008B41
	protected void OnDestroy()
	{
		this._liv.didActivateEvent -= this.HandleLIVDidActivate;
		this._liv.didDeactivateEvent -= this.HandleLIVDidDeactivate;
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0000A971 File Offset: 0x00008B71
	private void HandleLIVDidActivate()
	{
		if (this._smoothCamera.enabled)
		{
			this._smoothCamera.enabled = false;
		}
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0000A98C File Offset: 0x00008B8C
	private void HandleLIVDidDeactivate()
	{
		this.ActivateSmoothCameraIfNeeded();
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x000397A0 File Offset: 0x000379A0
	private void ActivateSmoothCameraIfNeeded()
	{
		if (this._liv.isActive)
		{
			return;
		}
		if (this._mainSettingsModel.smoothCameraEnabled && !this._smoothCamera.enabled)
		{
			this._smoothCamera.enabled = true;
			this._smoothCamera.Init(this._mainSettingsModel.smoothCameraFieldOfView, this._mainSettingsModel.smoothCameraPositionSmooth, this._mainSettingsModel.smoothCameraRotationSmooth, this._mainSettingsModel.smoothCameraThirdPersonEnabled, this._mainSettingsModel.smoothCameraThirdPersonPosition, this._mainSettingsModel.smoothCameraThirdPersonEulerAngles);
		}
	}

	// Token: 0x04000E1C RID: 3612
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x04000E1D RID: 3613
	[SerializeField]
	private SmoothCamera _smoothCamera;

	// Token: 0x04000E1E RID: 3614
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private LIV.SDK.Unity.LIV _liv;
}
