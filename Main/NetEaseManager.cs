using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NetEase;
using NetEase.Docker;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class NetEaseManager : MonoBehaviour
{
	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600006F RID: 111 RVA: 0x000025CF File Offset: 0x000007CF
	public bool supportsLeaderboards
	{
		get
		{
			return DockerWrap.WillProvideHighscore();
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000070 RID: 112 RVA: 0x000025D6 File Offset: 0x000007D6
	public string userName
	{
		get
		{
			return this._userName;
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00015858 File Offset: 0x00013A58
	protected void Awake()
	{
		DockerWrap.SetInitializeCallback(new Action<InitializeData>(this.HandleDidInitialize));
		DockerWrap.SetLoginCallback(new Action<LoginData>(this.HandleDidLogin));
		DockerWrap.SetLogoutCallback(new Action<LogoutData>(this.HandleDidLogout));
		DockerWrap.SetHighscoreReceivedCallback(new Action<ReceivedHighscoreData>(this.HandleDidReceiveHighscore));
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000025DE File Offset: 0x000007DE
	protected void Update()
	{
		DockerWrap.UpdateLoop();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000158AC File Offset: 0x00013AAC
	public async Task<InitializeData> InitAsync()
	{
		if (this._initTaskCompletionSource != null)
		{
			this._initTaskCompletionSource.TrySetCanceled();
			this._initTaskCompletionSource = null;
		}
		this._initTaskCompletionSource = new TaskCompletionSource<InitializeData>();
		DockerWrap.Initialize();
		return await this._initTaskCompletionSource.Task;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000025E5 File Offset: 0x000007E5
	private void HandleDidInitialize(InitializeData initializeData)
	{
		TaskCompletionSource<InitializeData> initTaskCompletionSource = this._initTaskCompletionSource;
		if (initTaskCompletionSource == null)
		{
			return;
		}
		initTaskCompletionSource.TrySetResult(initializeData);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000158F4 File Offset: 0x00013AF4
	public async Task<LoginData> LoginAsync()
	{
		if (this._loginTaskCompletionSource != null)
		{
			this._loginTaskCompletionSource.TrySetCanceled();
			this._loginTaskCompletionSource = null;
		}
		this._loginTaskCompletionSource = new TaskCompletionSource<LoginData>();
		DockerWrap.Login();
		return await this._loginTaskCompletionSource.Task;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000025F9 File Offset: 0x000007F9
	private void HandleDidLogin(LoginData loginData)
	{
		if (loginData.LoginSuccessful)
		{
			this._userName = loginData.UserName;
		}
		else
		{
			this._userName = null;
		}
		TaskCompletionSource<LoginData> loginTaskCompletionSource = this._loginTaskCompletionSource;
		if (loginTaskCompletionSource == null)
		{
			return;
		}
		loginTaskCompletionSource.TrySetResult(loginData);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0001593C File Offset: 0x00013B3C
	public async Task<LogoutData> LogoutAsync()
	{
		if (this._logoutTaskCompletionSource != null)
		{
			this._logoutTaskCompletionSource.TrySetCanceled();
			this._logoutTaskCompletionSource = null;
		}
		this._logoutTaskCompletionSource = new TaskCompletionSource<LogoutData>();
		DockerWrap.Logout();
		return await this._logoutTaskCompletionSource.Task;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000262C File Offset: 0x0000082C
	private void HandleDidLogout(LogoutData logoutData)
	{
		TaskCompletionSource<LogoutData> logoutTaskCompletionSource = this._logoutTaskCompletionSource;
		if (logoutTaskCompletionSource == null)
		{
			return;
		}
		logoutTaskCompletionSource.TrySetResult(logoutData);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00015984 File Offset: 0x00013B84
	public async Task<ReceivedHighscoreData> RequestHighscoreDataAsync(string leaderboardId)
	{
		if (this._requestHighscoreDataTaskCompletionSource != null)
		{
			this._requestHighscoreDataTaskCompletionSource.TrySetCanceled();
			this._requestHighscoreDataTaskCompletionSource = null;
		}
		ReceivedHighscoreData result;
		if (!DockerWrap.WillProvideHighscore())
		{
			result = new ReceivedHighscoreData
			{
				FetchingSuccessful = false
			};
		}
		else
		{
			RequestHighscoreData highscoreRequest = new RequestHighscoreData
			{
				Track = leaderboardId,
				HighscoreType = "All Time"
			};
			this._requestHighscoreDataTaskCompletionSource = new TaskCompletionSource<ReceivedHighscoreData>();
			DockerWrap.RequestHighscoreList(highscoreRequest);
			result = await this._requestHighscoreDataTaskCompletionSource.Task;
		}
		return result;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00002640 File Offset: 0x00000840
	private void HandleDidReceiveHighscore(ReceivedHighscoreData highscoreData)
	{
		TaskCompletionSource<ReceivedHighscoreData> requestHighscoreDataTaskCompletionSource = this._requestHighscoreDataTaskCompletionSource;
		if (requestHighscoreDataTaskCompletionSource == null)
		{
			return;
		}
		requestHighscoreDataTaskCompletionSource.TrySetResult(highscoreData);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000159D4 File Offset: 0x00013BD4
	public void UploadHighscore(string leaderboardId, int score)
	{
		DockerWrap.UploadScore(new UploadScoreData
		{
			Track = leaderboardId,
			Score = score
		});
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00002654 File Offset: 0x00000854
	[Conditional("NetEaseManagerLog")]
	public static void Log(string message)
	{
		UnityEngine.Debug.Log(message);
	}

	// Token: 0x04000060 RID: 96
	private string _userName;

	// Token: 0x04000061 RID: 97
	private TaskCompletionSource<ReceivedHighscoreData> _requestHighscoreDataTaskCompletionSource;

	// Token: 0x04000062 RID: 98
	private TaskCompletionSource<InitializeData> _initTaskCompletionSource;

	// Token: 0x04000063 RID: 99
	private TaskCompletionSource<LoginData> _loginTaskCompletionSource;

	// Token: 0x04000064 RID: 100
	private TaskCompletionSource<LogoutData> _logoutTaskCompletionSource;
}
