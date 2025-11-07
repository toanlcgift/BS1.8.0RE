using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000502 RID: 1282
	public class UISnapToGrid : IUISnapToGrid
	{
		// Token: 0x06001822 RID: 6178 RVA: 0x00055EB8 File Offset: 0x000540B8
		public void SnapToGrid(RectTransform rectTransform, Vector2 grid)
		{
			Vector2 anchoredPosition = new Vector2(this.RoundToNearest(rectTransform.anchoredPosition.x, grid.x), this.RoundToNearest(rectTransform.anchoredPosition.y, grid.y));
			rectTransform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00011D92 File Offset: 0x0000FF92
		private float RoundToNearest(float n, float multiple)
		{
			if (multiple == 0f)
			{
				return n;
			}
			return Mathf.Round(n / multiple) * multiple;
		}
	}
}
