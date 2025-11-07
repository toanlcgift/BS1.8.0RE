using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

// Token: 0x02000068 RID: 104
public abstract class AdditionalContentModel : MonoBehaviour
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060001C1 RID: 449 RVA: 0x00018F64 File Offset: 0x00017164
	// (remove) Token: 0x060001C2 RID: 450 RVA: 0x00018F9C File Offset: 0x0001719C
	public event Action didInvalidateDataEvent;

	// Token: 0x060001C3 RID: 451 RVA: 0x00003621 File Offset: 0x00001821
	protected void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			this.InvalidateData();
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000362C File Offset: 0x0000182C
	protected void InvalidateData()
	{
		this.InvalidateDataInternal();
		Action action = this.didInvalidateDataEvent;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00018FD4 File Offset: 0x000171D4
	public async Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusAsync(string levelId, CancellationToken token)
	{
		AdditionalContentModel.EntitlementStatus result;
		if (this._alwaysOwnedContentContainer.alwaysOwnedBeatmapLevelIds.Contains(levelId))
		{
			result = AdditionalContentModel.EntitlementStatus.Owned;
		}
		else if (levelId.StartsWith("custom_level_"))
		{
			result = AdditionalContentModel.EntitlementStatus.Owned;
		}
		else
		{
			result = await this.GetLevelEntitlementStatusInternalAsync(levelId, token);
		}
		return result;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0001902C File Offset: 0x0001722C
	public async Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusAsync(string levelPackId, CancellationToken token)
	{
		AdditionalContentModel.EntitlementStatus result;
		if (this._alwaysOwnedContentContainer.alwaysOwnedPacksIds.Contains(levelPackId))
		{
			result = AdditionalContentModel.EntitlementStatus.Owned;
		}
		else if (levelPackId.StartsWith("custom_levelpack_"))
		{
			result = AdditionalContentModel.EntitlementStatus.Owned;
		}
		else
		{
			result = await this.GetPackEntitlementStatusInternalAsync(levelPackId, token);
		}
		return result;
	}

	// Token: 0x060001C7 RID: 455
	protected abstract void InvalidateDataInternal();

	// Token: 0x060001C8 RID: 456
	protected abstract Task<AdditionalContentModel.EntitlementStatus> GetLevelEntitlementStatusInternalAsync(string levelId, CancellationToken token);

	// Token: 0x060001C9 RID: 457
	protected abstract Task<AdditionalContentModel.EntitlementStatus> GetPackEntitlementStatusInternalAsync(string levelPackId, CancellationToken token);

	// Token: 0x060001CA RID: 458
	public abstract Task<AdditionalContentModel.IsPackBetterBuyThanLevelResult> IsPackBetterBuyThanLevelAsync(string levelPackId, CancellationToken token);

	// Token: 0x060001CB RID: 459
	public abstract Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelProductStoreAsync(string levelId, CancellationToken token);

	// Token: 0x060001CC RID: 460
	public abstract Task<AdditionalContentModel.OpenProductStoreResult> OpenLevelPackProductStoreAsync(string levelPackId, CancellationToken token);

	// Token: 0x040001BD RID: 445
	[Inject]
	private AlwaysOwnedContentContainerSO _alwaysOwnedContentContainer;

	// Token: 0x02000069 RID: 105
	public enum EntitlementStatus
	{
		// Token: 0x040001C0 RID: 448
		Failed,
		// Token: 0x040001C1 RID: 449
		Owned,
		// Token: 0x040001C2 RID: 450
		NotOwned
	}

	// Token: 0x0200006A RID: 106
	public enum OpenProductStoreResult
	{
		// Token: 0x040001C4 RID: 452
		OK,
		// Token: 0x040001C5 RID: 453
		Failed
	}

	// Token: 0x0200006B RID: 107
	public enum UpdateEntitlementsResult
	{
		// Token: 0x040001C7 RID: 455
		OK,
		// Token: 0x040001C8 RID: 456
		Failed
	}

	// Token: 0x0200006C RID: 108
	public enum IsPackBetterBuyThanLevelResult
	{
		// Token: 0x040001CA RID: 458
		PackIsBetter,
		// Token: 0x040001CB RID: 459
		LevelIsBetter,
		// Token: 0x040001CC RID: 460
		Failed
	}
}
