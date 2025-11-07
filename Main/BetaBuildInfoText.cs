using System;
using TMPro;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class BetaBuildInfoText : MonoBehaviour
{
	// Token: 0x06001403 RID: 5123 RVA: 0x0000906B File Offset: 0x0000726B
	protected void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040013BE RID: 5054
	[SerializeField]
	private TextMeshProUGUI _text;
}
