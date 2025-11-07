using System;
using System.Collections;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000575 RID: 1397
	public class HelpPanelController : MonoBehaviour
	{
		// Token: 0x06001B31 RID: 6961 RVA: 0x000141ED File Offset: 0x000123ED
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00014202 File Offset: 0x00012402
		public IEnumerator ShowCoroutine(Action finishCallback)
		{
			this.Show(finishCallback);
			WaitUntil waitUntil = new WaitUntil(() => !this._isShown);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0005E4D0 File Offset: 0x0005C6D0
		public void Show(Action finishCallback)
		{
			this._isShown = true;
			base.gameObject.SetActive(true);
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			this._buttonBinder = new ButtonBinder();
			this._buttonBinder.AddBinding(this._closeButton, delegate
			{
				Action finishCallback2 = finishCallback;
				if (finishCallback2 == null)
				{
					return;
				}
				finishCallback2();
			});
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00014218 File Offset: 0x00012418
		public void Hide()
		{
			base.gameObject.SetActive(false);
			this._isShown = false;
		}

		// Token: 0x040019EC RID: 6636
		[SerializeField]
		private Button _closeButton;

		// Token: 0x040019ED RID: 6637
		private bool _isShown;

		// Token: 0x040019EE RID: 6638
		private ButtonBinder _buttonBinder;

		// Token: 0x040019EF RID: 6639
		private string[] _levelsNames;

		// Token: 0x040019F0 RID: 6640
		private string[] _levelsDirectories;
	}
}
