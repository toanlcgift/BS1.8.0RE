using System;

// Token: 0x020001FB RID: 507
public class PlayerMissionStatsData
{
	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000810 RID: 2064 RVA: 0x00006A47 File Offset: 0x00004C47
	public string missionId
	{
		get
		{
			return this._missionId;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000811 RID: 2065 RVA: 0x00006A4F File Offset: 0x00004C4F
	// (set) Token: 0x06000812 RID: 2066 RVA: 0x00006A57 File Offset: 0x00004C57
	public bool cleared
	{
		get
		{
			return this._cleared;
		}
		set
		{
			this._cleared = value;
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00006A60 File Offset: 0x00004C60
	public PlayerMissionStatsData(string missionId, bool cleared)
	{
		this._missionId = missionId;
		this._cleared = cleared;
	}

	// Token: 0x0400085C RID: 2140
	private string _missionId;

	// Token: 0x0400085D RID: 2141
	private bool _cleared;
}
