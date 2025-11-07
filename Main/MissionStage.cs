using System;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class MissionStage : MonoBehaviour
{
	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000640 RID: 1600 RVA: 0x00005A6E File Offset: 0x00003C6E
	public int minimumMissionsToUnlock
	{
		get
		{
			return this._minimumMissionsToUnlock;
		}
	}

	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000641 RID: 1601 RVA: 0x00005A76 File Offset: 0x00003C76
	public Vector2 position
	{
		get
		{
			return this._rectTransform.localPosition;
		}
	}

	// Token: 0x040006B2 RID: 1714
	[SerializeField]
	private int _minimumMissionsToUnlock;

	// Token: 0x040006B3 RID: 1715
	[SerializeField]
	private RectTransform _rectTransform;
}
