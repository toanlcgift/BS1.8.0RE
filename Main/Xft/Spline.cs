using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xft
{
	// Token: 0x020004C3 RID: 1219
	public class Spline
	{
		// Token: 0x17000414 RID: 1044
		public SplineControlPoint this[int index]
		{
			get
			{
				if (index > -1 && index < this.mSegments.Count)
				{
					return this.mSegments[index];
				}
				return null;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0001077F File Offset: 0x0000E97F
		public List<SplineControlPoint> Segments
		{
			get
			{
				return this.mSegments;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00010787 File Offset: 0x0000E987
		public List<SplineControlPoint> ControlPoints
		{
			get
			{
				return this.mControlPoints;
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00051A74 File Offset: 0x0004FC74
		public SplineControlPoint NextControlPoint(SplineControlPoint controlpoint)
		{
			if (this.mControlPoints.Count == 0)
			{
				return null;
			}
			int num = controlpoint.ControlPointIndex + 1;
			if (num >= this.mControlPoints.Count)
			{
				return null;
			}
			return this.mControlPoints[num];
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00051AB8 File Offset: 0x0004FCB8
		public SplineControlPoint PreviousControlPoint(SplineControlPoint controlpoint)
		{
			if (this.mControlPoints.Count == 0)
			{
				return null;
			}
			int num = controlpoint.ControlPointIndex - 1;
			if (num < 0)
			{
				return null;
			}
			return this.mControlPoints[num];
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00051AF0 File Offset: 0x0004FCF0
		public Vector3 NextPosition(SplineControlPoint controlpoint)
		{
			SplineControlPoint splineControlPoint = this.NextControlPoint(controlpoint);
			if (splineControlPoint != null)
			{
				return splineControlPoint.Position;
			}
			return controlpoint.Position;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00051B18 File Offset: 0x0004FD18
		public Vector3 PreviousPosition(SplineControlPoint controlpoint)
		{
			SplineControlPoint splineControlPoint = this.PreviousControlPoint(controlpoint);
			if (splineControlPoint != null)
			{
				return splineControlPoint.Position;
			}
			return controlpoint.Position;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00051B40 File Offset: 0x0004FD40
		public Vector3 PreviousNormal(SplineControlPoint controlpoint)
		{
			SplineControlPoint splineControlPoint = this.PreviousControlPoint(controlpoint);
			if (splineControlPoint != null)
			{
				return splineControlPoint.Normal;
			}
			return controlpoint.Normal;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00051B68 File Offset: 0x0004FD68
		public Vector3 NextNormal(SplineControlPoint controlpoint)
		{
			SplineControlPoint splineControlPoint = this.NextControlPoint(controlpoint);
			if (splineControlPoint != null)
			{
				return splineControlPoint.Normal;
			}
			return controlpoint.Normal;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00051B90 File Offset: 0x0004FD90
		public SplineControlPoint LenToSegment(float t, out float localF)
		{
			SplineControlPoint splineControlPoint = null;
			t = Mathf.Clamp01(t);
			float num = t * this.mSegments[this.mSegments.Count - 1].Dist;
			int i;
			for (i = 0; i < this.mSegments.Count; i++)
			{
				if (this.mSegments[i].Dist >= num)
				{
					splineControlPoint = this.mSegments[i];
					break;
				}
			}
			if (i == 0)
			{
				localF = 0f;
				return splineControlPoint;
			}
			int index = splineControlPoint.SegmentIndex - 1;
			SplineControlPoint splineControlPoint2 = this.mSegments[index];
			float num2 = splineControlPoint.Dist - splineControlPoint2.Dist;
			localF = (num - splineControlPoint2.Dist) / num2;
			return splineControlPoint2;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00051C4C File Offset: 0x0004FE4C
		public static Vector3 CatmulRom(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
		{
			float num = -0.5f;
			float num2 = 1.5f;
			float num3 = -1.5f;
			float num4 = 0.5f;
			float num5 = -2.5f;
			float num6 = 2f;
			float num7 = -0.5f;
			float num8 = -0.5f;
			float num9 = 0.5f;
			float num10 = num * T0.x + num2 * P0.x + num3 * P1.x + num4 * T1.x;
			float num11 = T0.x + num5 * P0.x + num6 * P1.x + num7 * T1.x;
			float num12 = num8 * T0.x + num9 * P1.x;
			float x = P0.x;
			float num13 = num * T0.y + num2 * P0.y + num3 * P1.y + num4 * T1.y;
			float num14 = T0.y + num5 * P0.y + num6 * P1.y + num7 * T1.y;
			float num15 = num8 * T0.y + num9 * P1.y;
			float y = P0.y;
			float num16 = num * T0.z + num2 * P0.z + num3 * P1.z + num4 * T1.z;
			float num17 = T0.z + num5 * P0.z + num6 * P1.z + num7 * T1.z;
			float num18 = num8 * T0.z + num9 * P1.z;
			float z = P0.z;
			float x2 = ((num10 * f + num11) * f + num12) * f + x;
			float y2 = ((num13 * f + num14) * f + num15) * f + y;
			float z2 = ((num16 * f + num17) * f + num18) * f + z;
			return new Vector3(x2, y2, z2);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00051E14 File Offset: 0x00050014
		public Vector3 InterpolateByLen(float tl)
		{
			float localF;
			return this.LenToSegment(tl, out localF).Interpolate(localF);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00051E30 File Offset: 0x00050030
		public Vector3 InterpolateNormalByLen(float tl)
		{
			float localF;
			return this.LenToSegment(tl, out localF).InterpolateNormal(localF);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00051E4C File Offset: 0x0005004C
		public SplineControlPoint AddControlPoint(Vector3 pos, Vector3 up)
		{
			SplineControlPoint splineControlPoint = new SplineControlPoint();
			splineControlPoint.Init(this);
			splineControlPoint.Position = pos;
			splineControlPoint.Normal = up;
			this.mControlPoints.Add(splineControlPoint);
			splineControlPoint.ControlPointIndex = this.mControlPoints.Count - 1;
			return splineControlPoint;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0001078F File Offset: 0x0000E98F
		public void Clear()
		{
			this.mControlPoints.Clear();
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00051E94 File Offset: 0x00050094
		private void RefreshDistance()
		{
			if (this.mSegments.Count < 1)
			{
				return;
			}
			this.mSegments[0].Dist = 0f;
			for (int i = 1; i < this.mSegments.Count; i++)
			{
				float magnitude = (this.mSegments[i].Position - this.mSegments[i - 1].Position).magnitude;
				this.mSegments[i].Dist = this.mSegments[i - 1].Dist + magnitude;
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00051F34 File Offset: 0x00050134
		public void RefreshSpline()
		{
			this.mSegments.Clear();
			for (int i = 0; i < this.mControlPoints.Count; i++)
			{
				if (this.mControlPoints[i].IsValid)
				{
					this.mSegments.Add(this.mControlPoints[i]);
					this.mControlPoints[i].SegmentIndex = this.mSegments.Count - 1;
				}
			}
			this.RefreshDistance();
		}

		// Token: 0x0400168B RID: 5771
		private List<SplineControlPoint> mControlPoints = new List<SplineControlPoint>();

		// Token: 0x0400168C RID: 5772
		private List<SplineControlPoint> mSegments = new List<SplineControlPoint>();
	}
}
