using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class DeactivateAfterFirstFrame : MonoBehaviour
{
	// Token: 0x06000E3B RID: 3643 RVA: 0x0000B00E File Offset: 0x0000920E
	protected IEnumerator Start()
	{
		yield return null;
		base.gameObject.SetActive(false);
		yield break;
	}
}
