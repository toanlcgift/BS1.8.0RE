using System;
using TMPro;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004FC RID: 1276
	public class UIActivityIndicatorText : MonoBehaviour, IUIActivityIndicatorText
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00011BAD File Offset: 0x0000FDAD
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

		// Token: 0x06001808 RID: 6152 RVA: 0x00011BBB File Offset: 0x0000FDBB
		public void Show()
		{
			this._rootGameObject.SetActive(true);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00011BC9 File Offset: 0x0000FDC9
		public void Show(string text)
		{
			this.text = text;
			this.Show();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		public void Hide()
		{
			this._rootGameObject.SetActive(false);
		}

		// Token: 0x040017AD RID: 6061
		[SerializeField]
		private GameObject _rootGameObject;

		// Token: 0x040017AE RID: 6062
		[SerializeField]
		private TextMeshProUGUI _text;
	}
}
