using System;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class NoteCutParticlesEffect : MonoBehaviour
{
	// Token: 0x06000AC1 RID: 2753 RVA: 0x00031A08 File Offset: 0x0002FC08
	protected void Awake()
	{
		for (int i = 0; i < 2; i++)
		{
			this._sparklesPSEmitParams[i] = default(ParticleSystem.EmitParams);
			this._sparklesPSEmitParams[i].applyShapeToPosition = true;
			this._sparklesPSMainModule[i] = this._sparklesPS[i].main;
			this._sparklesPSShapeModule[i] = this._sparklesPS[i].shape;
		}
		this._explosionPSForceOverLifetimeModule = this._explosionPS.forceOverLifetime;
		this._explosionPSForceOverLifetimeModule.enabled = true;
		this._explosionPSEmitParams = default(ParticleSystem.EmitParams);
		this._explosionPSEmitParams.applyShapeToPosition = true;
		this._explosionCoresPSEmitParams = default(ParticleSystem.EmitParams);
		this._sparklesLifetimeMinMaxCurve = new ParticleSystem.MinMaxCurve(0.6f, 1f);
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00031AD0 File Offset: 0x0002FCD0
	public void SpawnParticles(Vector3 pos, Vector3 cutNormal, Vector3 saberDir, Vector3 moveVec, Color32 color, int sparkleParticlesCount, int explosionParticlesCount, float lifetimeMultiplier, SaberType saberType)
	{
		Quaternion rhs = default(Quaternion);
		rhs.eulerAngles = new Vector3(0f, 0f, 40f);
		Quaternion lhs = default(Quaternion);
		lhs.SetLookRotation(cutNormal, saberDir);
		lhs *= rhs;
		this._sparklesPSEmitParams[0].startColor = color;
		this._sparklesPSEmitParams[0].position = pos;
		this._sparklesPSShapeModule[0].rotation = lhs.eulerAngles;
		this._sparklesLifetimeMinMaxCurve.constantMin = 0.8f * lifetimeMultiplier;
		this._sparklesLifetimeMinMaxCurve.constantMax = 1.2f * lifetimeMultiplier;
		this._sparklesPSMainModule[0].startLifetime = this._sparklesLifetimeMinMaxCurve;
		this._sparklesPS[0].Emit(this._sparklesPSEmitParams[0], sparkleParticlesCount);
		this._explosionPSEmitParams.startColor = color;
		this._explosionPSEmitParams.position = pos;
		this._explosionPSForceOverLifetimeModule.x = new ParticleSystem.MinMaxCurve(moveVec.x);
		this._explosionPSForceOverLifetimeModule.y = new ParticleSystem.MinMaxCurve(moveVec.y);
		this._explosionPSForceOverLifetimeModule.z = new ParticleSystem.MinMaxCurve(moveVec.z);
		this._explosionPS.Emit(this._explosionPSEmitParams, explosionParticlesCount);
		rhs.eulerAngles = new Vector3(-90f, 0f, 0f);
		lhs *= rhs;
		this._explosionCoresPSEmitParams.startColor = color;
		this._explosionCoresPSEmitParams.position = pos;
		this._explosionCoresPSEmitParams.rotation3D = lhs.eulerAngles;
		this._explosionCorePS.Emit(this._explosionCoresPSEmitParams, 1);
	}

	// Token: 0x04000B1C RID: 2844
	[SerializeField]
	private ParticleSystem[] _sparklesPS;

	// Token: 0x04000B1D RID: 2845
	[SerializeField]
	private ParticleSystem _explosionPS;

	// Token: 0x04000B1E RID: 2846
	[SerializeField]
	private ParticleSystem _explosionCorePS;

	// Token: 0x04000B1F RID: 2847
	private ParticleSystem.EmitParams[] _sparklesPSEmitParams = new ParticleSystem.EmitParams[2];

	// Token: 0x04000B20 RID: 2848
	private ParticleSystem.MainModule[] _sparklesPSMainModule = new ParticleSystem.MainModule[2];

	// Token: 0x04000B21 RID: 2849
	private ParticleSystem.ShapeModule[] _sparklesPSShapeModule = new ParticleSystem.ShapeModule[2];

	// Token: 0x04000B22 RID: 2850
	private ParticleSystem.ForceOverLifetimeModule _explosionPSForceOverLifetimeModule;

	// Token: 0x04000B23 RID: 2851
	private ParticleSystem.EmitParams _explosionPSEmitParams;

	// Token: 0x04000B24 RID: 2852
	private ParticleSystem.EmitParams _explosionCoresPSEmitParams;

	// Token: 0x04000B25 RID: 2853
	private ParticleSystem.MinMaxCurve _sparklesLifetimeMinMaxCurve;
}
