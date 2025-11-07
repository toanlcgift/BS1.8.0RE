using System;
using LIV.SDK.Unity;
using UnityEngine;

// Token: 0x02000435 RID: 1077
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x0600149C RID: 5276 RVA: 0x0000F88D File Offset: 0x0000DA8D
	public Camera camera
	{
		get
		{
			return this._camera;
		}
	}

	// Token: 0x170003EE RID: 1006
	// (set) Token: 0x0600149D RID: 5277 RVA: 0x0000F895 File Offset: 0x0000DA95
	public bool enableCamera
	{
		set
		{
			base.gameObject.SetActive(value);
		}
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x0600149E RID: 5278 RVA: 0x0000F8A3 File Offset: 0x0000DAA3
	public Vector3 position
	{
		get
		{
			return this._transform.position;
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x0600149F RID: 5279 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
	public Quaternion rotation
	{
		get
		{
			return this._transform.rotation;
		}
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x0004B758 File Offset: 0x00049958
	protected void Awake()
	{
		this._transform = base.transform;
		this._camera = base.GetComponent<Camera>();
		this.liv = base.gameObject.GetComponent<LIV.SDK.Unity.LIV>();
		if (this.liv == null)
		{
			this.liv = base.gameObject.AddComponent<LIV.SDK.Unity.LIV>();
			this.liv.Init(this._camera, base.gameObject.transform.parent);
		}
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x0000F8BD File Offset: 0x0000DABD
	protected void OnEnable()
	{
		if (this.liv != null)
		{
			this.liv.OnLIVEnable();
		}
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
	protected void Update()
	{
		if (this.liv != null)
		{
			this.liv.OnLIVUpdate();
		}
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x0000F8F3 File Offset: 0x0000DAF3
	protected void OnDisable()
	{
		if (this.liv != null)
		{
			this.liv.OnLIVDisable();
		}
	}

	// Token: 0x04001450 RID: 5200
	private Camera _camera;

	// Token: 0x04001451 RID: 5201
	private Transform _transform;

	// Token: 0x04001452 RID: 5202
	private LIV.SDK.Unity.LIV liv;
}
