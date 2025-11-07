using System;
using UnityEngine;
using Zenject;

// Token: 0x02000292 RID: 658
public class TunnelSmokeEventEffect : MonoBehaviour
{
	// Token: 0x06000B12 RID: 2834 RVA: 0x00033820 File Offset: 0x00031A20
	protected void Start()
	{
		this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent += this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		this._mainModule = this._particleSystem.main;
		this._shapeModule = this._particleSystem.shape;
		this._particles = new ParticleSystem.Particle[50];
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00008AC4 File Offset: 0x00006CC4
	private void OnDestroy()
	{
		if (this._beatmapObjectCallbackController)
		{
			this._beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= this.HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00033874 File Offset: 0x00031A74
	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == this._event)
		{
			this._mainModule.startSpeedMultiplier = (float)beatmapEventData.value * 5f;
			float num = this._mainModule.startSpeed.constant * this._mainModule.startLifetime.constant * 0.5f;
			this._shapeModule.position = new Vector3(-num, 0f, 0f);
			int particles = this._particleSystem.GetParticles(this._particles);
			for (int i = 0; i < particles; i++)
			{
				this._particles[i].velocity = new Vector3(this._mainModule.startSpeed.constant * 0.5f, 0f, 0f);
			}
			this._particleSystem.SetParticles(this._particles, particles);
		}
	}

	// Token: 0x04000BB1 RID: 2993
	[SerializeField]
	private BeatmapEventType _event;

	// Token: 0x04000BB2 RID: 2994
	[SerializeField]
	private ParticleSystem _particleSystem;

	// Token: 0x04000BB3 RID: 2995
	[Inject]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	// Token: 0x04000BB4 RID: 2996
	private const float kSpeedMultiplier = 5f;

	// Token: 0x04000BB5 RID: 2997
	private const int kMaxParticles = 50;

	// Token: 0x04000BB6 RID: 2998
	private ParticleSystem.MainModule _mainModule;

	// Token: 0x04000BB7 RID: 2999
	private ParticleSystem.ShapeModule _shapeModule;

	// Token: 0x04000BB8 RID: 3000
	private ParticleSystem.Particle[] _particles;
}
