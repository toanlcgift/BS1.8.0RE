using System;
using UnityEngine;

// Token: 0x0200047B RID: 1147
public class TutorialScenesTransitionSetupDataSO : ScenesTransitionSetupDataSO
{
	// Token: 0x140000CC RID: 204
	// (add) Token: 0x0600157D RID: 5501 RVA: 0x0004EBE0 File Offset: 0x0004CDE0
	// (remove) Token: 0x0600157E RID: 5502 RVA: 0x0004EC18 File Offset: 0x0004CE18
	public event Action<TutorialScenesTransitionSetupDataSO, TutorialScenesTransitionSetupDataSO.TutorialEndStateType> didFinishEvent;

	// Token: 0x0600157F RID: 5503 RVA: 0x0004EC50 File Offset: 0x0004CE50
	public void Init()
	{
		ColorScheme colorScheme = new ColorScheme(this._environmentInfo.colorScheme);
		SceneInfo[] scenes = new SceneInfo[]
		{
			this._environmentInfo.sceneInfo,
			this._tutorialSceneInfo,
			this._gameCoreSceneInfo
		};
		SceneSetupData[] sceneSetupData = new SceneSetupData[]
		{
			new GameCoreSceneSetupData(colorScheme)
		};
		base.Init(scenes, sceneSetupData);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000101A5 File Offset: 0x0000E3A5
	public void Finish(TutorialScenesTransitionSetupDataSO.TutorialEndStateType endState)
	{
		Action<TutorialScenesTransitionSetupDataSO, TutorialScenesTransitionSetupDataSO.TutorialEndStateType> action = this.didFinishEvent;
		if (action == null)
		{
			return;
		}
		action(this, endState);
	}

	// Token: 0x04001566 RID: 5478
	[SerializeField]
	private EnvironmentInfoSO _environmentInfo;

	// Token: 0x04001567 RID: 5479
	[SerializeField]
	private SceneInfo _tutorialSceneInfo;

	// Token: 0x04001568 RID: 5480
	[SerializeField]
	private SceneInfo _gameCoreSceneInfo;

	// Token: 0x0200047C RID: 1148
	public enum TutorialEndStateType
	{
		// Token: 0x0400156B RID: 5483
		Completed,
		// Token: 0x0400156C RID: 5484
		ReturnToMenu,
		// Token: 0x0400156D RID: 5485
		Restart
	}
}
