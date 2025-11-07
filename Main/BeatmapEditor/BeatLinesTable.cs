using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200056A RID: 1386
	public class BeatLinesTable : BeatmapEditorTable
	{
		// Token: 0x06001AF1 RID: 6897 RVA: 0x00013F0F File Offset: 0x0001210F
		protected override void Start()
		{
			base.Start();
			this._editorBeatmap.didChangeAllDataEvent += this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00013F2E File Offset: 0x0001212E
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this._editorBeatmap.didChangeAllDataEvent -= this.HandleEditorBeatmapDidChangeAllData;
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005D940 File Offset: 0x0005BB40
		protected override BeatmapEditorTableCell CellForRow(int row)
		{
			int num = 4 * this._editorBeatmap.beatsPerBpmBeat;
			BeatLineTableCell beatLineTableCell = (BeatLineTableCell)base.DequeueReusableCell("Cell");
			if (beatLineTableCell == null)
			{
				beatLineTableCell = UnityEngine.Object.Instantiate<BeatLineTableCell>(this._beatLineTableCellPrefab);
				beatLineTableCell.reuseIdentifier = "Cell";
			}
			if (num % 3 == 0)
			{
				beatLineTableCell.type = ((row % 3 == 0) ? BeatLineTableCell.Type.Bar : BeatLineTableCell.Type.Subdivision);
				if (num > 24)
				{
					if (row % (num / 4) == 0)
					{
						int num2 = Mathf.FloorToInt((float)(row / num));
						int num3 = Mathf.FloorToInt((float)((row - num2 * num) / (num / 4)));
						beatLineTableCell.text = string.Format("{0}-{1}", num2 + 1, num3 + 1);
					}
					else
					{
						beatLineTableCell.text = null;
					}
				}
				else
				{
					beatLineTableCell.text = ((row % num == 0) ? (row / num).ToString() : null);
				}
				if (row % num == 0)
				{
					beatLineTableCell.alpha = 1f;
				}
				else if (row % 12 == 0)
				{
					beatLineTableCell.alpha = 0.7f;
				}
				else if (row % 6 == 0)
				{
					beatLineTableCell.alpha = 0.3f;
				}
				else if (row % 3 == 0)
				{
					beatLineTableCell.alpha = 0.08f;
				}
				else
				{
					beatLineTableCell.alpha = 0.05f;
				}
			}
			else
			{
				beatLineTableCell.type = ((row % 2 == 0) ? BeatLineTableCell.Type.Bar : BeatLineTableCell.Type.Subdivision);
				beatLineTableCell.text = ((row % num == 0) ? (row / num).ToString() : null);
				if (num > 8)
				{
					if (row % num == 0)
					{
						beatLineTableCell.alpha = 1f;
					}
					else if (row % (num / 2) == 0)
					{
						beatLineTableCell.alpha = 0.3f;
					}
					else if (row % (num / 4) == 0)
					{
						beatLineTableCell.alpha = 0.2f;
					}
					else
					{
						beatLineTableCell.alpha = 0.05f;
					}
				}
				else if (row % num == 0)
				{
					beatLineTableCell.alpha = 1f;
				}
				else if (row % (num / 2) == 0)
				{
					beatLineTableCell.alpha = 0.3f;
				}
				else
				{
					beatLineTableCell.alpha = 0.05f;
				}
			}
			return beatLineTableCell;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00013F4D File Offset: 0x0001214D
		private void HandleEditorBeatmapDidChangeAllData()
		{
			base.UpdateAllCells();
		}

		// Token: 0x040019C7 RID: 6599
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019C8 RID: 6600
		[SerializeField]
		private BeatLineTableCell _beatLineTableCellPrefab;

		// Token: 0x040019C9 RID: 6601
		private const string kCellIdentifier = "Cell";
	}
}
