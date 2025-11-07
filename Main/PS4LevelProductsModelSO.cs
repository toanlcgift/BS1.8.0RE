using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class PS4LevelProductsModelSO : PersistentScriptableObject
{
	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600074D RID: 1869 RVA: 0x000062E8 File Offset: 0x000044E8
	public PS4LevelProductsModelSO.LevelPackProductData[] levelPackProductsData
	{
		get
		{
			return this._levelPackProductsData;
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002836C File Offset: 0x0002656C
	protected override void OnEnable()
	{
		base.OnEnable();
		this._levelPackIdToProductData.Clear();
		this._levelIdToProductData.Clear();
		foreach (PS4LevelProductsModelSO.LevelPackProductData levelPackProductData in this._levelPackProductsData)
		{
			this._levelPackIdToProductData.Add(levelPackProductData.levelPackId, levelPackProductData);
			foreach (PS4LevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
			{
				this._levelIdToProductData.Add(levelProductData.levelId, levelProductData);
			}
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x000283F4 File Offset: 0x000265F4
	public PS4LevelProductsModelSO.LevelProductData GetLevelProductData(string levelId)
	{
		PS4LevelProductsModelSO.LevelProductData result = null;
		if (!this._levelIdToProductData.TryGetValue(levelId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00028418 File Offset: 0x00026618
	public PS4LevelProductsModelSO.LevelPackProductData GetLevelPackProductData(string levelPackId)
	{
		PS4LevelProductsModelSO.LevelPackProductData result = null;
		if (!this._levelPackIdToProductData.TryGetValue(levelPackId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x040007C0 RID: 1984
	[SerializeField]
	private PS4LevelProductsModelSO.LevelPackProductData[] _levelPackProductsData = new PS4LevelProductsModelSO.LevelPackProductData[0];

	// Token: 0x040007C1 RID: 1985
	private Dictionary<string, PS4LevelProductsModelSO.LevelProductData> _levelIdToProductData = new Dictionary<string, PS4LevelProductsModelSO.LevelProductData>();

	// Token: 0x040007C2 RID: 1986
	private Dictionary<string, PS4LevelProductsModelSO.LevelPackProductData> _levelPackIdToProductData = new Dictionary<string, PS4LevelProductsModelSO.LevelPackProductData>();

	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class LevelProductData
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0000631A File Offset: 0x0000451A
		public string entitlementLabel
		{
			get
			{
				return this._entitlementLabel;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00006322 File Offset: 0x00004522
		public string productLabel
		{
			get
			{
				return this._productLabel;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000632A File Offset: 0x0000452A
		public string levelId
		{
			get
			{
				return this._levelId;
			}
		}

		// Token: 0x040007C3 RID: 1987
		[SerializeField]
		private string _entitlementLabel;

		// Token: 0x040007C4 RID: 1988
		[SerializeField]
		private string _productLabel;

		// Token: 0x040007C5 RID: 1989
		[SerializeField]
		private string _levelId;
	}

	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class LevelPackProductData
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00006332 File Offset: 0x00004532
		public string productLabel
		{
			get
			{
				return this._productLabel;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0000633A File Offset: 0x0000453A
		public string categoryLabel
		{
			get
			{
				return this._categoryLabel;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00006342 File Offset: 0x00004542
		public string levelPackId
		{
			get
			{
				return this._packId;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000634A File Offset: 0x0000454A
		public float packLevelPriceDiscountMul
		{
			get
			{
				return this._packLevelPriceDiscountMul;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00006352 File Offset: 0x00004552
		public PS4LevelProductsModelSO.LevelProductData[] levelProductsData
		{
			get
			{
				return this._levelProductsData;
			}
		}

		// Token: 0x040007C6 RID: 1990
		[SerializeField]
		private string _productLabel;

		// Token: 0x040007C7 RID: 1991
		[SerializeField]
		private string _categoryLabel;

		// Token: 0x040007C8 RID: 1992
		[SerializeField]
		private string _packId;

		// Token: 0x040007C9 RID: 1993
		[SerializeField]
		private float _packLevelPriceDiscountMul;

		// Token: 0x040007CA RID: 1994
		[SerializeField]
		private PS4LevelProductsModelSO.LevelProductData[] _levelProductsData = new PS4LevelProductsModelSO.LevelProductData[0];
	}
}
