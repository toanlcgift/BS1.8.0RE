using System;
using TMPro;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class CrashInfoViewController : MonoBehaviour
{
	// Token: 0x060011EB RID: 4587 RVA: 0x0000DB03 File Offset: 0x0000BD03
	protected void Start()
	{
		this._text.text = this._crashManager.logString + "\n\n" + this._crashManager.stackTrace;
	}

	// Token: 0x040011AE RID: 4526
	[SerializeField]
	private CrashManagerSO _crashManager;

	// Token: 0x040011AF RID: 4527
	[SerializeField]
	private TextMeshProUGUI _text;
}
