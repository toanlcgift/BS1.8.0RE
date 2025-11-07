using System;

namespace BeatmapEditor3D
{
	// Token: 0x0200050A RID: 1290
	[Serializable]
	public class UserInputSensitivityConfig
	{
		// Token: 0x040017FB RID: 6139
		public float fpsKeyboardSensitivity = 5f;

		// Token: 0x040017FC RID: 6140
		public float fpsKeyboardFasterSensitivityMultiplier = 2f;

		// Token: 0x040017FD RID: 6141
		public float fpsKeyboardSlowerSensitivityMultiplier = 0.5f;

		// Token: 0x040017FE RID: 6142
		public float fpsMouseMovementSensitivity = 20f;

		// Token: 0x040017FF RID: 6143
		public float mouseScrollSensitivity = 0.1f;
	}
}
