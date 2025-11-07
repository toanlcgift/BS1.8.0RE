using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000554 RID: 1364
	public abstract class BeatmapEditorTable : MonoBehaviour
	{
		// Token: 0x06001A6D RID: 6765 RVA: 0x000138BB File Offset: 0x00011ABB
		protected virtual void Awake()
		{
			this._scrollRect = this._beatmapEditorScrollView.scrollRect;
			this._playHeadOffset = this._beatmapEditorScrollView.playHeadPointsOffset;
			this._reusableCells = new Dictionary<string, List<BeatmapEditorTableCell>>();
			this._activeCells = new Dictionary<int, BeatmapEditorTableCell>();
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000138F5 File Offset: 0x00011AF5
		protected virtual void Start()
		{
			this.UpdateCells(true);
			this._scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.ScrollViewDidScroll));
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0001391A File Offset: 0x00011B1A
		protected virtual void OnDestroy()
		{
			this._scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.ScrollViewDidScroll));
		}

		// Token: 0x06001A70 RID: 6768
		protected abstract BeatmapEditorTableCell CellForRow(int row);

		// Token: 0x06001A71 RID: 6769 RVA: 0x0005BF2C File Offset: 0x0005A12C
		public BeatmapEditorTableCell DequeueReusableCell(string identifier)
		{
			List<BeatmapEditorTableCell> list;
			if (this._reusableCells.TryGetValue(identifier, out list) && list.Count > 0)
			{
				int index = list.Count - 1;
				BeatmapEditorTableCell result = list[index];
				list.RemoveAt(index);
				return result;
			}
			return null;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00013938 File Offset: 0x00011B38
		private void ScrollViewDidScroll(Vector2 normalizedPos)
		{
			this.UpdateCells(false);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00013941 File Offset: 0x00011B41
		public void UpdateAllCells()
		{
			this.UpdateCells(true);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0005BF6C File Offset: 0x0005A16C
		protected void UpdateCells(bool forceUpdate)
		{
			float rowHeight = this._beatmapEditorScrollView.rowHeight;
			float height = this._scrollRect.viewport.rect.height;
			int num = Mathf.CeilToInt(height / rowHeight) + 1;
			int num2 = Mathf.Max(Mathf.FloorToInt((this._scrollRect.normalizedPosition.y * (this._scrollRect.content.sizeDelta.y - height) - this._playHeadOffset) / rowHeight), 0);
			int num3 = num2 + num;
			if (forceUpdate)
			{
				foreach (BeatmapEditorTableCell beatmapEditorTableCell in this._activeCells.Values)
				{
					beatmapEditorTableCell.gameObject.SetActive(false);
					List<BeatmapEditorTableCell> list;
					if (!this._reusableCells.TryGetValue(beatmapEditorTableCell.reuseIdentifier, out list))
					{
						list = new List<BeatmapEditorTableCell>();
						this._reusableCells.Add(beatmapEditorTableCell.reuseIdentifier, list);
					}
					list.Add(beatmapEditorTableCell);
				}
				this._activeCells.Clear();
				for (int i = num2; i <= num3; i++)
				{
					BeatmapEditorTableCell beatmapEditorTableCell2 = this.CellForRow(i);
					if (beatmapEditorTableCell2 != null)
					{
						this.SetupCellForRow(beatmapEditorTableCell2, i);
						this._activeCells.Add(i, beatmapEditorTableCell2);
					}
				}
				this._prevMinRow = num2;
				this._prevMaxRow = num3;
				return;
			}
			if (num2 == this._prevMinRow && num3 == this._prevMaxRow)
			{
				return;
			}
			this._keysToRemove.Clear();
			foreach (int num4 in this._activeCells.Keys)
			{
				if (num4 < num2 || num4 > num3)
				{
					this._keysToRemove.Add(num4);
				}
			}
			for (int j = 0; j < this._keysToRemove.Count; j++)
			{
				int key = this._keysToRemove[j];
				BeatmapEditorTableCell beatmapEditorTableCell3 = this._activeCells[key];
				this._activeCells.Remove(key);
				beatmapEditorTableCell3.gameObject.SetActive(false);
				List<BeatmapEditorTableCell> list2;
				if (!this._reusableCells.TryGetValue(beatmapEditorTableCell3.reuseIdentifier, out list2))
				{
					list2 = new List<BeatmapEditorTableCell>();
					this._reusableCells.Add(beatmapEditorTableCell3.reuseIdentifier, list2);
				}
				list2.Add(beatmapEditorTableCell3);
			}
			for (int k = num2; k <= Mathf.Min(this._prevMinRow - 1, num3); k++)
			{
				BeatmapEditorTableCell beatmapEditorTableCell4 = this.CellForRow(k);
				if (beatmapEditorTableCell4 != null)
				{
					this.SetupCellForRow(beatmapEditorTableCell4, k);
					this._activeCells.Add(k, beatmapEditorTableCell4);
				}
			}
			for (int l = Mathf.Max(this._prevMaxRow + 1, num2); l <= num3; l++)
			{
				BeatmapEditorTableCell beatmapEditorTableCell5 = this.CellForRow(l);
				if (beatmapEditorTableCell5)
				{
					this.SetupCellForRow(beatmapEditorTableCell5, l);
					this._activeCells.Add(l, beatmapEditorTableCell5);
				}
			}
			this._prevMinRow = num2;
			this._prevMaxRow = num3;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0005C288 File Offset: 0x0005A488
		private void SetupCellForRow(BeatmapEditorTableCell cell, int row)
		{
			float rowHeight = this._beatmapEditorScrollView.rowHeight;
			cell.gameObject.transform.SetParent(this._cellsContainer, false);
			cell.gameObject.SetActive(true);
			((RectTransform)cell.transform).anchoredPosition = new Vector2(base.transform.localPosition.x, (float)row * rowHeight + this._playHeadOffset);
		}

		// Token: 0x04001958 RID: 6488
		[SerializeField]
		protected BeatmapEditorScrollView _beatmapEditorScrollView;

		// Token: 0x04001959 RID: 6489
		[SerializeField]
		private Transform _cellsContainer;

		// Token: 0x0400195A RID: 6490
		private int _prevMinRow = int.MaxValue;

		// Token: 0x0400195B RID: 6491
		private int _prevMaxRow = int.MinValue;

		// Token: 0x0400195C RID: 6492
		private float _playHeadOffset;

		// Token: 0x0400195D RID: 6493
		private Dictionary<int, BeatmapEditorTableCell> _activeCells;

		// Token: 0x0400195E RID: 6494
		private Dictionary<string, List<BeatmapEditorTableCell>> _reusableCells;

		// Token: 0x0400195F RID: 6495
		private ScrollRect _scrollRect;

		// Token: 0x04001960 RID: 6496
		private List<int> _keysToRemove = new List<int>();
	}
}
