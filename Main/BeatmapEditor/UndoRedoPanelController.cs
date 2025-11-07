using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000592 RID: 1426
	public class UndoRedoPanelController : MonoBehaviour
	{
		// Token: 0x06001BEB RID: 7147 RVA: 0x0005F788 File Offset: 0x0005D988
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._undoButton,
					delegate()
					{
						this._editorBeatmap.Undo();
					}
				},
				{
					this._redoButton,
					delegate()
					{
						this._editorBeatmap.Redo();
					}
				}
			});
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00014A8D File Offset: 0x00012C8D
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x04001A6C RID: 6764
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001A6D RID: 6765
		[Space]
		[SerializeField]
		private Button _undoButton;

		// Token: 0x04001A6E RID: 6766
		[SerializeField]
		private Button _redoButton;

		// Token: 0x04001A6F RID: 6767
		private ButtonBinder _buttonBinder;
	}
}
