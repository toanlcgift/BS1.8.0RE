using System;
using Polyglot;

// Token: 0x02000213 RID: 531
public class BrowsingMenusRichPresenceData : IRichPresenceData
{
	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x00006D46 File Offset: 0x00004F46
	public string apiName
	{
		get
		{
			return "menu";
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000853 RID: 2131 RVA: 0x00006D4D File Offset: 0x00004F4D
	// (set) Token: 0x06000854 RID: 2132 RVA: 0x00006D55 File Offset: 0x00004F55
	public string localizedDescription { get; private set; }

	// Token: 0x06000855 RID: 2133 RVA: 0x00006D5E File Offset: 0x00004F5E
	public BrowsingMenusRichPresenceData()
	{
		this.localizedDescription = Localization.Get("BROWSING_MENUS_PRESENCE");
	}

	// Token: 0x040008D4 RID: 2260
	private const string kBrowsingMenusRichPresenceLocalizationKey = "BROWSING_MENUS_PRESENCE";
}
