using System;

// Token: 0x02000030 RID: 48
public class NoAnalyticsModel : IAnalyticsModel
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002907 File Offset: 0x00000B07
	public bool supportsOpenDataPrivacyPage
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000023E9 File Offset: 0x000005E9
	public void OpenDataPrivacyPage()
	{
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000023E9 File Offset: 0x000005E9
	public void LogEvent(string eventCategory, string eventAction, string eventLabel, long value)
	{
	}
}
