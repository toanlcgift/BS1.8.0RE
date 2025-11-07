using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200051A RID: 1306
	public class CopyAndPasteKeyboardShortcuts : MonoBehaviour
	{
		// Token: 0x060018CA RID: 6346 RVA: 0x0005761C File Offset: 0x0005581C
		protected void Start()
		{
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action>
			{
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad1, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(1);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad2, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(2);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad3, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(3);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad4, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(4);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad5, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(5);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad6, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(6);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad7, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(7);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad8, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(8);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.Keypad9, KeyCode.C, KeyCode.None),
					delegate()
					{
						this.CopyIfActiveInUI(9);
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.V, KeyCode.None, KeyCode.None),
					delegate()
					{
						this.PasteIfActiveInUI();
					}
				}
			};
			this._keyboardShortcutsHelper.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00012688 File Offset: 0x00010888
		protected void OnDestroy()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutsHelper.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000126A3 File Offset: 0x000108A3
		private void CopyIfActiveInUI(int numberOfBars)
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._copyAndPasteController.CopyBeatSegment(numberOfBars);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000126C6 File Offset: 0x000108C6
		private void PasteIfActiveInUI()
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._copyAndPasteController.PasteBeatSement();
		}

		// Token: 0x04001853 RID: 6227
		[SerializeField]
		private CopyAndPasteController _copyAndPasteController;

		// Token: 0x04001854 RID: 6228
		[SerializeField]
		private KeyboardShortcutsManager _keyboardShortcutsHelper;

		// Token: 0x04001855 RID: 6229
		[SerializeField]
		private RaycastUITopLevelChecker _raycastTopLevelChecker;

		// Token: 0x04001856 RID: 6230
		private Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
