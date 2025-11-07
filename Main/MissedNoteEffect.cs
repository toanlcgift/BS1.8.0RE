using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class MissedNoteEffect : MonoBehaviour
{
	// Token: 0x14000047 RID: 71
	// (add) Token: 0x06000AB8 RID: 2744 RVA: 0x000318FC File Offset: 0x0002FAFC
	// (remove) Token: 0x06000AB9 RID: 2745 RVA: 0x00031934 File Offset: 0x0002FB34
	public event Action<MissedNoteEffect> didFinishEvent;

	// Token: 0x06000ABA RID: 2746 RVA: 0x000023E9 File Offset: 0x000005E9
	protected void Awake()
	{
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0003196C File Offset: 0x0002FB6C
	protected void Update()
	{
		this._animationClip.SampleAnimation(base.gameObject, Mathf.Clamp01((this._songTime.value - this._startAnimationTime) / this._animationDuration) * this._animationClip.length);
		if (this._songTime.value - this._startAnimationTime > this._animationDuration && this.didFinishEvent != null)
		{
			this.didFinishEvent(this);
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00008637 File Offset: 0x00006837
	public void Init(NoteData noteData, float animationDuration, float startAnimationTime)
	{
		this._animationDuration = animationDuration;
		this._startAnimationTime = startAnimationTime;
		this._animationClip.SampleAnimation(base.gameObject, 0f);
	}

	// Token: 0x04000B15 RID: 2837
	[SerializeField]
	private FloatSO _songTime;

	// Token: 0x04000B16 RID: 2838
	[Space]
	[SerializeField]
	private AnimationClip _animationClip;

	// Token: 0x04000B17 RID: 2839
	[SerializeField]
	private SpriteRenderer[] _spriteRenderers;

	// Token: 0x04000B19 RID: 2841
	private float _animationDuration;

	// Token: 0x04000B1A RID: 2842
	private float _startAnimationTime;

	// Token: 0x0200027E RID: 638
	public class Pool : MemoryPoolWithActiveItems<MissedNoteEffect>
	{
	}
}
