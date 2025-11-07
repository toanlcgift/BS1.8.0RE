using System;
using System.Collections.Generic;
using HMUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatmapEditor3D
{
	// Token: 0x020004FE RID: 1278
	public class UISlider : MonoBehaviour
	{
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06001811 RID: 6161 RVA: 0x00055CF8 File Offset: 0x00053EF8
		// (remove) Token: 0x06001812 RID: 6162 RVA: 0x00055D30 File Offset: 0x00053F30
		public event Action<float> didChangeValueEvent;

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x00011C83 File Offset: 0x0000FE83
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x00011C90 File Offset: 0x0000FE90
		public float value
		{
			get
			{
				return this._slider.value;
			}
			set
			{
				this._slider.value = Mathf.Clamp(value, this._slider.minValue, this._slider.maxValue);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		public bool interactable
		{
			get
			{
				return this._slider.interactable;
			}
			set
			{
				this._slider.interactable = value;
				this._minusButton.interactable = value;
				this._plusButton.interactable = value;
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00055D68 File Offset: 0x00053F68
		protected void Awake()
		{
			this._buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
			{
				{
					this._minusButton,
					new Action(this.StepMinus)
				},
				{
					this._plusButton,
					new Action(this.StepPlus)
				}
			});
			this._slider.onValueChanged.AddListener(new UnityAction<float>(this.HandleDidChangeValue));
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00011CEC File Offset: 0x0000FEEC
		protected void OnDestroy()
		{
			if (this._buttonBinder != null)
			{
				this._buttonBinder.ClearBindings();
			}
			this._slider.onValueChanged.RemoveListener(new UnityAction<float>(this.HandleDidChangeValue));
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00011D1D File Offset: 0x0000FF1D
		private void CenterValue()
		{
			this.value = (this._slider.maxValue - this._slider.minValue) * 0.5f;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00011D42 File Offset: 0x0000FF42
		private void StepMinus()
		{
			this.value -= this._step;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00011D57 File Offset: 0x0000FF57
		private void StepPlus()
		{
			this.value += this._step;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00011D6C File Offset: 0x0000FF6C
		private void HandleDidChangeValue(float value)
		{
			Action<float> action = this.didChangeValueEvent;
			if (action == null)
			{
				return;
			}
			action(value);
		}

		// Token: 0x040017B8 RID: 6072
		[SerializeField]
		private Slider _slider;

		// Token: 0x040017B9 RID: 6073
		[SerializeField]
		private Button _minusButton;

		// Token: 0x040017BA RID: 6074
		[SerializeField]
		private Button _plusButton;

		// Token: 0x040017BB RID: 6075
		[SerializeField]
		[Range(0f, 1f)]
		private float _step = 0.05f;

		// Token: 0x040017BD RID: 6077
		private ButtonBinder _buttonBinder;
	}
}
