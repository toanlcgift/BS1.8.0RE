using System;
using UnityEngine;

namespace Xft
{
	// Token: 0x020004C4 RID: 1220
	public class SplineControlPoint
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x000107BA File Offset: 0x0000E9BA
		public SplineControlPoint NextControlPoint
		{
			get
			{
				return this.mSpline.NextControlPoint(this);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public SplineControlPoint PreviousControlPoint
		{
			get
			{
				return this.mSpline.PreviousControlPoint(this);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x000107D6 File Offset: 0x0000E9D6
		public Vector3 NextPosition
		{
			get
			{
				return this.mSpline.NextPosition(this);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public Vector3 PreviousPosition
		{
			get
			{
				return this.mSpline.PreviousPosition(this);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x000107F2 File Offset: 0x0000E9F2
		public Vector3 NextNormal
		{
			get
			{
				return this.mSpline.NextNormal(this);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00010800 File Offset: 0x0000EA00
		public Vector3 PreviousNormal
		{
			get
			{
				return this.mSpline.PreviousNormal(this);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0001080E File Offset: 0x0000EA0E
		public bool IsValid
		{
			get
			{
				return this.NextControlPoint != null;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00051FB0 File Offset: 0x000501B0
		private Vector3 GetNext2Position()
		{
			SplineControlPoint nextControlPoint = this.NextControlPoint;
			if (nextControlPoint != null)
			{
				return nextControlPoint.NextPosition;
			}
			return this.NextPosition;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00051FD4 File Offset: 0x000501D4
		private Vector3 GetNext2Normal()
		{
			SplineControlPoint nextControlPoint = this.NextControlPoint;
			if (nextControlPoint != null)
			{
				return nextControlPoint.NextNormal;
			}
			return this.Normal;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00010819 File Offset: 0x0000EA19
		public Vector3 Interpolate(float localF)
		{
			localF = Mathf.Clamp01(localF);
			return Spline.CatmulRom(this.PreviousPosition, this.Position, this.NextPosition, this.GetNext2Position(), localF);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00010841 File Offset: 0x0000EA41
		public Vector3 InterpolateNormal(float localF)
		{
			localF = Mathf.Clamp01(localF);
			return Spline.CatmulRom(this.PreviousNormal, this.Normal, this.NextNormal, this.GetNext2Normal(), localF);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00010869 File Offset: 0x0000EA69
		public void Init(Spline owner)
		{
			this.mSpline = owner;
			this.SegmentIndex = -1;
		}

		// Token: 0x0400168D RID: 5773
		public Vector3 Position;

		// Token: 0x0400168E RID: 5774
		public Vector3 Normal;

		// Token: 0x0400168F RID: 5775
		public int ControlPointIndex = -1;

		// Token: 0x04001690 RID: 5776
		public int SegmentIndex = -1;

		// Token: 0x04001691 RID: 5777
		public float Dist;

		// Token: 0x04001692 RID: 5778
		protected Spline mSpline;
	}
}
