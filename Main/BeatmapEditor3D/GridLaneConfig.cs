using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E7 RID: 1255
	[Serializable]
	public class GridLaneConfig
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00011476 File Offset: 0x0000F676
		public int columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0001147E File Offset: 0x0000F67E
		public int rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00011486 File Offset: 0x0000F686
		public Vector3 lineCellSize
		{
			get
			{
				return this._lineCellSize;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0001148E File Offset: 0x0000F68E
		public Vector2 frontQuadMargin
		{
			get
			{
				return this._frontQuadMargin;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00011496 File Offset: 0x0000F696
		public float groundHeight
		{
			get
			{
				return this._groundHeight;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0001149E File Offset: 0x0000F69E
		public float laneLength
		{
			get
			{
				return this._laneLength;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x000114A6 File Offset: 0x0000F6A6
		public float laneWidth
		{
			get
			{
				return (float)this.columns * this.lineCellSize.x;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x000114BB File Offset: 0x0000F6BB
		public float baseBeatLength
		{
			get
			{
				return this._baseBeatLength;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000114C3 File Offset: 0x0000F6C3
		public float firstVisibleNoteDistanceOffset
		{
			get
			{
				return this._firstVisibleNoteDistanceOffset;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000114CB File Offset: 0x0000F6CB
		public float lastVisibleNoteDistanceOffset
		{
			get
			{
				return this._lastVisibleNoteDistanceOffset;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000114D3 File Offset: 0x0000F6D3
		public float fullBeatLineThicknes
		{
			get
			{
				return this._fullBeatLineThicknes;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000114DB File Offset: 0x0000F6DB
		public float majorBeatLineThicknes
		{
			get
			{
				return this._majorBeatLineThicknes;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000114E3 File Offset: 0x0000F6E3
		public float minorBeatLineThicknes
		{
			get
			{
				return this._minorBeatLineThicknes;
			}
		}

		// Token: 0x04001736 RID: 5942
		[SerializeField]
		private int _columns;

		// Token: 0x04001737 RID: 5943
		[SerializeField]
		private int _rows;

		// Token: 0x04001738 RID: 5944
		[SerializeField]
		private Vector2 _frontQuadMargin;

		// Token: 0x04001739 RID: 5945
		[SerializeField]
		private Vector3 _lineCellSize;

		// Token: 0x0400173A RID: 5946
		[SerializeField]
		private float _baseBeatLength;

		// Token: 0x0400173B RID: 5947
		[SerializeField]
		private float _laneLength;

		// Token: 0x0400173C RID: 5948
		[SerializeField]
		private float _groundHeight;

		// Token: 0x0400173D RID: 5949
		[SerializeField]
		private float _firstVisibleNoteDistanceOffset;

		// Token: 0x0400173E RID: 5950
		[SerializeField]
		private float _lastVisibleNoteDistanceOffset;

		// Token: 0x0400173F RID: 5951
		[SerializeField]
		private float _fullBeatLineThicknes;

		// Token: 0x04001740 RID: 5952
		[SerializeField]
		private float _majorBeatLineThicknes;

		// Token: 0x04001741 RID: 5953
		[SerializeField]
		private float _minorBeatLineThicknes;
	}
}
