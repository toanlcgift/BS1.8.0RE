using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BeatmapEditor
{
	// Token: 0x02000578 RID: 1400
	[Serializable]
	public class MouseClickHandlerEvent : UnityEvent<PointerEventData.InputButton>
	{
	}
}
