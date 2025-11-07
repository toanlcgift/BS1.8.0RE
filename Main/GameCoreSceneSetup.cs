using System;
using UnityEngine;
using Zenject;

// Token: 0x02000458 RID: 1112
public class GameCoreSceneSetup : MonoInstaller
{
	// Token: 0x0600150E RID: 5390 RVA: 0x0004DC40 File Offset: 0x0004BE40
	public override void InstallBindings()
	{
		base.Container.Bind<ColorScheme>().FromInstance(this._sceneSetupData.colorScheme).AsSingle();
		if (this._mainSettingsModel.pauseButtonPressDurationLevel == 0)
		{
			base.Container.Bind(new Type[]
			{
				typeof(ITickable),
				typeof(IPauseTrigger)
			}).To<InstantPauseTrigger>().AsSingle();
		}
		else
		{
			base.Container.Bind<float>().FromInstance((float)this._mainSettingsModel.pauseButtonPressDurationLevel * 0.75f).WhenInjectedInto<DelayedPauseTrigger>();
			base.Container.Bind(new Type[]
			{
				typeof(ITickable),
				typeof(IPauseTrigger)
			}).To<DelayedPauseTrigger>().AsSingle();
		}
		base.Container.Bind<ISaberModelController>().FromComponentInNewPrefab(this._basicSaberModelControllerPrefab).AsTransient();
		if (this._mainSettingsModel.createScreenshotDuringTheGame)
		{
			base.Container.Bind<ScreenCaptureAfterDelay.InitData>().FromInstance(new ScreenCaptureAfterDelay.InitData(ScreenCaptureCache.ScreenshotType.Game, 5f, 1920, 1080));
			base.Container.Bind<ScreenCaptureAfterDelay>().FromComponentInNewPrefab(this._screenCaptureAfterDelayPrefab).AsSingle().NonLazy();
		}
	}

	// Token: 0x040014F8 RID: 5368
	[SerializeField]
	private BasicSaberModelController _basicSaberModelControllerPrefab;

	// Token: 0x040014F9 RID: 5369
	[SerializeField]
	private ScreenCaptureAfterDelay _screenCaptureAfterDelayPrefab;

	// Token: 0x040014FA RID: 5370
	[SerializeField]
	private MainSettingsModelSO _mainSettingsModel;

	// Token: 0x040014FB RID: 5371
	[Inject]
	private GameCoreSceneSetupData _sceneSetupData;

	// Token: 0x040014FC RID: 5372
	private const float kPauseButtonPressDurationMultiplier = 0.75f;
}
