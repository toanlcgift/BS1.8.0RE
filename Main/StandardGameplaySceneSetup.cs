using System;
using Zenject;

// Token: 0x02000480 RID: 1152
public class StandardGameplaySceneSetup : MonoInstaller
{
	// Token: 0x06001592 RID: 5522 RVA: 0x0004EE94 File Offset: 0x0004D094
	public override void InstallBindings()
	{
		base.Container.Bind<PauseMenuManager.InitData>().FromInstance(new PauseMenuManager.InitData(this._sceneSetupData.backButtonText, this._sceneSetupData.songName, this._sceneSetupData.songSubName, this._sceneSetupData.difficultyName)).AsSingle();
		base.Container.Bind<StandardLevelFailedController.InitData>().FromInstance(new StandardLevelFailedController.InitData(this._sceneSetupData.autoRestart)).AsSingle();
	}

	// Token: 0x0400157E RID: 5502
	[Inject]
	private StandardGameplaySceneSetupData _sceneSetupData;
}
