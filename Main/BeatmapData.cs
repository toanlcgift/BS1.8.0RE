using System;

// Token: 0x020000FC RID: 252
public class BeatmapData
{
	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060003CD RID: 973 RVA: 0x000044D2 File Offset: 0x000026D2
	// (set) Token: 0x060003CE RID: 974 RVA: 0x000044DA File Offset: 0x000026DA
	public BeatmapLineData[] beatmapLinesData { get; private set; }

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060003CF RID: 975 RVA: 0x000044E3 File Offset: 0x000026E3
	// (set) Token: 0x060003D0 RID: 976 RVA: 0x000044EB File Offset: 0x000026EB
	public BeatmapEventData[] beatmapEventData { get; private set; }

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x000044F4 File Offset: 0x000026F4
	// (set) Token: 0x060003D2 RID: 978 RVA: 0x000044FC File Offset: 0x000026FC
	public int notesCount { get; private set; }

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060003D3 RID: 979 RVA: 0x00004505 File Offset: 0x00002705
	// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000450D File Offset: 0x0000270D
	public int obstaclesCount { get; private set; }

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060003D5 RID: 981 RVA: 0x00004516 File Offset: 0x00002716
	// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000451E File Offset: 0x0000271E
	public int bombsCount { get; private set; }

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060003D7 RID: 983 RVA: 0x00004527 File Offset: 0x00002727
	// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000452F File Offset: 0x0000272F
	public int spawnRotationEventsCount { get; private set; }

	// Token: 0x060003D9 RID: 985 RVA: 0x0002039C File Offset: 0x0001E59C
	public BeatmapData(BeatmapLineData[] beatmapLinesData, BeatmapEventData[] beatmapEventData)
	{
		this.beatmapLinesData = beatmapLinesData;
		this.beatmapEventData = beatmapEventData;
		for (int i = 0; i < beatmapLinesData.Length; i++)
		{
			foreach (BeatmapObjectData beatmapObjectData in beatmapLinesData[i].beatmapObjectsData)
			{
				if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
				{
					NoteType noteType = ((NoteData)beatmapObjectData).noteType;
					if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
					{
						int num = this.notesCount;
						this.notesCount = num + 1;
					}
					else if (noteType == NoteType.Bomb)
					{
						int num = this.bombsCount;
						this.bombsCount = num + 1;
					}
				}
				else if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
				{
					int num = this.obstaclesCount;
					this.obstaclesCount = num + 1;
				}
			}
		}
		for (int i = 0; i < beatmapEventData.Length; i++)
		{
			if (beatmapEventData[i].type.IsRotationEvent())
			{
				int j = this.spawnRotationEventsCount;
				this.spawnRotationEventsCount = j + 1;
			}
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0002048C File Offset: 0x0001E68C
	public BeatmapData GetCopy()
	{
		BeatmapLineData[] beatmapLineDataCopy = this.GetBeatmapLineDataCopy();
		BeatmapEventData[] beatmapEventDataCopy = this.GetBeatmapEventDataCopy();
		return new BeatmapData(beatmapLineDataCopy, beatmapEventDataCopy);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000204AC File Offset: 0x0001E6AC
	public BeatmapLineData[] GetBeatmapLineDataCopy()
	{
		BeatmapLineData[] array = new BeatmapLineData[this.beatmapLinesData.Length];
		for (int i = 0; i < this.beatmapLinesData.Length; i++)
		{
			BeatmapLineData beatmapLineData = this.beatmapLinesData[i];
			BeatmapLineData beatmapLineData2 = new BeatmapLineData();
			beatmapLineData2.beatmapObjectsData = new BeatmapObjectData[beatmapLineData.beatmapObjectsData.Length];
			for (int j = 0; j < beatmapLineData.beatmapObjectsData.Length; j++)
			{
				BeatmapObjectData beatmapObjectData = beatmapLineData.beatmapObjectsData[j];
				beatmapLineData2.beatmapObjectsData[j] = beatmapObjectData.GetCopy();
			}
			array[i] = beatmapLineData2;
		}
		return array;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00020534 File Offset: 0x0001E734
	public BeatmapEventData[] GetBeatmapEventDataCopy()
	{
		BeatmapEventData[] array = new BeatmapEventData[this.beatmapEventData.Length];
		for (int i = 0; i < this.beatmapEventData.Length; i++)
		{
			BeatmapEventData beatmapEventData = this.beatmapEventData[i];
			array[i] = beatmapEventData;
		}
		return array;
	}
}
