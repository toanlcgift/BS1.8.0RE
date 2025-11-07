using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E9 RID: 1257
	public class GridLaneFrontQuad : MonoBehaviour
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x000114F3 File Offset: 0x0000F6F3
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x000114FB File Offset: 0x0000F6FB
		public bool isHighlighted
		{
			get
			{
				return this._isHighlighted;
			}
			set
			{
				this._isHighlighted = value;
				this.SetColorAtGradientPosition(this._colorGradientPosition);
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00011510 File Offset: 0x0000F710
		protected void Awake()
		{
			this._renderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0001151E File Offset: 0x0000F71E
		public void SetColorScheme(GridColorScheme colorScheme)
		{
			this._normalColorGradient = colorScheme.frontQuadNormalColorGradient;
			this._highlightedColorGradient = colorScheme.frontQuadHighlightedColorGradient;
			this.SetColorAtGradientPosition(this._colorGradientPosition);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00054C44 File Offset: 0x00052E44
		public void SetColorAtGradientPosition(float gradientPosition)
		{
			this._colorGradientPosition = gradientPosition;
			Color color = (this.isHighlighted ? this._highlightedColorGradient : this._normalColorGradient).Evaluate(gradientPosition);
			this._renderer.material.color = color;
		}

		// Token: 0x04001743 RID: 5955
		private float _colorGradientPosition;

		// Token: 0x04001744 RID: 5956
		private Gradient _normalColorGradient;

		// Token: 0x04001745 RID: 5957
		private Gradient _highlightedColorGradient;

		// Token: 0x04001746 RID: 5958
		private Renderer _renderer;

		// Token: 0x04001747 RID: 5959
		private bool _isHighlighted;

		// Token: 0x04001748 RID: 5960
		public float laneAngle;

		// Token: 0x04001749 RID: 5961
		public Vector2Int positionIndex;
	}
}
