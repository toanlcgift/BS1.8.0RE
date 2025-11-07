using System;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class ConditionalMaterialSwitcher : MonoBehaviour
{
	// Token: 0x06000E35 RID: 3637 RVA: 0x0000AFA3 File Offset: 0x000091A3
	public void Awake()
	{
		if (this._value)
		{
			this._renderer.sharedMaterial = this._material1;
			return;
		}
		this._renderer.sharedMaterial = this._material0;
	}

	// Token: 0x04000E98 RID: 3736
	[Header("False")]
	[SerializeField]
	private Material _material0;

	// Token: 0x04000E99 RID: 3737
	[Header("True")]
	[SerializeField]
	private Material _material1;

	// Token: 0x04000E9A RID: 3738
	[Space]
	[SerializeField]
	private BoolSO _value;

	// Token: 0x04000E9B RID: 3739
	[SerializeField]
	private Renderer _renderer;
}
