using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class DisableOnNonQuest : MonoBehaviour
{
	// Token: 0x06000E45 RID: 3653 RVA: 0x0000906B File Offset: 0x0000726B
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}
}
