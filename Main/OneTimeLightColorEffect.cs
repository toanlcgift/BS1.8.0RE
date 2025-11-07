using System;
using UnityEngine;
using Zenject;

// Token: 0x02000282 RID: 642
public class OneTimeLightColorEffect : MonoBehaviour
{
	// Token: 0x06000AC7 RID: 2759 RVA: 0x0000869F File Offset: 0x0000689F
	protected void Update()
	{
		this._lightWithIdManager.SetColorForId(this._lightsId, this._color.color.ColorWithAlpha(this._alpha));
		base.enabled = false;
	}

	// Token: 0x04000B28 RID: 2856
	[SerializeField]
	private ColorSO _color;

	// Token: 0x04000B29 RID: 2857
	[SerializeField]
	private float _alpha = 1f;

	// Token: 0x04000B2A RID: 2858
	[SerializeField]
	private int _lightsId;

	// Token: 0x04000B2B RID: 2859
	[Inject]
	private LightWithIdManager _lightWithIdManager;
}
