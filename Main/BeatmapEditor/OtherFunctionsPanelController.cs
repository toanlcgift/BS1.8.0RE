using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000589 RID: 1417
	public class OtherFunctionsPanelController : MonoBehaviour
	{
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0005F294 File Offset: 0x0005D494
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._clearEventsButton,
					delegate()
					{
						this._editorBeatmap.EraseAllEvents();
					}
				},
				{
					this._clearNotesAndCoButton,
					delegate()
					{
						this._editorBeatmap.EraseAllNotesAndCo();
					}
				}
			});
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000146FD File Offset: 0x000128FD
		protected void OnDestroy()
		{
			ButtonBinder buttonBinder = this._buttonBinder;
			if (buttonBinder == null)
			{
				return;
			}
			buttonBinder.ClearBindings();
		}

		// Token: 0x04001A3D RID: 6717
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001A3E RID: 6718
		[Space]
		[SerializeField]
		private Button _clearEventsButton;

		// Token: 0x04001A3F RID: 6719
		[SerializeField]
		private Button _clearNotesAndCoButton;

		// Token: 0x04001A40 RID: 6720
		private ButtonBinder _buttonBinder;
	}
}
