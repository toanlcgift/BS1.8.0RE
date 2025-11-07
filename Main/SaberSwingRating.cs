using System;
using UnityEngine;

// Token: 0x0200021E RID: 542
public class SaberSwingRating
{
	// Token: 0x0600087E RID: 2174 RVA: 0x00006F3B File Offset: 0x0000513B
	private static float NormalRating(float normalDiff)
	{
		return 1f - Mathf.Clamp((normalDiff - 75f) / 15f, 0f, 1f);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00006F5F File Offset: 0x0000515F
	public static float BeforeCutStepRating(float angleDiff, float normalDiff)
	{
		return angleDiff * SaberSwingRating.NormalRating(normalDiff) / 100f;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00006F6F File Offset: 0x0000516F
	public static float AfterCutStepRating(float angleDiff, float normalDiff)
	{
		return angleDiff * SaberSwingRating.NormalRating(normalDiff) / 60f;
	}

	// Token: 0x040008F2 RID: 2290
	public const float kMaxNormalAngleDiff = 90f;

	// Token: 0x040008F3 RID: 2291
	public const float kToleranceNormalAngleDiff = 75f;

	// Token: 0x040008F4 RID: 2292
	public const float kMaxBeforeCutSwingDuration = 0.4f;

	// Token: 0x040008F5 RID: 2293
	public const float kMaxAfterCutSwingDuration = 0.4f;

	// Token: 0x040008F6 RID: 2294
	public const float kBeforeCutAngleFor1Rating = 100f;

	// Token: 0x040008F7 RID: 2295
	public const float kAfterCutAngleFor1Rating = 60f;
}
