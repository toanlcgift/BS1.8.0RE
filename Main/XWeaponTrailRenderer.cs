using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000010 RID: 16
public class XWeaponTrailRenderer : MonoBehaviour
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000041 RID: 65 RVA: 0x0000235B File Offset: 0x0000055B
	public Mesh mesh
	{
		get
		{
			if (this._mesh == null)
			{
				this._mesh = new Mesh();
				this._meshFilter.mesh = this._mesh;
			}
			return this._mesh;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0000238D File Offset: 0x0000058D
	protected void OnDestroy()
	{
		EssentialHelpers.SafeDestroy(this._mesh);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00015310 File Offset: 0x00013510
	private void OnValidate()
	{
		Scene scene = base.gameObject.scene;
		if (this._meshFilter == null)
		{
			this._meshFilter = base.GetComponent<MeshFilter>();
		}
		if (this._meshRenderer == null)
		{
			this._meshRenderer = base.GetComponent<MeshRenderer>();
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000239A File Offset: 0x0000059A
	protected void OnEnable()
	{
		this._meshRenderer.enabled = true;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000023A8 File Offset: 0x000005A8
	protected void OnDisable()
	{
		if (this._meshRenderer)
		{
			this._meshRenderer.enabled = false;
		}
	}

	// Token: 0x0400003E RID: 62
	[SerializeField]
	private MeshRenderer _meshRenderer;

	// Token: 0x0400003F RID: 63
	[SerializeField]
	private MeshFilter _meshFilter;

	// Token: 0x04000040 RID: 64
	private Mesh _mesh;
}
