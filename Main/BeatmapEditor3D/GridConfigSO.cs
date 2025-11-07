using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E2 RID: 1250
	public class GridConfigSO : PersistentScriptableObject
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00011059 File Offset: 0x0000F259
		public GridConfig gridConfig
		{
			get
			{
				return this._gridConfig;
			}
		}

		// Token: 0x0400170D RID: 5901
		[SerializeField]
		private GridConfig _gridConfig;
	}
}
