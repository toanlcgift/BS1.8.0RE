using System;
using System.Collections;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200058B RID: 1419
	public class SaveAsPanelController : MonoBehaviour
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00014864 File Offset: 0x00012A64
		public bool isShown
		{
			get
			{
				return this._isShown;
			}
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0001486C File Offset: 0x00012A6C
		public IEnumerator ShowCoroutine(string defaultName, Action<string> finishCallback)
		{
			this.Show(defaultName, finishCallback);
			WaitUntil waitUntil = new WaitUntil(() => !this._isShown);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0005F340 File Offset: 0x0005D540
		public void Show(string defaultName, Action<string> finishCallback)
		{
			this._isShown = true;
			base.gameObject.SetActive(true);
			this._inputField.text = defaultName;
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._saveButton,
					delegate()
					{
						finishCallback(this._inputField.text);
					}
				},
				{
					this._closeButton,
					delegate()
					{
						finishCallback(null);
					}
				}
			});
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00014889 File Offset: 0x00012A89
		public void Hide()
		{
			base.gameObject.SetActive(false);
			this._buttonBinder.ClearBindings();
			this._isShown = false;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000148A9 File Offset: 0x00012AA9
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x04001A46 RID: 6726
		[SerializeField]
		private InputField _inputField;

		// Token: 0x04001A47 RID: 6727
		[SerializeField]
		private Button _saveButton;

		// Token: 0x04001A48 RID: 6728
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04001A49 RID: 6729
		private bool _isShown;

		// Token: 0x04001A4A RID: 6730
		private ButtonBinder _buttonBinder;
	}
}
