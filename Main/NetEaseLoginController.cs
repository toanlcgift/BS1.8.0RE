using System;
using System.Collections;
using System.Runtime.CompilerServices;
using NetEase.Docker;
using UnityEngine;
using Zenject;

// Token: 0x02000017 RID: 23
public class NetEaseLoginController : MonoBehaviour
{
	// Token: 0x06000064 RID: 100 RVA: 0x0000259B File Offset: 0x0000079B
	protected IEnumerator Start()
	{
		yield return this._gameScenesManager.waitUntilSceneTransitionFinish;
		this.LoginAsync();
		yield break;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0001565C File Offset: 0x0001385C
	private async void LoginAsync()
	{
		TaskAwaiter<InitializeData> taskAwaiter;
		do
		{
			taskAwaiter = this._netEaseManager.InitAsync().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				taskAwaiter.GetResult();
				TaskAwaiter<InitializeData> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<InitializeData>);
			}
		}
		while (!taskAwaiter.GetResult().InitializeSuccessful);
		TaskAwaiter<LoginData> taskAwaiter3;
		do
		{
			taskAwaiter3 = this._netEaseManager.LoginAsync().GetAwaiter();
			if (!taskAwaiter3.IsCompleted)
			{
				taskAwaiter3.GetResult();
				TaskAwaiter<LoginData> taskAwaiter4;
				taskAwaiter3 = taskAwaiter4;
				taskAwaiter4 = default(TaskAwaiter<LoginData>);
			}
		}
		while (!taskAwaiter3.GetResult().LoginSuccessful);
		this._arcadeMenuTransitionSetupData.Init();
		this._gameScenesManager.ReplaceScenes(this._arcadeMenuTransitionSetupData, 0f, null, null);
	}

	// Token: 0x04000055 RID: 85
	[SerializeField]
	private MenuScenesTransitionSetupDataSO _arcadeMenuTransitionSetupData;

	// Token: 0x04000056 RID: 86
	[Inject]
	private GameScenesManager _gameScenesManager;

	// Token: 0x04000057 RID: 87
	[Inject]
	private NetEaseManager _netEaseManager;
}
