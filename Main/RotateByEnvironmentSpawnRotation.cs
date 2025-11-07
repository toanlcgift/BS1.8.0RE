using System;
using UnityEngine;
using Zenject;

// Token: 0x0200029A RID: 666
public class RotateByEnvironmentSpawnRotation : MonoBehaviour
{
	// Token: 0x06000B3E RID: 2878 RVA: 0x00008D5E File Offset: 0x00006F5E
	protected void Start()
	{
		this._environmentSpawnRotation.didRotateEvent += this.HandleEnvironmentSpawnRotationDidRotate;
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00008D77 File Offset: 0x00006F77
	protected void OnDestroy()
	{
		this._environmentSpawnRotation.didRotateEvent -= this.HandleEnvironmentSpawnRotationDidRotate;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00008D90 File Offset: 0x00006F90
	private void HandleEnvironmentSpawnRotationDidRotate(Quaternion rotation)
	{
		base.transform.rotation = rotation;
	}

	// Token: 0x04000BE0 RID: 3040
	[Inject]
	private EnvironmentSpawnRotation _environmentSpawnRotation;
}
