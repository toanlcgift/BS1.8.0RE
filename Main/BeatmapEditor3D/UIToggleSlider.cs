using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor3D
{
	// Token: 0x02000504 RID: 1284
	public class UIToggleSlider : MonoBehaviour
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00011DDE File Offset: 0x0000FFDE
		public UISlider slider
		{
			get
			{
				return this._slider;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00011DE6 File Offset: 0x0000FFE6
		public Toggle toggle
		{
			get
			{
				return this._toggle;
			}
		}

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x0600182C RID: 6188 RVA: 0x00055F04 File Offset: 0x00054104
		// (remove) Token: 0x0600182D RID: 6189 RVA: 0x00055F3C File Offset: 0x0005413C
		public event Action<bool> toggleDidChangeValueEvent;

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00011DEE File Offset: 0x0000FFEE
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x00011DFB File Offset: 0x0000FFFB
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

		// Token: 0x06001830 RID: 6192 RVA: 0x00011E09 File Offset: 0x00010009
		protected void Awake()
		{
			this._toggle.onValueChanged.AddListener(new UnityAction<bool>(this.HandleToggleDidChangeValue));
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00011E27 File Offset: 0x00010027
		protected void OnDestroy()
		{
			this._toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.HandleToggleDidChangeValue));
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00011E45 File Offset: 0x00010045
		private void HandleToggleDidChangeValue(bool value)
		{
			this._slider.interactable = value;
			Action<bool> action = this.toggleDidChangeValueEvent;
			if (action == null)
			{
				return;
			}
			action(value);
		}

		// Token: 0x040017C0 RID: 6080
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x040017C1 RID: 6081
		[SerializeField]
		private Toggle _toggle;

		// Token: 0x040017C2 RID: 6082
		[SerializeField]
		private UISlider _slider;
	}
}
