using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000500 RID: 1280
	public class UISnapToBorder : IUISnapToBorder
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x00055DD4 File Offset: 0x00053FD4
		public void SnapToParentBorder(RectTransform rectTransform)
		{
			Vector2 sizeDelta = ((RectTransform)rectTransform.parent.transform).sizeDelta;
			Vector2 sizeDelta2 = rectTransform.sizeDelta;
			Vector2 pivot = rectTransform.pivot;
			Vector2 a = sizeDelta * rectTransform.anchorMin + rectTransform.anchoredPosition;
			Vector2 vector = a - pivot * sizeDelta2;
			Vector2 b = a + (Vector2.one - pivot) * sizeDelta2;
			Vector2 vector2 = vector;
			Vector2 vector3 = sizeDelta - b;
			float x = vector2.x;
			float x2 = vector3.x;
			float y = vector2.y;
			float y2 = vector3.y;
			if (x2 < x)
			{
				rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, x2, sizeDelta2.x);
			}
			else
			{
				rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, x, sizeDelta2.x);
			}
			if (y2 < y)
			{
				rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, y2, sizeDelta2.y);
				return;
			}
			rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, y, sizeDelta2.y);
		}
	}
}
