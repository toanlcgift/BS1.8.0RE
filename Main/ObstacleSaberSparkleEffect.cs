using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class ObstacleSaberSparkleEffect : MonoBehaviour
{
	// Token: 0x170002AE RID: 686
	// (set) Token: 0x06000B02 RID: 2818 RVA: 0x000332F4 File Offset: 0x000314F4
	public Color color
	{
		set
		{
			//this._sparkleParticleSystem.main.startColor = value;
			//this._burnParticleSystem.main.startColor = value;
		}
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x000089AA File Offset: 0x00006BAA
	protected void Awake()
	{
		this._sparkleParticleSystemEmmisionModule = this._sparkleParticleSystem.emission;
		this._burnParticleSystemEmmisionModule = this._burnParticleSystem.emission;
		this._sparkleParticleSystemEmmisionModule.enabled = false;
		this._burnParticleSystemEmmisionModule.enabled = false;
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000089E6 File Offset: 0x00006BE6
	public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
	{
		base.transform.SetPositionAndRotation(pos, rot);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x000089F5 File Offset: 0x00006BF5
	public void StartEmission()
	{
		if (this._burnParticleSystemEmmisionModule.enabled)
		{
			return;
		}
		this._sparkleParticleSystemEmmisionModule.enabled = true;
		this._burnParticleSystemEmmisionModule.enabled = true;
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00008A1D File Offset: 0x00006C1D
	public void StopEmission()
	{
		if (!this._burnParticleSystemEmmisionModule.enabled)
		{
			return;
		}
		this._burnParticleSystem.Clear();
		this._sparkleParticleSystemEmmisionModule.enabled = false;
		this._burnParticleSystemEmmisionModule.enabled = false;
	}

	// Token: 0x04000B92 RID: 2962
	[SerializeField]
	private ParticleSystem _sparkleParticleSystem;

	// Token: 0x04000B93 RID: 2963
	[SerializeField]
	private ParticleSystem _burnParticleSystem;

	// Token: 0x04000B94 RID: 2964
	private ParticleSystem.EmissionModule _sparkleParticleSystemEmmisionModule;

	// Token: 0x04000B95 RID: 2965
	private ParticleSystem.EmissionModule _burnParticleSystemEmmisionModule;
}
