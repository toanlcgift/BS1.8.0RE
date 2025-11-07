using System;

// Token: 0x0200002D RID: 45
public interface IAnalyticsModel
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060000BB RID: 187
	bool supportsOpenDataPrivacyPage { get; }

	// Token: 0x060000BC RID: 188
	void OpenDataPrivacyPage();

	// Token: 0x060000BD RID: 189
	void LogEvent(string eventCategory, string eventAction, string eventLabel, long value);
}
