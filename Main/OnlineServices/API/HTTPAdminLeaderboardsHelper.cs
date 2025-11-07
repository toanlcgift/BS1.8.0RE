using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace OnlineServices.API
{
	// Token: 0x0200049E RID: 1182
	public class HTTPAdminLeaderboardsHelper
	{
		// Token: 0x060015D2 RID: 5586 RVA: 0x000104F5 File Offset: 0x0000E6F5
		public HTTPAdminLeaderboardsHelper(string uri, string secret)
		{
			this._uriBuilder = new UriBuilder(uri);
			this._secret = secret;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0004FB58 File Offset: 0x0004DD58
		public async Task<HTTPAdminLeaderboardsHelper.ServerStatusResultDto> ServerStatus(CancellationToken cancellationToken)
		{
			this._uriBuilder.Path = "/Admin/ServerStatus";
			this._uriBuilder.Query = "?Secret=" + UnityWebRequest.EscapeURL(this._secret);
			string uri = this._uriBuilder.Uri.ToString();
			return JsonUtility.FromJson<HTTPAdminLeaderboardsHelper.ServerStatusResultDto>(await this.SendWebRequestAsync(uri, "POST", null, cancellationToken));
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0004FBA8 File Offset: 0x0004DDA8
		public async Task<HTTPAdminLeaderboardsHelper.LeaderboardsInfoResultDto> LeaderboardsExist(string[] leaderboardIds, CancellationToken cancellationToken)
		{
			this._uriBuilder.Path = "/Admin/LeaderboardsExist";
			this._uriBuilder.Query = "?Secret=" + UnityWebRequest.EscapeURL(this._secret);
			string uri = this._uriBuilder.Uri.ToString();
			string bodyData = JsonUtility.ToJson(new HTTPAdminLeaderboardsHelper.LeaderboardsIdsDto
			{
				leaderboardsIds = leaderboardIds
			});
			return JsonUtility.FromJson<HTTPAdminLeaderboardsHelper.LeaderboardsInfoResultDto>(await this.SendWebRequestAsync(uri, "POST", bodyData, cancellationToken));
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0004FC00 File Offset: 0x0004DE00
		public async Task<bool> CreateOrUpdateLeaderboards(string[] leaderboardIds, CancellationToken cancellationToken)
		{
			this._uriBuilder.Path = "/Admin/CreateOrUpdateLeaderboards";
			this._uriBuilder.Query = "?Secret=" + UnityWebRequest.EscapeURL(this._secret);
			string text = this._uriBuilder.Uri.ToString();
			Debug.Log(text);
			string text2 = JsonUtility.ToJson(new HTTPAdminLeaderboardsHelper.LeaderboardsIdsDto
			{
				leaderboardsIds = leaderboardIds
			});
			Debug.Log(text2);
			string text3 = await this.SendWebRequestAsync(text, "POST", text2, cancellationToken);
			bool result = false;
			Debug.Log(text3);
			bool.TryParse(text3, out result);
			return result;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0004FC58 File Offset: 0x0004DE58
		private async Task<string> SendWebRequestAsync(string uri, string method, string bodyData, CancellationToken cancellationToken)
		{
			string result;
			using (UnityWebRequest webRequest = new UnityWebRequest(uri, method))
			{
				if (bodyData != null)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(bodyData);
					webRequest.uploadHandler = new UploadHandlerRaw(bytes);
					webRequest.SetRequestHeader("Content-Type", "application/json");
				}
				webRequest.downloadHandler = new DownloadHandlerBuffer();
				await this.SendAndWaitAsync(webRequest, cancellationToken);
				if (webRequest.isNetworkError || webRequest.isHttpError)
				{
					if (webRequest.responseCode == 401L)
					{
						Debug.Log("Unauthorized");
					}
					Debug.Log(webRequest.downloadHandler.text);
					Debug.Log(webRequest.error);
					result = null;
				}
				else
				{
					result = webRequest.downloadHandler.text;
				}
			}
			return result;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0004FCC0 File Offset: 0x0004DEC0
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
				await Task.Delay(100);
			}
		}

		// Token: 0x0400160C RID: 5644
		private string _secret;

		// Token: 0x0400160D RID: 5645
		private UriBuilder _uriBuilder;

		// Token: 0x0200049F RID: 1183
		[Serializable]
		public class LeaderboardsIdsDto
		{
			// Token: 0x0400160E RID: 5646
			public string[] leaderboardsIds;
		}

		// Token: 0x020004A0 RID: 1184
		[Serializable]
		public class ServerStatusResultDto
		{
			// Token: 0x0400160F RID: 5647
			public bool everythingOK;
		}

		// Token: 0x020004A1 RID: 1185
		[Serializable]
		public class LeaderboardsInfoDto
		{
			// Token: 0x04001610 RID: 5648
			public bool exist;
		}

		// Token: 0x020004A2 RID: 1186
		[Serializable]
		public class LeaderboardsInfoResultDto
		{
			// Token: 0x04001611 RID: 5649
			public HTTPAdminLeaderboardsHelper.LeaderboardsInfoDto[] leaderboardsInfoDtos;
		}
	}
}
