using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public abstract class CuttableBySaber : MonoBehaviour
{
	// Token: 0x14000067 RID: 103
	// (add) Token: 0x06000D1D RID: 3357 RVA: 0x00037ED0 File Offset: 0x000360D0
	// (remove) Token: 0x06000D1E RID: 3358 RVA: 0x00037F08 File Offset: 0x00036108
	public event CuttableBySaber.WasCutBySaberDelegate wasCutBySaberEvent;

	// Token: 0x06000D1F RID: 3359 RVA: 0x0000A327 File Offset: 0x00008527
	protected void CallWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		CuttableBySaber.WasCutBySaberDelegate wasCutBySaberDelegate = this.wasCutBySaberEvent;
		if (wasCutBySaberDelegate == null)
		{
			return;
		}
		wasCutBySaberDelegate(saber, cutPoint, orientation, cutDirVec);
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06000D20 RID: 3360
	// (set) Token: 0x06000D21 RID: 3361
	public abstract bool canBeCut { get; set; }

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06000D22 RID: 3362
	public abstract float radius { get; }

	// Token: 0x06000D23 RID: 3363
	public abstract void Cut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec);

	// Token: 0x020002FD RID: 765
	// (Invoke) Token: 0x06000D26 RID: 3366
	public delegate void WasCutBySaberDelegate(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec);
}
