using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class StretchableCube : MonoBehaviour
{
	// Token: 0x06000A20 RID: 2592 RVA: 0x00007DBC File Offset: 0x00005FBC
	protected void Awake()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		this._mesh = this.CreateBox();
		component.mesh = this._mesh;
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00007DDB File Offset: 0x00005FDB
	protected void OnDestroy()
	{
		EssentialHelpers.SafeDestroy(this._mesh);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0002FA88 File Offset: 0x0002DC88
	private Mesh CreateBox()
	{
		Mesh mesh = new Mesh();
		mesh.name = "StretchableCube";
		this._uvs = new Vector2[24];
		this.RecalculateUVs(this._uvs);
		mesh.vertices = StretchableCube.vertices;
		mesh.normals = StretchableCube.normals;
		mesh.uv = this._uvs;
		mesh.triangles = StretchableCube.triangles;
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
	private void RecalculateUVs(Vector2[] uvs)
	{
		Vector3 lossyScale = base.transform.lossyScale;
		Vector2[] array = new Vector2[]
		{
			new Vector2(lossyScale.x, lossyScale.z),
			new Vector2(lossyScale.z, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.y),
			new Vector2(lossyScale.z, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.z)
		};
		for (int i = 0; i < array.Length; i++)
		{
			uvs[i * 4] = array[i];
			uvs[i * 4 + 1] = new Vector2(0f, array[i].y);
			uvs[i * 4 + 2] = Vector2.zero;
			uvs[i * 4 + 3] = new Vector2(array[i].x, 0f);
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00007DE8 File Offset: 0x00005FE8
	public void RefreshUVs()
	{
		if (this._mesh == null)
		{
			return;
		}
		this.RecalculateUVs(this._uvs);
		this._mesh.uv = this._uvs;
	}

	// Token: 0x04000A55 RID: 2645
	private const float kLength = 1f;

	// Token: 0x04000A56 RID: 2646
	private const float kWidth = 1f;

	// Token: 0x04000A57 RID: 2647
	private const float kHeight = 1f;

	// Token: 0x04000A58 RID: 2648
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p0 = new Vector3(-0.5f, -0.5f, 0.5f);

	// Token: 0x04000A59 RID: 2649
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p1 = new Vector3(0.5f, -0.5f, 0.5f);

	// Token: 0x04000A5A RID: 2650
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p2 = new Vector3(0.5f, -0.5f, -0.5f);

	// Token: 0x04000A5B RID: 2651
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p3 = new Vector3(-0.5f, -0.5f, -0.5f);

	// Token: 0x04000A5C RID: 2652
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p4 = new Vector3(-0.5f, 0.5f, 0.5f);

	// Token: 0x04000A5D RID: 2653
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p5 = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x04000A5E RID: 2654
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p6 = new Vector3(0.5f, 0.5f, -0.5f);

	// Token: 0x04000A5F RID: 2655
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 p7 = new Vector3(-0.5f, 0.5f, -0.5f);

	// Token: 0x04000A60 RID: 2656
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3[] vertices = new Vector3[]
	{
		StretchableCube.p0,
		StretchableCube.p1,
		StretchableCube.p2,
		StretchableCube.p3,
		StretchableCube.p7,
		StretchableCube.p4,
		StretchableCube.p0,
		StretchableCube.p3,
		StretchableCube.p4,
		StretchableCube.p5,
		StretchableCube.p1,
		StretchableCube.p0,
		StretchableCube.p6,
		StretchableCube.p7,
		StretchableCube.p3,
		StretchableCube.p2,
		StretchableCube.p5,
		StretchableCube.p6,
		StretchableCube.p2,
		StretchableCube.p1,
		StretchableCube.p7,
		StretchableCube.p6,
		StretchableCube.p5,
		StretchableCube.p4
	};

	// Token: 0x04000A61 RID: 2657
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 up = Vector3.up;

	// Token: 0x04000A62 RID: 2658
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 down = Vector3.down;

	// Token: 0x04000A63 RID: 2659
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 front = Vector3.forward;

	// Token: 0x04000A64 RID: 2660
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 back = Vector3.back;

	// Token: 0x04000A65 RID: 2661
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 left = Vector3.left;

	// Token: 0x04000A66 RID: 2662
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3 right = Vector3.right;

	// Token: 0x04000A67 RID: 2663
	[DoesNotRequireDomainReloadInit]
	private static readonly Vector3[] normals = new Vector3[]
	{
		StretchableCube.down,
		StretchableCube.down,
		StretchableCube.down,
		StretchableCube.down,
		StretchableCube.left,
		StretchableCube.left,
		StretchableCube.left,
		StretchableCube.left,
		StretchableCube.front,
		StretchableCube.front,
		StretchableCube.front,
		StretchableCube.front,
		StretchableCube.back,
		StretchableCube.back,
		StretchableCube.back,
		StretchableCube.back,
		StretchableCube.right,
		StretchableCube.right,
		StretchableCube.right,
		StretchableCube.right,
		StretchableCube.up,
		StretchableCube.up,
		StretchableCube.up,
		StretchableCube.up
	};

	// Token: 0x04000A68 RID: 2664
	[DoesNotRequireDomainReloadInit]
	private static readonly int[] triangles = new int[]
	{
		3,
		1,
		0,
		3,
		2,
		1,
		7,
		5,
		4,
		7,
		6,
		5,
		11,
		9,
		8,
		11,
		10,
		9,
		15,
		13,
		12,
		15,
		14,
		13,
		19,
		17,
		16,
		19,
		18,
		17,
		23,
		21,
		20,
		23,
		22,
		21
	};

	// Token: 0x04000A69 RID: 2665
	private Vector2[] _uvs;

	// Token: 0x04000A6A RID: 2666
	private Mesh _mesh;
}
