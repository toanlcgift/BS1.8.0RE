using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class SetBlocksBladeSaberGlowColor : MonoBehaviour
{
	// Token: 0x06000D63 RID: 3427 RVA: 0x0000A5D8 File Offset: 0x000087D8
	protected void Start()
	{
		this._blocksBlade.color = this._colorManager.ColorForSaberType(this._saber.saberType);
	}

	// Token: 0x04000DD2 RID: 3538
	[SerializeField]
	private SaberTypeObject _saber;

	// Token: 0x04000DD3 RID: 3539
	[SerializeField]
	private ColorManager _colorManager;

	// Token: 0x04000DD4 RID: 3540
	[SerializeField]
	private BlocksBlade _blocksBlade;
}
