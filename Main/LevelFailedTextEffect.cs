using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class LevelFailedTextEffect : MonoBehaviour
{
	// Token: 0x06000AA4 RID: 2724 RVA: 0x0000851A File Offset: 0x0000671A
	public void ShowEffect()
	{
		base.gameObject.SetActive(true);
		this._animator.enabled = true;
	}

	// Token: 0x04000B0A RID: 2826
	[SerializeField]
	private Animator _animator;
}
