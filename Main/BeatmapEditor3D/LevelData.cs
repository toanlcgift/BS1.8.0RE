using System;
using System.Collections.Generic;
using BeatmapEditor;

namespace BeatmapEditor3D
{
	// Token: 0x020004DA RID: 1242
	public class LevelData
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00010F14 File Offset: 0x0000F114
		public float songDuration
		{
			get
			{
				return this.editorAudio.songDuration;
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00010F21 File Offset: 0x0000F121
		public void SetBeatmapSaveData(string characteristic, BeatmapDifficulty difficulty, BeatmapSaveData beatmapSaveData)
		{
			if (!this._characteristicDifficultyBeatmap.ContainsKey(characteristic))
			{
				this._characteristicDifficultyBeatmap[characteristic] = new Dictionary<BeatmapDifficulty, BeatmapSaveData>();
			}
			this._characteristicDifficultyBeatmap[characteristic][difficulty] = beatmapSaveData;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00010F55 File Offset: 0x0000F155
		public bool ContainsBeatmapSaveData(string characteristic, BeatmapDifficulty difficulty)
		{
			return this.GetBeatmapSaveData(characteristic, difficulty) != null;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00053470 File Offset: 0x00051670
		public BeatmapSaveData GetBeatmapSaveData(string characteristic, BeatmapDifficulty difficulty)
		{
			if (this._characteristicDifficultyBeatmap.ContainsKey(characteristic))
			{
				Dictionary<BeatmapDifficulty, BeatmapSaveData> dictionary = this._characteristicDifficultyBeatmap[characteristic];
				if (dictionary.ContainsKey(difficulty))
				{
					return dictionary[difficulty];
				}
			}
			return null;
		}

		// Token: 0x040016F1 RID: 5873
		private Dictionary<string, Dictionary<BeatmapDifficulty, BeatmapSaveData>> _characteristicDifficultyBeatmap = new Dictionary<string, Dictionary<BeatmapDifficulty, BeatmapSaveData>>();

		// Token: 0x040016F2 RID: 5874
		public string directoryPath;

		// Token: 0x040016F3 RID: 5875
		public StandardLevelInfoSaveData levelInfo;

		// Token: 0x040016F4 RID: 5876
		public EditorAudioSO editorAudio;

		// Token: 0x040016F5 RID: 5877
		public EditorLevelCoverImageSO coverImage;
	}
}
