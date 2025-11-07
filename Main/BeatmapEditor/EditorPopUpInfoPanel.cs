using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200055C RID: 1372
	public class EditorPopUpInfoPanel : MonoBehaviour
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00013BD1 File Offset: 0x00011DD1
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00013BC3 File Offset: 0x00011DC3
		public Color color
		{
			get
			{
				return this._bgImage.color;
			}
			set
			{
				this._bgImage.color = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x00013BEC File Offset: 0x00011DEC
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x00013BDE File Offset: 0x00011DDE
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00013C0C File Offset: 0x00011E0C
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x00013BF9 File Offset: 0x00011DF9
		public Vector3 anchoredPosition
		{
			get
			{
				return this._transform.anchoredPosition;
			}
			set
			{
				this._transform.anchoredPosition = value;
			}
		}

		// Token: 0x0400198A RID: 6538
		[SerializeField]
		private Text _text;

		// Token: 0x0400198B RID: 6539
		[SerializeField]
		private Image _bgImage;

		// Token: 0x0400198C RID: 6540
		[SerializeField]
		private RectTransform _transform;
	}
}
