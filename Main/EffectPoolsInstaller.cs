using System;
using UnityEngine;
using Zenject;

// Token: 0x0200033E RID: 830
public class EffectPoolsInstaller : MonoInstaller
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x0003B428 File Offset: 0x00039628
	public override void InstallBindings()
	{
		base.Container.BindMemoryPool<FlyingTextEffect, FlyingTextEffect.Pool>().WithInitialSize(20).FromComponentInNewPrefab(this._flyingTextEffectPrefab);
		base.Container.BindMemoryPool<FlyingScoreEffect, FlyingScoreEffect.Pool>().WithInitialSize(20).FromComponentInNewPrefab(this._flyingScoreEffectPrefab);
		base.Container.BindMemoryPool<FlyingSpriteEffect, FlyingSpriteEffect.Pool>().WithInitialSize(20).FromComponentInNewPrefab(this._flyingSpriteEffectPrefab);
		base.Container.BindMemoryPool<NoteDebris, NoteDebris.Pool>().WithInitialSize(40).FromComponentInNewPrefab(this._noteDebrisHDConditionVariable ? this._noteDebrisHDPrefab : this._noteDebrisLWPrefab);
		base.Container.BindMemoryPool<BeatEffect, BeatEffect.Pool>().WithInitialSize(20).FromComponentInNewPrefab(this._beatEffectPrefab);
		base.Container.BindMemoryPool<NoteCutSoundEffect, NoteCutSoundEffect.Pool>().WithInitialSize(16).FromComponentInNewPrefab(this._noteCutSoundEffectPrefab);
		base.Container.BindMemoryPool<BombCutSoundEffect, BombCutSoundEffect.Pool>().WithInitialSize(20).FromComponentInNewPrefab(this._bombCutSoundEffectPrefab);
	}

	// Token: 0x04000EDC RID: 3804
	[SerializeField]
	private FlyingTextEffect _flyingTextEffectPrefab;

	// Token: 0x04000EDD RID: 3805
	[SerializeField]
	private FlyingScoreEffect _flyingScoreEffectPrefab;

	// Token: 0x04000EDE RID: 3806
	[SerializeField]
	private BeatEffect _beatEffectPrefab;

	// Token: 0x04000EDF RID: 3807
	[SerializeField]
	private NoteCutSoundEffect _noteCutSoundEffectPrefab;

	// Token: 0x04000EE0 RID: 3808
	[SerializeField]
	private BombCutSoundEffect _bombCutSoundEffectPrefab;

	// Token: 0x04000EE1 RID: 3809
	[SerializeField]
	private FlyingSpriteEffect _flyingSpriteEffectPrefab;

	// Token: 0x04000EE2 RID: 3810
	[SerializeField]
	private BoolSO _noteDebrisHDConditionVariable;

	// Token: 0x04000EE3 RID: 3811
	[SerializeField]
	private NoteDebris _noteDebrisHDPrefab;

	// Token: 0x04000EE4 RID: 3812
	[SerializeField]
	private NoteDebris _noteDebrisLWPrefab;
}
