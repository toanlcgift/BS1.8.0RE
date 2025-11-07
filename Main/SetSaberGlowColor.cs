using System;
using UnityEngine;
using Zenject;

// Token: 0x0200030F RID: 783
public class SetSaberGlowColor : MonoBehaviour
{
	// Token: 0x170002F9 RID: 761
	// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0000A6B2 File Offset: 0x000088B2
	public SaberType saberType
	{
		set
		{
			this._saberTypeObject = null;
			this._saberType = value;
			this.SetColors();
		}
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0000A6C8 File Offset: 0x000088C8
	protected void Start()
	{
		if (this._saberTypeObject != null)
		{
			this._saberType = this._saberTypeObject.saberType;
		}
		this.SetColors();
		this._colorManager.colorsDidChangeEvent += this.HandleColorManagerColorsDidChange;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0000A706 File Offset: 0x00008906
	protected void OnDestroy()
	{
		if (this._colorManager != null)
		{
			this._colorManager.colorsDidChangeEvent -= this.HandleColorManagerColorsDidChange;
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0000A72D File Offset: 0x0000892D
	private void HandleColorManagerColorsDidChange()
	{
		this.SetColors();
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00038BE4 File Offset: 0x00036DE4
	private void SetColors()
	{
		if (this._materialPropertyBlock == null)
		{
			this._materialPropertyBlock = new MaterialPropertyBlock();
		}
		Color a = this._colorManager.ColorForSaberType(this._saberType);
		foreach (SetSaberGlowColor.PropertyTintColorPair propertyTintColorPair in this._propertyTintColorPairs)
		{
			this._materialPropertyBlock.SetColor(propertyTintColorPair.property, a * propertyTintColorPair.tintColor);
		}
		this._meshRenderer.SetPropertyBlock(this._materialPropertyBlock);
	}

	// Token: 0x04000DE3 RID: 3555
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private SaberTypeObject _saberTypeObject;

	// Token: 0x04000DE4 RID: 3556
	[SerializeField]
	private MeshRenderer _meshRenderer;

	// Token: 0x04000DE5 RID: 3557
	[SerializeField]
	[NullAllowed]
	private SetSaberGlowColor.PropertyTintColorPair[] _propertyTintColorPairs;

	// Token: 0x04000DE6 RID: 3558
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000DE7 RID: 3559
	private MaterialPropertyBlock _materialPropertyBlock;

	// Token: 0x04000DE8 RID: 3560
	private SaberType _saberType;

	// Token: 0x02000310 RID: 784
	[Serializable]
	public class PropertyTintColorPair
	{
		// Token: 0x04000DE9 RID: 3561
		public Color tintColor;

		// Token: 0x04000DEA RID: 3562
		public string property;
	}
}
