using System;
using Polyglot;

// Token: 0x02000214 RID: 532
public class PlayingCampaignRichPresenceData : IRichPresenceData
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000856 RID: 2134 RVA: 0x00006D76 File Offset: 0x00004F76
	public string apiName
	{
		get
		{
			return "campaign";
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000857 RID: 2135 RVA: 0x00006D7D File Offset: 0x00004F7D
	// (set) Token: 0x06000858 RID: 2136 RVA: 0x00006D85 File Offset: 0x00004F85
	public string localizedDescription { get; private set; }

	// Token: 0x06000859 RID: 2137 RVA: 0x00006D8E File Offset: 0x00004F8E
	public PlayingCampaignRichPresenceData()
	{
		this.localizedDescription = Localization.Get("PLAYING_CAMPAIGN_PRESENCE");
	}

	// Token: 0x040008D6 RID: 2262
	private const string kPlayingCampaignRichPresenceLocalizationKey = "PLAYING_CAMPAIGN_PRESENCE";
}
