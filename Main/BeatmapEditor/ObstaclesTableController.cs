using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BeatmapEditor
{
	// Token: 0x02000582 RID: 1410
	public class ObstaclesTableController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06001B76 RID: 7030 RVA: 0x0001450C File Offset: 0x0001270C
		protected void Awake()
		{
			this._obstaclesTable.Init(this._editorBeatmap);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0005EBF4 File Offset: 0x0005CDF4
		public void OnPointerClick(PointerEventData pointerEventData)
		{
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out v);
			Vector2Int vector2Int = this._obstaclesTable.GridPosForLocalPos(v);
			if (pointerEventData.button == PointerEventData.InputButton.Left)
			{
				this._editorBeatmap.AddObstacle(vector2Int.y, vector2Int.x, this._obstacleLength.obstacleLength, this._obstacleType.obstacleType);
				this._obstaclesTable.UpdateAllCells();
				return;
			}
			if (pointerEventData.button == PointerEventData.InputButton.Right)
			{
				this._editorBeatmap.EraseObstacle(vector2Int.y, vector2Int.x);
				this._obstaclesTable.UpdateAllCells();
			}
		}

		// Token: 0x04001A1B RID: 6683
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001A1C RID: 6684
		[SerializeField]
		private ObstaclesTable _obstaclesTable;

		// Token: 0x04001A1D RID: 6685
		[Inject]
		private IBeatmapEditorObstacleLength _obstacleLength;

		// Token: 0x04001A1E RID: 6686
		[Inject]
		private IBeatmapEditorObstacleType _obstacleType;
	}
}
