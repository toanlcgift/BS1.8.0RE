using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004ED RID: 1261
	public class UIInstaller : MonoInstaller
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x00011544 File Offset: 0x0000F744
		public override void InstallBindings()
		{
			base.Container.Bind<UIActivityIndicatorText>().FromInstance(this._fullscreenActivityIndicator);
		}

		// Token: 0x04001754 RID: 5972
		[SerializeField]
		private UIActivityIndicatorText _fullscreenActivityIndicator;
	}
}
