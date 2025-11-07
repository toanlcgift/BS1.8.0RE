using System;
using Polyglot;

namespace OnlineServices
{
	// Token: 0x02000497 RID: 1175
	public class PlatformServicesAvailabilityInfo
	{
		// Token: 0x060015BC RID: 5564 RVA: 0x00010409 File Offset: 0x0000E609
		private PlatformServicesAvailabilityInfo(PlatformServicesAvailabilityInfo.OnlineServicesAvailability availability, string localizedMessage)
		{
			this.availability = availability;
			this.localizedMessage = localizedMessage;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0001041F File Offset: 0x0000E61F
		public static PlatformServicesAvailabilityInfo everythingOK
		{
			get
			{
				return new PlatformServicesAvailabilityInfo(PlatformServicesAvailabilityInfo.OnlineServicesAvailability.Available, null);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00010428 File Offset: 0x0000E628
		public static PlatformServicesAvailabilityInfo onlineServicesUnavailableError
		{
			get
			{
				return new PlatformServicesAvailabilityInfo(PlatformServicesAvailabilityInfo.OnlineServicesAvailability.Unavailable, Localization.Get("LEADERBOARDS_PLATFORM_SERVICES_ERROR"));
			}
		}

		// Token: 0x040015F0 RID: 5616
		public readonly PlatformServicesAvailabilityInfo.OnlineServicesAvailability availability;

		// Token: 0x040015F1 RID: 5617
		public readonly string localizedMessage;

		// Token: 0x02000498 RID: 1176
		public enum OnlineServicesAvailability
		{
			// Token: 0x040015F3 RID: 5619
			Available,
			// Token: 0x040015F4 RID: 5620
			Unavailable
		}
	}
}
