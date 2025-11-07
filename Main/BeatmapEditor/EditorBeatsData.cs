using BeatmapEditor;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200051F RID: 1311
	public class EditorBeatsData
	{
		// Token: 0x17000499 RID: 1177
		public BeatData this[int index]
		{
			get
			{
				return this._beats[index];
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000129BB File Offset: 0x00010BBB
		public int length
		{
			get
			{
				if (this._beats == null)
				{
					return 0;
				}
				return this._beats.Length;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x000129CF File Offset: 0x00010BCF
		public int beatsPerBpmBeat
		{
			get
			{
				return this._beatsPerBPMBeat;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x000129D7 File Offset: 0x00010BD7
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x000129DF File Offset: 0x00010BDF
		public float noteJumpMovementSpeed
		{
			get
			{
				return this._noteJumpMovementSpeed;
			}
			set
			{
				this._noteJumpMovementSpeed = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x000129E8 File Offset: 0x00010BE8
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x000129F0 File Offset: 0x00010BF0
		public float noteJumpStartBeatOffset
		{
			get
			{
				return this._noteJumpStartBeatOffset;
			}
			set
			{
				this._noteJumpStartBeatOffset = value;
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00057A40 File Offset: 0x00055C40
		public EditorBeatsData(int lengthInBeats, int beatsPerBPMBeat, float noteJumpMovementSpeed, float noteJumpStartBeatOffset)
		{
			this._beats = new BeatData[lengthInBeats];
			for (int i = 0; i < this._beats.Length; i++)
			{
				this._beats[i] = new BeatData();
			}
			this._beatsPerBPMBeat = beatsPerBPMBeat;
			this._noteJumpMovementSpeed = noteJumpMovementSpeed;
			this._noteJumpStartBeatOffset = noteJumpStartBeatOffset;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00057A9C File Offset: 0x00055C9C
		public EditorBeatsData(EditorBeatsData other)
		{
			if (other._beats == null)
			{
				return;
			}
			this._beats = new BeatData[other._beats.Length];
			for (int i = 0; i < this._beats.Length; i++)
			{
				if (other._beats[i] != null)
				{
					this._beats[i] = new BeatData(other._beats[i]);
				}
			}
			this._beatsPerBPMBeat = other._beatsPerBPMBeat;
			this._noteJumpMovementSpeed = other._noteJumpMovementSpeed;
			this._noteJumpStartBeatOffset = other._noteJumpStartBeatOffset;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000129F9 File Offset: 0x00010BF9
		public EditorBeatsData Clone()
		{
			return new EditorBeatsData(this);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00057B28 File Offset: 0x00055D28
		public HashSet<int> FindBeatsWithObstacleCollision()
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < this._beats.Length; i++)
			{
				BeatData beatData = this._beats[i];
				bool flag = false;
				for (int j = 0; j < beatData.obstaclesData.Length; j++)
				{
					EditorObstacleData editorObstacleData = beatData.obstaclesData[j];
					if (editorObstacleData != null)
					{
						flag |= (beatData.upperNotesData[j] != null || beatData.topNotesData[j] != null);
						if (editorObstacleData.type == ObstacleType.FullHeight)
						{
							flag |= (beatData.baseNotesData[j] != null);
						}
						if (flag)
						{
							hashSet.Add(i);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00057BC0 File Offset: 0x00055DC0
		public void Stretch(int value)
		{
			this._beatsPerBPMBeat *= value;
			BeatData[] array = new BeatData[this._beats.Length * value];
			for (int i = 0; i < this._beats.Length; i++)
			{
				array[i * value] = this._beats[i];
				for (int j = 1; j < value; j++)
				{
					BeatData beatData = array[i * value + j] = new BeatData();
					for (int k = 0; k < beatData.obstaclesData.Length; k++)
					{
						beatData.obstaclesData[k] = this._beats[i].obstaclesData[k];
					}
				}
			}
			this._beats = array;
			int num = this._beats[0].eventsData.Length;
			for (int l = 0; l < num; l++)
			{
				this.FillPreviousValidEvent(0, l, false);
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00057C90 File Offset: 0x00055E90
		public void Squish(int value)
		{
			if (this._beats.Length < 2)
			{
				return;
			}
			this._beatsPerBPMBeat /= value;
			BeatData[] array = new BeatData[Mathf.CeilToInt((float)this._beats.Length / (float)value)];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._beats[i * value];
			}
			this._beats = array;
			int num = this._beats[0].eventsData.Length;
			for (int j = 0; j < num; j++)
			{
				this.FillPreviousValidEvent(0, j, false);
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00057D18 File Offset: 0x00055F18
		public bool CanSquish2x(out int problematicBeatIndex)
		{
			problematicBeatIndex = -1;
			if (this._beats.Length < 2)
			{
				return false;
			}
			for (int i = 1; i < this._beats.Length; i += 2)
			{
				problematicBeatIndex = i;
				BeatData beatData = this._beats[i];
				for (int j = 0; j < beatData.baseNotesData.Length; j++)
				{
					if (beatData.baseNotesData[j] != null)
					{
						return false;
					}
				}
				for (int k = 0; k < beatData.upperNotesData.Length; k++)
				{
					if (beatData.upperNotesData[k] != null)
					{
						return false;
					}
				}
				for (int l = 0; l < beatData.topNotesData.Length; l++)
				{
					if (beatData.topNotesData[l] != null)
					{
						return false;
					}
				}
				for (int m = 0; m < beatData.eventsData.Length; m++)
				{
					if (beatData.eventsData[m] != null && !beatData.eventsData[m].isPreviousValidValue)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00057DF4 File Offset: 0x00055FF4
		public bool CanSquish3x(out int problematicBeatIndex)
		{
			problematicBeatIndex = -1;
			if (this._beats.Length < 3 || this._beats.Length % 3 != 0)
			{
				return false;
			}
			for (int i = 1; i < this._beats.Length; i += 3)
			{
				problematicBeatIndex = i;
				BeatData beatData = this._beats[i];
				BeatData beatData2 = this._beats[i + 1];
				if (!EditorBeatsData.CheckNoteData(beatData.baseNotesData) || !EditorBeatsData.CheckNoteData(beatData2.baseNotesData))
				{
					return false;
				}
				if (!EditorBeatsData.CheckNoteData(beatData.upperNotesData) || !EditorBeatsData.CheckNoteData(beatData2.upperNotesData))
				{
					return false;
				}
				if (!EditorBeatsData.CheckNoteData(beatData.topNotesData) || !EditorBeatsData.CheckNoteData(beatData2.topNotesData))
				{
					return false;
				}
				if (!EditorBeatsData.CheckEventData(beatData.eventsData) || !EditorBeatsData.CheckEventData(beatData2.eventsData))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00057EC0 File Offset: 0x000560C0
		public void Resize(int newLengthInBeats)
		{
			if (newLengthInBeats == this._beats.Length)
			{
				return;
			}
			BeatData[] array = new BeatData[newLengthInBeats];
			int num = this._beats.Length;
			int num2 = Mathf.Min(num, newLengthInBeats);
			for (int i = 0; i < num2; i++)
			{
				array[i] = this._beats[i];
			}
			for (int j = num2; j < array.Length; j++)
			{
				array[j] = new BeatData();
			}
			this._beats = array;
			if (this._beats.Length != 0)
			{
				int num3 = this._beats[0].eventsData.Length;
				for (int k = 0; k < num3; k++)
				{
					this.FillPreviousValidEvent(num, k, false);
				}
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00057F60 File Offset: 0x00056160
		public void Clear()
		{
			for (int i = 0; i < this._beats.Length; i++)
			{
				this._beats[i] = new BeatData();
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00057F90 File Offset: 0x00056190
		public void FillPreviousValidEvent(int startBeatIndex, int eventIndex, bool untilNextValid)
		{
			if (startBeatIndex >= this._beats.Length)
			{
				return;
			}
			EditorEventData editorEventData = null;
			if (startBeatIndex >= 1)
			{
				editorEventData = this._beats[startBeatIndex - 1].eventsData[eventIndex];
			}
			for (int i = startBeatIndex; i < this._beats.Length; i++)
			{
				EditorEventData[] eventsData = this._beats[i].eventsData;
				if (eventsData[eventIndex] != null && !eventsData[eventIndex].isPreviousValidValue)
				{
					if (untilNextValid)
					{
						break;
					}
					editorEventData = eventsData[eventIndex];
				}
				else if (editorEventData != null)
				{
					eventsData[eventIndex] = new EditorEventData(editorEventData.value, true);
				}
				else
				{
					eventsData[eventIndex] = null;
				}
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00058014 File Offset: 0x00056214
		public EditorNoteData FindNeighbourNote(EditorNoteData noteData, int direction)
		{
			if (direction != 1 && direction != -1)
			{
				return null;
			}
			int num = noteData.beatIndex + direction;
			while (num >= 0 && num < this._beats.Length)
			{
				for (int i = 0; i < this._beats[num].baseNotesData.Length; i++)
				{
					EditorNoteData editorNoteData = this._beats[num].baseNotesData[i];
					if (editorNoteData != null && editorNoteData.type == noteData.type)
					{
						return editorNoteData;
					}
				}
				for (int j = 0; j < this._beats[num].upperNotesData.Length; j++)
				{
					EditorNoteData editorNoteData2 = this._beats[num].upperNotesData[j];
					if (editorNoteData2 != null && editorNoteData2.type == noteData.type)
					{
						return editorNoteData2;
					}
				}
				for (int k = 0; k < this._beats[num].topNotesData.Length; k++)
				{
					EditorNoteData editorNoteData3 = this._beats[num].topNotesData[k];
					if (editorNoteData3 != null && editorNoteData3.type == noteData.type)
					{
						return editorNoteData3;
					}
				}
				num += direction;
			}
			return null;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00058114 File Offset: 0x00056314
		public bool IsNoteProblematic(EditorNoteData noteData, EditorNoteData prevNoteData)
		{
			if (noteData == null || prevNoteData == null)
			{
				return false;
			}
			if (noteData.type != NoteType.NoteA && noteData.type != NoteType.NoteB)
			{
				return false;
			}
			if (prevNoteData.type != noteData.type)
			{
				return false;
			}
			if (prevNoteData.beatIndex <= noteData.beatIndex - this._beatsPerBPMBeat * 2)
			{
				return false;
			}
			if (noteData.cutDirection == NoteCutDirection.Up)
			{
				if (prevNoteData.noteLineLayer != NoteLineLayer.Base && noteData.noteLineLayer == NoteLineLayer.Upper)
				{
					return true;
				}
				if (prevNoteData.cutDirection == NoteCutDirection.Down || prevNoteData.cutDirection == NoteCutDirection.DownRight || prevNoteData.cutDirection == NoteCutDirection.DownLeft)
				{
					return false;
				}
			}
			if (noteData.cutDirection == NoteCutDirection.Down)
			{
				if (noteData.noteLineLayer != NoteLineLayer.Base)
				{
					return true;
				}
				if (prevNoteData.cutDirection == NoteCutDirection.Up || prevNoteData.cutDirection == NoteCutDirection.UpRight || prevNoteData.cutDirection == NoteCutDirection.UpLeft || prevNoteData.cutDirection == NoteCutDirection.Left || prevNoteData.cutDirection == NoteCutDirection.Right)
				{
					return false;
				}
			}
			if (noteData.cutDirection == NoteCutDirection.Left && prevNoteData.cutDirection != NoteCutDirection.Left)
			{
				return false;
			}
			if (noteData.cutDirection == NoteCutDirection.Right && prevNoteData.cutDirection != NoteCutDirection.Right)
			{
				return false;
			}
			if (noteData.cutDirection == NoteCutDirection.UpLeft)
			{
				if (noteData.noteLineLayer == NoteLineLayer.Base)
				{
					return true;
				}
				if (prevNoteData.cutDirection == NoteCutDirection.Down || prevNoteData.cutDirection == NoteCutDirection.DownRight || (prevNoteData.cutDirection == NoteCutDirection.Right && prevNoteData.noteLineLayer == NoteLineLayer.Base))
				{
					return false;
				}
			}
			if (noteData.cutDirection == NoteCutDirection.UpRight)
			{
				if (noteData.noteLineLayer == NoteLineLayer.Base)
				{
					return true;
				}
				if (prevNoteData.cutDirection == NoteCutDirection.Down || prevNoteData.cutDirection == NoteCutDirection.DownLeft || (prevNoteData.cutDirection == NoteCutDirection.Left && prevNoteData.noteLineLayer == NoteLineLayer.Base))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00058278 File Offset: 0x00056478
		[CompilerGenerated]
		internal static bool CheckNoteData(EditorNoteData[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005829C File Offset: 0x0005649C
		[CompilerGenerated]
		internal static bool CheckEventData(EditorEventData[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != null && !data[i].isPreviousValidValue)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400186A RID: 6250
		private BeatData[] _beats;

		// Token: 0x0400186B RID: 6251
		private int _beatsPerBPMBeat = 1;

		// Token: 0x0400186C RID: 6252
		private float _noteJumpMovementSpeed;

		// Token: 0x0400186D RID: 6253
		private float _noteJumpStartBeatOffset;
	}
}
