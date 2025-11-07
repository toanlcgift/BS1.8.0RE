using System;
using Polyglot;

// Token: 0x02000215 RID: 533
public class PlayingTutorialPresenceData : IRichPresenceData
{
	// Token: 0x17000252 RID: 594
	// (get) Token: 0x0600085A RID: 2138 RVA: 0x00006DA6 File Offset: 0x00004FA6
	public string apiName
	{
		get
		{
			return "tutorial";
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x0600085B RID: 2139 RVA: 0x00006DAD File Offset: 0x00004FAD
	// (set) Token: 0x0600085C RID: 2140 RVA: 0x00006DB5 File Offset: 0x00004FB5
	public string localizedDescription { get; private set; }

	// Token: 0x0600085D RID: 2141 RVA: 0x00006DBE File Offset: 0x00004FBE
	public PlayingTutorialPresenceData()
	{
		this.localizedDescription = Localization.Get("PLAYING_TUTORIAL_PRESENCE");
	}

	// Token: 0x040008D8 RID: 2264
	private const string kPlayingCampaignRichPresenceLocalizationKey = "PLAYING_TUTORIAL_PRESENCE";
}
