using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class OculusLevelProductsModelSO : PersistentScriptableObject
{
	// Token: 0x170001EA RID: 490
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x00006015 File Offset: 0x00004215
	public OculusLevelProductsModelSO.LevelPackProductData[] levelPackProductsData
	{
		get
		{
			return this._levelPackProductsData;
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00027780 File Offset: 0x00025980
	protected override void OnEnable()
	{
		base.OnEnable();
		this._levelPackIdToProductData.Clear();
		this._levelIdToProductData.Clear();
		foreach (OculusLevelProductsModelSO.LevelPackProductData levelPackProductData in this._levelPackProductsData)
		{
			this._levelPackIdToProductData.Add(levelPackProductData.levelPackId, levelPackProductData);
			foreach (OculusLevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
			{
				this._levelIdToProductData.Add(levelProductData.levelId, levelProductData);
				this._assetFileToSku.Add(levelProductData.assetFile, levelProductData.sku);
			}
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00027820 File Offset: 0x00025A20
	public OculusLevelProductsModelSO.LevelProductData GetLevelProductData(string levelId)
	{
		OculusLevelProductsModelSO.LevelProductData result = null;
		if (!this._levelIdToProductData.TryGetValue(levelId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00027844 File Offset: 0x00025A44
	public OculusLevelProductsModelSO.LevelPackProductData GetLevelPackProductData(string levelPackId)
	{
		OculusLevelProductsModelSO.LevelPackProductData result = null;
		if (!this._levelPackIdToProductData.TryGetValue(levelPackId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00027868 File Offset: 0x00025A68
	public string GetLevelSku(string assetFile)
	{
		string result = null;
		if (!this._assetFileToSku.TryGetValue(assetFile, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x0400078A RID: 1930
	[SerializeField]
	private OculusLevelProductsModelSO.LevelPackProductData[] _levelPackProductsData = new OculusLevelProductsModelSO.LevelPackProductData[0];

	// Token: 0x0400078B RID: 1931
	private Dictionary<string, OculusLevelProductsModelSO.LevelProductData> _levelIdToProductData = new Dictionary<string, OculusLevelProductsModelSO.LevelProductData>();

	// Token: 0x0400078C RID: 1932
	private Dictionary<string, OculusLevelProductsModelSO.LevelPackProductData> _levelPackIdToProductData = new Dictionary<string, OculusLevelProductsModelSO.LevelPackProductData>();

	// Token: 0x0400078D RID: 1933
	private Dictionary<string, string> _assetFileToSku = new Dictionary<string, string>();

	// Token: 0x020001CC RID: 460
	[Serializable]
	public class LevelProductData
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00006052 File Offset: 0x00004252
		public string sku
		{
			get
			{
				return this._sku;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000605A File Offset: 0x0000425A
		public string levelId
		{
			get
			{
				return this._levelId;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00006062 File Offset: 0x00004262
		public string assetFile
		{
			get
			{
				return this._assetFile;
			}
		}

		// Token: 0x0400078E RID: 1934
		[SerializeField]
		private string _levelId;

		// Token: 0x0400078F RID: 1935
		[SerializeField]
		private string _sku;

		// Token: 0x04000790 RID: 1936
		[SerializeField]
		private string _assetFile;
	}

	// Token: 0x020001CD RID: 461
	[Serializable]
	public class LevelPackProductData
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000606A File Offset: 0x0000426A
		public string sku
		{
			get
			{
				return this._sku;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00006072 File Offset: 0x00004272
		public string levelPackId
		{
			get
			{
				return this._levelPackId;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000607A File Offset: 0x0000427A
		public OculusLevelProductsModelSO.LevelProductData[] levelProductsData
		{
			get
			{
				return this._levelProductsData;
			}
		}

		// Token: 0x04000791 RID: 1937
		[SerializeField]
		private string _sku;

		// Token: 0x04000792 RID: 1938
		[SerializeField]
		private string _levelPackId;

		// Token: 0x04000793 RID: 1939
		[SerializeField]
		private OculusLevelProductsModelSO.LevelProductData[] _levelProductsData = new OculusLevelProductsModelSO.LevelProductData[0];
	}
}
