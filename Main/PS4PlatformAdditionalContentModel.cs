using System;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x020001E3 RID: 483
public class PS4PlatformAdditionalContentModel : AdditionalContentModel
{
	// Token: 0x06000760 RID: 1888 RVA: 0x000023E9 File Offset: 0x000005E9
	protected override void InvalidateDataInternal()
	{
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000063CE File Offset: 0x000045CE
	protected override Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusInternalAsync(string levelId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.EntitlementStatus>(AdditionalContentModel.EntitlementStatus.Failed);
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x000063CE File Offset: 0x000045CE
	protected override Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusInternalAsync(string levelPackId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.EntitlementStatus>(AdditionalContentModel.EntitlementStatus.Failed);
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x000063D6 File Offset: 0x000045D6
	public override Task<AdditionalContentModel.IsPackBetterBuyThanLevelResult> IsPackBetterBuyThanLevelAsync(string levelPackId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.IsPackBetterBuyThanLevelResult>(AdditionalContentModel.IsPackBetterBuyThanLevelResult.Failed);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x000063DE File Offset: 0x000045DE
	public override Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelProductStoreAsync(string levelId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.Failed);
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x000063DE File Offset: 0x000045DE
	public override Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelPackProductStoreAsync(string levelPackId, CancellationToken token)
	{
		return Task.FromResult<AdditionalContentModel.OpenProductStoreResult>(AdditionalContentModel.OpenProductStoreResult.Failed);
	}
}
