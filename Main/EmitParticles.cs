using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class EmitParticles : MonoBehaviour
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x00008264 File Offset: 0x00006464
	public void Emit(int count)
	{
		this._particleSystem.Emit(count);
	}

	// Token: 0x04000AD9 RID: 2777
	[SerializeField]
	private ParticleSystem _particleSystem;
}
