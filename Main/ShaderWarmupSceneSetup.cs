using System;
using System.Collections;
using UnityEngine;
using Zenject;

// Token: 0x0200047D RID: 1149
public class ShaderWarmupSceneSetup : MonoBehaviour
{
	// Token: 0x06001582 RID: 5506 RVA: 0x000101B9 File Offset: 0x0000E3B9
	protected IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		this._gameScenesManager.ReplaceScenes(this._sceneSetupData.nextScenesTransitionSetupData, 0f, null, null);
		yield break;
	}

	// Token: 0x0400156E RID: 5486
	[Inject]
	private ShaderWarmupSceneSetupData _sceneSetupData;

	// Token: 0x0400156F RID: 5487
	[Inject]
	private GameScenesManager _gameScenesManager;
}
