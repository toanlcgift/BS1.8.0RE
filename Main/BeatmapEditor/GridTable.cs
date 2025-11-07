using System;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200056D RID: 1389
	public abstract class GridTable : BeatmapEditorTable
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001B05 RID: 6917
		protected abstract int numberOfColumns { get; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001B06 RID: 6918
		protected abstract float columnWidth { get; }

		// Token: 0x06001B07 RID: 6919 RVA: 0x0005DF2C File Offset: 0x0005C12C
		public Vector2Int GridPosForWorldPos(Vector3 worldPos)
		{
			float x = base.transform.InverseTransformPoint(worldPos).x;
			return new Vector2Int
			{
				x = Mathf.FloorToInt(x / this.columnWidth),
				y = this._beatmapEditorScrollView.GetRowForWorldPos(worldPos)
			};
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0005DF7C File Offset: 0x0005C17C
		public Vector2Int GridPosForLocalPos(Vector3 localPos)
		{
			Vector2Int result = default(Vector2Int);
			result.x = Mathf.FloorToInt(localPos.x / this.columnWidth);
			result.y = this._beatmapEditorScrollView.GetRowForWorldPos(base.transform.TransformPoint(localPos));
			result.x = Mathf.Clamp(result.x, 0, this.numberOfColumns - 1);
			return result;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
		public Vector2 GridCellBottomLeftPosforWorldPos(Vector3 worldPos)
		{
			Vector2Int vector2Int = this.GridPosForWorldPos(worldPos);
			float x = (float)vector2Int.x * this.columnWidth;
			float y = base.transform.InverseTransformPoint(new Vector2(0f, this._beatmapEditorScrollView.GetRowWorldPos(vector2Int.y))).y;
			return new Vector2(x, y);
		}
	}
}
