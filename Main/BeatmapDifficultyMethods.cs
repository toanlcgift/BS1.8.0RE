using System;
using Polyglot;

// Token: 0x0200012A RID: 298
public static class BeatmapDifficultyMethods
{
	// Token: 0x0600048A RID: 1162 RVA: 0x00021D2C File Offset: 0x0001FF2C
	public static string Name(this BeatmapDifficulty difficulty)
	{
		if (difficulty == BeatmapDifficulty.Easy)
		{
			return Localization.Get("DIFFICULTY_EASY");
		}
		if (difficulty == BeatmapDifficulty.Normal)
		{
			return Localization.Get("DIFFICULTY_NORMAL");
		}
		if (difficulty == BeatmapDifficulty.Hard)
		{
			return Localization.Get("DIFFICULTY_HARD");
		}
		if (difficulty == BeatmapDifficulty.Expert)
		{
			return Localization.Get("DIFFICULTY_EXPERT");
		}
		if (difficulty == BeatmapDifficulty.ExpertPlus)
		{
			return Localization.Get("DIFFICULTY_EXPERT_PLUS");
		}
		return Localization.Get("DIFFICULTY_UNKNOWN");
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00021D90 File Offset: 0x0001FF90
	public static string ShortName(this BeatmapDifficulty difficulty)
	{
		if (difficulty == BeatmapDifficulty.Easy)
		{
			return Localization.Get("DIFFICULTY_EASY_SHORT");
		}
		if (difficulty == BeatmapDifficulty.Normal)
		{
			return Localization.Get("DIFFICULTY_NORMAL_SHORT");
		}
		if (difficulty == BeatmapDifficulty.Hard)
		{
			return Localization.Get("DIFFICULTY_HARD_SHORT");
		}
		if (difficulty == BeatmapDifficulty.Expert)
		{
			return Localization.Get("DIFFICULTY_EXPERT_SHORT");
		}
		if (difficulty == BeatmapDifficulty.ExpertPlus)
		{
			return Localization.Get("DIFFICULTY_EXPERT_PLUS_SHORT");
		}
		return Localization.Get("DIFFICULTY_UNKNOWN_SHORT");
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00004CA5 File Offset: 0x00002EA5
	public static int DefaultRating(this BeatmapDifficulty difficulty)
	{
		if (difficulty == BeatmapDifficulty.Easy)
		{
			return 1;
		}
		if (difficulty == BeatmapDifficulty.Normal)
		{
			return 3;
		}
		if (difficulty == BeatmapDifficulty.Hard)
		{
			return 5;
		}
		if (difficulty == BeatmapDifficulty.Expert)
		{
			return 7;
		}
		if (difficulty == BeatmapDifficulty.ExpertPlus)
		{
			return 9;
		}
		return 5;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00004CC6 File Offset: 0x00002EC6
	public static float NoteJumpMovementSpeed(this BeatmapDifficulty difficulty)
	{
		if (difficulty == BeatmapDifficulty.Easy)
		{
			return 10f;
		}
		if (difficulty == BeatmapDifficulty.Normal)
		{
			return 10f;
		}
		if (difficulty == BeatmapDifficulty.Hard)
		{
			return 10f;
		}
		if (difficulty == BeatmapDifficulty.Expert)
		{
			return 12f;
		}
		if (difficulty == BeatmapDifficulty.ExpertPlus)
		{
			return 16f;
		}
		return 5f;
	}
}
