using System;
using System.Collections;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x0200045D RID: 1117
public class MenuSceneSetup : MonoBehaviour
{
	// Token: 0x0600151D RID: 5405 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
	protected IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		yield return null;
		this._hierarchyManager.StartWithFlowCoordinator(this._rootFlowCoordinator);
		yield break;
	}

	// Token: 0x0400150C RID: 5388
	[SerializeField]
	private FlowCoordinator _rootFlowCoordinator;

	// Token: 0x0400150D RID: 5389
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x0400150E RID: 5390
	[Inject]
	private HierarchyManager _hierarchyManager;
}
