using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000568 RID: 1384
	public class BeatLineTableCell : BeatmapEditorTableCell
	{
		// Token: 0x170004EF RID: 1263
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x00013E8D File Offset: 0x0001208D
		public string text
		{
			set
			{
				this._text.text = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x00013E9B File Offset: 0x0001209B
		public BeatLineTableCell.Type type
		{
			set
			{
				if (value == this._type)
				{
					return;
				}
				this._type = value;
				if (value == BeatLineTableCell.Type.Bar)
				{
					this._lineImage.sprite = this._barLineSprite;
					return;
				}
				if (value == BeatLineTableCell.Type.Subdivision)
				{
					this._lineImage.sprite = this._subdivisionLineSprite;
				}
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x00013ED8 File Offset: 0x000120D8
		public float alpha
		{
			set
			{
				if (this._alpha == value)
				{
					return;
				}
				this._lineImage.color = this._lineImage.color.ColorWithAlpha(value);
				this._alpha = value;
			}
		}

		// Token: 0x040019BE RID: 6590
		[SerializeField]
		private Image _lineImage;

		// Token: 0x040019BF RID: 6591
		[SerializeField]
		private Sprite _barLineSprite;

		// Token: 0x040019C0 RID: 6592
		[SerializeField]
		private Sprite _subdivisionLineSprite;

		// Token: 0x040019C1 RID: 6593
		[SerializeField]
		private Text _text;

		// Token: 0x040019C2 RID: 6594
		private BeatLineTableCell.Type _type;

		// Token: 0x040019C3 RID: 6595
		private float _alpha;

		// Token: 0x02000569 RID: 1385
		public enum Type
		{
			// Token: 0x040019C5 RID: 6597
			Bar,
			// Token: 0x040019C6 RID: 6598
			Subdivision
		}
	}
}
