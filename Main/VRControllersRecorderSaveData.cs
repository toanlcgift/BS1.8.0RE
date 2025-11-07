using System;
using UnityEngine.XR;

// Token: 0x0200044B RID: 1099
[Serializable]
public class VRControllersRecorderSaveData
{
	// Token: 0x040014CA RID: 5322
	public VRControllersRecorderSaveData.NodeInfo[] nodesInfo;

	// Token: 0x040014CB RID: 5323
	public VRControllersRecorderSaveData.Keyframe[] keyframes;

	// Token: 0x0200044C RID: 1100
	[Serializable]
	public class PositionAndRotation
	{
		// Token: 0x040014CC RID: 5324
		public float posX;

		// Token: 0x040014CD RID: 5325
		public float posY;

		// Token: 0x040014CE RID: 5326
		public float posZ;

		// Token: 0x040014CF RID: 5327
		public float rotX;

		// Token: 0x040014D0 RID: 5328
		public float rotY;

		// Token: 0x040014D1 RID: 5329
		public float rotZ;

		// Token: 0x040014D2 RID: 5330
		public float rotW;
	}

	// Token: 0x0200044D RID: 1101
	[Serializable]
	public class Keyframe
	{
		// Token: 0x040014D3 RID: 5331
		public VRControllersRecorderSaveData.PositionAndRotation[] positionsAndRotations;

		// Token: 0x040014D4 RID: 5332
		public float time;
	}

	// Token: 0x0200044E RID: 1102
	[Serializable]
	public class NodeInfo
	{
		// Token: 0x040014D5 RID: 5333
		public XRNode nodeType;

		// Token: 0x040014D6 RID: 5334
		public int nodeIdx;
	}
}
