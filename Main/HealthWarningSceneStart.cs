using System;
using System.Collections;
using HMUI;
using UnityEngine;
using Zenject;

// Token: 0x0200045B RID: 1115
public class HealthWarningSceneStart : MonoBehaviour
{
	// Token: 0x06001515 RID: 5397 RVA: 0x0000FDBE File Offset: 0x0000DFBE
	public IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		this._hierarchyManager.StartWithFlowCoordinator(this._healthWarninglowCoordinator);
		yield break;
	}

	// Token: 0x04001506 RID: 5382
	[SerializeField]
	private HealthWarningFlowCoordinator _healthWarninglowCoordinator;

	// Token: 0x04001507 RID: 5383
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04001508 RID: 5384
	[Inject]
	private HierarchyManager _hierarchyManager;
}
