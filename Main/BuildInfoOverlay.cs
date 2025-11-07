using System;
using TMPro;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class BuildInfoOverlay : MonoBehaviour
{
	// Token: 0x06000B74 RID: 2932 RVA: 0x0000906B File Offset: 0x0000726B
	protected void Start()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000C20 RID: 3104
	[SerializeField]
	private TextMeshProUGUI _text;

	// Token: 0x04000C21 RID: 3105
	[SerializeField]
	private GameObject _canvas;
}
