using System;
using UnityEngine;
using Zenject;

// Token: 0x02000011 RID: 17
public class NetEaseAppCoreInstaller : MonoInstaller
{
	// Token: 0x06000047 RID: 71 RVA: 0x000023C3 File Offset: 0x000005C3
	public override void InstallBindings()
	{
		//base.Container.Bind<NetEaseManager>().FromComponentInNewPrefab(this._netEaseManagerPrefab).AsSingle();
	}

	// Token: 0x04000041 RID: 65
	//[SerializeField]
	//private NetEaseManager _netEaseManagerPrefab;
}
