using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022C RID: 556
public class BeatmapCallbackItemDataList
{
	// Token: 0x060008B5 RID: 2229 RVA: 0x0002B358 File Offset: 0x00029558
	public BeatmapCallbackItemDataList(BeatmapCallbackItemDataList.SpawnNoteCallback spawnNoteCallback, BeatmapCallbackItemDataList.SpawnObstacleCallback spawnObstacleCallback, BeatmapCallbackItemDataList.ProcessBeatmapEventCallback processEarlyBeatmapEventCallback, BeatmapCallbackItemDataList.ProcessBeatmapEventCallback processLateBeatmapEventCallback, Action earlyEventsWereProcessedCallback, BeatmapCallbackItemDataList.GetRelativeNoteOffsetCallback getRelativeNoteOffsetCallback)
	{
		this.spawnNoteCallback = spawnNoteCallback;
		this.spawnObstacleCallback = spawnObstacleCallback;
		this.processEarlyBeatmapEventCallback = processEarlyBeatmapEventCallback;
		this.processLateBeatmapEventCallback = processLateBeatmapEventCallback;
		this.earlyEventsWereProcessedCallback = earlyEventsWereProcessedCallback;
		this.getRelativeNoteOffsetCallback = getRelativeNoteOffsetCallback;
		foreach (NoteType key in (NoteType[])Enum.GetValues(typeof(NoteType)))
		{
			this._notesByType[key] = new List<NoteData>(4);
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0002B428 File Offset: 0x00029628
	public void InsertBeatmapObjectData(BeatmapObjectData beatmapObjectData)
	{
		bool flag = false;
		for (int i = this._beatmapObjectDataList.Count; i > 0; i--)
		{
			if (this._beatmapObjectDataList[i - 1].time <= beatmapObjectData.time)
			{
				flag = true;
				this._beatmapObjectDataList.Insert(i, beatmapObjectData);
				break;
			}
		}
		if (!flag)
		{
			this._beatmapObjectDataList.Insert(0, beatmapObjectData);
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0002B48C File Offset: 0x0002968C
	public void InsertBeatmapEventData(BeatmapEventData beatmapEventData)
	{
		bool flag = false;
		for (int i = this._beatmapEventDataList.Count; i > 0; i--)
		{
			if (this._beatmapEventDataList[i - 1].time <= beatmapEventData.time)
			{
				flag = true;
				this._beatmapEventDataList.Insert(i, beatmapEventData);
				break;
			}
		}
		if (!flag)
		{
			this._beatmapEventDataList.Insert(0, beatmapEventData);
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0002B4F0 File Offset: 0x000296F0
	public void ProcessData()
	{
		int num = 0;
		int num2 = 0;
		BeatmapObjectData beatmapObjectData = (this._beatmapObjectDataList.Count > 0) ? this._beatmapObjectDataList[0] : null;
		BeatmapEventData beatmapEventData = (this._beatmapEventDataList.Count > 0) ? this._beatmapEventDataList[0] : null;
		float num3 = 0f;
		if (beatmapObjectData != null)
		{
			num3 = beatmapObjectData.time;
		}
		if (beatmapEventData != null && beatmapEventData.time < num3)
		{
			num3 = beatmapEventData.time;
		}
		while (num < this._beatmapObjectDataList.Count || num2 < this._beatmapEventDataList.Count)
		{
			if (beatmapEventData == null || (beatmapObjectData != null && beatmapObjectData.time <= beatmapEventData.time))
			{
				if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
				{
					NoteData noteData = (NoteData)beatmapObjectData;
					this._notesByType[noteData.noteType].Add(noteData);
				}
				else if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
				{
					this._obstacles.Add((ObstacleData)beatmapObjectData);
				}
				num++;
				beatmapObjectData = ((num < this._beatmapObjectDataList.Count) ? this._beatmapObjectDataList[num] : null);
			}
			else if (beatmapObjectData == null || (beatmapEventData != null && beatmapEventData.time <= beatmapObjectData.time))
			{
				if (beatmapEventData.type.IsEarlyEvent())
				{
					this._beatmapEarlyEvents.Add(beatmapEventData);
				}
				else
				{
					this._beatmapLateEvents.Add(beatmapEventData);
				}
				num2++;
				beatmapEventData = ((num2 < this._beatmapEventDataList.Count) ? this._beatmapEventDataList[num2] : null);
			}
			float num4 = (beatmapObjectData != null) ? beatmapObjectData.time : float.MaxValue;
			if (beatmapEventData != null && beatmapEventData.time < num4)
			{
				num4 = beatmapEventData.time;
			}
			if (num4 > num3)
			{
				foreach (BeatmapEventData beatmapEventData2 in this._beatmapEarlyEvents)
				{
					this.processEarlyBeatmapEventCallback(beatmapEventData2);
				}
				this._beatmapEarlyEvents.Clear();
				this.earlyEventsWereProcessedCallback();
				foreach (KeyValuePair<NoteType, List<NoteData>> keyValuePair in this._notesByType)
				{
					List<NoteData> value = keyValuePair.Value;
					if (value.Count != 0)
					{
						if (!this.ProcessNotesByType(value))
						{
							foreach (NoteData noteData2 in value)
							{
								this.spawnNoteCallback(noteData2, 0f);
							}
						}
						keyValuePair.Value.Clear();
					}
				}
				foreach (ObstacleData obstacleData in this._obstacles)
				{
					this.spawnObstacleCallback(obstacleData);
				}
				this._obstacles.Clear();
				foreach (BeatmapEventData beatmapEventData3 in this._beatmapLateEvents)
				{
					this.processLateBeatmapEventCallback(beatmapEventData3);
				}
				this._beatmapLateEvents.Clear();
				num3 = num4;
			}
		}
		this._beatmapObjectDataList.Clear();
		this._beatmapEventDataList.Clear();
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0002B874 File Offset: 0x00029A74
	private bool ProcessNotesByType(List<NoteData> notesWithTheSameTypeList)
	{
		if (notesWithTheSameTypeList.Count != 2)
		{
			return false;
		}
		NoteData noteData = notesWithTheSameTypeList[0];
		if (!noteData.noteType.IsBasicNote())
		{
			return false;
		}
		NoteData noteData2 = notesWithTheSameTypeList[1];
		if (noteData.cutDirection != noteData2.cutDirection && noteData.cutDirection != NoteCutDirection.Any && noteData2.cutDirection != NoteCutDirection.Any)
		{
			return false;
		}
		NoteData noteData3;
		NoteData noteData4;
		if (noteData.cutDirection != NoteCutDirection.Any)
		{
			noteData3 = noteData;
			noteData4 = noteData2;
		}
		else
		{
			noteData3 = noteData2;
			noteData4 = noteData;
		}
		Vector2 line = this.getRelativeNoteOffsetCallback(noteData4.lineIndex, noteData4.noteLineLayer) - this.getRelativeNoteOffsetCallback(noteData3.lineIndex, noteData3.noteLineLayer);
		float num = ((noteData3.cutDirection == NoteCutDirection.Any) ? new Vector2(0f, 1f) : noteData3.cutDirection.Direction()).SignedAngleToLine(line);
		if (noteData4.cutDirection == NoteCutDirection.Any && noteData3.cutDirection == NoteCutDirection.Any)
		{
			this.spawnNoteCallback(noteData3, num);
			this.spawnNoteCallback(noteData4, num);
		}
		else
		{
			if (Mathf.Abs(num) > this._maxNotesAlignmentAngle)
			{
				return false;
			}
			this.spawnNoteCallback(noteData3, num);
			if (noteData4.cutDirection == NoteCutDirection.Any && !noteData3.cutDirection.IsMainDirection())
			{
				this.spawnNoteCallback(noteData4, num + 45f);
			}
			else
			{
				this.spawnNoteCallback(noteData4, num);
			}
		}
		return true;
	}

	// Token: 0x04000931 RID: 2353
	private BeatmapCallbackItemDataList.SpawnNoteCallback spawnNoteCallback;

	// Token: 0x04000932 RID: 2354
	private BeatmapCallbackItemDataList.SpawnObstacleCallback spawnObstacleCallback;

	// Token: 0x04000933 RID: 2355
	private BeatmapCallbackItemDataList.ProcessBeatmapEventCallback processEarlyBeatmapEventCallback;

	// Token: 0x04000934 RID: 2356
	private BeatmapCallbackItemDataList.ProcessBeatmapEventCallback processLateBeatmapEventCallback;

	// Token: 0x04000935 RID: 2357
	private Action earlyEventsWereProcessedCallback;

	// Token: 0x04000936 RID: 2358
	private BeatmapCallbackItemDataList.GetRelativeNoteOffsetCallback getRelativeNoteOffsetCallback;

	// Token: 0x04000937 RID: 2359
	private List<BeatmapObjectData> _beatmapObjectDataList = new List<BeatmapObjectData>(10);

	// Token: 0x04000938 RID: 2360
	private List<BeatmapEventData> _beatmapEventDataList = new List<BeatmapEventData>(10);

	// Token: 0x04000939 RID: 2361
	private Dictionary<NoteType, List<NoteData>> _notesByType = new Dictionary<NoteType, List<NoteData>>();

	// Token: 0x0400093A RID: 2362
	private List<ObstacleData> _obstacles = new List<ObstacleData>(4);

	// Token: 0x0400093B RID: 2363
	private List<BeatmapEventData> _beatmapEarlyEvents = new List<BeatmapEventData>(4);

	// Token: 0x0400093C RID: 2364
	private List<BeatmapEventData> _beatmapLateEvents = new List<BeatmapEventData>(4);

	// Token: 0x0400093D RID: 2365
	private float _maxNotesAlignmentAngle = 40f;

	// Token: 0x0200022D RID: 557
	// (Invoke) Token: 0x060008BB RID: 2235
	public delegate void SpawnNoteCallback(NoteData noteData, float cutDirectionAngleOffset);

	// Token: 0x0200022E RID: 558
	// (Invoke) Token: 0x060008BF RID: 2239
	public delegate void SpawnObstacleCallback(ObstacleData obstacleData);

	// Token: 0x0200022F RID: 559
	// (Invoke) Token: 0x060008C3 RID: 2243
	public delegate void ProcessBeatmapEventCallback(BeatmapEventData beatmapEventData);

	// Token: 0x02000230 RID: 560
	// (Invoke) Token: 0x060008C7 RID: 2247
	public delegate Vector2 GetRelativeNoteOffsetCallback(int lineIndex, NoteLineLayer noteLineLayer);
}
