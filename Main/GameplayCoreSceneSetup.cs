using System;
using UnityEngine;
using Zenject;

// Token: 0x02000459 RID: 1113
public class GameplayCoreSceneSetup : MonoInstaller
{
	// Token: 0x06001510 RID: 5392 RVA: 0x0004DD8C File Offset: 0x0004BF8C
	public override void InstallBindings()
	{
		IDifficultyBeatmap difficultyBeatmap = this._sceneSetupData.difficultyBeatmap;
		IBeatmapLevel level = difficultyBeatmap.level;
		PracticeSettings practiceSettings = this._sceneSetupData.practiceSettings;
		PlayerSpecificSettings playerSpecificSettings = this._sceneSetupData.playerSpecificSettings;
		GameplayModifiers gameplayModifiers = this._sceneSetupData.gameplayModifiers;
		float songSpeedMul = gameplayModifiers.songSpeedMul;
		float startSongTime = 0f;
		float spawningStartTime = 0f;
		float jumpOffsetY = BeatmapObjectSpawnControllerPlayerHeightSetter.JumpOffsetYForPlayerHeight(playerSpecificSettings.playerHeight);
		bool flag = difficultyBeatmap.beatmapData.spawnRotationEventsCount > 0;
		if (practiceSettings != null)
		{
			spawningStartTime = practiceSettings.startSongTime;
			if (practiceSettings.startInAdvanceAndClearNotes)
			{
				startSongTime = Mathf.Max(0f, practiceSettings.startSongTime - 1f);
			}
			else
			{
				startSongTime = practiceSettings.startSongTime;
			}
			songSpeedMul = practiceSettings.songSpeedMul;
		}
		float num = difficultyBeatmap.noteJumpMovementSpeed;
		if (num <= 0f)
		{
			num = difficultyBeatmap.difficulty.NoteJumpMovementSpeed();
		}
		if (gameplayModifiers.fastNotes)
		{
			num = 20f;
		}
		this._audioMixer.musicPitch = 1f / songSpeedMul;
		float volumeOffset = AudioHelpers.NormalizedVolumeToDB(this._sceneSetupData.playerSpecificSettings.sfxVolume);
		base.Container.Bind<AutomaticSFXVolume.InitData>().FromInstance(new AutomaticSFXVolume.InitData(volumeOffset)).AsSingle();
		BeatmapData beatmapData = BeatDataTransformHelper.CreateTransformedBeatmapData(difficultyBeatmap.beatmapData, gameplayModifiers, practiceSettings, playerSpecificSettings);
		base.Container.Bind<BeatmapData>().FromInstance(beatmapData).AsSingle();
		base.Container.Bind<IDifficultyBeatmap>().FromInstance(difficultyBeatmap).AsSingle();
		base.Container.Bind<BeatmapObjectCallbackController.InitData>().FromInstance(new BeatmapObjectCallbackController.InitData(beatmapData, spawningStartTime)).AsSingle();
		base.Container.Bind<BeatmapObjectSpawnController.InitData>().FromInstance(new BeatmapObjectSpawnController.InitData(level.beatsPerMinute, beatmapData.beatmapLinesData.Length, num, difficultyBeatmap.noteJumpStartBeatOffset, gameplayModifiers.disappearingArrows, gameplayModifiers.ghostNotes, jumpOffsetY)).AsSingle();
		base.Container.Bind<GameplayModifiers>().FromInstance(this._sceneSetupData.gameplayModifiers).AsSingle();
		if (this._sceneSetupData.playerSpecificSettings.automaticPlayerHeight)
		{
			base.Container.Bind<BeatmapObjectSpawnControllerPlayerHeightSetter>().FromComponentInNewPrefab(this._beatmapObjectSpawnControllerPlayerHeightSetterPrefab).AsSingle().NonLazy();
			base.Container.Bind<PlayerHeightDetector>().FromComponentInNewPrefab(this._playerHeightDetectorPrefab).AsSingle().NonLazy();
		}
		SaberType oneSaberType;
		bool oneSaberMode = this.UseOneSaberOnly(difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic, playerSpecificSettings, out oneSaberType);
		base.Container.Bind<PlayerController.InitData>().FromInstance(new PlayerController.InitData(oneSaberMode, oneSaberType)).AsSingle();
		base.Container.Bind<AudioTimeSyncController.InitData>().FromInstance(new AudioTimeSyncController.InitData(level.beatmapLevelData.audioClip, startSongTime, level.songTimeOffset, songSpeedMul)).AsSingle();
		base.Container.Bind<GameEnergyCounter.InitData>().FromInstance(new GameEnergyCounter.InitData(gameplayModifiers.energyType, gameplayModifiers.noFail || gameplayModifiers.demoNoFail, gameplayModifiers.instaFail, gameplayModifiers.failOnSaberClash)).AsSingle();
		base.Container.Bind<MainCameraCullingMask.InitData>().FromInstance(new MainCameraCullingMask.InitData(!playerSpecificSettings.reduceDebris)).AsSingle();
		base.Container.Bind<NoteCutEffectSpawner.InitData>().FromInstance(new NoteCutEffectSpawner.InitData(!playerSpecificSettings.noTextsAndHuds, !playerSpecificSettings.noFailEffects)).AsSingle();
		base.Container.Bind<MissedNoteEffectSpawner.InitData>().FromInstance(new MissedNoteEffectSpawner.InitData(!playerSpecificSettings.noFailEffects)).AsSingle();
		if (!playerSpecificSettings.noTextsAndHuds)
		{
			CoreGameHUDController prefab = flag ? this._flyingHUDPrefab : this._basicHUDPrefab;
			base.Container.Bind<CoreGameHUDController>().FromComponentInNewPrefab(prefab).AsSingle().NonLazy();
			base.Container.Bind<CoreGameHUDController.InitData>().FromInstance(new CoreGameHUDController.InitData(!gameplayModifiers.noFail, playerSpecificSettings.advancedHud)).AsSingle();
		}
		FlyingScoreSpawner.SpawnPosition spawnPosition = flag ? FlyingScoreSpawner.SpawnPosition.AboveGround : FlyingScoreSpawner.SpawnPosition.Underground;
		base.Container.Bind<FlyingScoreSpawner.InitData>().FromInstance(new FlyingScoreSpawner.InitData(spawnPosition));
		base.Container.Bind<NoteCutSoundEffectManager.InitData>().FromInstance(new NoteCutSoundEffectManager.InitData(this._sceneSetupData.useTestNoteCutSoundEffects, playerSpecificSettings.noFailEffects)).AsSingle();
		if (flag)
		{
			base.Container.Bind<BeatLineManager>().FromComponentInNewPrefab(this._beatLineManagerPrefab).AsSingle().NonLazy();
		}
		base.Container.Bind<SpawnRotationChevron.InitData>().FromInstance(new SpawnRotationChevron.InitData(!playerSpecificSettings.staticLights, true));
		base.Container.Bind<BasicSaberModelController.InitData>().FromInstance(new BasicSaberModelController.InitData(new Color(1f, 1f, 1f, this._sceneSetupData.playerSpecificSettings.saberTrailIntensity))).AsSingle();
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x0000FD9A File Offset: 0x0000DF9A
	private bool UseOneSaberOnly(BeatmapCharacteristicSO beatmapCharacteristic, PlayerSpecificSettings playerSpecificSettings, out SaberType saberType)
	{
		saberType = SaberType.SaberA;
		if (beatmapCharacteristic == this._oneColorBeatmapCharacteristic)
		{
			saberType = (playerSpecificSettings.leftHanded ? SaberType.SaberA : SaberType.SaberB);
			return true;
		}
		return false;
	}

	// Token: 0x040014FD RID: 5373
	[SerializeField]
	private BeatmapObjectSpawnControllerPlayerHeightSetter _beatmapObjectSpawnControllerPlayerHeightSetterPrefab;

	// Token: 0x040014FE RID: 5374
	[SerializeField]
	private PlayerHeightDetector _playerHeightDetectorPrefab;

	// Token: 0x040014FF RID: 5375
	[SerializeField]
	private CoreGameHUDController _basicHUDPrefab;

	// Token: 0x04001500 RID: 5376
	[SerializeField]
	private CoreGameHUDController _flyingHUDPrefab;

	// Token: 0x04001501 RID: 5377
	[SerializeField]
	private BeatLineManager _beatLineManagerPrefab;

	// Token: 0x04001502 RID: 5378
	[Space]
	[SerializeField]
	private AudioManagerSO _audioMixer;

	// Token: 0x04001503 RID: 5379
	[Space]
	[SerializeField]
	private BeatmapCharacteristicSO _oneColorBeatmapCharacteristic;

	// Token: 0x04001504 RID: 5380
	[Inject]
	private GameplayCoreSceneSetupData _sceneSetupData;
}
