using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200051B RID: 1307
	public class NoteTypeAndCutDirectionKeyboardChanger : MonoBehaviour
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00057778 File Offset: 0x00055978
		protected void Start()
		{
			this._keyboardShortcutActions = new Dictionary<KeyCode, Action>
			{
				{
					KeyCode.Keypad0,
					delegate()
					{
						this.SelectNoteTypeIfActiveInUI(NoteType.NoteA);
					}
				},
				{
					KeyCode.KeypadPeriod,
					delegate()
					{
						this.SelectNoteTypeIfActiveInUI(NoteType.NoteB);
					}
				},
				{
					KeyCode.Keypad1,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.DownLeft);
					}
				},
				{
					KeyCode.Keypad2,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.Down);
					}
				},
				{
					KeyCode.Keypad3,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.DownRight);
					}
				},
				{
					KeyCode.Keypad4,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.Left);
					}
				},
				{
					KeyCode.Keypad5,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.Any);
					}
				},
				{
					KeyCode.Keypad6,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.Right);
					}
				},
				{
					KeyCode.Keypad7,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.UpLeft);
					}
				},
				{
					KeyCode.Keypad8,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.Up);
					}
				},
				{
					KeyCode.Keypad9,
					delegate()
					{
						this.SelectNoteCutDirectionIfActiveInUI(NoteCutDirection.UpRight);
					}
				}
			};
			this._keyboardShortcutsHelper.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00012742 File Offset: 0x00010942
		protected void OnDestroy()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutsHelper.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0001275D File Offset: 0x0001095D
		private void SelectNoteTypeIfActiveInUI(NoteType noteType)
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._selectedNoteType.value = noteType;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00012780 File Offset: 0x00010980
		private void SelectNoteCutDirectionIfActiveInUI(NoteCutDirection noteCutDirection)
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._selectedNoteCutDirection.value = noteCutDirection;
		}

		// Token: 0x04001857 RID: 6231
		[SerializeField]
		private EditorSelectedNoteTypeSO _selectedNoteType;

		// Token: 0x04001858 RID: 6232
		[SerializeField]
		private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

		// Token: 0x04001859 RID: 6233
		[Space]
		[SerializeField]
		private KeyboardShortcutsManager _keyboardShortcutsHelper;

		// Token: 0x0400185A RID: 6234
		[SerializeField]
		private RaycastUITopLevelChecker _raycastTopLevelChecker;

		// Token: 0x0400185B RID: 6235
		private Dictionary<KeyCode, Action> _keyboardShortcutActions;
	}
}
