using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class TestPlatformAdditionalContentModel : AdditionalContentModel
{
	// Token: 0x06000214 RID: 532 RVA: 0x000023E9 File Offset: 0x000005E9
	protected override void InvalidateDataInternal()
	{
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0001AAA4 File Offset: 0x00018CA4
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusInternalAsync(string levelId, CancellationToken token)
	{
		await Task.Yield();
		token.ThrowIfCancellationRequested();
		foreach (TestPlatformAdditionalContentModel.Entitlement entitlement in this._levelsEntitlements)
		{
			if (entitlement.id == levelId)
			{
				return entitlement.status;
			}
		}
		return AdditionalContentModel.EntitlementStatus.NotOwned;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0001AAFC File Offset: 0x00018CFC
	protected override async Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusInternalAsync(string levelPackId, CancellationToken token)
	{
		await Task.Yield();
		token.ThrowIfCancellationRequested();
		foreach (TestPlatformAdditionalContentModel.Entitlement entitlement in this._levelPacksEntitlements)
		{
			if (entitlement.id == levelPackId)
			{
				return entitlement.status;
			}
		}
		return AdditionalContentModel.EntitlementStatus.NotOwned;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0001AB54 File Offset: 0x00018D54
	public override async Task<AdditionalContentModel.IsPackBetterBuyThanLevelResult> IsPackBetterBuyThanLevelAsync(string levelPackId, CancellationToken token)
	{
		await Task.Yield();
		token.ThrowIfCancellationRequested();
		return this._packBetterBuyThanLevel ? AdditionalContentModel.IsPackBetterBuyThanLevelResult.PackIsBetter : AdditionalContentModel.IsPackBetterBuyThanLevelResult.LevelIsBetter;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0001ABA4 File Offset: 0x00018DA4
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelProductStoreAsync(string levelId, CancellationToken token)
	{
		await Task.Yield();
		token.ThrowIfCancellationRequested();
		Debug.Log("Opening test product store for levelId " + levelId);
		this.BuyLevel(levelId);
		base.InvalidateData();
		return AdditionalContentModel.OpenProductStoreResult.OK;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0001ABFC File Offset: 0x00018DFC
	private void BuyLevel(string levelId)
	{
		TestPlatformAdditionalContentModel.Entitlement entitlement = null;
		foreach (TestPlatformAdditionalContentModel.Entitlement entitlement2 in this._levelsEntitlements)
		{
			if (entitlement2.id == levelId)
			{
				entitlement = entitlement2;
				break;
			}
		}
		if (entitlement == null)
		{
			entitlement = new TestPlatformAdditionalContentModel.Entitlement();
			entitlement.id = levelId;
			this._levelsEntitlements = new List<TestPlatformAdditionalContentModel.Entitlement>(this._levelsEntitlements)
			{
				entitlement
			}.ToArray();
		}
		entitlement.status = AdditionalContentModel.EntitlementStatus.Owned;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0001AC70 File Offset: 0x00018E70
	public override async Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelPackProductStoreAsync(string levelPackId, CancellationToken token)
	{
		await Task.Yield();
		token.ThrowIfCancellationRequested();
		Debug.Log("Opening test product store for levelPackId " + levelPackId);
		return AdditionalContentModel.OpenProductStoreResult.OK;
	}

	// Token: 0x04000238 RID: 568
	[SerializeField]
	private TestPlatformAdditionalContentModel.Entitlement[] _levelsEntitlements;

	// Token: 0x04000239 RID: 569
	[SerializeField]
	private TestPlatformAdditionalContentModel.Entitlement[] _levelPacksEntitlements;

	// Token: 0x0400023A RID: 570
	[SerializeField]
	private bool _packBetterBuyThanLevel = true;

	// Token: 0x02000084 RID: 132
	[Serializable]
	private class Entitlement
	{
		// Token: 0x0400023B RID: 571
		public string id;

		// Token: 0x0400023C RID: 572
		public AdditionalContentModel.EntitlementStatus status;
	}
}
