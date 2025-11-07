using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000511 RID: 1297
	public class KeyboardShortcutManager : MonoBehaviour
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x00056990 File Offset: 0x00054B90
		protected void Update()
		{
			bool anyKey = Input.anyKey;
			bool flag = this.wasAnyKeyDown != anyKey;
			bool flag2 = anyKey || flag;
			this.wasAnyKeyDown = anyKey;
			if (flag2)
			{
				foreach (KeyValuePair<KeyboardShortcut, List<Action>> keyValuePair in this._keyboardShortcuts)
				{
					KeyboardShortcut key = keyValuePair.Key;
					bool flag3 = key.key1 != KeyCode.None && key.key2 > KeyCode.None;
					bool flag4 = false;
					if (key.isContinuous)
					{
						if (flag3)
						{
							flag4 = (Input.GetKey(key.key1) && Input.GetKey(key.key2));
						}
						else
						{
							flag4 = (Input.GetKey(key.key1) || Input.GetKey(key.key2));
						}
					}
					else if (key.keyPressType == KeyPressType.Down)
					{
						bool keyDown = Input.GetKeyDown(key.key1);
						bool keyDown2 = Input.GetKeyDown(key.key2);
						if (flag3)
						{
							flag4 = ((keyDown && Input.GetKey(key.key2)) || (keyDown2 && Input.GetKey(key.key1)));
						}
						else
						{
							flag4 = (keyDown || keyDown2);
						}
					}
					else if (key.keyPressType == KeyPressType.Up)
					{
						bool keyUp = Input.GetKeyUp(key.key1);
						bool keyUp2 = Input.GetKeyUp(key.key2);
						if (flag3)
						{
							flag4 = ((keyUp && Input.GetKey(key.key2)) || (keyUp2 && Input.GetKey(key.key1)));
						}
						else
						{
							flag4 = (keyUp || keyUp2);
						}
					}
					bool flag5 = (key.modificationKey1 == KeyCode.None || Input.GetKey(key.modificationKey1)) && (key.modificationKey2 == KeyCode.None || Input.GetKey(key.modificationKey2));
					if (flag4 && flag5)
					{
						foreach (Action action in keyValuePair.Value)
						{
							action();
						}
					}
				}
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00056BCC File Offset: 0x00054DCC
		public void AddKeyboardShortcuts(Dictionary<KeyboardShortcut, Action> shortcuts)
		{
			foreach (KeyValuePair<KeyboardShortcut, Action> keyValuePair in shortcuts)
			{
				this.AddKeyboardShortcut(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00056C28 File Offset: 0x00054E28
		public void AddKeyboardShortcut(KeyboardShortcut shortcut, Action action)
		{
			List<Action> list = null;
			if (!this._keyboardShortcuts.TryGetValue(shortcut, out list))
			{
				list = new List<Action>();
				this._keyboardShortcuts[shortcut] = list;
			}
			list.Add(action);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00056C64 File Offset: 0x00054E64
		public void RemoveKeyboardShortcuts(Dictionary<KeyboardShortcut, Action> actions)
		{
			foreach (KeyValuePair<KeyboardShortcut, Action> keyValuePair in actions)
			{
				this.RemoveKeyboardShortcut(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00056CC0 File Offset: 0x00054EC0
		public void RemoveKeyboardShortcut(KeyboardShortcut shortcut, Action action)
		{
			List<Action> list = null;
			if (this._keyboardShortcuts.TryGetValue(shortcut, out list))
			{
				list.Remove(action);
			}
		}

		// Token: 0x0400181F RID: 6175
		private Dictionary<KeyboardShortcut, List<Action>> _keyboardShortcuts = new Dictionary<KeyboardShortcut, List<Action>>();

		// Token: 0x04001820 RID: 6176
		private bool wasAnyKeyDown;
	}
}
