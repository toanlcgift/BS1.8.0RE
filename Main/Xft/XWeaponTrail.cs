using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xft
{
	// Token: 0x020004C7 RID: 1223
	public class XWeaponTrail : MonoBehaviour
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000108BC File Offset: 0x0000EABC
		public Vector3 curHeadPos
		{
			get
			{
				return (this._pointStart.position + this._pointEnd.position) / 2f;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x000108E3 File Offset: 0x0000EAE3
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x000108EB File Offset: 0x0000EAEB
		public virtual Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0005232C File Offset: 0x0005052C
		protected void Start()
		{
			this._elemPool = new XWeaponTrail.ElementPool(this._maxFrame);
			this._trailWidth = (this._pointStart.position - this._pointEnd.position).magnitude;
			this._trailRenderer = UnityEngine.Object.Instantiate<XWeaponTrailRenderer>(this._trailRendererPrefab, Vector3.zero, Quaternion.identity);
			this._vertexPool = new VertexPool(this._trailRenderer.mesh);
			this._vertexSegment = this._vertexPool.GetVertices(this._granularity * 3, (this._granularity - 1) * 12);
			this.UpdateIndices();
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000108F4 File Offset: 0x0000EAF4
		protected void OnEnable()
		{
			this._frameNum = 0;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000108FD File Offset: 0x0000EAFD
		protected void OnDisable()
		{
			if (this._trailRenderer)
			{
				this._trailRenderer.enabled = false;
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000523D0 File Offset: 0x000505D0
		protected void LateUpdate()
		{
			this._frameNum++;
			if (this._frameNum == this._skipFirstFrames + 1)
			{
				if (this._trailRenderer)
				{
					this._trailRenderer.enabled = true;
				}
				this._spline.Clear();
				for (int i = 0; i < this._maxFrame; i++)
				{
					this._spline.AddControlPoint(this.curHeadPos, this._pointStart.position - this._pointEnd.position);
				}
				this._snapshotList.Clear();
				this._snapshotList.Add(new XWeaponTrail.Element(this._pointStart.position, this._pointEnd.position));
				this._snapshotList.Add(new XWeaponTrail.Element(this._pointStart.position, this._pointEnd.position));
			}
			else if (this._frameNum < this._skipFirstFrames + 1)
			{
				return;
			}
			this.UpdateHeadElem();
			this.RecordCurElem();
			this.RefreshSpline();
			this.UpdateVertices();
			this._vertexPool.ManualUpdate(Time.deltaTime);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00010918 File Offset: 0x0000EB18
		protected void OnDestroy()
		{
			if (this._trailRenderer != null)
			{
				UnityEngine.Object.Destroy(this._trailRenderer.gameObject);
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000524F4 File Offset: 0x000506F4
		private void OnDrawGizmosSelected()
		{
			if (this._pointEnd == null || this._pointStart == null)
			{
				return;
			}
			float magnitude = (this._pointStart.position - this._pointEnd.position).magnitude;
			if (magnitude < Mathf.Epsilon)
			{
				return;
			}
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(this._pointStart.position, magnitude * 0.04f);
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(this._pointEnd.position, magnitude * 0.04f);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00052590 File Offset: 0x00050790
		private void RefreshSpline()
		{
			for (int i = 0; i < this._snapshotList.Count; i++)
			{
				this._spline.ControlPoints[i].Position = this._snapshotList[i].pos;
				this._spline.ControlPoints[i].Normal = this._snapshotList[i].pointEnd - this._snapshotList[i].pointStart;
			}
			this._spline.RefreshSpline();
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00052624 File Offset: 0x00050824
		protected void UpdateVertices()
		{
			VertexPool pool = this._vertexSegment.Pool;
			Color color = this.color;
			for (int i = 0; i < this._granularity; i++)
			{
				int num = this._vertexSegment.VertStart + i * 3;
				float num2 = (float)i / (float)this._granularity;
				float tl = num2;
				Vector2 zero = Vector2.zero;
				Vector3 vector = this._spline.InterpolateByLen(tl);
				Vector3 vector2 = this._spline.InterpolateNormalByLen(tl);
				Vector3 vector3 = vector + vector2.normalized * this._trailWidth * 0.5f;
				Vector3 vector4 = vector - vector2.normalized * this._trailWidth * 0.5f;
				if (num < this._whiteSteps)
				{
					color = Color.white;
				}
				else
				{
					color = this.color;
				}
				pool.vertices[num] = vector3;
				pool.colors[num] = color;
				zero.x = 0f;
				zero.y = num2;
				pool.uvs[num] = zero;
				pool.vertices[num + 1] = vector;
				pool.colors[num + 1] = color;
				zero.x = 0.5f;
				zero.y = num2;
				pool.uvs[num + 1] = zero;
				pool.vertices[num + 2] = vector4;
				pool.colors[num + 2] = color;
				zero.x = 1f;
				zero.y = num2;
				pool.uvs[num + 2] = zero;
			}
			this._vertexSegment.Pool.uvChanged = true;
			this._vertexSegment.Pool.vertChanged = true;
			this._vertexSegment.Pool.colorChanged = true;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000527FC File Offset: 0x000509FC
		protected void UpdateIndices()
		{
			VertexPool pool = this._vertexSegment.Pool;
			for (int i = 0; i < this._granularity - 1; i++)
			{
				int num = this._vertexSegment.VertStart + i * 3;
				int num2 = this._vertexSegment.VertStart + (i + 1) * 3;
				int num3 = this._vertexSegment.IndexStart + i * 12;
				pool.indices[num3] = num2;
				pool.indices[num3 + 1] = num2 + 1;
				pool.indices[num3 + 2] = num;
				pool.indices[num3 + 3] = num2 + 1;
				pool.indices[num3 + 4] = num + 1;
				pool.indices[num3 + 5] = num;
				pool.indices[num3 + 6] = num2 + 1;
				pool.indices[num3 + 7] = num2 + 2;
				pool.indices[num3 + 8] = num + 1;
				pool.indices[num3 + 9] = num2 + 2;
				pool.indices[num3 + 10] = num + 2;
				pool.indices[num3 + 11] = num + 1;
			}
			pool.indiceChanged = true;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00010938 File Offset: 0x0000EB38
		protected void UpdateHeadElem()
		{
			this._snapshotList[0].pointStart = this._pointStart.position;
			this._snapshotList[0].pointEnd = this._pointEnd.position;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005290C File Offset: 0x00050B0C
		private void RecordCurElem()
		{
			XWeaponTrail.Element element = this._elemPool.Get();
			element.pointStart = this._pointStart.position;
			element.pointEnd = this._pointEnd.position;
			if (this._snapshotList.Count < this._maxFrame)
			{
				this._snapshotList.Insert(1, element);
				return;
			}
			this._elemPool.Release(this._snapshotList[this._snapshotList.Count - 1]);
			this._snapshotList.RemoveAt(this._snapshotList.Count - 1);
			this._snapshotList.Insert(1, element);
		}

		// Token: 0x040016A8 RID: 5800
		[SerializeField]
		private XWeaponTrailRenderer _trailRendererPrefab;

		// Token: 0x040016A9 RID: 5801
		[SerializeField]
		private Transform _pointStart;

		// Token: 0x040016AA RID: 5802
		[SerializeField]
		private Transform _pointEnd;

		// Token: 0x040016AB RID: 5803
		[SerializeField]
		private int _maxFrame = 20;

		// Token: 0x040016AC RID: 5804
		[SerializeField]
		private int _granularity = 60;

		// Token: 0x040016AD RID: 5805
		[SerializeField]
		private int _whiteSteps = 4;

		// Token: 0x040016AE RID: 5806
		[SerializeField]
		private Color _color = Color.white;

		// Token: 0x040016AF RID: 5807
		[SerializeField]
		private int _skipFirstFrames = 4;

		// Token: 0x040016B0 RID: 5808
		private float _trailWidth;

		// Token: 0x040016B1 RID: 5809
		private List<XWeaponTrail.Element> _snapshotList = new List<XWeaponTrail.Element>();

		// Token: 0x040016B2 RID: 5810
		private XWeaponTrail.ElementPool _elemPool;

		// Token: 0x040016B3 RID: 5811
		private Spline _spline = new Spline();

		// Token: 0x040016B4 RID: 5812
		private VertexPool _vertexPool;

		// Token: 0x040016B5 RID: 5813
		private VertexPool.VertexSegment _vertexSegment;

		// Token: 0x040016B6 RID: 5814
		private XWeaponTrailRenderer _trailRenderer;

		// Token: 0x040016B7 RID: 5815
		private int _frameNum;

		// Token: 0x020004C8 RID: 1224
		public class Element
		{
			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x06001678 RID: 5752 RVA: 0x00010972 File Offset: 0x0000EB72
			public Vector3 pos
			{
				get
				{
					return (this.pointStart + this.pointEnd) * 0.5f;
				}
			}

			// Token: 0x06001679 RID: 5753 RVA: 0x0001098F File Offset: 0x0000EB8F
			public Element(Vector3 start, Vector3 end)
			{
				this.pointStart = start;
				this.pointEnd = end;
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x00002198 File Offset: 0x00000398
			public Element()
			{
			}

			// Token: 0x040016B8 RID: 5816
			public Vector3 pointStart;

			// Token: 0x040016B9 RID: 5817
			public Vector3 pointEnd;
		}

		// Token: 0x020004C9 RID: 1225
		public class ElementPool
		{
			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x0600167B RID: 5755 RVA: 0x000109A5 File Offset: 0x0000EBA5
			// (set) Token: 0x0600167C RID: 5756 RVA: 0x000109AD File Offset: 0x0000EBAD
			public int CountAll { get; private set; }

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x0600167D RID: 5757 RVA: 0x000109B6 File Offset: 0x0000EBB6
			public int CountActive
			{
				get
				{
					return this.CountAll - this.CountInactive;
				}
			}

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x0600167E RID: 5758 RVA: 0x000109C5 File Offset: 0x0000EBC5
			public int CountInactive
			{
				get
				{
					return this._stack.Count;
				}
			}

			// Token: 0x0600167F RID: 5759 RVA: 0x00052A04 File Offset: 0x00050C04
			public ElementPool(int preCount)
			{
				for (int i = 0; i < preCount; i++)
				{
					XWeaponTrail.Element item = new XWeaponTrail.Element();
					this._stack.Push(item);
					int countAll = this.CountAll;
					this.CountAll = countAll + 1;
				}
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x00052A50 File Offset: 0x00050C50
			public XWeaponTrail.Element Get()
			{
				XWeaponTrail.Element result;
				if (this._stack.Count == 0)
				{
					result = new XWeaponTrail.Element();
					int countAll = this.CountAll;
					this.CountAll = countAll + 1;
				}
				else
				{
					result = this._stack.Pop();
				}
				return result;
			}

			// Token: 0x06001681 RID: 5761 RVA: 0x000109D2 File Offset: 0x0000EBD2
			public void Release(XWeaponTrail.Element element)
			{
				if (this._stack.Count > 0 && this._stack.Peek() == element)
				{
					Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
				}
				this._stack.Push(element);
			}

			// Token: 0x040016BA RID: 5818
			private readonly Stack<XWeaponTrail.Element> _stack = new Stack<XWeaponTrail.Element>();
		}
	}
}
