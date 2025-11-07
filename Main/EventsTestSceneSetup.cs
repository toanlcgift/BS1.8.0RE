using System;
using Polyglot;
using Zenject;

// Token: 0x02000457 RID: 1111
public class EventsTestSceneSetup : MonoInstaller
{
	// Token: 0x0600150C RID: 5388 RVA: 0x0004DBAC File Offset: 0x0004BDAC
	public override void InstallBindings()
	{
		base.Container.Bind<PauseMenuManager.InitData>().FromInstance(new PauseMenuManager.InitData(Localization.Get("BUTTON_MENU"), Localization.Get("TITLE_TUTORIAL"), "", "")).AsSingle();
		base.Container.Bind<BeatmapObjectCallbackController.InitData>().FromInstance(new BeatmapObjectCallbackController.InitData(null, 0f)).AsSingle();
		base.Container.Bind<AudioTimeSyncController.InitData>().FromInstance(new AudioTimeSyncController.InitData(null, 0f, 0f, 1f)).AsSingle();
	}
}
