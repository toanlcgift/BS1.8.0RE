using System;
using UnityEngine;
using Zenject;

// Token: 0x0200027A RID: 634
public class EnvironmentLightSimpleController : MonoBehaviour
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x00008534 File Offset: 0x00006734
	protected void LateUpdate()
	{
		this._lightManager.SetColorForId(this._colorId, this._color);
	}

	// Token: 0x04000B0B RID: 2827
	[SerializeField]
	private Color _color;

	// Token: 0x04000B0C RID: 2828
	[SerializeField]
	private int _colorId;

	// Token: 0x04000B0D RID: 2829
	[Inject]
	private LightWithIdManager _lightManager;
}
