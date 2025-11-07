using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;
using UnityEngine;
using UnityEngine.Networking;

namespace OnlineServices.API
{
	// Token: 0x020004A8 RID: 1192
	public class HTTPLeaderboardsOathHelper
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x00010556 File Offset: 0x0000E756
		public HTTPLeaderboardsOathHelper(IUserLoginDtoDataSource userLoginDataSource, UriBuilder uriBuilder)
		{
			this._userLoginDataSource = userLoginDataSource;
			this._uriBuilder = uriBuilder;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00050364 File Offset: 0x0004E564
		public async Task<string> SendWebRequestWithOathAsync(string path, string method, object objectToSendAsJson, CancellationToken cancellationToken)
		{
			await this.LoginIfNeededAsync(cancellationToken);
			string result;
			if (!this.IsUserLoggedIn())
			{
				result = null;
			}
			else
			{
				this._uriBuilder.Path = path;
				this._uriBuilder.Query = "";
				string uri = this._uriBuilder.Uri.ToString();
				string bodyData = (objectToSendAsJson == null) ? null : JsonUtility.ToJson(objectToSendAsJson);
				string text = await this.SendWebRequestAsync(uri, method, bodyData, this._jwtToken.token);
				if (text == null && this._jwtToken == null)
				{
					await this.LoginIfNeededAsync(cancellationToken);
					if (!this.IsUserLoggedIn())
					{
						return null;
					}
					text = await this.SendWebRequestAsync(uri, method, bodyData, this._jwtToken.token);
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000503CC File Offset: 0x0004E5CC
		public async Task LogOut()
		{
			await this.SendWebRequestWithOathAsync("/api/v1/Account/LogOut", "POST", null, new CancellationTokenSource().Token);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00050414 File Offset: 0x0004E614
		private async Task LoginIfNeededAsync(CancellationToken cancellationToken)
		{
			if (this._jwtToken == null)
			{
				TokenDTO jwtToken = await this.GetJwtTokenAsync(cancellationToken);
				this._jwtToken = jwtToken;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0001056C File Offset: 0x0000E76C
		private bool IsUserLoggedIn()
		{
			return this._jwtToken != null;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00050464 File Offset: 0x0004E664
		private async Task<TokenDTO> GetJwtTokenAsync(CancellationToken cancellationToken)
		{
			this._uriBuilder.Path = "/api/v1/Account/LogIn";
			this._uriBuilder.Query = "";
			string uri = this._uriBuilder.Uri.ToString();
			LoginRequestDTO loginRequestDTO = await this._userLoginDataSource.GetLoginRequestDTOAsync(cancellationToken);
			TokenDTO result;
			if (loginRequestDTO == null)
			{
				result = null;
			}
			else
			{
				string bodyData = JsonUtility.ToJson(loginRequestDTO);
				try
				{
					result = JsonUtility.FromJson<TokenDTO>(await this.SendWebRequestAsync(uri, "POST", bodyData, null));
				}
				catch (NullReferenceException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000504B4 File Offset: 0x0004E6B4
		private async Task<string> SendWebRequestAsync(string uri, string method, string bodyData, string bearerToken)
		{
			string result;
			using (UnityWebRequest webRequest = new UnityWebRequest(uri, method))
			{
				if (bearerToken != null)
				{
					webRequest.SetRequestHeader("Authorization", "Bearer " + bearerToken);
				}
				if (bodyData != null)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(bodyData);
					webRequest.uploadHandler = new UploadHandlerRaw(bytes);
					webRequest.SetRequestHeader("Content-Type", "application/json");
				}
				webRequest.downloadHandler = new DownloadHandlerBuffer();
				AsyncOperation asyncOperation = webRequest.SendWebRequest();
				TaskCompletionSource<bool> taskComplitionSource = new TaskCompletionSource<bool>();
				asyncOperation.completed += delegate(AsyncOperation asyncOperation2)
				{
					if (webRequest.isNetworkError || webRequest.isHttpError)
					{
						if (webRequest.responseCode == 401L)
						{
							this._jwtToken = null;
						}
						taskComplitionSource.TrySetResult(false);
					}
					taskComplitionSource.TrySetResult(true);
				};
				TaskAwaiter<bool> taskAwaiter = taskComplitionSource.Task.GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					taskAwaiter.GetResult();
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				result = (taskAwaiter.GetResult() ? webRequest.downloadHandler.text : null);
			}
			return result;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0005051C File Offset: 0x0004E71C
		private async Task SendAndWaitAsync(UnityWebRequest webRequest, CancellationToken cancellationToken)
		{
			AsyncOperation asyncOperation = webRequest.SendWebRequest();
			while (!asyncOperation.isDone)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					webRequest.Abort();
					throw new OperationCanceledException(cancellationToken);
				}
				await Task.Delay(1000);
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00002654 File Offset: 0x00000854
		[Conditional("HTTPLeaderboardsOathHelperLog")]
		public static void Log(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		// Token: 0x04001632 RID: 5682
		private const string kLoginPath = "/api/v1/Account/LogIn";

		// Token: 0x04001633 RID: 5683
		private const string kLogoutPath = "/api/v1/Account/LogOut";

		// Token: 0x04001634 RID: 5684
		private IUserLoginDtoDataSource _userLoginDataSource;

		// Token: 0x04001635 RID: 5685
		private UriBuilder _uriBuilder;

		// Token: 0x04001636 RID: 5686
		private TokenDTO _jwtToken;
	}
}
