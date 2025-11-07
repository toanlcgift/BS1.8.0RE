using System;
using TMPro;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000503 RID: 1283
	public class UITitleValue : MonoBehaviour
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00011DB5 File Offset: 0x0000FFB5
		public string titleText
		{
			get
			{
				return this._titleText.text;
			}
			set
			{
				this._titleText.text = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00011DC3 File Offset: 0x0000FFC3
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public string valueText
		{
			get
			{
				return this._valueText.text;
			}
			set
			{
				this._valueText.text = value;
			}
		}

		// Token: 0x040017BE RID: 6078
		[SerializeField]
		private TextMeshProUGUI _titleText;

		// Token: 0x040017BF RID: 6079
		[SerializeField]
		private TextMeshProUGUI _valueText;
	}
}
