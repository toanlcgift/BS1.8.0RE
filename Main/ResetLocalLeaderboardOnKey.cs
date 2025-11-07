using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class ResetLocalLeaderboardOnKey : MonoBehaviour
{
	// Token: 0x0600084B RID: 2123 RVA: 0x00006CF7 File Offset: 0x00004EF7
	private void Update()
	{
		if (Input.GetKeyDown(this._keyCode))
		{
			this._localLeaderboardsModel.ClearAllLeaderboards(true);
		}
	}

	// Token: 0x040008D1 RID: 2257
	[SerializeField]
	private LocalLeaderboardsModel _localLeaderboardsModel;

	// Token: 0x040008D2 RID: 2258
	[SerializeField]
	private KeyCode _keyCode = KeyCode.F9;
}
