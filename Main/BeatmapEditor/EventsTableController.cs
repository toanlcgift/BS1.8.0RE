using System;
using HMUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000567 RID: 1383
	public class EventsTableController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x0005D808 File Offset: 0x0005BA08
		protected void Awake()
		{
			this._eventsTable.Init(this._editorBeatmap, this._eventSetDrawStyle, this._plus16Toggle.isOn);
			this._toggleBinder = new ToggleBinder();
			this._toggleBinder.AddBinding(this._plus16Toggle, delegate(bool on)
			{
				this._eventsTable.plus16 = on;
			});
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00013E6A File Offset: 0x0001206A
		protected void OnDestroy()
		{
			if (this._toggleBinder != null)
			{
				this._toggleBinder.ClearBindings();
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0005D860 File Offset: 0x0005BA60
		public void OnPointerClick(PointerEventData pointerEventData)
		{
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out v);
			Vector2Int vector2Int = this._eventsTable.GridPosForLocalPos(v);
			int y = vector2Int.y;
			int num = vector2Int.x + (this._plus16Toggle.isOn ? 16 : 0);
			if (pointerEventData.button == PointerEventData.InputButton.Left)
			{
				EventDrawStyleSO eventDrawStyle = this._eventSetDrawStyle.specificEvents[num].eventDrawStyle;
				if (eventDrawStyle != null)
				{
					EditorEventData eventData = new EditorEventData(this._selectedBeatmapEventValues.GetEventValueForBeatmapEventWithID(eventDrawStyle.eventID), false);
					this._editorBeatmap.AddEvent(y, num, eventData);
					this._eventsTable.UpdateAllCells();
					return;
				}
			}
			else if (pointerEventData.button == PointerEventData.InputButton.Right)
			{
				this._editorBeatmap.EraseEvent(y, num);
				this._eventsTable.UpdateAllCells();
			}
		}

		// Token: 0x040019B8 RID: 6584
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x040019B9 RID: 6585
		[SerializeField]
		private EventSetDrawStyleSO _eventSetDrawStyle;

		// Token: 0x040019BA RID: 6586
		[SerializeField]
		private EditorSelectedBeatmapEventValues _selectedBeatmapEventValues;

		// Token: 0x040019BB RID: 6587
		[Space]
		[SerializeField]
		private EventsTable _eventsTable;

		// Token: 0x040019BC RID: 6588
		[SerializeField]
		private Toggle _plus16Toggle;

		// Token: 0x040019BD RID: 6589
		private ToggleBinder _toggleBinder;
	}
}
