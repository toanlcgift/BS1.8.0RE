using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x0200027F RID: 639
public class NoteCutHapticEffect : MonoBehaviour
{
	// Token: 0x06000ABF RID: 2751 RVA: 0x000319E4 File Offset: 0x0002FBE4
	public void HitNote(SaberType saberType)
	{
		XRNode node = (saberType == SaberType.SaberA) ? XRNode.LeftHand : XRNode.RightHand;
		this._hapticFeedbackController.HitNote(node);
	}

	// Token: 0x04000B1B RID: 2843
	[Inject]
	private HapticFeedbackController _hapticFeedbackController;
}
