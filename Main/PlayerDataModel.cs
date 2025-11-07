using System;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class PlayerDataModel : MonoBehaviour
{
	// Token: 0x17000230 RID: 560
	// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000068E3 File Offset: 0x00004AE3
	public PlayerData playerData
	{
		get
		{
			return this._playerData;
		}
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000068EB File Offset: 0x00004AEB
	protected void OnEnable()
	{
		this.Load();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000068F3 File Offset: 0x00004AF3
	protected void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			this.Save();
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x000068FE File Offset: 0x00004AFE
	protected void OnDisable()
	{
		this.Save();
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00006906 File Offset: 0x00004B06
	public void ResetData()
	{
		this._playerData = this._playerDataFileManager.CreateDefaultPlayerData();
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00006919 File Offset: 0x00004B19
	public void Save()
	{
		this._playerDataFileManager.Save(this._playerData);
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0000692C File Offset: 0x00004B2C
	public void Load()
	{
		this._playerData = this._playerDataFileManager.Load();
	}

	// Token: 0x04000851 RID: 2129
	[SerializeField]
	private PlayerDataFileManagerSO _playerDataFileManager;

	// Token: 0x04000852 RID: 2130
	private PlayerData _playerData;
}
