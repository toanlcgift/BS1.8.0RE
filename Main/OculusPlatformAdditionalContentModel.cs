using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using Zenject;

// Token: 0x02000071 RID: 113
public class OculusPlatformAdditionalContentModel : AdditionalContentModel
{
	// Token: 0x060001DA RID: 474 RVA: 0x000036AA File Offset: 0x000018AA
	protected override void InvalidateDataInternal()
	{
		this._isDataValid = false;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0001933C File Offset: 0x0001753C
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusInternalAsync(string levelId, CancellationToken cancellationToken)
	{
		Task<bool> taskAwaiter = this.DataIsValidAsync(cancellationToken);
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.Wait();
			Task<bool> taskAwaiter2 = null;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(Task<bool>);
		}
		AdditionalContentModel.EntitlementStatus result;
		if (taskAwaiter.Result)
		{
			result = (this.HasLevelEntitlement(levelId) ? AdditionalContentModel.EntitlementStatus.Owned : AdditionalContentModel.EntitlementStatus.NotOwned);
		}
		else
		{
			result = AdditionalContentModel.EntitlementStatus.Failed;
		}
		return result;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00019394 File Offset: 0x00017594
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusInternalAsync(string packId, CancellationToken cancellationToken)
	{
		Task<bool> taskAwaiter = this.DataIsValidAsync(cancellationToken);
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.Wait();
			Task<bool> taskAwaiter2 = null;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(Task<bool>);
		}
		AdditionalContentModel.EntitlementStatus result;
		if (taskAwaiter.Result)
		{
			result = (this.HasLevelPackEntitlement(packId) ? AdditionalContentModel.EntitlementStatus.Owned : AdditionalContentModel.EntitlementStatus.NotOwned);
		}
		else
		{
			result = AdditionalContentModel.EntitlementStatus.Failed;
		}
		return result;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x000193EC File Offset: 0x000175EC
	private async Task<bool> DataIsValidAsync(CancellationToken cancellationToken)
	{
		this._semaphoreSlim.Wait();
		try
		{
			if (!this._isDataValid)
			{
				this._isDataValid = (this.CheckForNewEntitlementsAsync(cancellationToken).Result == AdditionalContentModel.UpdateEntitlementsResult.OK);
			}
		}
		finally
		{
			this._semaphoreSlim.Release();
		}
		return this._isDataValid;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0001943C File Offset: 0x0001763C
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelProductStoreAsync(string levelId, CancellationToken cancellationToken)
	{
		string sku = this._oculusLevelProductsModel.GetLevelProductData(levelId).sku;
		Message<Purchase> message = this.LaunchCheckoutFlow(sku).Result;
		AdditionalContentModel.OpenProductStoreResult result;
		if (message.IsError)
		{
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.Failed;
		}
		else if (!string.IsNullOrEmpty(message.Data.Sku))
		{
			this._entitlementsSKU.Add(message.Data.Sku);
			
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.OK;
		}
		else
		{
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.OK;
		}
		return result;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00019494 File Offset: 0x00017694
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelPackProductStoreAsync(string levelPackId, CancellationToken cancellationToken)
	{
		string sku = this._oculusLevelProductsModel.GetLevelPackProductData(levelPackId).sku;
		Message<Purchase> message = this.LaunchCheckoutFlow(sku).Result;
		AdditionalContentModel.OpenProductStoreResult result;
		if (message.IsError)
		{
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.Failed;
		}
		else if (!string.IsNullOrEmpty(message.Data.Sku))
		{
			OculusLevelProductsModelSO.LevelPackProductData levelPackProductData = this._oculusLevelProductsModel.GetLevelPackProductData(levelPackId);
			if (levelPackProductData != null)
			{
				foreach (OculusLevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
				{
					this._entitlementsSKU.Add(levelProductData.sku);
				}
			}
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.OK;
		}
		else
		{
			base.InvalidateData();
			cancellationToken.ThrowIfCancellationRequested();
			result = AdditionalContentModel.OpenProductStoreResult.OK;
		}
		return result;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x000194EC File Offset: 0x000176EC
	private async Task<Message<Purchase>> LaunchCheckoutFlow(string sku)
	{
		TaskCompletionSource<Message<Purchase>> launchCheckoutFlowTaskSource = new TaskCompletionSource<Message<Purchase>>();
		IAP.LaunchCheckoutFlow(sku).OnComplete(delegate(Message<Purchase> msg)
		{
			launchCheckoutFlowTaskSource.TrySetResult(msg);
		});
		return  launchCheckoutFlowTaskSource.Task.Result;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00019534 File Offset: 0x00017734
	public override async Task<AdditionalContentModel.IsPackBetterBuyThanLevelResult> IsPackBetterBuyThanLevelAsync(string levelPackId, CancellationToken token)
	{
		return AdditionalContentModel.IsPackBetterBuyThanLevelResult.PackIsBetter;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00019574 File Offset: 0x00017774
	private async Task<AdditionalContentModel.UpdateEntitlementsResult> CheckForNewEntitlementsAsync(CancellationToken cancellationToken)
	{
		TaskCompletionSource<AdditionalContentModel.UpdateEntitlementsResult> getViewerPurchasesTaskSource = new TaskCompletionSource<AdditionalContentModel.UpdateEntitlementsResult>();
		Message<PurchaseList>.Callback messageCallback = null;
		AssetFile.GetList().OnComplete(delegate(Message<AssetDetailsList> getListMsg)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				getViewerPurchasesTaskSource.TrySetCanceled();
				return;
			}
			if (getListMsg.IsError)
			{
				getViewerPurchasesTaskSource.TrySetResult(AdditionalContentModel.UpdateEntitlementsResult.Failed);
				return;
			}
			bool flag = true;
			foreach (AssetDetails assetDetails in getListMsg.Data)
			{
				string fileName = Path.GetFileName(assetDetails.Filepath);
				string levelSku = this._oculusLevelProductsModel.GetLevelSku(fileName);
				if (assetDetails.IapStatus == "free" || assetDetails.IapStatus == "entitled")
				{
					this._entitlementsSKU.Add(levelSku);
				}
				else
				{
					flag = false;
				}
			}
			if (flag || !UnityEngine.Application.isMobilePlatform)
			{
				getViewerPurchasesTaskSource.TrySetResult(AdditionalContentModel.UpdateEntitlementsResult.OK);
				return;
			}
			Request<PurchaseList> viewerPurchases = IAP.GetViewerPurchases();
			Message<PurchaseList>.Callback callback;
			if ((callback = messageCallback) == null)
			{
				callback = (messageCallback = delegate(Message<PurchaseList> getPurchasesMsg)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						getViewerPurchasesTaskSource.TrySetCanceled();
						return;
					}
					if (!getPurchasesMsg.IsError)
					{
						foreach (Purchase purchase in getPurchasesMsg.Data)
						{
							this._entitlementsSKU.Add(purchase.Sku);
						}
					}
					getViewerPurchasesTaskSource.TrySetResult(AdditionalContentModel.UpdateEntitlementsResult.OK);
				});
			}
			viewerPurchases.OnComplete(callback);
		});
		return  getViewerPurchasesTaskSource.Task.Result;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x000195C4 File Offset: 0x000177C4
	private bool HasLevelEntitlement(string levelId)
	{
		string sku = this._oculusLevelProductsModel.GetLevelProductData(levelId).sku;
		return this._entitlementsSKU.Contains(sku);
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x000195F0 File Offset: 0x000177F0
	private bool HasLevelPackEntitlement(string levelPackId)
	{
		OculusLevelProductsModelSO.LevelPackProductData levelPackProductData = this._oculusLevelProductsModel.GetLevelPackProductData(levelPackId);
		if (levelPackProductData != null)
		{
			foreach (OculusLevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
			{
				if (!this._entitlementsSKU.Contains(levelProductData.sku))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x040001DE RID: 478
	[Inject]
	private OculusLevelProductsModelSO _oculusLevelProductsModel;

	// Token: 0x040001DF RID: 479
	private HashSet<string> _entitlementsSKU = new HashSet<string>();

	// Token: 0x040001E0 RID: 480
	private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

	// Token: 0x040001E1 RID: 481
	private bool _isDataValid;
}
