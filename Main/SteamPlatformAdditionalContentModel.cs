using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks;
using Zenject;

// Token: 0x0200007C RID: 124
public class SteamPlatformAdditionalContentModel : AdditionalContentModel
{
	// Token: 0x060001FB RID: 507 RVA: 0x00003752 File Offset: 0x00001952
	protected override void InvalidateDataInternal()
	{
		this._isDataValid = false;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0001A15C File Offset: 0x0001835C
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusInternalAsync(string levelId, CancellationToken cancellationToken)
	{
		TaskAwaiter<bool> taskAwaiter = this.DataIsValidAsync(cancellationToken).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			 taskAwaiter.GetResult();
			TaskAwaiter<bool> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<bool>);
		}
		AdditionalContentModel.EntitlementStatus result;
		if (taskAwaiter.GetResult())
		{
			result = (this.HasLevelEntitlement(levelId) ? AdditionalContentModel.EntitlementStatus.Owned : AdditionalContentModel.EntitlementStatus.NotOwned);
		}
		else
		{
			result = AdditionalContentModel.EntitlementStatus.Failed;
		}
		return result;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0001A1B4 File Offset: 0x000183B4
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusInternalAsync(string packId, CancellationToken cancellationToken)
	{
		TaskAwaiter<bool> taskAwaiter = this.DataIsValidAsync(cancellationToken).GetAwaiter();
		if (!taskAwaiter.IsCompleted)
		{
			taskAwaiter.GetResult();
			TaskAwaiter<bool> taskAwaiter2;
			taskAwaiter = taskAwaiter2;
			taskAwaiter2 = default(TaskAwaiter<bool>);
		}
		AdditionalContentModel.EntitlementStatus result;
		if (taskAwaiter.GetResult())
		{
			result = (this.HasLevelPackEntitlement(packId) ? AdditionalContentModel.EntitlementStatus.Owned : AdditionalContentModel.EntitlementStatus.NotOwned);
		}
		else
		{
			result = AdditionalContentModel.EntitlementStatus.Failed;
		}
		return result;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0001A20C File Offset: 0x0001840C
	private async Task<bool> DataIsValidAsync(CancellationToken cancellationToken)
	{
		await this._semaphoreSlim.WaitAsync(cancellationToken);
		try
		{
			if (!this._isDataValid)
			{
				this._isDataValid = (await this.CheckForNewEntitlementsAsync(cancellationToken) == AdditionalContentModel.UpdateEntitlementsResult.OK);
			}
		}
		finally
		{
			this._semaphoreSlim.Release();
		}
		return this._isDataValid;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0001A25C File Offset: 0x0001845C
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelProductStoreAsync(string levelId, CancellationToken token)
	{
		SteamLevelProductsModelSO.LevelProductData levelProductData = this._steamLevelProductsModel.GetLevelProductData(levelId);
		AdditionalContentModel.OpenProductStoreResult result;
		if (levelProductData == null)
		{
			result = await Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.Failed);
		}
		else
		{
			this.OpenProductStore(levelProductData.appId);
			result = await Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.OK);
		}
		return result;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0001A2AC File Offset: 0x000184AC
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelPackProductStoreAsync(string levelPackId, CancellationToken token)
	{
		SteamLevelProductsModelSO.LevelPackProductData levelPackProductData = this._steamLevelProductsModel.GetLevelPackProductData(levelPackId);
		AdditionalContentModel.OpenProductStoreResult result;
		if (levelPackProductData == null)
		{
			result = await Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.Failed);
		}
		else
		{
			this.OpenBundleUrl(levelPackProductData.bundleId);
			result = await Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.OK);
		}
		return result;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000375B File Offset: 0x0000195B
	private void OpenProductStore(uint appId)
	{
		SteamFriends.ActivateGameOverlayToStore((AppId_t)appId, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
		base.InvalidateData();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000376F File Offset: 0x0000196F
	private void OpenBundleUrl(uint bundleId)
	{
		SteamFriends.ActivateGameOverlayToWebPage("https://store.steampowered.com/bundle/" + bundleId);
		base.InvalidateData();
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000378C File Offset: 0x0000198C
	public override Task<AdditionalContentModel.IsPackBetterBuyThanLevelResult> IsPackBetterBuyThanLevelAsync(string levelPackId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.IsPackBetterBuyThanLevelResult>(AdditionalContentModel.IsPackBetterBuyThanLevelResult.PackIsBetter);
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0001A2FC File Offset: 0x000184FC
	private async Task<AdditionalContentModel.UpdateEntitlementsResult> CheckForNewEntitlementsAsync(CancellationToken cancellationToken)
	{
		SteamLevelProductsModelSO.LevelPackProductData[] levelPackProductsData = this._steamLevelProductsModel.levelPackProductsData;
		for (int i = 0; i < levelPackProductsData.Length; i++)
		{
			foreach (SteamLevelProductsModelSO.LevelProductData levelProductData in levelPackProductsData[i].levelProductsData)
			{
				if (SteamApps.BIsDlcInstalled((AppId_t)levelProductData.appId))
				{
					this._entitlementsAppIds.Add(levelProductData.appId);
				}
			}
		}
		return await Task.FromResult<AdditionalContentModel.UpdateEntitlementsResult>(AdditionalContentModel.UpdateEntitlementsResult.OK);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0001A344 File Offset: 0x00018544
	private bool HasLevelEntitlement(string levelId)
	{
		SteamLevelProductsModelSO.LevelProductData levelProductData = this._steamLevelProductsModel.GetLevelProductData(levelId);
		return levelProductData != null && this._entitlementsAppIds.Contains(levelProductData.appId);
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0001A374 File Offset: 0x00018574
	private bool HasLevelPackEntitlement(string levelPackId)
	{
		SteamLevelProductsModelSO.LevelPackProductData levelPackProductData = this._steamLevelProductsModel.GetLevelPackProductData(levelPackId);
		if (levelPackProductData != null)
		{
			foreach (SteamLevelProductsModelSO.LevelProductData levelProductData in levelPackProductData.levelProductsData)
			{
				if (!this._entitlementsAppIds.Contains(levelProductData.appId))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x04000213 RID: 531
	[Inject]
	private SteamLevelProductsModelSO _steamLevelProductsModel;

	// Token: 0x04000214 RID: 532
	private HashSet<uint> _entitlementsAppIds = new HashSet<uint>();

	// Token: 0x04000215 RID: 533
	private TaskCompletionSource<bool> _dataIsValidTaskCompletionSource;

	// Token: 0x04000216 RID: 534
	private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

	// Token: 0x04000217 RID: 535
	private bool _isDataValid;
}
