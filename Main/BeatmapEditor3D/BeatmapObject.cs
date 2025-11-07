using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x020004D0 RID: 1232
	public class BeatmapObject
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public BeatmapObject()
		{
			this.type = BeatmapObjectType.NoteA;
			this.position = new BeatmapObjectPosition();
			this.direction = NoteCutDirection.None;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00010C16 File Offset: 0x0000EE16
		public BeatmapObject(BeatmapObjectType type, BeatmapObjectPosition position)
		{
			this.type = type;
			this.position = position;
			this.direction = NoteCutDirection.None;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00010C34 File Offset: 0x0000EE34
		public BeatmapObject(BeatmapObjectType type, BeatmapObjectPosition position, NoteCutDirection direction)
		{
			this.type = type;
			this.position = position;
			this.direction = direction;
		}

		// Token: 0x040016D6 RID: 5846
		public BeatmapObjectType type;

		// Token: 0x040016D7 RID: 5847
		public BeatmapObjectPosition position;

		// Token: 0x040016D8 RID: 5848
		public NoteCutDirection direction;

		// Token: 0x040016D9 RID: 5849
		public GameObject gameObject;
	}
}
