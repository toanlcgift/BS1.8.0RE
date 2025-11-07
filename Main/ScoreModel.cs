using System;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class ScoreModel
{
	// Token: 0x06000896 RID: 2198 RVA: 0x0002AC6C File Offset: 0x00028E6C
	public static int MaxRawScoreForNumberOfNotes(int noteCount)
	{
		int num = 0;
		int i;
		for (i = 1; i < 8; i *= 2)
		{
			if (noteCount < i * 2)
			{
				num += i * noteCount;
				noteCount = 0;
				break;
			}
			num += i * i * 2 + i;
			noteCount -= i * 2;
		}
		num += noteCount * i;
		return num * 115;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0002ACB8 File Offset: 0x00028EB8
	public static void RawScoreWithoutMultiplier(NoteCutInfo noteCutInfo, out int beforeCutRawScore, out int afterCutRawScore, out int cutDistanceRawScore)
	{
		beforeCutRawScore = ((noteCutInfo.swingRatingCounter != null) ? Mathf.RoundToInt(70f * noteCutInfo.swingRatingCounter.beforeCutRating) : 0);
		afterCutRawScore = ((noteCutInfo.swingRatingCounter != null) ? Mathf.RoundToInt(30f * noteCutInfo.swingRatingCounter.afterCutRating) : 0);
		float num = 1f - Mathf.Clamp01(noteCutInfo.cutDistanceToCenter / 0.3f);
		cutDistanceRawScore = Mathf.RoundToInt(15f * num);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00006FAC File Offset: 0x000051AC
	public static int GetModifiedScoreForGameplayModifiersScoreMultiplier(int rawScore, float gameplayModifiersScoreMultiplier)
	{
		return Mathf.FloorToInt((float)rawScore * gameplayModifiersScoreMultiplier);
	}

	// Token: 0x0400090F RID: 2319
	public const int kMaxMultiplier = 8;

	// Token: 0x04000910 RID: 2320
	public const int kMaxBeforeCutSwingRawScore = 70;

	// Token: 0x04000911 RID: 2321
	public const int kMaxCutDistanceRawScore = 15;

	// Token: 0x04000912 RID: 2322
	public const int kMaxAfterCutSwingRawScore = 30;

	// Token: 0x04000913 RID: 2323
	public const int kMaxCutRawScore = 115;

	// Token: 0x04000914 RID: 2324
	private const float kSwingScorePart = 0.7f;

	// Token: 0x04000915 RID: 2325
	private const float kDistanceToCenterScorePart = 0.3f;

	// Token: 0x04000916 RID: 2326
	private const float kMaxDistanceForDistanceToCenterScore = 0.3f;
}
