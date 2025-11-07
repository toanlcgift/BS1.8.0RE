using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor
{
	// Token: 0x0200057D RID: 1405
	public class NotesTableController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler
	{
		// Token: 0x06001B61 RID: 7009 RVA: 0x0001446F File Offset: 0x0001266F
		protected void Awake()
		{
			this._notesTable.Init(this._editorBeatmap, this._noteLineLayer);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0005E9F0 File Offset: 0x0005CBF0
		public void OnPointerClick(PointerEventData pointerEventData)
		{
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out v);
			this._canvasCamera = pointerEventData.enterEventCamera;
			Vector2Int vector2Int = this._notesTable.GridPosForLocalPos(v);
			if (pointerEventData.button == PointerEventData.InputButton.Left)
			{
				this._editorBeatmap.AddNote(vector2Int.y, this._noteLineLayer, vector2Int.x, new EditorNoteData(this._selectedNoteType.value, this._selectedNoteCutDirection.value, this._noteLineLayer, vector2Int.x, vector2Int.y, false));
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00014488 File Offset: 0x00012688
		public void OnPointerDown(PointerEventData pointerEventData)
		{
			if (pointerEventData.button == PointerEventData.InputButton.Right && this._canvasCamera != null)
			{
				this._prevGridPos = new Vector2Int(-1, -1);
				base.StartCoroutine(this.ErasingCoroutine());
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000144BB File Offset: 0x000126BB
		private IEnumerator ErasingCoroutine()
		{
			while (Input.GetMouseButton(1))
			{
				Vector2 v;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, Input.mousePosition, this._canvasCamera, out v);
				Vector2Int vector2Int = this._notesTable.GridPosForLocalPos(v);
				if (this._prevGridPos != vector2Int)
				{
					this._editorBeatmap.EraseNote(vector2Int.y, this._noteLineLayer, vector2Int.x);
					this._prevGridPos = vector2Int;
					this._notesTable.UpdateAllCells();
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04001A0E RID: 6670
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001A0F RID: 6671
		[Space]
		[SerializeField]
		private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

		// Token: 0x04001A10 RID: 6672
		[SerializeField]
		private EditorSelectedNoteTypeSO _selectedNoteType;

		// Token: 0x04001A11 RID: 6673
		[Space]
		[SerializeField]
		private NoteLineLayer _noteLineLayer;

		// Token: 0x04001A12 RID: 6674
		[SerializeField]
		private NotesTable _notesTable;

		// Token: 0x04001A13 RID: 6675
		private Vector2Int _prevGridPos;

		// Token: 0x04001A14 RID: 6676
		private Camera _canvasCamera;
	}
}
