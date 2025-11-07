using System;
using Polyglot;
using UnityEngine;
using Zenject;

// Token: 0x02000481 RID: 1153
public class TutorialSceneSetup : MonoInstaller
{
	// Token: 0x06001594 RID: 5524 RVA: 0x0004EF10 File Offset: 0x0004D110
	public override void InstallBindings()
	{
		base.Container.Bind<PauseMenuManager.InitData>().FromInstance(new PauseMenuManager.InitData(Localization.Get("BUTTON_MENU"), Localization.Get("TITLE_TUTORIAL"), "", "")).AsSingle();
		base.Container.Bind<BeatmapObjectCallbackController.InitData>().FromInstance(new BeatmapObjectCallbackController.InitData(null, 0f)).AsSingle();
		base.Container.Bind<AudioTimeSyncController.InitData>().FromInstance(new AudioTimeSyncController.InitData(this._audioClip, 0f, 0f, 1f)).AsSingle();
		base.Container.Bind<TutorialSongController.InitData>().FromInstance(new TutorialSongController.InitData(this._songBPM)).AsSingle();
		base.Container.Bind<BeatmapObjectSpawnController.InitData>().FromInstance(new BeatmapObjectSpawnController.InitData(this._songBPM, 4, 10f, 2f, false, false, 0f)).AsSingle();
		base.Container.Bind<AutomaticSFXVolume.InitData>().FromInstance(new AutomaticSFXVolume.InitData(0f)).AsSingle();
		base.Container.Bind<MissedNoteEffectSpawner.InitData>().FromInstance(new MissedNoteEffectSpawner.InitData(true)).AsSingle();
		base.Container.Bind<BeatmapObjectSpawnControllerPlayerHeightSetter>().FromComponentInNewPrefab(this._beatmapObjectSpawnControllerPlayerHeightSetterPrefab).AsSingle().NonLazy();
		base.Container.Bind<PlayerHeightDetector>().FromComponentInNewPrefab(this._playerHeightDetectorPrefab).AsSingle().NonLazy();
	}

	// Token: 0x0400157F RID: 5503
	[SerializeField]
	private AudioClip _audioClip;

	// Token: 0x04001580 RID: 5504
	[SerializeField]
	private float _songBPM = 1f;

	// Token: 0x04001581 RID: 5505
	[Space]
	[SerializeField]
	private BeatmapObjectSpawnControllerPlayerHeightSetter _beatmapObjectSpawnControllerPlayerHeightSetterPrefab;

	// Token: 0x04001582 RID: 5506
	[SerializeField]
	private PlayerHeightDetector _playerHeightDetectorPrefab;
}
