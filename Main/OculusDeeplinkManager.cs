using System;
using System.Diagnostics;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class OculusDeeplinkManager : IDeeplinkManager
{
	// Token: 0x14000009 RID: 9
	// (add) Token: 0x060002B0 RID: 688 RVA: 0x0001E274 File Offset: 0x0001C474
	// (remove) Token: 0x060002B1 RID: 689 RVA: 0x0001E2AC File Offset: 0x0001C4AC
	public event Action<Deeplink> didReceiveDeeplinkEvent;

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003C7A File Offset: 0x00001E7A
	public Deeplink currentDeeplink
	{
		get
		{
			return this._currentDeeplink;
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
	public void OculusPlatformWasInitialized()
	{
		LaunchDetails launchDetails = ApplicationLifecycle.GetLaunchDetails();
		this.ProcessLaunchDetails(launchDetails);
		ApplicationLifecycle.SetLaunchIntentChangedNotificationCallback(new Message<string>.Callback(this.SetLaunchIntentChangedNotificationCallback));
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0001E310 File Offset: 0x0001C510
	private void SetLaunchIntentChangedNotificationCallback(Message<string> message)
	{
		LaunchDetails launchDetails = ApplicationLifecycle.GetLaunchDetails();
		this.ProcessLaunchDetails(launchDetails);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0001E32C File Offset: 0x0001C52C
	private void ProcessLaunchDetails(LaunchDetails launchDetails)
	{
		string deeplinkMessage = null;
		if (launchDetails != null && launchDetails.LaunchType == LaunchType.Deeplink)
		{
			deeplinkMessage = launchDetails.DeeplinkMessage;
		}
		this.UpdateDeeplinkMessage(deeplinkMessage);
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0001E358 File Offset: 0x0001C558
	private void UpdateDeeplinkMessage(string deeplinkMessage)
	{
		if (!string.IsNullOrEmpty(deeplinkMessage))
		{
			try
			{
				Deeplink deeplink = JsonUtility.FromJson<Deeplink>(deeplinkMessage);
				if (deeplink != null && this.IsAtLeastOneFieldPopulated(deeplink))
				{
					this._currentDeeplink = deeplink;
					Action<Deeplink> action = this.didReceiveDeeplinkEvent;
					if (action != null)
					{
						action(this._currentDeeplink);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0001E3BC File Offset: 0x0001C5BC
	private bool IsAtLeastOneFieldPopulated(Deeplink deeplink)
	{
		return !string.IsNullOrEmpty(deeplink.Destination) || !string.IsNullOrEmpty(deeplink.LevelID) || !string.IsNullOrEmpty(deeplink.PackID) || !string.IsNullOrEmpty(deeplink.Difficulty) || !string.IsNullOrEmpty(deeplink.Characteristic);
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00002654 File Offset: 0x00000854
	[Conditional("OculusDeeplinkManagerLogging")]
	public static void Log(string message)
	{
		UnityEngine.Debug.Log(message);
	}

	// Token: 0x0400034A RID: 842
	private Deeplink _currentDeeplink;
}
