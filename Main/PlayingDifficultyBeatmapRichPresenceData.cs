using System;
using System.Text;
using Polyglot;

// Token: 0x02000216 RID: 534
public class PlayingDifficultyBeatmapRichPresenceData : IRichPresenceData
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x0600085E RID: 2142 RVA: 0x00006DD6 File Offset: 0x00004FD6
	// (set) Token: 0x0600085F RID: 2143 RVA: 0x00006DDE File Offset: 0x00004FDE
	public string apiName { get; private set; }

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000860 RID: 2144 RVA: 0x00006DE7 File Offset: 0x00004FE7
	// (set) Token: 0x06000861 RID: 2145 RVA: 0x00006DEF File Offset: 0x00004FEF
	public string localizedDescription { get; private set; }

	// Token: 0x06000862 RID: 2146 RVA: 0x00006DF8 File Offset: 0x00004FF8
	public PlayingDifficultyBeatmapRichPresenceData(IDifficultyBeatmap difficultyBeatmap)
	{
		this.apiName = difficultyBeatmap.SerializedName();
		this.localizedDescription = this.GetDestinationLocalizedString(difficultyBeatmap);
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0002A104 File Offset: 0x00028304
	private string GetDestinationLocalizedString(IDifficultyBeatmap difficultyBeatmap)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(difficultyBeatmap.level.songName);
		if (difficultyBeatmap.parentDifficultyBeatmapSet != null && !string.IsNullOrEmpty(difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.compoundIdPartName))
		{
			stringBuilder.Append(" [");
			stringBuilder.Append(Localization.Get(difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.characteristicNameLocalizationKey));
			stringBuilder.Append("]");
		}
		stringBuilder.Append(" [");
		stringBuilder.Append(difficultyBeatmap.difficulty.Name());
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}
}
