using System;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class BombExplosionEffect : MonoBehaviour
{
	// Token: 0x06000A5B RID: 2651 RVA: 0x000080B9 File Offset: 0x000062B9
	protected void Awake()
	{
		this._emitParams = default(ParticleSystem.EmitParams);
		this._emitParams.applyShapeToPosition = true;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x000080D3 File Offset: 0x000062D3
	public void SpawnExplosion(Vector3 pos)
	{
		this._emitParams.position = pos;
		this._debrisPS.Emit(this._emitParams, this._debrisCount);
		this._explosionPS.Emit(this._emitParams, this._explosionParticlesCount);
	}

	// Token: 0x04000AC2 RID: 2754
	[SerializeField]
	private ParticleSystem _debrisPS;

	// Token: 0x04000AC3 RID: 2755
	[SerializeField]
	private ParticleSystem _explosionPS;

	// Token: 0x04000AC4 RID: 2756
	[SerializeField]
	private int _debrisCount = 40;

	// Token: 0x04000AC5 RID: 2757
	[SerializeField]
	private int _explosionParticlesCount = 70;

	// Token: 0x04000AC6 RID: 2758
	private ParticleSystem.EmitParams _emitParams;

	// Token: 0x04000AC7 RID: 2759
	private ParticleSystem.EmitParams _explosionPSEmitParams;
}
