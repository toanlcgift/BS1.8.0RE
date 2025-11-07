using System;

namespace BeatmapEditor
{
	// Token: 0x02000534 RID: 1332
	public class EditorSelectedNoteCutDirectionSO : ObservableVariableSO<NoteCutDirection>
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x00012F1C File Offset: 0x0001111C
		protected override void OnEnable()
		{
			base.OnEnable();
			base.value = NoteCutDirection.Up;
		}
	}
}
