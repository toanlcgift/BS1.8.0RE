using System;
using UnityEngine;
using Zenject;

// Token: 0x0200030C RID: 780
public class SetSaberBladeParams : MonoBehaviour
{
	// Token: 0x06000D67 RID: 3431 RVA: 0x00038B7C File Offset: 0x00036D7C
	protected void Start()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		foreach (SetSaberBladeParams.PropertyTintColorPair propertyTintColorPair in this._propertyTintColorPairs)
		{
			materialPropertyBlock.SetColor(propertyTintColorPair.property, this._colorManager.ColorForSaberType(this._saber.saberType) * propertyTintColorPair.tintColor);
		}
		this._meshRenderer.SetPropertyBlock(materialPropertyBlock);
	}

	// Token: 0x04000DD8 RID: 3544
	[SerializeField]
	private SaberTypeObject _saber;

	// Token: 0x04000DD9 RID: 3545
	[SerializeField]
	private MeshRenderer _meshRenderer;

	// Token: 0x04000DDA RID: 3546
	[SerializeField]
	[NullAllowed]
	private SetSaberBladeParams.PropertyTintColorPair[] _propertyTintColorPairs;

	// Token: 0x04000DDB RID: 3547
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x0200030D RID: 781
	[Serializable]
	public class PropertyTintColorPair
	{
		// Token: 0x04000DDC RID: 3548
		public Color tintColor;

		// Token: 0x04000DDD RID: 3549
		public string property;
	}
}
