using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineServices
{
	// Token: 0x02000495 RID: 1173
	public class PlatformOnlineServicesAvailabilityModel
	{
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060015B6 RID: 5558 RVA: 0x0004F554 File Offset: 0x0004D754
		// (remove) Token: 0x060015B7 RID: 5559 RVA: 0x0004F58C File Offset: 0x0004D78C
		public event Action<PlatformServicesAvailabilityInfo> platformServicesAvailabilityInfoChangedEvent;

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
		public async Task<PlatformServicesAvailabilityInfo> GetPlatformServicesAvailabilityInfo(CancellationToken cancellationToken)
		{
			return await Task.FromResult<PlatformServicesAvailabilityInfo>(PlatformServicesAvailabilityInfo.everythingOK);
		}
	}
}
