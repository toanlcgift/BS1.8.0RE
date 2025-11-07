using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004D9 RID: 1241
	public class BeatmapObjectsConfigSO : PersistentScriptableObject
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00010F0C File Offset: 0x0000F10C
		public BeatmapObjectsConfig beatmapObjectsConfig
		{
			get
			{
				return this._beatmapObjectsConfig;
			}
		}

		// Token: 0x040016F0 RID: 5872
		[SerializeField]
		private BeatmapObjectsConfig _beatmapObjectsConfig;
	}
}
