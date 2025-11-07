using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E8 RID: 1256
	public class GridLaneConfigSO : PersistentScriptableObject
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x000114EB File Offset: 0x0000F6EB
		public GridLaneConfig gridLaneConfig
		{
			get
			{
				return this._gridLaneConfig;
			}
		}

		// Token: 0x04001742 RID: 5954
		[SerializeField]
		private GridLaneConfig _gridLaneConfig;
	}
}
