using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004EB RID: 1259
	public class CameraInstaller : MonoInstaller
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x00054C88 File Offset: 0x00052E88
		public override void InstallBindings()
		{
			base.Container.Bind<Camera>().FromInstance(this._mainCamera);
			base.Container.Bind<CameraController>().FromInstance(this._cameraController);
			base.Container.Bind<PositionAnimator>().FromInstance(this._cameraPositionAnimator);
			base.Container.Bind<RotateAroundAnimator>().FromInstance(this._cameraRotateAroundAnimator);
		}

		// Token: 0x0400174A RID: 5962
		[SerializeField]
		private Camera _mainCamera;

		// Token: 0x0400174B RID: 5963
		[SerializeField]
		private CameraController _cameraController;

		// Token: 0x0400174C RID: 5964
		[SerializeField]
		private PositionAnimator _cameraPositionAnimator;

		// Token: 0x0400174D RID: 5965
		[SerializeField]
		private RotateAroundAnimator _cameraRotateAroundAnimator;
	}
}
