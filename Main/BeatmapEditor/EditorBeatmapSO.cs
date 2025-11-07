using System;

namespace BeatmapEditor
{
	// Token: 0x0200052A RID: 1322
	public class EditorBeatmapSO : PersistentScriptableObject
	{
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x0600194E RID: 6478 RVA: 0x000584A0 File Offset: 0x000566A0
		// (remove) Token: 0x0600194F RID: 6479 RVA: 0x000584D8 File Offset: 0x000566D8
		public event Action didChangeAllDataEvent;

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00012C9A File Offset: 0x00010E9A
		public int beatsPerBpmBeat
		{
			get
			{
				if (this._beatsData == null)
				{
					return 1;
				}
				return this._beatsData.beatsPerBpmBeat;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00012CB1 File Offset: 0x00010EB1
		public int beatsDataLength
		{
			get
			{
				if (this._beatsData == null)
				{
					return 0;
				}
				return this._beatsData.length;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public bool hasBeatsData
		{
			get
			{
				return this._beatsData != null;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00012CD3 File Offset: 0x00010ED3
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00012CEE File Offset: 0x00010EEE
		public float noteJumpMovementSpeed
		{
			get
			{
				if (this._beatsData == null)
				{
					return 0f;
				}
				return this._beatsData.noteJumpMovementSpeed;
			}
			set
			{
				if (this._beatsData != null)
				{
					this._beatsData.noteJumpMovementSpeed = value;
				}
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00012D04 File Offset: 0x00010F04
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x00012D1F File Offset: 0x00010F1F
		public float noteJumpStartBeatOffset
		{
			get
			{
				if (this._beatsData == null)
				{
					return 0f;
				}
				return this._beatsData.noteJumpStartBeatOffset;
			}
			set
			{
				if (this._beatsData != null)
				{
					this._beatsData.noteJumpStartBeatOffset = value;
				}
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00012D35 File Offset: 0x00010F35
		public EditorBeatsData beatsData
		{
			get
			{
				return this._beatsData;
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00012D3D File Offset: 0x00010F3D
		public BeatData GetBeatData(int beatIndex)
		{
			return this._beatsData[beatIndex];
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00012D4B File Offset: 0x00010F4B
		public float BeatDuration(float beatsPerMinute)
		{
			return 60f / beatsPerMinute / (float)this.beatsPerBpmBeat;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00058510 File Offset: 0x00056710
		public void Undo()
		{
			EditorBeatsData editorBeatsData = this._undoRedoBuffer.Undo();
			if (editorBeatsData == null)
			{
				return;
			}
			this._beatsData = editorBeatsData;
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00058544 File Offset: 0x00056744
		public void Redo()
		{
			EditorBeatsData editorBeatsData = this._undoRedoBuffer.Redo();
			if (editorBeatsData == null)
			{
				return;
			}
			this._beatsData = editorBeatsData;
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00058578 File Offset: 0x00056778
		public void InitWithEmptyData(int numberOfBeats)
		{
			this._beatsData = new EditorBeatsData(numberOfBeats, 4, 0f, 0f);
			this._undoRedoBuffer.Clear();
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00012D5C File Offset: 0x00010F5C
		public void LoadData(EditorBeatsData beatsData)
		{
			this._beatsData = beatsData;
			this._undoRedoBuffer.Clear();
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000585D0 File Offset: 0x000567D0
		public void AddNote(int beatIndex, NoteLineLayer lineLayer, int lineIndex, EditorNoteData noteData)
		{
			if (beatIndex >= this._beatsData.length)
			{
				this._beatsData.Resize(beatIndex + 1);
			}
			EditorNoteData editorNoteData = null;
			if (lineLayer == NoteLineLayer.Base)
			{
				editorNoteData = this._beatsData[beatIndex].baseNotesData[lineIndex];
				this._beatsData[beatIndex].baseNotesData[lineIndex] = noteData;
			}
			else if (lineLayer == NoteLineLayer.Upper)
			{
				editorNoteData = this._beatsData[beatIndex].upperNotesData[lineIndex];
				this._beatsData[beatIndex].upperNotesData[lineIndex] = noteData;
			}
			else if (lineLayer == NoteLineLayer.Top)
			{
				editorNoteData = this._beatsData[beatIndex].topNotesData[lineIndex];
				this._beatsData[beatIndex].topNotesData[lineIndex] = noteData;
			}
			if (editorNoteData != null && editorNoteData.type != noteData.type)
			{
				EditorNoteData editorNoteData2 = this._beatsData.FindNeighbourNote(editorNoteData, 1);
				if (editorNoteData2 != null)
				{
					EditorNoteData prevNoteData = this._beatsData.FindNeighbourNote(editorNoteData2, -1);
					editorNoteData2.highlight = this._beatsData.IsNoteProblematic(editorNoteData2, prevNoteData);
				}
			}
			EditorNoteData prevNoteData2 = this._beatsData.FindNeighbourNote(noteData, -1);
			EditorNoteData editorNoteData3 = this._beatsData.FindNeighbourNote(noteData, 1);
			noteData.highlight = this._beatsData.IsNoteProblematic(noteData, prevNoteData2);
			if (editorNoteData3 != null)
			{
				editorNoteData3.highlight = this._beatsData.IsNoteProblematic(editorNoteData3, noteData);
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005873C File Offset: 0x0005693C
		public void AddEvent(int beatIndex, int eventIndex, EditorEventData eventData)
		{
			if (beatIndex >= this._beatsData.length)
			{
				this._beatsData.Resize(beatIndex + 1);
			}
			this._beatsData[beatIndex].eventsData[eventIndex] = eventData;
			this._beatsData.FillPreviousValidEvent(beatIndex + 1, eventIndex, true);
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000587A0 File Offset: 0x000569A0
		public void AddObstacle(int beatIndex, int lineIndex, int obstacleLength, ObstacleType obstacleType)
		{
			if (beatIndex + obstacleLength >= this._beatsData.length)
			{
				this._beatsData.Resize(beatIndex + obstacleLength + 1);
			}
			EditorObstacleData[] obstaclesData = this._beatsData[beatIndex].obstaclesData;
			for (int i = 0; i < obstacleLength; i++)
			{
				obstaclesData = this._beatsData[beatIndex + i].obstaclesData;
				if (obstacleType == ObstacleType.Top)
				{
					for (int j = 0; j < 4; j++)
					{
						obstaclesData[j] = new EditorObstacleData(obstacleType);
					}
				}
				else
				{
					obstaclesData[lineIndex] = new EditorObstacleData(obstacleType);
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005883C File Offset: 0x00056A3C
		public void EraseNote(int beatIndex, NoteLineLayer lineLayer, int lineIndex)
		{
			if (beatIndex >= this._beatsData.length || beatIndex < 0 || lineIndex < 0 || lineIndex > 3)
			{
				return;
			}
			EditorNoteData editorNoteData = null;
			if (lineLayer == NoteLineLayer.Base)
			{
				editorNoteData = this._beatsData[beatIndex].baseNotesData[lineIndex];
				this._beatsData[beatIndex].baseNotesData[lineIndex] = null;
			}
			else if (lineLayer == NoteLineLayer.Upper)
			{
				editorNoteData = this._beatsData[beatIndex].upperNotesData[lineIndex];
				this._beatsData[beatIndex].upperNotesData[lineIndex] = null;
			}
			else if (lineLayer == NoteLineLayer.Top)
			{
				editorNoteData = this._beatsData[beatIndex].topNotesData[lineIndex];
				this._beatsData[beatIndex].topNotesData[lineIndex] = null;
			}
			if (editorNoteData != null)
			{
				EditorNoteData editorNoteData2 = this._beatsData.FindNeighbourNote(editorNoteData, 1);
				if (editorNoteData2 != null)
				{
					EditorNoteData prevNoteData = this._beatsData.FindNeighbourNote(editorNoteData2, -1);
					editorNoteData2.highlight = this._beatsData.IsNoteProblematic(editorNoteData2, prevNoteData);
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00058938 File Offset: 0x00056B38
		public void EraseObstacle(int beatIndex, int lineIndex)
		{
			if (beatIndex >= this._beatsData.length)
			{
				return;
			}
			EditorObstacleData[] obstaclesData = this._beatsData[beatIndex].obstaclesData;
			if (obstaclesData[lineIndex] != null)
			{
				EditorObstacleData editorObstacleData = obstaclesData[lineIndex];
				obstaclesData[lineIndex] = null;
				int num = beatIndex;
				while (beatIndex < this._beatsData.length - 1)
				{
					beatIndex++;
					obstaclesData = this._beatsData[beatIndex].obstaclesData;
					if (obstaclesData == null || obstaclesData[lineIndex] == null || obstaclesData[lineIndex].type != editorObstacleData.type)
					{
						break;
					}
					obstaclesData[lineIndex] = null;
				}
				beatIndex = num;
				while (beatIndex > 0)
				{
					beatIndex--;
					obstaclesData = this._beatsData[beatIndex].obstaclesData;
					if (obstaclesData == null || obstaclesData[lineIndex] == null || obstaclesData[lineIndex].type != editorObstacleData.type)
					{
						break;
					}
					obstaclesData[lineIndex] = null;
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x00058A10 File Offset: 0x00056C10
		public void EraseEvent(int beatIndex, int eventIndex)
		{
			if (beatIndex >= this._beatsData.length)
			{
				return;
			}
			EditorEventData editorEventData = this._beatsData[beatIndex].eventsData[eventIndex];
			if (editorEventData != null && !editorEventData.isPreviousValidValue)
			{
				this._beatsData[beatIndex].eventsData[eventIndex] = null;
				this._beatsData.FillPreviousValidEvent(beatIndex, eventIndex, true);
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00058A84 File Offset: 0x00056C84
		public void EraseAllEvents()
		{
			for (int i = 0; i < this._beatsData.length; i++)
			{
				for (int j = 0; j < this._beatsData[i].eventsData.Length; j++)
				{
					this._beatsData[i].eventsData[j] = null;
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00058B00 File Offset: 0x00056D00
		public void EraseAllNotesAndCo()
		{
			for (int i = 0; i < this._beatsData.length; i++)
			{
				for (int j = 0; j < this._beatsData[i].baseNotesData.Length; j++)
				{
					this._beatsData[i].baseNotesData[j] = null;
				}
				for (int k = 0; k < this._beatsData[i].topNotesData.Length; k++)
				{
					this._beatsData[i].topNotesData[k] = null;
				}
				for (int l = 0; l < this._beatsData[i].upperNotesData.Length; l++)
				{
					this._beatsData[i].upperNotesData[l] = null;
				}
				for (int m = 0; m < this._beatsData[i].obstaclesData.Length; m++)
				{
					this._beatsData[i].obstaclesData[m] = null;
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			Action action = this.didChangeAllDataEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00058C1C File Offset: 0x00056E1C
		public bool PasteBeatSegments(int segmentStartBeatIndex, EditorNoteData[] copyPasteSegmentBaseNotesData, EditorNoteData[] copyPasteSegmentUpperNotesData, EditorNoteData[] copyPasteSegmentTopNotesData, EditorObstacleData[] copyPasteSegmentObstaclesData, EditorEventData[] copyPasteSegmentEventsData, int copyPasteSegmenBeatsPerBpmBeat, out string errorMessage)
		{
			if (copyPasteSegmenBeatsPerBpmBeat > this._beatsData.beatsPerBpmBeat)
			{
				errorMessage = "Can not paste data, because they were copied from beatmap with higher beat density.";
				return false;
			}
			if (copyPasteSegmenBeatsPerBpmBeat == 0)
			{
				errorMessage = null;
				return true;
			}
			int num = this._beatsData.beatsPerBpmBeat / copyPasteSegmenBeatsPerBpmBeat;
			if (copyPasteSegmentBaseNotesData != null)
			{
				int num2 = copyPasteSegmentBaseNotesData.Length / 4;
				for (int i = 0; i < num2; i++)
				{
					int num3 = segmentStartBeatIndex + i;
					num3 *= num;
					if (num3 >= this._beatsData.length)
					{
						this._beatsData.Resize(num3 + num2);
					}
					for (int j = 0; j < 4; j++)
					{
						this._beatsData[num3].baseNotesData[j] = copyPasteSegmentBaseNotesData[i * 4 + j];
					}
				}
			}
			if (copyPasteSegmentUpperNotesData != null)
			{
				int num4 = copyPasteSegmentUpperNotesData.Length / 4;
				for (int k = 0; k < num4; k++)
				{
					int num5 = segmentStartBeatIndex + k;
					num5 *= num;
					if (num5 >= this._beatsData.length)
					{
						this._beatsData.Resize(num5 + num4);
					}
					for (int l = 0; l < 4; l++)
					{
						this._beatsData[num5].upperNotesData[l] = copyPasteSegmentUpperNotesData[k * 4 + l];
					}
				}
			}
			if (copyPasteSegmentTopNotesData != null)
			{
				int num6 = copyPasteSegmentTopNotesData.Length / 4;
				for (int m = 0; m < num6; m++)
				{
					int num7 = segmentStartBeatIndex + m;
					num7 *= num;
					if (num7 >= this._beatsData.length)
					{
						this._beatsData.Resize(num7 + num6);
					}
					for (int n = 0; n < 4; n++)
					{
						this._beatsData[num7].topNotesData[n] = copyPasteSegmentTopNotesData[m * 4 + n];
					}
				}
			}
			if (copyPasteSegmentObstaclesData != null)
			{
				int num8 = copyPasteSegmentObstaclesData.Length / 4;
				for (int num9 = 0; num9 < num8; num9++)
				{
					int num10 = segmentStartBeatIndex + num9;
					num10 *= num;
					if (num10 >= this._beatsData.length)
					{
						this._beatsData.Resize(num10 + num8);
					}
					for (int num11 = 0; num11 < 4; num11++)
					{
						this._beatsData[num10].obstaclesData[num11] = copyPasteSegmentObstaclesData[num9 * 4 + num11];
					}
				}
			}
			if (copyPasteSegmentEventsData != null)
			{
				int num12 = copyPasteSegmentEventsData.Length / 32;
				for (int num13 = 0; num13 < num12; num13++)
				{
					int num14 = segmentStartBeatIndex + num13;
					num14 *= num;
					if (num14 >= this._beatsData.length)
					{
						this._beatsData.Resize(num14 + num12);
					}
					for (int num15 = 0; num15 < 32; num15++)
					{
						this._beatsData[num14].eventsData[num15] = copyPasteSegmentEventsData[num13 * 32 + num15];
					}
				}
				for (int num16 = 0; num16 < 32; num16++)
				{
					this._beatsData.FillPreviousValidEvent(segmentStartBeatIndex, num16, true);
					this._beatsData.FillPreviousValidEvent(segmentStartBeatIndex + num12, num16, true);
				}
			}
			Action action = this.didChangeAllDataEvent;
			if (action != null)
			{
				action();
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
			errorMessage = null;
			return true;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00012D96 File Offset: 0x00010F96
		public void Stretch(int value)
		{
			this.StretchInternal(value, true);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00012DA0 File Offset: 0x00010FA0
		public void Squish(int value)
		{
			this.SquishInternal(value, true);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00012DAA File Offset: 0x00010FAA
		public bool CanSquish2x(out int problematicBeatIndex)
		{
			return this._beatsData.CanSquish2x(out problematicBeatIndex);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00012DB8 File Offset: 0x00010FB8
		public bool CanSquish3x(out int problematicBeatIndex)
		{
			return this._beatsData.CanSquish3x(out problematicBeatIndex);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00012DC6 File Offset: 0x00010FC6
		private void StretchInternal(int value, bool useCallback)
		{
			this._beatsData.Stretch(value);
			if (useCallback)
			{
				Action action = this.didChangeAllDataEvent;
				if (action != null)
				{
					action();
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00012DFE File Offset: 0x00010FFE
		private void SquishInternal(int value, bool useCallback)
		{
			this._beatsData.Squish(value);
			if (useCallback)
			{
				Action action = this.didChangeAllDataEvent;
				if (action != null)
				{
					action();
				}
			}
			this._undoRedoBuffer.Add(this._beatsData.Clone());
		}

		// Token: 0x04001888 RID: 6280
		public const int bpmBeatsPerBar = 4;

		// Token: 0x04001889 RID: 6281
		private EditorBeatsData _beatsData;

		// Token: 0x0400188A RID: 6282
		private UndoRedoBuffer<EditorBeatsData> _undoRedoBuffer = new UndoRedoBuffer<EditorBeatsData>(40);
	}
}
