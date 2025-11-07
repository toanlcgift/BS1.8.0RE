using System;
using UnityEngine;
using Zenject;

// Token: 0x020002E0 RID: 736
public class MoveBackWall : MonoBehaviour
{
	// Token: 0x06000C83 RID: 3203 RVA: 0x00009BFD File Offset: 0x00007DFD
	protected void Start()
	{
		this._thisZ = base.transform.position.z;
		this._material = this._meshRenderer.sharedMaterial;
		this._meshRenderer.enabled = false;
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00036A84 File Offset: 0x00034C84
	protected void Update()
	{
		float num = Mathf.Abs(this._playerController.headPos.z - this._thisZ);
		if (num < this._fadeInRegion && !this._isVisible)
		{
			this._isVisible = true;
			this._meshRenderer.enabled = true;
		}
		else if (num > this._fadeInRegion && this._isVisible)
		{
			this._isVisible = false;
			this._meshRenderer.enabled = false;
		}
		if (this._isVisible)
		{
			this._material.color = new Color(1f, 1f, 1f, 1f - num / this._fadeInRegion);
		}
	}

	// Token: 0x04000D01 RID: 3329
	[SerializeField]
	private float _fadeInRegion = 0.5f;

	// Token: 0x04000D02 RID: 3330
	[SerializeField]
	private MeshRenderer _meshRenderer;

	// Token: 0x04000D03 RID: 3331
	[Inject]
	private PlayerController _playerController;

	// Token: 0x04000D04 RID: 3332
	private float _thisZ;

	// Token: 0x04000D05 RID: 3333
	private bool _isVisible;

	// Token: 0x04000D06 RID: 3334
	private Material _material;
}
