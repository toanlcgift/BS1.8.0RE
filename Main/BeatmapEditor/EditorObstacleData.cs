using System;

namespace BeatmapEditor
{
	// Token: 0x02000522 RID: 1314
	public class EditorObstacleData
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00012AA1 File Offset: 0x00010CA1
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x00012AA9 File Offset: 0x00010CA9
		public ObstacleType type { get; private set; }

		// Token: 0x0600192C RID: 6444 RVA: 0x00012AB2 File Offset: 0x00010CB2
		public EditorObstacleData(ObstacleType type)
		{
			this.type = type;
		}
	}
}
