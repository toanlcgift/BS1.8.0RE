using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class StaticEnvironmentLights : MonoBehaviour
{
	// Token: 0x06000B50 RID: 2896 RVA: 0x00034008 File Offset: 0x00032208
	protected void Awake()
	{
		for (int i = 0; i < this._materials.Length; i++)
		{
			this._materials[i].color = this._lightColors[i];
		}
	}

	// Token: 0x04000BF0 RID: 3056
	[SerializeField]
	private Color[] _lightColors;

	// Token: 0x04000BF1 RID: 3057
	[SerializeField]
	private Material[] _materials;
}
