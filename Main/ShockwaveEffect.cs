using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class ShockwaveEffect : MonoBehaviour
{
	// Token: 0x06000AD4 RID: 2772 RVA: 0x00032840 File Offset: 0x00030A40
	protected void Start()
	{
		//this._shockwavePS.main.maxParticles = this._maxShockwaveParticles;
		this._shockwavePSEmitParams = default(ParticleSystem.EmitParams);
		this._shockwavePSEmitParams.position = Vector3.zero;
		this._shockwavePS.Emit(this._shockwavePSEmitParams, 1);
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0003289C File Offset: 0x00030A9C
	public void SpawnShockwave(Vector3 pos)
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		if (timeSinceLevelLoad - this._prevShockwaveParticleSpawnTime > 0.06f)
		{
			this._shockwavePSEmitParams.position = pos;
			this._shockwavePS.Emit(this._shockwavePSEmitParams, 1);
			this._prevShockwaveParticleSpawnTime = timeSinceLevelLoad;
		}
	}

	// Token: 0x04000B4C RID: 2892
	[SerializeField]
	private ParticleSystem _shockwavePS;

	// Token: 0x04000B4D RID: 2893
	[SerializeField]
	private IntSO _maxShockwaveParticles;

	// Token: 0x04000B4E RID: 2894
	private ParticleSystem.EmitParams _shockwavePSEmitParams;

	// Token: 0x04000B4F RID: 2895
	private float _prevShockwaveParticleSpawnTime;
}
