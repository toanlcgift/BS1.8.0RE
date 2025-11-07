using System;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using Zenject;

// Token: 0x02000039 RID: 57
public class OculusInit : MonoBehaviour
{
	// Token: 0x060000EA RID: 234 RVA: 0x000023E9 File Offset: 0x000005E9
	public void Init()
	{
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00016C2C File Offset: 0x00014E2C
	private void InitCallback(Message<PlatformInitialize> msg)
	{
		if (msg.IsError)
		{
			Debug.Log("Oculus PlatformInitialize Error: " + msg.GetError().Message);
			UnityEngine.Application.Quit();
			return;
		}
		OculusDeeplinkManager oculusDeeplinkManager = this._oculusDeeplinkManager;
		if (oculusDeeplinkManager != null)
		{
			oculusDeeplinkManager.OculusPlatformWasInitialized();
		}
		Entitlements.IsUserEntitledToApplication().OnComplete(delegate(Message message)
		{
			if (message.IsError)
			{
				Error error = message.GetError();
				Debug.LogWarning("Oculus user entitlement error: " + error.Message);
				Debug.LogError("Oculus user entitlement check failed. You are NOT entitled to use this app.");
				UnityEngine.Application.Quit();
			}
		});
	}

	// Token: 0x040000C2 RID: 194
	[InjectOptional]
	private OculusDeeplinkManager _oculusDeeplinkManager;
}
