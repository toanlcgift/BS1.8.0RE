using System;
using UnityEngine;

namespace Xft
{
	// Token: 0x020004C5 RID: 1221
	public class VertexPool
	{
		// Token: 0x06001663 RID: 5731 RVA: 0x00051FF8 File Offset: 0x000501F8
		public VertexPool(Mesh mesh)
		{
			this._mesh = mesh;
			this._mesh.MarkDynamic();
			this._vertexTotal = (this._vertexUsed = 0);
			this._vertCountChanged = false;
			this.vertices = new Vector3[4];
			this.uvs = new Vector2[4];
			this.colors = new Color[4];
			this.indices = new int[6];
			this._vertexTotal = 4;
			this._indexTotal = 6;
			this.indiceChanged = (this.colorChanged = (this.uvChanged = (this.uv2Changed = (this.vertChanged = true))));
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000520A0 File Offset: 0x000502A0
		public VertexPool.VertexSegment GetVertices(int vcount, int icount)
		{
			int num = 0;
			int num2 = 0;
			if (this._vertexUsed + vcount >= this._vertexTotal)
			{
				num = (vcount / 108 + 1) * 108;
			}
			if (this._indexUsed + icount >= this._indexTotal)
			{
				num2 = (icount / 108 + 1) * 108;
			}
			this._vertexUsed += vcount;
			this._indexUsed += icount;
			if (num != 0 || num2 != 0)
			{
				this.EnlargeArrays(num, num2);
				this._vertexTotal += num;
				this._indexTotal += num2;
			}
			return new VertexPool.VertexSegment(this._vertexUsed - vcount, vcount, this._indexUsed - icount, icount, this);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00052144 File Offset: 0x00050344
		public void EnlargeArrays(int count, int icount)
		{
			Array array = this.vertices;
			this.vertices = new Vector3[this.vertices.Length + count];
			array.CopyTo(this.vertices, 0);
			Array array2 = this.uvs;
			this.uvs = new Vector2[this.uvs.Length + count];
			array2.CopyTo(this.uvs, 0);
			Array array3 = this.colors;
			this.colors = new Color[this.colors.Length + count];
			array3.CopyTo(this.colors, 0);
			Array array4 = this.indices;
			this.indices = new int[this.indices.Length + icount];
			array4.CopyTo(this.indices, 0);
			this._vertCountChanged = true;
			this.indiceChanged = true;
			this.colorChanged = true;
			this.uvChanged = true;
			this.vertChanged = true;
			this.uv2Changed = true;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00052218 File Offset: 0x00050418
		public void ManualUpdate(float deltaTime)
		{
			if (this._vertCountChanged)
			{
				this._mesh.Clear();
			}
			this._mesh.vertices = this.vertices;
			if (this.uvChanged)
			{
				this._mesh.uv = this.uvs;
			}
			if (this.colorChanged)
			{
				this._mesh.colors = this.colors;
			}
			if (this.indiceChanged)
			{
				this._mesh.triangles = this.indices;
				this._mesh.bounds = new Bounds(Vector3.zero, new Vector3(100f, 100f, 100f));
			}
			this._vertCountChanged = false;
			this.indiceChanged = false;
			this.colorChanged = false;
			this.uvChanged = false;
			this.uv2Changed = false;
			this.vertChanged = false;
		}

		// Token: 0x04001693 RID: 5779
		public Vector3[] vertices;

		// Token: 0x04001694 RID: 5780
		public int[] indices;

		// Token: 0x04001695 RID: 5781
		public Vector2[] uvs;

		// Token: 0x04001696 RID: 5782
		public Color[] colors;

		// Token: 0x04001697 RID: 5783
		public bool indiceChanged;

		// Token: 0x04001698 RID: 5784
		public bool colorChanged;

		// Token: 0x04001699 RID: 5785
		public bool uvChanged;

		// Token: 0x0400169A RID: 5786
		public bool vertChanged;

		// Token: 0x0400169B RID: 5787
		public bool uv2Changed;

		// Token: 0x0400169C RID: 5788
		private const int _blockSize = 108;

		// Token: 0x0400169D RID: 5789
		private int _vertexTotal;

		// Token: 0x0400169E RID: 5790
		private int _vertexUsed;

		// Token: 0x0400169F RID: 5791
		private int _indexTotal;

		// Token: 0x040016A0 RID: 5792
		private int _indexUsed;

		// Token: 0x040016A1 RID: 5793
		private bool _vertCountChanged;

		// Token: 0x040016A2 RID: 5794
		private Mesh _mesh;

		// Token: 0x020004C6 RID: 1222
		public class VertexSegment
		{
			// Token: 0x06001667 RID: 5735 RVA: 0x0001088F File Offset: 0x0000EA8F
			public VertexSegment(int start, int count, int istart, int icount, VertexPool pool)
			{
				this.VertStart = start;
				this.VertCount = count;
				this.IndexCount = icount;
				this.IndexStart = istart;
				this.Pool = pool;
			}

			// Token: 0x06001668 RID: 5736 RVA: 0x000522E8 File Offset: 0x000504E8
			public void ClearIndices()
			{
				for (int i = this.IndexStart; i < this.IndexStart + this.IndexCount; i++)
				{
					this.Pool.indices[i] = 0;
				}
				this.Pool.indiceChanged = true;
			}

			// Token: 0x040016A3 RID: 5795
			public int VertStart;

			// Token: 0x040016A4 RID: 5796
			public int IndexStart;

			// Token: 0x040016A5 RID: 5797
			public int VertCount;

			// Token: 0x040016A6 RID: 5798
			public int IndexCount;

			// Token: 0x040016A7 RID: 5799
			public VertexPool Pool;
		}
	}
}
