using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class SetTubeBloomPrePassLightColor : MonoBehaviour
{
	// Token: 0x06000D77 RID: 3447 RVA: 0x00038C60 File Offset: 0x00036E60
	protected void Start()
	{
		for (int i = 0; i < this._tubeLights.Length; i++)
		{
			this._tubeLights[i].color = this._color;
		}
	}

	// Token: 0x04000DEB RID: 3563
	[SerializeField]
	private ColorSO _color;

	// Token: 0x04000DEC RID: 3564
	[SerializeField]
	private TubeBloomPrePassLight[] _tubeLights;
}
