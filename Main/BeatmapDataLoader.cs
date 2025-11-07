using System;
using System.Collections.Generic;

// Token: 0x02000112 RID: 274
public class BeatmapDataLoader
{
	// Token: 0x06000446 RID: 1094 RVA: 0x00020994 File Offset: 0x0001EB94
	private float GetRealTimeFromBPMTime(float bmpTime, float bpm, float shuffle, float shufflePeriod)
	{
		float num = bmpTime;
		if (shufflePeriod > 0f && (int)(num * (1f / shufflePeriod)) % 2 == 1)
		{
			num += shuffle * shufflePeriod;
		}
		if (bpm > 0f)
		{
			num = num / bpm * 60f;
		}
		return num;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x000209D8 File Offset: 0x0001EBD8
	public BeatmapData GetBeatmapDataFromBeatmapSaveData(List<BeatmapSaveData.NoteData> notesSaveData, List<BeatmapSaveData.ObstacleData> obstaclesSaveData, List<BeatmapSaveData.EventData> eventsSaveData, float startBPM, float shuffle, float shufflePeriod)
	{
		List<BeatmapObjectData>[] array = new List<BeatmapObjectData>[4];
		List<BeatmapEventData> list = new List<BeatmapEventData>(eventsSaveData.Count);
		List<BeatmapDataLoader.BPMChangeData> list2 = new List<BeatmapDataLoader.BPMChangeData>();
		list2.Add(new BeatmapDataLoader.BPMChangeData(0f, 0f, startBPM));
		BeatmapDataLoader.BPMChangeData bpmchangeData = list2[0];
		foreach (BeatmapSaveData.EventData eventData in eventsSaveData)
		{
			if (eventData.type.IsBPMChangeEvent())
			{
				float time = eventData.time;
				int value = eventData.value;
				float bpmChangeStartTime = bpmchangeData.bpmChangeStartTime + this.GetRealTimeFromBPMTime(time - bpmchangeData.bpmChangeStartBPMTime, (float)value, shuffle, shufflePeriod);
				list2.Add(new BeatmapDataLoader.BPMChangeData(bpmChangeStartTime, time, (float)value));
			}
		}
		for (int i = 0; i < 4; i++)
		{
			array[i] = new List<BeatmapObjectData>(3000);
		}
		int num = 0;
		float num2 = -1f;
		List<NoteData> list3 = new List<NoteData>(4);
		List<NoteData> list4 = new List<NoteData>(4);
		int num3 = 0;
		foreach (BeatmapSaveData.NoteData noteData in notesSaveData)
		{
			float time2 = noteData.time;
			while (num3 < list2.Count - 1 && list2[num3 + 1].bpmChangeStartBPMTime < time2)
			{
				num3++;
			}
			BeatmapDataLoader.BPMChangeData bpmchangeData2 = list2[num3];
			float num4 = bpmchangeData2.bpmChangeStartTime + this.GetRealTimeFromBPMTime(time2 - bpmchangeData2.bpmChangeStartBPMTime, bpmchangeData2.bpm, shuffle, shufflePeriod);
			int lineIndex = noteData.lineIndex;
			NoteLineLayer lineLayer = noteData.lineLayer;
			NoteLineLayer startNoteLineLayer = NoteLineLayer.Base;
			NoteType type = noteData.type;
			NoteCutDirection cutDirection = noteData.cutDirection;
			if (list3.Count > 0 && list3[0].time < num4 - 0.001f && type.IsBasicNote())
			{
				this._notesInTimeRowProcessor.ProcessBasicNotesInTimeRow(list3, num4);
				num2 = list3[0].time;
				list3.Clear();
			}
			if (list4.Count > 0 && list4[0].time < num4 - 0.001f)
			{
				this._notesInTimeRowProcessor.ProcessNotesInTimeRow(list4);
				list4.Clear();
			}
			NoteData noteData2 = new NoteData(num++, num4, lineIndex, lineLayer, startNoteLineLayer, type, cutDirection, float.MaxValue, num4 - num2);
			array[lineIndex].Add(noteData2);
			NoteData item = noteData2;
			if (noteData2.noteType.IsBasicNote())
			{
				list3.Add(item);
			}
			list4.Add(item);
		}
		this._notesInTimeRowProcessor.ProcessBasicNotesInTimeRow(list3, float.MaxValue);
		this._notesInTimeRowProcessor.ProcessNotesInTimeRow(list4);
		num3 = 0;
		foreach (BeatmapSaveData.ObstacleData obstacleData in obstaclesSaveData)
		{
			float time3 = obstacleData.time;
			while (num3 < list2.Count - 1 && list2[num3 + 1].bpmChangeStartBPMTime < time3)
			{
				num3++;
			}
			BeatmapDataLoader.BPMChangeData bpmchangeData3 = list2[num3];
			float time4 = bpmchangeData3.bpmChangeStartTime + this.GetRealTimeFromBPMTime(time3 - bpmchangeData3.bpmChangeStartBPMTime, bpmchangeData3.bpm, shuffle, shufflePeriod);
			int lineIndex2 = obstacleData.lineIndex;
			ObstacleType type2 = obstacleData.type;
			float realTimeFromBPMTime = this.GetRealTimeFromBPMTime(obstacleData.duration, startBPM, shuffle, shufflePeriod);
			int width = obstacleData.width;
			ObstacleData item2 = new ObstacleData(num++, time4, lineIndex2, type2, realTimeFromBPMTime, width);
			array[lineIndex2].Add(item2);
		}
		foreach (BeatmapSaveData.EventData eventData2 in eventsSaveData)
		{
			float time5 = eventData2.time;
			while (num3 < list2.Count - 1 && list2[num3 + 1].bpmChangeStartBPMTime < time5)
			{
				num3++;
			}
			BeatmapDataLoader.BPMChangeData bpmchangeData4 = list2[num3];
			float time6 = bpmchangeData4.bpmChangeStartTime + this.GetRealTimeFromBPMTime(time5 - bpmchangeData4.bpmChangeStartBPMTime, bpmchangeData4.bpm, shuffle, shufflePeriod);
			BeatmapEventType type3 = eventData2.type;
			int value2 = eventData2.value;
			BeatmapEventData item3 = new BeatmapEventData(time6, type3, value2);
			list.Add(item3);
		}
		if (list.Count == 0)
		{
			list.Add(new BeatmapEventData(0f, BeatmapEventType.Event0, 1));
			list.Add(new BeatmapEventData(0f, BeatmapEventType.Event4, 1));
		}
		BeatmapLineData[] array2 = new BeatmapLineData[4];
		for (int j = 0; j < 4; j++)
		{
			array[j].Sort(delegate(BeatmapObjectData x, BeatmapObjectData y)
			{
				if (x.time == y.time)
				{
					return 0;
				}
				if (x.time <= y.time)
				{
					return -1;
				}
				return 1;
			});
			array2[j] = new BeatmapLineData();
			array2[j].beatmapObjectsData = array[j].ToArray();
		}
		return new BeatmapData(array2, list.ToArray());
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00020F1C File Offset: 0x0001F11C
	public BeatmapData GetBeatmapDataFromBinary(byte[] data, float startBPM, float shuffle, float shufflePeriod)
	{
		BeatmapSaveData beatmapSaveData = BeatmapSaveData.DeserializeFromFromBinary(data);
		if (beatmapSaveData != null)
		{
			List<BeatmapSaveData.NoteData> notes = beatmapSaveData.notes;
			List<BeatmapSaveData.ObstacleData> obstacles = beatmapSaveData.obstacles;
			List<BeatmapSaveData.EventData> events = beatmapSaveData.events;
			return this.GetBeatmapDataFromBeatmapSaveData(notes, obstacles, events, startBPM, shuffle, shufflePeriod);
		}
		return null;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00020F5C File Offset: 0x0001F15C
	public BeatmapData GetBeatmapDataFromJson(string json, float startBPM, float shuffle, float shufflePeriod)
	{
		BeatmapSaveData beatmapSaveData = BeatmapSaveData.DeserializeFromJSONString(json);
		if (beatmapSaveData != null)
		{
			List<BeatmapSaveData.NoteData> notes = beatmapSaveData.notes;
			List<BeatmapSaveData.ObstacleData> obstacles = beatmapSaveData.obstacles;
			List<BeatmapSaveData.EventData> events = beatmapSaveData.events;
			return this.GetBeatmapDataFromBeatmapSaveData(notes, obstacles, events, startBPM, shuffle, shufflePeriod);
		}
		return null;
	}

	// Token: 0x04000492 RID: 1170
	private BeatmapDataLoader.NotesInTimeRowProcessor _notesInTimeRowProcessor = new BeatmapDataLoader.NotesInTimeRowProcessor();

	// Token: 0x02000113 RID: 275
	private struct BPMChangeData
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00004A61 File Offset: 0x00002C61
		public BPMChangeData(float bpmChangeStartTime, float bpmChangeStartBPMTime, float bpm)
		{
			this.bpmChangeStartTime = bpmChangeStartTime;
			this.bpmChangeStartBPMTime = bpmChangeStartBPMTime;
			this.bpm = bpm;
		}

		// Token: 0x04000493 RID: 1171
		public readonly float bpmChangeStartTime;

		// Token: 0x04000494 RID: 1172
		public readonly float bpmChangeStartBPMTime;

		// Token: 0x04000495 RID: 1173
		public readonly float bpm;
	}

	// Token: 0x02000114 RID: 276
	private class NotesInTimeRowProcessor
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00020F9C File Offset: 0x0001F19C
		public void ProcessBasicNotesInTimeRow(List<NoteData> basicNotes, float nextBasicNoteTimeSlice)
		{
			for (int i = 0; i < basicNotes.Count; i++)
			{
				NoteData noteData = basicNotes[i];
				noteData.timeToNextBasicNote = nextBasicNoteTimeSlice - noteData.time;
			}
			if (basicNotes.Count == 2)
			{
				NoteData noteData2 = basicNotes[0];
				NoteData noteData3 = basicNotes[1];
				if (noteData2.noteType != noteData3.noteType && ((noteData2.noteType == NoteType.NoteA && noteData2.lineIndex > noteData3.lineIndex) || (noteData2.noteType == NoteType.NoteB && noteData2.lineIndex < noteData3.lineIndex)))
				{
					noteData2.SetNoteFlipToNote(noteData3);
					noteData3.SetNoteFlipToNote(noteData2);
				}
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00021034 File Offset: 0x0001F234
		public void ProcessNotesInTimeRow(List<NoteData> notes)
		{
			this._numberOfNotesInLines.Clear();
			for (int i = 0; i < notes.Count; i++)
			{
				NoteData noteData = notes[i];
				int num;
				if (this._numberOfNotesInLines.TryGetValue(noteData.lineIndex, out num))
				{
					if (num == 1)
					{
						noteData.SetNoteStartLineLayer(NoteLineLayer.Upper);
					}
					else if (num == 2)
					{
						noteData.SetNoteStartLineLayer(NoteLineLayer.Top);
					}
					Dictionary<int, int> numberOfNotesInLines = this._numberOfNotesInLines;
					int lineIndex = noteData.lineIndex;
					int num2 = numberOfNotesInLines[lineIndex];
					numberOfNotesInLines[lineIndex] = num2 + 1;
				}
				else
				{
					this._numberOfNotesInLines[noteData.lineIndex] = 1;
				}
			}
		}

		// Token: 0x04000496 RID: 1174
		private Dictionary<int, int> _numberOfNotesInLines = new Dictionary<int, int>();
	}
}
