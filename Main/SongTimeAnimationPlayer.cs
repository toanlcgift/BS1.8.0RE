using System;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class SongTimeAnimationPlayer : MonoBehaviour
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x0000B141 File Offset: 0x00009341
	protected void Update()
	{
		this._animationClip.SampleAnimation(base.gameObject, this._songTime);
	}

	// Token: 0x04000EC2 RID: 3778
	[SerializeField]
	private FloatSO _songTime;

	// Token: 0x04000EC3 RID: 3779
	[SerializeField]
	private AnimationClip _animationClip;
}
