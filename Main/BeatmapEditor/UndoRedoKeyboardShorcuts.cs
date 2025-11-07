using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200051D RID: 1309
	public class UndoRedoKeyboardShorcuts : MonoBehaviour
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x00057908 File Offset: 0x00055B08
		protected void Start()
		{
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action>
			{
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Z, KeyCode.LeftControl, KeyCode.None),
					delegate()
					{
						this.UndoIfActiveInUI();
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Y, KeyCode.LeftControl, KeyCode.None),
					delegate()
					{
						this.RedoIfActiveInUI();
					}
				}
			};
			this._keyboardShortcutsHelper.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00012875 File Offset: 0x00010A75
		protected void OnDestroy()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutsHelper.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00012890 File Offset: 0x00010A90
		private void UndoIfActiveInUI()
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._editorBeatmap.Undo();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000128B2 File Offset: 0x00010AB2
		private void RedoIfActiveInUI()
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._editorBeatmap.Redo();
		}

		// Token: 0x04001860 RID: 6240
		[SerializeField]
		private EditorBeatmapSO _editorBeatmap;

		// Token: 0x04001861 RID: 6241
		[SerializeField]
		private KeyboardShortcutsManager _keyboardShortcutsHelper;

		// Token: 0x04001862 RID: 6242
		[SerializeField]
		private RaycastUITopLevelChecker _raycastTopLevelChecker;

		// Token: 0x04001863 RID: 6243
		private Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
