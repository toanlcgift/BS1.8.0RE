using System;
using UnityEngine;

// Token: 0x02000298 RID: 664
[ExecuteInEditMode]
public class RectangleFakeGlow : MonoBehaviour
{
	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00008CA9 File Offset: 0x00006EA9
	// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00008C7A File Offset: 0x00006E7A
	public Color color
	{
		get
		{
			return this._color;
		}
		set
		{
			this._color = value;
			this._materialPropertyBlockController.materialPropertyBlock.SetColor(RectangleFakeGlow._colorID, this._color);
			this._materialPropertyBlockController.ApplyChanges();
		}
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00033DA4 File Offset: 0x00031FA4
	protected void Awake()
	{
		Renderer[] renderers = this._materialPropertyBlockController.renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00033DD4 File Offset: 0x00031FD4
	protected void OnEnable()
	{
		this.Refresh();
		Renderer[] renderers = this._materialPropertyBlockController.renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = true;
		}
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00033DA4 File Offset: 0x00031FA4
	protected void OnDisable()
	{
		Renderer[] renderers = this._materialPropertyBlockController.renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00033E0C File Offset: 0x0003200C
	public void Refresh()
	{
		Vector4 vector = new Vector4(this._size.x * 0.5f, this._size.y * 0.5f, 1f, this._edgeSize * 0.5f);
		base.transform.localScale = vector;
		MaterialPropertyBlock materialPropertyBlock = this._materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.SetColor(RectangleFakeGlow._colorID, this._color);
		materialPropertyBlock.SetVector(RectangleFakeGlow._sizeParamsID, vector);
		this._materialPropertyBlockController.ApplyChanges();
	}

	// Token: 0x04000BD7 RID: 3031
	[SerializeField]
	private Vector2 _size = new Vector2(1f, 1f);

	// Token: 0x04000BD8 RID: 3032
	[SerializeField]
	private float _edgeSize = 0.1f;

	// Token: 0x04000BD9 RID: 3033
	[SerializeField]
	private Color _color = Color.white;

	// Token: 0x04000BDA RID: 3034
	[Space]
	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	// Token: 0x04000BDB RID: 3035
	[DoesNotRequireDomainReloadInit]
	private static readonly int _colorID = Shader.PropertyToID("_Color");

	// Token: 0x04000BDC RID: 3036
	[DoesNotRequireDomainReloadInit]
	private static readonly int _sizeParamsID = Shader.PropertyToID("_SizeParams");
}
