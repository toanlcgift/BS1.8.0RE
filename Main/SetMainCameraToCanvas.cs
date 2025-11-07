using System;
using UnityEngine;
using Zenject;

// Token: 0x020003AD RID: 941
public class SetMainCameraToCanvas : MonoBehaviour
{
	// Token: 0x0600114F RID: 4431 RVA: 0x0000D178 File Offset: 0x0000B378
	private void Start()
	{
		this._canvas.worldCamera = this._mainCamera.camera;
	}

	// Token: 0x0400112F RID: 4399
	[SerializeField]
	private Canvas _canvas;

	// Token: 0x04001130 RID: 4400
	[Inject]
	private MainCamera _mainCamera;
}
