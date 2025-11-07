using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x0200051C RID: 1308
	public class StandardLevelProjectKeyboardShortcuts : MonoBehaviour
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x000578A0 File Offset: 0x00055AA0
		protected void Start()
		{
			this._keyboardShortcutActions = new Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action>
			{
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.S, KeyCode.LeftControl, KeyCode.None),
					delegate()
					{
						this.SaveLevelIfActiveInUI();
					}
				},
				{
					new KeyboardShortcutsManager.KeyboardShortcut(KeyCode.T, KeyCode.LeftControl, KeyCode.None),
					delegate()
					{
						this.TestBeatmapIfActiveInUI();
					}
				}
			};
			this._keyboardShortcutsHelper.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00012806 File Offset: 0x00010A06
		protected void OnDestroy()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutsHelper.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00012821 File Offset: 0x00010A21
		private void SaveLevelIfActiveInUI()
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._standardLevelProjectController.SaveLevel();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00012843 File Offset: 0x00010A43
		private void TestBeatmapIfActiveInUI()
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this._standardLevelProjectController.TestBeatmap();
		}

		// Token: 0x0400185C RID: 6236
		[SerializeField]
		private StandardLevelProjectController _standardLevelProjectController;

		// Token: 0x0400185D RID: 6237
		[SerializeField]
		private KeyboardShortcutsManager _keyboardShortcutsHelper;

		// Token: 0x0400185E RID: 6238
		[SerializeField]
		private RaycastUITopLevelChecker _raycastTopLevelChecker;

		// Token: 0x0400185F RID: 6239
		private Dictionary<KeyboardShortcutsManager.KeyboardShortcut, Action> _keyboardShortcutActions;
	}
}
