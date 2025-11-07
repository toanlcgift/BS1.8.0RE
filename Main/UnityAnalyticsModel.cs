using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

// Token: 0x02000031 RID: 49
public class UnityAnalyticsModel : IAnalyticsModel
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x060000CA RID: 202 RVA: 0x00002969 File Offset: 0x00000B69
	public bool supportsOpenDataPrivacyPage
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000296C File Offset: 0x00000B6C
	public void OpenDataPrivacyPage()
	{
		DataPrivacy.FetchPrivacyUrl(delegate(string url)
		{
			Application.OpenURL(url);
		}, null);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00002993 File Offset: 0x00000B93
	public void LogEvent(string eventCategory, string eventAction, string eventLabel, long value)
	{
		Analytics.CustomEvent(eventCategory + " " + eventAction, new Dictionary<string, object>
		{
			{
				"LevelId",
				eventLabel
			},
			{
				"Score",
				value
			}
		});
	}
}
