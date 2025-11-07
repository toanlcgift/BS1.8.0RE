using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x0200058F RID: 1423
	public class ShufflePanelController : MonoBehaviour
	{
		// Token: 0x06001BD5 RID: 7125 RVA: 0x00014966 File Offset: 0x00012B66
		protected void Start()
		{
			this.RefreshUI();
			this._shufflePeriod.didChangeEvent += this.HandleShufflePeriodDidChange;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00014985 File Offset: 0x00012B85
		protected void OnDestroy()
		{
			if (this._shufflePeriod != null)
			{
				this._shufflePeriod.didChangeEvent -= this.HandleShufflePeriodDidChange;
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0005F52C File Offset: 0x0005D72C
		private void RefreshUI()
		{
			this._shufflePeriodText.text = Mathf.RoundToInt(4f / this._shufflePeriod).ToString();
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000149AC File Offset: 0x00012BAC
		private void HandleShufflePeriodDidChange()
		{
			this.RefreshUI();
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0005F564 File Offset: 0x0005D764
		public void ShufflePeriodMul(float mul)
		{
			float num = this._shufflePeriod * mul;
			if (num > 0.5f)
			{
				this._shufflePeriod.value = 0.5f;
				return;
			}
			if (num < 0.125f)
			{
				this._shufflePeriod.value = 0.125f;
				return;
			}
			this._shufflePeriod.value = num;
		}

		// Token: 0x04001A59 RID: 6745
		[SerializeField]
		private FloatSO _shufflePeriod;

		// Token: 0x04001A5A RID: 6746
		[SerializeField]
		private Text _shufflePeriodText;
	}
}
