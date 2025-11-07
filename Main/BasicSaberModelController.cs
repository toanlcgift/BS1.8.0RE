using System;
using UnityEngine;
using Xft;
using Zenject;

// Token: 0x020002F7 RID: 759
public class BasicSaberModelController : MonoBehaviour, ISaberModelController
{
	// Token: 0x06000D07 RID: 3335 RVA: 0x00037A10 File Offset: 0x00035C10
	public void Init(Transform parent, SaberType saberType)
	{
		base.transform.SetParent(parent, false);
		base.transform.localPosition = Vector3.zero;
		base.transform.localRotation = Quaternion.identity;
		Color color = this._colorManager.ColorForSaberType(saberType);
		this._saberWeaponTrail.color = (color * this._initData.trailTintColor).linear;
		SetSaberGlowColor[] setSaberGlowColors = this._setSaberGlowColors;
		for (int i = 0; i < setSaberGlowColors.Length; i++)
		{
			setSaberGlowColors[i].saberType = saberType;
		}
		SetSaberFakeGlowColor[] setSaberFakeGlowColors = this._setSaberFakeGlowColors;
		for (int i = 0; i < setSaberFakeGlowColors.Length; i++)
		{
			setSaberFakeGlowColors[i].saberType = saberType;
		}
		this._light.color = color;
	}

	// Token: 0x04000D6D RID: 3437
	[SerializeField]
	private XWeaponTrail _saberWeaponTrail;

	// Token: 0x04000D6E RID: 3438
	[SerializeField]
	private SetSaberGlowColor[] _setSaberGlowColors;

	// Token: 0x04000D6F RID: 3439
	[SerializeField]
	private SetSaberFakeGlowColor[] _setSaberFakeGlowColors;

	// Token: 0x04000D70 RID: 3440
	[SerializeField]
	private Light _light;

	// Token: 0x04000D71 RID: 3441
	[InjectOptional]
	private BasicSaberModelController.InitData _initData = new BasicSaberModelController.InitData(new Color(1f, 1f, 1f, 0.5f));

	// Token: 0x04000D72 RID: 3442
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x020002F8 RID: 760
	public class InitData
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x0000A233 File Offset: 0x00008433
		public InitData(Color trailTintColor)
		{
			this.trailTintColor = trailTintColor;
		}

		// Token: 0x04000D73 RID: 3443
		public readonly Color trailTintColor;
	}
}
