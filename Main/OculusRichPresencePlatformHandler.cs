using System;
using Oculus.Platform;

// Token: 0x02000211 RID: 529
public class OculusRichPresencePlatformHandler : IRichPresencePlatformHandler
{
	// Token: 0x0600084D RID: 2125 RVA: 0x00006D25 File Offset: 0x00004F25
	public void SetPresence(IRichPresenceData richPresenceData)
	{
		RichPresenceOptions richPresenceOptions = new RichPresenceOptions();
		richPresenceOptions.SetApiName(richPresenceData.apiName);
		RichPresence.Set(richPresenceOptions);
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00006D3E File Offset: 0x00004F3E
	public void Clear()
	{
		RichPresence.Clear();
	}
}
