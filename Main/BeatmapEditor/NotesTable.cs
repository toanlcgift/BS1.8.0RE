using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000571 RID: 1393
	public class NotesTable : GridTable
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00014081 File Offset: 0x00012281
		protected override int numberOfColumns
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00014084 File Offset: 0x00012284
		protected override float columnWidth
		{
			get
			{
				return this._columnWidth;
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0001408C File Offset: 0x0001228C
		public void Init(EditorBeatmapSO editorBeatmap, NoteLineLayer noteLineLayer)
		{
			this._editorBeatmap = editorBeatmap;
			this._noteLineLayer = noteLineLayer;
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000140CA File Offset: 0x000122CA
		protected override void Awake()
		{
			base.Awake();
			this._columnWidth = this._notesTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)this.numberOfColumns;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000140F5 File Offset: 0x000122F5
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00013F4D File Offset: 0x0001214D
		private void HandleEditorBeatmapDidChangeAllData()
		{
			base.UpdateAllCells();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0005E0C8 File Offset: 0x0005C2C8
		protected override BeatmapEditorTableCell CellForRow(int row)
		{
			if (this._editorBeatmap == null)
			{
				return null;
			}
			BeatData beatData = null;
			if (row < this._editorBeatmap.beatsDataLength)
			{
				beatData = this._editorBeatmap.GetBeatData(row);
			}
			if (beatData == null)
			{
				return null;
			}
			bool flag = true;
			EditorNoteData[] array = null;
			if (beatData != null)
			{
				array = beatData.NoteDataForLineLayer(this._noteLineLayer);
				for (int i = 0; i < this.numberOfColumns; i++)
				{
					if (array[i] != null)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				return null;
			}
			NotesTableCell notesTableCell = (NotesTableCell)base.DequeueReusableCell("Cell");
			if (notesTableCell == null)
			{
				notesTableCell = UnityEngine.Object.Instantiate<NotesTableCell>(this._notesTableCellPrefab);
				notesTableCell.reuseIdentifier = "Cell";
			}
			for (int j = 0; j < this.numberOfColumns; j++)
			{
				notesTableCell.SetLineActive(j, array != null && array[j] != null);
				if (array != null && array[j] != null)
				{
					notesTableCell.SetLineType(j, array[j].type, array[j].cutDirection, array[j].highlight);
				}
			}
			return notesTableCell;
		}

		// Token: 0x040019DC RID: 6620
		[SerializeField]
		private NotesTableCell _notesTableCellPrefab;

		// Token: 0x040019DD RID: 6621
		private const string kCellIdentifier = "Cell";

		// Token: 0x040019DE RID: 6622
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019DF RID: 6623
		private NoteLineLayer _noteLineLayer;

		// Token: 0x040019E0 RID: 6624
		private float _columnWidth;
	}
}
