using System;
using UnityEngine;
using Zenject;

// Token: 0x02000463 RID: 1123
public class TutorialNoTransitionInstaller : NoTransitionInstaller
{
	// Token: 0x0600152D RID: 5421 RVA: 0x0000FE31 File Offset: 0x0000E031
	public override void InstallBindings(DiContainer container)
	{
		this._scenesTransitionSetupData.Init();
		this._scenesTransitionSetupData.InstallBindings(container);
	}

	// Token: 0x04001528 RID: 5416
	[SerializeField]
	private TutorialScenesTransitionSetupDataSO _scenesTransitionSetupData;
}
