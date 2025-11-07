using System;
using Steamworks;

// Token: 0x0200021A RID: 538
public class SteamRichPresencePlatformHandler : IRichPresencePlatformHandler
{
	// Token: 0x0600086D RID: 2157 RVA: 0x00006E93 File Offset: 0x00005093
	public void SetPresence(IRichPresenceData richPresenceData)
	{
		if (SteamManager.Initialized)
		{
			SteamFriends.SetRichPresence("steam_display", "#StatusWithMessage");
			SteamFriends.SetRichPresence("StatusMessage", richPresenceData.localizedDescription);
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00006EBD File Offset: 0x000050BD
	public void Clear()
	{
		if (SteamManager.Initialized)
		{
			SteamFriends.ClearRichPresence();
		}
	}
}
