using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class PauseAnimationController : MonoBehaviour
{
	// Token: 0x1400005B RID: 91
	// (add) Token: 0x06000CB2 RID: 3250 RVA: 0x00036F8C File Offset: 0x0003518C
	// (remove) Token: 0x06000CB3 RID: 3251 RVA: 0x00036FC4 File Offset: 0x000351C4
	public event Action resumeFromPauseAnimationDidFinishEvent;

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00009DE4 File Offset: 0x00007FE4
	protected void Awake()
	{
		base.enabled = false;
		this._animator.enabled = false;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00009DF9 File Offset: 0x00007FF9
	public void StartEnterPauseAnimation()
	{
		base.enabled = true;
		this._animator.enabled = true;
		this._animator.SetTrigger("EnterPause");
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00009E1E File Offset: 0x0000801E
	public void StartResumeFromPauseAnimation()
	{
		base.enabled = true;
		this._animator.enabled = true;
		this._animator.SetTrigger("ResumeFromPause");
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00009DE4 File Offset: 0x00007FE4
	public void EnterPauseAnimationDidFinish()
	{
		base.enabled = false;
		this._animator.enabled = false;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00009E43 File Offset: 0x00008043
	public void ResumeFromPauseAnimationDidFinish()
	{
		base.enabled = false;
		this._animator.enabled = false;
		Action action = this.resumeFromPauseAnimationDidFinishEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000D2B RID: 3371
	[SerializeField]
	private Animator _animator;
}
