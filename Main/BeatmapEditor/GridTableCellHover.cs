using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200056E RID: 1390
	public class GridTableCellHover : MonoBehaviour
	{
		// Token: 0x170004F7 RID: 1271
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x00013FEF File Offset: 0x000121EF
		public Vector3 position
		{
			set
			{
				((RectTransform)base.transform).anchoredPosition = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0000F895 File Offset: 0x0000DA95
		public bool visible
		{
			set
			{
				base.gameObject.SetActive(value);
			}
		}
	}
}
