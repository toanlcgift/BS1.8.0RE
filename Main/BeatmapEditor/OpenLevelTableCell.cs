using System;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000587 RID: 1415
	public class OpenLevelTableCell : TableCell
	{
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06001B90 RID: 7056 RVA: 0x0005F018 File Offset: 0x0005D218
		// (remove) Token: 0x06001B91 RID: 7057 RVA: 0x0005F050 File Offset: 0x0005D250
		public event Action<int> deleteButtonPressedEvent;

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000145D3 File Offset: 0x000127D3
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x000145C5 File Offset: 0x000127C5
		public string text
		{
			get
			{
				return this._text.text;
			}
			set
			{
				this._text.text = value;
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000145E0 File Offset: 0x000127E0
		protected override void Start()
		{
			base.Start();
			this._buttonBinder = new ButtonBinder();
			this._buttonBinder.AddBinding(this._deleteButton, delegate
			{
				Action<int> action = this.deleteButtonPressedEvent;
				if (action == null)
				{
					return;
				}
				action(base.idx);
			});
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00014610 File Offset: 0x00012810
		protected void OnDestroy()
		{
			ButtonBinder buttonBinder = this._buttonBinder;
			if (buttonBinder == null)
			{
				return;
			}
			buttonBinder.ClearBindings();
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0005F088 File Offset: 0x0005D288
		protected override void SelectionDidChange(TableCell.TransitionType transitionType)
		{
			if (base.selected)
			{
				this._highlightImage.enabled = false;
				this._bgImage.enabled = true;
				this._text.color = Color.black;
				return;
			}
			this._bgImage.enabled = false;
			this._text.color = Color.white;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00014622 File Offset: 0x00012822
		protected override void HighlightDidChange(TableCell.TransitionType transitionType)
		{
			if (base.selected)
			{
				this._highlightImage.enabled = false;
				return;
			}
			this._highlightImage.enabled = base.highlighted;
			this._deleteButton.gameObject.SetActive(base.highlighted);
		}

		// Token: 0x04001A30 RID: 6704
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04001A31 RID: 6705
		[SerializeField]
		private Image _bgImage;

		// Token: 0x04001A32 RID: 6706
		[SerializeField]
		private Image _highlightImage;

		// Token: 0x04001A33 RID: 6707
		[SerializeField]
		private Button _deleteButton;

		// Token: 0x04001A35 RID: 6709
		private ButtonBinder _buttonBinder;
	}
}
