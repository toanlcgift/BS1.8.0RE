using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004E1 RID: 1249
	[Serializable]
	public class GridConfig
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0001100C File Offset: 0x0000F20C
		public GridLaneConfig gridLaneConfig
		{
			get
			{
				return this._gridLaneConfig.gridLaneConfig;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00011019 File Offset: 0x0000F219
		public float laneDegreesStep
		{
			get
			{
				return this._laneDegreesStep;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00011021 File Offset: 0x0000F221
		public float innerCircleRadius
		{
			get
			{
				return this._innerCircleRadius;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00011029 File Offset: 0x0000F229
		public int totalLanesCount
		{
			get
			{
				return Mathf.RoundToInt(360f / this.laneDegreesStep);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0001103C File Offset: 0x0000F23C
		public int sideLanesCount
		{
			get
			{
				return (this.totalLanesCount - 2) / 2;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00011048 File Offset: 0x0000F248
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00011050 File Offset: 0x0000F250
		public bool variablePlayheadSpeed
		{
			get
			{
				return this._variablePlayheadSpeed;
			}
			set
			{
				this._variablePlayheadSpeed = value;
			}
		}

		// Token: 0x04001709 RID: 5897
		[SerializeField]
		private GridLaneConfigSO _gridLaneConfig;

		// Token: 0x0400170A RID: 5898
		[SerializeField]
		private float _laneDegreesStep;

		// Token: 0x0400170B RID: 5899
		[SerializeField]
		private float _innerCircleRadius;

		// Token: 0x0400170C RID: 5900
		[SerializeField]
		private bool _variablePlayheadSpeed;
	}
}
