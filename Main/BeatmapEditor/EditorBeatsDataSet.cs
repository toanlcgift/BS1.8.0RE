using System;
using System.Collections.Generic;

namespace BeatmapEditor
{
	// Token: 0x02000524 RID: 1316
	public class EditorBeatsDataSet
	{
		// Token: 0x170004A5 RID: 1189
		public EditorBeatsData this[BeatmapCharacteristicDifficulty beatmapCharacteristicDifficulty]
		{
			get
			{
				if (this._editorBeatsData == null)
				{
					return null;
				}
				EditorBeatsData result = null;
				this._editorBeatsData.TryGetValue(beatmapCharacteristicDifficulty, out result);
				return result;
			}
			set
			{
				if (this._editorBeatsData == null)
				{
					this._editorBeatsData = new Dictionary<BeatmapCharacteristicDifficulty, EditorBeatsData>();
				}
				this._editorBeatsData[beatmapCharacteristicDifficulty] = value;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00012B78 File Offset: 0x00010D78
		public void Clear()
		{
			if (this._editorBeatsData != null)
			{
				this._editorBeatsData.Clear();
			}
		}

		// Token: 0x04001878 RID: 6264
		public Dictionary<BeatmapCharacteristicDifficulty, EditorBeatsData> _editorBeatsData;
	}
}
