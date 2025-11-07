using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class TestRichPresencePlatformHandler : IRichPresencePlatformHandler
{
	// Token: 0x0600086A RID: 2154 RVA: 0x00006E7A File Offset: 0x0000507A
	public void SetPresence(IRichPresenceData richPresenceData)
	{
		Debug.Log(richPresenceData.localizedDescription);
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00006E87 File Offset: 0x00005087
	public void Clear()
	{
		Debug.Log("Clear RichPresence");
	}
}
