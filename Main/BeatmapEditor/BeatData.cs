using System;

namespace BeatmapEditor
{
	// Token: 0x0200051E RID: 1310
	public class BeatData
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x000128E4 File Offset: 0x00010AE4
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x000128EC File Offset: 0x00010AEC
		public EditorNoteData[] baseNotesData { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x000128F5 File Offset: 0x00010AF5
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x000128FD File Offset: 0x00010AFD
		public EditorNoteData[] upperNotesData { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00012906 File Offset: 0x00010B06
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0001290E File Offset: 0x00010B0E
		public EditorNoteData[] topNotesData { get; private set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00012917 File Offset: 0x00010B17
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0001291F File Offset: 0x00010B1F
		public EditorObstacleData[] obstaclesData { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00012928 File Offset: 0x00010B28
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x00012930 File Offset: 0x00010B30
		public EditorEventData[] eventsData { get; private set; }

		// Token: 0x06001901 RID: 6401 RVA: 0x00012939 File Offset: 0x00010B39
		public BeatData()
		{
			this.Init();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00057970 File Offset: 0x00055B70
		public BeatData(BeatData other)
		{
			this.Init();
			for (int i = 0; i < this.baseNotesData.Length; i++)
			{
				this.baseNotesData[i] = other.baseNotesData[i];
			}
			for (int j = 0; j < this.upperNotesData.Length; j++)
			{
				this.upperNotesData[j] = other.upperNotesData[j];
			}
			for (int k = 0; k < this.topNotesData.Length; k++)
			{
				this.topNotesData[k] = other.topNotesData[k];
			}
			for (int l = 0; l < this.obstaclesData.Length; l++)
			{
				this.obstaclesData[l] = other.obstaclesData[l];
			}
			for (int m = 0; m < this.eventsData.Length; m++)
			{
				this.eventsData[m] = other.eventsData[m];
			}
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00012947 File Offset: 0x00010B47
		private void Init()
		{
			this.baseNotesData = new EditorNoteData[4];
			this.upperNotesData = new EditorNoteData[4];
			this.topNotesData = new EditorNoteData[4];
			this.obstaclesData = new EditorObstacleData[4];
			this.eventsData = new EditorEventData[32];
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00012986 File Offset: 0x00010B86
		public BeatData Clone()
		{
			return new BeatData(this);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0001298E File Offset: 0x00010B8E
		public EditorNoteData[] NoteDataForLineLayer(NoteLineLayer noteLineLayer)
		{
			if (noteLineLayer == NoteLineLayer.Base)
			{
				return this.baseNotesData;
			}
			if (noteLineLayer == NoteLineLayer.Upper)
			{
				return this.upperNotesData;
			}
			if (noteLineLayer == NoteLineLayer.Top)
			{
				return this.topNotesData;
			}
			return null;
		}

		// Token: 0x04001869 RID: 6249
		public const int kNumberOfEvents = 32;
	}
}
