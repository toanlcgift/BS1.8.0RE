using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

// Token: 0x02000291 RID: 657
public class SaberClashEffect : MonoBehaviour
{
	// Token: 0x06000B0E RID: 2830 RVA: 0x000336E0 File Offset: 0x000318E0
	protected void Start()
	{
		this._sparkleParticleSystemEmmisionModule = this._sparkleParticleSystem.emission;
		this._sparkleParticleSystemEmmisionModule.enabled = false;
		this._glowParticleSystemEmmisionModule = this._glowParticleSystem.emission;
		this._glowParticleSystemEmmisionModule.enabled = false;
		Color color = Color.Lerp(this._colorManager.EffectsColorForSaberType(SaberType.SaberA), this._colorManager.EffectsColorForSaberType(SaberType.SaberB), 0.5f);
		this._sparkleParticleSystem.startColor = color;
		this._glowParticleSystem.startColor = color;
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00008AB3 File Offset: 0x00006CB3
	protected void OnDisable()
	{
		if (this._sabersAreClashing)
		{
			this._sabersAreClashing = false;
		}
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0003377C File Offset: 0x0003197C
	protected void LateUpdate()
	{
		if (this._saberClashChecker.sabersAreClashing)
		{
			base.transform.position = this._saberClashChecker.clashingPoint;
			this._hapticFeedbackController.ContinuousRumble(XRNode.LeftHand);
			this._hapticFeedbackController.ContinuousRumble(XRNode.RightHand);
			if (!this._sabersAreClashing)
			{
				this._sabersAreClashing = true;
				this._sparkleParticleSystemEmmisionModule.enabled = true;
				this._glowParticleSystemEmmisionModule.enabled = true;
				return;
			}
		}
		else if (this._sabersAreClashing)
		{
			this._sabersAreClashing = false;
			this._sparkleParticleSystemEmmisionModule.enabled = false;
			this._glowParticleSystemEmmisionModule.enabled = false;
			this._glowParticleSystem.Clear();
		}
	}

	// Token: 0x04000BA9 RID: 2985
	[SerializeField]
	private ParticleSystem _sparkleParticleSystem;

	// Token: 0x04000BAA RID: 2986
	[SerializeField]
	private ParticleSystem _glowParticleSystem;

	// Token: 0x04000BAB RID: 2987
	[Inject]
	private SaberClashChecker _saberClashChecker;

	// Token: 0x04000BAC RID: 2988
	[Inject]
	private HapticFeedbackController _hapticFeedbackController;

	// Token: 0x04000BAD RID: 2989
	[Inject]
	private ColorManager _colorManager;

	// Token: 0x04000BAE RID: 2990
	private ParticleSystem.EmissionModule _sparkleParticleSystemEmmisionModule;

	// Token: 0x04000BAF RID: 2991
	private ParticleSystem.EmissionModule _glowParticleSystemEmmisionModule;

	// Token: 0x04000BB0 RID: 2992
	private bool _sabersAreClashing;
}
