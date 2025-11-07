using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor
{
	// Token: 0x02000579 RID: 1401
	public class MouseClickHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0005E59C File Offset: 0x0005C79C
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == this._mouseButton && Vector3.Distance(eventData.pressPosition, eventData.position) < 5f)
			{
				this._clickEvent.Invoke(this._mouseButton);
			}
		}

		// Token: 0x040019F6 RID: 6646
		[SerializeField]
		private MouseClickHandlerEvent _clickEvent;

		// Token: 0x040019F7 RID: 6647
		[SerializeField]
		private PointerEventData.InputButton _mouseButton;

		// Token: 0x040019F8 RID: 6648
		private Vector3 _prevMousePos;

		// Token: 0x040019F9 RID: 6649
		private bool _mouseDown;
	}
}
