using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class RankModel
{
	// Token: 0x06000847 RID: 2119 RVA: 0x0002A00C File Offset: 0x0002820C
	public static string GetRankName(RankModel.Rank rank)
	{
		switch (rank)
		{
		case RankModel.Rank.E:
			return "E";
		case RankModel.Rank.D:
			return "D";
		case RankModel.Rank.C:
			return "C";
		case RankModel.Rank.B:
			return "B";
		case RankModel.Rank.A:
			return "A";
		case RankModel.Rank.S:
			return "S";
		case RankModel.Rank.SS:
			return "SS";
		case RankModel.Rank.SSS:
			return "SSS";
		default:
			return "XXX";
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0002A078 File Offset: 0x00028278
	public static RankModel.Rank MaxRankForGameplayModifiers(GameplayModifiers gameplayModifiers, GameplayModifiersModelSO gameplayModifiersModel)
	{
		int maxRawScore = 1000000;
		int num = gameplayModifiersModel.MaxModifiedScoreForMaxRawScore(maxRawScore, gameplayModifiers);
		return RankModel.GetRankForScore(0, num, maxRawScore, num);
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0002A0A0 File Offset: 0x000282A0
	public static RankModel.Rank GetRankForScore(int rawScore, int modifiedScore, int maxRawScore, int maxModifiedScore)
	{
		int num = Mathf.Max(maxRawScore, maxModifiedScore);
		float num2 = (float)modifiedScore / (float)num;
		if (rawScore == maxRawScore && modifiedScore >= rawScore)
		{
			return RankModel.Rank.SSS;
		}
		if (num2 > 0.9f)
		{
			return RankModel.Rank.SS;
		}
		if (num2 > 0.8f)
		{
			return RankModel.Rank.S;
		}
		if (num2 > 0.65f)
		{
			return RankModel.Rank.A;
		}
		if (num2 > 0.5f)
		{
			return RankModel.Rank.B;
		}
		if (num2 > 0.35f)
		{
			return RankModel.Rank.C;
		}
		if (num2 > 0.2f)
		{
			return RankModel.Rank.D;
		}
		return RankModel.Rank.E;
	}

	// Token: 0x0200020F RID: 527
	public enum Rank
	{
		// Token: 0x040008C9 RID: 2249
		E,
		// Token: 0x040008CA RID: 2250
		D,
		// Token: 0x040008CB RID: 2251
		C,
		// Token: 0x040008CC RID: 2252
		B,
		// Token: 0x040008CD RID: 2253
		A,
		// Token: 0x040008CE RID: 2254
		S,
		// Token: 0x040008CF RID: 2255
		SS,
		// Token: 0x040008D0 RID: 2256
		SSS
	}
}
