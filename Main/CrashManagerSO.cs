using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000432 RID: 1074
public class CrashManagerSO : PersistentScriptableObject
{
	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x0600148A RID: 5258 RVA: 0x0000F78E File Offset: 0x0000D98E
	public string logString
	{
		get
		{
			return this._logString;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x0600148B RID: 5259 RVA: 0x0000F796 File Offset: 0x0000D996
	public string stackTrace
	{
		get
		{
			return this._stackTrace;
		}
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0000F79E File Offset: 0x0000D99E
	public void StartCatchingExceptions()
	{
		Application.logMessageReceived += this.HandleLog;
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x0000F7B1 File Offset: 0x0000D9B1
	protected void OnDisable()
	{
		Application.logMessageReceived -= this.HandleLog;
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		this._logString = logString;
		this._stackTrace = stackTrace;
		if (Application.CanStreamedLevelBeLoaded("CrashInfo"))
		{
			SceneManager.LoadScene("CrashInfo");
		}
	}

	// Token: 0x04001439 RID: 5177
	private string _logString;

	// Token: 0x0400143A RID: 5178
	private string _stackTrace;
}
