using System;

// Token: 0x02000129 RID: 297
public static class BeatmapDifficultySerializedMethods
{
	// Token: 0x06000488 RID: 1160 RVA: 0x00004C6D File Offset: 0x00002E6D
	public static string SerializedName(this BeatmapDifficulty difficulty)
	{
		if (difficulty == BeatmapDifficulty.Easy)
		{
			return "Easy";
		}
		if (difficulty == BeatmapDifficulty.Normal)
		{
			return "Normal";
		}
		if (difficulty == BeatmapDifficulty.Hard)
		{
			return "Hard";
		}
		if (difficulty == BeatmapDifficulty.Expert)
		{
			return "Expert";
		}
		if (difficulty == BeatmapDifficulty.ExpertPlus)
		{
			return "ExpertPlus";
		}
		return "Unknown";
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00021CB4 File Offset: 0x0001FEB4
	public static bool BeatmapDifficultyFromSerializedName(this string name, out BeatmapDifficulty difficulty)
	{
		if (name == "Easy")
		{
			difficulty = BeatmapDifficulty.Easy;
			return true;
		}
		if (name == "Normal")
		{
			difficulty = BeatmapDifficulty.Normal;
			return true;
		}
		if (name == "Hard")
		{
			difficulty = BeatmapDifficulty.Hard;
			return true;
		}
		if (name == "Expert")
		{
			difficulty = BeatmapDifficulty.Expert;
			return true;
		}
		if (name == "Expert+" || name == "ExpertPlus")
		{
			difficulty = BeatmapDifficulty.ExpertPlus;
			return true;
		}
		difficulty = BeatmapDifficulty.Normal;
		return false;
	}

	// Token: 0x040004DD RID: 1245
	private const string kDifficultyEasySerializedName = "Easy";

	// Token: 0x040004DE RID: 1246
	private const string kDifficultyNormalSerializedName = "Normal";

	// Token: 0x040004DF RID: 1247
	private const string kDifficultyHardSerializedName = "Hard";

	// Token: 0x040004E0 RID: 1248
	private const string kDifficultyExpertSerializedName = "Expert";

	// Token: 0x040004E1 RID: 1249
	private const string kDifficultyExpertPlusNameSerializedLegacy = "Expert+";

	// Token: 0x040004E2 RID: 1250
	private const string kDifficultyExpertPlusSerializedName = "ExpertPlus";

	// Token: 0x040004E3 RID: 1251
	private const string kDifficultyUnknownSerializedName = "Unknown";
}
