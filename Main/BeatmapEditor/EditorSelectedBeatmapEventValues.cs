using System;
using System.Collections.Generic;

namespace BeatmapEditor
{
	// Token: 0x02000533 RID: 1331
	public class EditorSelectedBeatmapEventValues : PersistentScriptableObject
	{
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x0600198B RID: 6539 RVA: 0x00059960 File Offset: 0x00057B60
		// (remove) Token: 0x0600198C RID: 6540 RVA: 0x00059998 File Offset: 0x00057B98
		public event Action selectedBeatmapEventValueDidChangeEvent;

		// Token: 0x0600198D RID: 6541 RVA: 0x000599D0 File Offset: 0x00057BD0
		public void SetSelectedBeatmapEventValue(string beatmapEventID, int beatmapEventValue)
		{
			int num = 0;
			if (this._selectedBeatmapEventValues.TryGetValue(beatmapEventID, out num) && num == beatmapEventValue)
			{
				return;
			}
			this._selectedBeatmapEventValues[beatmapEventID] = beatmapEventValue;
			Action action = this.selectedBeatmapEventValueDidChangeEvent;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00059A14 File Offset: 0x00057C14
		public int GetEventValueForBeatmapEventWithID(string beatmapEventID)
		{
			int result = 0;
			this._selectedBeatmapEventValues.TryGetValue(beatmapEventID, out result);
			return result;
		}

		// Token: 0x040018A2 RID: 6306
		private Dictionary<string, int> _selectedBeatmapEventValues = new Dictionary<string, int>(32);
	}
}
