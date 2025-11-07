using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200032B RID: 811
public class ConditionalImageMaterialSwitcher : MonoBehaviour
{
	// Token: 0x06000E33 RID: 3635 RVA: 0x0000AF71 File Offset: 0x00009171
	public void Awake()
	{
		if (this._value)
		{
			this._image.material = this._material1;
			return;
		}
		this._image.material = this._material0;
	}

	// Token: 0x04000E94 RID: 3732
	[Header("False")]
	[SerializeField]
	private Material _material0;

	// Token: 0x04000E95 RID: 3733
	[Header("True")]
	[SerializeField]
	private Material _material1;

	// Token: 0x04000E96 RID: 3734
	[Space]
	[SerializeField]
	private BoolSO _value;

	// Token: 0x04000E97 RID: 3735
	[SerializeField]
	private Image _image;
}
