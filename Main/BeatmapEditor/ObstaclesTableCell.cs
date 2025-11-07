using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor
{
	// Token: 0x02000574 RID: 1396
	public class ObstaclesTableCell : BeatmapEditorTableCell
	{
		// Token: 0x06001B2D RID: 6957 RVA: 0x000023E9 File Offset: 0x000005E9
		protected void Awake()
		{
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000141BA File Offset: 0x000123BA
		public void SetLineActive(int lineIdx, bool active)
		{
			this._backgrounds[lineIdx].gameObject.SetActive(active);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000141CF File Offset: 0x000123CF
		public void SetLineType(int lineIdx, ObstacleType type)
		{
			this._backgrounds[lineIdx].color = ((type == ObstacleType.FullHeight) ? Color.magenta : Color.green);
		}

		// Token: 0x040019EB RID: 6635
		[SerializeField]
		private Image[] _backgrounds;
	}
}
