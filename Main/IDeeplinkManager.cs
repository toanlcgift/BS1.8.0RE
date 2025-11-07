using System;

// Token: 0x020000B8 RID: 184
public interface IDeeplinkManager
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x0600029C RID: 668
	// (remove) Token: 0x0600029D RID: 669
	event Action<Deeplink> didReceiveDeeplinkEvent;

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600029E RID: 670
	Deeplink currentDeeplink { get; }
}
