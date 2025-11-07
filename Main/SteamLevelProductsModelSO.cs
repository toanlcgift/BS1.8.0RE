using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class SteamLevelProductsModelSO : PersistentScriptableObject
{
	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000715 RID: 1813 RVA: 0x000060DD File Offset: 0x000042DD
	public SteamLevelProductsModelSO.LevelPackProductData[] levelPackProductsData
	{
		get
		{
			return this._levelPackProductsData;
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00027BD8 File Offset: 0x00025DD8
	protected override void OnEnable()
	{
		base.OnEnable();
		this._levelPackIdToProductData.Clear();
		this._levelIdToProductData.Clear();
		foreach (SteamLevelProductsModelSO.LevelPackProductData levelPackProductData in this._levelPackProductsData)
		{
			this._levelPackIdToProductData.Add(levelPackProductData.levelPackId, levelPackProductData);
			foreach (SteamLevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
			{
				this._levelIdToProductData.Add(levelProductData.levelId, levelProductData);
			}
		}
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00027C60 File Offset: 0x00025E60
	public SteamLevelProductsModelSO.LevelProductData GetLevelProductData(string levelId)
	{
		SteamLevelProductsModelSO.LevelProductData result = null;
		if (!this._levelIdToProductData.TryGetValue(levelId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00027C84 File Offset: 0x00025E84
	public SteamLevelProductsModelSO.LevelPackProductData GetLevelPackProductData(string levelPackId)
	{
		SteamLevelProductsModelSO.LevelPackProductData result = null;
		if (!this._levelPackIdToProductData.TryGetValue(levelPackId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x0400079F RID: 1951
	[SerializeField]
	private SteamLevelProductsModelSO.LevelPackProductData[] _levelPackProductsData = new SteamLevelProductsModelSO.LevelPackProductData[0];

	// Token: 0x040007A0 RID: 1952
	private Dictionary<string, SteamLevelProductsModelSO.LevelProductData> _levelIdToProductData = new Dictionary<string, SteamLevelProductsModelSO.LevelProductData>();

	// Token: 0x040007A1 RID: 1953
	private Dictionary<string, SteamLevelProductsModelSO.LevelPackProductData> _levelPackIdToProductData = new Dictionary<string, SteamLevelProductsModelSO.LevelPackProductData>();

	// Token: 0x020001D2 RID: 466
	[Serializable]
	public class LevelProductData
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000610F File Offset: 0x0000430F
		public uint appId
		{
			get
			{
				return this._appId;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00006117 File Offset: 0x00004317
		public string levelId
		{
			get
			{
				return this._levelId;
			}
		}

		// Token: 0x040007A2 RID: 1954
		[SerializeField]
		private uint _appId;

		// Token: 0x040007A3 RID: 1955
		[SerializeField]
		private string _levelId;
	}

	// Token: 0x020001D3 RID: 467
	[Serializable]
	public class LevelPackProductData
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000611F File Offset: 0x0000431F
		public uint bundleId
		{
			get
			{
				return this._bundleId;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00006127 File Offset: 0x00004327
		public string levelPackId
		{
			get
			{
				return this._levelPackId;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000612F File Offset: 0x0000432F
		public SteamLevelProductsModelSO.LevelProductData[] levelProductsData
		{
			get
			{
				return this._levelProductsData;
			}
		}

		// Token: 0x040007A4 RID: 1956
		[SerializeField]
		private uint _bundleId;

		// Token: 0x040007A5 RID: 1957
		[SerializeField]
		private string _levelPackId;

		// Token: 0x040007A6 RID: 1958
		[SerializeField]
		private SteamLevelProductsModelSO.LevelProductData[] _levelProductsData = new SteamLevelProductsModelSO.LevelProductData[0];
	}
}
