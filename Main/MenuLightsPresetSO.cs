using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class MenuLightsPresetSO : PersistentScriptableObject
{
	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x0600060B RID: 1547 RVA: 0x000057AD File Offset: 0x000039AD
	public ColorSO playersPlaceNeonsColor
	{
		get
		{
			return this._playersPlaceNeonsColor;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x000057B5 File Offset: 0x000039B5
	public float playersPlaceNeonsIntensity
	{
		get
		{
			return this._playersPlaceNeonsIntensity;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x0600060D RID: 1549 RVA: 0x000057BD File Offset: 0x000039BD
	public MenuLightsPresetSO.LightIdColorPair[] lightIdColorPairs
	{
		get
		{
			return this._lightIdColorPairs;
		}
	}

	// Token: 0x04000690 RID: 1680
	[SerializeField]
	private ColorSO _playersPlaceNeonsColor;

	// Token: 0x04000691 RID: 1681
	[SerializeField]
	[Range(0f, 1f)]
	private float _playersPlaceNeonsIntensity = 1f;

	// Token: 0x04000692 RID: 1682
	[SerializeField]
	private MenuLightsPresetSO.LightIdColorPair[] _lightIdColorPairs;

	// Token: 0x02000184 RID: 388
	[Serializable]
	public class LightIdColorPair
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000057D8 File Offset: 0x000039D8
		public Color lightColor
		{
			get
			{
				return this.baseColor.color.ColorWithAlpha(this.intensity);
			}
		}

		// Token: 0x04000693 RID: 1683
		public int lightId;

		// Token: 0x04000694 RID: 1684
		public ColorSO baseColor;

		// Token: 0x04000695 RID: 1685
		[Range(0f, 1f)]
		public float intensity;
	}
}
