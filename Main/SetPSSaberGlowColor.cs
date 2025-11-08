using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class SetPSSaberGlowColor : MonoBehaviour
{
	// Token: 0x06000D65 RID: 3429 RVA: 0x00038B40 File Offset: 0x00036D40
	protected void Start()
	{
		this._particleSystem.startColor = this._colorManager.ColorForSaberType(this._saber.saberType);
	}

	// Token: 0x04000DD5 RID: 3541
	[SerializeField]
	private SaberTypeObject _saber;

	// Token: 0x04000DD6 RID: 3542
	[SerializeField]
	private ColorManager _colorManager;

	// Token: 0x04000DD7 RID: 3543
	[SerializeField]
	private ParticleSystem _particleSystem;
}
