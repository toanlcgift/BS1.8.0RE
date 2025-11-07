using System;
using UnityEngine;
using Zenject;

// Token: 0x02000460 RID: 1120
public class HealthWarningNoTransitionInstaller : NoTransitionInstaller
{
	// Token: 0x06001527 RID: 5415 RVA: 0x0000FE0A File Offset: 0x0000E00A
	public override void InstallBindings(DiContainer container)
	{
		this._scenesTransitionSetupData.Init(this._healthWarningSceneSetupData);
		this._scenesTransitionSetupData.InstallBindings(container);
	}

	// Token: 0x04001513 RID: 5395
	[SerializeField]
	private HealthWarningSceneSetupData _healthWarningSceneSetupData;

	// Token: 0x04001514 RID: 5396
	[SerializeField]
	private HealthWarningScenesTransitionSetupDataSO _scenesTransitionSetupData;
}
