using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

// Token: 0x02000186 RID: 390
public class CampaignProgressModel : MonoBehaviour
{
	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000613 RID: 1555 RVA: 0x00005806 File Offset: 0x00003A06
	public int numberOfClearedMissions
	{
		get
		{
			if (this._numberOfClearedMissionsDirty)
			{
				this.UpdateNumberOfClearedMissions();
				this._numberOfClearedMissionsDirty = false;
			}
			return this._numberOfClearedMissions;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x00005824 File Offset: 0x00003A24
	protected void Awake()
	{
		this._missionIds = new HashSet<string>();
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x00005831 File Offset: 0x00003A31
	public bool IsMissionRegistered(string missionId)
	{
		return this._missionIds.Contains(missionId);
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0000583F File Offset: 0x00003A3F
	public void RegisterMissionId(string missionId)
	{
		this._missionIds.Add(missionId);
		this._numberOfClearedMissionsDirty = true;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00005855 File Offset: 0x00003A55
	public bool IsMissionCleared(string missionId)
	{
		return this._playerDataModel.playerData.GetPlayerMissionStatsData(missionId).cleared;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0000586D File Offset: 0x00003A6D
	public bool IsMissionFinal(string missionId)
	{
		return this._finalMissionId == missionId;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0000587B File Offset: 0x00003A7B
	public void SetFinalMissionId(string missionId)
	{
		this._finalMissionId = missionId;
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00005884 File Offset: 0x00003A84
	public bool WillFinishGameAfterThisMission(string missionId)
	{
		return this.IsMissionFinal(missionId) && !this.IsMissionCleared(missionId);
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0000589B File Offset: 0x00003A9B
	public void SetMissionCleared(string missionId)
	{
		this.__SetMissionCleared(missionId, true);
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x000058A5 File Offset: 0x00003AA5
	public void __SetMissionCleared(string missionId, bool cleared)
	{
		this._playerDataModel.playerData.GetPlayerMissionStatsData(missionId).cleared = cleared;
		this._numberOfClearedMissionsDirty = true;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x000249E8 File Offset: 0x00022BE8
	private int UpdateNumberOfClearedMissions()
	{
		this._numberOfClearedMissions = 0;
		foreach (string missionId in this._missionIds)
		{
			if (this._playerDataModel.playerData.GetPlayerMissionStatsData(missionId).cleared)
			{
				this._numberOfClearedMissions++;
			}
		}
		return this._numberOfClearedMissions;
	}

	// Token: 0x04000698 RID: 1688
	[Inject]
	private PlayerDataModel _playerDataModel;

	// Token: 0x04000699 RID: 1689
	private HashSet<string> _missionIds;

	// Token: 0x0400069A RID: 1690
	private string _finalMissionId;

	// Token: 0x0400069B RID: 1691
	private bool _numberOfClearedMissionsDirty = true;

	// Token: 0x0400069C RID: 1692
	private int _numberOfClearedMissions;
}
