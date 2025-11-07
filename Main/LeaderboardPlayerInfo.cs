using System;

// Token: 0x020000D3 RID: 211
public class LeaderboardPlayerInfo
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000319 RID: 793 RVA: 0x00003F81 File Offset: 0x00002181
	// (set) Token: 0x0600031A RID: 794 RVA: 0x00003F89 File Offset: 0x00002189
	public string playerId { get; private set; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x0600031B RID: 795 RVA: 0x00003F92 File Offset: 0x00002192
	// (set) Token: 0x0600031C RID: 796 RVA: 0x00003F9A File Offset: 0x0000219A
	public string playerName { get; private set; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x0600031D RID: 797 RVA: 0x00003FA3 File Offset: 0x000021A3
	// (set) Token: 0x0600031E RID: 798 RVA: 0x00003FAB File Offset: 0x000021AB
	public string playerKey { get; private set; }

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x0600031F RID: 799 RVA: 0x00003FB4 File Offset: 0x000021B4
	// (set) Token: 0x06000320 RID: 800 RVA: 0x00003FBC File Offset: 0x000021BC
	public string authType { get; private set; }

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000321 RID: 801 RVA: 0x00003FC5 File Offset: 0x000021C5
	// (set) Token: 0x06000322 RID: 802 RVA: 0x00003FCD File Offset: 0x000021CD
	public string playerFriends { get; private set; }

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000323 RID: 803 RVA: 0x00003FD6 File Offset: 0x000021D6
	// (set) Token: 0x06000324 RID: 804 RVA: 0x00003FDE File Offset: 0x000021DE
	public bool succeeded { get; private set; }

	// Token: 0x06000325 RID: 805 RVA: 0x00003FE7 File Offset: 0x000021E7
	public LeaderboardPlayerInfo(bool succeeded, string playerId, string playerName, string playerKey, string authType, string playerFriends)
	{
		this.playerId = playerId;
		this.playerName = playerName;
		this.playerKey = playerKey;
		this.authType = authType;
		this.playerFriends = playerFriends;
		this.succeeded = succeeded;
	}

	// Token: 0x0400038B RID: 907
	public string serverKey;
}
