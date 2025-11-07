using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;

namespace BeatmapEditor
{
	// Token: 0x02000519 RID: 1305
	public class BeatmapEventValuesKeyboardChanger : MonoBehaviour
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x000574A0 File Offset: 0x000556A0
		protected void Start()
		{
			this._keyboardShortcutActions = new Dictionary<KeyCode, Action>
			{
				{
					KeyCode.Keypad0,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(0);
					}
				},
				{
					KeyCode.Keypad1,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(1);
					}
				},
				{
					KeyCode.Keypad2,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(2);
					}
				},
				{
					KeyCode.Keypad3,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(3);
					}
				},
				{
					KeyCode.Keypad4,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(4);
					}
				},
				{
					KeyCode.Keypad5,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(5);
					}
				},
				{
					KeyCode.Keypad6,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(6);
					}
				},
				{
					KeyCode.Keypad7,
					delegate()
					{
						this.SelectSubeventsWithOrderNumIfActiveInUI(7);
					}
				}
			};
			this._keyboardShortcutsHelper.AddKeyboardShortcuts(this._keyboardShortcutActions);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00012607 File Offset: 0x00010807
		protected void OnDestroy()
		{
			if (this._keyboardShortcutActions != null)
			{
				this._keyboardShortcutsHelper.RemoveKeyboardShortcuts(this._keyboardShortcutActions);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00012622 File Offset: 0x00010822
		private void SelectSubeventsWithOrderNumIfActiveInUI(int orderNum)
		{
			if (EventSystemHelpers.IsInputFieldSelected() || !this._raycastTopLevelChecker.isOnTop)
			{
				return;
			}
			this.SelectSubeventsWithOrderNum(orderNum);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00057584 File Offset: 0x00055784
		private void SelectSubeventsWithOrderNum(int orderNum)
		{
			HashSet<string> hashSet = new HashSet<string>();
			EventSetDrawStyleSO.SpecificEventDrawStyle[] specificEvents = this._eventSetDrawStyle.specificEvents;
			for (int i = 0; i < specificEvents.Length; i++)
			{
				EventDrawStyleSO eventDrawStyle = specificEvents[i].eventDrawStyle;
				if (eventDrawStyle != null && !hashSet.Contains(eventDrawStyle.eventID))
				{
					int num = 0;
					foreach (EventDrawStyleSO.SubEventDrawStyle subEventDrawStyle in eventDrawStyle.subEvents)
					{
						if (num == orderNum)
						{
							this._selectedBeatmapEventValues.SetSelectedBeatmapEventValue(eventDrawStyle.eventID, subEventDrawStyle.eventValue);
						}
						num++;
					}
				}
			}
		}

		// Token: 0x0400184E RID: 6222
		[SerializeField]
		private EventSetDrawStyleSO _eventSetDrawStyle;

		// Token: 0x0400184F RID: 6223
		[SerializeField]
		private EditorSelectedBeatmapEventValues _selectedBeatmapEventValues;

		// Token: 0x04001850 RID: 6224
		[Space]
		[SerializeField]
		private KeyboardShortcutsManager _keyboardShortcutsHelper;

		// Token: 0x04001851 RID: 6225
		[SerializeField]
		private RaycastUITopLevelChecker _raycastTopLevelChecker;

		// Token: 0x04001852 RID: 6226
		private Dictionary<KeyCode, Action> _keyboardShortcutActions;
	}
}
