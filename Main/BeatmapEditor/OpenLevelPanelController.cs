using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000583 RID: 1411
	public class OpenLevelPanelController : MonoBehaviour
	{
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06001B79 RID: 7033 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
		// (remove) Token: 0x06001B7A RID: 7034 RVA: 0x0005ECDC File Offset: 0x0005CEDC
		public event Action<string> didDeleteLevelEvent;

		// Token: 0x06001B7B RID: 7035 RVA: 0x0001451F File Offset: 0x0001271F
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00014534 File Offset: 0x00012734
		public IEnumerator ShowCoroutine(Action<string> finishCallback)
		{
			this.Show(finishCallback);
			WaitUntil waitUntil = new WaitUntil(() => !this._isShown);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0005ED14 File Offset: 0x0005CF14
		public void Show(Action<string> finishCallback)
		{
			this._isShown = true;
			this.GetLevels();
			base.gameObject.SetActive(true);
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			this._buttonBinder = new ButtonBinder();
			this._buttonBinder.AddBinding(this._closeButton, delegate
			{
				Action<string> finishCallback2 = finishCallback;
				if (finishCallback2 == null)
				{
					return;
				}
				finishCallback2(null);
			});
			Action buttonAction = null;
			this._openLevelTableController.Init(this._levelsNames, delegate(int row)
			{
				Action<string> finishCallback2 = finishCallback;
				if (finishCallback2 == null)
				{
					return;
				}
				finishCallback2(this._levelsDirectories[row]);
			}, delegate(int row)
			{
				AlertPanelController alertPanelController = this._alertPanelController;
				string title = "Delete Level";
				string subtitle = "Do you really want to delete this level?";
				string button0Text = "Cancel";
				Action button0Action;
				if ((button0Action = buttonAction) == null)
				{
					button0Action = (buttonAction = delegate()
					{
						this._alertPanelController.Hide();
					});
				}
				alertPanelController.Show(title, subtitle, button0Text, button0Action, "Delete", delegate()
				{
					this._alertPanelController.Hide();
					string text = this._levelsDirectories[row];
					this.DeleteLevel(text);
					this.GetLevels();
					this._openLevelTableController.SetContent(this._levelsNames);
					Action<string> action = this.didDeleteLevelEvent;
					if (action == null)
					{
						return;
					}
					action(text);
				}, null, null);
			});
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0005EDB4 File Offset: 0x0005CFB4
		private void GetLevels()
		{
			this._levelsNames = null;
			this._levelsDirectories = null;
			try
			{
				List<string> list = new List<string>();
				if (Directory.Exists(CustomLevelPathHelper.customLevelsDirectoryPath))
				{
					list.AddRange(Directory.GetDirectories(CustomLevelPathHelper.customLevelsDirectoryPath));
				}
				this._levelsDirectories = list.ToArray();
				List<string> list2 = new List<string>();
				list.Clear();
				foreach (string text in this._levelsDirectories)
				{
					if (File.Exists(Path.Combine(text, "Info.dat")))
					{
						list2.Add(Path.GetFileName(text));
						list.Add(text);
					}
				}
				this._levelsNames = list2.ToArray();
				this._levelsDirectories = list.ToArray();
			}
			catch
			{
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0005EE78 File Offset: 0x0005D078
		private void DeleteLevel(string levelDirectoryPath)
		{
			try
			{
				Directory.Delete(levelDirectoryPath, true);
			}
			catch
			{
			}
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0001454A File Offset: 0x0001274A
		public void Hide()
		{
			base.gameObject.SetActive(false);
			this._isShown = false;
		}

		// Token: 0x04001A1F RID: 6687
		[SerializeField]
		private OpenLevelTableController _openLevelTableController;

		// Token: 0x04001A20 RID: 6688
		[SerializeField]
		private AlertPanelController _alertPanelController;

		// Token: 0x04001A21 RID: 6689
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04001A23 RID: 6691
		private bool _isShown;

		// Token: 0x04001A24 RID: 6692
		private ButtonBinder _buttonBinder;

		// Token: 0x04001A25 RID: 6693
		private string[] _levelsNames;

		// Token: 0x04001A26 RID: 6694
		private string[] _levelsDirectories;
	}
}
