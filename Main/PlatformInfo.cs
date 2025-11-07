using System;

// Token: 0x020001B2 RID: 434
public class PlatformInfo
{
	// Token: 0x060006C0 RID: 1728 RVA: 0x00005EA9 File Offset: 0x000040A9
	private PlatformInfo(PlatformInfo.Platform platform, string serialzedName)
	{
		this.platform = platform;
		this.serialzedName = serialzedName;
	}

	// Token: 0x04000734 RID: 1844
	public readonly PlatformInfo.Platform platform;

	// Token: 0x04000735 RID: 1845
	public readonly string serialzedName;

	// Token: 0x04000736 RID: 1846
	[DoesNotRequireDomainReloadInit]
	public static readonly PlatformInfo testPlatformInfo = new PlatformInfo(PlatformInfo.Platform.Test, "Test");

	// Token: 0x04000737 RID: 1847
	[DoesNotRequireDomainReloadInit]
	public static readonly PlatformInfo steamPlatformInfo = new PlatformInfo(PlatformInfo.Platform.Steam, "Steam");

	// Token: 0x04000738 RID: 1848
	[DoesNotRequireDomainReloadInit]
	public static readonly PlatformInfo oculusPlatformInfo = new PlatformInfo(PlatformInfo.Platform.Oculus, "Oculus");

	// Token: 0x04000739 RID: 1849
	[DoesNotRequireDomainReloadInit]
	public static readonly PlatformInfo ps4PlatformInfo = new PlatformInfo(PlatformInfo.Platform.PS4, "PS4");

	// Token: 0x020001B3 RID: 435
	public enum Platform : byte
	{
		// Token: 0x0400073B RID: 1851
		Test,
		// Token: 0x0400073C RID: 1852
		Steam,
		// Token: 0x0400073D RID: 1853
		Oculus,
		// Token: 0x0400073E RID: 1854
		PS4
	}
}
