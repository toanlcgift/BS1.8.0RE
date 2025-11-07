using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E0 RID: 1248
	public class GridColorSchemeSO : PersistentScriptableObject
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00011004 File Offset: 0x0000F204
		public GridColorScheme colorScheme
		{
			get
			{
				return this._colorScheme;
			}
		}

		// Token: 0x04001708 RID: 5896
		[SerializeField]
		private GridColorScheme _colorScheme;
	}
}
