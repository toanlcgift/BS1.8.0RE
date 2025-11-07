using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class TestAnalyticsModel : IAnalyticsModel
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060000BE RID: 190 RVA: 0x00002907 File Offset: 0x00000B07
	public bool supportsOpenDataPrivacyPage
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000023E9 File Offset: 0x000005E9
	public void OpenDataPrivacyPage()
	{
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000290A File Offset: 0x00000B0A
	public void LogEvent(string eventCategory, string eventAction, string eventLabel, long value)
	{
		Debug.Log(string.Format("{0} {1} {2} {3}", new object[]
		{
			eventCategory,
			eventAction,
			eventLabel,
			value
		}));
	}
}
