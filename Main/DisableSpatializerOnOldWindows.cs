using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class DisableSpatializerOnOldWindows : MonoBehaviour
{
	// Token: 0x0600015C RID: 348 RVA: 0x000031A1 File Offset: 0x000013A1
	protected void Awake()
	{
		if (SystemInfo.operatingSystem.IndexOf("Windows 10", StringComparison.OrdinalIgnoreCase) < 0)
		{
			this._audioSource.spatialize = false;
		}
	}

	// Token: 0x04000137 RID: 311
	[SerializeField]
	private AudioSource _audioSource;
}
