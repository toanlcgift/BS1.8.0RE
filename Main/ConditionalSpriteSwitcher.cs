using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class ConditionalSpriteSwitcher : MonoBehaviour
{
	// Token: 0x06000E39 RID: 3641 RVA: 0x0003AA18 File Offset: 0x00038C18
	public void Awake()
	{
		if (this._value)
		{
			this._spriteRenderer.sprite = this._sprite1;
			this._spriteRenderer.sharedMaterial = this._material1;
			return;
		}
		this._spriteRenderer.sprite = this._sprite0;
		this._spriteRenderer.sharedMaterial = this._material0;
	}

	// Token: 0x04000E9F RID: 3743
	[Header("False")]
	[SerializeField]
	private Sprite _sprite0;

	// Token: 0x04000EA0 RID: 3744
	[SerializeField]
	private Material _material0;

	// Token: 0x04000EA1 RID: 3745
	[Header("True")]
	[SerializeField]
	private Sprite _sprite1;

	// Token: 0x04000EA2 RID: 3746
	[SerializeField]
	private Material _material1;

	// Token: 0x04000EA3 RID: 3747
	[Space]
	[SerializeField]
	private BoolSO _value;

	// Token: 0x04000EA4 RID: 3748
	[SerializeField]
	private SpriteRenderer _spriteRenderer;
}
