using System;
using Zenject;

// Token: 0x0200045F RID: 1119
public class MissionGameplaySceneSetup : MonoInstaller
{
	// Token: 0x06001525 RID: 5413 RVA: 0x0004E36C File Offset: 0x0004C56C
	public override void InstallBindings()
	{
		base.Container.Bind<MissionObjectiveCheckersManager.InitData>().FromInstance(new MissionObjectiveCheckersManager.InitData(this._sceneSetupData.missionObjectives)).AsSingle();
		base.Container.Bind<ScoreUIController.InitData>().FromInstance(new ScoreUIController.InitData(ScoreUIController.ScoreDisplayType.RawScore)).AsSingle();
		base.Container.Bind<PauseMenuManager.InitData>().FromInstance(new PauseMenuManager.InitData(this._sceneSetupData.backButtonText, this._sceneSetupData.songName, this._sceneSetupData.songSubName, this._sceneSetupData.difficultyName)).AsSingle();
		base.Container.Bind<MissionLevelFailedController.InitData>().FromInstance(new MissionLevelFailedController.InitData(this._sceneSetupData.autoRestart)).AsSingle();
	}

	// Token: 0x04001512 RID: 5394
	[Inject]
	private MissionGameplaySceneSetupData _sceneSetupData;
}
