using System;
using System.Collections;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200054E RID: 1358
	public class AlertPanelController : MonoBehaviour
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000136BF File Offset: 0x000118BF
		public bool isShown
		{
			get
			{
				return this._isShown;
			}
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000136C7 File Offset: 0x000118C7
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0005B794 File Offset: 0x00059994
		public IEnumerator ShowCoroutine(string title, string subtitle, string button0Text, Action button0Action, string button1Text = null, Action button1Action = null, string button2Text = null, Action button2Action = null)
		{
			this.Show(title, subtitle, button0Text, button0Action, button1Text, button1Action, button2Text, button2Action);
			WaitUntil waitUntil = new WaitUntil(() => !this._isShown);
			yield return waitUntil;
			yield break;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0005B7EC File Offset: 0x000599EC
		public void Show(string title, string subtitle, string button0Text, Action button0Action, string button1Text = null, Action button1Action = null, string button2Text = null, Action button2Action = null)
		{
			List<string> list = new List<string>();
			List<Action> list2 = new List<Action>();
			if (button0Text != null)
			{
				list.Add(button0Text);
			}
			if (button1Text != null)
			{
				list.Add(button1Text);
			}
			if (button2Text != null)
			{
				list.Add(button2Text);
			}
			if (button0Action != null)
			{
				list2.Add(button0Action);
			}
			if (button1Action != null)
			{
				list2.Add(button1Action);
			}
			if (button2Action != null)
			{
				list2.Add(button2Action);
			}
			this.Show(title, subtitle, list.ToArray(), list2.ToArray());
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0005B860 File Offset: 0x00059A60
		public void Show(string title, string subtitle, string[] buttonTexts, Action[] buttonActions)
		{
			this._isShown = true;
			if (this._textButtons == null)
			{
				this._textButtons = new TextButton[]
				{
					this._button0,
					this._button1,
					this._button2
				};
			}
			if (this._buttonBinder == null)
			{
				this._buttonBinder = new ButtonBinder();
			}
			this._titleText.text = title.ToUpper();
			this._subtitleText.text = subtitle;
			for (int i = 0; i < buttonTexts.Length; i++)
			{
				this._textButtons[i].text.text = buttonTexts[i].ToUpper();
				this._textButtons[i].gameObject.SetActive(true);
			}
			for (int j = buttonTexts.Length; j < 3; j++)
			{
				this._textButtons[j].gameObject.SetActive(false);
			}
			for (int k = 0; k < buttonActions.Length; k++)
			{
				this._buttonBinder.AddBinding(this._textButtons[k].button, buttonActions[k]);
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000136DC File Offset: 0x000118DC
		public void Hide()
		{
			base.gameObject.SetActive(false);
			this._buttonBinder.ClearBindings();
			this._isShown = false;
		}

		// Token: 0x04001929 RID: 6441
		private const int kMaxButtons = 3;

		// Token: 0x0400192A RID: 6442
		[SerializeField]
		private TextButton _button0;

		// Token: 0x0400192B RID: 6443
		[SerializeField]
		private TextButton _button1;

		// Token: 0x0400192C RID: 6444
		[SerializeField]
		private TextButton _button2;

		// Token: 0x0400192D RID: 6445
		[Space]
		[SerializeField]
		private Text _titleText;

		// Token: 0x0400192E RID: 6446
		[SerializeField]
		private Text _subtitleText;

		// Token: 0x0400192F RID: 6447
		private bool _isShown;

		// Token: 0x04001930 RID: 6448
		private TextButton[] _textButtons;

		// Token: 0x04001931 RID: 6449
		private ButtonBinder _buttonBinder;
	}
}
