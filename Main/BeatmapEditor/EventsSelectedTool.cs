using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000566 RID: 1382
	public class EventsSelectedTool : MonoBehaviour
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00013E2F File Offset: 0x0001202F
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x00013E37 File Offset: 0x00012037
		public ObstacleType selectedType { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00013E40 File Offset: 0x00012040
		// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x00013E48 File Offset: 0x00012048
		public int value { get; private set; }

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00013E51 File Offset: 0x00012051
		protected void Awake()
		{
			this.value = 0;
			this._valueText.text = "0";
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0005D770 File Offset: 0x0005B970
		public void IncreaseValue()
		{
			int value = this.value;
			this.value = value + 1;
			this.value = Mathf.Clamp(this.value, 0, 7);
			this._valueText.text = this.value.ToString();
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0005D7BC File Offset: 0x0005B9BC
		public void DecreaseValue()
		{
			int value = this.value;
			this.value = value - 1;
			this.value = Mathf.Clamp(this.value, 0, 7);
			this._valueText.text = this.value.ToString();
		}

		// Token: 0x040019B5 RID: 6581
		[SerializeField]
		private Text _valueText;
	}
}
