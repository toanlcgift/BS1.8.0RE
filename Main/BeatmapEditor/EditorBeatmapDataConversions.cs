using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200052B RID: 1323
	public static class EditorBeatmapDataConversions
	{
		// Token: 0x0600196E RID: 6510 RVA: 0x00058F00 File Offset: 0x00057100
		public static BeatmapSaveData ConvertToBeatmapSaveData(this EditorBeatsData beatsData, float beatsPerMinute, bool clipToTime, float maxTime)
		{
			List<BeatmapSaveData.EventData> list = new List<BeatmapSaveData.EventData>(100);
			List<BeatmapSaveData.NoteData> list2 = new List<BeatmapSaveData.NoteData>(1000);
			List<BeatmapSaveData.ObstacleData> list3 = new List<BeatmapSaveData.ObstacleData>(100);
			float[] array = new float[4];
			int[] array2 = new int[4];
			ObstacleType[] array3 = new ObstacleType[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = 0f;
				array2[i] = 0;
				array3[i] = ObstacleType.FullHeight;
			}
			for (int j = 0; j < beatsData.length; j++)
			{
				BeatData beatData = beatsData[j];
				float timeForBeatIndex = EditorBeatmapDataConversions.GetTimeForBeatIndex(j, beatsData.beatsPerBpmBeat);
				if (clipToTime && timeForBeatIndex / beatsPerMinute * 60f > maxTime)
				{
					break;
				}
				for (int k = 0; k < 4; k++)
				{
					EditorNoteData editorNoteData = beatData.baseNotesData[k];
					if (editorNoteData != null)
					{
						list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Base, editorNoteData.type, editorNoteData.cutDirection));
					}
					editorNoteData = beatData.upperNotesData[k];
					if (editorNoteData != null)
					{
						list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Upper, editorNoteData.type, editorNoteData.cutDirection));
					}
					editorNoteData = beatData.topNotesData[k];
					if (editorNoteData != null)
					{
						list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Top, editorNoteData.type, editorNoteData.cutDirection));
					}
					EditorObstacleData editorObstacleData = beatData.obstaclesData[k];
					if (array2[k] > 0 && (editorObstacleData == null || editorObstacleData.type != array3[k]))
					{
						int l;
						for (l = 1; l < 4 - k; l++)
						{
							EditorObstacleData editorObstacleData2 = beatData.obstaclesData[k + l];
							if (array2[k + l] <= 0 || (editorObstacleData2 != null && editorObstacleData2.type == array3[k + l]) || array2[k + l] != array2[k] || array3[k + l] != array3[k])
							{
								break;
							}
							array2[k + l] = 0;
						}
						list3.Add(new BeatmapSaveData.ObstacleData(array[k], k, array3[k], EditorBeatmapDataConversions.GetTimeForBeatIndex(array2[k], beatsData.beatsPerBpmBeat), l));
					}
					if (editorObstacleData != null)
					{
						if (array2[k] == 0 || editorObstacleData.type != array3[k])
						{
							array2[k] = 1;
							array3[k] = editorObstacleData.type;
							array[k] = timeForBeatIndex;
						}
						else
						{
							array2[k]++;
						}
					}
					else
					{
						array2[k] = 0;
					}
				}
				for (int m = 0; m < 32; m++)
				{
					EditorEventData editorEventData = beatData.eventsData[m];
					if (editorEventData != null && !editorEventData.isPreviousValidValue)
					{
						list.Add(new BeatmapSaveData.EventData(timeForBeatIndex, (BeatmapEventType)m, editorEventData.value));
					}
				}
			}
			if (list.Count == 0 && list2.Count == 0 && list3.Count == 0)
			{
				return null;
			}
			return new BeatmapSaveData(list, list2, list3);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000591B4 File Offset: 0x000573B4
		public static EditorBeatsData ConvertToEditorBeatsData(this BeatmapSaveData beatmapSaveData, float noteJumpMovementSpeed, float noteJumpStartBeatOffset)
		{
			if (beatmapSaveData == null)
			{
				return null;
			}
			int num = 1;
			float time = 0f;
			bool flag = false;
			float num2 = 0f;
			EditorBeatmapDataConversions.ComputeBeatsPerBPMBeat(beatmapSaveData.notes, ref flag, ref num, ref time, ref num2);
			EditorBeatmapDataConversions.ComputeBeatsPerBPMBeat(beatmapSaveData.obstacles, ref flag, ref num, ref time, ref num2);
			EditorBeatmapDataConversions.ComputeBeatsPerBPMBeat(beatmapSaveData.events, ref flag, ref num, ref time, ref num2);
			if (num2 > 0f)
			{
				int beatIndexForTime = EditorBeatmapDataConversions.GetBeatIndexForTime(num2, num);
				int num3 = num * 4;
				Debug.Log(string.Format("3/4 rhythm detected at {0} - {1}:{2}", num2, beatIndexForTime / num3, beatIndexForTime % num3 + 1));
			}
			EditorBeatsData editorBeatsData = new EditorBeatsData(EditorBeatmapDataConversions.GetBeatIndexForTime(time, num) + 1, num, noteJumpMovementSpeed, noteJumpStartBeatOffset);
			EditorNoteData prevNoteData = null;
			EditorNoteData prevNoteData2 = null;
			foreach (BeatmapSaveData.NoteData noteData in beatmapSaveData.notes)
			{
				int beatIndexForTime2 = EditorBeatmapDataConversions.GetBeatIndexForTime(noteData.time, editorBeatsData.beatsPerBpmBeat);
				BeatData beatData = editorBeatsData[beatIndexForTime2];
				EditorNoteData editorNoteData = new EditorNoteData(noteData.type, noteData.cutDirection, noteData.lineLayer, noteData.lineIndex, beatIndexForTime2, false);
				if (noteData.type == NoteType.NoteA && editorBeatsData.IsNoteProblematic(editorNoteData, prevNoteData))
				{
					editorNoteData.highlight = true;
				}
				else if (noteData.type == NoteType.NoteB && editorBeatsData.IsNoteProblematic(editorNoteData, prevNoteData2))
				{
					editorNoteData.highlight = true;
				}
				if (noteData.lineLayer == NoteLineLayer.Base)
				{
					if (beatData.baseNotesData[noteData.lineIndex] != null)
					{
						Debug.Log(string.Format("Collision detected in base notes - beat {0}, line {1}.", beatIndexForTime2, noteData.lineIndex));
					}
					beatData.baseNotesData[noteData.lineIndex] = editorNoteData;
				}
				else if (noteData.lineLayer == NoteLineLayer.Upper)
				{
					if (beatData.upperNotesData[noteData.lineIndex] != null)
					{
						Debug.Log(string.Format("Collision detected in upper notes - beat {0}, line {1}.", beatIndexForTime2, noteData.lineIndex));
					}
					beatData.upperNotesData[noteData.lineIndex] = editorNoteData;
				}
				else if (noteData.lineLayer == NoteLineLayer.Top)
				{
					if (beatData.topNotesData[noteData.lineIndex] != null)
					{
						Debug.Log(string.Format("Collision detected in top notes - beat {0}, line {1}.", beatIndexForTime2, noteData.lineIndex));
					}
					beatData.topNotesData[noteData.lineIndex] = editorNoteData;
				}
				if (noteData.type == NoteType.NoteA)
				{
					prevNoteData = editorNoteData;
				}
				else if (noteData.type == NoteType.NoteB)
				{
					prevNoteData2 = editorNoteData;
				}
			}
			foreach (BeatmapSaveData.ObstacleData obstacleData in beatmapSaveData.obstacles)
			{
				int beatIndexForTime3 = EditorBeatmapDataConversions.GetBeatIndexForTime(obstacleData.time, editorBeatsData.beatsPerBpmBeat);
				int beatIndexForTime4 = EditorBeatmapDataConversions.GetBeatIndexForTime(obstacleData.duration, editorBeatsData.beatsPerBpmBeat);
				for (int i = 0; i < beatIndexForTime4; i++)
				{
					if (beatIndexForTime3 + i < editorBeatsData.length)
					{
						BeatData beatData2 = editorBeatsData[beatIndexForTime3 + i];
						for (int j = 0; j < obstacleData.width; j++)
						{
							if (beatData2.obstaclesData[obstacleData.lineIndex + j] != null)
							{
								Debug.Log(string.Format("Collision detected in obstacles - beat {0}, line {1}.", beatIndexForTime3, obstacleData.lineIndex + j));
							}
							beatData2.obstaclesData[obstacleData.lineIndex + j] = new EditorObstacleData(obstacleData.type);
						}
					}
				}
			}
			foreach (BeatmapSaveData.EventData eventData in beatmapSaveData.events)
			{
				int beatIndexForTime5 = EditorBeatmapDataConversions.GetBeatIndexForTime(eventData.time, editorBeatsData.beatsPerBpmBeat);
				BeatData beatData3 = editorBeatsData[beatIndexForTime5];
				int type = (int)eventData.type;
				if (beatData3.eventsData[type] != null)
				{
					Debug.Log(string.Format("Collision detected in events - beat {0}, line {1}.", beatIndexForTime5, type));
				}
				beatData3.eventsData[type] = new EditorEventData(eventData.value, false);
			}
			for (int k = 0; k < 32; k++)
			{
				editorBeatsData.FillPreviousValidEvent(0, k, false);
			}
			return editorBeatsData;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00059634 File Offset: 0x00057834
		private static void MoveTime(IEnumerable<BeatmapSaveData.ITime> items, float offset)
		{
			foreach (BeatmapSaveData.ITime time in items)
			{
				time.MoveTime(offset);
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005967C File Offset: 0x0005787C
		private static void ComputeBeatsPerBPMBeat(IEnumerable<BeatmapSaveData.ITime> items, ref bool ignoreDivision2, ref int maxBeatsPerBPMBeat, ref float maxTime, ref float time34)
		{
			int num = 1;
			foreach (BeatmapSaveData.ITime time35 in items)
			{
				if (maxTime < time35.time)
				{
					maxTime = time35.time;
				}
				int beatsPerBPMBeat = EditorBeatmapDataConversions.GetBeatsPerBPMBeat(time35.time, 3f);
				int num2;
				if (!ignoreDivision2)
				{
					int beatsPerBPMBeat2 = EditorBeatmapDataConversions.GetBeatsPerBPMBeat(time35.time, 2f);
					if (beatsPerBPMBeat < beatsPerBPMBeat2)
					{
						if (time34 == 0f)
						{
							time34 = time35.time;
						}
						ignoreDivision2 = true;
						num2 = beatsPerBPMBeat;
					}
					else
					{
						num2 = beatsPerBPMBeat2;
					}
				}
				else
				{
					num2 = beatsPerBPMBeat;
				}
				if (num2 > num)
				{
					num = num2;
				}
			}
			if (maxBeatsPerBPMBeat < num)
			{
				maxBeatsPerBPMBeat = num;
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00059738 File Offset: 0x00057938
		private static int GetBeatsPerBPMBeat(float time, float division)
		{
			float num = time - Mathf.Floor(time);
			float num2 = 1f;
			float num3 = num / num2;
			int num4 = 0;
			while (Mathf.Abs(num3 - Mathf.Round(num3)) > 0.001f && num4 < 4)
			{
				num4++;
				num2 = 1f / division;
				num3 = num / num2;
				division *= 2f;
			}
			return Mathf.RoundToInt(1f / num2);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00012E4B File Offset: 0x0001104B
		private static float GetTimeForBeatIndex(int beatIndex, int beatsPerBPMBeat)
		{
			return (float)beatIndex / (float)beatsPerBPMBeat;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00012E52 File Offset: 0x00011052
		private static int GetBeatIndexForTime(float time, int beatsPerBPMBeat)
		{
			return (int)(time * (float)beatsPerBPMBeat + 0.5f);
		}
	}
}
