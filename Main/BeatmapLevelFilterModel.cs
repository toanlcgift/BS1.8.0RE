using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000131 RID: 305
public static class BeatmapLevelFilterModel
{
	// Token: 0x0600049D RID: 1181 RVA: 0x00021E78 File Offset: 0x00020078
	public static IBeatmapLevelCollection FilerBeatmapLevelPackCollection(IBeatmapLevelPackCollection beatmapLevelPackCollection, BeatmapLevelFilterModel.LevelFilterParams levelFilterParams)
	{
		IEnumerable<IPreviewBeatmapLevel> source = BeatmapLevelFilterModel.GetAllBeatmapLevels(beatmapLevelPackCollection);
		switch (levelFilterParams.filterBy)
		{
		case BeatmapLevelFilterModel.LevelFilterParams.FilterBy.BeatmapLevelIds:
			source = from beatmapLevel in source
			where levelFilterParams.beatmapLevelIds.Contains(beatmapLevel.levelID)
			select beatmapLevel;
			break;
		case BeatmapLevelFilterModel.LevelFilterParams.FilterBy.BeatmapCharacteristic:
			source = from beatmapLevel in source
			where beatmapLevel.previewDifficultyBeatmapSets.GetBeatmapCharacteristics().Contains(levelFilterParams.beatmapCharacteristic)
			select beatmapLevel;
			break;
		}
		return new BeatmapLevelCollection(source.ToArray<IPreviewBeatmapLevel>());
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00021EEC File Offset: 0x000200EC
	private static IEnumerable<IPreviewBeatmapLevel> GetAllBeatmapLevels(IBeatmapLevelPackCollection beatmapLevelPackCollection)
	{
		List<IPreviewBeatmapLevel> list = new List<IPreviewBeatmapLevel>();
		IBeatmapLevelPack[] beatmapLevelPacks = beatmapLevelPackCollection.beatmapLevelPacks;
		for (int i = 0; i < beatmapLevelPacks.Length; i++)
		{
			foreach (IPreviewBeatmapLevel item in beatmapLevelPacks[i].beatmapLevelCollection.beatmapLevels)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00021F44 File Offset: 0x00020144
	private static IPreviewBeatmapLevel[] FilterByLevelIds(IBeatmapLevelPackCollection beatmapLevelPackCollection, HashSet<string> beatmapLevelIds)
	{
		List<IPreviewBeatmapLevel> list = new List<IPreviewBeatmapLevel>();
		IBeatmapLevelPack[] beatmapLevelPacks = beatmapLevelPackCollection.beatmapLevelPacks;
		for (int i = 0; i < beatmapLevelPacks.Length; i++)
		{
			foreach (IPreviewBeatmapLevel previewBeatmapLevel in beatmapLevelPacks[i].beatmapLevelCollection.beatmapLevels)
			{
				if (beatmapLevelIds.Contains(previewBeatmapLevel.levelID))
				{
					list.Add(previewBeatmapLevel);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x02000132 RID: 306
	public class LevelFilterParams
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00004D71 File Offset: 0x00002F71
		internal LevelFilterParams()
		{
			this.filterBy = BeatmapLevelFilterModel.LevelFilterParams.FilterBy.AllLevels;
			this.beatmapLevelIds = null;
			this.beatmapCharacteristic = null;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00004D8E File Offset: 0x00002F8E
		internal LevelFilterParams(HashSet<string> beatmapLevelIds)
		{
			this.filterBy = BeatmapLevelFilterModel.LevelFilterParams.FilterBy.BeatmapLevelIds;
			this.beatmapLevelIds = beatmapLevelIds;
			this.beatmapCharacteristic = null;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00004DAB File Offset: 0x00002FAB
		internal LevelFilterParams(BeatmapCharacteristicSO beatmapCharacteristic)
		{
			this.filterBy = BeatmapLevelFilterModel.LevelFilterParams.FilterBy.BeatmapCharacteristic;
			this.beatmapLevelIds = null;
			this.beatmapCharacteristic = beatmapCharacteristic;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public static BeatmapLevelFilterModel.LevelFilterParams NoFilter()
		{
			return new BeatmapLevelFilterModel.LevelFilterParams();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00004DCF File Offset: 0x00002FCF
		public static BeatmapLevelFilterModel.LevelFilterParams ByBeatmapLevelIds(HashSet<string> beatmapLevelIds)
		{
			return new BeatmapLevelFilterModel.LevelFilterParams(beatmapLevelIds);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00004DD7 File Offset: 0x00002FD7
		public static BeatmapLevelFilterModel.LevelFilterParams ByBeatmapCharacteristic(BeatmapCharacteristicSO beatmapCharacteristic)
		{
			return new BeatmapLevelFilterModel.LevelFilterParams(beatmapCharacteristic);
		}

		// Token: 0x040004ED RID: 1261
		public BeatmapLevelFilterModel.LevelFilterParams.FilterBy filterBy;

		// Token: 0x040004EE RID: 1262
		public readonly HashSet<string> beatmapLevelIds;

		// Token: 0x040004EF RID: 1263
		public readonly BeatmapCharacteristicSO beatmapCharacteristic;

		// Token: 0x02000133 RID: 307
		public enum FilterBy
		{
			// Token: 0x040004F1 RID: 1265
			AllLevels,
			// Token: 0x040004F2 RID: 1266
			BeatmapLevelIds,
			// Token: 0x040004F3 RID: 1267
			BeatmapCharacteristic
		}
	}
}
