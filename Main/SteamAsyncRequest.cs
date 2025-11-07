using System;
using Steamworks;

// Token: 0x020000D1 RID: 209
public class SteamAsyncRequest<T> : HMAutoincrementedRequestId
{
	// Token: 0x06000311 RID: 785 RVA: 0x00003F16 File Offset: 0x00002116
	public void MakeRequest(SteamAPICall_t apiCall, SteamAsyncRequest<T>.CompletionHander completionHandler)
	{
		this._completionHander = completionHandler;
		this._callResult = CallResult<T>.Create(new CallResult<T>.APIDispatchDelegate(this.CallResult));
		this._callResult.Set(apiCall, new CallResult<T>.APIDispatchDelegate(this.CallResult));
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00003F4E File Offset: 0x0000214E
	public void Cancel()
	{
		this._callResult.Cancel();
		this._completionHander = null;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00003F62 File Offset: 0x00002162
	private void CallResult(T callback, bool ioFailure)
	{
		if (this._completionHander != null)
		{
			this._completionHander(callback, ioFailure);
		}
	}

	// Token: 0x04000389 RID: 905
	private SteamAsyncRequest<T>.CompletionHander _completionHander;

	// Token: 0x0400038A RID: 906
	private CallResult<T> _callResult;

	// Token: 0x020000D2 RID: 210
	// (Invoke) Token: 0x06000316 RID: 790
	public delegate void CompletionHander(T callback, bool ioFailure);
}
