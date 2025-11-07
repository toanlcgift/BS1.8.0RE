using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200056C RID: 1388
	public class EventsTableCell : BeatmapEditorTableCell
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x000023E9 File Offset: 0x000005E9
		protected void Awake()
		{
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0005DE8C File Offset: 0x0005C08C
		public void SetLineActive(int lineIdx, bool active, Color color, Sprite image, string text)
		{
			this._backgrounds[lineIdx].gameObject.SetActive(active || color.a > 0f);
			this._backgrounds[lineIdx].color = color;
			this._images[lineIdx].gameObject.SetActive(active && image != null);
			this._images[lineIdx].sprite = image;
			this._texts[lineIdx].gameObject.SetActive(active && image == null);
			this._texts[lineIdx].text = text;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00013FD5 File Offset: 0x000121D5
		public void SetDescriptionText(string text, Color color)
		{
			this._descriptionText.color = color;
			this._descriptionText.text = text;
		}

		// Token: 0x040019D1 RID: 6609
		[SerializeField]
		private Image[] _backgrounds;

		// Token: 0x040019D2 RID: 6610
		[SerializeField]
		private Image[] _images;

		// Token: 0x040019D3 RID: 6611
		[SerializeField]
		private TextMeshProUGUI[] _texts;

		// Token: 0x040019D4 RID: 6612
		[SerializeField]
		private TextMeshProUGUI _descriptionText;
	}
}
