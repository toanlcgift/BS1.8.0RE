using System;
using UnityEngine;
using Zenject;

namespace BeatmapEditor3D
{
	// Token: 0x020004DB RID: 1243
	public class EditorManager : MonoBehaviour
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x00010F75 File Offset: 0x0000F175
		protected void Start()
		{
			this._gridController.Init();
		}

		// Token: 0x040016F6 RID: 5878
		[Inject]
		private GridController _gridController;
	}
}
