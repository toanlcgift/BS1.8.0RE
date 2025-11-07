using System;

// Token: 0x020001B4 RID: 436
public class PlatformTokenInfo
{
	// Token: 0x060006C2 RID: 1730 RVA: 0x00005EBF File Offset: 0x000040BF
	public PlatformTokenInfo(string platformToken, PlatformTokenInfo.PlatformEnvironmentInfo platformEnvironmentInfo)
	{
		this.platformToken = platformToken;
		this.platformEnvironmentInfo = platformEnvironmentInfo;
	}

	// Token: 0x0400073F RID: 1855
	public readonly string platformToken;

	// Token: 0x04000740 RID: 1856
	public readonly PlatformTokenInfo.PlatformEnvironmentInfo platformEnvironmentInfo;

	// Token: 0x020001B5 RID: 437
	public class PlatformEnvironmentInfo
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00005ED5 File Offset: 0x000040D5
		private PlatformEnvironmentInfo(PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment platformEnvironment, string serializedName)
		{
			this.platformEnvironment = platformEnvironment;
			this.serializedName = serializedName;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00005EEB File Offset: 0x000040EB
		public static PlatformTokenInfo.PlatformEnvironmentInfo TestPlatformEnvironmentInfo()
		{
			return new PlatformTokenInfo.PlatformEnvironmentInfo(PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment.Test, "Test");
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00005EF8 File Offset: 0x000040F8
		public static PlatformTokenInfo.PlatformEnvironmentInfo DevelopmentPlatformEnvironmentInfo()
		{
			return new PlatformTokenInfo.PlatformEnvironmentInfo(PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment.Development, "Development");
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00005F05 File Offset: 0x00004105
		public static PlatformTokenInfo.PlatformEnvironmentInfo CertificationPlatformEnvironmentInfo()
		{
			return new PlatformTokenInfo.PlatformEnvironmentInfo(PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment.Certification, "Certification");
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00005F12 File Offset: 0x00004112
		public static PlatformTokenInfo.PlatformEnvironmentInfo LivePlatformEnvironmentInfo()
		{
			return new PlatformTokenInfo.PlatformEnvironmentInfo(PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment.Live, "Live");
		}

		// Token: 0x04000741 RID: 1857
		public readonly PlatformTokenInfo.PlatformEnvironmentInfo.PlatformEnvironment platformEnvironment;

		// Token: 0x04000742 RID: 1858
		public readonly string serializedName;

		// Token: 0x020001B6 RID: 438
		public enum PlatformEnvironment : byte
		{
			// Token: 0x04000744 RID: 1860
			Test,
			// Token: 0x04000745 RID: 1861
			Development,
			// Token: 0x04000746 RID: 1862
			Certification,
			// Token: 0x04000747 RID: 1863
			Live
		}
	}
}
