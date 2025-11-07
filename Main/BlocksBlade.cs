using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020002F9 RID: 761
public class BlocksBlade : MonoBehaviour
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0000A242 File Offset: 0x00008442
	// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0000A24A File Offset: 0x0000844A
	public Color color { get; set; }

	// Token: 0x06000D0C RID: 3340 RVA: 0x00037AC8 File Offset: 0x00035CC8
	protected void Start()
	{
		this._layer = base.gameObject.layer;
		this._positions = new Vector4[this._numberOfElements];
		this._sizes = new Vector4[this._numberOfElements];
		this._matrices = new Matrix4x4[this._numberOfElements];
		this._colors = new Vector4[this._numberOfElements];
		this._elements = new List<BlocksBlade.Element>(this._numberOfElements);
		Color color = this.color;
		for (int i = 0; i < this._numberOfElements; i++)
		{
			BlocksBlade.Element element = new BlocksBlade.Element();
			element.idx = i;
			this.SetUpElement(element, UnityEngine.Random.Range(this._minVelocity, this._maxVelocity), color);
			this._elements.Add(element);
		}
		this._materialPropertyBlock = new MaterialPropertyBlock();
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x00037B90 File Offset: 0x00035D90
	protected void Update()
	{
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Color color = this.color;
		foreach (BlocksBlade.Element element in this._elements)
		{
			Vector4[] positions = this._positions;
			int idx = element.idx;
			positions[idx].z = positions[idx].z + element.velocity * Time.deltaTime;
			Vector4 vector = this._positions[element.idx];
			Vector4 vector2 = this._sizes[element.idx];
			if (element.velocity > 0f && vector.z - vector2.z * 0.5f > this._length)
			{
				this.SetUpElement(element, UnityEngine.Random.Range(this._minVelocity, this._maxVelocity), color);
			}
			this._matrices[element.idx] = localToWorldMatrix;
		}
		this._materialPropertyBlock.SetVectorArray(BlocksBlade._positionPropertyID, this._positions);
		this._materialPropertyBlock.SetVectorArray(BlocksBlade._sizePropertyID, this._sizes);
		this._materialPropertyBlock.SetVectorArray(BlocksBlade._colorPropertyID, this._colors);
		this._materialPropertyBlock.SetFloat(BlocksBlade._zClipPropertyID, this._length);
		Graphics.DrawMeshInstanced(this._elementMesh, 0, this._material, this._matrices, this._matrices.Length, this._materialPropertyBlock, ShadowCastingMode.Off, false, this._layer);
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x00037D1C File Offset: 0x00035F1C
	private void SetUpElement(BlocksBlade.Element element, float velocity, Color color)
	{
		Vector4 vector = new Vector4(this._elementWidth, this._elementWidth, UnityEngine.Random.Range(this._minElementLength, this._maxElementLength), 1f);
		this._sizes[element.idx] = vector;
		this._positions[element.idx] = this.RandomPointOnCircle(this._radius - this._elementWidth * 0.5f);
		this._positions[element.idx].z = -this._sizes[element.idx].z * 0.5f;
		element.velocity = velocity;
		color.a = 1f;
		this._colors[element.idx] = color;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00037DF0 File Offset: 0x00035FF0
	private Vector2 RandomPointOnCircle(float radius)
	{
		float f = UnityEngine.Random.Range(0f, 6.2831855f);
		float x = Mathf.Sin(f) * radius;
		float y = Mathf.Cos(f) * radius;
		return new Vector2(x, y);
	}

	// Token: 0x04000D74 RID: 3444
	[SerializeField]
	private Mesh _elementMesh;

	// Token: 0x04000D75 RID: 3445
	[SerializeField]
	private Material _material;

	// Token: 0x04000D76 RID: 3446
	[SerializeField]
	private int _numberOfElements = 25;

	// Token: 0x04000D77 RID: 3447
	[SerializeField]
	private float _radius = 0.3f;

	// Token: 0x04000D78 RID: 3448
	[SerializeField]
	private float _length = 1f;

	// Token: 0x04000D79 RID: 3449
	[SerializeField]
	private float _minVelocity = 3f;

	// Token: 0x04000D7A RID: 3450
	[SerializeField]
	private float _maxVelocity = 5f;

	// Token: 0x04000D7B RID: 3451
	[SerializeField]
	private float _elementWidth = 0.01f;

	// Token: 0x04000D7C RID: 3452
	[SerializeField]
	private float _minElementLength = 0.1f;

	// Token: 0x04000D7D RID: 3453
	[SerializeField]
	private float _maxElementLength = 0.5f;

	// Token: 0x04000D7F RID: 3455
	private List<BlocksBlade.Element> _elements;

	// Token: 0x04000D80 RID: 3456
	private Vector4[] _positions;

	// Token: 0x04000D81 RID: 3457
	private Vector4[] _sizes;

	// Token: 0x04000D82 RID: 3458
	private Vector4[] _colors;

	// Token: 0x04000D83 RID: 3459
	private Matrix4x4[] _matrices;

	// Token: 0x04000D84 RID: 3460
	private MaterialPropertyBlock _materialPropertyBlock;

	// Token: 0x04000D85 RID: 3461
	[DoesNotRequireDomainReloadInit]
	private static readonly int _positionPropertyID = Shader.PropertyToID("_Position");

	// Token: 0x04000D86 RID: 3462
	[DoesNotRequireDomainReloadInit]
	private static readonly int _sizePropertyID = Shader.PropertyToID("_Size");

	// Token: 0x04000D87 RID: 3463
	[DoesNotRequireDomainReloadInit]
	private static readonly int _colorPropertyID = Shader.PropertyToID("_Color");

	// Token: 0x04000D88 RID: 3464
	[DoesNotRequireDomainReloadInit]
	private static readonly int _zClipPropertyID = Shader.PropertyToID("_ZClip");

	// Token: 0x04000D89 RID: 3465
	private int _layer;

	// Token: 0x020002FA RID: 762
	private class Element
	{
		// Token: 0x04000D8A RID: 3466
		public int idx;

		// Token: 0x04000D8B RID: 3467
		public float velocity;
	}
}
