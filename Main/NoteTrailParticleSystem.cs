using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class NoteTrailParticleSystem : MonoBehaviour
{
	// Token: 0x06000AC4 RID: 2756 RVA: 0x00008691 File Offset: 0x00006891
	protected void Awake()
	{
		this._emitParams = default(ParticleSystem.EmitParams);
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00031C80 File Offset: 0x0002FE80
	public void Emit(Vector3 startPos, Vector3 endPos, int count)
	{
		for (int i = 0; i < count; i++)
		{
			this._emitParams.position = Vector3.Lerp(startPos, endPos, ((float)i + 1f) / (float)count);
			this._particleSystem.Emit(this._emitParams, 1);
		}
	}

	// Token: 0x04000B26 RID: 2854
	[SerializeField]
	private ParticleSystem _particleSystem;

	// Token: 0x04000B27 RID: 2855
	private ParticleSystem.EmitParams _emitParams;
}
