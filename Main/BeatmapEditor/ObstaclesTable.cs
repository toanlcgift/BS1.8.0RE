using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000573 RID: 1395
	public class ObstaclesTable : GridTable
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00014081 File Offset: 0x00012281
		protected override int numberOfColumns
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00014131 File Offset: 0x00012331
		protected override float columnWidth
		{
			get
			{
				return this._columnWidth;
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00014139 File Offset: 0x00012339
		public void Init(EditorBeatmapSO editorBeatmap)
		{
			this._editorBeatmap = editorBeatmap;
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00014170 File Offset: 0x00012370
		protected override void Awake()
		{
			base.Awake();
			this._columnWidth = this._obstaclesTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)this.numberOfColumns;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0001419B File Offset: 0x0001239B
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00013F4D File Offset: 0x0001214D
		private void HandleEditorBeatmapDidChangeAllData()
		{
			base.UpdateAllCells();
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0005E400 File Offset: 0x0005C600
		protected override BeatmapEditorTableCell CellForRow(int row)
		{
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
			EditorObstacleData[] array = null;
			if (beatData != null)
			{
				array = beatData.obstaclesData;
				for (int i = 0; i < 4; i++)
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
			ObstaclesTableCell obstaclesTableCell = (ObstaclesTableCell)base.DequeueReusableCell("Cell");
			if (obstaclesTableCell == null)
			{
				obstaclesTableCell = UnityEngine.Object.Instantiate<ObstaclesTableCell>(this._obstaclesTableCellPrefab);
				obstaclesTableCell.reuseIdentifier = "Cell";
			}
			for (int j = 0; j < 4; j++)
			{
				obstaclesTableCell.SetLineActive(j, array != null && array[j] != null);
				if (array != null && array[j] != null)
				{
					obstaclesTableCell.SetLineType(j, array[j].type);
				}
			}
			return obstaclesTableCell;
		}

		// Token: 0x040019E7 RID: 6631
		[SerializeField]
		private ObstaclesTableCell _obstaclesTableCellPrefab;

		// Token: 0x040019E8 RID: 6632
		private const string kCellIdentifier = "Cell";

		// Token: 0x040019E9 RID: 6633
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019EA RID: 6634
		private float _columnWidth;
	}
}
