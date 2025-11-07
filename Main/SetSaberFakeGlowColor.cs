using System;
using UnityEngine;
using Zenject;

// Token: 0x0200030E RID: 782
public class SetSaberFakeGlowColor : MonoBehaviour
{
	// Token: 0x170002F8 RID: 760
	// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0000A5FB File Offset: 0x000087FB
	public SaberType saberType
	{
		set
		{
			this._saberTypeObject = null;
			this._saberType = value;
			this.SetColors();
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0000A611 File Offset: 0x00008811
	protected void Start()
	{
		if (this._saberTypeObject != null)
		{
			this._saberType = this._saberTypeObject.saberType;
		}
		this.SetColors();
		this._colorManager.colorsDidChangeEvent += this.HandleColorManagerColorsDidChange;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0000A64F File Offset: 0x0000884F
	protected void OnDestroy()
	{
		if (this._colorManager != null)
		{
			this._colorManager.colorsDidChangeEvent -= this.HandleColorManagerColorsDidChange;
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0000A676 File Offset: 0x00008876
	private void HandleColorManagerColorsDidChange()
	{
		this.SetColors();
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0000A67E File Offset: 0x0000887E
	private void SetColors()
	{
		this._parametric3SliceSprite.color = this._colorManager.ColorForSaberType(this._saberType) * this._tintColor;
		this._parametric3SliceSprite.Refresh();
	}

	// Token: 0x04000DDE RID: 3550
	[SerializeField]
	private Color _tintColor;

	// Token: 0x04000DDF RID: 3551
	[Space]
	[SerializeField]
	[NullAllowed(NullAllowed.Context.Prefab)]
	private SaberTypeObject _saberTypeObject;

	// Token: 0x04000DE0 RID: 3552
	[SerializeField]
	private Parametric3SliceSpriteController _parametric3SliceSprite;

	// Token: 0x04000DE1 RID: 3553
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000DE2 RID: 3554
	private SaberType _saberType;
}
